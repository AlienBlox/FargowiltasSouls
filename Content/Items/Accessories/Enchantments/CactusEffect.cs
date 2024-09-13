// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.CactusEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class CactusEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<LifeHeader>();

    public override int ToggleItemType => ModContent.ItemType<CactusEnchant>();

    public override bool ExtraAttackEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.CactusProcCD <= 0)
        return;
      --fargoSoulsPlayer.CactusProcCD;
    }

    public override void TryAdditionalAttacks(Player player, int damage, DamageClass damageType)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.CactusProcCD != 0)
        return;
      CactusEffect.CactusSpray(player, ((Entity) player).Center);
      fargoSoulsPlayer.CactusProcCD = 15;
    }

    public static void CactusProc(NPC npc, Player player)
    {
      CactusEffect.CactusSpray(player, ((Entity) npc).Center);
    }

    private static void CactusSpray(Player player, Vector2 position)
    {
      int dmg = 20;
      int num = 8;
      if (player.FargoSouls().ForceEffect<CactusEnchant>())
      {
        dmg = 75;
        num = 16;
      }
      for (int index1 = 0; index1 < num; ++index1)
      {
        int index2 = Projectile.NewProjectile(player.GetSource_EffectItem<CactusEffect>(), ((Entity) player).Center, Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 6.2831854820251465), 4f), ModContent.ProjectileType<CactusNeedle>(), FargoSoulsUtil.HighestDamageTypeScaling(player, dmg), 5f, -1, 0.0f, 0.0f, 0.0f);
        if (index2 != Main.maxProjectiles)
        {
          Projectile projectile = Main.projectile[index2];
          if (projectile != null && ((Entity) projectile).active)
          {
            projectile.FargoSouls().CanSplit = false;
            projectile.ai[0] = 1f;
          }
        }
      }
    }
  }
}
