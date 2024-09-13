// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.WallofFleshEye
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class WallofFleshEye : EModeNPCBehaviour
  {
    public int PreventAttacks;
    public bool RepeatingAI;
    public bool HasTelegraphedNormalLasers;
    public bool TelegraphingLasers;
    public int TelegraphTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(114);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.PreventAttacks);
      bitWriter.WriteBit(this.RepeatingAI);
      bitWriter.WriteBit(this.HasTelegraphedNormalLasers);
      bitWriter.WriteBit(this.TelegraphingLasers);
      binaryWriter.Write7BitEncodedInt(this.TelegraphTimer);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.PreventAttacks = binaryReader.Read7BitEncodedInt();
      this.RepeatingAI = bitReader.ReadBit();
      this.HasTelegraphedNormalLasers = bitReader.ReadBit();
      this.TelegraphingLasers = bitReader.ReadBit();
      this.TelegraphTimer = binaryReader.Read7BitEncodedInt();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax = (int) Math.Round((double) npc.lifeMax * 2.2);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[24] = true;
      npc.buffImmune[323] = true;
      npc.buffImmune[ModContent.BuffType<ClippedWingsBuff>()] = true;
      npc.buffImmune[ModContent.BuffType<LethargicBuff>()] = true;
    }

    public override bool SafePreAI(NPC npc)
    {
      ref float local1 = ref npc.ai[1];
      ref float local2 = ref npc.ai[2];
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.realLife, new int[1]
      {
        113
      });
      if (WorldSavingSystem.SwarmActive || this.RepeatingAI || npc1 == null)
        return true;
      if (this.PreventAttacks > 0)
        --this.PreventAttacks;
      float num1 = 540f;
      if (this.TelegraphingLasers)
      {
        if (!this.HasTelegraphedNormalLasers && Main.netMode != 1)
          this.TelegraphTimer = 0;
        float num2 = npc.rotation + (((Entity) npc).direction > 0 ? 0.0f : 3.14159274f);
        Vector2 rotationVector2 = Utils.ToRotationVector2(num2);
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply((float) (((Entity) npc).width - 52), Utils.RotatedBy(Vector2.UnitX, (double) num2, new Vector2())));
        if ((double) this.TelegraphTimer < (double) npc.localAI[1])
          this.TelegraphTimer = (int) npc.localAI[1];
        float num3 = (float) Math.Cos(0.0024353431423176689 * (double) this.TelegraphTimer);
        Color drawColor = Color.op_Multiply(new Color((int) byte.MaxValue, 0, (int) byte.MaxValue, 100), (float) ((1.0 - (double) num3) / 4.0 + 0.75));
        int num4 = 2 + (int) Math.Ceiling((double) num3 * 6.0);
        if (num4 <= 0)
          num4 = 1;
        float num5 = 1.2566371f * num3;
        float num6 = (float) (6.0 + (1.0 - (double) num3) * 6.0);
        double num7 = (double) num5;
        Vector2 vector2_2 = Utils.RotatedByRandom(rotationVector2, num7);
        float num8 = (float) (25.0 + 30.0 * (double) num3);
        Vector2 vector2_3 = Vector2.op_Multiply(vector2_2, Utils.NextFloat(Main.rand, num8, num8 * 2f));
        Vector2 velocity = Vector2.op_Multiply(vector2_2, Utils.NextFloat(Main.rand, num6, num6 + 4f));
        if (this.TelegraphTimer % num4 == 0)
          new SparkParticle(Vector2.op_Addition(vector2_1, vector2_3), velocity, drawColor, Utils.NextFloat(Main.rand, 1.25f, 2f), 20).Spawn();
        if (--this.TelegraphTimer <= 0)
        {
          this.TelegraphTimer = 0;
          this.TelegraphingLasers = false;
        }
      }
      if (npc1.GetGlobalNPC<WallofFlesh>().InDesperationPhase)
      {
        if ((double) local1 < (double) num1 - 180.0)
          num1 = 240f;
        if (!WorldSavingSystem.MasochistModeReal)
        {
          npc.localAI[1] = -1f;
          npc.localAI[2] = 0.0f;
        }
      }
      ref float local3 = ref local1;
      float num9 = local1 + 1f;
      double num10 = (double) num9;
      local3 = (float) num10;
      if ((double) num9 >= (double) num1)
      {
        local1 = 0.0f;
        if ((double) local2 == 0.0)
          local2 = 1f;
        else
          local2 *= -1f;
        if ((double) local2 > 0.0)
        {
          Vector2 vector2 = Utils.RotatedBy(Vector2.UnitX, (double) npc.ai[3], new Vector2());
          if (FargoSoulsUtil.HostCheck && this.PreventAttacks <= 0)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<PhantasmalDeathrayWOF>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) npc).whoAmI, 0.0f);
        }
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if ((double) local2 >= 0.0)
      {
        npc.dontTakeDamage = true;
        if ((double) local1 <= 90.0)
        {
          this.RepeatingAI = true;
          npc.AI();
          this.RepeatingAI = false;
          npc.localAI[1] = -1f;
          npc.localAI[2] = 0.0f;
          npc.rotation = npc.ai[3];
          return false;
        }
        local2 = 1f;
      }
      else
      {
        npc.dontTakeDamage = false;
        if ((double) local1 == (double) num1 - 15.0 && FargoSoulsUtil.HostCheck && FargoSoulsUtil.HostCheck && this.PreventAttacks <= 0)
        {
          float num11 = npc.realLife == -1 || (double) ((Entity) Main.npc[npc.realLife]).velocity.X <= 0.0 ? 0.0f : 1f;
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<WOFBlast>(), 0, 0.0f, Main.myPlayer, num11, (float) ((Entity) npc).whoAmI, 0.0f);
        }
        if ((double) local1 > (double) num1 - 180.0)
        {
          float num12 = num1 - 90f;
          if ((double) local1 == (double) num12)
          {
            int index = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
            if (index != -1)
            {
              if ((double) ((Entity) npc).Distance(((Entity) Main.player[index]).Center) < 3000.0)
                SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) Main.player[index]).Center), (SoundUpdateCallback) null);
              local2 = -2f;
              npc.ai[3] = Utils.ToRotation(Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) Main.player[index]).Center));
              if (npc.realLife != -1 && (double) ((Entity) Main.npc[npc.realLife]).velocity.X > 0.0)
                npc.ai[3] += 3.14159274f;
              Vector2 vector2 = Utils.RotatedBy(Vector2.UnitX, (double) npc.ai[3], new Vector2());
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<PhantasmalDeathrayWOFS>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) npc).whoAmI, 0.0f);
            }
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
          }
          else if ((double) local1 > (double) num12)
          {
            this.HasTelegraphedNormalLasers = false;
            this.RepeatingAI = true;
            npc.AI();
            this.RepeatingAI = false;
            npc.localAI[1] = -1f;
            npc.localAI[2] = 0.0f;
            npc.rotation = npc.ai[3];
            return false;
          }
        }
      }
      if ((npc1.GetGlobalNPC<WallofFlesh>().InPhase2 && npc1.GetGlobalNPC<WallofFlesh>().WorldEvilAttackCycleTimer < 240 || npc1.GetGlobalNPC<WallofFlesh>().InDesperationPhase) && !WorldSavingSystem.MasochistModeReal)
      {
        npc.localAI[1] = -90f;
        npc.localAI[2] = 0.0f;
        this.HasTelegraphedNormalLasers = false;
      }
      if ((double) npc.localAI[2] > 1.0)
        this.HasTelegraphedNormalLasers = false;
      else if ((double) npc.localAI[1] >= 0.0 && !this.HasTelegraphedNormalLasers && npc.HasValidTarget)
      {
        this.HasTelegraphedNormalLasers = true;
        this.TelegraphingLasers = true;
      }
      return true;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(67, 300, true, false);
    }

    public virtual bool PreDraw(
      NPC npc,
      SpriteBatch spriteBatch,
      Vector2 screenPos,
      Color drawColor)
    {
      if (npc.dontTakeDamage && !npc.IsABestiaryIconDummy)
      {
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
        GameShaders.Armor.GetShaderFromItemId(3042).Apply((Entity) npc, new DrawData?());
      }
      return true;
    }

    public virtual void PostDraw(
      NPC npc,
      SpriteBatch spriteBatch,
      Vector2 screenPos,
      Color drawColor)
    {
      if (!npc.dontTakeDamage || npc.IsABestiaryIconDummy)
        return;
      spriteBatch.End();
      spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
    }
  }
}
