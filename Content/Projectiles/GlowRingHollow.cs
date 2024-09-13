// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.GlowRingHollow
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Life;
using FargowiltasSouls.Content.Bosses.Champions.Terra;
using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.Projectiles.ChallengerItems;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class GlowRingHollow : ModProjectile
  {
    public Color color = Color.White;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = 2400;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 10;
      ((Entity) this.Projectile).height = 10;
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

    public virtual void AI()
    {
      this.Projectile.timeLeft = 2;
      float num1 = 500f;
      int num2 = 60;
      int num3 = 3;
      switch (this.Projectile.ai[0])
      {
        case 1f:
          this.Projectile.FargoSouls().TimeFreezeImmune = true;
          this.color = Color.Red;
          num1 = 525f;
          num2 = 90;
          num3 = 2;
          break;
        case 2f:
          this.Projectile.FargoSouls().TimeFreezeImmune = true;
          this.color = Color.Green;
          num1 = 350f;
          num2 = 90;
          num3 = 2;
          break;
        case 3f:
          this.color = Color.Yellow;
          num2 = 180;
          num3 = 10;
          if ((double) this.Projectile.localAI[0] > (double) (num2 / 2))
          {
            num3 = -1;
            this.Projectile.alpha = 0;
          }
          NPC npc1 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>());
          if (npc1 != null)
          {
            ((Entity) this.Projectile).Center = ((Entity) npc1).Center;
            num1 = (float) (1400.0 * ((double) num2 - (double) this.Projectile.localAI[0])) / (float) num2;
            break;
          }
          this.Projectile.Kill();
          return;
        case 4f:
          this.color = Color.Cyan;
          num1 = 1200f;
          num2 = 360;
          break;
        case 5f:
          this.Projectile.FargoSouls().TimeFreezeImmune = true;
          this.color = FargoSoulsUtil.AprilFools ? Color.Red : new Color(51, (int) byte.MaxValue, 191);
          num2 = 120;
          num1 = 1200f * (float) Math.Cos(Math.PI / 2.0 * (double) this.Projectile.localAI[0] / (double) num2);
          num3 = -1;
          this.Projectile.alpha = 0;
          break;
        case 6f:
          this.color = Color.Purple;
          num2 = 120;
          num3 = (double) this.Projectile.localAI[0] > (double) (num2 / 2) ? 10 : 1;
          NPC npc2 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 134);
          if (npc2 != null)
          {
            ((Entity) this.Projectile).Center = ((Entity) npc2).Center;
            num1 = (float) (1200.0 * ((double) num2 - (double) this.Projectile.localAI[0])) / (float) num2;
            break;
          }
          this.Projectile.Kill();
          return;
        case 7f:
          this.color = Color.Yellow;
          num3 = 10;
          NPC npc3 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<LifeChampion>());
          if (npc3 != null && (double) npc3.ai[3] == 0.0)
          {
            ((Entity) this.Projectile).Center = ((Entity) npc3).Center;
            num2 = (double) npc3.localAI[2] == 1.0 ? 30 : 60;
            if ((double) npc3.ai[1] == 0.0)
              this.Projectile.localAI[0] = 0.0f;
            num1 = (float) (1800.0 * ((double) num2 - (double) this.Projectile.localAI[0])) / (float) num2;
            break;
          }
          this.Projectile.Kill();
          return;
        case 8f:
          this.color = Color.Red;
          num2 = 60;
          num3 = 3;
          num1 = this.Projectile.ai[1] * (float) Math.Sqrt(Math.Sin(Math.PI / 2.0 * (double) this.Projectile.localAI[0] / (double) num2));
          break;
        case 9f:
          this.color = Color.Yellow;
          num2 = 120;
          num3 = (double) this.Projectile.localAI[0] > (double) (num2 / 2) ? 10 : 1;
          NPC npc4 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 134);
          if (npc4 != null)
          {
            ((Entity) this.Projectile).Center = ((Entity) npc4).Center;
            num1 = (float) (1200.0 * ((double) num2 - (double) this.Projectile.localAI[0])) / (float) num2;
            break;
          }
          this.Projectile.Kill();
          return;
        case 10f:
          this.color = Color.Violet;
          num2 = 90;
          num3 = 10;
          NPC npc5 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 507);
          if (npc5 != null)
          {
            if ((double) this.Projectile.localAI[0] == (double) num2)
            {
              ((Entity) npc5).Center = ((Entity) this.Projectile).Center;
              for (int index1 = 0; index1 < 100; ++index1)
              {
                int index2 = Dust.NewDust(((Entity) npc5).position, ((Entity) npc5).width, ((Entity) npc5).height, 86, 0.0f, 0.0f, 0, new Color(), 4f);
                Dust dust = Main.dust[index2];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
                Main.dust[index2].noGravity = true;
              }
              if (FargoSoulsUtil.HostCheck)
              {
                for (int index3 = -2; index3 <= 2; ++index3)
                {
                  Vector2 vector2 = Vector2.op_Addition(((Entity) npc5).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) index3), 1000f));
                  for (int index4 = -1; index4 <= 1; index4 += 2)
                    Projectile.NewProjectile(((Entity) npc5).GetSource_FromThis((string) null), vector2, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), 0, 0.0f, Main.myPlayer, 15f, 1.57079637f * (float) index4, 0.0f);
                }
              }
            }
            num1 = (float) (1200.0 * ((double) num2 - (double) this.Projectile.localAI[0])) / (float) num2;
            break;
          }
          this.Projectile.Kill();
          return;
        case 11f:
          this.color = Color.Red;
          if ((double) this.Projectile.localAI[0] > (double) (num2 / 2))
            this.Projectile.localAI[0] = (float) (num2 / 2);
          NPC npc6 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 125);
          if (npc6 != null)
          {
            ((Entity) this.Projectile).Center = ((Entity) npc6).Center;
            num1 = (float) (2000.0 - (double) (1200 * npc6.GetGlobalNPC<Retinazer>().AuraRadiusCounter) / 180.0);
            if (WorldSavingSystem.MasochistModeReal)
              num1 *= 0.75f;
            if ((double) num1 == 2000.0)
            {
              this.Projectile.localAI[0] = -1f;
              break;
            }
            break;
          }
          this.Projectile.Kill();
          return;
        case 12f:
          this.color = Color.OrangeRed;
          num2 = 210;
          num3 = (double) this.Projectile.localAI[0] > (double) (num2 / 2) ? 10 : 1;
          NPC npc7 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<TerraChampion>());
          if (npc7 != null)
          {
            ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc7).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(((Entity) npc7).velocity), 1.5707963705062866, new Vector2()), 300f));
            num1 = (float) (2000.0 * (1.0 - (double) this.Projectile.localAI[0] / (double) num2));
            break;
          }
          this.Projectile.Kill();
          return;
        case 13f:
          this.color = Color.Orange;
          num1 = 2000f;
          if ((double) this.Projectile.localAI[0] > (double) (num2 / 2))
            this.Projectile.localAI[0] = (float) (num2 / 2);
          NPC npc8 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 113);
          if (npc8 != null)
          {
            ((Entity) this.Projectile).Center = ((Entity) npc8).Center;
            break;
          }
          this.Projectile.Kill();
          return;
        case 14f:
          this.color = Color.Red;
          num1 = (float) (DecrepitAirstrikeNuke.ExplosionDiameter / 2);
          num2 = (int) this.Projectile.ai[1];
          if (this.Projectile.timeLeft > num2)
            this.Projectile.timeLeft = num2;
          num3 = 3;
          break;
        default:
          Main.NewText("glow ring hollow: you shouldnt be seeing this text, show terry", byte.MaxValue, byte.MaxValue, byte.MaxValue);
          break;
      }
      if ((double) ++this.Projectile.localAI[0] > (double) num2)
      {
        this.Projectile.Kill();
      }
      else
      {
        if (num3 >= 0)
        {
          this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * Math.Sin(Math.PI / (double) num2 * (double) this.Projectile.localAI[0])) * num3;
          if (this.Projectile.alpha < 0)
            this.Projectile.alpha = 0;
        }
        ((Color) ref this.color).A = (byte) 0;
        this.Projectile.scale = (float) ((double) num1 * 2.0 / 1000.0);
      }
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
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
