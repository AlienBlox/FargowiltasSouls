// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.AbomWandCrit
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class AbomWandCrit : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<DeviEnergyHeader>();

    public override int ToggleItemType => ModContent.ItemType<AbominableWand>();

    public override bool ExtraAttackEffect => true;

    public override void OnHitNPCEither(
      Player player,
      NPC target,
      NPC.HitInfo hitInfo,
      DamageClass damageClass,
      int baseDamage,
      Projectile projectile,
      Item item)
    {
      Player player1 = player;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!hitInfo.Crit || fargoSoulsPlayer.AbomWandCD > 0 || projectile != null && projectile.type == ModContent.ProjectileType<AbomScytheFriendly>())
        return;
      fargoSoulsPlayer.AbomWandCD = 360;
      float x = Main.screenPosition.X;
      if (((Entity) player1).direction < 0)
        x += (float) Main.screenWidth;
      float num1 = Main.screenPosition.Y + (float) Main.rand.Next(Main.screenHeight);
      Vector2 spawn;
      // ISSUE: explicit constructor call
      ((Vector2) ref spawn).\u002Ector(x, num1);
      Vector2 vector2 = Vector2.op_Subtraction(((Entity) target).Center, spawn);
      ((Vector2) ref vector2).Normalize();
      Vector2 velocity = Vector2.op_Multiply(vector2, 27f);
      int rawBaseDamage = 150;
      if (fargoSoulsPlayer.MutantEyeItem != null)
        rawBaseDamage *= 3;
      if (projectile != null && FargoSoulsUtil.IsSummonDamage(projectile))
      {
        int index = FargoSoulsUtil.NewSummonProjectile(player1.GetSource_EffectItem<AbomWandCrit>(), spawn, velocity, ModContent.ProjectileType<SpectralAbominationn>(), rawBaseDamage, 10f, ((Entity) player1).whoAmI, (float) ((Entity) target).whoAmI);
        if (index == Main.maxProjectiles)
          return;
        Main.projectile[index].DamageType = DamageClass.Summon;
      }
      else
      {
        int num2 = (int) ((double) rawBaseDamage * (double) player1.ActualClassDamage(damageClass));
        int index = Projectile.NewProjectile(player1.GetSource_EffectItem<AbomWandCrit>(), spawn, velocity, ModContent.ProjectileType<SpectralAbominationn>(), num2, 10f, ((Entity) player1).whoAmI, (float) ((Entity) target).whoAmI, 0.0f, 0.0f);
        if (index == Main.maxProjectiles)
          return;
        Main.projectile[index].DamageType = damageClass;
      }
    }
  }
}
