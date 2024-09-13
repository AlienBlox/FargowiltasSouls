// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.DungeonGuardian
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Shadow;
using FargowiltasSouls.Content.Bosses.DeviBoss;
using FargowiltasSouls.Content.Buffs.Masomode;
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
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class DungeonGuardian : EModeNPCBehaviour
  {
    public int AITimer;
    public int AttackTimer;
    public bool TeleportCheck;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(68);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.AITimer);
      binaryWriter.Write7BitEncodedInt(this.AttackTimer);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.AITimer = binaryReader.Read7BitEncodedInt();
      this.AttackTimer = binaryReader.Read7BitEncodedInt();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax /= 8;
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
    }

    public override bool SafePreAI(NPC npc)
    {
      npc.boss = true;
      return base.SafePreAI(npc);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (WorldSavingSystem.SwarmActive)
        return;
      EModeGlobalNPC.guardBoss = ((Entity) npc).whoAmI;
      npc.damage = npc.defDamage;
      npc.defense = npc.defDefense;
      while (npc.buffType[0] != 0)
      {
        npc.buffImmune[npc.buffType[0]] = true;
        npc.DelBuff(0);
      }
      if (((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
      {
        Rectangle hitbox = ((Entity) npc).Hitbox;
        if (((Rectangle) ref hitbox).Intersects(((Entity) Main.LocalPlayer).Hitbox))
        {
          Main.LocalPlayer.immune = false;
          Main.LocalPlayer.immuneTime = 0;
          Main.LocalPlayer.hurtCooldowns[0] = 0;
          Main.LocalPlayer.hurtCooldowns[1] = 0;
        }
      }
      if (npc.HasValidTarget && (double) npc.ai[1] == 2.0)
      {
        NPC npc1 = npc;
        ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, ((Entity) npc).velocity);
        float num1 = 6f;
        float num2 = Math.Max(Math.Abs(((Entity) Main.player[npc.target]).velocity.X), Math.Abs(((Entity) Main.player[npc.target]).velocity.Y)) * 1.02f;
        if ((double) num1 < (double) num2)
          num1 = num2;
        NPC npc2 = npc;
        ((Entity) npc2).position = Vector2.op_Addition(((Entity) npc2).position, Vector2.op_Multiply(Vector2.Normalize(((Entity) npc).velocity), num1));
      }
      if (!this.TeleportCheck)
      {
        this.TeleportCheck = true;
        npc.TargetClosest(false);
        if (npc.HasValidTarget && (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > 800.0 && (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) < 3000.0)
        {
          for (int index1 = 0; index1 < 50; ++index1)
          {
            int index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 112, 0.0f, 0.0f, 0, Color.White, 2.5f);
            Main.dust[index2].noGravity = true;
            Dust dust = Main.dust[index2];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 12f);
          }
          if (FargoSoulsUtil.HostCheck)
            ((Entity) npc).Center = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(800f, Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI)));
          for (int index3 = 0; index3 < 50; ++index3)
          {
            int index4 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 112, 0.0f, 0.0f, 0, Color.White, 2.5f);
            Main.dust[index4].noGravity = true;
            Dust dust = Main.dust[index4];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 12f);
          }
        }
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if (++this.AITimer < 90)
      {
        if (this.AITimer == 1 && npc.HasPlayerTarget)
        {
          if (FargoSoulsUtil.HostCheck)
          {
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.player[npc.target]).Center, Vector2.UnitY, ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, (float) npc.target, -1f, 0.0f);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.player[npc.target]).Center, Vector2.op_UnaryNegation(Vector2.UnitY), ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, (float) npc.target, -1f, 0.0f);
          }
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
        }
        if (++this.AttackTimer <= 1)
          return;
        SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        this.AttackTimer = 0;
        Vector2 center = ((Entity) Main.player[npc.target]).Center;
        center.X += Utils.NextFloat(Main.rand, -100f, 100f);
        center.Y -= Utils.NextFloat(Main.rand, 700f, 800f);
        if (!FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), center, Vector2.op_Multiply(Vector2.UnitY, Utils.NextFloat(Main.rand, 10f, 20f)), ModContent.ProjectileType<SkeletronBone>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.2f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      else if (this.AITimer < 220)
      {
        if (this.AITimer == 91)
        {
          for (int index = 0; index < 6; ++index)
          {
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), Math.PI / 3.0 * (double) index, new Vector2()), ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, -1f, (float) ((Entity) npc).whoAmI, 0.0f);
          }
          this.AttackTimer = 30;
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
        }
        if (++this.AttackTimer <= 60)
          return;
        this.AttackTimer = 0;
        if (!FargoSoulsUtil.HostCheck)
          return;
        Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
        vector2_1.X += (float) Main.rand.Next(-20, 21);
        vector2_1.Y += (float) Main.rand.Next(-20, 21);
        ((Vector2) ref vector2_1).Normalize();
        Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 3f);
        for (int index = 0; index < 6; ++index)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(vector2_2, Math.PI / 3.0 * (double) index, new Vector2()), 270, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.2f), 0.0f, Main.myPlayer, -1f, 0.0f, 0.0f);
      }
      else
      {
        if (this.AITimer < 280)
          return;
        if (this.AITimer < 410)
        {
          if (this.AITimer == 281)
          {
            for (int index = 0; index < 4; ++index)
            {
              if (FargoSoulsUtil.HostCheck)
              {
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.player[npc.target]).Center, Utils.RotatedBy(Vector2.UnitX, Math.PI / 2.0 * (double) index, new Vector2()), ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, (float) npc.target, -1f, 0.0f);
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(160f, Utils.RotatedBy(Vector2.UnitY, Math.PI / 2.0 * (double) index, new Vector2()))), Utils.RotatedBy(Vector2.UnitX, Math.PI / 2.0 * (double) index, new Vector2()), ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, (float) npc.target, -1f, 0.0f);
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(-160f, Utils.RotatedBy(Vector2.UnitY, Math.PI / 2.0 * (double) index, new Vector2()))), Utils.RotatedBy(Vector2.UnitX, Math.PI / 2.0 * (double) index, new Vector2()), ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, (float) npc.target, -1f, 0.0f);
              }
            }
            this.AttackTimer = 0;
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
          }
          if (++this.AttackTimer != 30)
            return;
          this.AITimer += 60;
          if (!FargoSoulsUtil.HostCheck)
            return;
          for (int index5 = 0; index5 < 4; ++index5)
          {
            for (int index6 = -2; index6 <= 2; ++index6)
            {
              Vector2 vector2_3;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2_3).\u002Ector(1200f, (float) (80 * index6));
              Vector2 vector2_4 = Vector2.op_Multiply(-18f, Vector2.UnitX);
              vector2_3 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Utils.RotatedBy(vector2_3, Math.PI / 2.0 * (double) index5, new Vector2()));
              Vector2 vector2_5 = Utils.RotatedBy(vector2_4, Math.PI / 2.0 * (double) index5, new Vector2());
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_3, vector2_5, ModContent.ProjectileType<ShadowGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.2f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
        }
        else if (this.AITimer < 540)
        {
          if (this.AITimer == 481)
          {
            for (int index = 0; index < 16; ++index)
            {
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.player[npc.target]).Center, Utils.RotatedBy(Vector2.UnitX, Math.PI / 8.0 * (double) index, new Vector2()), ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, (float) npc.target, -1f, 0.0f);
            }
            this.AttackTimer = 0;
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
          }
          if (++this.AttackTimer != 30 || !FargoSoulsUtil.HostCheck)
            return;
          Vector2 vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center);
          for (int index = 0; index < 16; ++index)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(1000f, Utils.RotatedBy(vector2, Math.PI / 8.0 * (double) index, new Vector2()))), Vector2.op_Multiply(-10f, Utils.RotatedBy(vector2, Math.PI / 8.0 * (double) index, new Vector2())), ModContent.ProjectileType<DeviGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.2f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
        else if (this.AITimer < 700)
        {
          if (this.AITimer == 541)
          {
            if (FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.UnitY, ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, -1f, (float) ((Entity) npc).whoAmI, 0.0f);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(-200f, Vector2.UnitX)), Vector2.UnitY, ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, -1f, (float) ((Entity) npc).whoAmI, 0.0f);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(200f, Vector2.UnitX)), Vector2.UnitY, ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, -1f, (float) ((Entity) npc).whoAmI, 0.0f);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_UnaryNegation(Vector2.UnitY), ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, -1f, (float) ((Entity) npc).whoAmI, 0.0f);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(-200f, Vector2.UnitX)), Vector2.op_UnaryNegation(Vector2.UnitY), ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, -1f, (float) ((Entity) npc).whoAmI, 0.0f);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(200f, Vector2.UnitX)), Vector2.op_UnaryNegation(Vector2.UnitY), ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, -1f, (float) ((Entity) npc).whoAmI, 0.0f);
            }
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
          }
          if (++this.AttackTimer <= 2)
            return;
          SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          this.AttackTimer = 0;
          Vector2 vector2_6;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_6).\u002Ector((float) Main.rand.Next(-100, 101), (float) Main.rand.Next(-100, 101));
          ((Vector2) ref vector2_6).Normalize();
          Vector2 vector2_7 = Vector2.op_Addition(Vector2.op_Multiply(vector2_6, 6f), Vector2.op_Multiply(((Entity) npc).velocity, 1.25f));
          vector2_7.Y -= Math.Abs(vector2_7.X) * 0.2f;
          if (!FargoSoulsUtil.HostCheck)
            return;
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2_7, ModContent.ProjectileType<SkeletronBone>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.2f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
        else if (this.AITimer < 820)
        {
          if (this.AITimer == 701)
          {
            for (int index7 = 0; index7 < 4; ++index7)
            {
              Vector2 vector2_8 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(1000f, Utils.RotatedBy(Vector2.UnitX, Math.PI / 2.0 * (double) index7, new Vector2())));
              for (int index8 = -1; index8 <= 1; ++index8)
              {
                if (index8 != 0)
                {
                  Vector2 vector2_9 = Utils.RotatedBy(((Entity) Main.player[npc.target]).DirectionFrom(vector2_8), (double) MathHelper.ToRadians(15f) * (double) index8, new Vector2());
                  for (int index9 = 0; index9 < 7; ++index9)
                  {
                    if (index9 % 2 != 1 && FargoSoulsUtil.HostCheck)
                      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_8, Utils.RotatedBy(vector2_9, (double) MathHelper.ToRadians(10f) * (double) index8 * (double) index9, new Vector2()), ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, (float) npc.target, -1f, 0.0f);
                  }
                }
              }
            }
            this.AttackTimer = 0;
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
          }
          if (++this.AttackTimer != 30)
            return;
          for (int index10 = 0; index10 < 4; ++index10)
          {
            Vector2 vector2_10 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(1000f, Utils.RotatedBy(Vector2.UnitX, Math.PI / 2.0 * (double) index10, new Vector2())));
            for (int index11 = -1; index11 <= 1; ++index11)
            {
              if (index11 != 0)
              {
                Vector2 vector2_11 = Vector2.op_Multiply(22f, Utils.RotatedBy(((Entity) Main.player[npc.target]).DirectionFrom(vector2_10), (double) MathHelper.ToRadians(15f) * (double) index11, new Vector2()));
                for (int index12 = 0; index12 < 7; ++index12)
                {
                  if (FargoSoulsUtil.HostCheck)
                    Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_10, Utils.RotatedBy(vector2_11, (double) MathHelper.ToRadians(10f) * (double) index11 * (double) index12, new Vector2()), ModContent.ProjectileType<ShadowGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.2f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
              }
            }
          }
        }
        else
        {
          this.AITimer = 0;
          this.AttackTimer = 0;
          this.TeleportCheck = false;
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
        }
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<GodEaterBuff>(), 420, true, false);
      target.AddBuff(ModContent.BuffType<FlamesoftheUniverseBuff>(), 420, true, false);
      target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 420, true, false);
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      ((NPC.HitModifiers) ref modifiers).SetMaxDamage(1);
      base.ModifyIncomingHit(npc, ref modifiers);
    }
  }
}
