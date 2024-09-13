// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Tiles.Trophies.BaseTrophy
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace FargowiltasSouls.Content.Tiles.Trophies
{
  public abstract class BaseTrophy : ModTile
  {
    protected abstract int ItemType { get; }

    public virtual void SetStaticDefaults()
    {
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      Main.tileLavaDeath[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.FramesOnKillWall[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      this.AddMapEntry(new Color(120, 85, 60), Language.GetText("MapObject.Trophy"));
      ((ModBlockType) this).DustType = 7;
    }

    public virtual bool CanDrop(int i, int j) => false;

    public virtual void KillMultiTile(int i, int j, int frameX, int frameY)
    {
      Item.NewItem((IEntitySource) new EntitySource_TileBreak(i, j, (string) null), i * 16, j * 16, 32, 32, this.ItemType, 1, false, 0, false, false);
    }
  }
}
