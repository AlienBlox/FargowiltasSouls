// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.GoblinInvasion.GoblinSorcerer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.NPCMatching;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.GoblinInvasion
{
  public class GoblinSorcerer : Teleporters
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(29);

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (Main.hardMode || NPC.downedSlimeKing || NPC.downedBoss1 || NPC.CountNPCS(npc.type) <= 2)
        return;
      npc.Transform(26);
    }
  }
}
