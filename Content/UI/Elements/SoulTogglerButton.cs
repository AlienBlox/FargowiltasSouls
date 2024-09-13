// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.UI.Elements.SoulTogglerButton
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls.Content.UI.Elements
{
  public class SoulTogglerButton : UIState
  {
    public UIImage Icon;
    public FargoUIHoverTextImageButton IconHighlight;
    public UIImage IconFlash;
    public UIOncomingMutant OncomingMutant;

    public virtual void OnActivate()
    {
      this.IconFlash = new UIImage(FargoUIManager.SoulTogglerButton_MouseOverTexture);
      ((StyleDimension) ref ((UIElement) this.IconFlash).Left).Set(570f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.IconFlash).Top).Set(275f, 0.0f);
      ((UIElement) this).Append((UIElement) this.IconFlash);
      this.Icon = new UIImage(FargoUIManager.SoulTogglerButtonTexture);
      ((StyleDimension) ref ((UIElement) this.Icon).Left).Set(570f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.Icon).Top).Set(275f, 0.0f);
      ((UIElement) this).Append((UIElement) this.Icon);
      this.IconHighlight = new FargoUIHoverTextImageButton(FargoUIManager.SoulTogglerButton_MouseOverTexture, Language.GetTextValue("Mods.FargowiltasSouls.UI.SoulTogglerButton"));
      ((StyleDimension) ref ((UIElement) this.IconHighlight).Left).Set(0.0f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.IconHighlight).Top).Set(0.0f, 0.0f);
      this.IconHighlight.SetVisibility(1f, 0.0f);
      // ISSUE: method pointer
      ((UIElement) this.IconHighlight).OnMouseOver += new UIElement.MouseEvent((object) this, __methodptr(IconHighlight_MouseOver));
      // ISSUE: method pointer
      ((UIElement) this.IconHighlight).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(IconHighlight_OnClick));
      ((UIElement) this.Icon).Append((UIElement) this.IconHighlight);
      this.OncomingMutant = new UIOncomingMutant(FargoUIManager.OncomingMutantTexture.Value, FargoUIManager.OncomingMutantAuraTexture.Value, Language.GetTextValue("Mods.FargowiltasSouls.UI.EternityEnabled"), Language.GetTextValue("Mods.FargowiltasSouls.UI.MasochistEnabled"));
      ((StyleDimension) ref ((UIElement) this.OncomingMutant).Left).Set(610f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.OncomingMutant).Top).Set(250f, 0.0f);
      ((UIElement) this).Append((UIElement) this.OncomingMutant);
      ((UIElement) this).OnActivate();
    }

    private void IconHighlight_MouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      FargoUIManager.SoulToggler.NeedsToggleListBuilding = true;
    }

    private void IconHighlight_OnClick(UIMouseEvent evt, UIElement listeningElement)
    {
      if (!Main.playerInventory)
        return;
      FargoUIManager.ToggleSoulToggler();
      Main.LocalPlayer.FargoSouls().HasClickedWrench = true;
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      if (!Main.playerInventory)
        return;
      ((UIElement) this.Icon).Draw(spriteBatch);
      ((UIElement) this.IconHighlight).Draw(spriteBatch);
      ((UIElement) this.OncomingMutant).Draw(spriteBatch);
      if (Main.LocalPlayer.FargoSouls().HasClickedWrench || (double) Main.GlobalTimeWrappedHourly % 1.0 >= 0.5)
        return;
      ((UIElement) this.IconFlash).Draw(spriteBatch);
    }
  }
}
