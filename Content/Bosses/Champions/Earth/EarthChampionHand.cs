// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Earth.EarthChampionHand
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Earth
{
  public class EarthChampionHand : ModNPC
  {
    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 2;
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 6;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
      NPCID.Sets.CantTakeLunchMoney[this.NPC.type] = true;
      NPCID.Sets.ImmuneToRegularBuffs[this.Type] = true;
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 100;
      ((Entity) this.NPC).height = 100;
      this.NPC.damage = 130;
      this.NPC.defense = 80;
      this.NPC.lifeMax = 320000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit41);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath44);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      this.NPC.trapImmune = true;
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * (double) balance);
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot)
    {
      CooldownSlot = 1;
      return (double) this.NPC.localAI[3] == 1.0;
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

    public virtual void AI()
    {
      NPC npc1 = FargoSoulsUtil.NPCExists(this.NPC.ai[2], ModContent.NPCType<EarthChampion>());
      if (npc1 == null)
      {
        this.NPC.life = 0;
        this.NPC.checkDead();
        ((Entity) this.NPC).active = false;
      }
      else
      {
        this.NPC.lifeMax = npc1.lifeMax;
        this.NPC.damage = npc1.damage;
        this.NPC.defDamage = npc1.defDamage;
        this.NPC.defense = npc1.defense;
        this.NPC.defDefense = npc1.defDefense;
        this.NPC.target = npc1.target;
        this.NPC.life = this.NPC.lifeMax;
        Player player = Main.player[this.NPC.target];
        ((Entity) this.NPC).direction = this.NPC.spriteDirection = (int) this.NPC.ai[3];
        this.NPC.localAI[3] = 0.0f;
        switch (this.NPC.ai[0])
        {
          case -1f:
            Vector2 center1 = ((Entity) npc1).Center;
            center1.Y += (float) ((Entity) npc1).height;
            center1.X += (float) ((double) ((Entity) npc1).width * (double) this.NPC.ai[3] / 2.0);
            if ((double) this.NPC.ai[3] > 0.0)
              this.NPC.rotation = -0.7853982f;
            else
              this.NPC.rotation = 0.7853982f;
            if ((double) this.NPC.ai[1] == 120.0 && FargoSoulsUtil.HostCheck)
            {
              float num1 = 0.7853982f * Utils.NextFloat(Main.rand);
              for (int index = 0; index < 8; ++index)
              {
                float num2 = num1 + (float) (0.78539818525314331 * ((double) index + (double) Utils.NextFloat(Main.rand, -0.5f, 0.5f)));
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<EarthChainBlast2>(), 0, 0.0f, Main.myPlayer, num2, 3f, 0.0f);
              }
            }
            if ((double) ++this.NPC.ai[1] > 120.0)
            {
              this.Movement(center1, 0.6f, 32f);
              this.NPC.localAI[3] = 1f;
              if ((double) this.NPC.ai[3] > 0.0)
                this.NPC.rotation = -2.3561945f;
              else
                this.NPC.rotation = 2.3561945f;
              if ((double) this.NPC.ai[1] <= 240.0)
                break;
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            this.Movement(center1, 1.2f, 32f);
            break;
          case 0.0f:
          case 2f:
          case 4f:
          case 9f:
            this.NPC.noTileCollide = true;
            Vector2 center2 = ((Entity) npc1).Center;
            center2.Y += 250f;
            center2.X += (float) (300.0 * -(double) this.NPC.ai[3]);
            this.Movement(center2, 0.8f, 32f);
            this.NPC.rotation = 0.0f;
            if ((double) ++this.NPC.ai[1] <= 60.0)
              break;
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.netUpdate = true;
            break;
          case 1f:
            if ((double) ++this.NPC.ai[1] < 0.0)
            {
              this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity) - 1.57079637f;
              this.NPC.localAI[3] = 1f;
            }
            else if ((double) this.NPC.ai[1] < 75.0)
            {
              this.NPC.rotation = 0.0f;
              Vector2 targetPos = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, ((Entity) this.NPC).Center), 400f));
              if ((double) this.NPC.ai[3] < 0.0 && (double) targetPos.X < (double) ((Entity) player).Center.X + 400.0)
                targetPos.X = ((Entity) player).Center.X + 400f;
              if ((double) this.NPC.ai[3] > 0.0 && (double) targetPos.X > (double) ((Entity) player).Center.X - 400.0)
                targetPos.X = ((Entity) player).Center.X - 400f;
              if ((double) ((Entity) this.NPC).Distance(targetPos) > 50.0)
                this.Movement(targetPos, (double) npc1.localAI[2] == 1.0 ? 2.4f : 1.2f, 32f);
              if ((double) npc1.localAI[2] == 1.0)
              {
                NPC npc2 = this.NPC;
                ((Entity) npc2).position = Vector2.op_Addition(((Entity) npc2).position, Vector2.op_Division(((Entity) player).velocity, 3f));
              }
            }
            else if ((double) this.NPC.ai[1] < 120.0)
            {
              if ((double) npc1.localAI[2] == 1.0)
              {
                NPC npc3 = this.NPC;
                ((Entity) npc3).position = Vector2.op_Addition(((Entity) npc3).position, Vector2.op_Division(((Entity) player).velocity, 10f));
              }
              this.NPC.localAI[3] = 1f;
              NPC npc4 = this.NPC;
              ((Entity) npc4).velocity = Vector2.op_Multiply(((Entity) npc4).velocity, (double) this.NPC.localAI[2] == 1.0 ? 0.8f : 0.95f);
              this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center)) - 1.57079637f;
            }
            else if ((double) this.NPC.ai[1] == 120.0)
            {
              this.NPC.localAI[3] = 1f;
              ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), (double) npc1.localAI[2] == 1.0 ? 20f : 16f);
            }
            else
            {
              NPC npc5 = this.NPC;
              ((Entity) npc5).velocity = Vector2.op_Multiply(((Entity) npc5).velocity, 1.02f);
              this.NPC.localAI[3] = 1f;
              this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity) - 1.57079637f;
              for (int index1 = 0; index1 < 5; ++index1)
              {
                int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 6, (float) (-(double) ((Entity) this.NPC).velocity.X * 0.25), (float) (-(double) ((Entity) this.NPC).velocity.Y * 0.25), 0, new Color(), 3f);
                Dust dust1 = Main.dust[index2];
                dust1.position = Vector2.op_Subtraction(dust1.position, Vector2.op_Division(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), (float) ((Entity) this.NPC).width), 2f));
                Main.dust[index2].noGravity = true;
                Dust dust2 = Main.dust[index2];
                dust2.velocity = Vector2.op_Multiply(dust2.velocity, 4f);
              }
              if ((double) ++this.NPC.localAI[1] > 60.0 && (double) ((Entity) this.NPC).Distance(((Entity) player).Center) > 1000.0 || ((double) this.NPC.ai[3] > 0.0 ? ((double) ((Entity) this.NPC).Center.X > (double) Math.Min(((Entity) npc1).Center.X, ((Entity) player).Center.X) + 300.0 ? 1 : 0) : ((double) ((Entity) this.NPC).Center.X < (double) Math.Max(((Entity) npc1).Center.X, ((Entity) player).Center.X) - 300.0 ? 1 : 0)) != 0)
              {
                this.NPC.ai[1] = (double) npc1.localAI[2] == 1.0 ? 15f : 0.0f;
                this.NPC.localAI[1] = 0.0f;
                this.NPC.netUpdate = true;
                if ((double) npc1.localAI[2] == 1.0 && WorldSavingSystem.EternityMode && FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), 100f)), Vector2.Zero, ModContent.ProjectileType<EarthChainBlast>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, Utils.ToRotation(((Entity) this.NPC).velocity), 7f, 0.0f);
                ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), 0.1f);
              }
            }
            if ((double) ++this.NPC.localAI[0] <= 660.0)
              break;
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.localAI[0] = 0.0f;
            this.NPC.netUpdate = true;
            break;
          case 3f:
            if ((double) this.NPC.ai[3] > 0.0)
            {
              Vector2 center3 = ((Entity) player).Center;
              center3.Y += ((Entity) player).velocity.Y * 60f;
              center3.X = ((Entity) player).Center.X - 400f;
              if ((double) ((Entity) this.NPC).Distance(center3) > 50.0)
                this.Movement(center3, 0.4f, 32f);
            }
            else
            {
              Vector2 center4 = ((Entity) player).Center;
              center4.X += 400f;
              center4.Y += 600f * (float) Math.Sin(2.0 * Math.PI / 77.0 * (double) this.NPC.ai[1]);
              this.Movement(center4, 0.8f, 32f);
            }
            if ((double) ++this.NPC.localAI[0] > ((double) npc1.localAI[2] == 1.0 ? 18.0 : 24.0))
            {
              this.NPC.localAI[0] = 0.0f;
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitX, this.NPC.ai[3]), ModContent.ProjectileType<FlowerPetal>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (double) npc1.localAI[2] != 1.0 || !WorldSavingSystem.EternityMode ? 1f : 0.0f, 0.0f, 0.0f);
            }
            ((Entity) this.NPC).position.X += ((Entity) this.NPC).velocity.X;
            if ((double) ++this.NPC.ai[1] <= 360.0)
              break;
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.localAI[0] = 0.0f;
            this.NPC.netUpdate = true;
            break;
          case 5f:
          case 6f:
          case 7f:
            if ((double) ++this.NPC.ai[1] < 90.0)
            {
              this.NPC.noTileCollide = true;
              Vector2 center5 = ((Entity) npc1).Center;
              center5.Y -= (float) ((Entity) npc1).height;
              center5.X += (float) (50.0 * -(double) this.NPC.ai[3]);
              this.Movement(center5, 2f, 32f);
              this.NPC.rotation = 0.0f;
              break;
            }
            if ((double) this.NPC.ai[1] == 90.0)
            {
              ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.UnitY, (double) npc1.localAI[2] == 1.0 ? 36f : 24f);
              this.NPC.localAI[0] = ((Entity) player).position.Y;
              this.NPC.netUpdate = true;
              break;
            }
            this.NPC.localAI[3] = 1f;
            if ((double) this.NPC.ai[3] > 0.0)
              this.NPC.rotation = -1.57079637f;
            else
              this.NPC.rotation = 1.57079637f;
            if ((double) ((Entity) this.NPC).position.Y + (double) ((Entity) this.NPC).height > (double) this.NPC.localAI[0])
              this.NPC.noTileCollide = false;
            if (!this.NPC.noTileCollide && (Collision.SolidCollision(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height) || (double) ((Entity) this.NPC).position.Y + (double) ((Entity) this.NPC).height > (double) (Main.maxTilesY * 16 - 16)))
              ((Entity) this.NPC).velocity.Y = 0.0f;
            if ((double) ((Entity) this.NPC).velocity.Y != 0.0)
              break;
            if ((double) this.NPC.localAI[0] != 0.0)
            {
              this.NPC.localAI[0] = 0.0f;
              if (FargoSoulsUtil.HostCheck)
              {
                Projectile.NewProjectileDirect(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, 696, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<FuseBomb>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                if ((double) npc1.localAI[2] == 1.0 && WorldSavingSystem.EternityMode)
                {
                  for (int index = 0; index < 4; ++index)
                  {
                    Vector2 vector2 = Utils.RotatedBy(Vector2.Normalize(((Entity) this.NPC).oldVelocity), Math.PI / 2.0 * ((double) this.NPC.ai[3] < 0.0 ? (double) index : (double) index + 0.5), new Vector2());
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(1.5f, vector2), ModContent.ProjectileType<EarthPalladOrb>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  }
                }
                else
                {
                  Vector2 center6 = ((Entity) this.NPC).Center;
                  for (int index = 0; index <= 3; ++index)
                  {
                    int num3 = (int) center6.X / 16 + 250 * index / 16 * (int) -(double) this.NPC.ai[3];
                    int num4 = (int) center6.Y / 16;
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), (float) (num3 * 16 + 8), (float) (num4 * 16 + 8), 0.0f, 0.0f, ModContent.ProjectileType<EarthGeyser>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
                  }
                }
              }
            }
            ++this.NPC.localAI[1];
            if ((double) this.NPC.localAI[1] <= ((double) npc1.localAI[2] == 1.0 ? 20.0 : 30.0))
              break;
            this.NPC.netUpdate = true;
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.localAI[0] = 0.0f;
            this.NPC.localAI[1] = 0.0f;
            ((Entity) this.NPC).velocity = Vector2.Zero;
            for (int index = 0; index < Main.maxNPCs; ++index)
            {
              if (((Entity) Main.npc[index]).active && Main.npc[index].type == this.NPC.type && index != ((Entity) this.NPC).whoAmI && (double) Main.npc[index].ai[2] == (double) this.NPC.ai[2])
              {
                ((Entity) Main.npc[index]).velocity = Vector2.Zero;
                Main.npc[index].ai[0] = this.NPC.ai[0];
                Main.npc[index].ai[1] = this.NPC.ai[1];
                Main.npc[index].localAI[0] = this.NPC.localAI[0];
                Main.npc[index].localAI[1] = this.NPC.localAI[1];
                Main.npc[index].netUpdate = true;
                break;
              }
            }
            break;
          case 8f:
            this.NPC.noTileCollide = true;
            Vector2 targetPos1 = Vector2.op_Addition(((Entity) npc1).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, -this.NPC.ai[3]), (float) (((double) npc1.localAI[2] == 1.0 ? 450.0 : 600.0) + 500.0 * (1.0 - (double) Math.Min(1f, this.NPC.localAI[1] / 240f)))));
            if ((double) ((Entity) this.NPC).Distance(targetPos1) > 25.0)
              this.Movement(targetPos1, 0.8f, 32f);
            this.NPC.rotation = (float) (1.5707963705062866 * -(double) this.NPC.ai[3] + 3.1415927410125732);
            if ((double) ++this.NPC.localAI[1] > 90.0 && (double) this.NPC.localAI[1] % 6.0 == 0.0 && FargoSoulsUtil.HostCheck)
            {
              int index = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitY, Utils.NextFloat(Main.rand, 50f, 100f))), Vector2.Zero, ModContent.ProjectileType<MoonLordSunBlast>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, Utils.ToRotation(Vector2.UnitY), 16f, 0.0f);
              if (index != Main.maxProjectiles)
                Main.projectile[index].localAI[0] = 2f;
            }
            if ((double) this.NPC.ai[1] > 60.0)
            {
              if ((double) npc1.ai[0] == 1.0)
                break;
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.localAI[0] = 0.0f;
              this.NPC.localAI[1] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            ++this.NPC.ai[1];
            if ((double) npc1.ai[0] != 0.0)
              break;
            npc1.ai[0] = 1f;
            npc1.netUpdate = true;
            break;
          case 10f:
            if ((double) npc1.localAI[2] == 1.0)
            {
              NPC npc6 = this.NPC;
              ((Entity) npc6).position = Vector2.op_Addition(((Entity) npc6).position, Vector2.op_Division(((Entity) player).velocity, 2f));
            }
            if ((double) this.NPC.ai[3] > 0.0)
            {
              Vector2 center7 = ((Entity) player).Center;
              center7.Y = ((Entity) player).Center.Y - 400f;
              center7.X += ((Entity) player).velocity.X * 60f;
              if ((double) ((Entity) this.NPC).Distance(center7) > 50.0)
                this.Movement(center7, 0.6f, 32f);
              this.NPC.rotation = 1.57079637f;
            }
            else
            {
              Vector2 center8 = ((Entity) player).Center;
              center8.Y -= 300f;
              center8.X += 1000f * (float) Math.Sin(2.0 * Math.PI / 77.0 * (double) this.NPC.ai[1]);
              this.Movement(center8, 1.8f, 32f);
              this.NPC.rotation = -1.57079637f;
              this.NPC.localAI[0] += 0.5f;
            }
            if ((double) ++this.NPC.localAI[0] > 60.0 && (double) this.NPC.ai[1] > 120.0)
            {
              this.NPC.localAI[0] = 0.0f;
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitY, 2f), ModContent.ProjectileType<CrystalBomb>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, ((Entity) player).position.Y, 0.0f, 0.0f);
            }
            if ((double) ++this.NPC.ai[1] <= 600.0)
              break;
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.localAI[0] = 0.0f;
            this.NPC.netUpdate = true;
            break;
          default:
            this.NPC.ai[0] = 0.0f;
            goto case 0.0f;
        }
      }
    }

    public virtual void FindFrame(int frameHeight)
    {
      this.NPC.frame.Y = (double) this.NPC.localAI[3] == 1.0 ? 0 : frameHeight;
    }

    public virtual void ModifyIncomingHit(ref NPC.HitModifiers modifiers) => modifiers.Null();

    public virtual void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
    {
      if (!FargoSoulsUtil.CanDeleteProjectile(projectile))
        return;
      projectile.penetrate = 0;
      projectile.timeLeft = 0;
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
      target.AddBuff(24, 300, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(67, 300, true, false);
      target.AddBuff(ModContent.BuffType<LethargicBuff>(), 300, true, false);
    }

    public virtual bool CheckActive() => false;

    public virtual bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
      return new bool?(false);
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color alpha = this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects = this.NPC.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < NPCID.Sets.TrailCacheLength[this.NPC.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (NPCID.Sets.TrailCacheLength[this.NPC.type] - index) / (float) NPCID.Sets.TrailCacheLength[this.NPC.type]);
        Vector2 oldPo = this.NPC.oldPos[index];
        float rotation = this.NPC.rotation;
        Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), color, rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      }
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Earth/" + ((ModType) this).Name + "_Glow", (AssetRequestMode) 1).Value;
      if (this.NPC.dontTakeDamage)
      {
        Vector2 vector2_2 = Vector2.op_Multiply(Vector2.UnitX, Utils.NextFloat(Main.rand, -180f, 180f));
        Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.NPC).Center, vector2_2), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.op_Multiply(this.NPC.GetAlpha(drawColor), 0.5f), this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
        Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.NPC).Center, vector2_2), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.op_Multiply(this.NPC.GetAlpha(drawColor), 0.5f), this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.White, this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
