// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Systems.WorldGenSystem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Tiles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.Systems
{
  public class WorldGenSystem : ModSystem
  {
    public static bool TryPlacingStatue(int baseCheckX, int baseCheckY)
    {
      List<int> intList1 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList1, 11);
      Span<int> span = CollectionsMarshal.AsSpan<int>(intList1);
      int num1 = 0;
      span[num1] = 1;
      int num2 = num1 + 1;
      span[num2] = 2;
      int num3 = num2 + 1;
      span[num3] = 0;
      int num4 = num3 + 1;
      span[num4] = 147;
      int num5 = num4 + 1;
      span[num5] = 161;
      int num6 = num5 + 1;
      span[num6] = 40;
      int num7 = num6 + 1;
      span[num7] = 59;
      int num8 = num7 + 1;
      span[num8] = 60;
      int num9 = num8 + 1;
      span[num9] = 53;
      int num10 = num9 + 1;
      span[num10] = 57;
      int num11 = num10 + 1;
      span[num11] = 633;
      int num12 = num11 + 1;
      List<int> intList2 = intList1;
      bool flag = true;
      for (int index1 = 0; index1 < 3; ++index1)
      {
        for (int index2 = 0; index2 < 4; ++index2)
        {
          if (WorldGen.SolidOrSlopedTile(Framing.GetTileSafely(baseCheckX + index1, baseCheckY + index2)))
          {
            flag = false;
            break;
          }
        }
      }
      for (int index = 0; index < 3; ++index)
      {
        Tile tileSafely = Framing.GetTileSafely(baseCheckX + index, baseCheckY + 4);
        if (!WorldGen.SolidTile(tileSafely) || !intList2.Contains((int) ((Tile) ref tileSafely).TileType))
        {
          flag = false;
          break;
        }
      }
      if (!flag)
        return false;
      for (int index3 = 0; index3 < 3; ++index3)
      {
        for (int index4 = 0; index4 < 4; ++index4)
          WorldGen.KillTile(baseCheckX + index3, baseCheckY + index4, false, false, false);
      }
      WorldGen.PlaceTile(baseCheckX, baseCheckY + 4, 38, false, true, -1, 0);
      WorldGen.PlaceTile(baseCheckX + 1, baseCheckY + 4, 38, false, true, -1, 0);
      WorldGen.PlaceTile(baseCheckX + 2, baseCheckY + 4, 38, false, true, -1, 0);
      Tile tile = ((Tilemap) ref Main.tile)[baseCheckX, baseCheckY + 4];
      ((Tile) ref tile).Slope = (SlopeType) 0;
      tile = ((Tilemap) ref Main.tile)[baseCheckX + 1, baseCheckY + 4];
      ((Tile) ref tile).Slope = (SlopeType) 0;
      tile = ((Tilemap) ref Main.tile)[baseCheckX + 2, baseCheckY + 4];
      ((Tile) ref tile).Slope = (SlopeType) 0;
      WorldGen.PlaceTile(baseCheckX + 1, baseCheckY + 3, ModContent.TileType<MutantStatueGift>(), false, true, -1, 0);
      return true;
    }

    public virtual void PostWorldGen()
    {
      int num1 = Main.spawnTileX - 1;
      int num2 = Main.spawnTileY - 4;
      int num3 = -30;
      int num4 = 10;
      bool flag = false;
      for (int index1 = -50; index1 <= 50; ++index1)
      {
        for (int index2 = num3; index2 <= num4; ++index2)
        {
          if (WorldGenSystem.TryPlacingStatue(num1 + index1, num2 + index2))
          {
            flag = true;
            WorldSavingSystem.PlacedMutantStatue = true;
            break;
          }
        }
        if (flag)
          break;
      }
    }
  }
}
