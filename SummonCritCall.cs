// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.SummonCritCall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Luminance.Core.ModCalls;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable enable
namespace FargowiltasSouls
{
  internal sealed class SummonCritCall : ModCall
  {
    public virtual 
    #nullable disable
    IEnumerable<string> GetCallCommands()
    {
      yield return "SummonCrit";
      yield return "SummonCritChance";
      yield return "GetSummonCrit";
      yield return "GetSummonCritChance";
      yield return "SummonerCrit";
      yield return "SummonerCritChance";
      yield return "GetSummonerCrit";
      yield return "GetSummonerCritChance";
      yield return "MinionCrit";
      yield return "MinionCritChance";
      yield return "GetMinionCrit";
      yield return "GetMinionCritChance";
    }

    public virtual IEnumerable<Type> GetInputTypes() => (IEnumerable<Type>) null;

    protected virtual object SafeProcess(params object[] argsWithoutCommand)
    {
      return (object) (int) Main.LocalPlayer.ActualClassCrit(DamageClass.Summon);
    }
  }
}
