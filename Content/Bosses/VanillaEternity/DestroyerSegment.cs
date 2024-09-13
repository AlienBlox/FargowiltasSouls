// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.DestroyerSegment
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Projectiles.ChallengerItems;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class DestroyerSegment : EModeNPCBehaviour
  {
    public int ProjectileCooldownTimer;
    public int AttackTimer;
    public int ProbeReleaseTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(135, 136);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.ProjectileCooldownTimer);
      binaryWriter.Write7BitEncodedInt(this.AttackTimer);
      binaryWriter.Write7BitEncodedInt(this.ProbeReleaseTimer);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.ProjectileCooldownTimer = binaryReader.Read7BitEncodedInt();
      this.AttackTimer = binaryReader.Read7BitEncodedInt();
      this.ProbeReleaseTimer = binaryReader.Read7BitEncodedInt();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      this.ProbeReleaseTimer = -Main.rand.Next(360);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[68] = true;
      npc.buffImmune[46] = false;
      npc.buffImmune[ModContent.BuffType<TimeFrozenBuff>()] = false;
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      if (WorldSavingSystem.SwarmActive)
        return flag;
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.realLife, new int[1]
      {
        134
      });
      if (npc1 == null || npc.life <= 0 || !((Entity) npc1).active || npc1.life <= 0)
      {
        if (FargoSoulsUtil.HostCheck)
        {
          npc.life = 0;
          if (Main.netMode == 2)
            NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          ((Entity) npc).active = false;
        }
        return flag;
      }
      Destroyer globalNpc = npc1.GetGlobalNPC<Destroyer>();
      npc.defense = npc.defDefense;
      npc.localAI[0] = 0.0f;
      npc.buffImmune[ModContent.BuffType<TimeFrozenBuff>()] = npc1.buffImmune[ModContent.BuffType<TimeFrozenBuff>()];
      if (globalNpc.IsCoiling)
      {
        npc.defense = 0;
        this.ProjectileCooldownTimer = 180;
        if (this.AttackTimer < 0)
          this.AttackTimer = 0;
        Vector2 vector2 = Vector2.op_Addition(((Entity) Main.npc[npc.realLife]).Center, Vector2.op_Multiply(Vector2.Normalize(Utils.RotatedBy(((Entity) Main.npc[npc.realLife]).velocity, 1.5707963705062866 * (double) globalNpc.RotationDirection, new Vector2())), 600f));
        if ((double) ((Entity) npc).Distance(vector2) < 600.0)
          ((Entity) npc).Center = Vector2.op_Addition(vector2, Vector2.op_Multiply(((Entity) npc).DirectionFrom(vector2), 600f));
      }
      if (globalNpc.InPhase2)
        this.AttackTimer = 0;
      if (this.ProjectileCooldownTimer > 0)
      {
        --this.ProjectileCooldownTimer;
        if (this.AttackTimer > 1000)
          this.AttackTimer = 1000;
      }
      if ((double) npc.ai[2] == 0.0)
      {
        if (++this.ProbeReleaseTimer > 60)
        {
          this.ProbeReleaseTimer = -Main.rand.Next(360);
          float num = WorldSavingSystem.MasochistModeReal ? 0.8f : 0.4f;
          if ((double) Main.npc[npc.realLife].life < (double) Main.npc[npc.realLife].lifeMax * (double) num && NPC.CountNPCS(139) < 10 && FargoSoulsUtil.HostCheck && Utils.NextBool(Main.rand, WorldSavingSystem.MasochistModeReal ? 5 : 10))
          {
            npc.ai[2] = 1f;
            npc.HitEffect(0, 10.0, new bool?());
            npc.netUpdate = true;
            FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 139, velocity: new Vector2());
          }
        }
        this.AttackTimer += Main.rand.Next(6);
        if (this.AttackTimer > Main.rand.Next(1200, 22000))
        {
          this.AttackTimer = 0;
          npc.TargetClosest(true);
          if (Collision.CanHit(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0) && FargoSoulsUtil.HostCheck)
          {
            float num = (float) (2.0 + (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) / 360.0);
            if ((double) num > 16.0)
              num = 16f;
            int index = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(num, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)), 100, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            if (index != Main.maxProjectiles)
              Main.projectile[index].timeLeft = Math.Min((int) ((double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) / (double) num) + 180, 600);
          }
          npc.netUpdate = true;
        }
      }
      else
      {
        int num1 = Main.npc[npc.realLife].lifeMax / Main.npc[npc.realLife].life;
        if (num1 > 20)
          num1 = 20;
        this.AttackTimer += Main.rand.Next(2 + num1) + 1;
        if (this.AttackTimer >= Main.rand.Next(3600, 36000))
        {
          this.AttackTimer = 0;
          npc.TargetClosest(true);
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
            float num2 = (float) (28.0 * (1.0 - (double) Main.npc[npc.realLife].life / (double) Main.npc[npc.realLife].lifeMax));
            if ((double) num2 < 12.0)
              num2 = 12f;
            int num3 = (int) ((double) ((Vector2) ref vector2).Length() / (double) num2) / 2;
            if (num3 < 0)
              num3 = 0;
            int num4 = ModContent.ProjectileType<MechElectricOrbHoming>();
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(Vector2.Normalize(vector2), num2), num4, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, (float) npc.target, (float) -num3, 1f);
          }
        }
      }
      if (npc.buffType[0] != 0 && npc.buffType[0] != 46 && npc.buffType[0] != ModContent.BuffType<TimeFrozenBuff>())
        npc.DelBuff(0);
      return flag;
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot)
    {
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.realLife, new int[1]
      {
        134
      });
      if (npc1 == null)
        return base.CanHitPlayer(npc, target, ref CooldownSlot);
      Destroyer globalNpc = npc1.GetGlobalNPC<Destroyer>();
      if (globalNpc.IsCoiling)
      {
        if (globalNpc.AttackModeTimer < 15)
          return false;
      }
      else if (globalNpc.PrepareToCoil)
        return false;
      return true;
    }

    public override void ModifyHitByAnything(
      NPC npc,
      Player player,
      ref NPC.HitModifiers modifiers)
    {
      base.ModifyHitByAnything(npc, player, ref modifiers);
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.realLife, new int[1]
      {
        134
      });
      if (npc1 == null)
        return;
      Destroyer globalNpc = npc1.GetGlobalNPC<Destroyer>();
      if (globalNpc.IsCoiling)
      {
        if (npc.life < npc.lifeMax / 10)
        {
          float num = Math.Min(1f, (float) globalNpc.AttackModeTimer / 480f);
          ref StatModifier local = ref modifiers.FinalDamage;
          local = StatModifier.op_Multiply(local, num);
        }
        else
        {
          ref StatModifier local = ref modifiers.FinalDamage;
          local = StatModifier.op_Multiply(local, 0.1f);
        }
      }
      else if (globalNpc.PrepareToCoil || globalNpc.AttackModeTimer >= 1800 || npc1.life < npc1.lifeMax / 10)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, 0.1f);
      }
      if (((IEnumerable<NPC>) Main.npc).Count<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == npc.type && (double) ((Entity) n).Distance(((Entity) npc).Center) < (double) ((Entity) npc).width * 0.75)) <= 4)
        return;
      modifiers.Null();
    }

    public override void SafeModifyHitByProjectile(
      NPC npc,
      Projectile projectile,
      ref NPC.HitModifiers modifiers)
    {
      base.SafeModifyHitByProjectile(npc, projectile, ref modifiers);
      if (projectile.numHits > 0 && !FargoSoulsUtil.IsSummonDamage(projectile))
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, (float) (0.66666668653488159 + 0.3333333432674408 / (double) projectile.numHits));
      }
      if (projectile.type == 239)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Division(local, 2f);
      }
      if (projectile.type == 476)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, 0.75f);
      }
      if (projectile.type != ModContent.ProjectileType<DecrepitAirstrikeNuke>() && projectile.type != ModContent.ProjectileType<DecrepitAirstrikeNukeSplinter>())
        return;
      ref StatModifier local1 = ref modifiers.FinalDamage;
      local1 = StatModifier.op_Multiply(local1, 0.7f);
    }

    public override void SafeOnHitByProjectile(
      NPC npc,
      Projectile projectile,
      NPC.HitInfo hit,
      int damageDone)
    {
      if (FargoSoulsUtil.IsSummonDamage(projectile) || projectile.damage <= 5)
        return;
      projectile.damage = (int) Math.Min((double) (projectile.damage - 1), (double) projectile.damage * 0.75);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(144, 60, true, false);
      target.AddBuff(ModContent.BuffType<LightningRodBuff>(), 600, true, false);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
    }
  }
}
