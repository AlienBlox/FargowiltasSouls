// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.UI.Elements.FargoUIHeader
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls.Content.UI.Elements
{
  public class FargoUIHeader : UIElement
  {
    public const int TextureTextPadding = 8;
    public const int TextureBarPadding = 4;
    public const int ItemTextureDimensions = 32;
    private readonly string Text;
    private readonly string Mod;
    private readonly int Item;

    public FargoUIHeader(string text, string mod, int item, (int width, int height) dimensions)
    {
      this.Text = text;
      this.Mod = mod;
      this.Item = item;
      ((StyleDimension) ref this.Width).Set((float) dimensions.width, 0.0f);
      ((StyleDimension) ref this.Height).Set((float) dimensions.height, 0.0f);
    }

    protected virtual void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      CalculatedStyle dimensions = this.GetDimensions();
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector(dimensions.X, dimensions.Y);
      spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int) vector2.X + 2, (int) dimensions.Y + 22 - 1, (int) dimensions.Width - 2, 1), Color.Black);
      spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int) vector2.X + 2, (int) dimensions.Y + 22 - 1 + 1, (int) dimensions.Width - 2, 1), Color.LightGray);
      spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int) vector2.X + 2, (int) dimensions.Y + 22 - 1 + 2, (int) dimensions.Width - 2, 1), Color.Gray);
      spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int) vector2.X + 2, (int) dimensions.Y + 22 - 1 + 3, (int) dimensions.Width - 2, 1), Color.Black);
      spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int) vector2.X + 1, (int) dimensions.Y + 22 - 1 + 1, 1, 2), Color.Black);
      spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int) vector2.X + (int) dimensions.Width, (int) dimensions.Y + 22 - 1 + 1, 1, 2), Color.Black);
      Utils.DrawBorderString(spriteBatch, this.Text, vector2, Color.White, 1f, 0.0f, 0.0f, -1);
    }
  }
}
