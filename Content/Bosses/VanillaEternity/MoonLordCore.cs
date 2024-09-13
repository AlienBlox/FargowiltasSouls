// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.MoonLordCore
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Deathrays;
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
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class MoonLordCore : MoonLord
  {
    public int VulnerabilityState;
    public int AttackMemory;
    public float VulnerabilityTimer;
    public float AttackTimer;
    public bool EnteredPhase2;
    public bool SpawnedRituals;
    public bool DroppedSummon;
    public int SkyTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(398);

    public override int GetVulnerabilityState(NPC npc) => this.VulnerabilityState;

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.VulnerabilityState);
      binaryWriter.Write7BitEncodedInt(this.AttackMemory);
      binaryWriter.Write(this.VulnerabilityTimer);
      binaryWriter.Write(this.AttackTimer);
      bitWriter.WriteBit(this.EnteredPhase2);
      bitWriter.WriteBit(this.SpawnedRituals);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.VulnerabilityState = binaryReader.Read7BitEncodedInt();
      this.AttackMemory = binaryReader.Read7BitEncodedInt();
      this.VulnerabilityTimer = binaryReader.ReadSingle();
      this.AttackTimer = binaryReader.ReadSingle();
      this.EnteredPhase2 = bitReader.ReadBit();
      this.SpawnedRituals = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax *= 3;
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      EModeGlobalNPC.moonBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return flag;
      if (!this.SpawnedRituals)
      {
        this.SpawnedRituals = true;
        this.VulnerabilityState = 0;
        if (FargoSoulsUtil.HostCheck)
        {
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<LunarRitual>(), 25, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) npc).whoAmI, 0.0f);
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<FragmentRitual>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) npc).whoAmI, 0.0f);
        }
      }
      if (((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && this.VulnerabilityState >= 0 && this.VulnerabilityState <= 3)
        Main.LocalPlayer.AddBuff(ModContent.BuffType<NullificationCurseBuff>(), 2, true, false);
      if (!WorldSavingSystem.MasochistModeReal || !Main.getGoodWorld)
      {
        NPC npc1 = npc;
        ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, Vector2.op_Division(Vector2.op_Multiply(((Entity) npc).velocity, 2f), 3f));
      }
      if (npc.dontTakeDamage)
      {
        if ((double) this.AttackTimer == 370.0 && FargoSoulsUtil.HostCheck)
        {
          for (int index = 0; index < 3; ++index)
          {
            NPC npc2 = Main.npc[(int) npc.localAI[index]];
            if (((Entity) npc2).active)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc2).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc2).whoAmI, (float) npc2.type, 0.0f);
          }
        }
        if ((double) this.AttackTimer > 400.0)
        {
          this.AttackTimer = 0.0f;
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
          switch (this.VulnerabilityState)
          {
            case 0:
              for (int index1 = 0; index1 < 3; ++index1)
              {
                NPC npc3 = Main.npc[(int) npc.localAI[index1]];
                if (((Entity) npc3).active)
                {
                  int num = 30;
                  for (int index2 = -2; index2 <= 2; ++index2)
                  {
                    if (FargoSoulsUtil.HostCheck)
                      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc3).Center, Vector2.op_Multiply(6f, Utils.RotatedBy(((Entity) npc3).DirectionFrom(((Entity) Main.player[npc.target]).Center), Math.PI / 8.0 * (double) index2, new Vector2())), ModContent.ProjectileType<MoonLordFireball>(), num, 0.0f, Main.myPlayer, 20f, 80f, 0.0f);
                  }
                }
              }
              break;
            case 1:
              for (int index = 0; index < 6; ++index)
              {
                Vector2 vector2 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(500f, Utils.RotatedBy(((Entity) npc).DirectionFrom(((Entity) Main.player[npc.target]).Center), 1.0471975803375244 * ((double) index + 0.5), new Vector2())));
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2, Vector2.Zero, ModContent.ProjectileType<LightningVortexHostile>(), 30, 0.0f, Main.myPlayer, 1f, Utils.ToRotation(((Entity) Main.player[npc.target]).DirectionFrom(vector2)), 0.0f);
              }
              break;
            case 2:
              for (int index3 = 0; index3 < 3; ++index3)
              {
                NPC npc4 = Main.npc[(int) npc.localAI[index3]];
                if (((Entity) npc4).active && (index3 == 2 && npc4.type == 396 || npc4.type == 397))
                {
                  int num = 35;
                  for (int index4 = 0; index4 < 6; ++index4)
                  {
                    if (FargoSoulsUtil.HostCheck)
                    {
                      int index5 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc4).Center, Vector2.op_Multiply(2.5f, Utils.RotatedBy(((Entity) npc4).DirectionFrom(((Entity) Main.player[npc.target]).Center), Math.PI / 3.0 * ((double) index4 + 0.5), new Vector2())), ModContent.ProjectileType<MoonLordNebulaBlaze>(), num, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                      if (index5 != Main.maxProjectiles)
                        Main.projectile[index5].timeLeft = 1200;
                    }
                  }
                }
              }
              break;
            case 3:
              for (int index6 = 0; index6 < 3; ++index6)
              {
                NPC npc5 = Main.npc[(int) npc.localAI[index6]];
                if (((Entity) npc5).active && (index6 == 2 && npc5.type == 396 || npc5.type == 397))
                {
                  Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc5).Center);
                  ((Vector2) ref vector2_1).Normalize();
                  Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 5f);
                  for (int index7 = -1; index7 <= 1; ++index7)
                  {
                    Vector2 velocity = Utils.RotatedBy(vector2_2, (double) MathHelper.ToRadians(15f) * (double) index7, new Vector2());
                    FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc5).Center, 522, ai1: (float) (((double) Utils.NextFloat(Main.rand) - 0.5) * 0.30000001192092896 * 6.2831854820251465 / 60.0), ai2: velocity.X, ai3: velocity.Y, velocity: velocity);
                  }
                }
              }
              break;
            default:
              if (FargoSoulsUtil.HostCheck)
              {
                int num1 = 40;
                float num2 = 1.57079637f;
                Vector2 vector2 = Vector2.op_Multiply(Vector2.UnitY, 8f);
                int num3 = ModContent.ProjectileType<MutantSphereRing>();
                for (int index8 = 0; index8 < 4; ++index8)
                {
                  vector2 = Utils.RotatedBy(vector2, (double) num2, new Vector2());
                  if (FargoSoulsUtil.HostCheck)
                  {
                    int index9 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, num3, num1, 0.0f, Main.myPlayer, 0.5f, 8f, 0.0f);
                    if (index9 != Main.maxProjectiles)
                      Main.projectile[index9].timeLeft = 1800 - (int) this.VulnerabilityTimer;
                    int index10 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, num3, num1, 0.0f, Main.myPlayer, -0.5f, 8f, 0.0f);
                    if (index10 != Main.maxProjectiles)
                      Main.projectile[index10].timeLeft = 1800 - (int) this.VulnerabilityTimer;
                  }
                }
                SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
                break;
              }
              break;
          }
        }
      }
      else
      {
        if (!this.EnteredPhase2)
        {
          this.EnteredPhase2 = true;
          this.AttackTimer = 0.0f;
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
        }
        Player player = Main.player[npc.target];
        switch (this.VulnerabilityState)
        {
          case 0:
            if ((double) this.AttackTimer > 30.0)
            {
              this.AttackTimer -= 300f;
              this.AttackMemory = this.AttackMemory == 0 ? 1 : 0;
              float index = npc.localAI[this.AttackMemory];
              if (FargoSoulsUtil.HostCheck)
              {
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.npc[(int) index]).Center, Vector2.Zero, ModContent.ProjectileType<MoonLordSun>(), 60, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, index, 0.0f);
                break;
              }
              break;
            }
            break;
          case 1:
            if (this.AttackMemory == 0)
            {
              this.AttackMemory = 1;
              for (int index = -1; index <= 1; index += 2)
              {
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<MoonLordVortex>(), 40, 0.0f, Main.myPlayer, (float) index, (float) ((Entity) npc).whoAmI, 0.0f);
              }
              break;
            }
            break;
          case 2:
            if ((double) this.AttackTimer > 30.0)
            {
              this.AttackTimer -= 420f;
              for (int index11 = 0; index11 < 3; ++index11)
              {
                NPC npc6 = Main.npc[(int) npc.localAI[index11]];
                int num = 35;
                for (int index12 = -2; index12 <= 2; ++index12)
                {
                  if (FargoSoulsUtil.HostCheck)
                    Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc6).Center, Vector2.op_Multiply(2.5f, Utils.RotatedBy(((Entity) npc6).DirectionFrom(((Entity) Main.player[npc.target]).Center), Math.PI / 4.0 * ((double) index12 + (double) Utils.NextFloat(Main.rand, -0.25f, 0.25f)), new Vector2())), ModContent.ProjectileType<MoonLordNebulaBlaze2>(), num, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 0.0f, 0.0f);
                }
              }
              break;
            }
            break;
          case 3:
            if ((double) this.AttackTimer > 360.0)
            {
              this.AttackTimer -= 360f;
              this.AttackMemory = 0;
            }
            float radians = MathHelper.ToRadians(50f);
            if (++this.AttackMemory == 10)
            {
              if (FargoSoulsUtil.HostCheck)
              {
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.npc[(int) npc.localAI[0]]).Center, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) Main.npc[(int) npc.localAI[0]], ((Entity) player).Center), ModContent.ProjectileType<PhantasmalDeathrayMLSmall>(), 60, 0.0f, Main.myPlayer, radians * Utils.NextFloat(Main.rand, 0.9f, 1.1f), npc.localAI[0], 0.0f);
                break;
              }
              break;
            }
            if (this.AttackMemory == 20)
            {
              if (FargoSoulsUtil.HostCheck)
              {
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.npc[(int) npc.localAI[1]]).Center, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) Main.npc[(int) npc.localAI[2]], ((Entity) player).Center), ModContent.ProjectileType<PhantasmalDeathrayMLSmall>(), 60, 0.0f, Main.myPlayer, -radians * Utils.NextFloat(Main.rand, 0.9f, 1.1f), npc.localAI[1], 0.0f);
                break;
              }
              break;
            }
            if (this.AttackMemory == 30)
            {
              if (FargoSoulsUtil.HostCheck)
              {
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.npc[(int) npc.localAI[2]]).Center, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) Main.npc[(int) npc.localAI[1]], ((Entity) player).Center), ModContent.ProjectileType<PhantasmalDeathrayMLSmall>(), 60, 0.0f, Main.myPlayer, radians * Utils.NextFloat(Main.rand, 0.9f, 1.1f), npc.localAI[2], 0.0f);
                break;
              }
              break;
            }
            if (this.AttackMemory == 40 && FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) player).Center), ModContent.ProjectileType<PhantasmalDeathrayMLSmall>(), 60, 0.0f, Main.myPlayer, -radians * Utils.NextFloat(Main.rand, 0.9f, 1.1f), (float) ((Entity) npc).whoAmI, 0.0f);
              break;
            }
            break;
          default:
            if (this.AttackMemory == 0)
            {
              this.AttackMemory = 1;
              foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.hostile)))
              {
                if (projectile.type == ModContent.ProjectileType<LunarRitual>() && (double) projectile.ai[1] == (double) ((Entity) npc).whoAmI)
                {
                  if (FargoSoulsUtil.HostCheck)
                  {
                    for (int index = 0; index < 4; ++index)
                      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) projectile, ((Entity) Main.player[npc.target]).Center), 1.5707963705062866 * (double) index, new Vector2()), ModContent.ProjectileType<MoonLordMoon>(), 60, 0.0f, Main.myPlayer, (float) projectile.identity, 1450f, 0.0f);
                    for (int index = 0; index < 4; ++index)
                      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) projectile, ((Entity) Main.player[npc.target]).Center), 1.5707963705062866 * ((double) index + 0.5), new Vector2()), ModContent.ProjectileType<MoonLordMoon>(), 60, 0.0f, Main.myPlayer, (float) projectile.identity, -950f, 0.0f);
                    break;
                  }
                  break;
                }
              }
            }
            if (WorldSavingSystem.MasochistModeReal && (double) this.AttackTimer > 300.0)
            {
              this.AttackTimer -= 540f;
              if (FargoSoulsUtil.HostCheck)
              {
                int num4 = 40;
                float num5 = 0.7853982f;
                Vector2 vector2 = Vector2.op_Multiply(Vector2.UnitY, 8f);
                int num6 = ModContent.ProjectileType<MutantSphereRing>();
                for (int index = 0; index < 8; ++index)
                {
                  vector2 = Utils.RotatedBy(vector2, (double) num5, new Vector2());
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, num6, num4, 0.0f, Main.myPlayer, 0.5f, 8f, 0.0f);
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, num6, num4, 0.0f, Main.myPlayer, -0.5f, 8f, 0.0f);
                }
                SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
                break;
              }
              break;
            }
            break;
        }
      }
      if ((double) npc.ai[0] == 2.0)
      {
        this.VulnerabilityState = 4;
        this.VulnerabilityTimer = 0.0f;
        this.AttackTimer = 0.0f;
      }
      else
      {
        float num7 = (float) npc.life / (float) npc.lifeMax;
        if (WorldSavingSystem.MasochistModeReal)
          num7 = MathF.Pow(num7, 1.5f);
        float num8 = (float) (int) Math.Round((double) MathHelper.Lerp(3f, 1f, num7));
        this.VulnerabilityTimer += num8;
        this.AttackTimer += num8;
        if ((double) this.VulnerabilityTimer > 1800.0)
        {
          this.VulnerabilityState = ++this.VulnerabilityState % 5;
          this.VulnerabilityTimer = 0.0f;
          this.AttackTimer = 0.0f;
          this.AttackMemory = 0;
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
          if (WorldSavingSystem.MasochistModeReal)
          {
            switch (this.VulnerabilityState)
            {
              case 0:
                for (int index13 = 0; index13 < 3; ++index13)
                {
                  NPC npc7 = Main.npc[(int) npc.localAI[index13]];
                  if (((Entity) npc7).active && npc7.type == 396)
                  {
                    for (int index14 = -3; index14 <= 3; ++index14)
                      FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc7).Center, 519, target: npc.target, velocity: Vector2.op_Multiply(-10f, Utils.RotatedBy(Vector2.UnitY, (double) MathHelper.ToRadians((float) (20 * index14)), new Vector2())));
                  }
                }
                break;
              case 1:
                if (FargoSoulsUtil.HostCheck)
                {
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<MoonLordVortexOld>(), 40, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) npc).whoAmI, 0.0f);
                  break;
                }
                break;
              case 2:
                if (FargoSoulsUtil.HostCheck)
                {
                  int num9 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), 0, 0.0f, Main.myPlayer, 17f, (float) ((Entity) npc).whoAmI, 0.0f);
                  if (num9 != Main.maxProjectiles && Main.netMode == 2)
                  {
                    NetMessage.SendData(27, -1, -1, (NetworkText) null, num9, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                    break;
                  }
                  break;
                }
                break;
              case 3:
                for (int index15 = 0; index15 < 3; ++index15)
                {
                  NPC npc8 = Main.npc[(int) npc.localAI[index15]];
                  if (((Entity) npc8).active)
                  {
                    for (int index16 = -2; index16 <= 2; ++index16)
                    {
                      Vector2 velocity = Vector2.op_Multiply(9f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc8, ((Entity) Main.player[npc.target]).Center), 0.62831854820251465 * (double) index16, new Vector2()));
                      FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc8).Center, 522, ai1: (float) (((double) Utils.NextFloat(Main.rand) - 0.5) * 0.30000001192092896 * 6.2831854820251465 / 60.0), ai2: velocity.X, ai3: velocity.Y, target: npc.target, velocity: velocity);
                    }
                  }
                }
                break;
            }
          }
        }
      }
      GameModeData gameModeInfo = Main.GameModeInfo;
      if (((GameModeData) ref gameModeInfo).IsJourneyMode && ((CreativePowers.ASharedTogglePower) CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>()).Enabled)
        ((CreativePowers.ASharedTogglePower) CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>()).SetPowerInfo(false);
      if (!Main.dedServ && ++this.SkyTimer > 30 && NPC.FindFirstNPC(npc.type) == ((Entity) npc).whoAmI)
      {
        this.SkyTimer = 0;
        if (!((EffectManager<CustomSky>) SkyManager.Instance)["FargowiltasSouls:MoonLordSky"].IsActive())
          ((EffectManager<CustomSky>) SkyManager.Instance).Activate("FargowiltasSouls:MoonLordSky", new Vector2(), Array.Empty<object>());
        switch (this.VulnerabilityState)
        {
          case 0:
            HandleScene("Solar");
            break;
          case 1:
            HandleScene("Vortex");
            break;
          case 2:
            HandleScene("Nebula");
            break;
          case 3:
            HandleScene("Stardust");
            if ((double) this.VulnerabilityTimer < 120.0)
            {
              Main.LocalPlayer.Eternity().MasomodeMinionNerfTimer = 0;
              break;
            }
            break;
        }
      }
      EModeUtils.DropSummon(npc, "CelestialSigil2", NPC.downedMoonlord, ref this.DroppedSummon, NPC.downedAncientCultist);
      return flag;

      static void HandleScene(string name)
      {
        if (((EffectManager<Filter>) Filters.Scene)["FargowiltasSouls:" + name].IsActive())
          return;
        ((EffectManager<Filter>) Filters.Scene).Activate("FargowiltasSouls:" + name, new Vector2(), Array.Empty<object>());
      }
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 8);
      for (int type = 13; type <= 26; ++type)
      {
        if (type != 20)
          EModeNPCBehaviour.LoadExtra(recolor, type);
      }
      EModeNPCBehaviour.LoadExtra(recolor, 29);
    }
  }
}
