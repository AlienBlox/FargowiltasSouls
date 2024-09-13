// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.EModeGlobalProjectile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Earth;
using FargowiltasSouls.Content.Bosses.Champions.Spirit;
using FargowiltasSouls.Content.Bosses.Champions.Terra;
using FargowiltasSouls.Content.Bosses.Champions.Timber;
using FargowiltasSouls.Content.Bosses.Champions.Will;
using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class EModeGlobalProjectile : GlobalProjectile
  {
    public bool HasKillCooldown;
    public bool EModeCanHurt = true;
    public int NerfDamageBasedOnProjTypeCount;
    public bool altBehaviour;
    private int counter;
    private bool preAICheckDone;
    private bool firstTickAICheckDone;
    public static Dictionary<int, bool> IgnoreMinionNerf = new Dictionary<int, bool>();
    private int FadeTimer;

    public virtual bool InstancePerEntity => true;

    public virtual void Unload()
    {
      ((ModType) this).Unload();
      EModeGlobalProjectile.IgnoreMinionNerf.Clear();
    }

    public virtual void SetStaticDefaults()
    {
      EModeGlobalProjectile.IgnoreMinionNerf[625] = true;
      EModeGlobalProjectile.IgnoreMinionNerf[626] = true;
      EModeGlobalProjectile.IgnoreMinionNerf[627] = true;
      EModeGlobalProjectile.IgnoreMinionNerf[628] = true;
      EModeGlobalProjectile.IgnoreMinionNerf[831] = true;
      EModeGlobalProjectile.IgnoreMinionNerf[833] = true;
      EModeGlobalProjectile.IgnoreMinionNerf[834] = true;
      EModeGlobalProjectile.IgnoreMinionNerf[835] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[756] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[5] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[36] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[496] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[638] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[27] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[22] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[274] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[263] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[116] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[467] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[468] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[385] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[811] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[873] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[872] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[919] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[180] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[5] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[36] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[638] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[188] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[187] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[654] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[258] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[259] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[462] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[452] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[454] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[290] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[292] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[291] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[129] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[83] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[288] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[96] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[270] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[696] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[926] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[102] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[657] = true;
      A_SourceNPCGlobalProjectile.SourceNPCSync[575] = true;
    }

    public virtual void SetDefaults(Projectile projectile)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      int type = projectile.type;
      if (type <= 449)
      {
        if (type <= 110)
        {
          if (type <= 89)
          {
            if (type != 83)
              return;
          }
          else
          {
            if ((uint) (type - 91) <= 1U)
              return;
            if (type != 101)
            {
              if (type != 110)
                return;
              projectile.tileCollide = false;
              projectile.timeLeft = 600;
              return;
            }
          }
          projectile.tileCollide = false;
        }
        else if (type <= 240)
        {
          if (type != 115)
          {
            if (type != 207)
            {
              if (type != 240)
                return;
              projectile.scale = 2f;
            }
            else
            {
              projectile.extraUpdates = 1;
              projectile.timeLeft = 150;
            }
          }
          else
            ++projectile.extraUpdates;
        }
        else if (type != 384 && type != 386)
        {
          if (type != 449)
            return;
          projectile.tileCollide = false;
        }
        else
        {
          if (WorldSavingSystem.MasochistModeReal)
            return;
          this.EModeCanHurt = false;
          projectile.hide = true;
        }
      }
      else
      {
        if (type <= 873)
        {
          if (type <= 687)
          {
            if (type != 454)
            {
              if (type != 593)
              {
                if (type != 687)
                  return;
                projectile.tileCollide = false;
                projectile.penetrate = -1;
                return;
              }
              projectile.scale *= 1.5f;
              return;
            }
          }
          else if (type != 728)
          {
            if (type != 811)
            {
              if ((uint) (type - 872) > 1U)
                return;
            }
            else
            {
              projectile.tileCollide = false;
              return;
            }
          }
          else
          {
            projectile.penetrate = 7;
            return;
          }
        }
        else if (type <= 933)
        {
          if (type != 919)
          {
            if (type != 920)
            {
              if (type != 933 || WorldSavingSystem.DownedMutant)
                return;
              projectile.usesLocalNPCImmunity = false;
              projectile.localNPCHitCooldown = 0;
              projectile.usesIDStaticNPCImmunity = true;
              projectile.idStaticNPCHitCooldown = !WorldSavingSystem.DownedAbom ? 5 : 3;
              projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
              return;
            }
            projectile.scale *= 1.5f;
            projectile.timeLeft = 180;
            projectile.tileCollide = false;
            return;
          }
        }
        else
        {
          if (type != 962)
          {
            if (type != 967)
            {
              if (type != 969)
                return;
              projectile.idStaticNPCHitCooldown = 10;
              projectile.penetrate = 45;
              return;
            }
            ++projectile.extraUpdates;
            return;
          }
          projectile.extraUpdates = 1;
          return;
        }
        this.EModeCanHurt = false;
      }
    }

    private static bool NonSwarmFight(Projectile projectile, params int[] types)
    {
      if (WorldSavingSystem.SwarmActive)
        return false;
      NPC sourceNpc = projectile.GetSourceNPC();
      return projectile.GetSourceNPC() != null && ((IEnumerable<int>) types).Contains<int>(sourceNpc.type);
    }

    public virtual void OnSpawn(Projectile projectile, IEntitySource source)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      Projectile projectile1 = (Projectile) null;
      if (source is EntitySource_Parent entitySourceParent && entitySourceParent.Entity is Projectile)
        projectile1 = entitySourceParent.Entity as Projectile;
      if (FargoSoulsUtil.IsSummonDamage(projectile, includeWhips: false))
      {
        bool flag1;
        if (projectile.minion && !(EModeGlobalProjectile.IgnoreMinionNerf.TryGetValue(projectile.type, out flag1) & flag1))
        {
          this.NerfDamageBasedOnProjTypeCount = projectile.type;
        }
        else
        {
          bool flag2;
          if (projectile1 != null && !(EModeGlobalProjectile.IgnoreMinionNerf.TryGetValue(projectile1.type, out flag2) & flag2))
            this.NerfDamageBasedOnProjTypeCount = projectile1.Eternity().NerfDamageBasedOnProjTypeCount;
        }
      }
      switch (projectile.type)
      {
        case 12:
          if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()))
            break;
          ((Entity) projectile).active = false;
          break;
        case 305:
          if (Main.player[projectile.owner].statLife > Main.player[projectile.owner].statLifeMax2 / 3)
            Main.player[projectile.owner].lifeSteal -= projectile.ai[1];
          if (Main.player[projectile.owner].statLife <= Main.player[projectile.owner].statLifeMax2 * 2 / 3)
            break;
          Main.player[projectile.owner].lifeSteal -= projectile.ai[1];
          break;
        case 386:
          if (!EModeGlobalProjectile.NonSwarmFight(projectile, 370) || !WorldSavingSystem.MasochistModeReal || (double) projectile.ai[1] != 25.0 && (projectile1 == null || !projectile1.Eternity().altBehaviour))
            break;
          this.altBehaviour = true;
          break;
        case 876:
          projectile.originalDamage = projectile.damage;
          break;
        case 961:
          if (WorldSavingSystem.SwarmActive)
            break;
          if (WorldSavingSystem.MasochistModeReal)
            projectile.ai[0] -= 20f;
          if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.deerBoss, 668) && (double) Main.npc[EModeGlobalNPC.deerBoss].ai[0] == 4.0)
          {
            projectile.ai[0] -= 30f;
            if (Main.npc[EModeGlobalNPC.deerBoss].GetGlobalNPC<Deerclops>().EnteredPhase2)
              projectile.ai[0] -= 30f;
            if (Main.npc[EModeGlobalNPC.deerBoss].GetGlobalNPC<Deerclops>().EnteredPhase3)
              projectile.ai[0] -= 120f;
          }
          if (projectile.GetSourceNPC() == null || projectile.GetSourceNPC().type != 668 || projectile1 != null || ((double) projectile.GetSourceNPC().ai[0] != 1.0 || (double) projectile.GetSourceNPC().ai[1] != 52.0) && ((double) projectile.GetSourceNPC().ai[0] != 4.0 || (double) projectile.GetSourceNPC().ai[1] != 70.0 || projectile.GetSourceNPC().GetGlobalNPC<Deerclops>().DoLaserAttack))
            break;
          bool flag3 = (double) projectile.GetSourceNPC().ai[0] == 1.0;
          bool flag4 = true;
          if (flag3)
          {
            for (int index = 0; index < Main.maxProjectiles; ++index)
            {
              if (((Entity) Main.projectile[index]).active && Main.projectile[index].type == projectile.type && (double) Main.projectile[index].scale == (double) projectile.scale && Math.Sign(((Entity) Main.projectile[index]).velocity.X) == Math.Sign(((Entity) projectile).velocity.X))
              {
                if (index != ((Entity) projectile).whoAmI)
                {
                  flag4 = false;
                  break;
                }
                break;
              }
            }
          }
          if (!flag4)
            break;
          float num = 1.3f;
          if (projectile.GetSourceNPC().GetGlobalNPC<Deerclops>().EnteredPhase2)
            num = 1.35f;
          Vector2 vector2 = Vector2.op_Addition(((Entity) projectile).Center, Vector2.op_Multiply(200f, Vector2.Normalize(((Entity) projectile).velocity)));
          if (!FargoSoulsUtil.HostCheck)
            break;
          Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), vector2, ((Entity) projectile).velocity, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0.0f, num, 0.0f);
          if (flag3)
          {
            Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), vector2, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) Math.Sign(((Entity) projectile).velocity.X)), ((Vector2) ref ((Entity) projectile).velocity).Length()), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0.0f, num, 0.0f);
            Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), vector2, new Vector2(((Entity) projectile).velocity.X, -((Entity) projectile).velocity.Y), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0.0f, num, 0.0f);
            break;
          }
          Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, new Vector2(-((Entity) projectile).velocity.X, ((Entity) projectile).velocity.Y), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0.0f, num, 0.0f);
          if ((double) ((Entity) projectile).Center.Y < (double) ((Entity) projectile.GetSourceNPC()).Center.Y)
          {
            Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, Vector2.op_UnaryNegation(((Entity) projectile).velocity), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0.0f, num, 0.0f);
            Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, new Vector2(((Entity) projectile).velocity.X, -((Entity) projectile).velocity.Y), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0.0f, num, 0.0f);
            break;
          }
          Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), vector2, new Vector2(-((Entity) projectile).velocity.X, ((Entity) projectile).velocity.Y), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0.0f, num, 0.0f);
          break;
      }
    }

    public void OnFirstTick(Projectile projectile)
    {
      if (!A_SourceNPCGlobalProjectile.NeedsSync(A_SourceNPCGlobalProjectile.SourceNPCSync, projectile.type))
        return;
      NPC sourceNpc = projectile.GetSourceNPC();
      switch (projectile.type)
      {
        case 5:
        case 22:
        case 27:
        case 36:
        case 116:
        case 263:
        case 274:
        case 496:
        case 638:
        case 756:
          if (sourceNpc == null || sourceNpc.friendly || sourceNpc.townNPC)
            break;
          projectile.friendly = false;
          projectile.hostile = true;
          projectile.DamageType = DamageClass.Default;
          break;
        case 385:
          if (sourceNpc == null || sourceNpc.type != 370 || !sourceNpc.GetGlobalNPC<DukeFishron>().IsEX)
            break;
          ++projectile.extraUpdates;
          break;
        case 462:
          if (!EModeGlobalProjectile.NonSwarmFight(projectile, 397, 396, 400) || WorldSavingSystem.MasochistModeReal && Main.getGoodWorld)
            break;
          if (FargoSoulsUtil.HostCheck)
          {
            for (int index = -2; index <= 2; ++index)
              Projectile.NewProjectile(Entity.InheritSource((Entity) projectile), ((Entity) projectile).Center, Vector2.op_Multiply(1.5f, Utils.RotatedBy(Vector2.Normalize(((Entity) projectile).velocity), Math.PI / 4.0 * (double) index, new Vector2())), ModContent.ProjectileType<PhantasmalBolt2>(), projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
          projectile.Kill();
          break;
        case 467:
          if (!EModeGlobalProjectile.NonSwarmFight(projectile, 439) || !sourceNpc.GetGlobalNPC<LunaticCultist>().EnteredPhase2)
            break;
          projectile.timeLeft = 0;
          this.EModeCanHurt = false;
          break;
        case 468:
          if (!EModeGlobalProjectile.NonSwarmFight(projectile, 439))
            break;
          projectile.timeLeft = 0;
          this.EModeCanHurt = false;
          break;
        case 811:
          if (sourceNpc == null || sourceNpc.type != 619)
            break;
          projectile.damage /= 2;
          break;
        case 872:
          if (!EModeGlobalProjectile.NonSwarmFight(projectile, 636))
            break;
          projectile.timeLeft += 60;
          projectile.localAI[1] = Utils.ToRotation(((Entity) projectile).velocity);
          if ((double) sourceNpc.ai[0] == 7.0 && (double) sourceNpc.ai[1] >= (double) byte.MaxValue && sourceNpc.GetGlobalNPC<EmpressofLight>().DoParallelSwordWalls)
          {
            this.altBehaviour = true;
            break;
          }
          if (sourceNpc.GetGlobalNPC<EmpressofLight>().AttackTimer != 1)
            break;
          projectile.localAI[0] = 1f;
          break;
        case 873:
          if (!EModeGlobalProjectile.NonSwarmFight(projectile, 636))
            break;
          if (WorldSavingSystem.MasochistModeReal && (double) sourceNpc.ai[0] != 8.0 && (double) sourceNpc.ai[0] != 9.0)
            this.EModeCanHurt = true;
          if ((double) sourceNpc.ai[0] != 12.0)
            break;
          Projectile projectile1 = projectile;
          ((Entity) projectile1).velocity = Vector2.op_Multiply(((Entity) projectile1).velocity, 0.7f);
          break;
        case 919:
          if (!EModeGlobalProjectile.NonSwarmFight(projectile, 636) || (double) sourceNpc.ai[0] != 7.0)
            break;
          if ((double) sourceNpc.ai[1] < (double) byte.MaxValue)
          {
            Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2(Utils.NextFloat(Main.rand, 6.28318548f)), 2f);
            Projectile projectile2 = projectile;
            ((Entity) projectile2).position = Vector2.op_Subtraction(((Entity) projectile2).position, Vector2.op_Multiply(vector2, 60f));
            ((Entity) projectile).velocity = vector2;
            break;
          }
          if (!sourceNpc.GetGlobalNPC<EmpressofLight>().DoParallelSwordWalls)
            break;
          this.altBehaviour = true;
          break;
        default:
          if (!SpearRework.ReworkedSpears.Contains(projectile.type))
            break;
          projectile.damage = (int) ((double) projectile.damage * 1.5);
          break;
      }
    }

    public virtual bool? CanDamage(Projectile projectile)
    {
      if (!WorldSavingSystem.EternityMode)
        return base.CanDamage(projectile);
      return !this.EModeCanHurt ? new bool?(false) : base.CanDamage(projectile);
    }

    public virtual bool PreAI(Projectile projectile)
    {
      if (!WorldSavingSystem.EternityMode)
      {
        this.preAICheckDone = true;
        return base.PreAI(projectile);
      }
      if (!this.preAICheckDone)
      {
        this.preAICheckDone = true;
        this.OnFirstTick(projectile);
      }
      if (projectile.friendly && Main.player[projectile.owner].HasBuff(ModContent.BuffType<HypothermiaBuff>()) && (double) ((Vector2) ref ((Entity) projectile).velocity).Length() > 3.0)
      {
        Projectile projectile1 = projectile;
        ((Entity) projectile1).velocity = Vector2.op_Multiply(((Entity) projectile1).velocity, 0.975f);
      }
      ++this.counter;
      if (this.counter < 30 && (double) projectile.ai[0] == 15.0 && !WorldSavingSystem.MasochistModeReal && (projectile.type == 384 || projectile.type == 386) && (double) projectile.ai[1] == (projectile.type == 384 ? 15.0 : 24.0))
      {
        ++projectile.timeLeft;
        return false;
      }
      int type = projectile.type;
      return base.PreAI(projectile);
    }

    public virtual void AI(Projectile projectile)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      NPC sourceNpc = projectile.GetSourceNPC();
      switch (projectile.type)
      {
        case 102:
          if (sourceNpc != null && sourceNpc.type == 44)
            projectile.damage = sourceNpc.damage / 2;
          if (!WorldSavingSystem.SwarmActive)
          {
            projectile.damage = 40;
            break;
          }
          break;
        case 258:
          NPC npc1 = FargoSoulsUtil.NPCExists(NPC.golemBoss, new int[1]
          {
            245
          });
          if (npc1 != null && !npc1.dontTakeDamage)
          {
            projectile.timeLeft = 0;
            break;
          }
          break;
        case 259:
          if (!WorldSavingSystem.MasochistModeReal)
          {
            if (EModeGlobalProjectile.NonSwarmFight(projectile, 246, 249))
            {
              if (!this.firstTickAICheckDone)
              {
                ((Entity) projectile).velocity = Utils.SafeNormalize(((Entity) projectile).velocity, Vector2.UnitY);
                projectile.timeLeft = 180 * projectile.MaxUpdates;
              }
              if (projectile.timeLeft % projectile.MaxUpdates == 0 && (double) ++projectile.localAI[1] < 90.0)
              {
                Projectile projectile1 = projectile;
                ((Entity) projectile1).velocity = Vector2.op_Multiply(((Entity) projectile1).velocity, 1.04f);
                break;
              }
              break;
            }
            break;
          }
          break;
        case 325:
        case 508:
          Projectile projectile2 = projectile;
          ((Entity) projectile2).position = Vector2.op_Addition(((Entity) projectile2).position, Vector2.op_Multiply(((Entity) projectile).velocity, 0.5f));
          break;
        case 384:
        case 386:
          this.EModeCanHurt = true;
          projectile.hide = false;
          if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.fishBoss, 370))
          {
            projectile.timeLeft = Math.Min(120, projectile.timeLeft);
            break;
          }
          break;
        case 452:
          if (EModeGlobalProjectile.NonSwarmFight(projectile, 397, 396, 400))
          {
            if ((double) projectile.ai[0] == 2.0 && this.counter > 60)
              ((Entity) projectile).velocity.Y = 9f;
            else
              ((Entity) projectile).position.Y -= ((Entity) projectile).velocity.Y / 4f;
            float num = WorldSavingSystem.MasochistModeReal ? 2f : 1f;
            if ((double) ((Entity) projectile).velocity.X > (double) num)
            {
              ((Entity) projectile).velocity.X = num;
              break;
            }
            if ((double) ((Entity) projectile).velocity.X < -(double) num)
            {
              ((Entity) projectile).velocity.X = -num;
              break;
            }
            break;
          }
          break;
        case 454:
          if (!WorldSavingSystem.SwarmActive && (!WorldSavingSystem.MasochistModeReal || !Main.getGoodWorld))
          {
            this.EModeCanHurt = projectile.alpha == 0;
            if (sourceNpc != null && sourceNpc.type == 397 && (double) projectile.ai[0] == -1.0)
            {
              if ((double) ++projectile.localAI[1] < 150.0)
              {
                Projectile projectile3 = projectile;
                ((Entity) projectile3).velocity = Vector2.op_Multiply(((Entity) projectile3).velocity, 1.018f);
              }
              if ((double) projectile.localAI[0] == 0.0 && (double) ((Vector2) ref ((Entity) projectile).velocity).Length() > 11.0)
              {
                projectile.localAI[0] = 1f;
                ((Vector2) ref ((Entity) projectile).velocity).Normalize();
                if (FargoSoulsUtil.HostCheck && !WorldSavingSystem.MasochistModeReal)
                  Projectile.NewProjectile(Entity.InheritSource((Entity) projectile), ((Entity) projectile).Center, ((Entity) projectile).velocity, ModContent.ProjectileType<PhantasmalSphereDeathray>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) projectile.identity, 0.0f);
                projectile.netUpdate = true;
                break;
              }
              break;
            }
            break;
          }
          this.EModeCanHurt = true;
          break;
        case 455:
          if (WorldSavingSystem.MasochistModeReal)
          {
            ((Entity) projectile).velocity = Utils.RotatedBy(((Entity) projectile).velocity, (double) projectile.ai[0] * 0.5, new Vector2());
            projectile.rotation = Utils.ToRotation(((Entity) projectile).velocity) - 1.57079637f;
            projectile.scale *= sourceNpc == null || sourceNpc.type != 396 ? Utils.NextFloat(Main.rand, 4f, 6f) : Utils.NextFloat(Main.rand, 6f, 9f);
            if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
            {
              FargoSoulsUtil.ScreenshakeRumble(6f);
              break;
            }
            break;
          }
          break;
        case 456:
          if ((double) projectile.ai[0] > 0.0 && !WorldSavingSystem.SwarmActive)
          {
            Vector2 vector2 = Vector2.op_Subtraction(Vector2.op_Subtraction(((Entity) Main.player[(int) projectile.ai[1]]).Center, ((Entity) projectile).Center), ((Entity) projectile).velocity);
            if (Vector2.op_Inequality(vector2, Vector2.Zero))
            {
              Projectile projectile4 = projectile;
              ((Entity) projectile4).position = Vector2.op_Addition(((Entity) projectile4).position, Vector2.op_Multiply(Vector2.Normalize(vector2), Math.Min(16f, ((Vector2) ref vector2).Length())));
              break;
            }
            break;
          }
          break;
        case 467:
          if (!WorldSavingSystem.MasochistModeReal)
          {
            if (EModeGlobalProjectile.NonSwarmFight(projectile, 439))
            {
              Projectile projectile5 = projectile;
              ((Entity) projectile5).position = Vector2.op_Subtraction(((Entity) projectile5).position, Vector2.op_Multiply(((Entity) projectile).velocity, Math.Max(0.0f, (float) (1.0 - (double) this.counter / 45.0 / (double) projectile.MaxUpdates))));
              break;
            }
            break;
          }
          break;
        case 490:
          if (!WorldSavingSystem.SwarmActive)
          {
            NPC npc2 = FargoSoulsUtil.NPCExists(projectile.ai[1], 439);
            if (npc2 != null && (double) npc2.ai[3] == -1.0 && (double) npc2.ai[0] == 5.0)
              ((Entity) projectile).Center = ((Entity) Main.player[npc2.target]).Center;
            if (!this.firstTickAICheckDone)
            {
              if (npc2 != null)
              {
                if (Main.netMode == 1)
                {
                  LunaticCultist globalNpc = npc2.GetGlobalNPC<LunaticCultist>();
                  ModPacket packet = ((ModType) this).Mod.GetPacket(256);
                  ((BinaryWriter) packet).Write((byte) 2);
                  ((BinaryWriter) packet).Write((byte) ((Entity) npc2).whoAmI);
                  ((BinaryWriter) packet).Write(globalNpc.MeleeDamageCounter);
                  ((BinaryWriter) packet).Write(globalNpc.RangedDamageCounter);
                  ((BinaryWriter) packet).Write(globalNpc.MagicDamageCounter);
                  ((BinaryWriter) packet).Write(globalNpc.MinionDamageCounter);
                  packet.Send(-1, -1);
                }
                else
                {
                  for (int index = 0; index < Main.maxProjectiles; ++index)
                  {
                    if (((Entity) Main.projectile[index]).active && (double) Main.projectile[index].ai[1] == (double) ((Entity) npc2).whoAmI && Main.projectile[index].type == ModContent.ProjectileType<CultistRitual>())
                    {
                      Main.projectile[index].Kill();
                      break;
                    }
                  }
                  Projectile.NewProjectile(((Entity) npc2).GetSource_FromThis((string) null), ((Entity) projectile).Center, Vector2.Zero, ModContent.ProjectileType<CultistRitual>(), FargoSoulsUtil.ScaledProjectileDamage(npc2.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) npc2).whoAmI, 0.0f);
                  for (int index1 = 0; index1 < 16; ++index1)
                  {
                    float num = 0.3926991f * (float) index1;
                    for (int index2 = -1; index2 <= 1; index2 += 2)
                    {
                      Vector2 vector2 = Utils.RotatedBy(new Vector2(1500f, 0.0f), (double) num, new Vector2());
                      Projectile.NewProjectile(((Entity) npc2).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) projectile).Center, vector2), Vector2.op_Multiply((float) index2, Utils.RotatedBy(Vector2.UnitY, (double) num, new Vector2())), ModContent.ProjectileType<GlowLine>(), 0, 0.0f, Main.myPlayer, 18f, (float) ((Entity) npc2).whoAmI, 0.0f);
                    }
                  }
                }
              }
              for (int index = 0; index < Main.maxProjectiles; ++index)
              {
                if (((Entity) Main.projectile[index]).active && (Main.projectile[index].type == 356 || Main.projectile[index].type == 575))
                  Main.projectile[index].Kill();
              }
            }
            bool flag = false;
            if ((double) projectile.ai[1] == -1.0)
            {
              if (this.counter == 5)
                flag = true;
            }
            else
            {
              this.counter = 0;
              if ((double) projectile.ai[0] == 299.0)
                flag = true;
            }
            if (flag)
            {
              int index3 = -1;
              for (int index4 = 0; index4 < Main.maxNPCs; ++index4)
              {
                if (((Entity) Main.npc[index4]).active && Main.npc[index4].type == 439 && (double) Main.npc[index4].ai[2] == (double) ((Entity) projectile).whoAmI)
                {
                  index3 = index4;
                  break;
                }
              }
              if (index3 != -1)
              {
                float num = (float) Main.rand.Next(4);
                LunaticCultist globalNpc = Main.npc[index3].GetGlobalNPC<LunaticCultist>();
                int[] numArray = new int[4]
                {
                  globalNpc.MagicDamageCounter,
                  globalNpc.MeleeDamageCounter,
                  globalNpc.RangedDamageCounter,
                  globalNpc.MinionDamageCounter
                };
                globalNpc.MeleeDamageCounter = 0;
                globalNpc.RangedDamageCounter = 0;
                globalNpc.MagicDamageCounter = 0;
                globalNpc.MinionDamageCounter = 0;
                Main.npc[index3].netUpdate = true;
                int index5 = 0;
                for (int index6 = 1; index6 < 4; ++index6)
                {
                  if (numArray[index5] < numArray[index6])
                    index5 = index6;
                }
                if (numArray[index5] > 0)
                  num = (float) index5;
                if ((globalNpc.EnteredPhase2 || WorldSavingSystem.MasochistModeReal) && FargoSoulsUtil.HostCheck && !((IEnumerable<Projectile>) Main.projectile).Any<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.hostile && p.type == ModContent.ProjectileType<CelestialPillar>())))
                {
                  Projectile.NewProjectile(((Entity) Main.npc[index3]).GetSource_FromThis((string) null), ((Entity) projectile).Center, Vector2.op_Multiply(Vector2.UnitY, -10f), ModContent.ProjectileType<CelestialPillar>(), Math.Max(75, FargoSoulsUtil.ScaledProjectileDamage(Main.npc[index3].damage, 4f)), 0.0f, Main.myPlayer, num, 0.0f, 0.0f);
                  break;
                }
                break;
              }
              break;
            }
            break;
          }
          break;
        case 574:
          NPC npc3 = FargoSoulsUtil.NPCExists(projectile.ai[1], 420);
          if (npc3 != null && (double) npc3.ai[0] < 45.0 && (double) projectile.ai[0] >= 175.0)
          {
            projectile.ai[0] -= 180f;
            break;
          }
          break;
        case 575:
          if (sourceNpc != null && sourceNpc.type == 439)
          {
            int closest = (int) Player.FindClosest(((Entity) projectile).Center, 0, 0);
            if (closest != -1 && (double) ((Entity) projectile).Distance(((Entity) Main.player[closest]).Center) > 240.0)
            {
              Projectile projectile6 = projectile;
              ((Entity) projectile6).position = Vector2.op_Addition(((Entity) projectile6).position, ((Entity) projectile).velocity);
              break;
            }
            break;
          }
          break;
        case 581:
          Projectile projectile7 = projectile;
          ((Entity) projectile7).position = Vector2.op_Addition(((Entity) projectile7).position, Vector2.op_Multiply(((Entity) projectile).velocity, 0.25f));
          break;
        case 629:
          if (!this.firstTickAICheckDone)
          {
            NPC npc4 = FargoSoulsUtil.NPCExists(projectile.ai[0], 507, 517, 493, 422);
            if (npc4 != null)
            {
              if ((double) ((Entity) projectile).Distance(((Entity) npc4).Center) > 4000.0)
                ((Entity) projectile).active = false;
              int closest = (int) Player.FindClosest(((Entity) projectile).Center, 0, 0);
              if (closest != -1 && (!((Entity) Main.player[closest]).active || (double) ((Entity) npc4).Distance(((Entity) Main.player[closest]).Center) >= 4000.0))
              {
                ((Entity) projectile).active = false;
                break;
              }
              break;
            }
            break;
          }
          break;
        case 651:
          if (projectile.owner == Main.myPlayer && Main.player[projectile.owner].FargoSouls().LihzahrdCurse)
          {
            projectile.Kill();
            break;
          }
          break;
        case 654:
          if (!WorldSavingSystem.MasochistModeReal && sourceNpc != null && sourceNpc.type == 245 && this.counter > 45)
          {
            projectile.Kill();
            break;
          }
          break;
        case 655:
          if (projectile.timeLeft > 30 && ((double) ((Entity) projectile).velocity.X != 0.0 || (double) ((Entity) projectile).velocity.Y == 0.0))
          {
            projectile.timeLeft = 30;
            break;
          }
          break;
        case 657:
          if (Main.hardMode && projectile.timeLeft == 1199 && NPC.CountNPCS(542) < 10 && FargoSoulsUtil.HostCheck && (sourceNpc == null || sourceNpc.type != ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>() && sourceNpc.type != ModContent.NPCType<SpiritChampion>()))
            FargoSoulsUtil.NewNPCEasy(Entity.InheritSource((Entity) projectile), ((Entity) projectile).Center, 542, velocity: new Vector2(Utils.NextFloat(Main.rand, -10f, 10f), Utils.NextFloat(Main.rand, -20f, -10f)));
          if (sourceNpc != null && sourceNpc.type == ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>() && (double) sourceNpc.ai[0] != 5.0)
          {
            projectile.ai[0] += 2f;
            break;
          }
          break;
        case 686:
          if (!this.firstTickAICheckDone)
          {
            if (EModeGlobalProjectile.NonSwarmFight(projectile, 551))
            {
              bool inPhase2 = sourceNpc.GetGlobalNPC<Betsy>().InPhase2;
              int num1 = inPhase2 ? 2 : 1;
              for (int index = 0; index < num1; ++index)
              {
                Vector2 vector2 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 8f, 12f), Vector2.op_UnaryNegation(Utils.RotatedByRandom(Vector2.UnitY, Math.PI / 2.0)));
                float num2 = inPhase2 ? (float) (60 + Main.rand.Next(60)) : (float) (90 + Main.rand.Next(30));
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(Entity.InheritSource((Entity) projectile), ((Entity) projectile).Center, vector2, ModContent.ProjectileType<BetsyPhoenix>(), projectile.damage, 0.0f, Main.myPlayer, (float) Player.FindClosest(((Entity) projectile).Center, 0, 0), num2, 0.0f);
              }
              break;
            }
            break;
          }
          break;
        case 687:
          if (EModeGlobalProjectile.NonSwarmFight(projectile, 551))
          {
            bool inPhase2 = sourceNpc.GetGlobalNPC<Betsy>().InPhase2;
            if (inPhase2 && !this.firstTickAICheckDone && WorldSavingSystem.MasochistModeReal && FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(Entity.InheritSource((Entity) projectile), Vector2.op_Addition(((Entity) projectile).Center, Vector2.op_Multiply(100f, Vector2.Normalize(((Entity) sourceNpc).velocity))), Vector2.Zero, ModContent.ProjectileType<EarthChainBlast>(), projectile.damage, 0.0f, Main.myPlayer, Utils.ToRotation(((Entity) sourceNpc).velocity), 7f, 0.0f);
            if (this.counter > (inPhase2 ? 2 : 4))
            {
              this.counter = 0;
              SoundEngine.PlaySound(ref SoundID.Item34, new Vector2?(((Entity) projectile).Center), (SoundUpdateCallback) null);
              Vector2 vector2_1 = Utils.RotatedBy(((Entity) projectile).velocity, (Main.rand.NextDouble() - 0.5) * Math.PI / 10.0, new Vector2());
              ((Vector2) ref vector2_1).Normalize();
              Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, Utils.NextFloat(Main.rand, 8f, 12f));
              int num = ModContent.ProjectileType<BetsyHomingFireball>();
              if (!inPhase2 || Utils.NextBool(Main.rand))
              {
                num = ModContent.ProjectileType<WillFireball>();
                vector2_2 = Vector2.op_Multiply(vector2_2, 2f);
                if (inPhase2)
                  vector2_2 = Vector2.op_Multiply(vector2_2, 1.5f);
              }
              if (FargoSoulsUtil.HostCheck)
              {
                Projectile.NewProjectile(Entity.InheritSource((Entity) projectile), ((Entity) projectile).Center, vector2_2, num, projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                break;
              }
              break;
            }
            break;
          }
          break;
        case 719:
          ((Entity) projectile).velocity.Y -= 0.1f;
          break;
        case 811:
        case 813:
        case 814:
          if (!Collision.SolidTiles(((Entity) projectile).Center, 0, 0))
          {
            Lighting.AddLight(((Entity) projectile).Center, 19);
            if (this.counter > 180)
            {
              projectile.tileCollide = true;
              break;
            }
            break;
          }
          break;
        case 872:
          if (!EModeGlobalProjectile.NonSwarmFight(projectile, 636))
          {
            this.EModeCanHurt = true;
            this.altBehaviour = false;
            break;
          }
          if (!WorldSavingSystem.SwarmActive)
          {
            if ((double) Math.Abs(MathHelper.WrapAngle(Utils.ToRotation(((Entity) projectile).velocity) - projectile.localAI[1])) > 2.8274333477020264)
              this.EModeCanHurt = true;
            projectile.extraUpdates = this.EModeCanHurt ? 1 : 3;
            if ((double) projectile.localAI[0] == 1.0)
              ((Entity) projectile).velocity = Utils.RotatedBy(((Entity) projectile).velocity, -(double) projectile.ai[0] * 2.0, new Vector2());
          }
          if (this.altBehaviour && this.EModeCanHurt)
          {
            ((Entity) projectile).velocity = Utils.RotatedBy(((Entity) projectile).velocity, -(double) projectile.ai[0] * 0.5, new Vector2());
            break;
          }
          break;
        case 873:
          if (!this.EModeCanHurt)
          {
            this.EModeCanHurt = WorldSavingSystem.SwarmActive || projectile.timeLeft < 100;
            break;
          }
          break;
        case 876:
          if (projectile.damage > projectile.originalDamage)
          {
            projectile.damage = projectile.originalDamage;
            break;
          }
          break;
        case 919:
          this.EModeCanHurt = (double) projectile.localAI[0] > 60.0;
          if (this.altBehaviour)
          {
            if (!this.EModeCanHurt)
            {
              this.counter = 0;
              ++projectile.timeLeft;
              projectile.localAI[0] -= 0.33f;
            }
            Projectile projectile8 = projectile;
            ((Entity) projectile8).position = Vector2.op_Subtraction(((Entity) projectile8).position, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) projectile).velocity, 0.33f), Utils.Clamp<float>((float) Math.Sqrt(1.0 - (double) this.counter / 60.0), 0.0f, 1f)));
            break;
          }
          if (EModeGlobalProjectile.NonSwarmFight(projectile, 636))
          {
            if ((double) sourceNpc.ai[0] == 7.0 && (double) sourceNpc.ai[1] < (double) byte.MaxValue)
            {
              if (sourceNpc.HasValidTarget)
              {
                float num = WorldSavingSystem.MasochistModeReal ? 0.6f : 0.4f;
                Projectile projectile9 = projectile;
                ((Entity) projectile9).position = Vector2.op_Addition(((Entity) projectile9).position, Vector2.op_Multiply(num, Vector2.op_Subtraction(((Entity) Main.player[sourceNpc.target]).position, ((Entity) Main.player[sourceNpc.target]).oldPosition)));
                break;
              }
              break;
            }
            if ((double) sourceNpc.ai[0] == 6.0 && (double) sourceNpc.ai[1] > 60.0)
            {
              Projectile projectile10 = projectile;
              ((Entity) projectile10).position = Vector2.op_Addition(((Entity) projectile10).position, Vector2.op_Subtraction(((Entity) sourceNpc).position, ((Entity) sourceNpc).oldPosition));
              break;
            }
            break;
          }
          break;
        case 921:
          if (!WorldSavingSystem.MasochistModeReal && !WorldSavingSystem.SwarmActive)
          {
            float num = Math.Max(0.0f, (float) (1.0 - (double) this.counter / 60.0 / (double) projectile.MaxUpdates));
            Projectile projectile11 = projectile;
            ((Entity) projectile11).position = Vector2.op_Subtraction(((Entity) projectile11).position, Vector2.op_Multiply(((Entity) projectile).velocity, num));
            ((Entity) projectile).velocity.Y -= 0.15f * num;
            break;
          }
          break;
        case 923:
          if (!WorldSavingSystem.SwarmActive)
          {
            NPC npc5 = FargoSoulsUtil.NPCExists(projectile.ai[1], 636);
            if (npc5 != null)
            {
              if ((double) npc5.ai[0] == 8.0 || (double) npc5.ai[0] == 9.0)
              {
                projectile.rotation = projectile.ai[0];
                if (this.counter < 60)
                  this.counter += 9;
                if ((double) projectile.localAI[0] < 60.0)
                  projectile.localAI[0] += 9f;
              }
              if ((double) npc5.ai[0] == 1.0 || (double) npc5.ai[0] == 10.0)
              {
                this.EModeCanHurt = false;
                this.counter = 0;
                projectile.timeLeft = 0;
              }
              if ((double) npc5.ai[0] == 6.0 && npc5.GetGlobalNPC<EmpressofLight>().AttackCounter % 2 == 0)
              {
                projectile.scale *= Utils.Clamp<float>(npc5.ai[1] / 80f, 0.0f, 2.5f);
                break;
              }
              if (this.counter >= 60 && (double) projectile.scale > 0.5 && this.counter % 10 == 0)
              {
                float num3 = MathHelper.ToRadians(90f) * MathHelper.Lerp(0.0f, 1f, (float) ((double) this.counter % 50.0 / 50.0));
                for (int index = -1; index <= 1; index += 2)
                {
                  if (((double) Math.Abs(num3) >= 1.0 / 1000.0 || index >= 0) && FargoSoulsUtil.HostCheck)
                  {
                    float num4 = 800f;
                    Projectile.NewProjectile(Entity.InheritSource((Entity) projectile), Vector2.op_Addition(((Entity) projectile).Center, Vector2.op_Multiply(Utils.ToRotationVector2(projectile.rotation), num4)), Vector2.Zero, 919, projectile.damage, projectile.knockBack, projectile.owner, projectile.rotation + num3 * (float) index, projectile.ai[0], 0.0f);
                  }
                }
                break;
              }
              break;
            }
            break;
          }
          break;
        case 926:
          if (!WorldSavingSystem.SwarmActive)
          {
            if (!WorldSavingSystem.MasochistModeReal)
            {
              float num = Math.Max(0.0f, (float) (1.0 - (double) this.counter / 60.0 / (double) projectile.MaxUpdates));
              Projectile projectile12 = projectile;
              ((Entity) projectile12).position = Vector2.op_Subtraction(((Entity) projectile12).position, Vector2.op_Multiply(((Entity) projectile).velocity, num));
              ((Entity) projectile).velocity.Y -= 0.15f * num;
            }
            if (!this.firstTickAICheckDone && sourceNpc != null && sourceNpc.type == 657 && sourceNpc.life > sourceNpc.lifeMax / 2)
              ((Entity) projectile).velocity.Y -= 6f;
            if ((double) ((Entity) projectile).velocity.Y > 0.0 && (double) projectile.localAI[0] == 0.0)
            {
              projectile.localAI[0] = 1f;
              for (int index7 = -1; index7 <= 1; index7 += 2)
              {
                if (Math.Sign(((Entity) projectile).velocity.X) != -index7)
                {
                  Vector2 vector2_3 = Utils.RotatedBy(Vector2.UnitX, (double) MathHelper.ToRadians((float) (10 * index7)), new Vector2());
                  for (int index8 = 0; index8 < 12; ++index8)
                  {
                    Vector2 vector2_4 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.NextFloat(Main.rand, 6f, 18f) * (float) index7, Utils.RotatedBy(vector2_3, 0.065449848771095276 * ((double) index8 + 0.5) * (double) -index7, new Vector2())), WorldSavingSystem.MasochistModeReal ? 2f : 1.5f);
                    if (FargoSoulsUtil.HostCheck)
                      Projectile.NewProjectile(Entity.InheritSource((Entity) projectile), ((Entity) projectile).Center, vector2_4, 920, projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  }
                }
              }
              break;
            }
            break;
          }
          break;
        case 961:
          if (!WorldSavingSystem.SwarmActive && (double) this.counter == 2.0 && projectile.hostile && (double) projectile.ai[1] > 1.2999999523162842)
          {
            float num = 1.3f;
            if ((double) projectile.ai[1] > 1.3500000238418579)
              num = 1.35f;
            for (int index = -1; index <= 1; ++index)
            {
              Vector2 vector2 = Vector2.Lerp(((Entity) projectile).velocity, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, ((Vector2) ref ((Entity) projectile).velocity).Length()), (float) Math.Sign(((Entity) projectile).velocity.X)), 0.75f);
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(Entity.InheritSource((Entity) projectile), Vector2.op_Addition(((Entity) projectile).Center, Vector2.op_Multiply(200f, Vector2.Normalize(((Entity) projectile).velocity))), Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(30f) * (double) index, new Vector2()), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0.0f, num, 0.0f);
            }
            break;
          }
          break;
        case 965:
          if (!WorldSavingSystem.SwarmActive)
          {
            if (Main.player[projectile.owner].ownedProjectileCounts[projectile.type] >= 4)
            {
              projectile.extraUpdates = 1;
              Projectile projectile13 = projectile;
              ((Entity) projectile13).position = Vector2.op_Addition(((Entity) projectile13).position, Vector2.op_Multiply(((Entity) projectile).velocity, 0.5f));
              this.EModeCanHurt = true;
              this.counter = -600;
              break;
            }
            if (!WorldSavingSystem.MasochistModeReal || !Main.getGoodWorld)
            {
              this.EModeCanHurt = false;
              Projectile projectile14 = projectile;
              ((Entity) projectile14).position = Vector2.op_Subtraction(((Entity) projectile14).position, ((Entity) projectile).velocity);
              --projectile.ai[0];
              projectile.alpha = (int) byte.MaxValue;
              if (this.counter > 30 && Main.player[projectile.owner].ownedProjectileCounts[projectile.type] <= 1)
              {
                projectile.timeLeft = 0;
                break;
              }
              break;
            }
            break;
          }
          break;
      }
      this.firstTickAICheckDone = true;
    }

    public virtual void PostAI(Projectile projectile)
    {
      switch (projectile.type)
      {
        case 872:
        case 873:
          if (!projectile.hostile)
            break;
          if (!this.EModeCanHurt)
          {
            if (this.FadeTimer < 20)
              ++this.FadeTimer;
          }
          else if (this.FadeTimer > 0)
            --this.FadeTimer;
          float num = (float) (1.0 - 0.5 * (double) this.FadeTimer / 20.0);
          projectile.Opacity = num;
          break;
      }
    }

    public virtual void ModifyHitNPC(
      Projectile projectile,
      NPC target,
      ref NPC.HitModifiers modifiers)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      switch (projectile.type)
      {
        case 158:
          ref StatModifier local1 = ref modifiers.FinalDamage;
          local1 = StatModifier.op_Multiply(local1, 1.6f);
          break;
        case 159:
          ref StatModifier local2 = ref modifiers.FinalDamage;
          local2 = StatModifier.op_Multiply(local2, 0.9f);
          break;
        case 160:
          ref StatModifier local3 = ref modifiers.FinalDamage;
          local3 = StatModifier.op_Multiply(local3, 0.47f);
          break;
        case 161:
          ref StatModifier local4 = ref modifiers.FinalDamage;
          local4 = StatModifier.op_Multiply(local4, 0.275f);
          break;
        case 726:
          if (Main.hardMode)
            break;
          ref StatModifier local5 = ref modifiers.FinalDamage;
          local5 = StatModifier.op_Multiply(local5, 0.33f);
          break;
      }
    }

    public virtual bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
    {
      if (!WorldSavingSystem.EternityMode)
        return base.OnTileCollide(projectile, oldVelocity);
      switch (projectile.type)
      {
        case 31:
          if ((double) projectile.ai[0] == 2.0)
          {
            int index = Dust.NewDust(((Entity) projectile).position, ((Entity) projectile).width, ((Entity) projectile).height, 32, 0.0f, ((Entity) projectile).velocity.Y / 2f, 0, new Color(), 1f);
            Main.dust[index].velocity.X *= 0.4f;
            ((Entity) projectile).active = false;
            break;
          }
          break;
        case 109:
          ((Entity) projectile).active = false;
          break;
        case 277:
          projectile.timeLeft = 0;
          NPC npc = FargoSoulsUtil.NPCExists(NPC.plantBoss, new int[1]
          {
            262
          });
          if (npc != null && FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2 = Vector2.op_Multiply(8f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) projectile, ((Entity) npc).Center));
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Subtraction(((Entity) projectile).Center, ((Entity) projectile).oldVelocity), vector2, ModContent.ProjectileType<DicerPlantera>(), projectile.damage, projectile.knockBack, projectile.owner, 0.0f, 0.0f, 0.0f);
            break;
          }
          break;
        case 921:
        case 926:
          if (WorldSavingSystem.MasochistModeReal && Main.getGoodWorld)
          {
            if (FargoSoulsUtil.HostCheck)
            {
              for (int index = 0; index < 8; ++index)
                Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitY, Math.PI / 4.0 * (double) index, new Vector2()), 2f), 146, 0, 0.0f, Main.myPlayer, 8f, 0.0f, 0.0f);
              break;
            }
            break;
          }
          projectile.timeLeft = 0;
          break;
      }
      return base.OnTileCollide(projectile, oldVelocity);
    }

    public virtual void ModifyHitPlayer(
      Projectile projectile,
      Player target,
      ref Player.HurtModifiers modifiers)
    {
      if (!WorldSavingSystem.EternityMode || !NPC.downedGolemBoss || projectile.type != 580)
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 2f);
    }

    public virtual void OnHitNPC(
      Projectile projectile,
      NPC target,
      NPC.HitInfo hit,
      int damageDone)
    {
      base.OnHitNPC(projectile, target, hit, damageDone);
      if (!WorldSavingSystem.EternityMode)
        return;
      Player player = Main.player[projectile.owner];
      switch (projectile.type)
      {
        case 97:
          if ((double) projectile.ai[2] >= 2.0)
            break;
          Projectile projectile1 = FargoSoulsUtil.NewProjectileDirectSafe(((Entity) player).GetSource_OnHit((Entity) target, (string) null), Vector2.op_Addition(Vector2.op_Addition(((Entity) target).position, Vector2.op_Multiply(Vector2.UnitX, (float) Main.rand.Next(((Entity) target).width))), Vector2.op_Multiply(Vector2.UnitY, (float) Main.rand.Next(((Entity) target).height))), Vector2.Zero, ModContent.ProjectileType<CobaltExplosion>(), (int) ((double) ((NPC.HitInfo) ref hit).Damage * 0.40000000596046448), 0.0f, Main.myPlayer);
          if (projectile1 != null)
            projectile1.FargoSouls().CanSplit = false;
          ++projectile.ai[2];
          break;
        case 118:
          target.AddBuff(44, 180, false);
          break;
        case 212:
          if (target.type == 488 || target.friendly)
            break;
          player.AddBuff(58, 300, true, false);
          break;
      }
    }

    public virtual void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      NPC sourceNpc = projectile.GetSourceNPC();
      MoonLordBodyPart moonLordBodyPart;
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.moonBoss, 398) && sourceNpc != null && sourceNpc.TryGetGlobalNPC<MoonLordBodyPart>(ref moonLordBodyPart))
        target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 180, true, false);
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.cultBoss, 439) && (double) ((Entity) target).Distance(((Entity) Main.npc[EModeGlobalNPC.cultBoss]).Center) < 2400.0)
        target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 180, true, false);
      switch (projectile.type)
      {
        case 5:
          if (sourceNpc != null && sourceNpc.type == 475)
          {
            target.AddBuff(ModContent.BuffType<SmiteBuff>(), 600, true, false);
            goto case 180;
          }
          else
            goto case 180;
        case 22:
        case 27:
          target.AddBuff(103, 600, true, false);
          break;
        case 31:
          if ((double) ((Entity) projectile).velocity.X == 0.0)
            break;
          target.FargoSouls().AddBuffNoStack(ModContent.BuffType<StunnedBuff>(), 120);
          break;
        case 36:
          if (sourceNpc != null && sourceNpc.type == 292)
          {
            target.AddBuff(24, 360, true, false);
            target.AddBuff(67, 180, true, false);
            goto case 180;
          }
          else
            goto case 180;
        case 38:
          target.AddBuff(ModContent.BuffType<ClippedWingsBuff>(), 300, true, false);
          break;
        case 44:
          target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300, true, false);
          break;
        case 55:
        case 719:
          target.AddBuff(ModContent.BuffType<SwarmingBuff>(), 300, true, false);
          break;
        case 83:
          if (sourceNpc == null || sourceNpc.type != 113 && sourceNpc.type != 114)
            break;
          target.AddBuff(24, 300, true, false);
          if (!WorldSavingSystem.MasochistModeReal)
            break;
          target.AddBuff(67, 60, true, false);
          break;
        case 99:
          target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
          break;
        case 100:
          if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.retiBoss, 125))
            target.AddBuff(69, 600, true, false);
          if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.destroyBoss, 134))
            break;
          target.AddBuff(144, 60, true, false);
          break;
        case 102:
          target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
          break;
        case 109:
          target.FargoSouls().AddBuffNoStack(47, 45);
          break;
        case 110:
          target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 600, true, false);
          break;
        case 115:
          target.AddBuff(22, 300, true, false);
          target.AddBuff(80, 300, true, false);
          target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 600, true, false);
          break;
        case 129:
          target.AddBuff(ModContent.BuffType<HexedBuff>(), 240, true, false);
          if (sourceNpc == null || sourceNpc.type != 172)
            break;
          target.AddBuff(ModContent.BuffType<FlamesoftheUniverseBuff>(), 60, true, false);
          target.AddBuff(68, 240, true, false);
          break;
        case 174:
          target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 300, true, false);
          break;
        case 176:
          target.AddBuff(ModContent.BuffType<InfestedBuff>(), 300, true, false);
          break;
        case 180:
          if (sourceNpc == null || sourceNpc.type != 492 && sourceNpc.type != 214 && sourceNpc.type != 215)
            break;
          target.AddBuff(ModContent.BuffType<MidasBuff>(), 600, true, false);
          break;
        case 184:
        case 185:
        case 186:
          target.AddBuff(ModContent.BuffType<IvyVenomBuff>(), 360, true, false);
          break;
        case 187:
        case 188:
        case 258:
        case 259:
        case 654:
          target.AddBuff(24, 300, true, false);
          if (sourceNpc == null)
            break;
          if (sourceNpc.type == 245)
          {
            target.AddBuff(36, 600, true, false);
            target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
            target.AddBuff(195, 600, true, false);
            Tile tileSafely = Framing.GetTileSafely(((Entity) sourceNpc).Center);
            if (((Tile) ref tileSafely).WallType != (ushort) 87)
              target.AddBuff(67, 120, true, false);
          }
          if (sourceNpc.type == ModContent.NPCType<EarthChampion>())
            target.AddBuff(67, 300, true, false);
          if (sourceNpc.type != ModContent.NPCType<TerraChampion>())
            break;
          target.AddBuff(24, 600, true, false);
          target.AddBuff(ModContent.BuffType<LivingWastelandBuff>(), 600, true, false);
          break;
        case 240:
          target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
          target.AddBuff(ModContent.BuffType<MidasBuff>(), 900, true, false);
          break;
        case 263:
          target.AddBuff(44, 240, true, false);
          target.AddBuff(46, 120, true, false);
          break;
        case 270:
          target.FargoSouls().AddBuffNoStack(23, 30);
          if (sourceNpc == null || sourceNpc.type != 68)
            break;
          target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 600, true, false);
          break;
        case 274:
          target.AddBuff(ModContent.BuffType<LivingWastelandBuff>(), 600, true, false);
          target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 300, true, false);
          break;
        case 275:
        case 277:
          target.AddBuff(ModContent.BuffType<IvyVenomBuff>(), 240, true, false);
          break;
        case 276:
          target.AddBuff(20, 300, true, false);
          goto case 275;
        case 290:
          if (sourceNpc != null && sourceNpc.type == ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>())
            break;
          target.AddBuff(ModContent.BuffType<RottingBuff>(), 1800, true, false);
          target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300, true, false);
          break;
        case 291:
        case 292:
          if (sourceNpc != null && sourceNpc.type == ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>() || !Utils.NextBool(Main.rand, 5))
            break;
          target.AddBuff(ModContent.BuffType<FusedBuff>(), 1800, true, false);
          break;
        case 293:
          target.AddBuff(ModContent.BuffType<HexedBuff>(), 240, true, false);
          break;
        case 300:
          target.FargoSouls().AddBuffNoStack(ModContent.BuffType<StunnedBuff>(), 60);
          break;
        case 303:
          target.AddBuff(160, 120, true, false);
          target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300, true, false);
          break;
        case 325:
        case 326:
        case 327:
        case 328:
          target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 180, true, false);
          break;
        case 329:
          target.AddBuff(24, 900, true, false);
          target.AddBuff(ModContent.BuffType<LivingWastelandBuff>(), 900, true, false);
          break;
        case 348:
        case 349:
          target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 600, true, false);
          break;
        case 384:
        case 386:
          target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
          target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 1200, true, false);
          target.FargoSouls().MaxLifeReduction += FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.fishBossEX, 370) ? 100 : 25;
          break;
        case 433:
        case 447:
          target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 360, true, false);
          break;
        case 435:
        case 437:
          target.AddBuff(ModContent.BuffType<LightningRodBuff>(), 300, true, false);
          break;
        case 436:
          target.AddBuff(ModContent.BuffType<FlippedBuff>(), 60, true, false);
          target.AddBuff(ModContent.BuffType<UnstableBuff>(), 60, true, false);
          break;
        case 438:
          target.AddBuff(164, 180, true, false);
          break;
        case 448:
          target.AddBuff(ModContent.BuffType<ClippedWingsBuff>(), 300, true, false);
          target.AddBuff(ModContent.BuffType<CrippledBuff>(), 300, true, false);
          break;
        case 449:
          target.AddBuff(144, 300, true, false);
          break;
        case 452:
        case 454:
        case 462:
          target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, true, false);
          if (!WorldSavingSystem.EternityMode || sourceNpc == null || sourceNpc.type != ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>())
            break;
          target.FargoSouls().MaxLifeReduction += 100;
          target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
          target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
          break;
        case 455:
          target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, true, false);
          break;
        case 464:
          target.FargoSouls().AddBuffNoStack(47, 45);
          target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 1200, true, false);
          break;
        case 465:
        case 466:
          target.AddBuff(144, 300, true, false);
          break;
        case 467:
          target.AddBuff(24, 300, true, false);
          target.AddBuff(67, 120, true, false);
          if (sourceNpc == null || sourceNpc.type != 551)
            break;
          target.AddBuff(195, 300, true, false);
          target.AddBuff(196, 300, true, false);
          target.AddBuff(67, 300, true, false);
          break;
        case 468:
          target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 600, true, false);
          break;
        case 496:
          target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300, true, false);
          break;
        case 501:
          switch (Main.rand.Next(7))
          {
            case 0:
              target.AddBuff(70, 300, true, false);
              break;
            case 1:
              target.AddBuff(31, 300, true, false);
              break;
            case 2:
              target.AddBuff(39, 300, true, false);
              break;
            case 3:
              target.AddBuff(197, 300, true, false);
              break;
            case 4:
              target.AddBuff(ModContent.BuffType<LivingWastelandBuff>(), 600, true, false);
              break;
            case 5:
              target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
              break;
            case 6:
              target.AddBuff(ModContent.BuffType<PurifiedBuff>(), 600, true, false);
              break;
          }
          target.AddBuff(120, 1200, true, false);
          break;
        case 508:
          target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
          target.FargoSouls().AddBuffNoStack(ModContent.BuffType<StunnedBuff>(), 60);
          break;
        case 537:
        case 538:
        case 539:
          target.AddBuff(163, 20, true, false);
          target.AddBuff(80, 300, true, false);
          break;
        case 573:
        case 575:
        case 576:
          target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 300, true, false);
          target.AddBuff(ModContent.BuffType<LethargicBuff>(), 300, true, false);
          break;
        case 577:
        case 581:
          target.AddBuff(ModContent.BuffType<LightningRodBuff>(), 600, true, false);
          target.AddBuff(ModContent.BuffType<ClippedWingsBuff>(), 300, true, false);
          break;
        case 580:
          target.AddBuff(144, 300, true, false);
          break;
        case 593:
          target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 300, true, false);
          target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300, true, false);
          break;
        case 596:
          if (target.ZoneCorrupt)
          {
            target.AddBuff(39, 240, true, false);
            break;
          }
          if (!target.ZoneCrimson)
            break;
          target.AddBuff(69, 240, true, false);
          break;
        case 605:
          target.AddBuff(137, 120, true, false);
          break;
        case 638:
          if (sourceNpc == null || sourceNpc.type != 425)
            break;
          target.AddBuff(ModContent.BuffType<LightningRodBuff>(), 300, true, false);
          target.AddBuff(ModContent.BuffType<ClippedWingsBuff>(), 120, true, false);
          break;
        case 657:
          if (target.HasBuff(160))
            break;
          target.AddBuff(160, 120, true, false);
          break;
        case 670:
          target.AddBuff(160, 120, true, false);
          break;
        case 671:
          target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 600, true, false);
          break;
        case 675:
          target.AddBuff(ModContent.BuffType<HexedBuff>(), 240, true, false);
          break;
        case 683:
          target.AddBuff(36, 300, true, false);
          break;
        case 686:
        case 687:
          target.AddBuff(195, 300, true, false);
          target.AddBuff(196, 300, true, false);
          target.AddBuff(67, 300, true, false);
          break;
        case 696:
          if (sourceNpc == null || sourceNpc.type != ModContent.NPCType<TimberChampion>())
            break;
          target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300, true, false);
          break;
        case 727:
        case 763:
          target.AddBuff(20, 120, true, false);
          break;
        case 756:
        case 811:
        case 813:
        case 814:
          target.AddBuff(ModContent.BuffType<AnticoagulationBuff>(), 600, true, false);
          break;
        case 871:
        case 872:
        case 873:
        case 923:
        case 924:
          target.AddBuff(ModContent.BuffType<PurifiedBuff>(), 300, true, false);
          target.AddBuff(ModContent.BuffType<SmiteBuff>(), 1800, true, false);
          break;
        case 919:
          if (WorldSavingSystem.EternityMode && sourceNpc != null && sourceNpc.type == ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>())
          {
            target.FargoSouls().MaxLifeReduction += 100;
            target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
            target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
            goto case 871;
          }
          else
            goto case 871;
        case 920:
        case 921:
        case 922:
        case 926:
          target.AddBuff(137, 180, true, false);
          break;
        case 949:
          target.AddBuff(24, 60, true, false);
          break;
        case 961:
        case 962:
        case 965:
          target.AddBuff(44, 90, true, false);
          target.AddBuff(36, 90, true, false);
          if (WorldSavingSystem.MasochistModeReal)
            target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 900, true, false);
          target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 1200, true, false);
          break;
      }
    }

    public virtual bool PreKill(Projectile projectile, int timeLeft)
    {
      if (!WorldSavingSystem.EternityMode)
        return base.PreKill(projectile, timeLeft);
      if (projectile.owner != Main.myPlayer || !this.HasKillCooldown)
        return base.PreKill(projectile, timeLeft);
      if (Main.player[projectile.owner].Eternity().MasomodeCrystalTimer > 60)
        return false;
      Main.player[projectile.owner].Eternity().MasomodeCrystalTimer += 12;
      return true;
    }

    public virtual void OnKill(Projectile projectile, int timeLeft)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      switch (projectile.type)
      {
        case 813:
        case 814:
          if (!FargoSoulsUtil.HostCheck)
            break;
          Projectile.NewProjectile(Entity.InheritSource((Entity) projectile), ((Entity) projectile).Center, Vector2.Zero, ModContent.ProjectileType<BloodFountain>(), projectile.damage, 0.0f, Main.myPlayer, (float) Main.rand.Next(16, 48), 0.0f, 0.0f);
          break;
      }
    }

    public virtual Color? GetAlpha(Projectile projectile, Color lightColor)
    {
      if (!WorldSavingSystem.EternityMode)
        return base.GetAlpha(projectile, lightColor);
      return (projectile.type == 276 || projectile.type == 275) && this.counter % 8 < 4 ? new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), projectile.Opacity)) : base.GetAlpha(projectile, lightColor);
    }
  }
}
