// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.OOA.DD2EterniaCrystal
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.OOA
{
  public class DD2EterniaCrystal : EModeNPCBehaviour
  {
    public int InvulTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(548);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.betsyBoss, 551) && EModeGlobalNPC.betsyBoss >= 0 && EModeGlobalNPC.betsyBoss != Main.maxNPCs && ((IEnumerable<Player>) Main.player).Any<Player>((Func<Player, bool>) (p => ((Entity) p).active && !p.dead)))
      {
        this.InvulTimer = 15;
        if (npc.life < npc.lifeMax && npc.life < 500)
          ++npc.life;
      }
      if (this.InvulTimer > 0)
      {
        npc.chaseable = false;
        --this.InvulTimer;
      }
      else
        npc.chaseable = true;
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      if (this.InvulTimer > 0)
        modifiers.Null();
      base.ModifyIncomingHit(npc, ref modifiers);
    }
  }
}
