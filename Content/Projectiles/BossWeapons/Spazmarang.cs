// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.Spazmarang
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class Spazmarang : ModProjectile
  {
    private bool hitSomething;

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
      this.Projectile.penetrate = 1;
      this.Projectile.aiStyle = -1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.ai[0] == 0.0)
      {
        ++this.Projectile.ai[1];
        if ((double) this.Projectile.ai[1] > 20.0)
        {
          this.Projectile.ai[0] = 1f;
          this.Projectile.netUpdate = true;
        }
      }
      else
      {
        this.Projectile.extraUpdates = 0;
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) Main.player[this.Projectile.owner]).Center, ((Entity) this.Projectile).Center)), 45f);
        if ((double) ((Entity) this.Projectile).Distance(((Entity) Main.player[this.Projectile.owner]).Center) <= 30.0)
          this.Projectile.Kill();
      }
      this.Projectile.rotation += (float) ((Entity) this.Projectile).direction * -0.8f;
      int index = Dust.NewDust(new Vector2(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).position.Y + 2f), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height + 5, 75, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index].noGravity = true;
    }

    private void spawnFire()
    {
      if (this.hitSomething)
        return;
      this.hitSomething = true;
      if (this.Projectile.owner == Main.myPlayer)
      {
        SoundEngine.PlaySound(ref SoundID.Item74, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        FargoSoulsUtil.XWay(12, ((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, ModContent.ProjectileType<EyeFireFriendly>(), 3f, this.Projectile.damage / 2, this.Projectile.knockBack);
      }
      this.Projectile.ai[0] = 1f;
      this.Projectile.penetrate = 4;
      this.Projectile.netUpdate = true;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(39, 120, false);
      this.spawnFire();
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      this.spawnFire();
      this.Projectile.tileCollide = false;
      return false;
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
