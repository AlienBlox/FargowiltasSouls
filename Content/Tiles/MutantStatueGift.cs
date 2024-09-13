// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Tiles.MutantStatueGift
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace FargowiltasSouls.Content.Tiles
{
  public class MutantStatueGift : ModTile
  {
    public virtual void SetStaticDefaults()
    {
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      Main.tileObsidianKill[(int) ((ModBlockType) this).Type] = true;
      Main.tileNoAttach[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.PreventsTileRemovalIfOnTopOfIt[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
      TileObjectData.newTile.LavaDeath = false;
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      this.AddMapEntry(new Color(144, 144, 144), ((ModBlockType) this).CreateMapEntryName());
    }

    public virtual void MouseOver(int i, int j)
    {
      Player localPlayer = Main.LocalPlayer;
      localPlayer.noThrow = 2;
      localPlayer.cursorItemIconEnabled = true;
      localPlayer.cursorItemIconID = ModContent.ItemType<Masochist>();
    }

    public virtual bool RightClick(int i, int j)
    {
      Tile tileSafely = Framing.GetTileSafely(i, j);
      i -= (int) ((Tile) ref tileSafely).TileFrameX / 18;
      j -= (int) ((Tile) ref tileSafely).TileFrameY / 18;
      ++i;
      j += 2;
      WorldGen.KillTile(i, j, false, false, true);
      return true;
    }

    public virtual void KillMultiTile(int i, int j, int frameX, int frameY)
    {
      Tile tileSafely = Framing.GetTileSafely(i, j);
      i -= (int) ((Tile) ref tileSafely).TileFrameX / 18;
      j -= (int) ((Tile) ref tileSafely).TileFrameY / 18;
      ++i;
      j += 2;
      Item.NewItem((IEntitySource) new EntitySource_TileBreak(i, j - 1, (string) null), i * 16, j * 16, 48, 48, ModContent.ItemType<Masochist>(), 1, false, 0, false, false);
      WorldGen.PlaceTile(i, j, ModContent.TileType<MutantStatue>(), false, false, -1, 0);
    }

    public virtual void NumDust(int i, int j, bool fail, ref int num) => num = 0;

    public virtual bool CanDrop(int i, int j) => false;
  }
}
