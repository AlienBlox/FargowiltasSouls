// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Tiles.LifeRevitalizerPlaced
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Placables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace FargowiltasSouls.Content.Tiles
{
  public class LifeRevitalizerPlaced : ModTile
  {
    public virtual void SetStaticDefaults()
    {
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      Main.tileLighted[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.IsValidSpawnPoint[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.DisableSmartCursor[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.InteractibleByNPCs[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
      TileObjectData.newTile.LavaDeath = false;
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      this.AddMapEntry(Color.Pink, ((ModBlockType) this).CreateMapEntryName());
      this.AnimationFrameHeight = 54;
    }

    public virtual bool RightClick(int i, int j)
    {
      Player localPlayer = Main.LocalPlayer;
      Tile tileSafely = Framing.GetTileSafely(i, j);
      i -= (int) ((Tile) ref tileSafely).TileFrameX / 18;
      j -= (int) ((Tile) ref tileSafely).TileFrameY / 18;
      ++i;
      j += 3;
      localPlayer.FindSpawn();
      if (localPlayer.SpawnX == i && localPlayer.SpawnY == j)
      {
        localPlayer.RemoveSpawn();
        Main.NewText(Language.GetTextValue("Game.SpawnPointRemoved"), byte.MaxValue, (byte) 240, (byte) 20);
      }
      else if (WorldGen.InWorld(i, j, 0))
      {
        localPlayer.ChangeSpawn(i, j);
        Main.NewText(Language.GetTextValue("Game.SpawnPointSet"), byte.MaxValue, (byte) 240, (byte) 20);
      }
      return true;
    }

    public virtual void MouseOver(int i, int j)
    {
      Player localPlayer = Main.LocalPlayer;
      localPlayer.noThrow = 2;
      localPlayer.cursorItemIconEnabled = true;
      localPlayer.cursorItemIconID = ModContent.ItemType<LifeRevitalizer>();
    }

    public virtual void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
      r = 1f;
      g = 0.0784313753f;
      b = 0.5764706f;
    }

    public virtual void AnimateTile(ref int frame, ref int frameCounter)
    {
      if (++frameCounter < 6)
        return;
      frameCounter = 0;
      frame = ++frame % 3;
    }

    public virtual bool PreDraw(int i, int j, SpriteBatch spriteBatch) => true;
  }
}
