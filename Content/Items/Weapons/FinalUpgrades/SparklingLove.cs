// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.FinalUpgrades.SparklingLove
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.FinalUpgrades
{
  public class SparklingLove : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 1700;
      this.Item.useStyle = 1;
      this.Item.useAnimation = 27;
      this.Item.useTime = 27;
      this.Item.shootSpeed = 16f;
      this.Item.knockBack = 14f;
      ((Entity) this.Item).width = 32;
      ((Entity) this.Item).height = 32;
      this.Item.scale = 2f;
      this.Item.rare = 11;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.shoot = ModContent.ProjectileType<FargowiltasSouls.Content.Projectiles.BossWeapons.SparklingLove>();
      this.Item.value = Item.sellPrice(0, 70, 0, 0);
      this.Item.noMelee = true;
      this.Item.noUseGraphic = true;
      this.Item.DamageType = DamageClass.Melee;
      this.Item.autoReuse = true;
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool CanUseItem(Player player)
    {
      if (player.altFunctionUse == 2)
      {
        this.Item.shoot = ModContent.ProjectileType<SparklingDevi>();
        this.Item.useStyle = 1;
        this.Item.DamageType = DamageClass.Summon;
        this.Item.noUseGraphic = false;
        this.Item.noMelee = false;
        this.Item.useAnimation = 66;
        this.Item.useTime = 66;
        this.Item.mana = 100;
      }
      else
      {
        this.Item.shoot = ModContent.ProjectileType<FargowiltasSouls.Content.Projectiles.BossWeapons.SparklingLove>();
        this.Item.useStyle = 1;
        this.Item.DamageType = DamageClass.Melee;
        this.Item.noUseGraphic = true;
        this.Item.noMelee = true;
        this.Item.useAnimation = 27;
        this.Item.useTime = 27;
        this.Item.mana = 0;
      }
      return true;
    }

    public virtual bool Shoot(
      Player player,
      EntitySource_ItemUse_WithAmmo source,
      Vector2 position,
      Vector2 velocity,
      int type,
      int damage,
      float knockback)
    {
      if (player.altFunctionUse != 2)
        return base.Shoot(player, source, position, velocity, type, damage, knockback);
      FargoSoulsUtil.NewSummonProjectile((IEntitySource) source, position, velocity, type, this.Item.damage, knockback, ((Entity) player).whoAmI);
      return false;
    }

    public virtual bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
    {
      if (!(((TooltipLine) line).Mod == "Terraria") || !(((TooltipLine) line).Name == "ItemName"))
        return true;
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 1, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
      shader.TrySetParameter("mainColor", (object) new Color((int) byte.MaxValue, 48, 154));
      shader.TrySetParameter("secondaryColor", (object) new Color((int) byte.MaxValue, 169, 240));
      shader.Apply("PulseCircle");
      Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2((float) line.X, (float) line.Y), new Color((int) byte.MaxValue, 169, 240), 1f, 0.0f, 0.0f, -1);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<EternalEnergy>(), 30).AddIngredient(ModContent.ItemType<AbomEnergy>(), 30).AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 30).AddIngredient(ModContent.ItemType<BrokenBlade>(), 1).AddIngredient(ModContent.ItemType<SparklingAdoration>(), 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
