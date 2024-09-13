// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.CelestialRuneAttacks
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class CelestialRuneAttacks : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<ChaliceHeader>();

    public override int ToggleItemType => ModContent.ItemType<CelestialRune>();

    public override bool ExtraAttackEffect => true;

    public override void TryAdditionalAttacks(Player player, int damage, DamageClass damageType)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (((Entity) player).whoAmI != Main.myPlayer || fargoSoulsPlayer.AdditionalAttacksTimer > 0)
        return;
      fargoSoulsPlayer.AdditionalAttacksTimer = 60;
      Vector2 center = ((Entity) player).Center;
      Vector2 vector2_1 = Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, center));
      if (damageType.CountsAsClass(DamageClass.Melee))
      {
        SoundEngine.PlaySound(ref SoundID.Item34, new Vector2?(center), (SoundUpdateCallback) null);
        for (int index = 0; index < 3; ++index)
          Projectile.NewProjectile(this.GetSource_EffectItem(player), center, Vector2.op_Multiply(Utils.RotatedByRandom(vector2_1, Math.PI / 6.0), Utils.NextFloat(Main.rand, 6f, 10f)), ModContent.ProjectileType<CelestialRuneFireball>(), (int) (65.0 * (double) player.ActualClassDamage(DamageClass.Melee)), 9f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
      if (damageType.CountsAsClass(DamageClass.Ranged))
      {
        for (int index = -1; index <= 1; ++index)
        {
          float num = (float) Main.rand.Next(100);
          Vector2 vector2_2 = Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(Utils.RotatedByRandom(vector2_1, Math.PI / 4.0)), (double) MathHelper.ToRadians(5f) * (double) index, new Vector2()), 7f);
          Projectile.NewProjectile(this.GetSource_EffectItem(player), center, vector2_2, ModContent.ProjectileType<CelestialRuneLightningArc>(), (int) (65.0 * (double) player.ActualClassDamage(DamageClass.Ranged)), 1f, ((Entity) player).whoAmI, Utils.ToRotation(vector2_1), num, 0.0f);
        }
      }
      if (damageType.CountsAsClass(DamageClass.Magic))
        Projectile.NewProjectile(this.GetSource_EffectItem(player), center, Vector2.op_Multiply(vector2_1, 4.25f), ModContent.ProjectileType<CelestialRuneIceMist>(), (int) (65.0 * (double) player.ActualClassDamage(DamageClass.Magic)), 4f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      if (!damageType.CountsAsClass(DamageClass.Summon))
        return;
      FargoSoulsUtil.NewSummonProjectile(this.GetSource_EffectItem(player), center, Vector2.op_Multiply(vector2_1, 16f), ModContent.ProjectileType<CelestialRuneAncientVision>(), 65, 3f, ((Entity) player).whoAmI);
    }

    public override void OnHurt(Player player, Player.HurtInfo info)
    {
      int damage = ((Player.HurtInfo) ref info).Damage;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.HurtTimer > 0)
        return;
      fargoSoulsPlayer.HurtTimer = 20;
      if (fargoSoulsPlayer.MoonChalice)
      {
        int num = 50;
        if (fargoSoulsPlayer.MasochistSoul)
          num *= 2;
        for (int index = 0; index < 5; ++index)
          Projectile.NewProjectile(this.GetSource_EffectItem(player), ((Entity) player).Center, Utils.NextVector2Circular(Main.rand, 20f, 20f), ModContent.ProjectileType<AncientVision>(), (int) ((double) num * (double) player.ActualClassDamage(DamageClass.Summon)), 6f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
      else
        Projectile.NewProjectile(this.GetSource_EffectItem(player), ((Entity) player).Center, new Vector2(0.0f, -10f), ModContent.ProjectileType<AncientVision>(), (int) (40.0 * (double) player.ActualClassDamage(DamageClass.Summon)), 3f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }
  }
}
