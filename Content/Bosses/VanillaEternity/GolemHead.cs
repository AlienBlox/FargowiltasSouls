// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.GolemHead
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Deathrays;
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
  public class GolemHead : GolemPart
  {
    public int AttackTimer;
    public int DeathraySweepTargetHeight;
    public float SuppressedAi1;
    public float SuppressedAi2;
    public bool DoAttack;
    public bool DoDeathray;
    public bool SweepToLeft;
    public bool IsInTemple;

    public GolemHead()
      : base(180)
    {
    }

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(246, 249);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.AttackTimer);
      binaryWriter.Write7BitEncodedInt(this.DeathraySweepTargetHeight);
      binaryWriter.Write(this.SuppressedAi1);
      binaryWriter.Write(this.SuppressedAi2);
      bitWriter.WriteBit(this.DoAttack);
      bitWriter.WriteBit(this.DoDeathray);
      bitWriter.WriteBit(this.SweepToLeft);
      bitWriter.WriteBit(this.IsInTemple);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.AttackTimer = binaryReader.Read7BitEncodedInt();
      this.DeathraySweepTargetHeight = binaryReader.Read7BitEncodedInt();
      this.SuppressedAi1 = binaryReader.ReadSingle();
      this.SuppressedAi2 = binaryReader.ReadSingle();
      this.DoAttack = bitReader.ReadBit();
      this.DoDeathray = bitReader.ReadBit();
      this.SweepToLeft = bitReader.ReadBit();
      this.IsInTemple = bitReader.ReadBit();
    }

    public override void SetDefaults(NPC npc)
    {
      base.SetDefaults(npc);
      npc.lifeMax = (int) ((double) npc.lifeMax * 0.64999997615814209);
      this.AttackTimer = 540;
      this.DoDeathray = true;
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot)
    {
      return base.CanHitPlayer(npc, target, ref CooldownSlot) && npc.type != 249;
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag1 = base.SafePreAI(npc);
      if (npc.damage < 165)
        npc.damage = 165;
      if (WorldSavingSystem.SwarmActive)
        return flag1;
      NPC npc1 = FargoSoulsUtil.NPCExists(NPC.golemBoss, new int[1]
      {
        245
      });
      if (npc.type == 246)
      {
        if (npc1 != null)
        {
          NPC npc2 = npc;
          ((Entity) npc2).position = Vector2.op_Addition(((Entity) npc2).position, ((Entity) npc1).velocity);
        }
      }
      else if (!this.DoAttack)
      {
        NPC npc3 = npc;
        ((Entity) npc3).position = Vector2.op_Addition(((Entity) npc3).position, Vector2.op_Multiply(((Entity) npc).velocity, 0.25f));
        ((Entity) npc).position.Y += ((Entity) npc).velocity.Y * 0.25f;
        if (!npc.noTileCollide && npc.HasValidTarget && Collision.SolidCollision(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height))
        {
          NPC npc4 = npc;
          ((Entity) npc4).position = Vector2.op_Addition(((Entity) npc4).position, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), 4f));
        }
        if (npc.HasValidTarget && (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) < 350.0 && !WorldSavingSystem.MasochistModeReal)
        {
          if ((double) this.SuppressedAi1 < (double) npc.ai[1])
            this.SuppressedAi1 = npc.ai[1];
          npc.ai[1] = 0.0f;
          if ((double) this.SuppressedAi2 < (double) npc.ai[2])
            this.SuppressedAi2 = npc.ai[2];
          npc.ai[2] = 0.0f;
        }
        else
        {
          if ((double) npc.ai[1] < (double) this.SuppressedAi1)
            npc.ai[1] = this.SuppressedAi1;
          this.SuppressedAi1 = 0.0f;
          if ((double) npc.ai[2] < (double) this.SuppressedAi2)
            npc.ai[2] = this.SuppressedAi2;
          this.SuppressedAi2 = 0.0f;
          if (!this.DoDeathray && this.AttackTimer % 120 > 90)
          {
            npc.ai[1] += 90f;
            npc.ai[2] += 90f;
          }
        }
        if (++this.AttackTimer > 540)
        {
          this.AttackTimer = 0;
          this.DeathraySweepTargetHeight = 0;
          this.DoAttack = true;
          this.IsInTemple = Golem.CheckTempleWalls(((Entity) npc).Center);
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
        }
      }
      else
      {
        if (npc1 == null)
        {
          npc.life = 0;
          npc.HitEffect(0, 10.0, new bool?());
          npc.checkDead();
          return false;
        }
        npc.noTileCollide = true;
        npc.localAI[0] = this.AttackTimer > 120 ? 1f : 0.0f;
        bool flag2 = !this.DoDeathray;
        if (WorldSavingSystem.MasochistModeReal || !this.IsInTemple)
        {
          this.DoDeathray = true;
          flag2 = true;
        }
        if (++this.AttackTimer < 120)
        {
          if (this.AttackTimer == 1)
          {
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            if (this.DoDeathray && FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 222f, 0.0f);
          }
          Vector2 center = ((Entity) npc1).Center;
          center.Y -= 250f;
          if ((double) center.Y > (double) this.DeathraySweepTargetHeight)
            this.DeathraySweepTargetHeight = (int) center.Y;
          center.Y = (float) this.DeathraySweepTargetHeight;
          if (npc.HasPlayerTarget && (double) ((Entity) Main.player[npc.target]).position.Y < (double) center.Y)
            center.Y = ((Entity) Main.player[npc.target]).position.Y;
          ((Entity) npc).velocity = Vector2.op_Division(Vector2.op_Subtraction(center, ((Entity) npc).Center), 30f);
        }
        else if (this.AttackTimer == 120)
        {
          ((Entity) npc).velocity = Vector2.Zero;
          if (npc.HasPlayerTarget)
            this.SweepToLeft = (double) ((Entity) Main.player[npc.target]).Center.X < (double) ((Entity) npc).Center.X;
          npc.netUpdate = true;
          if (FargoSoulsUtil.HostCheck)
          {
            if (this.DoDeathray)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.UnitY, ModContent.ProjectileType<GolemBeam>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 1.5f), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) npc).whoAmI, 0.0f);
            if (flag2)
            {
              SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
              for (int index1 = -3; index1 <= 3; ++index1)
              {
                Vector2 vector2 = Vector2.op_Multiply(6f, Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, 0.52359879016876221 * ((double) index1 + (double) Utils.NextFloat(Main.rand, 0.25f, 0.75f) * (Utils.NextBool(Main.rand) ? -1.0 : 1.0)), new Vector2())));
                int index2 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<GolemSpikeBallBig>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                if (index2 != Main.maxProjectiles)
                  Main.projectile[index2].timeLeft -= Main.rand.Next(60);
              }
            }
          }
        }
        else if (this.AttackTimer >= 140)
        {
          if (this.AttackTimer < 270 && this.DoDeathray)
          {
            ((Entity) npc).velocity.X += this.SweepToLeft ? -0.15f : 0.15f;
            bool flag3 = Golem.CheckTempleWalls(((Entity) npc).Center);
            Tile tileSafely = Framing.GetTileSafely(((Entity) npc).Center);
            if (((this.AttackTimer <= 180 || !((Tile) ref tileSafely).HasUnactuatedTile ? 0 : (((Tile) ref tileSafely).TileType == (ushort) 226 ? 1 : 0)) & (flag3 ? 1 : 0)) != 0 || this.IsInTemple && !flag3)
            {
              ((Entity) npc).velocity = Vector2.Zero;
              npc.netUpdate = true;
              this.AttackTimer = 0;
              this.DeathraySweepTargetHeight = 0;
              this.DoAttack = false;
            }
          }
          else
          {
            ((Entity) npc).velocity = Vector2.Zero;
            npc.netUpdate = true;
            this.AttackTimer = 0;
            this.DeathraySweepTargetHeight = 0;
            this.DoAttack = false;
          }
        }
        if (!WorldSavingSystem.MasochistModeReal)
        {
          if ((double) this.AttackTimer % 100.0 == 95.0)
          {
            Vector2 center = ((Entity) npc1).Center;
            float num1 = (double) this.AttackTimer % 200.0 == 95.0 ? 0.0f : 0.5f;
            for (int index = -3; index <= 3; ++index)
            {
              int num2 = (int) ((double) center.X / 16.0 + (double) ((Entity) npc1).width * ((double) index + (double) num1) * 3.0 / 16.0);
              int num3 = (int) center.Y / 16;
              int num4 = this.IsInTemple ? ModContent.ProjectileType<GolemGeyser>() : ModContent.ProjectileType<GolemGeyser2>();
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), (float) (num2 * 16 + 8), (float) (num3 * 16 + 8), 0.0f, 0.0f, num4, FargoSoulsUtil.ScaledProjectileDamage(npc1.damage), 0.0f, Main.myPlayer, (float) ((Entity) npc1).whoAmI, 0.0f, 0.0f);
            }
          }
          if (this.IsInTemple && npc1.HasValidTarget)
          {
            if ((double) npc1.ai[0] == 0.0 && (double) ((Entity) npc1).velocity.Y == 0.0 && (double) npc1.ai[1] > 1.0)
              npc1.ai[1] = 1f;
            npc1.GetGlobalNPC<Golem>().DoStompBehaviour = false;
          }
        }
        if (!this.DoAttack)
        {
          this.DoDeathray = !this.DoDeathray;
          if (FargoSoulsUtil.HostCheck)
          {
            int num5 = this.IsInTemple ? 6 : 10;
            int num6 = this.IsInTemple ? 6 : -12;
            for (int index3 = -num5; index3 <= num5; ++index3)
            {
              int index4 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply((float) num6, Utils.RotatedBy(Vector2.UnitY, Math.PI / 2.0 / (double) num5 * (double) index3, new Vector2())), ModContent.ProjectileType<EyeBeam2>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              if (index4 != Main.maxProjectiles)
                Main.projectile[index4].timeLeft = 1200;
            }
          }
        }
        if (npc.netUpdate)
        {
          npc.netUpdate = false;
          if (Main.netMode == 2)
            NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          EModeNPCBehaviour.NetSync(npc);
        }
        return false;
      }
      return flag1;
    }
  }
}
