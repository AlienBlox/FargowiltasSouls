// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.Void
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class Void : ModProjectile
  {
    private int timer;

    public virtual string Texture => "Terraria/Images/Projectile_578";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.aiStyle = 0;
      this.Projectile.scale = 1f;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 600;
      this.Projectile.tileCollide = false;
      this.AIType = 14;
    }

    public virtual void AI()
    {
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] <= 40.0)
      {
        this.Projectile.scale = (float) ((double) this.Projectile.ai[0] / 40.0 * 3.0);
        this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale / 3.0);
        this.Projectile.rotation -= 0.1570796f;
        if (Utils.NextBool(Main.rand))
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
          dust.noGravity = true;
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, (float) Main.rand.Next(10, 21)));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, 1.5707963705062866, new Vector2()), 6f);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.fadeIn = 0.5f;
          dust.customData = (object) ((Entity) this.Projectile).Center;
        }
        if (Utils.NextBool(Main.rand))
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
          dust.noGravity = true;
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, -1.5707963705062866, new Vector2()), 3f);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.fadeIn = 0.5f;
          dust.customData = (object) ((Entity) this.Projectile).Center;
        }
        int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
        if (closest != -1)
        {
          this.Projectile.localAI[1] = Vector2.op_Equality(((Entity) this.Projectile).Center, ((Entity) Main.player[closest]).Center) ? 0.0f : Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[closest]).Center));
          this.Projectile.localAI[1] += 1.04719758f;
        }
      }
      else
      {
        this.Projectile.scale = 3f;
        this.Projectile.alpha = 0;
        this.Projectile.rotation -= (float) Math.PI / 60f;
        if (Utils.NextBool(Main.rand))
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
          dust.noGravity = true;
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, (float) Main.rand.Next(10, 21)));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, 1.5707963705062866, new Vector2()), 6f);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.fadeIn = 0.5f;
          dust.customData = (object) ((Entity) this.Projectile).Center;
        }
        else
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
          dust.noGravity = true;
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, -1.5707963705062866, new Vector2()), 3f);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.fadeIn = 0.5f;
          dust.customData = (object) ((Entity) this.Projectile).Center;
        }
        if (this.timer == 0)
        {
          int num1 = 500;
          Player player = Main.player[this.Projectile.owner];
          for (int index = 0; index < Main.maxProjectiles; ++index)
          {
            float num2 = ((Entity) this.Projectile).Distance(((Entity) Main.projectile[index]).Center);
            Projectile projectile = Main.projectile[index];
            if (FargoSoulsUtil.CanDeleteProjectile(projectile) && (double) num2 <= (double) num1 && projectile.type != this.Projectile.type && (!projectile.friendly || !player.FargoSouls().VortexStealth))
            {
              Vector2 vector2 = Vector2.op_Subtraction(((Entity) this.Projectile).position, ((Entity) projectile).Center);
              ((Entity) projectile).velocity = Vector2.op_Multiply(Utils.SafeNormalize(vector2, Vector2.Zero), 8f);
              if (this.Projectile.timeLeft <= 30 && (double) num2 < (double) (num1 / 4) && (double) projectile.minionSlots == 0.0)
                ((Entity) projectile).active = false;
            }
          }
          this.timer = 10;
        }
        --this.timer;
      }
      Dust dust1 = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
      dust1.velocity = Vector2.op_Multiply(dust1.velocity, 5f);
      dust1.fadeIn = 1f;
      dust1.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
      dust1.noGravity = true;
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      int num = 229;
      for (int index = 0; index < 80; ++index)
      {
        Dust dust1 = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num, 0.0f, 0.0f, 0, new Color(), 1f)];
        Dust dust2 = dust1;
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 10f);
        dust1.fadeIn = 1f;
        dust1.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
        if (!Utils.NextBool(Main.rand, 3))
        {
          dust1.noGravity = true;
          Dust dust3 = dust1;
          dust3.velocity = Vector2.op_Multiply(dust3.velocity, 3f);
          dust1.scale *= 2f;
        }
      }
      if ((double) this.Projectile.localAI[0] != 0.0 || this.Projectile.owner != Main.myPlayer)
        return;
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        if (((Entity) Main.npc[index]).active && (double) ((Entity) this.Projectile).Distance(((Entity) Main.npc[index]).Center) < 850.0)
          Main.npc[index].immune[this.Projectile.owner] = 0;
      }
      this.Projectile.localAI[0] = 1f;
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = 600;
      ((Entity) this.Projectile).height = 600;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      this.Projectile.damage *= 10;
      this.Projectile.knockBack *= 10f;
      this.Projectile.Damage();
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(Color.Black, this.Projectile.Opacity), -this.Projectile.rotation, vector2, this.Projectile.scale * 1.25f, (SpriteEffects) 1, 0.0f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
