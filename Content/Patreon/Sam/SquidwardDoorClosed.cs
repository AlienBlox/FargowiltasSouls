// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Sam.SquidwardDoorClosed
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Sam
{
  public class SquidwardDoorClosed : ModTile
  {
    public virtual void SetStaticDefaults()
    {
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      Main.tileBlockLight[(int) ((ModBlockType) this).Type] = true;
      Main.tileSolid[(int) ((ModBlockType) this).Type] = true;
      Main.tileNoAttach[(int) ((ModBlockType) this).Type] = true;
      Main.tileLavaDeath[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.NotReallySolid[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.DrawsWalls[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.HasOutlines[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.Width = 1;
      TileObjectData.newTile.Height = 3;
      TileObjectData.newTile.Origin = new Point16(0, 0);
      TileObjectData.newTile.AnchorTop = new AnchorData((AnchorType) 1, TileObjectData.newTile.Width, 0);
      TileObjectData.newTile.AnchorBottom = new AnchorData((AnchorType) 1, TileObjectData.newTile.Width, 0);
      TileObjectData.newTile.UsesCustomCanPlace = true;
      TileObjectData.newTile.LavaDeath = true;
      TileObjectData.newTile.CoordinateHeights = new int[3]
      {
        16,
        16,
        16
      };
      TileObjectData.newTile.CoordinateWidth = 16;
      TileObjectData.newTile.CoordinatePadding = 2;
      TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
      TileObjectData.newAlternate.Origin = new Point16(0, 1);
      TileObjectData.addAlternate(0);
      TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
      TileObjectData.newAlternate.Origin = new Point16(0, 2);
      TileObjectData.addAlternate(0);
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      this.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
      this.AddMapEntry(new Color(200, 200, 200), ((ModBlockType) this).CreateMapEntryName());
      this.AdjTiles = new int[1]{ 10 };
      TileID.Sets.OpenDoorID[(int) ((ModBlockType) this).Type] = ModContent.TileType<SquidwardDoorOpen>();
    }

    public virtual bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

    public virtual void MouseOver(int i, int j)
    {
      Player localPlayer = Main.LocalPlayer;
      localPlayer.noThrow = 2;
      localPlayer.cursorItemIconEnabled = true;
      localPlayer.cursorItemIconID = ModContent.ItemType<SquidwardDoor>();
    }
  }
}
