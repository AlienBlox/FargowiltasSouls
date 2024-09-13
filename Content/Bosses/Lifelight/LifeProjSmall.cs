// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeProjSmall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeProjSmall : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 7;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 4;
      ((Entity) this.Projectile).height = 4;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1.5f;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 2)
      {
        Rectangle rectangle = projHitbox;
        Vector2 vector2 = Vector2.op_Subtraction(this.Projectile.oldPos[index], ((Entity) this.Projectile).Center);
        rectangle.X += (int) vector2.X;
        rectangle.Y += (int) vector2.Y;
        if (((Rectangle) ref rectangle).Intersects(targetHitbox))
          return new bool?(true);
      }
      return new bool?(false);
    }

    public virtual void AI()
    {
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      if ((double) ++this.Projectile.ai[0] > 600.0)
        this.Projectile.Kill();
      if ((double) this.Projectile.ai[0] == 45.0 || (double) this.Projectile.ai[0] == 90.0)
      {
        if ((double) this.Projectile.ai[1] == 3.0)
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, -1.0 * Math.PI / 3.0, new Vector2());
        if ((double) this.Projectile.ai[1] == 4.0)
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, Math.PI / 3.0, new Vector2());
      }
      if (this.Projectile.tileCollide || (double) this.Projectile.ai[0] <= (double) (60 * this.Projectile.MaxUpdates) || (double) this.Projectile.ai[1] >= 3.0)
        return;
      Tile tileSafely = Framing.GetTileSafely(((Entity) this.Projectile).Center);
      if (((Tile) ref tileSafely).HasUnactuatedTile && Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] && !Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType])
        return;
      this.Projectile.tileCollide = true;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<SmiteBuff>(), 180, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 86, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 610 - (int) Main.mouseTextColor * 2), this.Projectile.Opacity), 0.9f));
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.75f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
