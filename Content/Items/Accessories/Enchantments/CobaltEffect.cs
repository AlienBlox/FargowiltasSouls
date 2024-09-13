// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.CobaltEffect
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
  public class CobaltEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<EarthHeader>();

    public override int ToggleItemType => ModContent.ItemType<CobaltEnchant>();

    public override void OnHurt(Player player, Player.HurtInfo info)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      int num1 = 75;
      int num2 = 2;
      int num3 = 150;
      if (player.ForceEffect<CobaltEffect>())
      {
        num1 = 150;
        num2 = 4;
        num3 = 400;
      }
      if (fargoSoulsPlayer.TerrariaSoul)
      {
        num1 = 300;
        num2 = 5;
        num3 = 600;
      }
      int num4 = num1 + ((Player.HurtInfo) ref info).Damage * num2;
      if (num4 > num3)
        num4 = num3;
      Projectile projectile = FargoSoulsUtil.NewProjectileDirectSafe(player.GetSource_Accessory(player.EffectItem<CobaltEffect>(), (string) null), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<CobaltExplosion>(), (int) ((double) num4 * (double) player.ActualClassDamage(DamageClass.Melee)), 0.0f, Main.myPlayer);
      if (projectile == null)
        return;
      projectile.FargoSouls().CanSplit = false;
    }
  }
}
