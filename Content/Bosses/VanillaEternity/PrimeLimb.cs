// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.PrimeLimb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class PrimeLimb : EModeNPCBehaviour
  {
    public int IdleOffsetX;
    public int IdleOffsetY;
    public int AttackTimer;
    public int NoContactDamageTimer;
    public float SpinRotation;
    public bool RangedAttackMode;
    public bool IsSwipeLimb;
    public bool InSpinningMode;
    public bool ModeReset;
    public bool CardinalSwipe;
    public int DontActWhenSpawnedTimer = 180;

    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(128, 131, 129, 130);
    }

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.IdleOffsetX);
      binaryWriter.Write7BitEncodedInt(this.IdleOffsetY);
      binaryWriter.Write7BitEncodedInt(this.AttackTimer);
      binaryWriter.Write7BitEncodedInt(this.NoContactDamageTimer);
      binaryWriter.Write(this.SpinRotation);
      bitWriter.WriteBit(this.RangedAttackMode);
      bitWriter.WriteBit(this.IsSwipeLimb);
      bitWriter.WriteBit(this.InSpinningMode);
      bitWriter.WriteBit(this.ModeReset);
      bitWriter.WriteBit(this.CardinalSwipe);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.IdleOffsetX = binaryReader.Read7BitEncodedInt();
      this.IdleOffsetY = binaryReader.Read7BitEncodedInt();
      this.AttackTimer = binaryReader.Read7BitEncodedInt();
      this.NoContactDamageTimer = binaryReader.Read7BitEncodedInt();
      this.SpinRotation = binaryReader.ReadSingle();
      this.RangedAttackMode = bitReader.ReadBit();
      this.IsSwipeLimb = bitReader.ReadBit();
      this.InSpinningMode = bitReader.ReadBit();
      this.ModeReset = bitReader.ReadBit();
      this.CardinalSwipe = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      npc.lifeMax = (int) ((double) npc.lifeMax * 1.5);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[68] = true;
      npc.buffImmune[ModContent.BuffType<ClippedWingsBuff>()] = true;
      npc.buffImmune[ModContent.BuffType<LethargicBuff>()] = true;
    }

    public override bool SafePreAI(NPC npc)
    {
      if (this.NoContactDamageTimer > 0)
        --this.NoContactDamageTimer;
      if (WorldSavingSystem.SwarmActive)
        return true;
      NPC head = FargoSoulsUtil.NPCExists(npc.ai[1], (int) sbyte.MaxValue);
      if (head == null)
      {
        if (FargoSoulsUtil.HostCheck)
        {
          npc.life = 0;
          npc.HitEffect(0, 10.0, new bool?());
          npc.checkDead();
        }
        return false;
      }
      if (!head.HasValidTarget || (double) head.ai[1] == 3.0)
        return true;
      if ((double) head.ai[0] != 2.0 && this.DontActWhenSpawnedTimer > 0)
      {
        --this.DontActWhenSpawnedTimer;
        ((Entity) npc).Center = ((Entity) head).Center;
        if (head.HasValidTarget)
        {
          Vector2 center = ((Entity) Main.player[head.target]).Center;
          center.Y -= 256f;
          if ((double) ((Entity) npc).Distance(center) < 256.0 && (double) ((Vector2) ref ((Entity) head).velocity).Length() < 8.0)
            this.DontActWhenSpawnedTimer = 0;
        }
        return false;
      }
      if (npc.timeLeft < 600)
        npc.timeLeft = 600;
      if (npc.dontTakeDamage)
      {
        if (npc.buffType[0] != 0)
          npc.DelBuff(0);
        if ((double) head.ai[0] != 2.0)
        {
          NPC npc1 = npc;
          ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, Vector2.op_Division(((Entity) npc).velocity, 2f));
        }
        int index = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, Utils.NextBool(Main.rand) ? 31 : 6, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index].noGravity = Utils.NextBool(Main.rand);
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
      }
      if (npc.type == 128)
      {
        if (npc.dontTakeDamage && (double) npc.localAI[0] > 1.0)
          npc.localAI[0] -= 0.5f;
        if ((double) npc.ai[2] != 0.0 && (double) npc.localAI[0] > 30.0)
        {
          npc.localAI[0] = 0.0f;
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2 = Utils.RotatedBy(new Vector2(16f, 0.0f), (double) npc.rotation + Math.PI / 2.0, new Vector2());
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<MechElectricOrb>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      else if (npc.type == 131)
      {
        if (npc.dontTakeDamage && (double) npc.localAI[0] > 1.0)
          npc.localAI[0] -= 0.5f;
        if ((double) npc.localAI[0] > 180.0)
        {
          npc.localAI[0] = 0.0f;
          Vector2 vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center);
          for (int index = -2; index <= 2; ++index)
          {
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(7f, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(1f) * (double) index, new Vector2())), 100, head.defDamage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      npc.damage = (double) head.ai[0] == 2.0 ? (int) ((double) head.defDamage * 1.25) : npc.defDamage;
      bool flag = false;
      if ((double) head.ai[0] != 2.0)
        return true;
      npc.target = head.target;
      if ((double) head.ai[1] == 3.0 || !npc.HasValidTarget)
        return true;
      if (this.IsSwipeLimb)
      {
        if (Main.getGoodWorld)
          flag = true;
        else if (!SwipeLimbAI())
          return false;
      }
      else if ((double) head.ai[1] == 1.0 || (double) head.ai[1] == 2.0)
      {
        if (!this.InSpinningMode)
        {
          this.NoContactDamageTimer = 2;
          int num = 0;
          switch (npc.type)
          {
            case 128:
              num = 0;
              break;
            case 129:
              num = 2;
              break;
            case 130:
              num = 3;
              break;
            case 131:
              num = 1;
              break;
          }
          Vector2 vector2_1 = Utils.RotatedBy(Vector2.op_Subtraction(((Entity) Main.player[head.target]).Center, ((Entity) head).Center), Math.PI / 2.0 * (double) num + Math.PI / 4.0, new Vector2());
          vector2_1 = Vector2.op_Multiply(Vector2.Normalize(vector2_1), ((Vector2) ref vector2_1).Length() + 200f);
          if ((double) ((Vector2) ref vector2_1).Length() < 600.0)
            vector2_1 = Vector2.op_Multiply(Vector2.Normalize(vector2_1), 600f);
          Vector2 vector2_2 = Vector2.op_Addition(((Entity) head).Center, vector2_1);
          ((Entity) npc).velocity = Vector2.op_Division(Vector2.op_Subtraction(vector2_2, ((Entity) npc).Center), 20f);
          if (this.IdleOffsetX == 0 && FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<PrimeTrail>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 1f, 0.0f);
          if (++this.IdleOffsetX > 60)
          {
            this.InSpinningMode = true;
            this.IdleOffsetX = (int) ((Vector2) ref vector2_1).Length();
            if (this.IdleOffsetX < 300)
              this.IdleOffsetX = 300;
            this.SpinRotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) head, ((Entity) npc).Center));
            npc.netUpdate = true;
          }
        }
        else
        {
          this.ModeReset = true;
          float num = (float) this.IdleOffsetX;
          if (head.HasValidTarget && (double) ((Entity) head).Distance(((Entity) Main.player[head.target]).Center) > (double) num)
            num = ((Entity) head).Distance(((Entity) Main.player[head.target]).Center);
          ((Entity) npc).Center = Vector2.op_Addition(((Entity) head).Center, Utils.RotatedBy(new Vector2(num, 0.0f), (double) this.SpinRotation, new Vector2()));
          this.SpinRotation += 0.1f;
          if ((double) this.SpinRotation > 3.1415927410125732)
          {
            this.SpinRotation -= 6.28318548f;
            npc.netUpdate = true;
          }
        }
        npc.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) head, ((Entity) npc).Center)) - 1.57079637f;
        if (npc.type == 131)
          npc.localAI[1] = 0.0f;
      }
      else
      {
        if (this.ModeReset)
        {
          this.ModeReset = false;
          this.RangedAttackMode = !this.RangedAttackMode;
          this.InSpinningMode = false;
          this.IdleOffsetX = 0;
          this.AttackTimer = -30;
          this.NoContactDamageTimer = 100;
          npc.netUpdate = true;
        }
        if (this.RangedAttackMode)
        {
          if (npc.type == 128)
          {
            flag = true;
            if (this.NoContactDamageTimer == 60 && FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.UnitX, ModContent.ProjectileType<GlowLine>(), 0, 0.0f, Main.myPlayer, 8f, (float) ((Entity) npc).whoAmI, 0.0f);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<PrimeTrail>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 2f, 0.0f);
            }
            if ((double) npc.ai[2] == 0.0)
            {
              ++npc.localAI[0];
              ++npc.ai[3];
            }
          }
          else if (npc.type == 131)
          {
            flag = true;
            if (this.NoContactDamageTimer == 60 && FargoSoulsUtil.HostCheck)
            {
              for (int index = -1; index <= 1; index += 2)
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(Vector2.UnitX, (double) MathHelper.ToRadians(20f) * (double) index, new Vector2()), ModContent.ProjectileType<GlowLine>(), 0, 0.0f, Main.myPlayer, 8f, (float) ((Entity) npc).whoAmI, 0.0f);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<PrimeTrail>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 2f, 0.0f);
            }
            if ((double) ((Entity) npc).Distance(((Entity) head).Center) > 400.0)
              ((Entity) npc).velocity = Vector2.op_Division(Vector2.op_Subtraction(((Entity) head).Center, ((Entity) npc).Center), 30f);
            npc.localAI[0] = 0.0f;
            if ((double) ++npc.localAI[1] == 95.0)
            {
              for (int index1 = -1; index1 <= 1; index1 += 2)
              {
                Vector2 vector2 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), (double) MathHelper.ToRadians(20f) * (double) index1, new Vector2());
                for (int index2 = -3; index2 <= 3; ++index2)
                {
                  if (FargoSoulsUtil.HostCheck)
                    Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(7.5f, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(1f) * (double) index2, new Vector2())), 100, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
              }
            }
            else if ((double) npc.localAI[1] > 190.0)
            {
              npc.localAI[1] = 0.0f;
              Vector2 vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center);
              for (int index = -3; index <= 3; ++index)
              {
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(7.5f, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(1f) * (double) index, new Vector2())), 100, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
          }
          else
          {
            Vector2 vector2 = Vector2.op_Division(Vector2.op_Subtraction(((Entity) head).Center, ((Entity) npc).Center), 8f);
            ((Entity) npc).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) npc).velocity, 23f), vector2), 24f);
            if (this.NoContactDamageTimer < 2)
              this.NoContactDamageTimer = 2;
          }
        }
        else if (npc.type == 129)
        {
          if (this.NoContactDamageTimer == 60)
          {
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, (float) npc.type, 0.0f);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<PrimeTrail>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 2f, 0.0f);
          }
          if (++this.AttackTimer < 90)
          {
            if (!npc.HasValidTarget)
              npc.TargetClosest(false);
            Vector2 vector2_3 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
            vector2_3.X -= 450f * (float) Math.Sign(vector2_3.X);
            vector2_3.Y -= 150f;
            ((Vector2) ref vector2_3).Normalize();
            Vector2 vector2_4 = Vector2.op_Multiply(vector2_3, 20f);
            if ((double) ((Entity) npc).velocity.X < (double) vector2_4.X)
            {
              ((Entity) npc).velocity.X += 0.75f;
              if ((double) ((Entity) npc).velocity.X < 0.0 && (double) vector2_4.X > 0.0)
                ((Entity) npc).velocity.X += 0.75f;
            }
            else if ((double) ((Entity) npc).velocity.X > (double) vector2_4.X)
            {
              ((Entity) npc).velocity.X -= 0.75f;
              if ((double) ((Entity) npc).velocity.X > 0.0 && (double) vector2_4.X < 0.0)
                ((Entity) npc).velocity.X -= 0.75f;
            }
            if ((double) ((Entity) npc).velocity.Y < (double) vector2_4.Y)
            {
              ((Entity) npc).velocity.Y += 0.75f;
              if ((double) ((Entity) npc).velocity.Y < 0.0 && (double) vector2_4.Y > 0.0)
                ((Entity) npc).velocity.Y += 0.75f;
            }
            else if ((double) ((Entity) npc).velocity.Y > (double) vector2_4.Y)
            {
              ((Entity) npc).velocity.Y -= 0.75f;
              if ((double) ((Entity) npc).velocity.Y > 0.0 && (double) vector2_4.Y < 0.0)
                ((Entity) npc).velocity.Y -= 0.75f;
            }
            npc.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)) - 1.57079637f;
          }
          else if (this.AttackTimer == 90)
          {
            SoundStyle soundStyle = SoundID.Item18;
            ((SoundStyle) ref soundStyle).Volume = 1.25f;
            SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            ((Entity) npc).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), npc.dontTakeDamage ? 20f : 25f);
            npc.rotation = Utils.ToRotation(((Entity) npc).velocity) - 1.57079637f;
            npc.netUpdate = true;
          }
          else if (this.AttackTimer > 120)
          {
            this.AttackTimer = npc.dontTakeDamage ? -90 : 0;
            npc.netUpdate = true;
          }
        }
        else if (npc.type == 130)
        {
          if (this.NoContactDamageTimer == 60 && FargoSoulsUtil.HostCheck)
          {
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, (float) npc.type, 0.0f);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<PrimeTrail>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 2f, 0.0f);
          }
          if (++this.AttackTimer < 90)
          {
            if (!npc.HasValidTarget)
              npc.TargetClosest(false);
            Vector2 center = ((Entity) Main.player[npc.target]).Center;
            center.X += (double) ((Entity) npc).Center.X < (double) center.X ? -50f : 50f;
            center.Y -= 300f;
            ((Entity) npc).velocity = Vector2.op_Division(Vector2.op_Subtraction(center, ((Entity) npc).Center), 40f);
          }
          else if (this.AttackTimer == 90)
          {
            SoundStyle soundStyle = SoundID.Item1;
            ((SoundStyle) ref soundStyle).Volume = 1.25f;
            ((SoundStyle) ref soundStyle).Pitch = 0.5f;
            SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
            vector2.Y += Math.Abs(vector2.X) * 0.25f;
            vector2.X *= 0.75f;
            ((Entity) npc).velocity = Vector2.op_Multiply(Vector2.Normalize(vector2), npc.dontTakeDamage ? 15f : 20f);
            npc.netUpdate = true;
          }
          else if (this.AttackTimer > 120)
          {
            this.AttackTimer = npc.dontTakeDamage ? -90 : 0;
            npc.netUpdate = true;
          }
          npc.rotation = Utils.ToRotation(((Entity) npc).DirectionFrom(((Entity) head).Center)) - 1.57079637f;
        }
        else
        {
          Vector2 vector2 = Vector2.op_Division(Vector2.op_Subtraction(((Entity) head).Center, ((Entity) npc).Center), 8f);
          ((Entity) npc).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) npc).velocity, 23f), vector2), 24f);
          if (this.NoContactDamageTimer < 2)
            this.NoContactDamageTimer = 2;
        }
      }
      if (npc.netUpdate)
      {
        npc.netUpdate = false;
        if (Main.netMode == 2)
          NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        EModeNPCBehaviour.NetSync(npc);
      }
      return flag;

      bool SwipeLimbAI()
      {
        npc.damage = (int) ((double) head.defDamage * 1.25);
        if (npc.life == 1 && head.GetGlobalNPC<SkeletronPrime>().FullySpawnedLimbs)
        {
          npc.dontTakeDamage = false;
          if (FargoSoulsUtil.HostCheck)
          {
            npc.life = 0;
            npc.HitEffect(0, 10.0, new bool?());
            ((Entity) npc).active = false;
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            return false;
          }
        }
        if (!this.ModeReset)
        {
          this.ModeReset = true;
          switch (npc.type)
          {
            case 128:
              this.IdleOffsetX = -1;
              this.IdleOffsetY = -1;
              break;
            case 129:
              this.IdleOffsetX = -1;
              this.IdleOffsetY = 1;
              break;
            case 130:
              this.IdleOffsetX = 1;
              this.IdleOffsetY = 1;
              break;
            case 131:
              this.IdleOffsetX = 1;
              this.IdleOffsetY = -1;
              break;
          }
          npc.netUpdate = true;
        }
        if ((double) ++npc.ai[2] < 180.0)
        {
          Vector2 vector2_1;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_1).\u002Ector((float) (400 * this.IdleOffsetX), (float) (400 * this.IdleOffsetY));
          if (this.CardinalSwipe)
            vector2_1 = Utils.RotatedBy(vector2_1, 0.78539818525314331, new Vector2());
          Vector2 vector2_2 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, vector2_1);
          ((Entity) npc).velocity = Vector2.op_Division(Vector2.op_Subtraction(vector2_2, ((Entity) npc).Center), 30f);
          if ((double) npc.ai[2] == 140.0)
          {
            SoundStyle soundStyle = SoundID.Item15;
            ((SoundStyle) ref soundStyle).Volume = 1.5f;
            SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<PrimeTrail>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 0.0f, 0.0f);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<IronParry>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
          if (this.NoContactDamageTimer < 2)
            this.NoContactDamageTimer = 2;
        }
        else if ((double) npc.ai[2] == 180.0)
        {
          SoundStyle soundStyle = SoundID.Item18;
          ((SoundStyle) ref soundStyle).Volume = 1.25f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          ((Entity) npc).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), 20f);
          this.IdleOffsetX *= -1;
          this.IdleOffsetY *= -1;
          this.CardinalSwipe = !this.CardinalSwipe;
          npc.netUpdate = true;
        }
        else if ((double) npc.ai[2] >= 210.0)
        {
          npc.ai[2] = (double) head.ai[1] == 1.0 || (double) head.ai[1] == 2.0 ? 0.0f : -90f;
          if (WorldSavingSystem.MasochistModeReal)
            npc.ai[2] += 60f;
          npc.netUpdate = true;
        }
        npc.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) head, ((Entity) npc).Center)) - 1.57079637f;
        return true;
      }
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot)
    {
      return this.NoContactDamageTimer <= 0;
    }

    public virtual bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
      return this.NoContactDamageTimer > 0 ? new bool?(false) : new bool?();
    }

    public virtual bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
      return this.NoContactDamageTimer > 0 ? new bool?(false) : new bool?();
    }

    public virtual bool CanBeHitByNPC(NPC npc, NPC attacker) => this.NoContactDamageTimer <= 0;

    public virtual bool ModifyCollisionData(
      NPC npc,
      Rectangle victimHitbox,
      ref int immunityCooldownSlot,
      ref MultipliableFloat damageMultiplier,
      ref Rectangle npcHitbox)
    {
      if (this.NoContactDamageTimer > 0)
        npcHitbox = new Rectangle();
      return base.ModifyCollisionData(npc, victimHitbox, ref immunityCooldownSlot, ref damageMultiplier, ref npcHitbox);
    }

    public virtual Color? GetAlpha(NPC npc, Color drawColor)
    {
      if (this.NoContactDamageTimer > 0)
        drawColor = Color.op_Multiply(drawColor, 0.5f);
      return base.GetAlpha(npc, drawColor);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<NanoInjectionBuff>(), 360, true, false);
    }

    public virtual bool CheckDead(NPC npc)
    {
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.ai[1], (int) sbyte.MaxValue);
      if (npc1 == null || (double) npc1.ai[1] == 2.0)
        return base.CheckDead(npc);
      npc.life = 1;
      ((Entity) npc).active = true;
      npc.dontTakeDamage = true;
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 50; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 30; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index5 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 6, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index5].velocity.Y -= Utils.NextFloat(Main.rand, 2f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
      if (FargoSoulsUtil.HostCheck)
        npc.netUpdate = true;
      return false;
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
    }
  }
}
