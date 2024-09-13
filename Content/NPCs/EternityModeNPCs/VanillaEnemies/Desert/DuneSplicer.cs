// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Desert.DuneSplicer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Desert
{
  public class DuneSplicer : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(511, 510, 512);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      if (Main.hardMode)
      {
        npc.lifeMax *= 3;
      }
      else
      {
        npc.defense /= 2;
        npc.damage /= 2;
      }
    }

    public override bool SafePreAI(NPC npc)
    {
      int index = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
      if (index != -1 && (double) ((Entity) npc).Distance(((Entity) Main.player[index]).Center) < 2400.0)
        Main.player[index].ZoneUndergroundDesert = true;
      return base.SafePreAI(npc);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<ClippedWingsBuff>(), 300, true, false);
    }

    public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) => base.ModifyNPCLoot(npc, npcLoot);
  }
}
