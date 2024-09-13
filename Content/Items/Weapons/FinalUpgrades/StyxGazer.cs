// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.FinalUpgrades.StyxGazer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.FinalUpgrades
{
  public class StyxGazer : SoulsItem
  {
    public bool flip;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 1700;
      this.Item.useStyle = 1;
      this.Item.useAnimation = 22;
      this.Item.useTime = 22;
      this.Item.shootSpeed = 16f;
      this.Item.knockBack = 14f;
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.scale = 1f;
      this.Item.rare = 11;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.shoot = ModContent.ProjectileType<StyxScythe>();
      this.Item.value = Item.sellPrice(0, 70, 0, 0);
      this.Item.DamageType = DamageClass.Melee;
      this.Item.autoReuse = true;
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool CanUseItem(Player player)
    {
      if (player.altFunctionUse == 2)
      {
        this.Item.shoot = ModContent.ProjectileType<FargowiltasSouls.Content.Projectiles.BossWeapons.StyxGazer>();
        this.Item.useStyle = 5;
        this.Item.DamageType = DamageClass.Magic;
        this.Item.noUseGraphic = true;
        this.Item.noMelee = true;
        this.Item.mana = 200;
      }
      else
      {
        this.Item.shoot = ModContent.ProjectileType<StyxScythe>();
        this.Item.useStyle = 1;
        this.Item.DamageType = DamageClass.Melee;
        this.Item.noUseGraphic = false;
        this.Item.noMelee = false;
        this.Item.mana = 0;
      }
      return true;
    }

    public virtual bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
    {
      if (!(((TooltipLine) line).Mod == "Terraria") || !(((TooltipLine) line).Name == "ItemName"))
        return true;
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 1, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
      shader.TrySetParameter("mainColor", (object) new Color((int) byte.MaxValue, 170, 12));
      shader.TrySetParameter("secondaryColor", (object) new Color(210, 69, 203));
      shader.Apply("PulseDiagonal");
      Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2((float) line.X, (float) line.Y), Color.White, 1f, 0.0f, 0.0f, -1);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
      return false;
    }

    public virtual string Texture => base.Texture;

    public virtual bool Shoot(
      Player player,
      EntitySource_ItemUse_WithAmmo source,
      Vector2 position,
      Vector2 velocity,
      int type,
      int damage,
      float knockback)
    {
      this.flip = !this.flip;
      if (player.altFunctionUse == 2)
      {
        velocity = Utils.RotatedBy(velocity, Math.PI / 2.0 * (this.flip ? 1.0 : -1.0), new Vector2());
        Projectile.NewProjectile((IEntitySource) source, position, velocity, type, damage, knockback, ((Entity) player).whoAmI, (float) (0.02617993950843811 * (this.flip ? -1.0 : 1.0)), 0.0f, 0.0f);
      }
      else
      {
        for (int index = 0; index < 5; ++index)
        {
          EntitySource_ItemUse_WithAmmo sourceItemUseWithAmmo = source;
          Vector2 vector2_1 = position;
          Vector2 vector2_2 = Utils.RotatedBy(velocity, 2.0 * Math.PI / 5.0 * (double) index, new Vector2());
          int num1 = type;
          int num2 = damage;
          double num3 = (double) knockback;
          int whoAmI = ((Entity) player).whoAmI;
          Vector2 vector2_3 = Vector2.op_Subtraction(Main.MouseWorld, position);
          double num4 = (double) ((Vector2) ref vector2_3).Length() * (this.flip ? 1.0 : -1.0);
          Projectile.NewProjectile((IEntitySource) sourceItemUseWithAmmo, vector2_1, vector2_2, num1, num2, (float) num3, whoAmI, 0.0f, (float) num4, 0.0f);
        }
      }
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<EternalEnergy>(), 30).AddIngredient(ModContent.ItemType<AbomEnergy>(), 30).AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 30).AddIngredient(ModContent.ItemType<BrokenHilt>(), 1).AddIngredient(ModContent.ItemType<AbominableWand>(), 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
