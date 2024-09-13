// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Sam.SquidwardDoorOpen
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
  public class SquidwardDoorOpen : ModTile
  {
    public virtual void SetStaticDefaults()
    {
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      Main.tileSolid[(int) ((ModBlockType) this).Type] = false;
      Main.tileLavaDeath[(int) ((ModBlockType) this).Type] = true;
      Main.tileNoSunLight[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.Width = 2;
      TileObjectData.newTile.Height = 3;
      TileObjectData.newTile.Origin = new Point16(0, 0);
      TileObjectData.newTile.AnchorTop = new AnchorData((AnchorType) 1, 1, 0);
      TileObjectData.newTile.AnchorBottom = new AnchorData((AnchorType) 1, 1, 0);
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
      TileObjectData.newTile.StyleHorizontal = true;
      TileObjectData.newTile.StyleMultiplier = 2;
      TileObjectData.newTile.StyleWrapLimit = 2;
      TileObjectData.newTile.Direction = (TileObjectDirection) 2;
      TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
      TileObjectData.newAlternate.Origin = new Point16(0, 1);
      TileObjectData.addAlternate(0);
      TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
      TileObjectData.newAlternate.Origin = new Point16(0, 2);
      TileObjectData.addAlternate(0);
      TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
      TileObjectData.newAlternate.Origin = new Point16(1, 0);
      TileObjectData.newAlternate.AnchorTop = new AnchorData((AnchorType) 1, 1, 1);
      TileObjectData.newAlternate.AnchorBottom = new AnchorData((AnchorType) 1, 1, 1);
      TileObjectData.newAlternate.Direction = (TileObjectDirection) 1;
      TileObjectData.addAlternate(1);
      TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
      TileObjectData.newAlternate.Origin = new Point16(1, 1);
      TileObjectData.newAlternate.AnchorTop = new AnchorData((AnchorType) 1, 1, 1);
      TileObjectData.newAlternate.AnchorBottom = new AnchorData((AnchorType) 1, 1, 1);
      TileObjectData.newAlternate.Direction = (TileObjectDirection) 1;
      TileObjectData.addAlternate(1);
      TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
      TileObjectData.newAlternate.Origin = new Point16(1, 2);
      TileObjectData.newAlternate.AnchorTop = new AnchorData((AnchorType) 1, 1, 1);
      TileObjectData.newAlternate.AnchorBottom = new AnchorData((AnchorType) 1, 1, 1);
      TileObjectData.newAlternate.Direction = (TileObjectDirection) 1;
      TileObjectData.addAlternate(1);
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      this.AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
      TileID.Sets.HousingWalls[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.HasOutlines[(int) ((ModBlockType) this).Type] = true;
      this.AddMapEntry(new Color(200, 200, 200), ((ModBlockType) this).CreateMapEntryName());
      this.AdjTiles = new int[1]{ 11 };
      TileID.Sets.CloseDoorID[(int) ((ModBlockType) this).Type] = ModContent.TileType<SquidwardDoorClosed>();
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
