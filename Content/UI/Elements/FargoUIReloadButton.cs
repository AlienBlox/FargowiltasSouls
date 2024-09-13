// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.UI.Elements.FargoUIReloadButton
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls.Content.UI.Elements
{
  public class FargoUIReloadButton : UIElement
  {
    public Texture2D Texture;
    public Func<string> Text;

    public FargoUIReloadButton(Texture2D tex, Func<string> text)
    {
      this.Texture = tex;
      this.Text = text;
      ((StyleDimension) ref this.Width).Set(20f, 0.0f);
      ((StyleDimension) ref this.Height).Set(20f, 0.0f);
    }

    protected virtual void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      CalculatedStyle dimensions = this.GetDimensions();
      bool flag = false;
      if (this.IsMouseHovering)
      {
        Vector2 vector2 = Vector2.op_Addition(((CalculatedStyle) ref dimensions).Position(), new Vector2(0.0f, dimensions.Height + 8f));
        Utils.DrawBorderString(spriteBatch, this.Text(), vector2, Color.White, 1f, 0.0f, 0.0f, -1);
        flag = true;
      }
      Texture2D texture2D = FargoUIManager.PresetButtonOutline.Value;
      Vector2 vector2_1 = ((CalculatedStyle) ref dimensions).Position();
      spriteBatch.Draw(texture2D, vector2_1, new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 1f, (SpriteEffects) 0, 0.0f);
      Vector2 vector2_2 = Vector2.op_Addition(vector2_1, new Vector2(2f));
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, 0, 20, 20);
      if (flag)
        rectangle.X += 20;
      spriteBatch.Draw(this.Texture, vector2_2, new Rectangle?(rectangle), Color.White, 0.0f, Vector2.Zero, 1f, (SpriteEffects) 0, 0.0f);
    }
  }
}
