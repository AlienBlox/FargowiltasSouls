// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Nature.NatureIcicle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Nature
{
  public class NatureIcicle : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Souls/FrostIcicle";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 600;
      this.Projectile.scale = 1.5f;
      this.Projectile.hide = true;
      this.CooldownSlot = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.coldDamage = true;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = Utils.NextBool(Main.rand) ? 1f : -1f;
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 0.0f, 6.28318548f);
        this.Projectile.hide = false;
      }
      if ((double) --this.Projectile.ai[0] > 0.0)
      {
        this.Projectile.tileCollide = false;
        this.Projectile.rotation += ((Vector2) ref ((Entity) this.Projectile).velocity).Length() * 0.1f * this.Projectile.localAI[0];
      }
      else if ((double) this.Projectile.ai[0] == 0.0)
      {
        int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
        if (closest == -1)
          return;
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[closest]).Center), 30f);
        this.Projectile.netUpdate = true;
        if ((double) this.Projectile.ai[1] > 0.0)
        {
          float num = MathHelper.ToRadians(20f) + Utils.NextFloat(Main.rand, MathHelper.ToRadians(30f));
          if (Utils.NextBool(Main.rand))
            num *= -1f;
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) num, new Vector2());
        }
        SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      else
      {
        if (!this.Projectile.tileCollide && !Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
          this.Projectile.tileCollide = true;
        if (!Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero))
          return;
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item27, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 92, 0.0f, 0.0f, 0, new Color(), 1f);
        if (!Utils.NextBool(Main.rand, 3))
        {
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].scale *= 1.75f;
        }
        else
          Main.dust[index2].scale *= 0.5f;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
        target.AddBuff(46, 300, true, false);
      target.AddBuff(44, 300, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), 0.75f), 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
