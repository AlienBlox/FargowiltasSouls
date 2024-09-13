// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.BanishedBaron.BaronMine
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
namespace FargowiltasSouls.Content.Bosses.BanishedBaron
{
  public class BaronMine : ModProjectile
  {
    public bool home = true;
    public bool BeenOutside;
    private Vector2 drawOffset = Vector2.Zero;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 3;
      ProjectileID.Sets.TrailCacheLength[this.Type] = 4;
      ProjectileID.Sets.TrailingMode[this.Type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 110;
      ((Entity) this.Projectile).height = 110;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.light = 1f;
      this.Projectile.frame = 2;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(30, 360, true, false);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      if ((double) ((Entity) this.Projectile).velocity.X != (double) oldVelocity.X && (double) Math.Abs(oldVelocity.X) > 1.0)
        ((Entity) this.Projectile).velocity.X = oldVelocity.X * 0.0f;
      if ((double) ((Entity) this.Projectile).velocity.Y != (double) oldVelocity.Y && (double) Math.Abs(oldVelocity.Y) > 1.0)
        ((Entity) this.Projectile).velocity.Y = oldVelocity.Y * 0.0f;
      return false;
    }

    public bool Floating => (double) this.Projectile.ai[0] == 1.0;

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 6.28318548f);
      this.Projectile.rotation += (this.Floating ? 0.4f : 1f) * MathHelper.ToRadians(((Vector2) ref ((Entity) this.Projectile).velocity).Length());
      int num1 = this.Floating ? 140 : 120;
      if ((double) ++this.Projectile.localAI[1] > (double) num1 * 0.33000001311302185 && this.Projectile.frame > 0)
      {
        --this.Projectile.frame;
        this.Projectile.localAI[1] = 0.0f;
      }
      this.Projectile.scale = (float) Utils.Lerp(0.0, 1.0, (double) Math.Clamp(this.Projectile.localAI[0] / 15f, 0.0f, 1f));
      if ((double) ++this.Projectile.localAI[0] > (double) num1)
        this.Projectile.Kill();
      float num2 = 2.5f * this.Projectile.localAI[0] / (float) num1;
      this.drawOffset = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 6.2831854820251465), num2);
      if (!this.Projectile.tileCollide && !Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).height, ((Entity) this.Projectile).width))
        this.Projectile.tileCollide = !this.Floating;
      if (this.Floating)
      {
        if (Collision.WetCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height) || Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
        {
          if ((double) ((Entity) this.Projectile).velocity.Y > 0.0)
            ((Entity) this.Projectile).velocity.Y *= 0.94f;
          ((Entity) this.Projectile).velocity.Y -= 0.12f;
        }
        else
        {
          if ((double) ((Entity) this.Projectile).velocity.Y < 0.0)
            ((Entity) this.Projectile).velocity.Y /= 3f;
          ((Entity) this.Projectile).velocity.Y += 0.3f;
        }
        ((Entity) this.Projectile).velocity.X *= 0.97f;
      }
      if ((double) this.Projectile.ai[0] != 2.0)
        return;
      Projectile projectile = this.Projectile;
      ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.97f);
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
      }
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index = 0; index < 4; ++index)
        Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Utils.RotatedByRandom(Vector2.op_Multiply(Vector2.UnitX, 5f), 6.2831854820251465), Main.rand.Next(61, 64), 1f);
      float num1 = (double) this.Projectile.ai[0] == 1.0 ? 1f : 1.5f;
      float num2 = 24f;
      for (int index3 = 0; index3 < 8; ++index3)
      {
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2_1 = Utils.RotatedBy(new Vector2(0.0f, 1f), (double) this.Projectile.rotation + (double) index3 * 6.2831854820251465 / 8.0, new Vector2());
          Vector2 vector2_2 = Vector2.op_Multiply(Vector2.op_Multiply(vector2_1, Utils.NextFloat(Main.rand, 5f, 6f)), num1);
          Vector2 vector2_3 = Vector2.op_Multiply(vector2_1, num2);
          int index4 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_3), vector2_2, ModContent.ProjectileType<BaronShrapnel>(), this.Projectile.damage, this.Projectile.knockBack, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          if (index4 != Main.maxProjectiles)
          {
            Main.projectile[index4].hostile = this.Projectile.hostile;
            Main.projectile[index4].friendly = this.Projectile.friendly;
          }
        }
      }
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(lightColor, 0.75f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, this.drawOffset), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
