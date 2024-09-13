// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.UI.SoulToggler
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.UI.Elements;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls.Content.UI
{
  public class SoulToggler : UIState
  {
    public static readonly Regex RemoveItemTags = new Regex("\\[[^\\[\\]]*\\]");
    public bool NeedsToggleListBuilding;
    public string DisplayMod;
    public string SortCategory;
    public const int BackWidth = 400;
    public const int BackHeight = 658;
    public FargoUIDragablePanel BackPanel;
    public UIPanel InnerPanel;
    public UIPanel PresetPanel;
    public UIScrollbar Scrollbar;
    public UIToggleList ToggleList;
    public FargoUISearchBar SearchBar;
    public FargoUIPresetButton OffButton;
    public FargoUIPresetButton OnButton;
    public FargoUIPresetButton MinimalButton;
    public FargoUIPresetButton SomeEffectsButton;
    public FargoUIPresetButton[] CustomButton = new FargoUIPresetButton[3];
    public FargoUIDisplayAllButton DisplayAllButton;

    public virtual void OnInitialize()
    {
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector((float) ((double) Main.screenWidth / 2.0 - 200.0), (float) ((double) Main.screenHeight / 2.0 - 329.0));
      this.NeedsToggleListBuilding = true;
      this.DisplayMod = "";
      this.SortCategory = "";
      this.Scrollbar = new UIScrollbar();
      this.Scrollbar.SetView(200f, 1000f);
      ((StyleDimension) ref ((UIElement) this.Scrollbar).Width).Set(20f, 0.0f);
      ((UIElement) this.Scrollbar).OverflowHidden = true;
      // ISSUE: method pointer
      ((UIElement) this.Scrollbar).OnScrollWheel += new UIElement.ScrollWheelEvent((object) this, __methodptr(HotbarScrollFix));
      this.ToggleList = new UIToggleList();
      this.ToggleList.SetScrollbar(this.Scrollbar);
      // ISSUE: method pointer
      ((UIElement) this.ToggleList).OnScrollWheel += new UIElement.ScrollWheelEvent((object) this, __methodptr(HotbarScrollFix));
      this.BackPanel = new FargoUIDragablePanel(new UIElement[2]
      {
        (UIElement) this.Scrollbar,
        (UIElement) this.ToggleList
      });
      ((StyleDimension) ref ((UIElement) this.BackPanel).Left).Set(vector2.X, 0.0f);
      ((StyleDimension) ref ((UIElement) this.BackPanel).Top).Set(vector2.Y, 0.0f);
      ((StyleDimension) ref ((UIElement) this.BackPanel).Width).Set(400f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.BackPanel).Height).Set(658f, 0.0f);
      ((UIElement) this.BackPanel).PaddingLeft = ((UIElement) this.BackPanel).PaddingRight = ((UIElement) this.BackPanel).PaddingTop = ((UIElement) this.BackPanel).PaddingBottom = 0.0f;
      this.BackPanel.BackgroundColor = Color.op_Multiply(new Color(29, 33, 70), 0.7f);
      this.InnerPanel = new UIPanel();
      ((StyleDimension) ref ((UIElement) this.InnerPanel).Width).Set(388f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.InnerPanel).Height).Set(588f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.InnerPanel).Left).Set(6f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.InnerPanel).Top).Set(32f, 0.0f);
      this.InnerPanel.BackgroundColor = Color.op_Multiply(new Color(73, 94, 171), 0.9f);
      this.SearchBar = new FargoUISearchBar(392, 26);
      ((StyleDimension) ref this.SearchBar.Left).Set(4f, 0.0f);
      ((StyleDimension) ref this.SearchBar.Top).Set(4f, 0.0f);
      this.SearchBar.OnTextChange += new FargoUISearchBar.TextChangeDelegate(this.SearchBar_OnTextChange);
      ((StyleDimension) ref ((UIElement) this.ToggleList).Width).Set(((UIElement) this.InnerPanel).Width.Pixels - ((UIElement) this.InnerPanel).PaddingLeft * 2f - ((UIElement) this.Scrollbar).Width.Pixels, 0.0f);
      ((StyleDimension) ref ((UIElement) this.ToggleList).Height).Set(((UIElement) this.InnerPanel).Height.Pixels - ((UIElement) this.InnerPanel).PaddingTop * 2f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.Scrollbar).Height).Set(((UIElement) this.InnerPanel).Height.Pixels - 16f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.Scrollbar).Left).Set((float) ((double) ((UIElement) this.InnerPanel).Width.Pixels - (double) ((UIElement) this.Scrollbar).Width.Pixels - 18.0), 0.0f);
      this.PresetPanel = new UIPanel();
      ((StyleDimension) ref ((UIElement) this.PresetPanel).Left).Set(5f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.PresetPanel).Top).Set((float) ((double) this.SearchBar.Height.Pixels + (double) ((UIElement) this.InnerPanel).Height.Pixels + 8.0), 0.0f);
      ((StyleDimension) ref ((UIElement) this.PresetPanel).Width).Set(390f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.PresetPanel).Height).Set(32f, 0.0f);
      ((UIElement) this.PresetPanel).PaddingTop = ((UIElement) this.PresetPanel).PaddingBottom = 0.0f;
      ((UIElement) this.PresetPanel).PaddingLeft = ((UIElement) this.PresetPanel).PaddingRight = 0.0f;
      this.PresetPanel.BackgroundColor = new Color(74, 95, 172);
      this.OffButton = new FargoUIPresetButton(FargoUIManager.PresetOffButton.Value, (Action<ToggleBackend>) (toggles => toggles.SetAll(false)), (Func<string>) (() => Language.GetTextValue("Mods.FargowiltasSouls.UI.TurnAllTogglesOff")));
      ((StyleDimension) ref this.OffButton.Top).Set(6f, 0.0f);
      ((StyleDimension) ref this.OffButton.Left).Set(8f, 0.0f);
      this.OnButton = new FargoUIPresetButton(FargoUIManager.PresetOnButton.Value, (Action<ToggleBackend>) (toggles => toggles.SetAll(true)), (Func<string>) (() => Language.GetTextValue("Mods.FargowiltasSouls.UI.TurnAllTogglesOn")));
      ((StyleDimension) ref this.OnButton.Top).Set(6f, 0.0f);
      ((StyleDimension) ref this.OnButton.Left).Set(30f, 0.0f);
      this.SomeEffectsButton = new FargoUIPresetButton(FargoUIManager.PresetMinimalButton.Value, (Action<ToggleBackend>) (toggles => toggles.SomeEffects()), (Func<string>) (() => Language.GetTextValue("Mods.FargowiltasSouls.UI.SomeEffectsPreset")));
      ((StyleDimension) ref this.SomeEffectsButton.Top).Set(6f, 0.0f);
      ((StyleDimension) ref this.SomeEffectsButton.Left).Set(52f, 0.0f);
      this.MinimalButton = new FargoUIPresetButton(FargoUIManager.PresetMinimalButton.Value, (Action<ToggleBackend>) (toggles => toggles.MinimalEffects()), (Func<string>) (() => Language.GetTextValue("Mods.FargowiltasSouls.UI.MinimalEffectsPreset")));
      ((StyleDimension) ref this.MinimalButton.Top).Set(6f, 0.0f);
      ((StyleDimension) ref this.MinimalButton.Left).Set(74f, 0.0f);
      ((UIElement) this).Append((UIElement) this.BackPanel);
      ((UIElement) this.BackPanel).Append((UIElement) this.InnerPanel);
      ((UIElement) this.BackPanel).Append((UIElement) this.SearchBar);
      ((UIElement) this.BackPanel).Append((UIElement) this.PresetPanel);
      ((UIElement) this.InnerPanel).Append((UIElement) this.Scrollbar);
      ((UIElement) this.InnerPanel).Append((UIElement) this.ToggleList);
      ((UIElement) this.PresetPanel).Append((UIElement) this.OffButton);
      ((UIElement) this.PresetPanel).Append((UIElement) this.OnButton);
      ((UIElement) this.PresetPanel).Append((UIElement) this.SomeEffectsButton);
      ((UIElement) this.PresetPanel).Append((UIElement) this.MinimalButton);
      for (int index = 0; index < 3; ++index)
      {
        int slot = index + 1;
        this.CustomButton[index] = new FargoUIPresetButton(FargoUIManager.PresetCustomButton.Value, (Action<ToggleBackend>) (toggles => toggles.LoadCustomPreset(slot)), (Action<ToggleBackend>) (toggles => toggles.SaveCustomPreset(slot)), (Func<string>) (() => Language.GetTextValue("Mods.FargowiltasSouls.UI.CustomPreset", (object) slot)));
        ((StyleDimension) ref this.CustomButton[index].Top).Set(6f, 0.0f);
        ((StyleDimension) ref this.CustomButton[index].Left).Set((float) (74 + 22 * slot), 0.0f);
        ((UIElement) this.PresetPanel).Append((UIElement) this.CustomButton[index]);
        if (slot == 3)
        {
          this.DisplayAllButton = new FargoUIDisplayAllButton(FargoUIManager.DisplayAllButtonTexture.Value, (Func<string>) (() => Language.GetTextValue("Mods.FargowiltasSouls.UI.DisplayAll")), (Func<string>) (() => Language.GetTextValue("Mods.FargowiltasSouls.UI.DisplayEquipped")));
          // ISSUE: method pointer
          this.DisplayAllButton.OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(DisplayAllButton_OnLeftClick));
          ((StyleDimension) ref this.DisplayAllButton.Top).Set(6f, 0.0f);
          ((StyleDimension) ref this.DisplayAllButton.Left).Set((float) (74 + 22 * (slot + 1)), 0.0f);
          ((UIElement) this.PresetPanel).Append((UIElement) this.DisplayAllButton);
        }
      }
      ((UIElement) this).OnInitialize();
    }

    private void DisplayAllButton_OnLeftClick(UIMouseEvent evt, UIElement listeningElement)
    {
      this.DisplayAllButton.DisplayAll = !this.DisplayAllButton.DisplayAll;
      this.NeedsToggleListBuilding = true;
    }

    private void SearchBar_OnTextChange(string oldText, string currentText)
    {
      this.NeedsToggleListBuilding = true;
    }

    private void HotbarScrollFix(UIScrollWheelEvent evt, UIElement listeningElement)
    {
      Main.LocalPlayer.ScrollHotbar(PlayerInput.ScrollWheelDelta / 120);
    }

    public virtual void Update(GameTime gameTime)
    {
      if (Main.LocalPlayer.mouseInterface && (Main.mouseLeft || Main.mouseRight))
        this.NeedsToggleListBuilding = true;
      ((UIElement) this).Update(gameTime);
      FargoSoulsPlayer fargoSoulsPlayer = Main.LocalPlayer.FargoSouls();
      if (!this.NeedsToggleListBuilding || fargoSoulsPlayer.ToggleRebuildCooldown > 0)
        return;
      this.BuildList();
      this.NeedsToggleListBuilding = false;
      fargoSoulsPlayer.ToggleRebuildCooldown = 30;
    }

    public void BuildList()
    {
      this.ToggleList.Clear();
      Player localPlayer = Main.LocalPlayer;
      ToggleBackend toggler = localPlayer.FargoSouls().Toggler;
      AccessoryEffectPlayer effectPlayer = localPlayer.AccessoryEffects();
      bool alwaysDisplay = this.DisplayAllButton.DisplayAll;
      IEnumerable<Header> loadedHeaders = (IEnumerable<Header>) ToggleLoader.LoadedHeaders;
      bool flag1 = false;
      bool flag2 = false;
      for (int index = 0; index < AccessoryEffectLoader.AccessoryEffects.Count; ++index)
      {
        if (effectPlayer.EquippedEffects[index] && AccessoryEffectLoader.AccessoryEffects[index].MinionEffect)
          flag1 = true;
        if (effectPlayer.EquippedEffects[index] && AccessoryEffectLoader.AccessoryEffects[index].ExtraAttackEffect)
          flag2 = true;
      }
      if (flag1)
        ((UIList) this.ToggleList).Add((UIElement) new MinionsToggle());
      if (flag2)
        ((UIList) this.ToggleList).Add((UIElement) new ExtraAttacksToggle());
      DisplayToggles((IEnumerable<Header>) loadedHeaders.OrderBy<Header, float>((Func<Header, float>) (h => h.Priority)));
      if (this.ToggleList.Count != 0)
        return;
      this.ToggleList.Clear();
      UIToggleList toggleList = this.ToggleList;
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
      interpolatedStringHandler.AppendLiteral("[i:");
      interpolatedStringHandler.AppendFormatted<int>(ModContent.ItemType<TogglerIconItem>());
      interpolatedStringHandler.AppendLiteral("] ");
      interpolatedStringHandler.AppendFormatted(Language.GetTextValue("Mods.FargowiltasSouls.UI.NoToggles"));
      FargoUIHeader fargoUiHeader = new FargoUIHeader(interpolatedStringHandler.ToStringAndClear(), FargowiltasSouls.FargowiltasSouls.Instance.Name, ModContent.ItemType<TogglerIconItem>(), (384, 20));
      ((UIList) toggleList).Add((UIElement) fargoUiHeader);

      bool SearchMatches(string[] words)
      {
        return ((IEnumerable<string>) words).Any<string>((Func<string, bool>) (s => s.StartsWith(this.SearchBar.Input, StringComparison.OrdinalIgnoreCase)));
      }

      void DisplayToggles(IEnumerable<Header> headers)
      {
        foreach (Header header1 in headers)
        {
          Header header = header1;
          string[] headerWords = header.GetRawToggleName().Split(' ', StringSplitOptions.None);
          IEnumerable<Toggle> source = toggler.Toggles.Values.Where<Toggle>((Func<Toggle, bool>) (toggle =>
          {
            string[] words = toggle.GetRawToggleName().Split(' ', StringSplitOptions.None);
            if (!(effectPlayer.Equipped(toggle.Effect) | alwaysDisplay) || toggle.Header != header || !string.IsNullOrEmpty(this.DisplayMod) && !(toggle.Mod == this.DisplayMod) || !string.IsNullOrEmpty(this.SortCategory) && !(toggle.Category == this.SortCategory))
              return false;
            return string.IsNullOrEmpty(this.SearchBar.Input) || SearchMatches(words) || SearchMatches(headerWords);
          }));
          if (source.Any<Toggle>())
          {
            if (this.ToggleList.Count > 0)
              ((UIList) this.ToggleList).Add((UIElement) new UIText("", 0.2f, false));
            string headerDescription = header.HeaderDescription;
            int num = header.Item;
            ((UIList) this.ToggleList).Add((UIElement) new FargoUIHeader(headerDescription, header.Mod.Name, num, (384, 20)));
            foreach (Toggle toggle in source)
              ((UIList) this.ToggleList).Add((UIElement) new UIToggle(toggle.Effect, toggle.Mod));
          }
        }
      }
    }
  }
}
