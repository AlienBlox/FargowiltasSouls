// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.UI.Elements.FargoUIDragablePanel
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls.Content.UI.Elements
{
  public class FargoUIDragablePanel : UIPanel
  {
    private Vector2 offset;
    public bool dragging;
    public UIElement[] ExtraChildren;

    public FargoUIDragablePanel()
    {
    }

    public FargoUIDragablePanel(params UIElement[] countMeAsChildren)
    {
      this.ExtraChildren = countMeAsChildren;
    }

    private void DragStart(Vector2 pos)
    {
      this.offset = new Vector2(pos.X - ((UIElement) this).Left.Pixels, pos.Y - ((UIElement) this).Top.Pixels);
      this.dragging = true;
    }

    private void DragEnd(Vector2 pos)
    {
      Vector2 vector2 = pos;
      this.dragging = false;
      ((StyleDimension) ref ((UIElement) this).Left).Set(vector2.X - this.offset.X, 0.0f);
      ((StyleDimension) ref ((UIElement) this).Top).Set(vector2.Y - this.offset.Y, 0.0f);
      ((UIElement) this).Recalculate();
    }

    public virtual void Update(GameTime gameTime)
    {
      ((UIElement) this).Update(gameTime);
      if (((UIElement) this).ContainsPoint(Main.MouseScreen))
        Main.LocalPlayer.mouseInterface = true;
      if (!this.dragging && ((UIElement) this).ContainsPoint(Main.MouseScreen) && Main.mouseLeft && ((MouseState) ref PlayerInput.MouseInfoOld).LeftButton == null)
      {
        bool flag = true;
        if (this.ExtraChildren != null)
        {
          foreach (UIElement uiElement in ((UIElement) this).Elements.Concat<UIElement>((IEnumerable<UIElement>) this.ExtraChildren))
          {
            if (uiElement.ContainsPoint(Main.MouseScreen) && !(uiElement is UIPanel))
            {
              flag = false;
              break;
            }
          }
        }
        if (flag)
          this.DragStart(Main.MouseScreen);
      }
      else if (this.dragging && !Main.mouseLeft)
        this.DragEnd(Main.MouseScreen);
      if (this.dragging)
      {
        ((StyleDimension) ref ((UIElement) this).Left).Set((float) Main.mouseX - this.offset.X, 0.0f);
        ((StyleDimension) ref ((UIElement) this).Top).Set((float) Main.mouseY - this.offset.Y, 0.0f);
        ((UIElement) this).Recalculate();
      }
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
  }
}
