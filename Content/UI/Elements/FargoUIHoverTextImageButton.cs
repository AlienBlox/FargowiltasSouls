// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.UI.Elements.FargoUIHoverTextImageButton
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls.Content.UI.Elements
{
  public class FargoUIHoverTextImageButton : UIImageButton
  {
    public string Text;

    public FargoUIHoverTextImageButton(Asset<Texture2D> texture, string text)
      : base(texture)
    {
      this.Text = text;
    }

    protected virtual void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      if (!((UIElement) this).IsMouseHovering)
        return;
      Main.LocalPlayer.mouseInterface = true;
      Main.hoverItemName = this.Text;
    }
  }
}
