// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.Plantera
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class Plantera : PlanteraPart
  {
    public int DicerTimer;
    public int RingTossTimer;
    public int TentacleTimer = 480;
    public int CrystalRedirectTimer;
    public float TentacleAttackAngleOffset;
    public bool IsVenomEnraged;
    public bool InPhase2;
    public bool EnteredPhase2;
    public bool EnteredPhase3;
    public bool DroppedSummon;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(262);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.DicerTimer);
      binaryWriter.Write7BitEncodedInt(this.RingTossTimer);
      binaryWriter.Write7BitEncodedInt(this.TentacleTimer);
      binaryWriter.Write7BitEncodedInt(this.CrystalRedirectTimer);
      bitWriter.WriteBit(this.IsVenomEnraged);
      bitWriter.WriteBit(this.InPhase2);
      bitWriter.WriteBit(this.EnteredPhase2);
      bitWriter.WriteBit(this.EnteredPhase3);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.DicerTimer = binaryReader.Read7BitEncodedInt();
      this.RingTossTimer = binaryReader.Read7BitEncodedInt();
      this.TentacleTimer = binaryReader.Read7BitEncodedInt();
      this.CrystalRedirectTimer = binaryReader.Read7BitEncodedInt();
      this.IsVenomEnraged = bitReader.ReadBit();
      this.InPhase2 = bitReader.ReadBit();
      this.EnteredPhase2 = bitReader.ReadBit();
      this.EnteredPhase3 = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax = (int) Math.Round((double) npc.lifeMax * 1.6499999761581421);
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag1 = base.SafePreAI(npc);
      this.IsVenomEnraged = false;
      if (WorldSavingSystem.SwarmActive)
        return flag1;
      if (!this.EnteredPhase3 && (!npc.HasValidTarget || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > 3000.0))
      {
        ++((Entity) npc).velocity.Y;
        npc.TargetClosest(false);
        if ((!npc.HasValidTarget || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > 3000.0) && npc.timeLeft > 60)
          npc.timeLeft = 60;
      }
      Player player = Main.player[npc.target];
      if (!this.EnteredPhase3 && (double) npc.GetLifePercent() < 0.25)
      {
        this.EnteredPhase3 = true;
        SoundEngine.PlaySound(ref SoundID.Zombie21, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        npc.localAI[1] = 0.0f;
        npc.ai[0] = 0.0f;
        npc.ai[1] = 0.0f;
        npc.ai[2] = 0.0f;
        npc.ai[3] = 0.0f;
        FargoSoulsUtil.ClearHostileProjectiles(2, ((Entity) npc).whoAmI);
        foreach (NPC npc1 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => n.TypeAlive<CrystalLeaf>() && (double) n.ai[0] == (double) ((Entity) npc).whoAmI)))
        {
          npc1.life = 0;
          npc1.HitEffect(0, 10.0, new bool?());
          npc1.checkDead();
          ((Entity) npc1).active = false;
          if (Main.netMode == 2)
            NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc1).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
        for (int index = -20; index <= 20; ++index)
        {
          if (FargoSoulsUtil.HostCheck)
          {
            int num = Utils.NextFromList<int>(Main.rand, new int[3]
            {
              ModContent.ProjectileType<PlanteraManEater>(),
              ModContent.ProjectileType<PlanteraSnatcher>(),
              ModContent.ProjectileType<PlanteraTrapper>()
            });
            Vector2 vector2 = Utils.RotatedBy(Vector2.UnitY, (double) Utils.NextFloat(Main.rand, (float) Math.PI / 40f) + 1.5707963705062866 * ((double) index / 20.0), new Vector2());
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(vector2, 2000f)), Vector2.op_Multiply(Vector2.op_UnaryNegation(vector2), 6f), num, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, Utils.ToRotation(vector2), 0.0f);
          }
        }
      }
      if (this.EnteredPhase3)
      {
        ref float local1 = ref npc.ai[0];
        ref float local2 = ref npc.ai[1];
        ref float local3 = ref npc.ai[2];
        ref float local4 = ref npc.ai[3];
        if (!((Entity) player).active || player.dead || player.ghost || (double) ((Entity) npc).Distance(((Entity) player).Center) > 3000.0)
        {
          npc.TargetClosest(false);
          player = Main.player[npc.target];
          if (!((Entity) player).active || player.dead || player.ghost || (double) ((Entity) npc).Distance(((Entity) player).Center) > 3000.0)
          {
            if (npc.timeLeft > 60)
              npc.timeLeft = 60;
            ((Entity) npc).velocity.Y -= 0.4f;
            return false;
          }
        }
        else if (npc.timeLeft < 60)
          npc.timeLeft = 60;
        if (FargoSoulsUtil.HostCheck && !((IEnumerable<NPC>) Main.npc).Any<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == ModContent.NPCType<CrystalLeaf>() && (double) n.ai[0] == (double) ((Entity) npc).whoAmI && (double) n.ai[1] == 130.0)))
        {
          float num = 1.2566371f;
          for (int index = 0; index < 5; ++index)
          {
            Vector2 spawnPos = Vector2.op_Addition(((Entity) npc).Center, Utils.RotatedBy(new Vector2(130f, 0.0f), (double) num * (double) index, new Vector2()));
            FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), spawnPos, ModContent.NPCType<CrystalLeaf>(), ai0: (float) ((Entity) npc).whoAmI, ai1: 130f, ai3: num * (float) index, velocity: new Vector2());
          }
        }
        EnsureInnerRingSpawned();
        npc.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) player).Center)) + 1.57079637f;
        if ((double) local2 == 0.0)
        {
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) player).Center);
          float num1 = Math.Sign(vector2.Y) <= -10 ? 0.0f : Utils.Clamp<float>(Math.Abs(vector2.X), 300f, 500f) * (float) Math.Sign(vector2.X);
          float num2 = ((Entity) player).Center.X + (float) Math.Sign(vector2.X) * num1;
          float num3 = 50f;
          float num4 = ((Entity) player).Center.Y - num3;
          Vector2 target = Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitX, num2), Vector2.op_Multiply(Vector2.UnitY, num4));
          if ((double) vector2.Y > 0.0)
          {
            Movement(target, 0.3f, true);
            if ((double) local1 > 50.0)
              --local1;
            if ((double) Math.Abs(vector2.Y) < (double) Math.Abs(vector2.X))
            {
              ((Entity) npc).velocity.Y *= 0.95f;
              ((Entity) npc).velocity.X += (float) Math.Sign(vector2.X) * 0.5f;
            }
          }
          else
          {
            NPC npc2 = npc;
            ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, 0.96f);
          }
          if ((double) local1 > 60.0)
          {
            local1 = 0.0f;
            local2 = 1f;
            npc.TargetClosest(false);
          }
        }
        else
          ++local3;
        int num5 = Collision.SolidTiles(Vector2.op_Subtraction(((Entity) npc).Center, Vector2.op_Multiply(Vector2.UnitX, 500f)), 500, ((Entity) npc).height) ? 1 : 0;
        bool flag2 = Collision.SolidTiles(((Entity) npc).Center, 500, ((Entity) npc).height);
        float x = ((Entity) player).Center.X;
        if (num5 != 0 && !flag2)
          x += 500f;
        if (num5 == 0 & flag2)
          x -= 500f;
        float num6 = ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center);
        if (npc.HasValidTarget && (double) num6 < 250.0 && ((double) npc.ai[1] != 1.0 || (double) npc.ai[2] <= 2.0 || (double) npc.ai[1] == 2.0))
        {
          float num7 = (float) (1.0 * (1.0 - (double) num6 / 250.0));
          NPC npc3 = npc;
          ((Entity) npc3).velocity = Vector2.op_Subtraction(((Entity) npc3).velocity, Vector2.op_Multiply(num7, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)));
        }
        float num8 = local2;
        if ((double) num8 != 1.0)
        {
          if ((double) num8 != 2.0)
          {
            if ((double) num8 == 3.0)
            {
              if ((double) local1 < 100.0)
              {
                float num9 = local1 / 100f;
                if ((double) local1 < 70.0)
                {
                  WallHugMovement(true, 4f, targetPosX: x);
                }
                else
                {
                  NPC npc4 = npc;
                  ((Entity) npc4).velocity = Vector2.op_Multiply(((Entity) npc4).velocity, 0.96f);
                }
                for (int index = -1; index <= 1; index += 2)
                {
                  float rotation = Utils.ToRotation(Vector2.Lerp(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) -index * 1.5707963705062866 * 0.10000000149011612, new Vector2())), Utils.RotatedBy(Vector2.UnitY, (double) index * 1.5707963705062866 * 0.30000001192092896, new Vector2()), num9));
                  if ((double) local1 % 5.0 == 4.0 && FargoSoulsUtil.HostCheck)
                    Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(Utils.RotatedByRandom(Utils.ToRotationVector2(rotation), 0.78539818525314331), 24f), ModContent.ProjectileType<PlanteraTentacle>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, rotation, 0.0f);
                }
              }
              else
              {
                NPC npc5 = npc;
                ((Entity) npc5).velocity = Vector2.op_Multiply(((Entity) npc5).velocity, 0.96f);
                int num10 = WorldSavingSystem.MasochistModeReal ? 9 : 14;
                if ((double) local1 % (double) num10 == 0.0 && ((double) local1 > 100.0 || WorldSavingSystem.MasochistModeReal) && (double) local1 < 333.0 && (double) local1 % (double) (num10 * 4) <= (double) (num10 * 2))
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) player).Center), ModContent.ProjectileType<PlanteraMushroomThing>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                if ((double) local1 == 440.0)
                {
                  for (int index = 0; index < Main.maxNPCs; ++index)
                  {
                    if (Main.npc[index].TypeAlive<CrystalLeaf>() && (double) Main.npc[index].ai[0] == (double) ((Entity) npc).whoAmI)
                      Main.npc[index].StrikeInstantKill();
                  }
                }
                if ((double) local1 > 500.0)
                {
                  local1 = 0.0f;
                  local2 = 1f;
                  npc.TargetClosest(false);
                }
              }
            }
          }
          else
          {
            ref float local5 = ref npc.localAI[1];
            if ((double) local1 >= (double) (100 + (WorldSavingSystem.MasochistModeReal ? -20 : 20)) && (double) local1 % 50.0 == 0.0)
            {
              SoundEngine.PlaySound(ref SoundID.NPCDeath13, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
              {
                float num11 = WorldSavingSystem.MasochistModeReal ? 0.18f : 0.27f;
                int num12 = WorldSavingSystem.MasochistModeReal ? 3 : 2;
                for (int index = -num12; index <= num12; ++index)
                {
                  double rotation = (double) Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) player).Center));
                  float num13 = 1f;
                  double num14 = (double) index * 1.5707963705062866 * (double) num11;
                  Vector2 rotationVector2 = Utils.ToRotationVector2((float) (rotation + num14));
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Division(Vector2.op_Multiply(rotationVector2, (float) ((Entity) npc).width), 2f)), Vector2.op_Multiply(rotationVector2, num13), ModContent.ProjectileType<PlanteraThornChakram>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 4f, 0.0f, 0.0f);
                }
                for (int index = -num12; index <= num12 + 1; ++index)
                {
                  float num15 = (float) index - 0.5f;
                  double rotation = (double) Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) player).Center));
                  float num16 = 2f;
                  double num17 = (double) num15 * 1.5707963705062866 * (double) num11;
                  Vector2 rotationVector2 = Utils.ToRotationVector2((float) (rotation + num17));
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Division(Vector2.op_Multiply(rotationVector2, (float) ((Entity) npc).width), 2f)), Vector2.op_Multiply(rotationVector2, num16), ModContent.ProjectileType<PlanteraThornChakram>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 8f, 0.0f, 0.0f);
                }
              }
            }
            if ((double) local1 < 100.0)
            {
              float num18 = local1 / 100f;
              if ((double) local5 == 0.0)
              {
                bool flag3 = Collision.SolidTiles(Vector2.op_Subtraction(((Entity) npc).Center, Vector2.op_Multiply(Vector2.UnitX, 500f)), 500, ((Entity) npc).height);
                bool flag4 = Collision.SolidTiles(((Entity) npc).Center, 500, ((Entity) npc).height);
                if ((double) local1 == 0.0)
                {
                  local4 = (float) Math.Sign(((Entity) npc).Center.X - ((Entity) player).Center.X);
                  if (flag3 && !flag4)
                    local4 = 1f;
                  if (!flag3 & flag4)
                    local4 = -1f;
                }
                if ((double) local4 == 0.0)
                  local4 = Utils.NextBool(Main.rand) ? 1f : -1f;
              }
              if ((double) local1 < 70.0 && ((double) local5 != 1.0 || (double) local1 >= 60.000003814697266))
              {
                WallHugMovement(true, 4f, 0.1f, x);
              }
              else
              {
                NPC npc6 = npc;
                ((Entity) npc6).velocity = Vector2.op_Multiply(((Entity) npc6).velocity, 0.96f);
              }
              float num19 = local4 * ((double) local5 == 1.0 ? -1f : 1f);
              float rotation = Utils.ToRotation(Vector2.Lerp(Vector2.op_Multiply(Vector2.UnitX, num19), Utils.RotatedBy(Vector2.UnitY, 0.23561947047710419 * (double) num19, new Vector2()), num18));
              if ((double) local1 % 5.0 == 4.0 && FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(Utils.RotatedByRandom(Utils.ToRotationVector2(rotation), 0.78539818525314331), 24f), ModContent.ProjectileType<PlanteraTentacle>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, rotation, 0.0f);
            }
            else
            {
              NPC npc7 = npc;
              ((Entity) npc7).velocity = Vector2.op_Multiply(((Entity) npc7).velocity, 0.96f);
              if ((double) local1 > 100.0 * ((double) local5 == 1.0 ? 4.4000000953674316 : 3.9000000953674316))
              {
                local1 = 0.0f;
                if ((double) local5 == 0.0)
                {
                  local5 = 1f;
                }
                else
                {
                  local5 = 0.0f;
                  local2 = 3f;
                  npc.TargetClosest(false);
                }
              }
            }
          }
        }
        else
        {
          ref float local6 = ref npc.localAI[1];
          if ((double) local1 < 360.0)
            WallHugMovement();
          else
            WallHugMovement(true, 4f, 0.1f, x);
          float frames = (float) Luminance.Common.Utilities.Utilities.SecondsToFrames(9f);
          int num20 = (int) (17.0 * (double) MathHelper.Lerp(WorldSavingSystem.MasochistModeReal ? 2f : 3f, 1f, MathHelper.Clamp(local1 * 2f / frames, 0.0f, 1f)));
          ++local6;
          if ((double) local6 >= (double) num20 && (double) local1 < 360.0)
          {
            local6 = 0.0f;
            foreach (NPC npc8 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == ModContent.NPCType<CrystalLeaf>() && (double) n.ai[0] == (double) ((Entity) npc).whoAmI && (double) n.ai[1] == 130.0)))
            {
              SoundEngine.PlaySound(ref SoundID.Grass, new Vector2?(((Entity) npc8).Center), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
              {
                Vector2 vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) npc8).Center);
                Projectile.NewProjectile(Entity.InheritSource((Entity) npc8), ((Entity) npc8).Center, Vector2.op_Multiply(7f, vector2), ModContent.ProjectileType<CrystalLeafShot>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 0.0f, 0.0f);
              }
            }
          }
          if ((double) local1 == 360.0)
          {
            SoundStyle zombie21 = SoundID.Zombie21;
            ((SoundStyle) ref zombie21).Pitch = -0.3f;
            SoundEngine.PlaySound(ref zombie21, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            int num21 = !SoulConfig.Instance.BossRecolors ? 0 : (WorldSavingSystem.EternityMode ? 1 : 0);
            Color drawColor = num21 != 0 ? Color.DeepSkyBlue : Color.LimeGreen;
            Color color = num21 != 0 ? Color.DarkBlue : Color.ForestGreen;
            new ExpandingBloomParticle(((Entity) npc).Center, Vector2.Zero, drawColor, Vector2.Zero, Vector2.op_Multiply(Vector2.One, 100f), 20, true, new Color?(color)).Spawn();
            foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.type == ModContent.ProjectileType<CrystalLeafShot>() && (double) p.ai[0] == (double) ((Entity) npc).whoAmI)))
            {
              projectile.ai[1] = 1f;
              projectile.ai[2] = (float) ((Entity) player).whoAmI;
              projectile.netUpdate = true;
            }
          }
          if ((double) local1 >= (double) frames)
          {
            local1 = 0.0f;
            local6 = 0.0f;
            local2 = 2f;
            npc.TargetClosest(false);
          }
        }
        if ((double) ((Entity) npc).Center.Y < (double) ((Entity) player).Center.Y)
        {
          float num22 = MathHelper.Lerp(-6f, -3f, Math.Clamp(((Entity) player).Center.Y - ((Entity) npc).Center.Y, 0.0f, 1000f) / 1000f);
          if ((double) ((Entity) npc).velocity.Y < (double) num22)
            ((Entity) npc).velocity.Y = MathHelper.Lerp(((Entity) npc).velocity.Y, num22, 0.2f);
        }
        ++local1;
        return false;
      }
      if (--this.RingTossTimer < 0)
      {
        this.RingTossTimer = 480;
        EnsureInnerRingSpawned();
      }
      else if (this.RingTossTimer == 120)
      {
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
        if (FargoSoulsUtil.HostCheck)
        {
          float shootSpeed = 8f;
          Vector2 vector2;
          if (WorldSavingSystem.MasochistModeReal)
          {
            vector2 = FargoSoulsUtil.PredictiveAim(((Entity) npc).Center, ((Entity) player).Center, ((Entity) player).velocity, shootSpeed);
            ((Vector2) ref vector2).Normalize();
          }
          else
            vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) player).Center);
          int index = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(shootSpeed, vector2), ModContent.ProjectileType<MutantMark2>(), npc.defDamage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          if (index != Main.maxProjectiles)
          {
            Main.projectile[index].timeLeft -= 300;
            foreach (NPC npc9 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == ModContent.NPCType<CrystalLeaf>() && (double) n.ai[0] == (double) ((Entity) npc).whoAmI && (double) n.ai[1] == 130.0)))
            {
              SoundEngine.PlaySound(ref SoundID.Grass, new Vector2?(((Entity) npc9).Center), (SoundUpdateCallback) null);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc9).Center, Vector2.Zero, ModContent.ProjectileType<PlanteraCrystalLeafRing>(), npc.defDamage / 4, 0.0f, Main.myPlayer, (float) Main.projectile[index].identity, npc9.ai[3], 0.0f);
              npc9.life = 0;
              npc9.HitEffect(0, 10.0, new bool?());
              npc9.checkDead();
              ((Entity) npc9).active = false;
              if (Main.netMode == 2)
                NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc9).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
          }
        }
      }
      if (!this.InPhase2)
      {
        if (this.RingTossTimer == 420)
        {
          if (this.CrystalRedirectTimer >= 2)
          {
            SoundStyle zombie21 = SoundID.Zombie21;
            ((SoundStyle) ref zombie21).Pitch = -0.3f;
            SoundEngine.PlaySound(ref zombie21, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            int num = !SoulConfig.Instance.BossRecolors ? 0 : (WorldSavingSystem.EternityMode ? 1 : 0);
            Color drawColor = num != 0 ? Color.DeepSkyBlue : Color.LimeGreen;
            Color color = num != 0 ? Color.DarkBlue : Color.ForestGreen;
            new ExpandingBloomParticle(((Entity) npc).Center, Vector2.Zero, drawColor, Vector2.Zero, Vector2.op_Multiply(Vector2.One, 100f), 20, true, new Color?(color)).Spawn();
            foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.type == ModContent.ProjectileType<CrystalLeafShot>() && (double) p.ai[0] == (double) ((Entity) npc).whoAmI)))
            {
              projectile.ai[1] = 1f;
              projectile.ai[2] = (float) ((Entity) player).whoAmI;
              projectile.netUpdate = true;
            }
            this.CrystalRedirectTimer = 0;
            npc.netUpdate = true;
          }
          else
          {
            ++this.CrystalRedirectTimer;
            npc.netUpdate = true;
          }
        }
        if (this.RingTossTimer.IsWithinBounds(300, 420) && this.CrystalRedirectTimer == 0 && !this.EnteredPhase2)
        {
          NPC npc10 = npc;
          ((Entity) npc10).velocity = Vector2.op_Multiply(((Entity) npc10).velocity, 0.96f);
          npc.localAI[1] = 0.0f;
        }
      }
      if (npc.life > npc.lifeMax / 2)
      {
        if (--this.DicerTimer < 0)
        {
          this.DicerTimer = 625;
          if (WorldSavingSystem.MasochistModeReal && npc.HasValidTarget && FargoSoulsUtil.HostCheck)
          {
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.player[npc.target]).Center, Vector2.Zero, ModContent.ProjectileType<DicerPlantera>(), npc.defDamage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            for (int index = 0; index < 3; ++index)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(30f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), 2.0943951606750488 * (double) index, new Vector2())), ModContent.ProjectileType<DicerPlantera>(), npc.defDamage / 4, 0.0f, Main.myPlayer, 1f, 1f, 0.0f);
          }
        }
      }
      else
      {
        if (!this.InPhase2)
        {
          this.InPhase2 = true;
          this.DicerTimer = 0;
        }
        if (!this.EnteredPhase2)
        {
          this.EnteredPhase2 = true;
          if (FargoSoulsUtil.HostCheck)
          {
            if (!((IEnumerable<NPC>) Main.npc).Any<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == ModContent.NPCType<CrystalLeaf>() && (double) n.ai[0] == (double) ((Entity) npc).whoAmI && (double) n.ai[1] == 130.0)))
            {
              float num = 1.2566371f;
              for (int index = 0; index < 5; ++index)
              {
                Vector2 spawnPos = Vector2.op_Addition(((Entity) npc).Center, Utils.RotatedBy(new Vector2(130f, 0.0f), (double) num * (double) index, new Vector2()));
                FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), spawnPos, ModContent.NPCType<CrystalLeaf>(), ai0: (float) ((Entity) npc).whoAmI, ai1: 130f, ai3: num * (float) index, velocity: new Vector2());
              }
            }
            SpawnOuterLeafRing();
          }
          DespawnProjs();
        }
        if (--this.DicerTimer < -120)
        {
          this.DicerTimer = 1520;
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
          if (FargoSoulsUtil.HostCheck)
          {
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<DicerPlantera>(), npc.defDamage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            for (int index = 0; index < 3; ++index)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(25f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), 2.0943951606750488 * (double) index, new Vector2())), ModContent.ProjectileType<DicerPlantera>(), npc.defDamage / 4, 0.0f, Main.myPlayer, 1f, 8f, 0.0f);
          }
        }
        if (this.DicerTimer > 800 || this.DicerTimer < 0)
        {
          if (this.RingTossTimer > 120)
            this.RingTossTimer = 120;
        }
        else if (this.DicerTimer < 800)
        {
          --this.RingTossTimer;
          if (this.RingTossTimer % 2 == 0)
            --this.RingTossTimer;
        }
        else if (this.DicerTimer == 800)
          this.RingTossTimer = 121;
        this.IsVenomEnraged = npc.HasPlayerTarget && Main.player[npc.target].venom;
        if (--this.TentacleTimer <= 0)
        {
          float num23 = Math.Min(0.9f, (float) -this.TentacleTimer / 60f);
          if (WorldSavingSystem.MasochistModeReal && (double) num23 > 0.75)
            num23 = 0.75f;
          NPC npc11 = npc;
          ((Entity) npc11).position = Vector2.op_Subtraction(((Entity) npc11).position, Vector2.op_Multiply(((Entity) npc).velocity, num23));
          if (this.TentacleTimer == 0)
          {
            this.TentacleAttackAngleOffset = Utils.NextFloat(Main.rand, 6.28318548f);
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
            foreach (NPC npc12 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == ModContent.NPCType<CrystalLeaf>() && (double) n.ai[0] == (double) ((Entity) npc).whoAmI && (double) n.ai[1] > 130.0)))
            {
              SoundEngine.PlaySound(ref SoundID.Grass, new Vector2?(((Entity) npc12).Center), (SoundUpdateCallback) null);
              npc12.life = 0;
              npc12.HitEffect(0, 10.0, new bool?());
              npc12.checkDead();
              ((Entity) npc12).active = false;
              if (Main.netMode == 2)
                NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc12).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
          }
          float num24 = 45f;
          if (this.TentacleTimer >= -30 && this.TentacleTimer % 3 == 0)
          {
            int num25 = Math.Abs(this.TentacleTimer) / 3;
            for (int index = -num25; index <= num25; index += num25 * 2)
            {
              float num26 = MathHelper.WrapAngle(this.TentacleAttackAngleOffset + MathHelper.ToRadians(num24 / 10f) * ((float) index + Utils.NextFloat(Main.rand, -0.5f, 0.5f)));
              if (FargoSoulsUtil.HostCheck)
              {
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.NextVector2CircularEdge(Main.rand, 24f, 24f), ModContent.ProjectileType<PlanteraTentacle>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, num26, 0.0f);
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.NextVector2CircularEdge(Main.rand, 24f, 24f), ModContent.ProjectileType<PlanteraTentacle>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, num26 + 3.14159274f, 0.0f);
              }
              if (index == 0)
                break;
            }
          }
          if (this.TentacleTimer < -390)
          {
            this.TentacleTimer = 600 + Main.rand.Next(120);
            if (!WorldSavingSystem.MasochistModeReal)
              ((Entity) npc).velocity = Vector2.Zero;
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
            SpawnOuterLeafRing();
          }
        }
        else
        {
          NPC npc13 = npc;
          ((Entity) npc13).position = Vector2.op_Subtraction(((Entity) npc13).position, Vector2.op_Multiply(((Entity) npc).velocity, this.IsVenomEnraged ? 0.1f : 0.2f));
        }
      }
      EModeUtils.DropSummon(npc, "PlanterasFruit", NPC.downedPlantBoss, ref this.DroppedSummon);
      return flag1;

      static void DespawnProjs()
      {
        for (int index = 0; index < Main.maxProjectiles; ++index)
        {
          if (((Entity) Main.projectile[index]).active && Main.projectile[index].hostile && (Main.projectile[index].type == 277 || Main.projectile[index].type == ModContent.ProjectileType<DicerPlantera>() || Main.projectile[index].type == ModContent.ProjectileType<PlanteraCrystalLeafRing>() || Main.projectile[index].type == ModContent.ProjectileType<CrystalLeafShot>()))
            Main.projectile[index].Kill();
        }
      }

      void Movement(Vector2 target, float speed, bool fastX = false)
      {
        float num1 = 1f;
        float num2 = 14f;
        if ((double) Math.Abs(((Entity) npc).Center.X - target.X) > 10.0)
        {
          if ((double) ((Entity) npc).Center.X < (double) target.X)
          {
            ((Entity) npc).velocity.X += speed;
            if ((double) ((Entity) npc).velocity.X < 0.0)
              ((Entity) npc).velocity.X += speed * (fastX ? 2f : 1f) * num1;
          }
          else
          {
            ((Entity) npc).velocity.X -= speed;
            if ((double) ((Entity) npc).velocity.X > 0.0)
              ((Entity) npc).velocity.X -= speed * (fastX ? 2f : 1f) * num1;
          }
        }
        if ((double) ((Entity) npc).Center.Y < (double) target.Y)
        {
          ((Entity) npc).velocity.Y += speed;
          if ((double) ((Entity) npc).velocity.Y < 0.0)
            ((Entity) npc).velocity.Y += speed * 2f * num1;
        }
        else
        {
          ((Entity) npc).velocity.Y -= speed;
          if ((double) ((Entity) npc).velocity.Y > 0.0)
            ((Entity) npc).velocity.Y -= speed * 2f * num1;
        }
        if ((double) Math.Abs(((Entity) npc).velocity.X) > (double) num2)
          ((Entity) npc).velocity.X = num2 * (float) Math.Sign(((Entity) npc).velocity.X);
        if ((double) Math.Abs(((Entity) npc).velocity.Y) <= (double) num2)
          return;
        ((Entity) npc).velocity.Y = num2 * (float) Math.Sign(((Entity) npc).velocity.Y);
      }

      void WallHugMovement(bool fastX = false, float speedMult = 1f, float heightMult = 1f, float targetPosX = 0.0f)
      {
        ref float local = ref npc.ai[2];
        int num1 = 100;
        int num2 = (int) ((double) (200 + 120 * (int) MathF.Sin((float) (6.2831854820251465 * (double) local / 501.00003051757813))) * (double) heightMult);
        bool flag = Collision.SolidCollision(Vector2.op_Subtraction(Vector2.op_Subtraction(((Entity) npc).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitX, (float) num1), 2f)), Vector2.op_Multiply(Vector2.UnitY, (float) num2)), num1, num2);
        if (!Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) player).Center, 0, 0))
          flag = true;
        if ((double) ((Entity) player).Center.X - (double) ((Entity) npc).Center.X > 900.0)
          flag = true;
        float num3 = !flag || (double) ((Entity) player).Center.Y - (double) ((Entity) npc).Center.Y <= 150.0 ? -1f : 1f;
        if (Collision.SolidCollision(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height))
          num3 = (float) Math.Sign(((Entity) player).Center.Y - 150f - ((Entity) npc).Center.Y);
        float num4 = ((Entity) player).Center.X + 130f * MathF.Sin((float) (6.2831854820251465 * (double) local / 300.0));
        if ((double) targetPosX != 0.0)
          num4 = targetPosX;
        Movement(Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitX, ((Entity) npc).Center.X + (float) Math.Sign(num4 - ((Entity) npc).Center.X) * 50f), Vector2.op_Multiply(Vector2.UnitY, ((Entity) npc).Center.Y + num3 * 60f)), 0.15f * speedMult, fastX);
      }

      void EnsureInnerRingSpawned()
      {
        if (!FargoSoulsUtil.HostCheck || ((IEnumerable<NPC>) Main.npc).Any<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == ModContent.NPCType<CrystalLeaf>() && (double) n.ai[0] == (double) ((Entity) npc).whoAmI && (double) n.ai[1] == 130.0)))
          return;
        float num = 1.2566371f;
        for (int index = 0; index < 5; ++index)
        {
          Vector2 spawnPos = Vector2.op_Addition(((Entity) npc).Center, Utils.RotatedBy(new Vector2(130f, 0.0f), (double) num * (double) index, new Vector2()));
          FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), spawnPos, ModContent.NPCType<CrystalLeaf>(), ai0: (float) ((Entity) npc).whoAmI, ai1: 130f, ai3: num * (float) index, velocity: new Vector2());
        }
      }

      void SpawnOuterLeafRing()
      {
        int num1 = WorldSavingSystem.MasochistModeReal ? 12 : 9;
        float num2 = 6.28318548f / (float) num1;
        for (int index = 0; index < num1; ++index)
        {
          Vector2 spawnPos = Vector2.op_Addition(((Entity) npc).Center, Utils.RotatedBy(new Vector2(250f, 0.0f), (double) num2 * (double) index, new Vector2()));
          FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), spawnPos, ModContent.NPCType<CrystalLeaf>(), ai0: (float) ((Entity) npc).whoAmI, ai1: 250f, ai3: num2 * (float) index, velocity: new Vector2());
        }
      }
    }

    public override void SafePostAI(NPC npc)
    {
      base.SafePostAI(npc);
      npc.defense = Math.Max(npc.defense, npc.defDefense);
    }

    public virtual Color? GetAlpha(NPC npc, Color drawColor)
    {
      return this.IsVenomEnraged ? new Color?(new Color((int) byte.MaxValue, (int) ((Color) ref drawColor).G / 2, (int) ((Color) ref drawColor).B / 2)) : base.GetAlpha(npc, drawColor);
    }

    public virtual bool PreDraw(
      NPC npc,
      SpriteBatch spriteBatch,
      Vector2 screenPos,
      Color drawColor)
    {
      bool flag = SoulConfig.Instance.BossRecolors && WorldSavingSystem.EternityMode;
      if (this.EnteredPhase3)
      {
        Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) npc).Center, screenPos);
        Color color = flag ? Color.Blue : Color.Green;
        for (int index = 0; index < 12; ++index)
        {
          Vector2 vector2_2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (6.2831854820251465 * (double) index / 12.0)), 1f);
          spriteBatch.Draw(TextureAssets.Npc[npc.type].Value, Vector2.op_Addition(vector2_1, vector2_2), new Rectangle?(npc.frame), color, npc.rotation, Vector2.op_Multiply(Utils.Size(npc.frame), 0.5f), npc.scale, (SpriteEffects) 0, 0.0f);
        }
      }
      return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
    }

    public static float DR(NPC npc)
    {
      return (double) npc.GetLifePercent() >= 0.25 && (double) npc.GetLifePercent() >= 0.5 ? 0.0f : 0.4f;
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 1f - Plantera.DR(npc));
    }

    public virtual void UpdateLifeRegen(NPC npc, ref int damage)
    {
      npc.lifeRegen = (int) Math.Round((double) npc.lifeRegen * (1.0 - (double) Plantera.DR(npc)));
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 11);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 12);
      EModeNPCBehaviour.LoadGoreRange(recolor, 378, 391);
      EModeNPCBehaviour.LoadSpecial(recolor, ref TextureAssets.Chain26, ref FargowiltasSouls.FargowiltasSouls.TextureBuffer.Chain26, "Chain26");
      EModeNPCBehaviour.LoadSpecial(recolor, ref TextureAssets.Chain27, ref FargowiltasSouls.FargowiltasSouls.TextureBuffer.Chain27, "Chain27");
      EModeNPCBehaviour.LoadProjectile(recolor, 275);
      EModeNPCBehaviour.LoadProjectile(recolor, 276);
      EModeNPCBehaviour.LoadProjectile(recolor, 277);
    }
  }
}
