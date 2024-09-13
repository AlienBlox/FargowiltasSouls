// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.GolemSpikyBall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class GolemSpikyBall : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_185";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(185);
      this.AIType = 185;
      this.Projectile.DamageType = DamageClass.Default;
      this.Projectile.hostile = true;
      this.Projectile.friendly = false;
      this.Projectile.trap = false;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.penetrate = 1;
    }

    public virtual bool? CanDamage() => new bool?(this.Projectile.alpha == 0);

    public virtual void AI()
    {
      if (this.Projectile.alpha > 0)
      {
        this.Projectile.alpha -= 2;
        if (this.Projectile.alpha < 0)
        {
          this.Projectile.alpha = 0;
          for (int index1 = 0; index1 < 7; ++index1)
          {
            int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 246, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 2f);
            Main.dust[index2].noGravity = true;
            Dust dust1 = Main.dust[index2];
            dust1.velocity = Vector2.op_Multiply(dust1.velocity, 1.5f);
            int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 246, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1f);
            Dust dust2 = Main.dust[index3];
            dust2.velocity = Vector2.op_Multiply(dust2.velocity, 1.5f);
          }
        }
      }
      Tile tileSafely = Framing.GetTileSafely(((Entity) this.Projectile).Center);
      if (!Tile.op_Inequality(tileSafely, (ArgumentException) null) || ((Tile) ref tileSafely).WallType != (ushort) 87)
        return;
      ((Entity) this.Projectile).velocity.Y -= 0.4f;
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item10, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 7; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 246, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 1.5f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 246, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 1.5f);
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(36, 600, true, false);
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
      target.AddBuff(195, 600, true, false);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      if ((double) ((Entity) this.Projectile).velocity.X != (double) oldVelocity.X)
        ((Entity) this.Projectile).velocity.X = (float) (-(double) oldVelocity.X * 0.89999997615814209);
      if ((double) ((Entity) this.Projectile).velocity.Y != (double) oldVelocity.Y && (double) Math.Abs(oldVelocity.Y) > 1.0)
        ((Entity) this.Projectile).velocity.Y = (float) (-(double) oldVelocity.Y * ((double) Math.Abs(oldVelocity.Y) > 8.0 ? 0.75 : 0.98000001907348633));
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, this.Projectile.alpha == 0 ? 0 : (int) byte.MaxValue), this.Projectile.Opacity));
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
      Color alpha = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(alpha, this.Projectile.Opacity), 0.75f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
