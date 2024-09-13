// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Tiles.MusicBoxes.StoriaMusicBoxSheet
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Placables.MusicBoxes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace FargowiltasSouls.Content.Tiles.MusicBoxes
{
  public class StoriaMusicBoxSheet : ModTile
  {
    public virtual void SetStaticDefaults()
    {
      Main.tileLighted[(int) ((ModBlockType) this).Type] = true;
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      Main.tileObsidianKill[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
      TileObjectData.newTile.Origin = new Point16(0, 1);
      TileObjectData.newTile.LavaDeath = false;
      TileObjectData.newTile.DrawYOffset = 2;
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      this.AddMapEntry(new Color(200, 200, 200), ((ModBlockType) this).CreateMapEntryName());
    }

    public virtual void MouseOver(int i, int j)
    {
      Player localPlayer = Main.LocalPlayer;
      localPlayer.noThrow = 2;
      localPlayer.cursorItemIconEnabled = true;
      localPlayer.cursorItemIconID = ModContent.ItemType<StoriaMusicBox>();
    }

    public virtual void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
      r = 1f;
      g = 1f;
      b = 1f;
    }
  }
}
