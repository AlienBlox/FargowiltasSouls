// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Tiles.Relics.BaseRelic
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace FargowiltasSouls.Content.Tiles.Relics
{
  public abstract class BaseRelic : ModTile
  {
    public const int FrameWidth = 54;
    public const int FrameHeight = 72;
    public const int HorizontalFrames = 1;
    public const int VerticalFrames = 1;
    private const string path = "FargowiltasSouls/Content/Tiles/Relics/";
    public Asset<Texture2D> RelicTexture;

    protected abstract int ItemType { get; }

    protected string RelicTextureName
    {
      get => "FargowiltasSouls/Content/Tiles/Relics/" + ((ModType) this).Name;
    }

    public virtual string Texture => "FargowiltasSouls/Content/Tiles/Relics/RelicPedestal";

    public virtual void Load()
    {
      if (Main.dedServ)
        return;
      this.RelicTexture = ModContent.Request<Texture2D>(this.RelicTextureName, (AssetRequestMode) 2);
    }

    public virtual void Unload() => this.RelicTexture = (Asset<Texture2D>) null;

    public virtual void SetStaticDefaults()
    {
      Main.tileShine[(int) ((ModBlockType) this).Type] = 400;
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      TileID.Sets.InteractibleByNPCs[(int) ((ModBlockType) this).Type] = true;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
      TileObjectData.newTile.LavaDeath = false;
      TileObjectData.newTile.DrawYOffset = 2;
      TileObjectData.newTile.Direction = (TileObjectDirection) 1;
      TileObjectData.newTile.StyleHorizontal = false;
      TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
      TileObjectData.newAlternate.Direction = (TileObjectDirection) 2;
      TileObjectData.addAlternate(1);
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      this.AddMapEntry(new Color(233, 207, 94), Language.GetText("MapObject.Relic"));
    }

    public virtual bool CanDrop(int i, int j) => false;

    public virtual void KillMultiTile(int i, int j, int frameX, int frameY)
    {
      int num1 = frameX / 54;
      int num2 = 0;
      if (num1 == 0)
        num2 = this.ItemType;
      if (num2 <= 0)
        return;
      Item.NewItem((IEntitySource) new EntitySource_TileBreak(i, j, (string) null), i * 16, j * 16, 32, 32, num2, 1, false, 0, false, false);
    }

    public virtual bool CreateDust(int i, int j, ref int type) => false;

    public virtual void SetDrawPositions(
      int i,
      int j,
      ref int width,
      ref int offsetY,
      ref int height,
      ref short tileFrameX,
      ref short tileFrameY)
    {
      tileFrameX %= (short) 54;
      tileFrameY %= (short) 144;
    }

    public virtual void DrawEffects(
      int i,
      int j,
      SpriteBatch spriteBatch,
      ref TileDrawInfo drawData)
    {
      if ((int) drawData.tileFrameX % 54 != 0 || (int) drawData.tileFrameY % 72 != 0)
        return;
      Main.instance.TilesRenderer.AddSpecialLegacyPoint(i, j);
    }

    public virtual void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
    {
      Vector2 zero;
      // ISSUE: explicit constructor call
      ((Vector2) ref zero).\u002Ector((float) Main.offScreenRange);
      if (Main.drawToScreen)
        zero = Vector2.Zero;
      Point point;
      // ISSUE: explicit constructor call
      ((Point) ref point).\u002Ector(i, j);
      Tile tile = ((Tilemap) ref Main.tile)[point.X, point.Y];
      if (Tile.op_Equality(tile, (ArgumentException) null) || !((Tile) ref tile).HasTile)
        return;
      Texture2D texture2D = this.RelicTexture.Value;
      int num1 = (int) ((Tile) ref tile).TileFrameX / 54;
      Rectangle rectangle = Utils.Frame(texture2D, 1, 1, 0, num1, 0, 0);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Vector2 worldCoordinates = Utils.ToWorldCoordinates(point, 24f, 64f);
      Color color1 = Lighting.GetColor(point.X, point.Y);
      SpriteEffects spriteEffects = (uint) ((Tile) ref tile).TileFrameY / 72U > 0U ? (SpriteEffects) 1 : (SpriteEffects) 0;
      float num2 = (float) Math.Sin((double) Main.GlobalTimeWrappedHourly * 6.2831854820251465 / 5.0);
      Vector2 vector2_2 = zero;
      Vector2 vector2_3 = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(worldCoordinates, vector2_2), Main.screenPosition), new Vector2(0.0f, -40f)), new Vector2(0.0f, num2 * 4f));
      spriteBatch.Draw(texture2D, vector2_3, new Rectangle?(rectangle), color1, 0.0f, vector2_1, 1f, spriteEffects, 0.0f);
      float num3 = (float) (Math.Sin((double) Main.GlobalTimeWrappedHourly * 6.2831854820251465 / 2.0) * 0.30000001192092896 + 0.699999988079071);
      Color color2 = color1;
      ((Color) ref color2).A = (byte) 0;
      Color color3 = Color.op_Multiply(Color.op_Multiply(color2, 0.1f), num3);
      for (float num4 = 0.0f; (double) num4 < 1.0; num4 += 0.166666672f)
        spriteBatch.Draw(texture2D, Vector2.op_Addition(vector2_3, Vector2.op_Multiply(Utils.ToRotationVector2(6.28318548f * num4), (float) (6.0 + (double) num2 * 2.0))), new Rectangle?(rectangle), color3, 0.0f, vector2_1, 1f, spriteEffects, 0.0f);
    }
  }
}
