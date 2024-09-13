﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PumpkinMoon.Hellhound
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PumpkinMoon
{
  public class Hellhound : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(329);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lavaImmune = true;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(148, 3600, true, false);
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 600, true, false);
    }
  }
}
