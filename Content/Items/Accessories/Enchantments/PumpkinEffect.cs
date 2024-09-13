// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.PumpkinEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class PumpkinEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<LifeHeader>();

    public override int ToggleItemType => ModContent.ItemType<PumpkinEnchant>();

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if ((player.controlLeft || player.controlRight) && !fargoSoulsPlayer.IsStandingStill && ((Entity) player).whoAmI == Main.myPlayer && fargoSoulsPlayer.PumpkinSpawnCD <= 0 && player.ownedProjectileCounts[ModContent.ProjectileType<GrowingPumpkin>()] < 10)
      {
        int num1 = (int) ((Entity) player).Center.X / 16;
        int num2 = (int) ((double) ((Entity) player).position.Y + (double) ((Entity) player).height - 1.0) / 16;
        Tile tile = ((Tilemap) ref Main.tile)[num1, num2];
        if (!((Tile) ref tile).HasTile)
        {
          tile = ((Tilemap) ref Main.tile)[num1, num2];
          if (((Tile) ref tile).LiquidType == 0 && Tile.op_Inequality(((Tilemap) ref Main.tile)[num1, num2 + 1], (ArgumentException) null))
          {
            if (!WorldGen.SolidTile(num1, num2 + 1, false))
            {
              tile = ((Tilemap) ref Main.tile)[num1, num2 + 1];
              if (((Tile) ref tile).TileType == (ushort) 19)
                goto label_6;
            }
            else
              goto label_6;
          }
        }
        if (!fargoSoulsPlayer.ForceEffect<PumpkinEnchant>())
          goto label_7;
label_6:
        Projectile.NewProjectile(player.GetSource_Accessory(player.EffectItem<PumpkinEffect>(), (string) null), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<GrowingPumpkin>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
        fargoSoulsPlayer.PumpkinSpawnCD = Luminance.Common.Utilities.Utilities.SecondsToFrames(7.5f);
      }
label_7:
      if (fargoSoulsPlayer.PumpkinSpawnCD <= 0)
        return;
      --fargoSoulsPlayer.PumpkinSpawnCD;
    }
  }
}
