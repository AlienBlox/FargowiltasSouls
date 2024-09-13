// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Placables.WiresPaintingSheet
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace FargowiltasSouls.Content.Items.Placables
{
  public class WiresPaintingSheet : ModTile
  {
    public virtual void SetStaticDefaults()
    {
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      Main.tileLavaDeath[(int) ((ModBlockType) this).Type] = true;
      Main.tileSpelunker[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
      TileObjectData.newTile.StyleHorizontal = true;
      TileObjectData.newTile.StyleWrapLimit = 36;
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      ((ModBlockType) this).DustType = 7;
    }
  }
}
