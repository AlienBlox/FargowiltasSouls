// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Toggler.Toggle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.UI.Chat;

#nullable disable
namespace FargowiltasSouls.Core.Toggler
{
  public class Toggle
  {
    public string Mod;
    public AccessoryEffect Effect;
    public bool ToggleBool;

    public Header Header => this.Effect.ToggleHeader;

    public string Category => this.Effect.ToggleHeader.SortCategory;

    public Toggle(AccessoryEffect effect, string mod)
    {
      this.Effect = effect;
      this.Mod = mod;
      this.ToggleBool = effect.DefaultToggle;
    }

    public override string ToString()
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 4);
      interpolatedStringHandler.AppendLiteral("Mod: ");
      interpolatedStringHandler.AppendFormatted(this.Mod);
      interpolatedStringHandler.AppendLiteral(", Category: ");
      interpolatedStringHandler.AppendFormatted(this.Category);
      interpolatedStringHandler.AppendLiteral(", Effect: ");
      interpolatedStringHandler.AppendFormatted(this.Effect.Name);
      interpolatedStringHandler.AppendLiteral(", Toggled: ");
      interpolatedStringHandler.AppendFormatted<bool>(this.ToggleBool);
      return interpolatedStringHandler.ToStringAndClear();
    }

    public string GetRawToggleName()
    {
      List<TextSnippet> message = ChatManager.ParseMessage(this.Effect.ToggleDescription, Color.White);
      string rawToggleName = "";
      foreach (TextSnippet textSnippet in message)
      {
        if (!textSnippet.Text.StartsWith("["))
          rawToggleName += textSnippet.Text.Trim();
      }
      return rawToggleName;
    }
  }
}
