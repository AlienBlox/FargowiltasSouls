// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Nature.NatureChampionHead
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Nature
{
  [AutoloadBossHead]
  public class NatureChampionHead : ModNPC
  {
    public Vector2 position;
    public Vector2 oldPosition;

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 4;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 7);
      Span<int> span = CollectionsMarshal.AsSpan<int>(debuffs);
      int num1 = 0;
      span[num1] = 31;
      int num2 = num1 + 1;
      span[num2] = 46;
      int num3 = num2 + 1;
      span[num3] = 24;
      int num4 = num3 + 1;
      span[num4] = 68;
      int num5 = num4 + 1;
      span[num5] = ModContent.BuffType<LethargicBuff>();
      int num6 = num5 + 1;
      span[num6] = ModContent.BuffType<ClippedWingsBuff>();
      int num7 = num6 + 1;
      span[num7] = ModContent.BuffType<LightningRodBuff>();
      int num8 = num7 + 1;
      npc.AddDebuffImmunities(debuffs);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 80;
      ((Entity) this.NPC).height = 80;
      this.NPC.damage = 110;
      this.NPC.defense = 100;
      this.NPC.lifeMax = 900000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit6);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
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
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.NPC.localAI[0] = reader.ReadSingle();
      this.NPC.localAI[1] = reader.ReadSingle();
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot)
    {
      if ((double) this.NPC.ai[0] == 0.0 || (double) this.NPC.ai[0] == -3.0)
        return false;
      CooldownSlot = 1;
      return true;
    }

    public virtual void AI()
    {
      NPC npc1 = FargoSoulsUtil.NPCExists(this.NPC.ai[1], ModContent.NPCType<NatureChampion>());
      if (npc1 == null)
      {
        this.NPC.life = 0;
        this.NPC.checkDead();
        ((Entity) this.NPC).active = false;
      }
      else
      {
        this.NPC.target = npc1.target;
        this.NPC.realLife = ((Entity) npc1).whoAmI;
        NPC npc2 = this.NPC;
        ((Entity) npc2).position = Vector2.op_Addition(((Entity) npc2).position, Vector2.op_Multiply(((Entity) npc1).velocity, 0.75f));
        Player player = Main.player[this.NPC.target];
        if ((double) ((Entity) player).Center.X < (double) ((Entity) this.NPC).position.X)
          ((Entity) this.NPC).direction = this.NPC.spriteDirection = -1;
        else if ((double) ((Entity) player).Center.X > (double) ((Entity) this.NPC).position.X + (double) ((Entity) this.NPC).width)
          ((Entity) this.NPC).direction = this.NPC.spriteDirection = 1;
        this.NPC.rotation = 0.0f;
        switch (this.NPC.ai[0])
        {
          case -3f:
            this.Movement(Vector2.op_Subtraction(((Entity) player).Center, Vector2.op_Multiply(Vector2.UnitY, 250f)), 0.3f, 24f);
            if ((double) ++this.NPC.ai[2] > 75.0)
            {
              if ((double) this.NPC.ai[2] > 105.0)
                this.NPC.ai[2] = 0.0f;
              NPC npc3 = this.NPC;
              ((Entity) npc3).velocity = Vector2.op_Multiply(((Entity) npc3).velocity, 0.99f);
              if ((double) ++this.NPC.localAI[1] > 2.0)
              {
                this.NPC.localAI[1] = 0.0f;
                if ((double) this.NPC.localAI[0] > 60.0 && FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Utils.NextVector2Circular(Main.rand, (float) (((Entity) this.NPC).width / 2), (float) (((Entity) this.NPC).height / 2))), Vector2.op_Multiply(Vector2.UnitY, Utils.NextFloat(Main.rand, -4f, 0.0f)), 288, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
            if ((double) ++this.NPC.localAI[0] > 300.0)
            {
              this.NPC.ai[0] = 0.0f;
              this.NPC.localAI[0] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.localAI[1] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case -2f:
            if ((double) ++this.NPC.localAI[0] < 240.0)
            {
              this.Movement(((Entity) player).Center, 0.1f, 24f);
              for (int index = 0; index < 20; ++index)
              {
                Vector2 vector2 = new Vector2();
                double num = Main.rand.NextDouble() * 2.0 * Math.PI;
                vector2.X += (float) (Math.Sin(num) * 400.0);
                vector2.Y += (float) (Math.Cos(num) * 400.0);
                Dust dust1 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.NPC).Center, vector2), new Vector2(4f, 4f)), 0, 0, 6, 0.0f, 0.0f, 100, Color.White, 2f)];
                dust1.velocity = ((Entity) this.NPC).velocity;
                if (Utils.NextBool(Main.rand, 3))
                {
                  Dust dust2 = dust1;
                  dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2), -5f));
                }
                dust1.noGravity = true;
              }
              break;
            }
            if ((double) this.NPC.localAI[0] == 240.0)
            {
              ((Entity) this.NPC).velocity = Vector2.Zero;
              this.NPC.netUpdate = true;
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
              {
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<NatureExplosion>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                for (int index = 0; index < 12; ++index)
                {
                  Vector2 vector2 = Vector2.op_Multiply(24f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), Math.PI / 6.0 * (double) index, new Vector2()));
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<NatureFireball>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
                break;
              }
              break;
            }
            if ((double) this.NPC.localAI[0] > 300.0)
            {
              this.NPC.ai[0] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.localAI[0] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case -1f:
            this.Movement(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), 300f)), 0.25f, 24f);
            if ((double) ++this.NPC.localAI[1] > 45.0)
            {
              this.NPC.localAI[1] = 0.0f;
              SoundEngine.PlaySound(ref SoundID.Item66, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
              {
                Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center);
                vector2_1.Y -= 300f;
                Vector2 vector2_2 = Vector2.op_Division(vector2_1, 40f);
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<NatureCloudMoving>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
            if ((double) ++this.NPC.localAI[0] > 300.0)
            {
              this.NPC.ai[0] = 0.0f;
              this.NPC.localAI[0] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.localAI[1] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 0.0f:
            Vector2 vector2_3;
            vector2_3.X = 100f * this.NPC.ai[3] - (float) (50 * Math.Sign(this.NPC.ai[3]));
            vector2_3.Y = (float) (75.0 * (double) Math.Abs(this.NPC.ai[3]) - 350.0);
            Vector2 targetPos1 = Vector2.op_Addition(((Entity) npc1).Center, vector2_3);
            if ((double) ((Entity) this.NPC).Distance(targetPos1) > 50.0)
            {
              this.Movement(targetPos1, 0.8f, 24f);
              break;
            }
            break;
          case 1f:
            Vector2 vector2_4;
            vector2_4.X = 100f * this.NPC.ai[3] - (float) (50 * Math.Sign(this.NPC.ai[3]));
            vector2_4.Y = (float) (75.0 * (double) Math.Abs(this.NPC.ai[3]) - 350.0);
            Vector2 targetPos2 = Vector2.op_Addition(((Entity) npc1).Center, vector2_4);
            if ((double) ((Entity) this.NPC).Distance(targetPos2) > 50.0)
              this.Movement(targetPos2, 0.8f, 24f);
            if ((double) ++this.NPC.ai[2] > 60.0)
            {
              this.NPC.ai[2] = 0.0f;
              if (FargoSoulsUtil.HostCheck)
              {
                for (int index = 0; index < 25; ++index)
                {
                  Vector2 vector2_5 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 1f, 3f), Utils.RotatedBy(Vector2.UnitX, 2.0 * Math.PI / 25.0 * ((double) index + Main.rand.NextDouble()), new Vector2()));
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_5, ModContent.ProjectileType<NatureIcicle>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) (60 + Main.rand.Next(20)), 1f, 0.0f);
                }
              }
            }
            if ((double) ++this.NPC.localAI[0] > 300.0)
            {
              this.NPC.ai[0] = 0.0f;
              this.NPC.localAI[0] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.localAI[1] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 2f:
            this.Movement(((Entity) player).Center, 0.12f, 24f);
            if ((double) this.NPC.ai[2] == 0.0)
            {
              this.NPC.ai[2] = 1f;
              if (FargoSoulsUtil.HostCheck)
              {
                float num = 1.2566371f;
                for (int index = 0; index < 5; ++index)
                {
                  Vector2 vector2_6 = Vector2.op_Addition(((Entity) this.NPC).Center, Utils.RotatedBy(new Vector2(125f, 0.0f), (double) num * (double) index, new Vector2()));
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_6, Vector2.Zero, ModContent.ProjectileType<NatureCrystalLeaf>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, num * (float) index, 0.0f);
                }
              }
            }
            if ((double) ++this.NPC.localAI[0] > 300.0)
            {
              this.NPC.ai[0] = 0.0f;
              this.NPC.localAI[0] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.localAI[1] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 3f:
            Vector2 vector2_7;
            vector2_7.X = 100f * this.NPC.ai[3] - (float) (50 * Math.Sign(this.NPC.ai[3]));
            vector2_7.Y = (float) (75.0 * (double) Math.Abs(this.NPC.ai[3]) - 350.0);
            Vector2 targetPos3 = Vector2.op_Addition(((Entity) npc1).Center, vector2_7);
            if ((double) ((Entity) this.NPC).Distance(targetPos3) > 50.0)
              this.Movement(targetPos3, 0.8f, 24f);
            if ((double) ++this.NPC.ai[2] < 20.0 && (double) this.NPC.localAI[0] > 60.0 && (double) this.NPC.ai[2] % 2.0 == 0.0)
            {
              Vector2 vector2_8 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center);
              vector2_8.X += (float) Main.rand.Next(-40, 41);
              vector2_8.Y += (float) Main.rand.Next(-40, 41);
              ((Vector2) ref vector2_8).Normalize();
              vector2_8 = Vector2.op_Multiply(vector2_8, 12.5f);
              int num = (int) ((double) ((Entity) this.NPC).Distance(((Entity) player).Center) - 100.0) / 14;
              if (num < 0)
                num = 0;
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitY, 10f)), vector2_8, ModContent.ProjectileType<NatureBullet>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) num, 0.0f, 0.0f);
            }
            if ((double) this.NPC.ai[2] > 60.0)
              this.NPC.ai[2] = 0.0f;
            if ((double) ++this.NPC.localAI[0] > 300.0)
            {
              this.NPC.ai[0] = 0.0f;
              this.NPC.localAI[0] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.localAI[1] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 4f:
            Vector2 vector2_9 = Vector2.op_Multiply(-600f, Utils.RotatedBy(Vector2.UnitY, (double) MathHelper.ToRadians(20f) * (double) this.NPC.ai[3], new Vector2()));
            this.Movement(Vector2.op_Addition(((Entity) npc1).Center, vector2_9), 0.8f, 24f);
            ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) npc1).Center.X ? 1 : -1;
            if ((double) ++this.NPC.ai[2] == 90.0)
            {
              this.NPC.netUpdate = true;
              this.NPC.localAI[1] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, Vector2.op_Subtraction(((Entity) npc1).Center, Vector2.op_Multiply(Vector2.UnitY, 300f))));
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.UnitX, (double) this.NPC.localAI[1], new Vector2()), ModContent.ProjectileType<NatureDeathraySmall>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
            }
            else if ((double) this.NPC.ai[2] == 150.0)
            {
              float num = (float) Math.PI / 60f * (float) Math.Sign(this.NPC.ai[3]);
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.UnitX, (double) this.NPC.localAI[1], new Vector2()), ModContent.ProjectileType<NatureDeathray>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, num, (float) ((Entity) this.NPC).whoAmI, 0.0f);
            }
            if ((double) ++this.NPC.localAI[0] > 330.0)
            {
              this.NPC.ai[0] = 0.0f;
              this.NPC.localAI[0] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.localAI[1] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          default:
            this.NPC.ai[0] = 0.0f;
            goto case 0.0f;
        }
        if ((double) ((Entity) this.NPC).Distance(((Entity) npc1).Center) <= 1400.0)
          return;
        ((Entity) this.NPC).Center = Vector2.op_Addition(((Entity) npc1).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc1, ((Entity) this.NPC).Center), 1400f));
      }
    }

    public virtual void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Division(local, 3f);
    }

    private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f)
    {
      if ((double) ((Entity) this.NPC).Center.X < (double) targetPos.X)
      {
        ((Entity) this.NPC).velocity.X += speedModifier;
        if ((double) ((Entity) this.NPC).velocity.X < 0.0)
          ((Entity) this.NPC).velocity.X += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.NPC).velocity.X -= speedModifier;
        if ((double) ((Entity) this.NPC).velocity.X > 0.0)
          ((Entity) this.NPC).velocity.X -= speedModifier * 2f;
      }
      if ((double) ((Entity) this.NPC).Center.Y < (double) targetPos.Y)
      {
        ((Entity) this.NPC).velocity.Y += speedModifier;
        if ((double) ((Entity) this.NPC).velocity.Y < 0.0)
          ((Entity) this.NPC).velocity.Y += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.NPC).velocity.Y -= speedModifier;
        if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
          ((Entity) this.NPC).velocity.Y -= speedModifier * 2f;
      }
      if ((double) Math.Abs(((Entity) this.NPC).velocity.X) > (double) cap)
        ((Entity) this.NPC).velocity.X = cap * (float) Math.Sign(((Entity) this.NPC).velocity.X);
      if ((double) Math.Abs(((Entity) this.NPC).velocity.Y) <= (double) cap)
        return;
      ((Entity) this.NPC).velocity.Y = cap * (float) Math.Sign(((Entity) this.NPC).velocity.Y);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(44, 300, true, false);
      target.AddBuff(24, 300, true, false);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life <= 0)
        FargoSoulsUtil.NPCExists(this.NPC.ai[1], ModContent.NPCType<NatureChampion>())?.HitEffect(hit);
      if (!Main.remixWorld || !Utils.NextBool(Main.rand, 10))
        return;
      int num1 = 780;
      int num2 = (int) this.NPC.ai[3];
      if (num2 > 0)
        --num2;
      switch (num2 + 3)
      {
        case 0:
          num1 = 784;
          break;
        case 1:
          num1 = 782;
          break;
        case 2:
          num1 = 5392;
          break;
        case 3:
          num1 = 5393;
          break;
        case 4:
          num1 = 780;
          break;
        case 5:
          num1 = 783;
          break;
      }
      if (!FargoSoulsUtil.HostCheck)
        return;
      Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) this.NPC).position, ((Entity) this.NPC).Size, num1, 1, false, 0, false, false);
    }

    public virtual bool CheckActive() => false;

    public virtual void FindFrame(int frameHeight)
    {
      this.NPC.frame.Y = 0;
      if (!this.NPC.HasValidTarget)
        this.NPC.frame.Y = frameHeight * 3;
      switch (this.NPC.ai[0])
      {
        case -3f:
          if ((double) this.NPC.ai[2] <= 60.0)
            break;
          this.NPC.frame.Y = frameHeight;
          break;
        case -2f:
          if ((double) this.NPC.localAI[0] <= 240.0)
            break;
          this.NPC.frame.Y = frameHeight * 2;
          break;
        case -1f:
          if ((double) this.NPC.localAI[1] >= 20.0)
            break;
          this.NPC.frame.Y = frameHeight;
          break;
        case 1f:
          if ((double) this.NPC.ai[2] <= 30.0)
            break;
          this.NPC.frame.Y = frameHeight;
          break;
        case 2f:
          this.NPC.frame.Y = frameHeight * 2;
          break;
        case 3f:
          if ((double) this.NPC.ai[2] >= 20.0 || (double) this.NPC.localAI[0] <= 60.0)
            break;
          this.NPC.frame.Y = frameHeight;
          break;
        case 4f:
          if ((double) this.NPC.ai[2] <= 90.0)
            break;
          this.NPC.frame.Y = frameHeight * 2;
          break;
      }
    }

    public virtual bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
      return new bool?(false);
    }

    private static float X(float t, float x0, float x1, float x2)
    {
      return (float) ((double) x0 * Math.Pow(1.0 - (double) t, 2.0) + (double) x1 * 2.0 * (double) t * Math.Pow(1.0 - (double) t, 1.0) + (double) x2 * Math.Pow((double) t, 2.0));
    }

    private static float Y(float t, float y0, float y1, float y2)
    {
      return (float) ((double) y0 * Math.Pow(1.0 - (double) t, 2.0) + (double) y1 * 2.0 * (double) t * Math.Pow(1.0 - (double) t, 1.0) + (double) y2 * Math.Pow((double) t, 2.0));
    }

    public void CheckDrawNeck(SpriteBatch spriteBatch)
    {
      if ((double) this.NPC.ai[1] <= -1.0 || (double) this.NPC.ai[1] >= (double) Main.maxNPCs || !((Entity) Main.npc[(int) this.NPC.ai[1]]).active || Main.npc[(int) this.NPC.ai[1]].type != ModContent.NPCType<NatureChampion>())
        return;
      NPC npc = Main.npc[(int) this.NPC.ai[1]];
      if ((double) ((Entity) Main.LocalPlayer).Distance(((Entity) npc).Center) <= 1200.0)
        return;
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Nature/NatureChampion_Neck", (AssetRequestMode) 1).Value;
      Vector2 center = ((Entity) this.NPC).Center;
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) npc).Center, new Vector2((float) (54 * npc.spriteDirection), -10f));
      float num1 = 0.05f;
      for (float t = 0.0f; (double) t <= 1.0; t += num1)
      {
        if ((double) t != 0.0)
        {
          Vector2 vector2_2;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_2).\u002Ector(NatureChampionHead.X(t, vector2_1.X, (float) (((double) vector2_1.X + (double) center.X) / 2.0), center.X) - NatureChampionHead.X(t - num1, vector2_1.X, (float) (((double) vector2_1.X + (double) center.X) / 2.0), center.X), NatureChampionHead.Y(t, vector2_1.Y, vector2_1.Y + 50f, center.Y) - NatureChampionHead.Y(t - num1, vector2_1.Y, vector2_1.Y + 50f, center.Y));
          if ((double) ((Vector2) ref vector2_2).Length() > 36.0 && (double) num1 > 0.0099999997764825821)
          {
            num1 -= 0.01f;
            t -= num1;
          }
          else
          {
            float num2 = Utils.ToRotation(vector2_2) - 1.57079637f;
            Vector2 vector2_3;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2_3).\u002Ector(NatureChampionHead.X(t, vector2_1.X, (float) (((double) vector2_1.X + (double) center.X) / 2.0), center.X), NatureChampionHead.Y(t, vector2_1.Y, vector2_1.Y + 50f, center.Y));
            spriteBatch.Draw(texture2D, new Vector2(NatureChampionHead.X(t, vector2_1.X, (float) (((double) vector2_1.X + (double) center.X) / 2.0), center.X) - Main.screenPosition.X, NatureChampionHead.Y(t, vector2_1.Y, vector2_1.Y + 50f, center.Y) - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, texture2D.Width, texture2D.Height)), npc.GetAlpha(Lighting.GetColor((int) vector2_3.X / 16, (int) vector2_3.Y / 16)), num2, new Vector2((float) texture2D.Width * 0.5f, (float) texture2D.Height * 0.5f), 1f, (double) center.X < (double) vector2_1.X ? (SpriteEffects) 1 : (SpriteEffects) 0, 0.0f);
          }
        }
      }
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      this.CheckDrawNeck(spriteBatch);
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
      this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects1 = this.NPC.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      int num1 = (int) this.NPC.ai[3];
      if (num1 > 0)
        --num1;
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Nature/NatureChampionHead_Glow" + (num1 + 3).ToString(), (AssetRequestMode) 1).Value;
      float num2 = (float) (((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.40000000596046448 + 0.800000011920929);
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.op_Multiply(this.NPC.GetAlpha(drawColor), 0.5f), this.NPC.rotation, vector2_1, this.NPC.scale * num2, spriteEffects1, 0.0f);
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects1, 0.0f);
      Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY));
      Rectangle? nullable = new Rectangle?(frame);
      Color white = Color.White;
      double rotation = (double) this.NPC.rotation;
      Vector2 vector2_3 = vector2_1;
      double scale = (double) this.NPC.scale;
      SpriteEffects spriteEffects2 = spriteEffects1;
      Main.EntitySpriteDraw(texture2D2, vector2_2, nullable, white, (float) rotation, vector2_3, (float) scale, spriteEffects2, 0.0f);
      return false;
    }
  }
}
