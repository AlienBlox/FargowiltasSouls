// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.UI.FargoUIManager
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.UI.Elements;
using FargowiltasSouls.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls.Content.UI
{
  public static class FargoUIManager
  {
    public static UserInterface TogglerUserInterface { get; private set; }

    public static UserInterface TogglerToggleUserInterface { get; private set; }

    public static SoulToggler SoulToggler { get; private set; }

    public static SoulTogglerButton SoulTogglerButton { get; private set; }

    private static GameTime LastUpdateUIGameTime { get; set; }

    public static Asset<Texture2D> CheckMark { get; private set; }

    public static Asset<Texture2D> CheckBox { get; private set; }

    public static Asset<Texture2D> Cross { get; private set; }

    public static Asset<Texture2D> SoulTogglerButtonTexture { get; private set; }

    public static Asset<Texture2D> SoulTogglerButton_MouseOverTexture { get; private set; }

    public static Asset<Texture2D> PresetButtonOutline { get; private set; }

    public static Asset<Texture2D> PresetOffButton { get; private set; }

    public static Asset<Texture2D> PresetOnButton { get; private set; }

    public static Asset<Texture2D> PresetMinimalButton { get; private set; }

    public static Asset<Texture2D> PresetCustomButton { get; private set; }

    public static Asset<Texture2D> ReloadButtonTexture { get; private set; }

    public static Asset<Texture2D> DisplayAllButtonTexture { get; private set; }

    public static Asset<Texture2D> OncomingMutantTexture { get; private set; }

    public static Asset<Texture2D> OncomingMutantAuraTexture { get; private set; }

    public static void LoadUI()
    {
      if (Main.dedServ)
        return;
      FargoUIManager.CheckMark = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/CheckMark", (AssetRequestMode) 1);
      FargoUIManager.CheckBox = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/CheckBox", (AssetRequestMode) 1);
      FargoUIManager.Cross = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/Cross", (AssetRequestMode) 1);
      FargoUIManager.SoulTogglerButtonTexture = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/SoulTogglerToggle", (AssetRequestMode) 1);
      FargoUIManager.SoulTogglerButton_MouseOverTexture = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/SoulTogglerToggle_MouseOver", (AssetRequestMode) 1);
      FargoUIManager.PresetButtonOutline = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/PresetOutline", (AssetRequestMode) 1);
      FargoUIManager.PresetOffButton = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/PresetOff", (AssetRequestMode) 1);
      FargoUIManager.PresetOnButton = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/PresetOn", (AssetRequestMode) 1);
      FargoUIManager.PresetMinimalButton = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/PresetMinimal", (AssetRequestMode) 1);
      FargoUIManager.PresetCustomButton = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/PresetCustom", (AssetRequestMode) 1);
      FargoUIManager.DisplayAllButtonTexture = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/DisplayAllButton", (AssetRequestMode) 1);
      FargoUIManager.ReloadButtonTexture = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/ReloadButton", (AssetRequestMode) 1);
      FargoUIManager.OncomingMutantTexture = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/OncomingMutant", (AssetRequestMode) 1);
      FargoUIManager.OncomingMutantAuraTexture = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/OncomingMutantAura", (AssetRequestMode) 1);
      FargoUIManager.TogglerUserInterface = new UserInterface();
      FargoUIManager.TogglerToggleUserInterface = new UserInterface();
      FargoUIManager.SoulToggler = new SoulToggler();
      ((UIElement) FargoUIManager.SoulToggler).Activate();
      FargoUIManager.SoulTogglerButton = new SoulTogglerButton();
      ((UIElement) FargoUIManager.SoulTogglerButton).Activate();
      FargoUIManager.TogglerToggleUserInterface.SetState((UIState) FargoUIManager.SoulTogglerButton);
    }

    public static void UpdateUI(GameTime gameTime)
    {
      FargoUIManager.LastUpdateUIGameTime = gameTime;
      if (!Main.playerInventory && SoulConfig.Instance.HideTogglerWhenInventoryIsClosed)
        FargoUIManager.CloseSoulToggler();
      if (!Main.playerInventory)
        FargoUIManager.CloseSoulTogglerButton();
      else
        FargoUIManager.OpenSoulTogglerButton();
      if (FargoUIManager.TogglerUserInterface?.CurrentState != null)
        FargoUIManager.TogglerUserInterface.Update(gameTime);
      if (FargoUIManager.TogglerToggleUserInterface?.CurrentState == null)
        return;
      FargoUIManager.TogglerToggleUserInterface.Update(gameTime);
    }

    public static bool IsSoulTogglerOpen()
    {
      return FargoUIManager.TogglerUserInterface?.CurrentState == null;
    }

    public static void CloseSoulToggler()
    {
      FargoUIManager.TogglerUserInterface?.SetState((UIState) null);
      if (SoulConfig.Instance.ToggleSearchReset)
        FargoUIManager.SoulToggler.SearchBar.Input = "";
      FargoUIManager.SoulToggler.NeedsToggleListBuilding = true;
    }

    public static bool IsTogglerOpen()
    {
      return FargoUIManager.TogglerUserInterface.CurrentState == FargoUIManager.SoulToggler;
    }

    public static void OpenToggler()
    {
      FargoUIManager.TogglerUserInterface.SetState((UIState) FargoUIManager.SoulToggler);
    }

    public static void CloseSoulTogglerButton()
    {
      FargoUIManager.TogglerToggleUserInterface.SetState((UIState) null);
    }

    public static void OpenSoulTogglerButton()
    {
      FargoUIManager.TogglerToggleUserInterface.SetState((UIState) FargoUIManager.SoulTogglerButton);
    }

    public static void ToggleSoulToggler()
    {
      if (FargoUIManager.IsSoulTogglerOpen())
      {
        SoundEngine.PlaySound(ref SoundID.MenuOpen, new Vector2?(), (SoundUpdateCallback) null);
        FargoUIManager.OpenToggler();
      }
      else
      {
        if (!FargoUIManager.IsTogglerOpen())
          return;
        SoundEngine.PlaySound(ref SoundID.MenuClose, new Vector2?(), (SoundUpdateCallback) null);
        FargoUIManager.CloseSoulToggler();
      }
    }

    public static void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
      int index = layers.FindIndex((Predicate<GameInterfaceLayer>) (layer => layer.Name == "Vanilla: Inventory"));
      if (index == -1)
        return;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      layers.Insert(index - 1, (GameInterfaceLayer) new LegacyGameInterfaceLayer("Fargos: Soul Toggler", FargoUIManager.\u003C\u003Ec.\u003C\u003E9__85_1 ?? (FargoUIManager.\u003C\u003Ec.\u003C\u003E9__85_1 = new GameInterfaceDrawMethod((object) FargoUIManager.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CModifyInterfaceLayers\u003Eb__85_1))), (InterfaceScaleType) 1));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      layers.Insert(index, (GameInterfaceLayer) new LegacyGameInterfaceLayer("Fargos: Soul Toggler Toggler", FargoUIManager.\u003C\u003Ec.\u003C\u003E9__85_2 ?? (FargoUIManager.\u003C\u003Ec.\u003C\u003E9__85_2 = new GameInterfaceDrawMethod((object) FargoUIManager.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CModifyInterfaceLayers\u003Eb__85_2))), (InterfaceScaleType) 1));
    }
  }
}
