// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.CultistDragon
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class CultistDragon : EModeNPCBehaviour
  {
    public int DamageReductionTimer;

    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(455, 456, 457, 458, 454, 459);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[68] = true;
      if (npc.type != 454)
        return;
      if (WorldSavingSystem.MasochistModeReal && FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.cultBoss, 439))
        ((Entity) npc).Center = ((Entity) Main.npc[EModeGlobalNPC.cultBoss]).Center;
      if (NPC.CountNPCS(521) >= 4 || !FargoSoulsUtil.HostCheck)
        return;
      FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 521, velocity: new Vector2());
    }

    public override bool SafePreAI(NPC npc)
    {
      int num = base.SafePreAI(npc) ? 1 : 0;
      ++this.DamageReductionTimer;
      return num != 0;
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, Math.Min(1f, (float) this.DamageReductionTimer / 300f));
      base.ModifyIncomingHit(npc, ref modifiers);
    }

    public override void SafeModifyHitByProjectile(
      NPC npc,
      Projectile projectile,
      ref NPC.HitModifiers modifiers)
    {
      base.SafeModifyHitByProjectile(npc, projectile, ref modifiers);
      if (projectile.maxPenetrate > 1)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Division(local, (float) projectile.maxPenetrate);
      }
      else
      {
        if (projectile.maxPenetrate >= 0)
          return;
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Division(local, 4f);
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, true, false);
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 300, true, false);
    }
  }
}
