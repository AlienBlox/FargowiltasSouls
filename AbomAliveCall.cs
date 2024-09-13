// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.AbomAliveCall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using Luminance.Core.ModCalls;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;

#nullable enable
namespace FargowiltasSouls
{
  internal sealed class AbomAliveCall : ModCall
  {
    public virtual 
    #nullable disable
    IEnumerable<string> GetCallCommands()
    {
      yield return "AbomAlive";
    }

    public virtual IEnumerable<Type> GetInputTypes() => (IEnumerable<Type>) null;

    protected virtual object SafeProcess(params object[] argsWithoutCommand)
    {
      return (object) FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.abomBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>());
    }
  }
}
