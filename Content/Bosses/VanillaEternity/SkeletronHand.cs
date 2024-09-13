// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.SkeletronHand
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Content.Buffs.Masomode;
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
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class SkeletronHand : EModeNPCBehaviour
  {
    public int AttackTimer;
    public int AI_Timer;
    public Vector2 storedVel;
    public int collisionCooldown = 60;
    public bool secondSet;
    public bool HitPlayer = true;
    public const int GuardianTime = 65;
    public const int GuardianDelay = 150;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(36);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.damage = (int) ((double) npc.damage * 1.7999999523162842);
      npc.lifeMax = (int) ((double) npc.lifeMax * 1.3999999761581421);
    }

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.AttackTimer);
      binaryWriter.Write7BitEncodedInt(this.AI_Timer);
      binaryWriter.Write7BitEncodedInt(this.collisionCooldown);
      binaryWriter.Write(npc.localAI[0]);
      binaryWriter.Write(npc.localAI[1]);
      binaryWriter.Write(npc.localAI[2]);
      binaryWriter.Write(npc.localAI[3]);
      Utils.WriteVector2(binaryWriter, this.storedVel);
      binaryWriter.Write(this.secondSet);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.AttackTimer = binaryReader.Read7BitEncodedInt();
      this.AI_Timer = binaryReader.Read7BitEncodedInt();
      this.collisionCooldown = binaryReader.Read7BitEncodedInt();
      npc.localAI[0] = binaryReader.ReadSingle();
      npc.localAI[1] = binaryReader.ReadSingle();
      npc.localAI[2] = binaryReader.ReadSingle();
      npc.localAI[3] = binaryReader.ReadSingle();
      this.storedVel = Utils.ReadVector2(binaryReader);
      this.secondSet = binaryReader.ReadBoolean();
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      if (WorldSavingSystem.SwarmActive)
        return flag;
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.ai[1], 35);
      if (npc1 == null)
        return flag;
      if (npc.timeLeft < 60)
        npc.timeLeft = 60;
      if ((double) ((Entity) npc).Distance(((Entity) npc1).Center) > 1600.0)
      {
        ((Entity) npc).Center = ((Entity) npc1).Center;
        npc.ai[2] = 0.0f;
        npc.ai[3] = 0.0f;
        npc.localAI[0] = 0.0f;
        npc.localAI[1] = 0.0f;
        npc.localAI[2] = 0.0f;
        npc.localAI[3] = 0.0f;
        npc.netUpdate = true;
      }
      if ((double) npc1.ai[1] == 1.0 || (double) npc1.ai[1] == 2.0)
      {
        --this.AttackTimer;
        if (this.AttackTimer >= 0 && (double) npc1.life >= (double) npc1.lifeMax * 0.75 && this.AttackTimer < 65)
        {
          if (!npc.HasValidTarget)
            ++this.AttackTimer;
          else if (this.AttackTimer % 7 == 0 && FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center);
            if (this.AttackTimer < 48)
              vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc1, ((Entity) npc).Center);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<SkeletronGuardian2>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      else if (this.AttackTimer != 215)
      {
        this.AttackTimer = 215;
        if (FargoSoulsUtil.HostCheck && npc.HasPlayerTarget && npc1.life >= npc1.lifeMax / 2)
        {
          float num = 0.4f;
          Vector2 velocity = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Top, ((Entity) npc).Center), Utils.NextVector2Circular(Main.rand, 80f, 80f));
          velocity.X /= 60f;
          velocity.Y = (float) ((double) velocity.Y / 60.0 - 0.5 * (double) num * 60.0);
          SoundEngine.PlaySound(ref SoundID.NPCDeath13, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, Utils.Next<int>(Main.rand, new int[4]
          {
            449,
            450,
            451,
            452
          }), velocity: velocity);
        }
      }
      return flag;
    }

    public override void SafePostAI(NPC npc)
    {
      if (WorldSavingSystem.SwarmActive || Main.dayTime)
        return;
      NPC head = FargoSoulsUtil.NPCExists(npc.ai[1], 35);
      if (head == null)
        return;
      Player player = Main.player[head.target];
      if (player == null || !((Entity) player).active || player.dead)
        return;
      npc.ai[3] = 0.0f;
      ref float local = ref npc.ai[0];
      int restDistance = ((Entity) head).width / 2 + ((Entity) npc).width / 2 + 30;
      int rotwaveTime = 300;
      float num = (float) (Math.Sin((double) local * ((double) this.AI_Timer / (double) rotwaveTime) * 3.1415927410125732) * 3.1415927410125732 / 3.0);
      if (this.secondSet)
        num += local * 1.57079637f;
      Vector2 restPos = Vector2.op_Subtraction(((Entity) head).Center, Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) restDistance + 50f * Math.Abs(num)), local), (double) num, new Vector2()));
      if (SkeletronHand.HeadSpinning(npc))
        SpinAttack();
      else
        NonSpinAI();
      if (this.collisionCooldown > 0)
        --this.collisionCooldown;
      ++this.AI_Timer;

      void SpinAttack()
      {
        ref float local1 = ref npc.localAI[0];
        ref float local2 = ref npc.localAI[2];
        ref float local3 = ref npc.localAI[3];
        ref float local4 = ref npc.ai[0];
        if (this.AttackTimer == 95)
        {
          local1 = Utils.ToRotation(Vector2.op_UnaryNegation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) head, ((Entity) player).Center)));
          local3 = -local4;
          if (this.secondSet)
            local1 += (float) ((double) local3 * 3.1415927410125732 * 18.0 / 16.0);
          else
            local1 += (float) ((double) local3 * 3.1415927410125732 * 2.0 / 16.0);
          local2 = Math.Max(((Entity) head).Distance(((Entity) player).Center), (float) (((Entity) head).width + ((Entity) npc).width));
          this.AI_Timer = rotwaveTime - 45;
          this.collisionCooldown = 50;
          EModeNPCBehaviour.NetSync(npc);
        }
        this.HitPlayer = this.AttackTimer < 50;
        if (this.AttackTimer < -30 && !WorldSavingSystem.MasochistModeReal || this.AttackTimer > 95)
        {
          Neutral();
        }
        else
        {
          if (WorldSavingSystem.MasochistModeReal && (double) head.life < (double) head.lifeMax * 0.75 && this.AttackTimer < 0)
            ++local2;
          if ((double) ((Entity) npc).Distance(((Entity) head).Center) > (double) (((Entity) head).width + ((Entity) npc).width / 2) && this.collisionCooldown <= 0 && this.CollidingWithOtherHand(npc))
          {
            SoundEngine.PlaySound(ref SoundID.DD2_MonkStaffGroundImpact, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            this.collisionCooldown = 20;
            if (FargoSoulsUtil.HostCheck)
            {
              for (int index = 0; index < 2; ++index)
              {
                Vector2 vector2 = Utils.RotatedBy(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) head, ((Entity) npc).Center), 10f), (double) index * (double) local3 * 3.1415927410125732 / 22.0, new Vector2());
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<SkeletronGuardian2>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
            local3 = -local3;
          }
          int num1 = restDistance * 3;
          if (head.life < head.lifeMax / 2)
            num1 = (int) local2 + 10;
          float num2 = 75f;
          float num3 = 0.2f;
          if (this.AttackTimer > 65)
            num3 /= 4f;
          else
            local1 += local3 * 3.14159274f / num2;
          Vector2 vector2_1 = Vector2.op_Addition(((Entity) head).Center, Vector2.op_Multiply(Utils.ToRotationVector2(local1), (float) num1));
          ((Entity) npc).velocity = Vector2.op_Multiply(Vector2.op_Subtraction(vector2_1, ((Entity) npc).Center), num3);
          NPC npc = npc;
          ((Entity) npc).velocity = Vector2.op_Addition(((Entity) npc).velocity, ((Entity) head).velocity);
        }
      }

      void NonSpinAI()
      {
        ref float local1 = ref npc.localAI[1];
        ref float local2 = ref npc.ai[0];
        if (this.AI_Timer % rotwaveTime == 0 && this.AI_Timer >= rotwaveTime && (double) head.ai[2] + 30.0 < 800.0)
        {
          int num = this.AI_Timer % (rotwaveTime * 2) == 0 ? 1 : -1;
          if ((double) local2 == (double) num || WorldSavingSystem.MasochistModeReal)
            PrepareLunge();
        }
        if ((double) head.life > (double) head.lifeMax * 0.75 && this.AI_Timer % rotwaveTime == rotwaveTime / 2 && (double) head.ai[2] + 60.0 < 800.0)
        {
          int num = this.AI_Timer % (rotwaveTime * 2) == rotwaveTime / 2 ? 1 : -1;
          if ((double) local2 == (double) num)
            PrepareLunge();
        }
        if ((double) local1 == 0.0)
          Neutral();
        if ((double) local1 > 0.0)
        {
          if ((double) local1 < 30.0)
          {
            float num = 1f - local1 / 30f;
            this.storedVel = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_UnaryNegation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) player).Center)), num), 3f);
          }
          if ((double) local1 == 30.0)
            this.storedVel = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) player).Center), 30f);
          if ((double) local1 > 40.0 && (double) local1 <= 60.0)
            this.storedVel = Vector2.op_Multiply(this.storedVel, 0.95f);
          if ((double) local1 > 60.0)
          {
            this.storedVel = Vector2.op_Addition(this.storedVel, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, restPos), 0.3f));
            if ((double) local1 > 130.0 || (double) ((Entity) npc).Distance(restPos) < (double) ((Entity) npc).width)
            {
              local1 = 0.0f;
              this.storedVel = Vector2.Zero;
              EModeNPCBehaviour.NetSync(npc);
            }
          }
        }
        if ((double) local1 == 0.0)
          return;
        local1 += (float) Math.Sign(local1);
        ((Entity) npc).velocity = Vector2.op_Addition(this.storedVel, ((Entity) head).velocity);
      }

      void PrepareLunge()
      {
        ref float local = ref npc.localAI[1];
        SoundStyle zombie82 = SoundID.Zombie82;
        ((SoundStyle) ref zombie82).Volume = 1.4f;
        SoundEngine.PlaySound(ref zombie82, new Vector2?(((Entity) head).Center), (SoundUpdateCallback) null);
        local = 1f;
        for (int index = 0; index < 10; ++index)
        {
          Vector2 velocity = Vector2.op_Multiply(Utils.RotatedByRandom(Utils.RotatedBy(Vector2.UnitX, 6.2831854820251465 * (double) index / 10.0, new Vector2()), 0.31415927410125732), Utils.NextFloat(Main.rand, 3f, 8f));
          new SparkParticle(((Entity) npc).Center, velocity, Color.Red, 1f, 30).Spawn();
          local = 1f;
        }
        EModeNPCBehaviour.NetSync(npc);
      }

      void Neutral()
      {
        ((Entity) npc).velocity = Vector2.op_Multiply(Vector2.op_Subtraction(restPos, ((Entity) npc).Center), 0.02f);
        NPC npc = npc;
        ((Entity) npc).velocity = Vector2.op_Addition(((Entity) npc).velocity, ((Entity) head).velocity);
      }
    }

    public static bool OtherHandAlive(NPC self, NPC other)
    {
      return other != null && ((Entity) other).active && other.type == 36 && (double) other.ai[1] == (double) self.ai[1] && ((Entity) other).whoAmI != ((Entity) self).whoAmI;
    }

    private bool CollidingWithOtherHand(NPC npc)
    {
      return ((IEnumerable<NPC>) Main.npc).Any<NPC>((Func<NPC, bool>) (n =>
      {
        if (!SkeletronHand.OtherHandAlive(npc, n))
          return false;
        Rectangle hitbox = ((Entity) npc).Hitbox;
        return ((Rectangle) ref hitbox).Intersects(((Entity) n).Hitbox);
      }));
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
    {
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.ai[1], 35);
      return (npc1 == null || npc1.type != 35 || npc1.GetGlobalNPC<SkeletronHead>().SpawnGrace <= 0) && this.HitPlayer;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<LethargicBuff>(), 300, true, false);
      if (SkeletronHand.HeadSpinning(npc) && !WorldSavingSystem.MasochistModeReal)
        return;
      target.AddBuff(160, 60, true, false);
    }

    public static bool HeadSpinning(NPC npc)
    {
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.ai[1], 35);
      if (npc1 == null || !((Entity) npc1).active)
        return false;
      return (double) npc1.ai[1] == 1.0 || (double) npc1.ai[1] == 2.0;
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
    }
  }
}
