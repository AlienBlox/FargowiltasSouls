// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.WyvernBalls
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
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
  public class WyvernBalls : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<BionomicHeader>();

    public override int ToggleItemType => ModContent.ItemType<WyvernFeather>();

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if ((double) ((Entity) player).velocity.Y == 0.0 || ++fargoSoulsPlayer.WyvernBallsCD <= 180)
        return;
      fargoSoulsPlayer.WyvernBallsCD = 0;
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      Projectile.NewProjectile(this.GetSource_EffectItem(player), ((Entity) player).Center, Vector2.op_Multiply(Utils.NextFloat(Main.rand, 6f, 12f), Utils.RotatedByRandom(Vector2.UnitX, 6.2831854820251465)), ModContent.ProjectileType<FlightBall>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }
  }
}
