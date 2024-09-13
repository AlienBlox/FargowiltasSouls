// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Tiles.FargoGlobalTile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Tiles
{
  public class FargoGlobalTile : GlobalTile
  {
    internal static bool InGameWorld(int x, int y)
    {
      return x > 39 && x < Main.maxTilesX - 39 && y > 39 && y < Main.maxTilesY - 39;
    }

    internal static bool InWorld(int x, int y)
    {
      return x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY;
    }

    internal static bool NoDungeon(int x, int y)
    {
      return FargoGlobalTile.NoBlueDungeon(x, y) && FargoGlobalTile.NoGreenDungeon(x, y) && FargoGlobalTile.NoPinkDungeon(x, y);
    }

    internal static bool NoBlueDungeon(int x, int y)
    {
      Tile tile = ((Tilemap) ref Main.tile)[x, y];
      return ((Tile) ref tile).TileType != (ushort) 41 && ((Tile) ref tile).WallType != (ushort) 94 && ((Tile) ref tile).WallType != (ushort) 95 && ((Tile) ref tile).WallType != (ushort) 7;
    }

    internal static bool NoGreenDungeon(int x, int y)
    {
      Tile tile = ((Tilemap) ref Main.tile)[x, y];
      return ((Tile) ref tile).TileType != (ushort) 43 && ((Tile) ref tile).WallType != (ushort) 98 && ((Tile) ref tile).WallType != (ushort) 99 && ((Tile) ref tile).WallType != (ushort) 8;
    }

    internal static bool NoPinkDungeon(int x, int y)
    {
      Tile tile = ((Tilemap) ref Main.tile)[x, y];
      return ((Tile) ref tile).TileType != (ushort) 44 && ((Tile) ref tile).WallType != (ushort) 96 && ((Tile) ref tile).WallType != (ushort) 97 && ((Tile) ref tile).WallType != (ushort) 9;
    }

    internal static bool NoUndergroundDesert(int x, int y)
    {
      Tile tile = ((Tilemap) ref Main.tile)[x, y];
      int num = (int) ((Tile) ref tile).WallType;
      switch (num)
      {
        case 187:
        case 220:
        case 221:
          return false;
        default:
          return num != 222;
      }
    }

    internal static bool PlanteraBulb(int x, int y)
    {
      Tile tile = ((Tilemap) ref Main.tile)[x, y];
      return ((Tile) ref tile).TileType == (ushort) 238;
    }

    internal static bool NoTemple(int x, int y)
    {
      Tile tile = ((Tilemap) ref Main.tile)[x, y];
      if (((Tile) ref tile).WallType == (ushort) 87 || ((Tile) ref tile).TileType == (ushort) 226)
        return false;
      return ((Tile) ref tile).TileType != (ushort) 10 || ((Tile) ref tile).TileFrameY < (short) 594 || ((Tile) ref tile).TileFrameY > (short) 646;
    }

    internal static bool Temple(int x, int y) => !FargoGlobalTile.NoTemple(x, y);

    internal static bool TempleAndGolemIsDead(int x, int y)
    {
      return !FargoGlobalTile.NoTemple(x, y) && NPC.downedGolemBoss;
    }

    internal static bool NoTempleOrGolemIsDead(int x, int y)
    {
      return FargoGlobalTile.NoTemple(x, y) || NPC.downedGolemBoss;
    }

    internal static bool NoOrbOrAltar(int x, int y)
    {
      Tile tile1 = ((Tilemap) ref Main.tile)[x, y];
      if (((Tile) ref tile1).TileType == (ushort) 31)
        return false;
      Tile tile2 = ((Tilemap) ref Main.tile)[x, y];
      return ((Tile) ref tile2).TileType != (ushort) 26;
    }

    internal static bool SolidTile(int x, int y)
    {
      Tile tile = ((Tilemap) ref Main.tile)[x, y];
      return ((Tile) ref tile).HasTile && Main.tileSolid[(int) ((Tile) ref tile).TileType] && !Main.tileSolidTop[(int) ((Tile) ref tile).TileType] && !((Tile) ref tile).IsHalfBlock && ((Tile) ref tile).Slope == null && !((Tile) ref tile).HasUnactuatedTile;
    }
  }
}
