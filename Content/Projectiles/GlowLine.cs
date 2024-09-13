// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.GlowLine
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class GlowLine : ModProjectile
  {
    public Color color = Color.White;
    private int counter;
    private int drawLayers = 1;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = 2400;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.hostile = true;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.hide = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      behindProjectiles.Add(index);
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.counter);
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.Projectile.localAI[1]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.counter = reader.ReadInt32();
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.Projectile.localAI[1] = reader.ReadSingle();
    }

    public virtual void AI()
    {
      int num1 = 60;
      float num2 = 3f;
      switch ((int) this.Projectile.ai[0])
      {
        case 0:
          this.color = Color.Yellow;
          num1 = 30;
          num2 = 10f;
          NPC npc1 = FargoSoulsUtil.NPCExists(this.Projectile.localAI[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>());
          if (npc1 != null)
          {
            ((Entity) this.Projectile).Center = ((Entity) npc1).Center;
            this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc1, ((Entity) Main.player[npc1.target]).Center)) + this.Projectile.ai[1];
            break;
          }
          break;
        case 1:
          this.color = Color.Yellow;
          num1 = 150;
          this.Projectile.rotation = this.Projectile.ai[1];
          num2 = 1f;
          if (this.counter < 90)
          {
            num2 = 0.0f;
            break;
          }
          ((Entity) this.Projectile).velocity = Vector2.Zero;
          break;
        case 2:
          this.color = Color.HotPink;
          num1 = 90;
          this.Projectile.scale = 0.5f;
          this.Projectile.rotation = this.Projectile.ai[1];
          num2 = 0.5f;
          if (Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero))
          {
            if (this.counter == 0)
              this.Projectile.localAI[1] = -((Vector2) ref ((Entity) this.Projectile).velocity).Length() / (float) num1;
            float num3 = ((Vector2) ref ((Entity) this.Projectile).velocity).Length() + this.Projectile.localAI[1];
            ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), num3);
            break;
          }
          break;
        case 3:
          this.color = Color.Yellow;
          num1 = 60;
          num2 = 6f;
          NPC npc2 = FargoSoulsUtil.NPCExists(this.Projectile.localAI[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>());
          if (npc2 != null)
          {
            ((Entity) this.Projectile).Center = ((Entity) npc2).Center;
            if (this.counter == 0)
              this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc2, ((Entity) Main.player[npc2.target]).Center));
            float num4 = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc2, ((Entity) Main.player[npc2.target]).Center)) + this.Projectile.ai[1];
            while ((double) num4 < -3.1415927410125732)
              num4 += 6.28318548f;
            while ((double) num4 > 3.1415927410125732)
              num4 -= 6.28318548f;
            this.Projectile.rotation = Utils.AngleLerp(this.Projectile.rotation, num4, 0.05f);
            break;
          }
          break;
        case 4:
          this.color = Color.Yellow;
          num1 = 150;
          num2 = 7f;
          NPC npc3 = FargoSoulsUtil.NPCExists(this.Projectile.localAI[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>());
          if (npc3 != null)
          {
            ((Entity) this.Projectile).Center = ((Entity) npc3).Center;
            float num5 = this.Projectile.ai[1];
            while ((double) num5 < -3.1415927410125732)
              num5 += 6.28318548f;
            while ((double) num5 > 3.1415927410125732)
              num5 -= 6.28318548f;
            ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(Utils.AngleLerp(Utils.ToRotation(((Entity) this.Projectile).velocity), num5, 0.05f));
          }
          Projectile projectile1 = this.Projectile;
          ((Entity) projectile1).position = Vector2.op_Subtraction(((Entity) projectile1).position, ((Entity) this.Projectile).velocity);
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          break;
        case 5:
          this.color = new Color(0.0f, 1f, 1f);
          num1 = 120;
          num2 = 4f;
          NPC npc4 = FargoSoulsUtil.NPCExists(this.Projectile.localAI[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>());
          if (npc4 != null)
            ((Entity) this.Projectile).Center = Vector2.Lerp(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) npc4).Center, Vector2.op_Multiply(Vector2.UnitX, this.Projectile.ai[1])), 0.03f);
          Projectile projectile2 = this.Projectile;
          ((Entity) projectile2).position = Vector2.op_Subtraction(((Entity) projectile2).position, ((Entity) this.Projectile).velocity);
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          break;
        case 6:
          this.Projectile.FargoSouls().TimeFreezeImmune = true;
          this.color = new Color(51, (int) byte.MaxValue, 191);
          num1 = 90;
          Player player = FargoSoulsUtil.PlayerExists(this.Projectile.ai[1]);
          if (player != null)
            this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center));
          else
            this.Projectile.ai[1] = (float) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
          Projectile projectile3 = this.Projectile;
          ((Entity) projectile3).position = Vector2.op_Subtraction(((Entity) projectile3).position, ((Entity) this.Projectile).velocity);
          this.Projectile.rotation += Utils.ToRotation(((Entity) this.Projectile).velocity);
          break;
        case 7:
          this.Projectile.FargoSouls().TimeFreezeImmune = true;
          Color color;
          switch ((int) this.Projectile.ai[1])
          {
            case 0:
              color = Color.Magenta;
              break;
            case 1:
              color = Color.Orange;
              break;
            case 2:
              color = new Color(51, (int) byte.MaxValue, 191);
              break;
            default:
              color = Color.SkyBlue;
              break;
          }
          this.color = color;
          num1 = 20;
          num2 = -1f;
          this.Projectile.alpha = 0;
          this.Projectile.scale = 0.5f;
          Projectile projectile4 = this.Projectile;
          ((Entity) projectile4).position = Vector2.op_Subtraction(((Entity) projectile4).position, ((Entity) this.Projectile).velocity);
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          if (this.counter == num1 && FargoSoulsUtil.HostCheck)
          {
            for (int index = 0; index < 4; ++index)
            {
              Vector2 vector2 = Vector2.op_Multiply((float) (8.0 * (double) (index + 1) + 4.0), ((Entity) this.Projectile).velocity);
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2, ModContent.ProjectileType<CelestialFragment>(), this.Projectile.damage, 0.0f, Main.myPlayer, this.Projectile.ai[1], 0.0f, 0.0f);
            }
            break;
          }
          break;
        case 8:
          this.color = new Color(51, (int) byte.MaxValue, 191, 0);
          num1 = 60;
          NPC npc5 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 128, 131, 129, 130);
          if (npc5 != null)
          {
            ((Entity) this.Projectile).Center = ((Entity) npc5).Center;
            this.Projectile.rotation = npc5.rotation + 1.57079637f;
            Projectile projectile5 = this.Projectile;
            ((Entity) projectile5).position = Vector2.op_Subtraction(((Entity) projectile5).position, ((Entity) this.Projectile).velocity);
            this.Projectile.rotation += Utils.ToRotation(((Entity) this.Projectile).velocity);
            break;
          }
          this.Projectile.Kill();
          return;
        case 9:
          this.color = Color.Red;
          num1 = 120;
          num2 = 2f;
          NPC npc6 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 125);
          if (npc6 != null)
          {
            Vector2 vector2 = Utils.RotatedBy(new Vector2((float) (((Entity) npc6).width - 24), 0.0f), (double) npc6.rotation + 1.57079633, new Vector2());
            ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc6).Center, vector2);
            this.Projectile.rotation = npc6.rotation + 1.57079637f;
            break;
          }
          this.Projectile.Kill();
          return;
        case 10:
          this.color = Color.Purple;
          num1 = 90;
          num2 = 1f;
          this.Projectile.scale = 0.5f;
          NPC npc7 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>());
          if (npc7 != null)
          {
            ((Entity) this.Projectile).Center = ((Entity) npc7).Center;
            this.Projectile.rotation = npc7.localAI[0];
            break;
          }
          this.Projectile.Kill();
          return;
        case 11:
          num1 = 90;
          num2 = -1f;
          this.Projectile.Opacity = Math.Clamp((float) this.counter / (float) num1, 0.0f, 1f);
          this.Projectile.scale = 0.6f;
          NPC npc8 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 135, 136);
          if (npc8 == null)
          {
            this.Projectile.Kill();
            return;
          }
          NPC npc9 = FargoSoulsUtil.NPCExists(npc8.realLife, new int[1]
          {
            134
          });
          if (npc9 == null || npc9.GetGlobalNPC<Destroyer>().IsCoiling)
          {
            this.Projectile.Kill();
            return;
          }
          if (this.counter == 0)
            this.Projectile.localAI[0] = Utils.NextFloat(Main.rand, 0.9f, 1.1f);
          this.color = (double) npc8.ai[2] == 0.0 ? Color.Cyan : Color.Blue;
          if (!WorldSavingSystem.EternityMode && SoulConfig.Instance.BossRecolors)
            this.color = (double) npc8.ai[2] == 0.0 ? Color.DarkRed : Color.OrangeRed;
          ((Entity) this.Projectile).Center = ((Entity) npc8).Center;
          float num6 = (float) ((1.0 - (double) this.Projectile.localAI[0]) * 10.0);
          this.Projectile.localAI[1] += MathHelper.ToRadians(WorldSavingSystem.MasochistModeReal ? 60f : 30f) * num6 / (float) num1;
          this.Projectile.rotation = this.Projectile.localAI[1];
          if (FargoSoulsUtil.HostCheck)
          {
            if ((double) npc8.ai[2] == 0.0)
            {
              if (this.counter == num1)
              {
                if (!WorldSavingSystem.MasochistModeReal)
                  Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Utils.ToRotationVector2(this.Projectile.rotation), this.Projectile.type, this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 16f, 0.0f, 0.0f);
                Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(this.Projectile.localAI[0], Utils.ToRotationVector2(this.Projectile.rotation)), ModContent.ProjectileType<DestroyerLaser>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 134f, 0.0f);
                break;
              }
              break;
            }
            if (this.counter > num1 - 20 && this.counter % 10 == 0)
            {
              if (!WorldSavingSystem.MasochistModeReal)
                Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Utils.ToRotationVector2(this.Projectile.rotation), this.Projectile.type, this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 16f, 0.0f, 0.0f);
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(this.Projectile.localAI[0], Utils.ToRotationVector2(this.Projectile.rotation)), ModContent.ProjectileType<MechElectricOrbHoming>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, -1f, 1f, 1f);
              break;
            }
            break;
          }
          break;
        case 12:
          this.color = Color.Purple;
          num1 = 645;
          this.drawLayers = 4;
          num2 = -1f;
          NPC npc10 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 114);
          if (npc10 != null && (npc10.GetGlobalNPC<WallofFleshEye>().HasTelegraphedNormalLasers || Main.netMode == 1))
          {
            this.Projectile.rotation = npc10.rotation + (((Entity) npc10).direction > 0 ? 0.0f : 3.14159274f);
            ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(this.Projectile.rotation);
            ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc10).Center, Vector2.op_Multiply((float) (((Entity) npc10).width - 52), Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.rotation, new Vector2())));
            if ((double) this.counter < (double) npc10.localAI[1])
              this.counter = (int) npc10.localAI[1];
            this.Projectile.alpha = (int) ((double) byte.MaxValue * Math.Cos(Math.PI / 2.0 / (double) num1 * (double) this.counter));
            break;
          }
          this.Projectile.Kill();
          return;
        case 13:
          this.color = FargoSoulsUtil.AprilFools ? Color.Yellow : new Color(51, (int) byte.MaxValue, 191);
          num1 = 90;
          num2 = this.counter > num1 / 2 ? 6f : 3f;
          this.Projectile.scale = 4f;
          NPC npc11 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>());
          if (npc11 != null)
          {
            float num7 = MathHelper.WrapAngle(npc11.ai[3]);
            ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(Utils.AngleLerp(Utils.ToRotation(((Entity) this.Projectile).velocity), num7, 0.12f * (float) Math.Pow((double) this.counter / (double) num1, 3.0)));
          }
          Projectile projectile6 = this.Projectile;
          ((Entity) projectile6).position = Vector2.op_Subtraction(((Entity) projectile6).position, ((Entity) this.Projectile).velocity);
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          break;
        case 14:
          this.color = new Color(51, (int) byte.MaxValue, 191);
          num1 = 180;
          num2 = 5f;
          Projectile projectile7 = FargoSoulsUtil.ProjectileExists(FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, this.Projectile.ai[1], ModContent.ProjectileType<MoonLordVortex>()), Array.Empty<int>());
          if (projectile7 != null)
          {
            ((Entity) this.Projectile).Center = ((Entity) projectile7).Center;
            Projectile projectile8 = this.Projectile;
            ((Entity) projectile8).position = Vector2.op_Subtraction(((Entity) projectile8).position, ((Entity) this.Projectile).velocity);
            this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
            break;
          }
          if (FargoSoulsUtil.HostCheck)
          {
            this.Projectile.Kill();
            return;
          }
          break;
        case 15:
          this.color = Color.Purple;
          num1 = 270;
          num2 = 4f;
          this.drawLayers = 4;
          this.Projectile.scale = 24f;
          this.Projectile.rotation = this.Projectile.ai[1];
          if (((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) Main.LocalPlayer).Hitbox))
          {
            Main.LocalPlayer.AddBuff(164, 2, true, false);
            break;
          }
          break;
        case 16:
          this.color = Color.SkyBlue;
          num1 = 30;
          num2 = -1f;
          this.Projectile.Opacity = Math.Clamp((float) (1.0 - (double) this.counter / (double) num1), 0.0f, 1f);
          this.Projectile.scale = 0.6f;
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          Projectile projectile9 = this.Projectile;
          ((Entity) projectile9).position = Vector2.op_Subtraction(((Entity) projectile9).position, ((Entity) this.Projectile).velocity);
          break;
        case 17:
          this.color = Color.Purple;
          num1 = 270;
          num2 = 2f;
          this.drawLayers = 2;
          this.Projectile.scale = 24f;
          NPC npc12 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 398);
          if (npc12 == null)
          {
            this.Projectile.Kill();
            return;
          }
          if (this.counter == 0)
          {
            for (int index = 0; index < Main.maxProjectiles; ++index)
            {
              if (((Entity) Main.projectile[index]).active && Main.projectile[index].type == ModContent.ProjectileType<LunarRitual>() && (double) Main.projectile[index].ai[1] == (double) ((Entity) npc12).whoAmI)
              {
                this.Projectile.localAI[1] = (float) index;
                break;
              }
            }
          }
          Projectile projectile10 = FargoSoulsUtil.ProjectileExists(this.Projectile.localAI[1], ModContent.ProjectileType<LunarRitual>());
          if (projectile10 != null && (double) projectile10.ai[1] == (double) ((Entity) npc12).whoAmI)
          {
            ((Entity) this.Projectile).Center = ((Entity) projectile10).Center;
            ((Entity) this.Projectile).position.X += this.Projectile.localAI[0];
            ((Entity) this.Projectile).position.Y += 1500f;
          }
          this.Projectile.rotation = -1.57079637f;
          if (npc12.GetGlobalNPC<MoonLordCore>().VulnerabilityState <= 2)
          {
            if (this.counter > num1 / 2)
              this.counter = num1 / 2;
          }
          else if (this.counter < num1 - 60)
            this.counter = num1 - 60;
          if (((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) Main.LocalPlayer).Hitbox))
          {
            Main.LocalPlayer.AddBuff(164, 2, true, false);
            break;
          }
          break;
        case 18:
          this.color = Color.op_Multiply(Color.Cyan, 0.75f);
          num1 = 120;
          num2 = 1f;
          NPC npc13 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 439);
          if (npc13 != null)
          {
            if (this.counter > num1 / 2)
              this.counter = num1 / 2;
            float num8 = (float) this.counter / (float) (num1 / 2);
            this.Projectile.scale = (float) (0.5 + 2.5 * (double) num8);
            if ((double) npc13.ai[0] == 5.0)
            {
              if (this.counter > 0 && (double) npc13.ai[1] == 1.0 && FargoSoulsUtil.HostCheck)
              {
                this.Projectile.Kill();
                return;
              }
              int index = (int) npc13.ai[2];
              if (index > -1 && index < Main.maxProjectiles && ((Entity) Main.projectile[index]).active && Main.projectile[index].type == 490)
              {
                if (this.counter == 0)
                {
                  Vector2 vector2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, ((Entity) Main.projectile[index]).Center);
                  this.Projectile.localAI[0] = vector2.X;
                  this.Projectile.localAI[1] = vector2.Y;
                }
                ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) Main.projectile[index]).Center, Vector2.op_Multiply(new Vector2(this.Projectile.localAI[0], this.Projectile.localAI[1]), num8));
              }
            }
          }
          Projectile projectile11 = this.Projectile;
          ((Entity) projectile11).position = Vector2.op_Subtraction(((Entity) projectile11).position, ((Entity) this.Projectile).velocity);
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          break;
        case 19:
          this.color = Color.op_Multiply(new Color(93, (int) byte.MaxValue, 241, 0), 0.75f);
          num2 = 1f;
          this.Projectile.scale = 2f;
          num1 = 40;
          if (this.counter < num1 / 2)
            this.counter = num1 / 2;
          Projectile projectile12 = this.Projectile;
          ((Entity) projectile12).position = Vector2.op_Subtraction(((Entity) projectile12).position, ((Entity) this.Projectile).velocity);
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          break;
        default:
          Main.NewText("glow line: you shouldnt be seeing this text, show terry", byte.MaxValue, byte.MaxValue, byte.MaxValue);
          break;
      }
      if (++this.counter > num1)
      {
        this.Projectile.Kill();
      }
      else
      {
        if ((double) num2 >= 0.0)
        {
          this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * Math.Sin(Math.PI / (double) num1 * (double) this.counter) * (double) num2);
          if (this.Projectile.alpha < 0)
            this.Projectile.alpha = 0;
        }
        ((Color) ref this.color).A = (byte) 0;
      }
    }

    public virtual void OnKill(int timeLeft) => base.OnKill(timeLeft);

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (((Rectangle) ref projHitbox).Intersects(targetHitbox))
        return new bool?(true);
      float num = 0.0f;
      return Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), ((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 3000f)), 16f * this.Projectile.scale, ref num) ? new bool?(true) : new bool?(false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(this.color, this.Projectile.Opacity), (float) Main.mouseTextColor / (float) byte.MaxValue), 0.9f));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = texture2D.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle1;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle1).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle1), 2f);
      Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 3000f), 2f);
      Vector2 vector2_3 = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenLastPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), vector2_2);
      Rectangle rectangle2;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle2).\u002Ector((int) vector2_3.X, (int) vector2_3.Y, 3000, (int) ((double) rectangle1.Height * (double) this.Projectile.scale / 5.3333334922790527));
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index = 0; index < this.drawLayers; ++index)
        Main.EntitySpriteDraw(new DrawData(texture2D, rectangle2, new Rectangle?(rectangle1), alpha, this.Projectile.rotation, vector2_1, (SpriteEffects) 0, 0.0f));
      return false;
    }
  }
}
