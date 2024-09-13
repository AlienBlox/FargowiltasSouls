// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.UI.Elements.MinionsToggle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls.Content.UI.Elements
{
  public class MinionsToggle : UIElement
  {
    public const int CheckboxTextSpace = 4;

    public static DynamicSpriteFont Font => UIToggle.Font;

    public MinionsToggle()
    {
      ((StyleDimension) ref this.Width).Set(19f, 0.0f);
      ((StyleDimension) ref this.Height).Set(21f, 0.0f);
    }

    protected virtual void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      CalculatedStyle dimensions = this.GetDimensions();
      Vector2 vector2_1 = ((CalculatedStyle) ref dimensions).Position();
      FargoSoulsPlayer fargoSoulsPlayer = Main.LocalPlayer.FargoSouls();
      if (this.IsMouseHovering && Main.mouseLeft && Main.mouseLeftRelease)
        fargoSoulsPlayer.Toggler_MinionsDisabled = !fargoSoulsPlayer.Toggler_MinionsDisabled;
      spriteBatch.Draw(FargoUIManager.CheckBox.Value, vector2_1, Color.White);
      if (fargoSoulsPlayer.Toggler_MinionsDisabled)
        spriteBatch.Draw(FargoUIManager.CheckMark.Value, vector2_1, Color.White);
      string textValue = Language.GetTextValue("Mods.FargowiltasSouls.Toggler.DisableAllMinionEffects");
      Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(vector2_1, new Vector2(this.Width.Pixels * Main.UIScale, 0.0f)), new Vector2(4f, 0.0f)), new Vector2(0.0f, MinionsToggle.Font.MeasureString(textValue).Y * 0.175f));
      Color white = Color.White;
      Utils.DrawBorderString(spriteBatch, textValue, vector2_2, white, 1f, 0.0f, 0.0f, -1);
    }
  }
}
