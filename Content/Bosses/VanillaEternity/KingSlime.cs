// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.KingSlime
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class KingSlime : EModeNPCBehaviour
  {
    public int SpikeRainCounter;
    public bool IsBerserk;
    public bool LandingAttackReady;
    public bool CurrentlyJumping;
    public bool DidSpecialTeleport;
    public int CertainAttackCooldown;
    public bool DroppedSummon;
    public float JumpTimer;
    private const int SpecialJumpTime = 900;
    private const int SummonWaves = 6;
    public float SummonCounter = 5f;
    public bool SpecialJumping;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(50);

    public override bool SafePreAI(NPC npc)
    {
      EModeGlobalNPC.slimeBoss = ((Entity) npc).whoAmI;
      npc.color = Color.op_Multiply(Main.DiscoColor, 0.3f);
      ref float local = ref npc.ai[2];
      if (WorldSavingSystem.SwarmActive)
        return true;
      if (this.CertainAttackCooldown > 0)
        --this.CertainAttackCooldown;
      Player player = Main.player[npc.target];
      if ((double) local >= 145.0 && (double) local < 150.0)
      {
        if ((double) this.JumpTimer < 900.0)
          this.JumpTimer = 900f;
        local = 145f;
      }
      if ((double) npc.GetLifePercent() < (double) this.SummonCounter / 6.0 && (this.CertainAttackCooldown <= 0 || WorldSavingSystem.MasochistModeReal))
      {
        this.CertainAttackCooldown = 180;
        if (FargoSoulsUtil.HostCheck)
        {
          for (int index1 = 0; index1 < 6; ++index1)
          {
            int num1 = (int) ((double) ((Entity) npc).position.X + (double) Main.rand.Next(((Entity) npc).width - 32));
            int num2 = (int) ((double) ((Entity) npc).position.Y + (double) Main.rand.Next(((Entity) npc).height - 32));
            int num3 = ModContent.NPCType<SlimeSwarm>();
            int index2 = NPC.NewNPC(((Entity) npc).GetSource_FromThis((string) null), num1, num2, num3, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
            if (index2.IsWithinBounds(Main.maxNPCs))
            {
              Main.npc[index2].SetDefaults(num3, new NPCSpawnParams());
              ((Entity) Main.npc[index2]).velocity.X = (float) Main.rand.Next(-15, 16) * 0.1f;
              ((Entity) Main.npc[index2]).velocity.Y = (float) Main.rand.Next(-30, 1) * 0.1f;
              if (npc.HasValidTarget)
                Main.npc[index2].ai[0] = (float) Math.Sign(((Entity) player).Center.X - ((Entity) npc).Center.X);
              if (Main.netMode == 2)
                NetMessage.SendData(23, -1, -1, (NetworkText) null, index2, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
          }
        }
        SoundEngine.PlaySound(ref SoundID.Item167, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        --this.SummonCounter;
      }
      if (WorldSavingSystem.MasochistModeReal)
        ((Entity) npc).position.X += ((Entity) npc).velocity.X * 0.2f;
      if (this.LandingAttackReady)
      {
        if ((double) ((Entity) npc).velocity.Y == 0.0)
        {
          this.LandingAttackReady = false;
          if ((double) this.JumpTimer >= 900.0 && !this.SpecialJumping && (this.CertainAttackCooldown <= 0 || WorldSavingSystem.MasochistModeReal))
          {
            SoundStyle soundStyle = SoundID.Item21;
            ((SoundStyle) ref soundStyle).Pitch = -1f;
            ((SoundStyle) ref soundStyle).Volume = 1.5f;
            SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            ExpandingBloomParticle expandingBloomParticle = new ExpandingBloomParticle(((Entity) npc).Center, Vector2.Zero, Color.Blue, Vector2.One, Vector2.op_Multiply(Vector2.One, 60f), 40, true, new Color?(Color.Transparent));
            this.SpecialJumping = true;
            this.CertainAttackCooldown = 240;
            expandingBloomParticle.Spawn();
          }
          else if (this.SpecialJumping)
          {
            this.JumpTimer = 0.0f;
            this.SpecialJumping = false;
            local = 150f;
          }
          else if (!FargoSoulsUtil.HostCheck)
            ;
        }
      }
      else if ((double) ((Entity) npc).velocity.Y > 0.0)
        this.LandingAttackReady = true;
      if ((double) ((Entity) npc).velocity.Y < 0.0)
      {
        if (!this.CurrentlyJumping)
        {
          this.CurrentlyJumping = true;
          if (this.SpecialJumping)
          {
            ((Entity) npc).velocity.Y = -18f;
            int num4 = Math.Sign(((Entity) player).Center.X - ((Entity) npc).Center.X);
            int num5 = 1000;
            Vector2 vector2 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) num5), (float) num4));
            float num6 = Math.Abs(2f * ((Entity) npc).velocity.Y / npc.gravity);
            ((Entity) npc).velocity.X = (vector2.X - ((Entity) npc).Center.X) / num6;
          }
          else
          {
            bool flag = false;
            if (WorldSavingSystem.MasochistModeReal)
              flag = true;
            if (npc.HasValidTarget)
            {
              if ((double) ((Entity) player).Center.Y < (double) ((Entity) npc).position.Y + (double) ((Entity) npc).height - 240.0)
                ((Entity) npc).velocity.Y *= 1.5f;
              float num7 = Math.Abs(((Entity) player).Center.X - ((Entity) npc).Center.X);
              if ((double) num7 > 0.0)
              {
                float num8 = (num7 - 0.0f) / 700f;
                float num9 = MathHelper.Clamp(num8 * num8 + 1f, 1f, 3f);
                ((Entity) npc).velocity.X *= num9;
                ((Entity) npc).velocity.Y *= Math.Min((float) Math.Cbrt((double) num9), 1.5f);
                ((Entity) npc).velocity.X += (float) Math.Sign(((Entity) npc).velocity.X) * 2.25f;
              }
            }
            if (flag && FargoSoulsUtil.HostCheck)
            {
              float num = 90f;
              Vector2 vector2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) player).Center, ((Entity) npc).Center), Vector2.op_Multiply(((Entity) player).velocity, 30f));
              vector2.X /= num;
              vector2.Y = (float) ((double) vector2.Y / (double) num - 0.075000002980232239 * (double) num);
              for (int index = 0; index < 15; ++index)
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Addition(vector2, Utils.NextVector2Square(Main.rand, -1f, 1f)), ModContent.ProjectileType<SlimeSpike>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
        }
      }
      else
        this.CurrentlyJumping = false;
      if ((double) ((Entity) npc).velocity.Y != 0.0 && this.SpecialJumping)
      {
        ++this.JumpTimer;
        if (Math.Sign(((Entity) npc).velocity.X) != Math.Sign(((Entity) npc).DirectionTo(((Entity) player).Center).X) && (double) Math.Abs(((Entity) npc).Center.X - ((Entity) player).Center.X) > 250.0 && (double) ((Entity) npc).velocity.Y > 0.0)
        {
          ((Entity) npc).velocity.X /= 5f;
          this.SpecialJumping = false;
          this.JumpTimer = 0.0f;
          local = 150f;
        }
        else if ((double) this.JumpTimer % 5.0 < 1.0 && ((double) this.JumpTimer % 15.0 > 1.0 || WorldSavingSystem.MasochistModeReal))
        {
          SoundEngine.PlaySound(ref SoundID.Item17, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 bottom = ((Entity) npc).Bottom;
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), bottom, Vector2.Zero, ModContent.ProjectileType<SlimeSpike2>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.6666667f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      if ((this.IsBerserk || (double) npc.life < (double) npc.lifeMax * 0.6600000262260437) && npc.HasValidTarget && !this.SpecialJumping && --this.SpikeRainCounter < 0)
      {
        this.SpikeRainCounter = 240;
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2_1 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Vector2.UnitX, (float) Main.rand.Next(-55, 55)));
          for (int index = -12; index <= 12; ++index)
          {
            Vector2 vector2_2 = vector2_1;
            vector2_2.X += (float) (110 * index);
            vector2_2.Y -= 500f;
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_2, Vector2.op_Multiply(this.IsBerserk ? 6f : 0.0f, Vector2.UnitY), ModContent.ProjectileType<SlimeSpike2>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.6666667f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      if ((double) npc.ai[1] == 5.0)
      {
        if (npc.HasPlayerTarget && (double) npc.ai[0] == 1.0)
          npc.localAI[2] = ((Entity) player).Center.Y;
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(npc.localAI[1], npc.localAI[2]);
        vector2.X -= (float) (((Entity) npc).width / 2);
        for (int index3 = 0; index3 < 10; ++index3)
        {
          int index4 = Dust.NewDust(vector2, ((Entity) npc).width, ((Entity) npc).height / 2, 4, 0.0f, 0.0f, 75, new Color(78, 136, (int) byte.MaxValue, 80), 2.5f);
          Main.dust[index4].noGravity = true;
          Main.dust[index4].velocity.Y -= 3f;
          Dust dust = Main.dust[index4];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
        }
      }
      EModeUtils.DropSummon(npc, "SlimyCrown", NPC.downedSlimeKing, ref this.DroppedSummon);
      return base.SafePreAI(npc);
    }

    public virtual bool? CanFallThroughPlatforms(NPC npc)
    {
      return this.SpecialJumping && !this.LandingAttackReady ? new bool?(false) : base.CanFallThroughPlatforms(npc);
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      ModNPC modNpc;
      if (!FargoSoulsUtil.HostCheck || FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()) || !ModContent.TryFind<ModNPC>("Fargowiltas", "Mutant", ref modNpc) || NPC.AnyNPCs(modNpc.Type))
        return;
      int num = NPC.NewNPC(((Entity) npc).GetSource_FromThis((string) null), (int) ((Entity) npc).Center.X, (int) ((Entity) npc).Center.Y, modNpc.Type, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
      if (num == Main.maxNPCs || Main.netMode != 2)
        return;
      NetMessage.SendData(23, -1, -1, (NetworkText) null, num, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(137, 60, true, false);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 7);
      EModeNPCBehaviour.LoadGore(recolor, 734);
      EModeNPCBehaviour.LoadExtra(recolor, 39);
      EModeNPCBehaviour.LoadSpecial(recolor, ref TextureAssets.Ninja, ref FargowiltasSouls.FargowiltasSouls.TextureBuffer.Ninja, "Ninja");
    }
  }
}
