﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.GoblinInvasion.ChaosBall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.GoblinInvasion
{
  public class ChaosBall : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(30);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (!Main.hardMode)
        return;
      NPC npc1 = npc;
      ((Entity) npc1).position = Vector2.op_Addition(((Entity) npc1).position, ((Entity) npc).velocity);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300, true, false);
    }
  }
}