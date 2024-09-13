// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.CursedCoffin.CursedSpirit
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.CursedCoffin
{
  public class CursedSpirit : ModNPC
  {
    private int Frame;
    public static readonly Color GlowColor = new Color(224, 196, 252, 0);
    public int BiteTimer;
    public int BittenPlayer;
    private readonly List<float> SlowChargeStates;

    public virtual bool IsLoadingEnabled(Mod mod) => false;

    public ref float Owner => ref this.NPC.ai[0];

    public ref float Timer => ref this.NPC.ai[1];

    public ref float State => ref this.NPC.ai[2];

    public ref float AI3 => ref this.NPC.ai[3];

    public ref float StartupFadein => ref this.NPC.localAI[0];

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 9;
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 20;
      NPCID.Sets.TrailingMode[this.NPC.type] = 2;
      NPCID.Sets.MPAllowedEnemies[this.Type] = true;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 5);
      Span<int> span = CollectionsMarshal.AsSpan<int>(debuffs);
      int num1 = 0;
      span[num1] = 31;
      int num2 = num1 + 1;
      span[num2] = 46;
      int num3 = num2 + 1;
      span[num3] = 68;
      int num4 = num3 + 1;
      span[num4] = ModContent.BuffType<LethargicBuff>();
      int num5 = num4 + 1;
      span[num5] = ModContent.BuffType<ClippedWingsBuff>();
      int num6 = num5 + 1;
      npc.AddDebuffImmunities(debuffs);
    }

    public virtual void SetDefaults()
    {
      this.NPC.aiStyle = -1;
      this.NPC.lifeMax = 2222;
      this.NPC.defense = 10;
      this.NPC.damage = 35;
      this.NPC.knockBackResist = 0.0f;
      ((Entity) this.NPC).width = 110;
      ((Entity) this.NPC).height = 110;
      this.NPC.lavaImmune = true;
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit54);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath52);
      this.NPC.hide = true;
      this.NPC.value = (float) Item.buyPrice(0, 0, 0, 0);
      this.NPC.Opacity = 0.0f;
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * (double) balance);
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.NPC.localAI[0]);
      writer.Write(this.NPC.localAI[1]);
      writer.Write(this.NPC.localAI[2]);
      writer.Write(this.NPC.localAI[3]);
      writer.Write7BitEncodedInt(this.BiteTimer);
      writer.Write7BitEncodedInt(this.BittenPlayer);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.NPC.localAI[0] = reader.ReadSingle();
      this.NPC.localAI[1] = reader.ReadSingle();
      this.NPC.localAI[2] = reader.ReadSingle();
      this.NPC.localAI[3] = reader.ReadSingle();
      this.BiteTimer = reader.Read7BitEncodedInt();
      this.BittenPlayer = reader.Read7BitEncodedInt();
    }

    public virtual void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
      if (!this.SlowChargeStates.Contains(this.State))
        return;
      target.longInvince = true;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (!this.SlowChargeStates.Contains(this.State) || target.HasBuff<GrabbedBuff>())
        return;
      target.buffImmune[ModContent.BuffType<CoffinTossBuff>()] = true;
      this.BittenPlayer = ((Entity) target).whoAmI;
      this.BiteTimer = 360;
      if (Main.netMode == 2)
        NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) this.NPC).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      NPC npc = FargoSoulsUtil.NPCExists(this.Owner, ModContent.NPCType<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin>());
      if (!npc.TypeAlive<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin>())
        return;
      Luminance.Common.Utilities.Utilities.As<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin>(npc).ForceGrabPunish = 1f;
      npc.netUpdate = true;
      if (Main.netMode != 2)
        return;
      NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }

    public virtual bool CanHitPlayer(Player target, ref int cooldownSlot)
    {
      return (double) this.NPC.Opacity >= 1.0 && this.Collides(((Entity) target).position, ((Entity) target).Size);
    }

    public virtual bool CanHitNPC(NPC target)
    {
      return this.Collides(((Entity) target).position, ((Entity) target).Size);
    }

    public bool Collides(Vector2 boxPos, Vector2 boxDim)
    {
      Vector2 size = ((Entity) this.NPC).Size;
      Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).position, Vector2.op_Multiply(0.5f, new Vector2((float) ((Entity) this.NPC).width, (float) ((Entity) this.NPC).height)));
      float num1 = 0.0f;
      float num2 = 0.0f;
      if ((double) boxPos.X > (double) vector2.X)
        num1 = boxPos.X - vector2.X;
      else if ((double) boxPos.X + (double) boxDim.X < (double) vector2.X)
        num1 = boxPos.X + boxDim.X - vector2.X;
      if ((double) boxPos.Y > (double) vector2.Y)
        num2 = boxPos.Y - vector2.Y;
      else if ((double) boxPos.Y + (double) boxDim.Y < (double) vector2.Y)
        num2 = boxPos.Y + boxDim.Y - vector2.Y;
      float num3 = size.X / 2f;
      float num4 = size.Y / 2f;
      return (double) num1 * (double) num1 / ((double) num3 * (double) num3) + (double) num2 * (double) num2 / ((double) num4 * (double) num4) < 1.0;
    }

    public virtual bool CheckActive() => false;

    public virtual void OnKill()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Owner, ModContent.NPCType<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin>());
      if (!npc.TypeAlive<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin>() || !FargoSoulsUtil.HostCheck)
        return;
      npc.StrikeInstantKill();
    }

    public virtual void AI()
    {
      NPC npc1 = FargoSoulsUtil.NPCExists(this.Owner, ModContent.NPCType<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin>());
      if (!npc1.TypeAlive<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin>())
      {
        this.NPC.StrikeInstantKill();
      }
      else
      {
        if ((double) this.StartupFadein < 10.0)
        {
          ++this.StartupFadein;
          this.NPC.Opacity = 0.0f;
        }
        else if ((double) this.StartupFadein == 10.0)
        {
          this.NPC.Opacity = 1f;
          ++this.StartupFadein;
        }
        this.NPC.lifeMax = npc1.lifeMax = Math.Min(this.NPC.lifeMax, npc1.lifeMax);
        this.NPC.life = npc1.life = Math.Min(this.NPC.life, npc1.life);
        this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity) + 1.57079637f;
        this.NPC.dontTakeDamage = (double) this.NPC.scale < 0.5;
        if (!npc1.target.IsWithinBounds((int) byte.MaxValue))
          return;
        Player player1 = Main.player[npc1.target];
        if (player1 == null || !player1.Alive())
          return;
        FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin cursedCoffin = Luminance.Common.Utilities.Utilities.As<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin>(npc1);
        if (this.BittenPlayer != -1)
        {
          Player player2 = Main.player[this.BittenPlayer];
          if (this.BiteTimer > 0 && ((Entity) player2).active && !player2.ghost && !player2.dead && ((double) ((Entity) this.NPC).Distance(((Entity) player2).Center) < 160.0 || ((Entity) player2).whoAmI != Main.myPlayer) && player2.FargoSouls().MashCounter < 20)
          {
            player2.AddBuff(ModContent.BuffType<GrabbedBuff>(), 2, true, false);
            NPC npc2 = this.NPC;
            ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, 0.92f);
            ((Entity) player2).velocity = Vector2.Zero;
            ((Entity) player2).Center = Vector2.Lerp(((Entity) player2).Center, ((Entity) this.NPC).Center, 0.1f);
          }
          else
          {
            this.BittenPlayer = -1;
            this.BiteTimer = -90;
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.op_UnaryNegation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player2).Center)), 50f);
            this.NPC.netUpdate = true;
            if (Main.netMode != 2)
              return;
            NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) this.NPC).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
        }
        else
        {
          if (cursedCoffin.StateMachine.CurrentState == null)
            return;
          bool flag = (double) cursedCoffin.StateMachine.CurrentState.Identifier != (double) this.State;
          FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates identifier = cursedCoffin.StateMachine.CurrentState.Identifier;
          switch (identifier)
          {
            case FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.StunPunish:
              if (flag)
              {
                this.Timer = 0.0f;
                this.AI3 = 0.0f;
              }
              this.Movement(Vector2.op_Addition(((Entity) player1).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo(((Entity) player1).Center, ((Entity) this.NPC).Center), 300f)), 0.1f, 10f, decel: 0.08f, slowdown: 20f);
              break;
            case FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.HoveringForSlam:
              if (flag)
              {
                this.Timer = 0.0f;
                this.AI3 = 0.0f;
              }
              this.Artillery(npc1);
              break;
            case FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.SlamWShockwave:
              if (flag)
              {
                this.Timer = 0.0f;
                this.AI3 = 0.0f;
              }
              this.SlamSupport(npc1);
              break;
            default:
              if (!this.SlowChargeStates.Contains((float) cursedCoffin.StateMachine.CurrentState.Identifier))
              {
                if (identifier == FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.BehaviorStates.PhaseTransition)
                {
                  ((Entity) this.NPC).Center = ((Entity) npc1).Center;
                  this.NPC.scale = 0.2f;
                  break;
                }
                break;
              }
              if (flag)
              {
                this.Timer = 0.0f;
                this.AI3 = 0.0f;
              }
              this.SlowCharges(npc1);
              break;
          }
          this.State = (float) cursedCoffin.StateMachine.CurrentState.Identifier;
        }
      }
    }

    private void SlamSupport(NPC owner)
    {
      FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin cursedCoffin = Luminance.Common.Utilities.Utilities.As<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin>(owner);
      Player player = Main.player[owner.target];
      if ((double) this.AI3 == 0.0)
      {
        if ((double) cursedCoffin.Timer < 0.0 || (double) ((Entity) owner).velocity.Y == 0.0)
          this.AI3 = 1f;
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) owner).Center), Math.Min(Math.Max(20f, ((Vector2) ref ((Entity) owner).velocity).Length()), ((Entity) this.NPC).Distance(((Entity) owner).Center))), 0.2f);
        this.LerpOpacity(0.15f);
        this.LerpScale(0.4f);
      }
      else if ((double) this.AI3 == 1.0)
      {
        if ((double) ((Entity) this.NPC).Distance(((Entity) owner).Center) > 50.0)
        {
          for (int index = 0; index < 20; ++index)
            Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 27, Utils.NextFloat(Main.rand, -3f, 3f), Utils.NextFloat(Main.rand, -3f, 3f), 0, new Color(), 1f);
          ((Entity) this.NPC).Center = ((Entity) owner).Center;
          this.NPC.netUpdate = true;
        }
        this.LerpOpacity(1f, 0.4f);
        this.LerpScale(1f, 0.4f);
        this.AI3 = 2f;
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.UnitY, 7f);
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.SoulShotSFX, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (!FargoSoulsUtil.HostCheck)
          return;
        int num = WorldSavingSystem.EternityMode ? (WorldSavingSystem.MasochistModeReal ? 3 : 2) : 1;
        for (int index = -num; index <= num; ++index)
        {
          if (index != 0)
          {
            Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitY, (double) index * 6.2831854820251465 * (0.046999998390674591 + (double) Utils.NextFloat(Main.rand, 0.02f)), new Vector2()), (float) (6 + Math.Abs(index)));
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Bottom, ((Entity) this.NPC).velocity), vector2, ModContent.ProjectileType<CoffinDarkSouls>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 1f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -0.135f, 0.0f);
          }
        }
      }
      else
      {
        if ((double) cursedCoffin.Timer > 0.0)
          this.AI3 = 0.0f;
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.97f);
        this.LerpOpacity(1f, 0.4f);
        this.LerpScale(1f, 0.4f);
      }
    }

    private void SlowCharges(NPC owner)
    {
      this.LerpOpacity(1f, 0.4f);
      this.LerpScale(1f, 0.4f);
      Player player = Main.player[owner.target];
      if ((double) this.Timer <= 1.0)
      {
        this.AI3 = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center)) + Utils.NextFloat(Main.rand, -0.9424779f, 0.9424779f);
        this.NPC.netUpdate = true;
      }
      else if ((double) this.Timer < 80.0)
      {
        Vector2 vector2 = Vector2.Lerp(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, ((Entity) this.NPC).Center), Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) owner, ((Entity) player).Center), this.Timer / 140f);
        this.Movement(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 300f)), 0.2f, lowspeed: 10f, decel: 0.1f, slowdown: 20f);
      }
      else if ((double) this.Timer < 90.0)
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.94f);
      }
      else
      {
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.SpiritDroneSFX, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center);
        float num1 = 6.5f;
        if (!WorldSavingSystem.EternityMode)
          num1 /= 2f;
        else if (!WorldSavingSystem.MasochistModeReal)
          num1 /= 1.1f;
        float num2 = 10f;
        ((Vector2) ref vector2_1).Normalize();
        Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, num1);
        ((Entity) this.NPC).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.NPC).velocity, num2 - 1f), vector2_2), num2);
        if (Vector2.op_Equality(((Entity) this.NPC).velocity, Vector2.Zero))
        {
          ((Entity) this.NPC).velocity.X = -0.15f;
          ((Entity) this.NPC).velocity.Y = -0.05f;
        }
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() > 6.5)
        {
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.97f);
        }
        if ((double) this.Timer <= 130.0 && !WorldSavingSystem.MasochistModeReal)
        {
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, this.Timer / 130f);
        }
      }
      ++this.Timer;
    }

    private void Artillery(NPC owner)
    {
      if ((double) this.NPC.Opacity > 0.89999997615814209)
        this.NPC.Opacity = 0.9f;
      this.LerpOpacity(0.4f);
      this.LerpScale(0.6f);
      Vector2 pos = Vector2.op_Subtraction(((Entity) owner).Center, Vector2.op_Multiply(Vector2.UnitY, (float) ((Entity) owner).height));
      this.Movement(pos, 0.1f, Math.Max(25f, ((Vector2) ref ((Entity) owner).velocity).Length()), ((Vector2) ref ((Entity) owner).velocity).Length(), 0.08f, 20f);
      if ((double) ((Entity) this.NPC).Distance(pos) >= (double) ((Entity) owner).height * 0.75)
        return;
      if ((double) this.Timer % 20.0 == 19.0)
      {
        SoundEngine.PlaySound(ref FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin.SoulShotSFX, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2 = Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, 0.8796459436416626 * Math.Sin(6.2831854820251465 * ((double) this.Timer + (double) Main.rand.Next(20)) / 53.0), new Vector2())), 4f);
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<CoffinDarkSouls>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 1f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.18f, 0.0f);
        }
      }
      ++this.Timer;
    }

    private void GrabbyHands(NPC owner)
    {
      FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin cursedCoffin = Luminance.Common.Utilities.Utilities.As<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin>(owner);
      Player player = Main.player[owner.target];
      if ((double) cursedCoffin.Timer < 40.0)
      {
        Vector2 vector2 = Vector2.op_Subtraction(Vector2.op_Multiply(Vector2.op_UnaryNegation(Vector2.UnitY), 300f), Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) Math.Sign(((Entity) owner).Center.X - ((Entity) player).Center.X)), 200f));
        this.Movement(Vector2.op_Addition(((Entity) player).Center, vector2), 0.1f, 10f, decel: 0.08f, slowdown: 20f);
      }
      else
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.97f);
      }
      if ((double) cursedCoffin.Timer < 40.0)
      {
        this.LerpOpacity(0.15f);
        this.LerpScale(0.4f);
      }
      else
      {
        this.LerpOpacity(1f, 0.3f);
        this.LerpScale(1f, 0.3f);
      }
      if ((double) cursedCoffin.Timer != 40.0 || !FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.rotation + 1.57079637f), 4f), ModContent.ProjectileType<CoffinHand>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.1f), 1f, Main.myPlayer, (float) ((Entity) owner).whoAmI, 1f, 1f);
    }

    private void Movement(
      Vector2 pos,
      float accel = 0.03f,
      float maxSpeed = 20f,
      float lowspeed = 5f,
      float decel = 0.03f,
      float slowdown = 30f)
    {
      if ((double) ((Entity) this.NPC).Distance(pos) > (double) slowdown)
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Utils.SafeNormalize(Vector2.op_Subtraction(pos, ((Entity) this.NPC).Center), Vector2.Zero), maxSpeed), accel);
      else
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Utils.SafeNormalize(Vector2.op_Subtraction(pos, ((Entity) this.NPC).Center), Vector2.Zero), lowspeed), decel);
    }

    private void LerpOpacity(float target, float speed = 0.15f)
    {
      this.NPC.Opacity = (float) Utils.Lerp((double) this.NPC.Opacity, (double) target, (double) speed);
    }

    private void LerpScale(float target, float speed = 0.15f)
    {
      this.NPC.scale = (float) Utils.Lerp((double) this.NPC.scale, (double) target, (double) speed);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos);
      SpriteEffects spriteEffects1 = ((Entity) this.NPC).direction == 1 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      int num = NPCID.Sets.TrailCacheLength[this.NPC.type];
      if ((double) this.NPC.scale > 0.5)
        num /= 2;
      for (int index = 0; index < num; ++index)
      {
        Vector2 oldPo = this.NPC.oldPos[index];
        DrawData drawData;
        // ISSUE: explicit constructor call
        ((DrawData) ref drawData).\u002Ector(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(this.NPC.frame), Color.op_Multiply(this.NPC.GetAlpha(drawColor), 0.5f / (float) index), this.NPC.rotation, Vector2.op_Division(((Entity) this.NPC).Size, 2f), this.NPC.scale, spriteEffects1, 0.0f);
        GameShaders.Misc["LCWingShader"].UseColor(Color.Blue).UseSecondaryColor(Color.Black);
        GameShaders.Misc["LCWingShader"].Apply(new DrawData?(drawData));
        ((DrawData) ref drawData).Draw(spriteBatch);
      }
      SpriteBatch spriteBatch1 = spriteBatch;
      Vector2 vector2_2 = Vector2.op_Division(((Entity) this.NPC).Size, 2f);
      Texture2D texture2D2 = texture2D1;
      Vector2 vector2_3 = vector2_1;
      Rectangle? nullable = new Rectangle?(this.NPC.frame);
      Color alpha = this.NPC.GetAlpha(drawColor);
      double rotation = (double) this.NPC.rotation;
      Vector2 vector2_4 = vector2_2;
      double scale = (double) this.NPC.scale;
      SpriteEffects spriteEffects2 = spriteEffects1;
      spriteBatch1.Draw(texture2D2, vector2_3, nullable, alpha, (float) rotation, vector2_4, (float) scale, spriteEffects2, 0.0f);
      return false;
    }

    public virtual void DrawBehind(int index)
    {
      if (!this.NPC.hide)
        return;
      Main.instance.DrawCacheNPCsBehindNonSolidTiles.Add(index);
    }

    public virtual void FindFrame(int frameHeight)
    {
      if (++this.NPC.frameCounter > 4.0)
      {
        if (++this.Frame >= Main.npcFrameCount[this.Type] - 1)
          this.Frame = 0;
        this.NPC.frameCounter = 0.0;
      }
      this.NPC.spriteDirection = ((Entity) this.NPC).direction;
      this.NPC.frame.Y = frameHeight * this.Frame;
      this.NPC.frame.Width = 120;
      if (this.SlowChargeStates.Contains(this.State))
        this.NPC.frame.X = 120;
      else
        this.NPC.frame.X = 0;
    }

    public CursedSpirit()
    {
      List<float> floatList = new List<float>();
      CollectionsMarshal.SetCount<float>(floatList, 5);
      Span<float> span = CollectionsMarshal.AsSpan<float>(floatList);
      int num1 = 0;
      span[num1] = 1f;
      int num2 = num1 + 1;
      span[num2] = 6f;
      int num3 = num2 + 1;
      span[num3] = 7f;
      int num4 = num3 + 1;
      span[num4] = 9f;
      int num5 = num4 + 1;
      span[num5] = 8f;
      int num6 = num5 + 1;
      this.SlowChargeStates = floatList;
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }
  }
}
