// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Desert.AntlionCharger
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Desert
{
  public class AntlionCharger : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(580);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.knockBackResist /= 2f;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.FargoSouls().AddBuffNoStack(160, 60);
      target.AddBuff(32, 120, true, false);
    }

    public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) => base.ModifyNPCLoot(npc, npcLoot);
  }
}
