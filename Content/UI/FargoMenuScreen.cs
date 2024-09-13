// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.UI.FargoMenuScreen
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Sky;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.UI
{
  public class FargoMenuScreen : ModMenu
  {
    private bool forgor;

    public virtual Asset<Texture2D> Logo
    {
      get
      {
        return !this.forgor ? ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/MenuLogo", (AssetRequestMode) 2) : ModContent.Request<Texture2D>("FargowiltasSouls/Assets/UI/ForgorMenuLogo", (AssetRequestMode) 2);
      }
    }

    public virtual int Music
    {
      get
      {
        Mod mod;
        return !Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod) ? 50 : MusicLoader.GetMusicSlot(mod, "Assets/Music/Nein");
      }
    }

    public virtual ModSurfaceBackgroundStyle MenuBackgroundStyle
    {
      get => (ModSurfaceBackgroundStyle) ModContent.GetInstance<MainMenuBackgroundStyle>();
    }

    public virtual string DisplayName => Language.GetTextValue("Mods.FargowiltasSouls.UI.MainMenu");

    public virtual void OnSelected()
    {
      ((MainMenuBackgroundStyle) base.MenuBackgroundStyle).fadeIn = 0;
      this.forgor = Utils.NextBool(Main.rand, 100);
    }

    public virtual bool PreDrawLogo(
      SpriteBatch spriteBatch,
      ref Vector2 logoDrawCenter,
      ref float logoRotation,
      ref float logoScale,
      ref Color drawColor)
    {
      return true;
    }
  }
}
