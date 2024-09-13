// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Stardust.StardustEnemies
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Stardust
{
  public class StardustEnemies : EModeNPCBehaviour
  {
    public static int[] StardustEnemyIDs = new int[10]
    {
      405,
      406,
      402,
      403,
      404,
      409,
      410,
      407,
      408,
      411
    };

    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(StardustEnemies.StardustEnemyIDs);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[68] = true;
    }

    public virtual bool CheckDead(NPC npc) => base.CheckDead(npc);

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(163, 20, true, false);
      target.AddBuff(80, 300, true, false);
    }
  }
}
