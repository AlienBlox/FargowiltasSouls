// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Challengers.NavalRustrifle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.Challengers
{
  public class NavalRustrifle : SoulsItem
  {
    private const int ShotType = 242;
    private bool EmpoweredShot;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 116;
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 82;
      ((Entity) this.Item).height = 24;
      this.Item.useTime = 28;
      this.Item.useAnimation = 28;
      this.Item.useStyle = 5;
      this.Item.knockBack = 15f;
      this.Item.value = Item.sellPrice(0, 5, 0, 0);
      this.Item.rare = 4;
      this.Item.UseSound = new SoundStyle?(SoundID.Item40);
      this.Item.autoReuse = true;
      this.Item.shoot = 10;
      this.Item.shootSpeed = 30f;
      this.Item.useAmmo = AmmoID.Bullet;
      this.Item.noMelee = true;
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(0.0f, 0.0f));

    public virtual void UseStyle(Player player, Rectangle heldItemFrame)
    {
      if (player.FargoSouls().RustRifleReloading)
        return;
      player.itemRotation = Utils.ToRotation(Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_UnaryNegation(Vector2.UnitY), (float) ((Entity) player).direction), (double) ((Entity) player).direction * 3.1415927410125732 / 4.0, new Vector2()));
      Player player1 = player;
      Vector2 vector2_1 = Vector2.op_Subtraction(player.HandPosition.Value, Vector2.op_Division(((Entity) this.Item).Size, 2f));
      Vector2 vector2_2 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(player.itemRotation), (float) ((Entity) player).direction), (float) Math.Sin(3.1415927410125732 * (double) player.itemAnimation / (double) player.itemAnimationMax));
      Vector2 size = ((Entity) this.Item).Size;
      double num = (double) ((Vector2) ref size).Length();
      Vector2 vector2_3 = Vector2.op_Division(Vector2.op_Multiply(vector2_2, (float) num), 2f);
      Vector2 vector2_4 = Vector2.op_Subtraction(vector2_1, vector2_3);
      player1.itemLocation = vector2_4;
    }

    public virtual void ModifyShootStats(
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      if (!this.EmpoweredShot)
        return;
      type = 242;
      damage *= 2;
    }

    public virtual void ModifyWeaponCrit(Player player, ref float crit)
    {
      if (!this.EmpoweredShot)
        return;
      crit = 100f;
    }

    public virtual void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
    {
      if (!this.EmpoweredShot)
        return;
      knockback = StatModifier.op_Multiply(knockback, 3f);
    }

    public virtual bool CanUseItem(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!fargoSoulsPlayer.RustRifleReloading)
        return base.CanUseItem(player);
      if ((double) Math.Abs(NavalRustrifle.ReloadProgress(fargoSoulsPlayer.RustRifleTimer) - fargoSoulsPlayer.RustRifleReloadZonePos) < 0.15000000596046448)
      {
        this.EmpoweredShot = true;
        SoundStyle soundStyle = SoundID.Item149;
        ((SoundStyle) ref soundStyle).Pitch = 0.5f;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        this.Item.UseSound = new SoundStyle?(SoundID.Item68);
      }
      else
      {
        this.EmpoweredShot = false;
        SoundStyle unlock = SoundID.Unlock;
        ((SoundStyle) ref unlock).Pitch = 0.0f;
        SoundEngine.PlaySound(ref unlock, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        this.Item.UseSound = new SoundStyle?(SoundID.Item40);
      }
      fargoSoulsPlayer.RustRifleReloading = false;
      fargoSoulsPlayer.RustRifleTimer = 0.0f;
      fargoSoulsPlayer.RustRifleReloadZonePos = 0.0f;
      player.reuseDelay = 20;
      player.FargoSouls().RustRifleTimer = 0.0f;
      return false;
    }

    public virtual bool? UseItem(Player player)
    {
      player.FargoSouls().RustRifleReloading = true;
      player.FargoSouls().RustRifleReloadZonePos = 0.725f;
      player.FargoSouls().RustRifleTimer = 0.0f;
      return base.UseItem(player);
    }

    public static float ReloadProgress(float timer)
    {
      return (float) ((1.0 + Math.Sin(3.1415927410125732 * ((double) timer - 30.0) / 60.0)) / 2.0);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient<BanishedBaronBag>(2).AddTile(220).DisableDecraft().Register();
    }
  }
}
