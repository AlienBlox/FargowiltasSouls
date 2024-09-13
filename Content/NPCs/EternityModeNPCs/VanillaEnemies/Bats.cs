// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Bats
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies
{
  public class Bats : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(51, 150, 159, 158, 152, 60, 151, 137, 49, 93, 634);
    }

    public override void OnFirstTick(NPC npc) => base.OnFirstTick(npc);

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      switch (npc.type)
      {
        case 49:
          target.AddBuff(30, 300, true, false);
          break;
        case 51:
          target.AddBuff(20, 240, true, false);
          break;
        case 60:
          target.AddBuff(24, 240, true, false);
          break;
        case 93:
          target.AddBuff(31, 240, true, false);
          break;
        case 150:
          target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 600, true, false);
          break;
        case 151:
          target.AddBuff(67, 240, true, false);
          break;
        case 152:
          target.AddBuff(ModContent.BuffType<BloodthirstyBuff>(), 300, true, false);
          break;
      }
    }
  }
}
