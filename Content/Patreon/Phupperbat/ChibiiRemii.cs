// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Phupperbat.ChibiiRemii
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Phupperbat
{
  public class ChibiiRemii : ModProjectile
  {
    private int sitTimer;
    private float realFrameCounter;
    private int realFrame;
    private bool squeak;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 11;
      Main.projPet[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(319);
      this.AIType = 319;
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 40;
    }

    public virtual bool PreAI()
    {
      Main.player[this.Projectile.owner].blackCat = false;
      return true;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      PatreonPlayer modPlayer = player.GetModPlayer<PatreonPlayer>();
      if (!((Entity) player).active || player.dead || player.ghost)
        modPlayer.ChibiiRemii = false;
      if (modPlayer.ChibiiRemii)
        this.Projectile.timeLeft = 2;
      if (this.Projectile.tileCollide)
      {
        if ((double) ((Entity) player).velocity.X == 0.0 && (double) Math.Abs(((Entity) player).Bottom.Y - ((Entity) this.Projectile).Bottom.Y) < 32.0 && (double) Math.Abs(((Entity) player).Center.X - ((Entity) this.Projectile).Center.X) < (double) (16 * ((double) ((Entity) this.Projectile).velocity.X == 0.0 ? 1 : 3)) && this.sitTimer < 600)
          ((Entity) this.Projectile).velocity.X += (float) (0.10000000149011612 * ((double) ((Entity) this.Projectile).Center.X == (double) ((Entity) player).Center.X ? (double) -((Entity) player).direction : (double) Math.Sign(((Entity) this.Projectile).Center.X - ((Entity) player).Center.X)));
        if (!Collision.SolidCollision(Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity.X, Vector2.UnitX)), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
          ((Entity) this.Projectile).position.X += ((Entity) this.Projectile).velocity.X;
        bool flag = (double) ((Entity) this.Projectile).velocity.Y >= 0.0 && (double) ((Entity) this.Projectile).velocity.Y <= 0.800000011920929;
        if (flag)
        {
          if ((double) Math.Abs(((Entity) this.Projectile).velocity.X) < 1.0)
          {
            this.realFrameCounter = 0.0f;
            this.realFrame = 1;
          }
          else
          {
            this.realFrameCounter += Math.Abs(((Entity) this.Projectile).velocity.X);
            if ((double) ++this.realFrameCounter > 8.0)
            {
              this.realFrameCounter = 0.0f;
              ++this.realFrame;
            }
          }
          if (this.realFrame >= 6)
            this.realFrame = 0;
        }
        else
          this.realFrame = 1;
        if ((double) ((Entity) this.Projectile).velocity.X == 0.0)
        {
          this.realFrame = !flag ? 1 : 0;
          this.realFrameCounter = 0.0f;
          if (!flag)
            this.sitTimer = 0;
          if (this.sitTimer >= 600)
          {
            this.realFrame = 6;
            if (this.sitTimer == 600 && this.squeak)
            {
              this.squeak = false;
              if (!Main.dedServ)
              {
                DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 1);
                interpolatedStringHandler.AppendLiteral("FargowiltasSouls/Assets/Sounds/SqueakyToy/squeak");
                interpolatedStringHandler.AppendFormatted<int>(Main.rand.Next(1, 7));
                SoundStyle soundStyle = new SoundStyle(interpolatedStringHandler.ToStringAndClear(), (SoundType) 0);
                SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
              }
            }
          }
          else
          {
            ++this.sitTimer;
            ((Entity) this.Projectile).direction = Math.Sign(((Entity) player).Center.X - ((Entity) this.Projectile).Center.X);
            if ((double) ((Entity) player).velocity.X != 0.0 || (double) Math.Abs(((Entity) player).velocity.Y) > 0.5)
              this.squeak = false;
          }
        }
        else
        {
          this.sitTimer = 0;
          this.squeak = true;
        }
        if (this.realFrame <= 6)
          return;
        this.realFrame = 0;
      }
      else
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).Center = Vector2.op_Addition(((Entity) projectile).Center, ((Entity) this.Projectile).velocity);
        if ((double) ++this.realFrameCounter > 3.0)
        {
          this.realFrameCounter = 0.0f;
          ++this.realFrame;
        }
        if (this.realFrame >= 7 && this.realFrame < Main.projFrames[this.Projectile.type])
          return;
        this.realFrame = 7;
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

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (this.Projectile.owner != Main.myPlayer)
        return;
      this.Projectile.vampireHeal(1, ((Entity) this.Projectile).Center, (Entity) target);
    }

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
      Color color1 = Color.op_Multiply(Color.Red, this.Projectile.Opacity);
      ((Color) ref color1).A = (byte) 20;
      Vector2 vector2_2 = Vector2.op_Multiply(-2f, Vector2.UnitY);
      float num3 = (float) ((double) this.Projectile.scale * ((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.44999998807907104 + 0.800000011920929);
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.25f)
      {
        Color color2 = Color.op_Multiply(color1, 0.3f);
        float num4 = ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
        Color color3 = Color.op_Multiply(color2, num4 * num4);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          float num5 = this.Projectile.oldRot[index2];
          Vector2 vector2_3 = Vector2.op_Addition(Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_3, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color3, num5, vector2_1, num3, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color1, this.Projectile.rotation, vector2_1, num3, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
