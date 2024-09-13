// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.GlowRing
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class GlowRing : ModProjectile
  {
    public Color color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = 2400;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 64;
      ((Entity) this.Projectile).height = 64;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.hostile = true;
      this.Projectile.alpha = 0;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0]);
      if (npc != null)
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
      float num1 = 12f;
      int num2 = 30;
      bool flag = false;
      switch ((int) this.Projectile.ai[1])
      {
        case -24:
          num2 = 60;
          float num3 = this.Projectile.localAI[0] / (float) num2;
          num1 = (float) (1.0 + 50.0 * (double) num3);
          this.color = Color.Blue;
          this.Projectile.alpha = (int) ((double) byte.MaxValue * (1.0 - (double) num3));
          ((Color) ref this.color).A = (byte) 0;
          break;
        case -23:
          flag = true;
          num2 = 90;
          float num4 = this.Projectile.localAI[0] / (float) num2;
          float num5 = 1f - num4;
          this.color = new Color((int) byte.MaxValue, 105, 180);
          this.Projectile.alpha = (int) ((double) byte.MaxValue * (double) num5);
          this.Projectile.scale = (float) (0.20000000298023224 + 0.800000011920929 * (double) num4);
          if (npc != null)
          {
            Projectile projectile = this.Projectile;
            ((Entity) projectile).Center = Vector2.op_Addition(((Entity) projectile).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitY, (double) npc.rotation, new Vector2()), 15f));
          }
          Vector2 worldPosition1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 6.2831854820251465), 120f * num5 * Utils.NextFloat(Main.rand, 0.6f, 1.3f)), 2f));
          float lifetime1 = 15f;
          Vector2 velocity1 = Vector2.op_Division(Vector2.op_Subtraction(((Entity) this.Projectile).Center, worldPosition1), lifetime1);
          float scale1 = (float) (2.0 - (double) num4 * 1.2000000476837158);
          new SparkParticle(worldPosition1, velocity1, this.color, scale1, (int) lifetime1).Spawn();
          break;
        case -22:
          flag = true;
          num2 = 645;
          if (npc != null && npc.type == 114 && (npc.GetGlobalNPC<WallofFleshEye>().HasTelegraphedNormalLasers || Main.netMode == 1))
          {
            this.Projectile.rotation = npc.rotation + (((Entity) npc).direction > 0 ? 0.0f : 3.14159274f);
            ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(this.Projectile.rotation);
            ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply((float) (((Entity) npc).width - 52), Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.rotation, new Vector2())));
            if ((double) this.Projectile.localAI[0] < (double) npc.localAI[1])
              this.Projectile.localAI[0] = (float) (int) npc.localAI[1];
            float num6 = (float) Math.Cos(Math.PI / 2.0 / (double) num2 * (double) this.Projectile.localAI[0]);
            this.color = Color.op_Multiply(new Color((int) byte.MaxValue, 0, (int) byte.MaxValue, 100), 1f - num6);
            this.Projectile.alpha = (int) ((double) byte.MaxValue * (double) num6);
            this.Projectile.scale = 18f * num6;
            break;
          }
          this.Projectile.Kill();
          return;
        case -21:
          num1 = 4f;
          num2 = 60;
          break;
        case -20:
          flag = true;
          num2 = 200;
          float num7 = this.Projectile.localAI[0] / (float) num2;
          float num8 = 1f - num7;
          this.color = new Color((int) byte.MaxValue, 105, 180);
          this.Projectile.alpha = (int) ((double) byte.MaxValue * (double) num8);
          this.Projectile.scale = (float) (0.20000000298023224 + 0.800000011920929 * (double) num7);
          if (npc != null)
          {
            Projectile projectile = this.Projectile;
            ((Entity) projectile).Center = Vector2.op_Addition(((Entity) projectile).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitY, (double) npc.rotation, new Vector2()), 15f));
          }
          Vector2 worldPosition2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 6.2831854820251465), 120f * num8 * Utils.NextFloat(Main.rand, 0.6f, 1.3f)));
          float lifetime2 = 15f;
          Vector2 velocity2 = Vector2.op_Division(Vector2.op_Subtraction(((Entity) this.Projectile).Center, worldPosition2), lifetime2);
          float scale2 = (float) (2.0 - (double) num7 * 1.2000000476837158);
          new SparkParticle(worldPosition2, velocity2, this.color, scale2, (int) lifetime2).Spawn();
          break;
        case -19:
          this.color = Color.Yellow;
          ((Color) ref this.color).A = (byte) 0;
          num1 = 18f;
          break;
        case -18:
          num1 = 36f;
          num2 = 120;
          break;
        case -17:
          num1 = 6f;
          goto case -16;
        case -16:
          this.color = new Color((int) byte.MaxValue, 51, 153, 0);
          break;
        case -15:
          num1 = 18f;
          goto case -16;
        case -14:
          num1 = 24f;
          goto case -16;
        case -13:
          this.color = new Color(93, (int) byte.MaxValue, 241, 0);
          num1 = 6f;
          num2 = 15;
          break;
        case -12:
          this.color = new Color(0, 0, (int) byte.MaxValue, 0);
          num2 = 45;
          break;
        case -11:
          this.color = new Color(0, (int) byte.MaxValue, 0, 0);
          num2 = 45;
          break;
        case -10:
          this.color = new Color(0, (int) byte.MaxValue, (int) byte.MaxValue, 0);
          num2 = 45;
          break;
        case -9:
          this.color = new Color((int) byte.MaxValue, (int) byte.MaxValue, 0, 0);
          num2 = 45;
          break;
        case -8:
          this.color = new Color((int) byte.MaxValue, (int) sbyte.MaxValue, 40, 0);
          num2 = 45;
          break;
        case -7:
          this.color = new Color((int) byte.MaxValue, 0, 0, 0);
          num2 = 45;
          break;
        case -6:
          this.color = new Color((int) byte.MaxValue, (int) byte.MaxValue, 0, 0);
          num1 = 18f;
          break;
        case -5:
          this.color = new Color(200, 0, (int) byte.MaxValue, 0);
          num1 = 18f;
          break;
        case -4:
          this.color = new Color((int) byte.MaxValue, (int) byte.MaxValue, 0, 0);
          num1 = 18f;
          num2 = 60;
          break;
        case -3:
          this.color = new Color((int) byte.MaxValue, 100, 0, 0);
          num1 = 18f;
          num2 = 60;
          break;
        case -2:
          this.color = new Color(51, (int) byte.MaxValue, 191, 0);
          num1 = 18f;
          break;
        case -1:
          this.color = new Color(200, 0, 200, 0);
          num2 = 60;
          break;
        case 4:
          this.color = new Color(51, (int) byte.MaxValue, 191, 0);
          num2 = 45;
          break;
        case 114:
          this.color = new Color(93, (int) byte.MaxValue, 241, 0);
          num1 = 12f;
          num2 = 30;
          break;
        case 125:
          this.color = new Color((int) byte.MaxValue, 0, 0, 0);
          num1 = 24f;
          num2 = 60;
          break;
        case 128:
        case 129:
        case 130:
        case 131:
          this.color = new Color(51, (int) byte.MaxValue, 191, 0);
          num1 = 12f;
          num2 = 30;
          break;
        case 222:
          this.color = new Color((int) byte.MaxValue, (int) byte.MaxValue, 100, 0);
          num2 = 45;
          break;
        case 396:
        case 397:
        case 398:
          this.color = new Color(51, (int) byte.MaxValue, 191, 0);
          num1 = 12f;
          num2 = 60;
          break;
        case 439:
          this.color = new Color((int) byte.MaxValue, (int) sbyte.MaxValue, 40, 0);
          break;
        case 657:
          this.color = Color.HotPink;
          ((Color) ref this.color).A = (byte) 200;
          num1 = 6f;
          num2 = 60;
          if ((double) this.Projectile.localAI[0] > (double) num2 * 0.25 && NPC.AnyNPCs(ModContent.NPCType<GelatinSubject>()))
            this.Projectile.localAI[0] = (float) num2 * 0.25f;
          if (npc != null)
          {
            ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Bottom, Vector2.op_Multiply((float) (((Entity) npc).height / 2), Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) npc.rotation, new Vector2()))));
            break;
          }
          break;
        case 668:
          this.color = npc.life < npc.lifeMax / 3 ? Color.Red : Color.LightSkyBlue;
          ((Color) ref this.color).A = (byte) 0;
          num1 = 9f;
          num2 = 30;
          if (npc != null)
          {
            ((Entity) this.Projectile).Center = ((Entity) npc).direction < 0 ? ((Entity) npc).TopLeft : ((Entity) npc).TopRight;
            break;
          }
          break;
        default:
          Main.NewText("glow ring: you shouldnt be seeing this text, show terry", byte.MaxValue, byte.MaxValue, byte.MaxValue);
          break;
      }
      if ((double) ++this.Projectile.localAI[0] > (double) num2)
      {
        this.Projectile.Kill();
      }
      else
      {
        if (!flag)
        {
          this.Projectile.scale = num1 * (float) Math.Sin(Math.PI / 2.0 * (double) this.Projectile.localAI[0] / (double) num2);
          this.Projectile.alpha = (int) ((double) byte.MaxValue * (double) this.Projectile.localAI[0] / (double) num2);
        }
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
        if (this.Projectile.alpha <= (int) byte.MaxValue)
          return;
        this.Projectile.alpha = (int) byte.MaxValue;
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(this.color, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      if ((double) this.Projectile.ai[1] == 657.0)
      {
        Main.spriteBatch.End();
        Main.spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, (Effect) null, Main.Transform);
        GameShaders.Misc["HallowBoss"].Apply(new DrawData?());
      }
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = texture2D.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      if ((double) this.Projectile.ai[1] == 657.0)
      {
        Main.spriteBatch.End();
        Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      return false;
    }
  }
}
