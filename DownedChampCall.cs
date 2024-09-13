// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.DownedChampCall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Luminance.Core.ModCalls;
using System;
using System.Collections.Generic;

#nullable enable
namespace FargowiltasSouls
{
  internal sealed class DownedChampCall : ModCall
  {
    public virtual 
    #nullable disable
    IEnumerable<string> GetCallCommands()
    {
      yield return "DownedChamp";
      yield return "DownedAbominationn";
    }

    public virtual IEnumerable<Type> GetInputTypes()
    {
      yield return typeof (string);
    }

    protected virtual object SafeProcess(params object[] argsWithoutCommand)
    {
      return (object) WorldSavingSystem.DownedBoss[(int) Enum.Parse<WorldSavingSystem.Downed>(argsWithoutCommand[1] as string, true)];
    }
  }
}
