// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Toggler.Header
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;

#nullable disable
namespace FargowiltasSouls.Core.Toggler
{
  public abstract class Header : ModType
  {
    public abstract string SortCategory { get; }

    public abstract float Priority { get; }

    public abstract int Item { get; }

    public string HeaderDescription
    {
      get
      {
        string textValue = Language.GetTextValue("Mods." + this.Mod.Name + ".Toggler." + this.Name);
        if (this.Item <= 0)
          return textValue;
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
        interpolatedStringHandler.AppendLiteral("[i:");
        interpolatedStringHandler.AppendFormatted<int>(this.Item);
        interpolatedStringHandler.AppendLiteral("]");
        return interpolatedStringHandler.ToStringAndClear() + " " + textValue;
      }
    }

    public virtual void Load()
    {
      ToggleLoader.RegisterHeader(this);
      ModTypeLookup<Header>.Register(this);
    }

    protected virtual void Register()
    {
    }

    public virtual string ToString()
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(37, 4);
      interpolatedStringHandler.AppendLiteral("Mod: ");
      interpolatedStringHandler.AppendFormatted<Mod>(this.Mod);
      interpolatedStringHandler.AppendLiteral(", Item: ");
      interpolatedStringHandler.AppendFormatted<int>(this.Item);
      interpolatedStringHandler.AppendLiteral(", Category: ");
      interpolatedStringHandler.AppendFormatted(this.SortCategory);
      interpolatedStringHandler.AppendLiteral(", Priority: ");
      interpolatedStringHandler.AppendFormatted<float>(this.Priority);
      return interpolatedStringHandler.ToStringAndClear();
    }

    public string GetRawToggleName()
    {
      List<TextSnippet> message = ChatManager.ParseMessage(this.HeaderDescription, Color.White);
      string rawToggleName = "";
      foreach (TextSnippet textSnippet in message)
      {
        if (!textSnippet.Text.StartsWith("["))
          rawToggleName += textSnippet.Text.Trim();
      }
      return rawToggleName;
    }

    public static T GetHeader<T>() where T : Header => ModContent.GetInstance<T>();

    public static Header GetHeaderFromItem<T>() where T : ModItem
    {
      return ToggleLoader.LoadedHeaders.FirstOrDefault<Header>((Func<Header, bool>) (h => h.Item == ModContent.ItemType<T>()));
    }

    public static Header GetHeaderFromItemType(int type)
    {
      return ToggleLoader.LoadedHeaders.FirstOrDefault<Header>((Func<Header, bool>) (h => h.Item == type));
    }
  }
}
