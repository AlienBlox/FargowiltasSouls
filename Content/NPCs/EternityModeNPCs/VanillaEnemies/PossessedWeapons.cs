// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PossessedWeapons
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
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies
{
  public class PossessedWeapons : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(84, 179, 83);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.scale = 2f;
      npc.lifeMax *= 2;
      npc.defense *= 2;
      npc.knockBackResist = 0.0f;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      NPC npc1 = npc;
      ((Entity) npc1).position = Vector2.op_Addition(((Entity) npc1).position, Vector2.op_Division(((Entity) npc).velocity, 2f));
      EModeGlobalNPC.Aura(npc, 400f, 195, true, 119, new Color());
      EModeGlobalNPC.Aura(npc, 400f, 196, true, 14, new Color());
      if ((double) npc.ai[0] != 2.0)
        return;
      npc.ai[1] += (float) (6.0 * (1.0 - (double) npc.life / (double) npc.lifeMax));
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      if (npc.type == 84)
        target.AddBuff(ModContent.BuffType<PurifiedBuff>(), 300, true, false);
      else if (npc.type == 83)
      {
        target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300, true, false);
      }
      else
      {
        if (npc.type != 179)
          return;
        target.AddBuff(ModContent.BuffType<InfestedBuff>(), 300, true, false);
      }
    }
  }
}
