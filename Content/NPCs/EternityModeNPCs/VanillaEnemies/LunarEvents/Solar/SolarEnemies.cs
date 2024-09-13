// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar.SolarEnemies
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar
{
  public class SolarEnemies : EModeNPCBehaviour
  {
    public static int[] SolarEnemyIDs = new int[11]
    {
      412,
      413,
      414,
      418,
      419,
      415,
      416,
      518,
      417,
      516,
      519
    };

    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(SolarEnemies.SolarEnemyIDs);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[68] = true;
      npc.buffImmune[24] = true;
      npc.buffImmune[323] = true;
      npc.buffImmune[69] = true;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(24, 600, true, false);
      target.AddBuff(67, 300, true, false);
    }
  }
}
