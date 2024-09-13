// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Pets.Nibble
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Pets
{
  public class Nibble : ModProjectile
  {
    private float realFrameCounter;
    private int realFrame;
    private bool shivering;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 9;
      Main.projPet[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(324);
      this.AIType = 324;
      ((Entity) this.Projectile).width = 24;
      ((Entity) this.Projectile).height = 40;
      this.Projectile.scale = 0.8f;
    }

    public virtual bool PreAI()
    {
      Main.player[this.Projectile.owner].cSapling = false;
      return true;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!((Entity) player).active || player.dead || player.ghost)
        fargoSoulsPlayer.Nibble = false;
      if (fargoSoulsPlayer.Nibble)
        this.Projectile.timeLeft = 2;
      this.shivering = player.ZoneSnow;
      this.Projectile.rotation = 0.0f;
      if (this.Projectile.tileCollide)
      {
        if ((double) ((Entity) player).velocity.X == 0.0 && (double) Math.Abs(((Entity) player).Bottom.Y - ((Entity) this.Projectile).Bottom.Y) < 32.0 && (double) Math.Abs(((Entity) player).Center.X - ((Entity) this.Projectile).Center.X) < (double) (16 * ((double) ((Entity) this.Projectile).velocity.X == 0.0 ? 1 : 3)))
          ((Entity) this.Projectile).velocity.X += (float) (0.11500000208616257 * ((double) ((Entity) this.Projectile).Center.X == (double) ((Entity) player).Center.X ? (double) -((Entity) player).direction : (double) Math.Sign(((Entity) this.Projectile).Center.X - ((Entity) player).Center.X)));
        if (!Collision.SolidCollision(Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity.X, Vector2.UnitX)), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
          ((Entity) this.Projectile).position.X += ((Entity) this.Projectile).velocity.X;
        if ((double) Math.Abs(((Entity) this.Projectile).velocity.X) < 0.10000000149011612)
        {
          this.realFrameCounter = 0.0f;
          this.realFrame = 0;
          this.Projectile.spriteDirection = (double) ((Entity) player).Center.X < (double) ((Entity) this.Projectile).Center.X ? 1 : -1;
        }
        else
        {
          this.realFrameCounter += Math.Abs(((Entity) this.Projectile).velocity.X);
          if ((double) ++this.realFrameCounter > 5.0)
          {
            this.realFrameCounter = 0.0f;
            if (++this.realFrame > 5)
              this.realFrame = 0;
          }
        }
        this.Projectile.ai[2] = 0.0f;
      }
      else
      {
        if (Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero))
        {
          float num = 0.03f * Math.Min(++this.Projectile.ai[2] / 480f, 1f);
          ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center), ((Vector2) ref ((Entity) this.Projectile).velocity).Length()), num);
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          if (this.Projectile.spriteDirection > 0)
            this.Projectile.rotation += 3.14159274f;
        }
        if ((double) ++this.realFrameCounter > 3.0)
        {
          this.realFrameCounter = 0.0f;
          ++this.realFrame;
        }
        if (this.realFrame >= 6 && this.realFrame < Main.projFrames[this.Projectile.type])
          return;
        this.realFrame = 6;
      }
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = (double) ((Entity) Main.player[this.Projectile.owner]).Center.Y > (double) ((Entity) this.Projectile).Bottom.Y;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity) => false;

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.realFrame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Vector2 vector2_2 = Vector2.op_Multiply(4f, Vector2.UnitY);
      if (this.shivering)
        vector2_2.X += Utils.NextFloat(Main.rand, -1f, 1f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
