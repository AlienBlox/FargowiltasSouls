// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Tiles.FMMBanner
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Placables;
using FargowiltasSouls.Content.NPCs.Critters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace FargowiltasSouls.Content.Tiles
{
  public class FMMBanner : ModTile
  {
    public virtual void SetStaticDefaults()
    {
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      Main.tileNoAttach[(int) ((ModBlockType) this).Type] = true;
      Main.tileLavaDeath[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
      TileObjectData.newTile.Height = 3;
      TileObjectData.newTile.CoordinateHeights = new int[3]
      {
        16,
        16,
        16
      };
      TileObjectData.newTile.StyleHorizontal = true;
      TileObjectData.newTile.StyleWrapLimit = 111;
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      this.AddMapEntry(new Color(13, 88, 130), ((ModBlockType) this).CreateMapEntryName());
    }

    public virtual bool CanDrop(int i, int j) => false;

    public virtual void KillMultiTile(int i, int j, int frameX, int frameY)
    {
      int num1 = frameX / 18;
      int num2 = ModContent.ItemType<TophatSquirrelBanner>();
      Item.NewItem((IEntitySource) new EntitySource_TileBreak(i, j, (string) null), i * 16, j * 16, 16, 48, num2, 1, false, 0, false, false);
    }

    public virtual void NearbyEffects(int i, int j, bool closer)
    {
      if (!closer)
        return;
      Player localPlayer = Main.LocalPlayer;
      Tile tile = ((Tilemap) ref Main.tile)[i, j];
      int num = (int) ((Tile) ref tile).TileFrameX / 18;
      int index = ModContent.NPCType<TophatSquirrelCritter>();
      Main.SceneMetrics.NPCBannerBuff[index] = true;
      Main.SceneMetrics.hasBanner = true;
    }

    public virtual void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
    {
      if (i % 2 != 1)
        return;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref spriteEffects = 1;
    }
  }
}
