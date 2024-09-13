// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.Retirang
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
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class Retirang : ModProjectile
  {
    private int counter;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.friendly = true;
      this.Projectile.light = 0.4f;
      ((Entity) this.Projectile).width = 50;
      ((Entity) this.Projectile).height = 50;
      this.Projectile.penetrate = -1;
      this.Projectile.aiStyle = -1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual bool PreAI()
    {
      if (++this.counter > 15)
      {
        this.counter = 0;
        if (this.Projectile.owner == Main.myPlayer)
        {
          Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) this.Projectile).Center)), 20f);
          SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          int index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2, ModContent.ProjectileType<PrimeLaser>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
          if (index != Main.maxProjectiles)
            Main.projectile[index].DamageType = DamageClass.Melee;
        }
      }
      if ((double) this.Projectile.ai[0] != 1.0)
        return true;
      ++this.Projectile.ai[1];
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).oldPosition;
      Projectile projectile = this.Projectile;
      ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.1f);
      this.Projectile.rotation += (float) ((Entity) this.Projectile).direction * 0.4f;
      this.counter += 2;
      if ((double) this.Projectile.ai[1] > 15.0)
        this.Projectile.ai[0] = 2f;
      return false;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.ai[0] == 0.0)
      {
        ++this.Projectile.ai[1];
        if ((double) this.Projectile.ai[1] > 30.0)
        {
          this.Projectile.ai[0] = 1f;
          this.Projectile.ai[1] = 0.0f;
          this.Projectile.netUpdate = true;
        }
      }
      else if ((double) this.Projectile.ai[0] == 2.0)
      {
        float num = Math.Max(((Vector2) ref ((Entity) this.Projectile).velocity).Length() * 1.02f, 20f);
        ((Entity) this.Projectile).velocity = Vector2.Normalize(Vector2.op_Subtraction(((Entity) Main.player[this.Projectile.owner]).Center, ((Entity) this.Projectile).Center));
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, num);
        if ((double) ((Entity) this.Projectile).Distance(((Entity) Main.player[this.Projectile.owner]).Center) <= (double) num * 2.0)
          this.Projectile.Kill();
      }
      this.Projectile.rotation += (float) ((Entity) this.Projectile).direction * 0.4f;
      int index = Dust.NewDust(new Vector2(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).position.Y + 2f), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height + 5, 60, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index].noGravity = true;
      if ((double) this.Projectile.ai[0] != 1.0)
        return;
      this.Projectile.localAI[0] += 0.1f;
      Projectile projectile1 = this.Projectile;
      ((Entity) projectile1).position = Vector2.op_Addition(((Entity) projectile1).position, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[this.Projectile.owner]).Center), this.Projectile.localAI[0]));
      if ((double) ((Entity) this.Projectile).Distance(((Entity) Main.player[this.Projectile.owner]).Center) > (double) this.Projectile.localAI[0])
        return;
      this.Projectile.Kill();
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      if ((double) this.Projectile.ai[0] == 0.0)
      {
        this.Projectile.ai[0] = 1f;
        this.Projectile.ai[1] = 0.0f;
      }
      this.Projectile.tileCollide = false;
      return false;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      width = 22;
      height = 22;
      return true;
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
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
