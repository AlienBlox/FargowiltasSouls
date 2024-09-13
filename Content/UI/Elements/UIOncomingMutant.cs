// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.UI.Elements.UIOncomingMutant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls.Content.UI.Elements
{
  public class UIOncomingMutant : UIPanel
  {
    private Vector2 offset;
    public bool dragging;
    public Texture2D Texture;
    public Texture2D AuraTexture;
    public string TextEMode;
    public string TextMaso;

    public UIOncomingMutant(Texture2D tex, Texture2D auraTex, string textEMode, string textMaso)
    {
      this.Texture = tex;
      this.AuraTexture = auraTex;
      this.TextEMode = textEMode;
      this.TextMaso = textMaso;
      ((StyleDimension) ref ((UIElement) this).Width).Set(24f, 0.0f);
      ((StyleDimension) ref ((UIElement) this).Height).Set(26f, 0.0f);
    }

    private void DragStart(Vector2 pos)
    {
      this.offset = new Vector2(pos.X - ((UIElement) this).Left.Pixels, pos.Y - ((UIElement) this).Top.Pixels);
      this.dragging = true;
    }

    private void DragEnd(Vector2 pos)
    {
      Vector2 vector2 = Vector2.op_Subtraction(pos, this.offset);
      this.dragging = false;
      ((StyleDimension) ref ((UIElement) this).Left).Set(vector2.X, 0.0f);
      ((StyleDimension) ref ((UIElement) this).Top).Set(vector2.Y, 0.0f);
      ((UIElement) this).Recalculate();
      this.StayInBounds();
      SoulConfig.Instance.OncomingMutantX = vector2.X;
      SoulConfig.Instance.OncomingMutantY = vector2.Y;
      SoulConfig.Instance.OnChanged();
    }

    public virtual void Update(GameTime gameTime)
    {
      ((UIElement) this).Update(gameTime);
      bool flag = WorldSavingSystem.EternityMode && Main.playerInventory;
      if (((UIElement) this).ContainsPoint(Main.MouseScreen) & flag)
        Main.LocalPlayer.mouseInterface = true;
      if (!this.dragging & flag && ((UIElement) this).ContainsPoint(Main.MouseScreen) && Main.mouseLeft && ((MouseState) ref PlayerInput.MouseInfoOld).LeftButton == null)
        this.DragStart(Main.MouseScreen);
      else if (this.dragging && (!Main.mouseLeft || !flag))
        this.DragEnd(Main.MouseScreen);
      if (this.dragging)
      {
        ((StyleDimension) ref ((UIElement) this).Left).Set((float) Main.mouseX - this.offset.X, 0.0f);
        ((StyleDimension) ref ((UIElement) this).Top).Set((float) Main.mouseY - this.offset.Y, 0.0f);
        ((UIElement) this).Recalculate();
      }
      else
      {
        ((StyleDimension) ref ((UIElement) this).Left).Set(SoulConfig.Instance.OncomingMutantX, 0.0f);
        ((StyleDimension) ref ((UIElement) this).Top).Set(SoulConfig.Instance.OncomingMutantY, 0.0f);
        ((UIElement) this).Recalculate();
      }
      this.StayInBounds();
    }

    private void StayInBounds()
    {
      CalculatedStyle dimensions1 = ((UIElement) this).Parent.GetDimensions();
      Rectangle rectangle1 = ((CalculatedStyle) ref dimensions1).ToRectangle();
      CalculatedStyle dimensions2 = ((UIElement) this).GetDimensions();
      Rectangle rectangle2 = ((CalculatedStyle) ref dimensions2).ToRectangle();
      if (((Rectangle) ref rectangle2).Intersects(rectangle1))
        return;
      ((UIElement) this).Left.Pixels = Utils.Clamp<float>(((UIElement) this).Left.Pixels, 0.0f, (float) ((Rectangle) ref rectangle1).Right - ((UIElement) this).Width.Pixels);
      ((UIElement) this).Top.Pixels = Utils.Clamp<float>(((UIElement) this).Top.Pixels, 0.0f, (float) ((Rectangle) ref rectangle1).Bottom - ((UIElement) this).Height.Pixels);
      ((UIElement) this).Recalculate();
    }

    protected virtual void DrawSelf(SpriteBatch spriteBatch)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      CalculatedStyle dimensions = ((UIElement) this).GetDimensions();
      if (((UIElement) this).IsMouseHovering && !this.dragging)
      {
        Vector2 vector2 = Vector2.op_Addition(Main.MouseScreen, new Vector2(21f, 21f));
        Utils.DrawBorderString(spriteBatch, WorldSavingSystem.MasochistModeReal ? this.TextMaso : this.TextEMode, vector2, WorldSavingSystem.MasochistModeReal ? new Color(51, (int) byte.MaxValue, 191) : Color.White, 1f, 0.0f, 0.0f, -1);
      }
      Vector2 vector2_1 = ((CalculatedStyle) ref dimensions).Position();
      spriteBatch.Draw(this.Texture, Vector2.op_Addition(vector2_1, new Vector2(2f)), new Rectangle?(this.Texture.Bounds), Color.White, 0.0f, Vector2.Zero, 1f, (SpriteEffects) 0, 0.0f);
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      spriteBatch.Draw(this.AuraTexture, vector2_1, new Rectangle?(this.AuraTexture.Bounds), Color.White, 0.0f, Vector2.Zero, 1f, (SpriteEffects) 0, 0.0f);
    }
  }
}
