// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.AncientLight
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class AncientLight : EModeNPCBehaviour
  {
    public int Timer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(522);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lavaImmune = true;
      npc.dontTakeDamage = true;
      npc.immortal = true;
      npc.chaseable = false;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[68] = true;
      npc.buffImmune[24] = true;
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      if (WorldSavingSystem.SwarmActive)
        return flag;
      npc.dontTakeDamage = true;
      npc.immortal = true;
      npc.chaseable = false;
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.moonBoss, 398))
      {
        if (npc.HasPlayerTarget)
        {
          Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
          ((Vector2) ref vector2_1).Normalize();
          Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 9f);
          npc.ai[2] += vector2_2.X / 100f;
          if ((double) npc.ai[2] > 9.0)
            npc.ai[2] = 9f;
          if ((double) npc.ai[2] < -9.0)
            npc.ai[2] = -9f;
          npc.ai[3] += vector2_2.Y / 100f;
          if ((double) npc.ai[3] > 9.0)
            npc.ai[3] = 9f;
          if ((double) npc.ai[3] < -9.0)
            npc.ai[3] = -9f;
        }
        else
          npc.TargetClosest(false);
        ++this.Timer;
        if (this.Timer > 240)
        {
          npc.HitEffect(0, 9999.0, new bool?());
          ((Entity) npc).active = false;
        }
        ((Entity) npc).velocity.X = npc.ai[2];
        ((Entity) npc).velocity.Y = npc.ai[3];
      }
      else if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.cultBoss, 439) && !WorldSavingSystem.MasochistModeReal)
      {
        if (++this.Timer > 20 && this.Timer < 40)
        {
          NPC npc1 = npc;
          ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, ((Entity) npc).velocity);
          return false;
        }
        if (this.Timer > 180)
        {
          npc.dontTakeDamage = false;
          npc.immortal = false;
        }
      }
      return flag;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<PurifiedBuff>(), 300, true, false);
    }
  }
}
