// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Toggler.ToggleLoader
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace FargowiltasSouls.Core.Toggler
{
  public static class ToggleLoader
  {
    public static Dictionary<AccessoryEffect, Toggle> LoadedToggles { get; set; }

    public static HashSet<Header> LoadedHeaders { get; set; }

    public static void Load()
    {
    }

    public static void Unload()
    {
      ToggleLoader.LoadedToggles?.Clear();
      ToggleLoader.LoadedHeaders?.Clear();
    }

    public static void LoadTogglesFromAssembly(Assembly assembly)
    {
    }

    public static void RegisterToggle(Toggle toggle)
    {
      if (ToggleLoader.LoadedToggles == null)
        ToggleLoader.LoadedToggles = new Dictionary<AccessoryEffect, Toggle>();
      if (ToggleLoader.LoadedToggles.ContainsKey(toggle.Effect))
        throw new Exception("Toggle of effect " + toggle.Effect.Name + " is already registered");
      ToggleLoader.LoadedToggles.Add(toggle.Effect, toggle);
    }

    public static void RegisterHeader(Header header)
    {
      if (ToggleLoader.LoadedHeaders == null)
        ToggleLoader.LoadedHeaders = new HashSet<Header>();
      ToggleLoader.LoadedHeaders.Add(header);
    }
  }
}
