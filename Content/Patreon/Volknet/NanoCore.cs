// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Volknet.NanoCore
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Patreon.Volknet.Projectiles;
using FargowiltasSouls.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Volknet
{
  public class NanoCore : PatreonModItem
  {
    private int switchCD;

    public override void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 60;
      ((Entity) this.Item).height = 30;
      this.Item.damage = 175;
      this.Item.knockBack = 2f;
      this.Item.channel = true;
      this.Item.useTime = 6;
      this.Item.useAnimation = 6;
      this.Item.value = Item.sellPrice(0, 15, 0, 0);
      this.Item.shoot = ModContent.ProjectileType<NanoBase>();
      this.Item.rare = 11;
      this.Item.useStyle = 5;
      this.Item.noUseGraphic = true;
      this.Item.noMelee = true;
      this.Item.autoReuse = true;
      this.Item.useTurn = true;
      this.Item.crit = 12;
    }

    public virtual bool? PrefixChance(int pre, UnifiedRandom rand) => new bool?(false);

    public override void SafePostDrawInWorld(
      SpriteBatch spriteBatch,
      Color lightColor,
      Color alphaColor,
      float rotation,
      float scale,
      int whoAmI)
    {
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Patreon/Volknet/NanoCoreGlow", (AssetRequestMode) 1).Value;
      spriteBatch.Draw(texture2D, Vector2.op_Subtraction(((Entity) this.Item).Center, Main.screenPosition), new Rectangle?(new Rectangle(0, 0, texture2D.Width, texture2D.Height)), Color.White, rotation, Vector2.op_Division(Utils.Size(texture2D), 2f), scale, (SpriteEffects) 0, 0.0f);
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public override void SafeModifyTooltips(List<TooltipLine> tooltips)
    {
      base.SafeModifyTooltips(tooltips);
      foreach (TooltipLine tooltip in tooltips)
      {
        if (tooltip.Mod == "Terraria" && tooltip.Name == "ItemName")
          tooltip.OverrideColor = new Color?(Main.DiscoColor);
      }
    }

    public virtual bool CanUseItem(Player player)
    {
      if (player.altFunctionUse == 2)
      {
        this.Item.autoReuse = false;
        this.Item.channel = false;
      }
      else
      {
        this.Item.autoReuse = true;
        this.Item.channel = true;
      }
      if (player.GetModPlayer<NanoPlayer>().NanoCoreMode == 1 && player.altFunctionUse != 2)
        this.Item.useAmmo = AmmoID.Arrow;
      else
        this.Item.useAmmo = AmmoID.None;
      return true;
    }

    public virtual void HoldItem(Player player)
    {
      if (this.switchCD > 0)
        --this.switchCD;
      int nanoCoreMode = player.GetModPlayer<NanoPlayer>().NanoCoreMode;
      switch (nanoCoreMode)
      {
        case 0:
          this.Item.DamageType = DamageClass.Melee;
          break;
        case 1:
          this.Item.DamageType = DamageClass.Ranged;
          break;
        case 2:
          this.Item.DamageType = DamageClass.Magic;
          break;
        default:
          this.Item.DamageType = DamageClass.Summon;
          break;
      }
      if (nanoCoreMode != 3)
      {
        ((Entity) player).direction = Math.Sign(Main.MouseWorld.X - ((Entity) player).Center.X);
        Vector2 vector2 = Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center));
        if (((Entity) player).direction < 0)
          vector2.Y = -vector2.Y;
        player.itemRotation = Utils.ToRotation(vector2);
      }
      if (nanoCoreMode == 1 && player.altFunctionUse != 2)
        this.Item.useAmmo = AmmoID.Arrow;
      else
        this.Item.useAmmo = AmmoID.None;
      if (((Entity) player).whoAmI != Main.myPlayer || player.ownedProjectileCounts[ModContent.ProjectileType<NanoBase>()] >= 1)
        return;
      int num = Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<NanoBase>(), player.HeldItem.damage, player.GetWeaponKnockback(player.HeldItem, 1f), ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      player.heldProj = num;
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
      if (player.altFunctionUse == 2 && this.switchCD <= 0)
      {
        this.switchCD = 12;
        player.GetModPlayer<NanoPlayer>().NanoCoreMode = (player.GetModPlayer<NanoPlayer>().NanoCoreMode + 1) % 4;
        foreach (Projectile projectile in Main.projectile)
        {
          if (((Entity) projectile).active && projectile.type == ModContent.ProjectileType<NanoProbe>() && projectile.owner == ((Entity) player).whoAmI)
            projectile.ai[1] = 0.0f;
        }
      }
      return false;
    }

    public virtual void AddRecipes()
    {
      if (!SoulConfig.Instance.PatreonNanoCore)
        return;
      this.CreateRecipe(1).AddIngredient(3368, 1).AddIngredient(4953, 1).AddIngredient(2882, 1).AddIngredient(2749, 1).AddIngredient(ModContent.ItemType<Eridanium>(), 99).AddIngredient(ModContent.ItemType<AbomEnergy>(), 99).AddIngredient(3358, 99).AddIngredient(3467, 99).AddIngredient(1346, 999).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
