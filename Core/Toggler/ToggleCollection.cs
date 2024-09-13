// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Toggler.ToggleCollection
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System.Collections.Generic;

#nullable disable
namespace FargowiltasSouls.Core.Toggler
{
  public abstract class ToggleCollection
  {
    public abstract string Mod { get; }

    public abstract string SortCategory { get; }

    public abstract float Priority { get; }

    public abstract bool Active { get; }

    public List<Toggle> Load() => new List<Toggle>();
  }
}
