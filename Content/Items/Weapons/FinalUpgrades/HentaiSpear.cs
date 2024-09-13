// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.FinalUpgrades.HentaiSpear
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
  public class HentaiSpear : SoulsItem
  {
    private int forceSwordTimer;

    public virtual void SetStaticDefaults()
    {
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(3, 10, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 1700;
      this.Item.useStyle = 5;
      this.Item.useAnimation = 16;
      this.Item.useTime = 16;
      this.Item.shootSpeed = 6f;
      this.Item.knockBack = 7f;
      ((Entity) this.Item).width = 72;
      ((Entity) this.Item).height = 72;
      this.Item.rare = 11;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.shoot = ModContent.ProjectileType<FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSpear>();
      this.Item.value = Item.sellPrice(0, 70, 0, 0);
      this.Item.noMelee = true;
      this.Item.noUseGraphic = true;
      this.Item.DamageType = DamageClass.Melee;
      this.Item.autoReuse = true;
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool CanUseItem(Player player)
    {
      this.Item.useStyle = 5;
      this.Item.useTurn = false;
      if (this.forceSwordTimer > 0)
      {
        this.Item.shoot = ModContent.ProjectileType<HentaiSword>();
        this.Item.shootSpeed = 6f;
        this.Item.useAnimation = 16;
        this.Item.useTime = 16;
        this.Item.useStyle = 1;
        this.Item.DamageType = DamageClass.Melee;
      }
      else if (player.altFunctionUse == 2)
      {
        if (player.controlUp && player.controlDown)
        {
          this.Item.shoot = ModContent.ProjectileType<HentaiSpearWand>();
          this.Item.shootSpeed = 6f;
          this.Item.useAnimation = 16;
          this.Item.useTime = 16;
        }
        else if (player.controlUp && !player.controlDown)
        {
          this.Item.shoot = ModContent.ProjectileType<HentaiSpearSpinBoundary>();
          this.Item.shootSpeed = 1f;
          this.Item.useAnimation = 16;
          this.Item.useTime = 16;
          this.Item.useTurn = true;
        }
        else if (player.controlDown && !player.controlUp)
        {
          this.Item.shoot = ModContent.ProjectileType<HentaiSpearSpinBoundary>();
          this.Item.shootSpeed = 1f;
          this.Item.useAnimation = 16;
          this.Item.useTime = 16;
          this.Item.useTurn = true;
        }
        else
        {
          this.Item.shoot = ModContent.ProjectileType<HentaiSpearThrown>();
          this.Item.shootSpeed = 25f;
          this.Item.useAnimation = 85;
          this.Item.useTime = 85;
        }
        this.Item.DamageType = DamageClass.Ranged;
      }
      else
      {
        if (player.controlUp && !player.controlDown)
        {
          this.Item.shoot = ModContent.ProjectileType<HentaiSpearSpin>();
          this.Item.shootSpeed = 1f;
          this.Item.useTurn = true;
        }
        else if (player.controlDown && !player.controlUp)
        {
          this.Item.shoot = ModContent.ProjectileType<HentaiSpearDive>();
          this.Item.shootSpeed = 6f;
        }
        else if (player.controlDown && player.controlUp)
        {
          this.Item.shoot = ModContent.ProjectileType<FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSpear>();
          this.Item.shootSpeed = 6f;
        }
        else
        {
          this.Item.shoot = ModContent.ProjectileType<HentaiSword>();
          this.Item.shootSpeed = 6f;
          this.Item.useStyle = 1;
        }
        this.Item.useAnimation = 16;
        this.Item.useTime = 16;
        this.Item.DamageType = DamageClass.Melee;
      }
      return true;
    }

    public virtual void UpdateInventory(Player player)
    {
      if (this.forceSwordTimer > 0)
        --this.forceSwordTimer;
      if (player.ownedProjectileCounts[ModContent.ProjectileType<HentaiSword>()] <= 0)
        return;
      this.forceSwordTimer = 3;
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
      if (this.forceSwordTimer > 0 || player.altFunctionUse != 2 && !player.controlUp && !player.controlDown)
      {
        // ISSUE: explicit constructor call
        ((Vector2) ref velocity).\u002Ector((double) velocity.X < 0.0 ? 1f : -1f, -1f);
        ((Vector2) ref velocity).Normalize();
        velocity = Vector2.op_Multiply(velocity, 80f);
        Projectile.NewProjectile((IEntitySource) source, position, velocity, type, damage, knockback, ((Entity) player).whoAmI, (float) -Math.Sign(velocity.X), 0.0f, 0.0f);
        return false;
      }
      if (player.altFunctionUse == 2)
      {
        if (!player.controlUp)
          return true;
        if (player.controlDown)
          return player.ownedProjectileCounts[this.Item.shoot] < 1;
        Projectile.NewProjectile((IEntitySource) source, position, velocity, type, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 1f);
        return false;
      }
      if (player.ownedProjectileCounts[this.Item.shoot] < 1)
      {
        if (player.controlUp && !player.controlDown)
          return true;
        if (player.ownedProjectileCounts[ModContent.ProjectileType<Dash>()] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<Dash2>()] < 1)
        {
          float num1 = 0.0f;
          float num2 = 2f;
          int num3 = ModContent.ProjectileType<Dash>();
          if (player.controlUp && player.controlDown)
          {
            num1 = 1f;
            num2 = 2.5f;
          }
          Vector2 vector2 = velocity;
          if (player.controlDown && !player.controlUp)
          {
            num1 = 2f;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2).\u002Ector((float) Math.Sign(velocity.X) * 0.0001f, ((Vector2) ref vector2).Length());
            num3 = ModContent.ProjectileType<Dash2>();
          }
          int index = Projectile.NewProjectile((IEntitySource) source, position, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(vector2), num2), this.Item.shootSpeed), num3, damage, knockback, ((Entity) player).whoAmI, Utils.ToRotation(vector2), num1, 0.0f);
          if (index != Main.maxProjectiles)
            Projectile.NewProjectile((IEntitySource) source, position, vector2, this.Item.shoot, damage, knockback, ((Entity) player).whoAmI, (float) Main.projectile[index].identity, 1f, 0.0f);
        }
      }
      return false;
    }

    public virtual bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
    {
      if (!(((TooltipLine) line).Mod == "Terraria") || !(((TooltipLine) line).Name == "ItemName"))
        return true;
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 1, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
      shader.TrySetParameter("mainColor", (object) new Color(28, 222, 152));
      shader.TrySetParameter("secondaryColor", (object) new Color(168, 245, 228));
      shader.Apply("PulseUpwards");
      Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2((float) line.X, (float) line.Y), Color.White, 1f, 0.0f, 0.0f, -1);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerMoon"), 1).AddIngredient(ModContent.ItemType<EternalEnergy>(), 30).AddIngredient(ModContent.ItemType<AbomEnergy>(), 30).AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 30).AddIngredient(ModContent.ItemType<PhantasmalEnergy>(), 1).AddIngredient(ModContent.ItemType<MutantEye>(), 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
