﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.NekomiArmorCall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Luminance.Core.ModCalls;
using System;
using System.Collections.Generic;
using Terraria;

#nullable enable
namespace FargowiltasSouls
{
  internal sealed class NekomiArmorCall : ModCall
  {
    public virtual 
    #nullable disable
    IEnumerable<string> GetCallCommands()
    {
      yield return "NekomiArmor";
      yield return "NekomiArmour";
    }

    public virtual IEnumerable<Type> GetInputTypes() => (IEnumerable<Type>) null;

    protected virtual object SafeProcess(params object[] argsWithoutCommand)
    {
      return (object) Main.LocalPlayer.FargoSouls().NekomiSet;
    }
  }
}