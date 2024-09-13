// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.AdamantiteEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Weapons.Challengers;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class AdamantiteEffect : AccessoryEffect
  {
    public static int[] AdamIgnoreItems = new int[6]
    {
      273,
      675,
      368,
      674,
      757,
      ModContent.ItemType<DecrepitAirstrikeRemote>()
    };

    public override Header ToggleHeader => (Header) Header.GetHeader<EarthHeader>();

    public override int ToggleItemType => ModContent.ItemType<AdamantiteEnchant>();

    public override bool ExtraAttackEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      int num = 60;
      if (fargoSoulsPlayer.WeaponUseTimer > 0)
        fargoSoulsPlayer.AdamantiteSpread += (double) num / 60.0 / 10.0;
      else
        fargoSoulsPlayer.AdamantiteSpread -= (double) num / 60.0 / 1.5;
      if (fargoSoulsPlayer.AdamantiteSpread < 0.0)
        fargoSoulsPlayer.AdamantiteSpread = 0.0;
      if (fargoSoulsPlayer.AdamantiteSpread <= (double) num)
        return;
      fargoSoulsPlayer.AdamantiteSpread = (double) num;
    }

    public static void AdamantiteSplit(
      Projectile projectile,
      FargoSoulsPlayer modPlayer,
      int splitDegreeAngle)
    {
      bool flag1 = modPlayer.ForceEffect<AdamantiteEnchant>();
      bool flag2 = ProjectileID.Sets.CultistIsResistantTo[projectile.type];
      if (((IEnumerable<int>) AdamantiteEffect.AdamIgnoreItems).Contains<int>(modPlayer.Player.HeldItem.type))
        return;
      float damageRatio = flag2 ? (flag1 ? 0.375f : 0.6f) : (flag1 ? 0.5f : 0.7f);
      foreach (Projectile projectile1 in FargoSoulsGlobalProjectile.SplitProj(projectile, 3, MathHelper.ToRadians((float) splitDegreeAngle), damageRatio))
      {
        if (projectile1.Alive())
          projectile1.FargoSouls().HuntressProj = projectile.FargoSouls().HuntressProj;
      }
      if (!flag1)
      {
        projectile.type = 0;
        projectile.timeLeft = 0;
        ((Entity) projectile).active = false;
      }
      else
        projectile.damage = (int) ((double) projectile.damage * (double) damageRatio);
    }
  }
}
