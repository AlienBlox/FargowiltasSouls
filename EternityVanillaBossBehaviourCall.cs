// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.EternityVanillaBossBehaviourCall
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
  internal sealed class EternityVanillaBossBehaviourCall : ModCall
  {
    public virtual 
    #nullable disable
    IEnumerable<string> GetCallCommands()
    {
      yield return "EternityVanillaBossBehaviour";
    }

    public virtual IEnumerable<Type> GetInputTypes()
    {
      yield return typeof (bool);
    }

    protected virtual object SafeProcess(params object[] argsWithoutCommand)
    {
      int num = WorldSavingSystem.EternityVanillaBehaviour ? 1 : 0;
      bool? nullable = argsWithoutCommand[0] as bool?;
      if (nullable.HasValue)
        WorldSavingSystem.EternityVanillaBehaviour = nullable.Value;
      return (object) (bool) num;
    }
  }
}
