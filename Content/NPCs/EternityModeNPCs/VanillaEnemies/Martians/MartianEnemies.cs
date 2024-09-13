﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Martians.MartianEnemies
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Martians
{
  public class MartianEnemies : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(390, 389, 386, 383, 382, 385, 381, 388, 520, 387, 391, 390, 392, 394, 395, 393);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[31] = true;
      npc.buffImmune[144] = true;
      npc.buffImmune[68] = true;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(144, 300, true, false);
    }
  }
}