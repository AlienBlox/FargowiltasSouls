// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.GladiatorBanner
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Minions;
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
  public class GladiatorBanner : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<WillHeader>();

    public override int ToggleItemType => ModContent.ItemType<GladiatorEnchant>();

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.GladiatorCD <= 0)
        return;
      --fargoSoulsPlayer.GladiatorCD;
    }

    public static void ActivateGladiatorBanner(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (((Entity) player).whoAmI != Main.myPlayer || !player.HasEffect<GladiatorBanner>())
        return;
      int index = ModContent.ProjectileType<GladiatorStandard>();
      if (player.ownedProjectileCounts[index] >= 1)
        return;
      Projectile.NewProjectile(((Entity) player).GetSource_Misc(""), ((Entity) player).Top, Vector2.op_Multiply(Vector2.UnitY, 25f), index, fargoSoulsPlayer.ForceEffect<GladiatorEnchant>() ? 300 : 100, 3f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }
  }
}
