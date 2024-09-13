// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.EaterofWorlds
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class EaterofWorlds : EModeNPCBehaviour
  {
    private int MassDefenseTimer;
    private bool UseMassDefense;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(13, 14, 15);

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[39] = true;
    }

    public override bool SafePreAI(NPC npc)
    {
      if (--this.MassDefenseTimer < 0)
      {
        this.MassDefenseTimer = 15;
        if (npc.type != 13)
        {
          if (npc.type == 14)
          {
            if (FargoSoulsUtil.NPCExists(npc.ai[1], 13) == null)
              goto label_7;
          }
          else
            goto label_7;
        }
        npc.defense = npc.defDefense;
        this.UseMassDefense = false;
        int num1 = ((IEnumerable<NPC>) Main.npc).Count<NPC>((Func<NPC, bool>) (n =>
        {
          if (!((Entity) n).active)
            return false;
          return n.type == 14 || n.type == 13 || n.type == 15;
        }));
        int num2 = NPC.CountNPCS(13);
        if (num1 > 12 && num2 < num1 / 5 + 1)
        {
          this.UseMassDefense = true;
          npc.defense += 30;
          if (npc.life < npc.lifeMax / 2)
            npc.life += 2;
        }
      }
label_7:
      return base.SafePreAI(npc);
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      if (this.UseMassDefense)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Division(local, 2f);
      }
      base.ModifyIncomingHit(npc, ref modifiers);
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (WorldSavingSystem.SwarmActive)
        return base.CheckDead(npc);
      int num = 0;
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        if (((Entity) Main.npc[index]).active && index != ((Entity) npc).whoAmI && (Main.npc[index].type == 13 || Main.npc[index].type == 14 || Main.npc[index].type == 15))
          ++num;
      }
      return num <= 2 && base.CheckDead(npc);
    }

    public override void SafeModifyHitByItem(
      NPC npc,
      Player player,
      Item item,
      ref NPC.HitModifiers modifiers)
    {
      base.SafeModifyHitByItem(npc, player, item, ref modifiers);
      if (EaterofWorldsHead.HaveSpawnDR <= 0)
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Division(local, 10f);
    }

    public override void SafeModifyHitByProjectile(
      NPC npc,
      Projectile projectile,
      ref NPC.HitModifiers modifiers)
    {
      base.SafeModifyHitByProjectile(npc, projectile, ref modifiers);
      if (EaterofWorldsHead.HaveSpawnDR <= 0)
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Division(local, (float) (projectile.numHits + 1));
    }

    public override void SafeOnHitByProjectile(
      NPC npc,
      Projectile projectile,
      NPC.HitInfo hit,
      int damageDone)
    {
      if (!FargoSoulsUtil.IsSummonDamage(projectile) && projectile.damage > 5)
        projectile.damage = (int) Math.Min((double) (projectile.damage - 1), (double) projectile.damage * 0.8);
      base.SafeOnHitByProjectile(npc, projectile, hit, damageDone);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(39, 180, true, false);
      target.AddBuff(ModContent.BuffType<RottingBuff>(), 600, true, false);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
    }
  }
}
