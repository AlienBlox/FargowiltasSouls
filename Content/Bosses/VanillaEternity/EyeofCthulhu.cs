// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.EyeofCthulhu
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Masomode;
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
  public class EyeofCthulhu : EModeNPCBehaviour
  {
    public int AITimer;
    public int ScytheSpawnTimer;
    public int FinalPhaseDashCD;
    public int FinalPhaseDashStageDuration;
    public int FinalPhaseAttackCounter;
    public bool IsInFinalPhase;
    public bool FinalPhaseBerserkDashesComplete;
    public bool FinalPhaseDashHorizSpeedSet;
    public bool DroppedSummon;
    public bool ScytheRingIsOnCD;
    public int TeleportDirection;
    private Vector2 targetCenter = Vector2.Zero;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(4);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.AITimer);
      binaryWriter.Write7BitEncodedInt(this.ScytheSpawnTimer);
      binaryWriter.Write7BitEncodedInt(this.FinalPhaseDashCD);
      binaryWriter.Write7BitEncodedInt(this.FinalPhaseDashStageDuration);
      binaryWriter.Write7BitEncodedInt(this.FinalPhaseAttackCounter);
      binaryWriter.Write7BitEncodedInt(this.TeleportDirection);
      bitWriter.WriteBit(this.IsInFinalPhase);
      bitWriter.WriteBit(this.FinalPhaseBerserkDashesComplete);
      bitWriter.WriteBit(this.FinalPhaseDashHorizSpeedSet);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.AITimer = binaryReader.Read7BitEncodedInt();
      this.ScytheSpawnTimer = binaryReader.Read7BitEncodedInt();
      this.FinalPhaseDashCD = binaryReader.Read7BitEncodedInt();
      this.FinalPhaseDashStageDuration = binaryReader.Read7BitEncodedInt();
      this.FinalPhaseAttackCounter = binaryReader.Read7BitEncodedInt();
      this.TeleportDirection = binaryReader.Read7BitEncodedInt();
      this.IsInFinalPhase = bitReader.ReadBit();
      this.FinalPhaseBerserkDashesComplete = bitReader.ReadBit();
      this.FinalPhaseDashHorizSpeedSet = bitReader.ReadBit();
    }

    public override bool SafePreAI(NPC npc)
    {
      ref float local1 = ref npc.ai[0];
      ref float local2 = ref npc.ai[1];
      ref float local3 = ref npc.ai[2];
      EModeGlobalNPC.eyeBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return true;
      npc.dontTakeDamage = npc.alpha > 50;
      if (npc.dontTakeDamage)
        Lighting.AddLight(((Entity) npc).Center, 0.75f, 1.35f, 1.5f);
      if (this.ScytheSpawnTimer > 0)
      {
        if (this.ScytheSpawnTimer % (this.IsInFinalPhase ? 2 : 6) == 0 && FargoSoulsUtil.HostCheck)
        {
          if (this.IsInFinalPhase && !WorldSavingSystem.MasochistModeReal)
          {
            int index = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<BloodScythe>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 1f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            if (index != Main.maxProjectiles)
              Main.projectile[index].timeLeft = 75;
          }
          else
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Normalize(((Entity) npc).velocity), ModContent.ProjectileType<BloodScythe>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 1f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
        --this.ScytheSpawnTimer;
      }
      if ((double) local1 == 0.0)
      {
        float num = 0.15f;
        if (npc.HasValidTarget)
          num = MathHelper.Lerp(0.15f, 0.5f, Math.Clamp(((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) / 1000f, 0.0f, 1f));
        NPC npc1 = npc;
        ((Entity) npc1).position = Vector2.op_Addition(((Entity) npc1).position, Vector2.op_Multiply(((Entity) npc).velocity, num));
        if ((double) local2 == 2.0)
        {
          NPC npc2 = npc;
          ((Entity) npc2).position = Vector2.op_Addition(((Entity) npc2).position, Vector2.op_Multiply(npc.ai[3] * 0.3f, ((Entity) npc).velocity));
        }
      }
      if (((double) local1 == 0.0 || (double) local1 == 3.0) && (double) local2 == 2.0 && !this.IsInFinalPhase)
      {
        float num = (double) local1 == 0.0 ? 0.25f : 0.5f;
        NPC npc3 = npc;
        ((Entity) npc3).position = Vector2.op_Addition(((Entity) npc3).position, Vector2.op_Multiply(npc.ai[3] * num, ((Entity) npc).velocity));
      }
      if ((double) local1 == 0.0 && (double) local2 == 2.0 && npc.HasValidTarget && WorldSavingSystem.MasochistModeReal)
      {
        float num1 = ((Vector2) ref ((Entity) npc).velocity).Length();
        float num2 = 0.25f;
        NPC npc4 = npc;
        ((Entity) npc4).velocity = Vector2.op_Addition(((Entity) npc4).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), num2));
        ((Entity) npc).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) npc).velocity), num1);
      }
      if ((double) local1 == 0.0 && (double) local2 == 2.0 && (double) local3 == 0.0)
        this.ScytheSpawnTimer = 30;
      if ((double) local2 == 3.0 && !this.IsInFinalPhase)
      {
        if (WorldSavingSystem.MasochistModeReal)
        {
          this.ScytheSpawnTimer = 30;
          SpawnServants();
        }
        if (!this.ScytheRingIsOnCD)
        {
          this.ScytheRingIsOnCD = true;
          if (FargoSoulsUtil.HostCheck)
            FargoSoulsUtil.XWay(8, ((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, ModContent.ProjectileType<BloodScythe>(), 1.5f, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f);
        }
      }
      else
        this.ScytheRingIsOnCD = false;
      if (npc.life < npc.lifeMax / 2)
      {
        if (this.IsInFinalPhase)
        {
          if (npc.HasValidTarget && (!Main.dayTime || Main.zenithWorld || Main.remixWorld))
          {
            if (npc.timeLeft < 300)
              npc.timeLeft = 300;
          }
          else
          {
            npc.TargetClosest(false);
            ((Entity) npc).velocity.X *= 0.98f;
            ((Entity) npc).velocity.Y -= (double) ((Entity) npc).velocity.Y > 0.0 ? 1f : 0.25f;
            if (npc.timeLeft > 30)
              npc.timeLeft = 30;
            this.AITimer = 90;
            this.FinalPhaseDashCD = 0;
            this.FinalPhaseBerserkDashesComplete = true;
            this.FinalPhaseDashHorizSpeedSet = false;
            this.FinalPhaseAttackCounter = 0;
            npc.alpha = 0;
            if ((double) npc.rotation > 3.1415927410125732)
              npc.rotation -= 6.28318548f;
            if ((double) npc.rotation < -3.1415927410125732)
              npc.rotation += 6.28318548f;
            float num = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)) - 1.57079637f;
            if ((double) num > 3.1415927410125732)
              num -= 6.28318548f;
            if ((double) num < -3.1415927410125732)
              num += 6.28318548f;
            npc.rotation = MathHelper.Lerp(npc.rotation, num, 0.07f);
          }
          if (++this.AITimer == 1)
          {
            if (FargoSoulsUtil.HostCheck)
            {
              ((Entity) npc).Center = ((Entity) Main.player[npc.target]).Center;
              ((Entity) npc).position.X += Utils.NextBool(Main.rand) ? -600f : 600f;
              ((Entity) npc).position.Y += Utils.NextBool(Main.rand) ? -400f : 400f;
              if (WorldSavingSystem.MasochistModeReal)
                ((Entity) npc).position.X += (float) Main.rand.Next(-100, 100);
              npc.TargetClosest(false);
              npc.netUpdate = true;
              EModeNPCBehaviour.NetSync(npc);
              this.AITimer = 40;
              if (npc.HasValidTarget)
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<SpectralEoC>(), 0, 0.0f, Main.myPlayer, (float) (this.AITimer + 20), (float) npc.target, 0.0f);
            }
            this.targetCenter = !npc.HasValidTarget ? ((Entity) npc).Center : ((Entity) Main.player[npc.target]).Center;
          }
          else if (this.AITimer < 90)
          {
            npc.alpha -= WorldSavingSystem.MasochistModeReal ? 30 : 25;
            if (npc.alpha < 0)
            {
              npc.alpha = 0;
              if (WorldSavingSystem.MasochistModeReal && this.AITimer < 90)
                this.AITimer = 90;
            }
            if ((double) npc.rotation > 3.1415927410125732)
              npc.rotation -= 6.28318548f;
            if ((double) npc.rotation < -3.1415927410125732)
              npc.rotation += 6.28318548f;
            float num = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, this.targetCenter)) - 1.57079637f;
            if ((double) num > 3.1415927410125732)
              num -= 6.28318548f;
            if ((double) num < -3.1415927410125732)
              num += 6.28318548f;
            npc.rotation = MathHelper.Lerp(npc.rotation, num, 0.3f);
            for (int index1 = 0; index1 < 3; ++index1)
            {
              int index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
              Main.dust[index2].noGravity = true;
              Main.dust[index2].noLight = true;
              Dust dust = Main.dust[index2];
              dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
            }
            Vector2 targetCenter = this.targetCenter;
            targetCenter.X += (double) ((Entity) npc).Center.X < (double) targetCenter.X ? -600f : 600f;
            targetCenter.Y += (double) ((Entity) npc).Center.Y < (double) targetCenter.Y ? -400f : 400f;
            ((Entity) npc).velocity = Vector2.Zero;
          }
          else if (!this.FinalPhaseBerserkDashesComplete)
          {
            this.AITimer = 90;
            if (++this.FinalPhaseDashCD == 1)
            {
              SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(this.targetCenter), (SoundUpdateCallback) null);
              if (!this.FinalPhaseDashHorizSpeedSet)
              {
                this.FinalPhaseDashHorizSpeedSet = true;
                ((Entity) npc).velocity.X = (double) ((Entity) npc).Center.X < (double) this.targetCenter.X ? 18f : -18f;
              }
              ((Entity) npc).velocity.Y = (double) ((Entity) npc).Center.Y < (double) this.targetCenter.Y ? 40f : -40f;
              this.ScytheSpawnTimer = 30;
              if (FargoSoulsUtil.HostCheck)
                FargoSoulsUtil.XWay(8, ((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, ModContent.ProjectileType<BloodScythe>(), 1f, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f);
              npc.netUpdate = true;
            }
            else if (this.FinalPhaseDashCD > 20)
              this.FinalPhaseDashCD = 0;
            if ((double) ++this.FinalPhaseDashStageDuration > 105.0)
            {
              this.ScytheSpawnTimer = 0;
              this.FinalPhaseDashStageDuration = 0;
              this.FinalPhaseBerserkDashesComplete = true;
              if (!WorldSavingSystem.MasochistModeReal)
                ++this.FinalPhaseAttackCounter;
              NPC npc5 = npc;
              ((Entity) npc5).velocity = Vector2.op_Multiply(((Entity) npc5).velocity, 0.75f);
              npc.netUpdate = true;
            }
            npc.rotation = Utils.ToRotation(((Entity) npc).velocity) - 1.57079637f;
            if ((double) npc.rotation > 3.1415927410125732)
              npc.rotation -= 6.28318548f;
            if ((double) npc.rotation < -3.1415927410125732)
              npc.rotation += 6.28318548f;
          }
          else
          {
            bool flag1 = this.FinalPhaseAttackCounter >= 3;
            int num3 = 180;
            if (flag1)
              num3 += 240;
            if (flag1 && this.AITimer < 330)
            {
              if (this.AITimer == 91)
                ((Entity) npc).velocity = Vector2.op_Multiply(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), ((Vector2) ref ((Entity) npc).velocity).Length()), 0.75f);
              ((Entity) npc).velocity.X *= 0.98f;
              if ((double) Math.Abs(((Entity) npc).Center.X - ((Entity) Main.player[npc.target]).Center.X) < 300.0)
                ((Entity) npc).velocity.X *= 0.9f;
              bool flag2 = Collision.SolidCollision(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height);
              if (!flag2 && (double) ((Entity) npc).Bottom.X > 0.0 && (double) ((Entity) npc).Bottom.X < (double) (Main.maxTilesX * 16) && (double) ((Entity) npc).Bottom.Y > 0.0 && (double) ((Entity) npc).Bottom.Y < (double) (Main.maxTilesY * 16))
              {
                Tile tileSafely = Framing.GetTileSafely(((Entity) npc).Bottom);
                if (Tile.op_Inequality(tileSafely, (ArgumentException) null) && ((Tile) ref tileSafely).HasUnactuatedTile)
                  flag2 = Main.tileSolid[(int) ((Tile) ref tileSafely).TileType];
              }
              if (flag2)
              {
                ((Entity) npc).velocity.X *= 0.95f;
                ((Entity) npc).velocity.Y -= 0.3f;
                if ((double) ((Entity) npc).velocity.Y > 0.0)
                  ((Entity) npc).velocity.Y = 0.0f;
                if ((double) Math.Abs(((Entity) npc).velocity.Y) > 24.0)
                  ((Entity) npc).velocity.Y = (float) (24 * Math.Sign(((Entity) npc).velocity.Y));
              }
              else
              {
                ((Entity) npc).velocity.Y += 0.3f;
                if ((double) ((Entity) npc).velocity.Y < 0.0)
                  ((Entity) npc).velocity.Y += 0.6f;
                if ((double) ((Entity) npc).velocity.Y > 15.0)
                  ((Entity) npc).velocity.Y = 15f;
              }
            }
            else
            {
              npc.alpha += WorldSavingSystem.MasochistModeReal ? 16 : 4;
              if (npc.alpha > (int) byte.MaxValue)
              {
                npc.alpha = (int) byte.MaxValue;
                if (WorldSavingSystem.MasochistModeReal && this.AITimer < num3)
                  this.AITimer = num3;
              }
              if (flag1)
              {
                ((Entity) npc).velocity.Y -= 0.15f;
                if ((double) ((Entity) npc).velocity.Y > 0.0)
                  ((Entity) npc).velocity.Y = 0.0f;
                if ((double) Math.Abs(((Entity) npc).velocity.Y) > 24.0)
                  ((Entity) npc).velocity.Y = (float) (24 * Math.Sign(((Entity) npc).velocity.Y));
              }
              else
              {
                NPC npc6 = npc;
                ((Entity) npc6).velocity = Vector2.op_Multiply(((Entity) npc6).velocity, 0.98f);
              }
            }
            float num4 = MathHelper.WrapAngle(Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)) - 1.57079637f);
            npc.rotation = MathHelper.WrapAngle(MathHelper.Lerp(npc.rotation, num4, 0.07f));
            if (npc.alpha > 0)
            {
              for (int index3 = 0; index3 < 3; ++index3)
              {
                int index4 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
                Main.dust[index4].noGravity = true;
                Main.dust[index4].noLight = true;
                Dust dust = Main.dust[index4];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
              }
            }
            if (this.AITimer > num3)
            {
              this.AITimer = 0;
              this.FinalPhaseDashCD = 0;
              this.FinalPhaseBerserkDashesComplete = false;
              this.FinalPhaseDashHorizSpeedSet = false;
              if (flag1)
                this.FinalPhaseAttackCounter = 0;
              ((Entity) npc).velocity = Vector2.Zero;
              npc.netUpdate = true;
            }
          }
          if (npc.netUpdate)
          {
            if (Main.netMode == 2)
            {
              NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              EModeNPCBehaviour.NetSync(npc);
            }
            npc.netUpdate = false;
          }
          return false;
        }
        if (!this.IsInFinalPhase && (double) npc.life <= (double) npc.lifeMax * 0.1)
        {
          NPC npc7 = npc;
          ((Entity) npc7).velocity = Vector2.op_Multiply(((Entity) npc7).velocity, 0.98f);
          npc.alpha += 4;
          for (int index5 = 0; index5 < 3; ++index5)
          {
            int index6 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
            Main.dust[index6].noGravity = true;
            Main.dust[index6].noLight = true;
            Dust dust = Main.dust[index6];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
          }
          if (npc.alpha > (int) byte.MaxValue)
          {
            npc.alpha = (int) byte.MaxValue;
            this.IsInFinalPhase = true;
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(npc.HasValidTarget ? ((Entity) Main.player[npc.target]).Center : ((Entity) npc).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, (float) npc.type, 0.0f);
          }
          return false;
        }
        if ((double) local1 == 3.0 && ((double) local2 == 0.0 || (double) local2 == 5.0))
        {
          if ((double) local3 < 2.0)
          {
            --local3;
            npc.alpha += 4;
            for (int index7 = 0; index7 < 3; ++index7)
            {
              int index8 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
              Main.dust[index8].noGravity = true;
              Main.dust[index8].noLight = true;
              Dust dust = Main.dust[index8];
              dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
            }
            if (npc.alpha > (int) byte.MaxValue)
            {
              npc.alpha = (int) byte.MaxValue;
              if (FargoSoulsUtil.HostCheck && npc.HasPlayerTarget)
              {
                local3 = 60f;
                local2 = 5f;
                Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
                if ((double) vector2.X == 0.0)
                  vector2.X = 1f;
                if ((double) Math.Abs(vector2.X) > 1200.0)
                  vector2.X = (float) (1200 * Math.Sign(vector2.X));
                else if ((double) Math.Abs(vector2.X) < 1100.0)
                  vector2.X = (float) (1100 * Math.Sign(vector2.X));
                if (this.TeleportDirection == 0)
                  this.TeleportDirection = Math.Sign(vector2.X);
                else
                  this.TeleportDirection *= -1;
                vector2.X = Math.Abs(vector2.X) * (float) this.TeleportDirection;
                if ((double) vector2.Y > 0.0)
                  vector2.Y *= -1f;
                if ((double) Math.Abs(vector2.Y) > 300.0)
                  vector2.Y = (float) (300 * Math.Sign(vector2.Y));
                if ((double) Math.Abs(vector2.Y) < 150.0)
                  vector2.Y = (float) (150 * Math.Sign(vector2.Y));
                vector2.X += Utils.NextFloat(Main.rand, -50f, 50f);
                vector2.Y += Utils.NextFloat(Main.rand, -200f, 200f);
                ((Entity) npc).Center = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, vector2);
                npc.netUpdate = true;
              }
            }
          }
          else
          {
            npc.alpha -= 2;
            if (Math.Abs(npc.alpha - 215) <= 2)
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            if (npc.alpha < 215 && npc.alpha > 182 && npc.HasValidTarget)
              ((Entity) npc).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), 50f);
            if (npc.alpha < 215 && npc.alpha > 90 && npc.alpha % 20 <= 2 && FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Normalize(((Entity) npc).velocity), ModContent.ProjectileType<BloodScythe>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 1f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            if (npc.alpha < 215 && npc.alpha > 30)
            {
              float num5 = ((Vector2) ref ((Entity) npc).velocity).Length();
              float num6 = 1f;
              NPC npc8 = npc;
              ((Entity) npc8).velocity = Vector2.op_Addition(((Entity) npc8).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), num6));
              ((Entity) npc).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) npc).velocity), num5);
            }
            if (npc.alpha < 0)
            {
              npc.alpha = 0;
            }
            else
            {
              --local3;
              NPC npc9 = npc;
              ((Entity) npc9).position = Vector2.op_Subtraction(((Entity) npc9).position, Vector2.op_Division(((Entity) npc).velocity, 2f));
              for (int index9 = 0; index9 < 3; ++index9)
              {
                int index10 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
                Main.dust[index10].noGravity = true;
                Main.dust[index10].noLight = true;
                Dust dust = Main.dust[index10];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
              }
            }
          }
        }
      }
      else
      {
        npc.alpha = 0;
        npc.dontTakeDamage = false;
      }
      EModeUtils.DropSummon(npc, "SuspiciousEye", NPC.downedBoss1, ref this.DroppedSummon);
      return true;

      void SpawnServants()
      {
        if ((double) npc.life > (double) npc.lifeMax * 0.65 || NPC.CountNPCS(5) >= 9 || !FargoSoulsUtil.HostCheck)
          return;
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(3f, 3f);
        for (int index1 = 0; index1 < 4; ++index1)
        {
          int index2 = NPC.NewNPC(((Entity) npc).GetSource_FromAI((string) null), (int) ((Entity) npc).Center.X, (int) ((Entity) npc).Center.Y, 5, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          if (index2 != Main.maxNPCs)
          {
            ((Entity) Main.npc[index2]).velocity = Utils.RotatedBy(vector2, Math.PI / 2.0 * (double) index1, new Vector2());
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, index2, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
        }
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      if (WorldSavingSystem.MasochistModeReal)
      {
        target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300, true, false);
        target.AddBuff(30, 600, true, false);
        target.AddBuff(163, 15, true, false);
      }
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 120, true, false);
      target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 300, true, false);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 0);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 1);
      EModeNPCBehaviour.LoadGoreRange(recolor, 6, 10);
    }
  }
}
