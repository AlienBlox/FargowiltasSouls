// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.WallofFlesh
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class WallofFlesh : EModeNPCBehaviour
  {
    public int WorldEvilAttackCycleTimer = 600;
    public int ChainBarrageTimer;
    public int TongueTimer;
    public bool UseCorruptAttack;
    public bool InPhase2;
    public bool InPhase3;
    public bool InDesperationPhase;
    public bool MadeEyeInvul;
    public bool DroppedSummon;
    public bool DidGrowl;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(113);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.WorldEvilAttackCycleTimer);
      binaryWriter.Write7BitEncodedInt(this.ChainBarrageTimer);
      bitWriter.WriteBit(this.UseCorruptAttack);
      bitWriter.WriteBit(this.InPhase2);
      bitWriter.WriteBit(this.InPhase3);
      bitWriter.WriteBit(this.InDesperationPhase);
      bitWriter.WriteBit(this.MadeEyeInvul);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.WorldEvilAttackCycleTimer = binaryReader.Read7BitEncodedInt();
      this.ChainBarrageTimer = binaryReader.Read7BitEncodedInt();
      this.UseCorruptAttack = bitReader.ReadBit();
      this.InPhase2 = bitReader.ReadBit();
      this.InPhase3 = bitReader.ReadBit();
      this.InDesperationPhase = bitReader.ReadBit();
      this.MadeEyeInvul = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax = (int) Math.Round((double) npc.lifeMax * 2.2);
      npc.defense = 0;
      npc.HitSound = new SoundStyle?(SoundID.NPCHit41);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[24] = true;
      npc.buffImmune[323] = true;
      if (!FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 13f, (float) ((Entity) npc).whoAmI, 0.0f);
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      EModeGlobalNPC.wallBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return flag;
      if (!this.MadeEyeInvul && (double) npc.ai[3] == 0.0)
      {
        for (int index = 0; index < Main.maxNPCs; ++index)
        {
          if (((Entity) Main.npc[index]).active && Main.npc[index].type == 114 && Main.npc[index].realLife == ((Entity) npc).whoAmI)
          {
            Main.npc[index].ai[2] = -1f;
            Main.npc[index].netUpdate = true;
            this.MadeEyeInvul = true;
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
            break;
          }
        }
      }
      SoundStyle soundStyle;
      if (this.InPhase2)
      {
        if (++this.WorldEvilAttackCycleTimer > 600)
        {
          this.WorldEvilAttackCycleTimer = 0;
          this.UseCorruptAttack = !this.UseCorruptAttack;
          this.DidGrowl = false;
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
        }
        else if (this.WorldEvilAttackCycleTimer > (this.InDesperationPhase ? 300 : 480))
        {
          if (!this.DidGrowl)
          {
            this.DidGrowl = true;
            if (!Main.dedServ)
            {
              soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Monster119", (SoundType) 0);
              SoundEngine.PlaySound(ref soundStyle, new Vector2?(!npc.HasValidTarget || !Main.player[npc.target].ZoneUnderworldHeight ? ((Entity) npc).Center : ((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
            }
          }
          for (int index = 0; index < 2; ++index)
          {
            int num1 = this.UseCorruptAttack ? 1 : 0;
            Color drawColor = !this.UseCorruptAttack ? new Color(96, 248, 2) : Color.Gold;
            int num2 = !this.UseCorruptAttack ? 10 : 8;
            float num3 = !this.UseCorruptAttack ? 6f : 4f;
            int num4 = this.UseCorruptAttack ? 1 : 0;
            Vector2 vector2_1 = Utils.RotatedByRandom(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), 0.31415927410125732);
            Vector2 vector2_2 = Vector2.op_Multiply(Vector2.op_Multiply((float) num2, vector2_1), Utils.NextFloat(Main.rand, 0.4f, 0.8f));
            ExpandingBloomParticle expandingBloomParticle = new ExpandingBloomParticle(Vector2.op_Addition(Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(32f, vector2_1)), Vector2.op_Multiply(vector2_2, 50f)), Vector2.op_Division(Vector2.op_UnaryNegation(vector2_2), 2f), drawColor, Vector2.Zero, Vector2.op_Multiply(Vector2.One, num3), 25);
            expandingBloomParticle.Velocity = Vector2.op_Multiply(expandingBloomParticle.Velocity, 2f);
            expandingBloomParticle.Spawn();
          }
        }
        else if (this.WorldEvilAttackCycleTimer < 240)
        {
          if (this.UseCorruptAttack)
          {
            if (this.WorldEvilAttackCycleTimer == 10 && FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.UnitY, ModContent.ProjectileType<CursedDeathrayWOFS>(), 0, 0.0f, Main.myPlayer, (float) Math.Sign(((Entity) npc).velocity.X), (float) ((Entity) npc).whoAmI, 0.0f);
            if (this.WorldEvilAttackCycleTimer % 4 == 0)
            {
              float num = (float) (2500.0 - 1800.0 * (double) this.WorldEvilAttackCycleTimer / 240.0) * (float) Math.Sign(((Entity) npc).velocity.X);
              Vector2 vector2;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2).\u002Ector(((Entity) npc).Center.X + num, ((Entity) npc).Center.Y);
              SoundEngine.PlaySound(ref SoundID.Item34, new Vector2?(vector2), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
              {
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(vector2, Vector2.op_Multiply(Vector2.UnitY, 800f)), Vector2.op_Multiply(Vector2.UnitY, -14f), ModContent.ProjectileType<CursedFlamethrower>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(vector2, Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitY, 800f), 2f)), Vector2.op_Multiply(Vector2.UnitY, 14f), ModContent.ProjectileType<CursedFlamethrower>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(vector2, Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitY, -800f), 2f)), Vector2.op_Multiply(Vector2.UnitY, -14f), ModContent.ProjectileType<CursedFlamethrower>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(vector2, Vector2.op_Multiply(Vector2.UnitY, -800f)), Vector2.op_Multiply(Vector2.UnitY, 14f), ModContent.ProjectileType<CursedFlamethrower>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
          }
          else if (this.WorldEvilAttackCycleTimer % 8 == 0 && FargoSoulsUtil.HostCheck)
          {
            int num5 = WorldSavingSystem.MasochistModeReal ? 12 : 8;
            int num6 = 1;
            for (int index = 0; index < num5; ++index)
            {
              if (WorldSavingSystem.MasochistModeReal)
                num6 *= -1;
              Vector2 center = ((Entity) npc).Center;
              center.X += (float) ((double) Math.Sign(((Entity) npc).velocity.X) * 1000.0 * (double) this.WorldEvilAttackCycleTimer / 240.0);
              center.X += Utils.NextFloat(Main.rand, -100f, 100f);
              center.Y += Utils.NextFloat(Main.rand, -450f, 450f);
              float num7 = 60f;
              Vector2 vector2 = Vector2.op_Subtraction(center, ((Entity) npc).Center);
              vector2.X /= num7;
              vector2.Y = (float) ((double) vector2.Y / (double) num7 - 0.25 * (double) num7);
              vector2.Y *= (float) num6;
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) Math.Sign(((Entity) npc).velocity.X)), 32f)), vector2, ModContent.ProjectileType<GoldenShowerWOF>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.75f), 0.0f, Main.myPlayer, num7, 0.0f, (float) num6);
            }
          }
        }
      }
      else if ((double) npc.life < (double) npc.lifeMax * (WorldSavingSystem.MasochistModeReal ? 0.9 : 0.75))
      {
        this.InPhase2 = true;
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
        if (!Main.dedServ)
        {
          soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Monster94", (SoundType) 0);
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(!npc.HasValidTarget || !Main.player[npc.target].ZoneUnderworldHeight ? ((Entity) npc).Center : ((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
          if (((Entity) Main.LocalPlayer).active)
            ScreenShakeSystem.StartShake(15f, 6.28318548f, new Vector2?(), 0.166666672f);
        }
      }
      if (this.InPhase3)
      {
        if (this.InDesperationPhase && this.WorldEvilAttackCycleTimer % 2 == 1)
          --this.WorldEvilAttackCycleTimer;
        int num8 = 240 - (this.InDesperationPhase ? 120 : 0);
        int num9 = 420 - (this.InDesperationPhase ? 120 : 0);
        if (this.WorldEvilAttackCycleTimer >= num8 && this.WorldEvilAttackCycleTimer <= num9)
        {
          if (--this.ChainBarrageTimer < 0)
          {
            this.ChainBarrageTimer = 80;
            if (npc.HasValidTarget && Main.player[npc.target].ZoneUnderworldHeight && FargoSoulsUtil.HostCheck)
            {
              Vector2 center = ((Entity) Main.player[npc.target]).Center;
              float num10 = 1000f * (float) (num9 - this.WorldEvilAttackCycleTimer) / (float) (num9 - num8);
              center.X += (float) Math.Sign(((Entity) npc).velocity.X) * num10;
              center.Y += Utils.NextFloat(Main.rand, -100f, 100f);
              if ((double) center.Y / 16.0 < (double) (Main.maxTilesY - 200))
                center.Y = (float) ((Main.maxTilesY - 200) * 16);
              if ((double) center.Y / 16.0 >= (double) Main.maxTilesY)
                center.Y = (float) (Main.maxTilesY * 16 - 16);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), center, Vector2.Zero, ModContent.ProjectileType<WOFReticle>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.6666667f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
        }
        else
          this.ChainBarrageTimer = 0;
      }
      else if (this.InPhase2 && (double) npc.life < (double) npc.lifeMax * (WorldSavingSystem.MasochistModeReal ? 0.8 : 0.5))
      {
        this.InPhase3 = true;
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
        if (!Main.dedServ)
        {
          soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Monster94", (SoundType) 0);
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(!npc.HasValidTarget || !Main.player[npc.target].ZoneUnderworldHeight ? ((Entity) npc).Center : ((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
          if (((Entity) Main.LocalPlayer).active)
            ScreenShakeSystem.StartShake(15f, 6.28318548f, new Vector2?(), 0.166666672f);
        }
      }
      if (npc.life < npc.lifeMax / (WorldSavingSystem.MasochistModeReal ? 4 : 10))
      {
        ++this.WorldEvilAttackCycleTimer;
        if (!this.InDesperationPhase)
        {
          this.InDesperationPhase = true;
          for (int index = 0; index < Main.maxNPCs; ++index)
          {
            if (((Entity) Main.npc[index]).active && Main.npc[index].type == 114 && Main.npc[index].realLife == ((Entity) npc).whoAmI)
              Main.npc[index].GetGlobalNPC<WallofFleshEye>().PreventAttacks = 60;
          }
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
          if (!Main.dedServ)
          {
            // ISSUE: explicit constructor call
            ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/Monster5", (SoundType) 0);
            ((SoundStyle) ref soundStyle).Volume = 1.5f;
            SoundEngine.PlaySound(ref soundStyle, new Vector2?(!npc.HasValidTarget || !Main.player[npc.target].ZoneUnderworldHeight ? ((Entity) npc).Center : ((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
            if (((Entity) Main.LocalPlayer).active)
              ScreenShakeSystem.StartShake(20f, 6.28318548f, new Vector2?(), 0.111111112f);
          }
        }
      }
      float num11 = WorldSavingSystem.MasochistModeReal ? 4.5f : 3.5f;
      if (!Main.getGoodWorld)
      {
        if (npc.HasPlayerTarget && (Main.player[npc.target].dead || (double) Vector2.Distance(((Entity) npc).Center, ((Entity) Main.player[npc.target]).Center) > 3000.0))
          npc.TargetClosest(true);
        else if ((double) Math.Abs(((Entity) npc).velocity.X) > (double) num11)
          ((Entity) npc).position.X -= (Math.Abs(((Entity) npc).velocity.X) - num11) * (float) Math.Sign(((Entity) npc).velocity.X);
      }
      if (((Entity) Main.LocalPlayer).active & !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && Main.LocalPlayer.ZoneUnderworldHeight)
      {
        float num12 = ((Entity) npc).velocity.X;
        if ((double) num12 > (double) num11)
          num12 = num11;
        else if ((double) num12 < -(double) num11)
          num12 = -num11;
        for (int index1 = 0; index1 < 10; ++index1)
        {
          Vector2 vector2 = Utils.RotatedBy(new Vector2((float) (2000 * ((Entity) npc).direction), 0.0f), Math.PI / 3.0 * (Main.rand.NextDouble() - 0.5), new Vector2());
          int index2 = Dust.NewDust(Vector2.op_Addition(((Entity) npc).Center, vector2), 0, 0, 6, 0.0f, 0.0f, 0, new Color(), 1f);
          ++Main.dust[index2].scale;
          Main.dust[index2].velocity.X = num12;
          Main.dust[index2].velocity.Y = ((Entity) npc).velocity.Y;
          Main.dust[index2].noGravity = true;
          Main.dust[index2].noLight = true;
        }
        if (++this.TongueTimer > 15)
        {
          this.TongueTimer = 0;
          if ((double) Math.Abs(2400f - ((Entity) npc).Distance(((Entity) Main.LocalPlayer).Center)) < 400.0)
          {
            if (!Main.LocalPlayer.tongued)
              SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
            Main.LocalPlayer.AddBuff(38, 10, true, false);
          }
        }
      }
      EModeUtils.DropSummon(npc, "FleshyDoll", Main.hardMode, ref this.DroppedSummon);
      return flag;
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      if (WorldSavingSystem.WOFDroppedDeviGift2)
        return;
      WorldSavingSystem.WOFDroppedDeviGift2 = true;
      npc.DropItemInstanced(((Entity) npc).position, ((Entity) npc).Size, 499, 15, true);
      Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, ModContent.Find<ModItem>("Fargowiltas", "PylonCleaner").Type, 1, false, 0, false, false);
      Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, 369, 30, false, 0, false, false);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(67, 300, true, false);
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Division(local, 3f);
      base.ModifyIncomingHit(npc, ref modifiers);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 22);
      EModeNPCBehaviour.LoadGoreRange(recolor, 132, 142);
      EModeNPCBehaviour.LoadSpecial(recolor, ref TextureAssets.Chain12, ref FargowiltasSouls.FargowiltasSouls.TextureBuffer.Chain12, "Chain12");
      EModeNPCBehaviour.LoadSpecial(recolor, ref TextureAssets.Wof, ref FargowiltasSouls.FargowiltasSouls.TextureBuffer.Wof, "WallOfFlesh");
    }
  }
}
