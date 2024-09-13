// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Expert.PrimeSoulEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Minions;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Expert
{
  public class PrimeSoulEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) null;

    public override void PostUpdateEquips(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer || player.ownedProjectileCounts[ModContent.ProjectileType<MinosPrime>()] >= 1)
        return;
      FargoSoulsUtil.NewSummonProjectile(this.GetSource_EffectItem(player), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<MinosPrime>(), 30, 10f, Main.myPlayer);
    }
  }
}
