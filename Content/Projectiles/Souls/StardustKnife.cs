// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.StardustKnife
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class StardustKnife : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 10;
      ((Entity) this.Projectile).height = 10;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.penetrate = 1;
      this.Projectile.timeLeft = 600;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = 0;
      this.Projectile.extraUpdates = 2;
    }

    public virtual void AI()
    {
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      if ((double) this.Projectile.localAI[0] == 0.0)
        SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if ((double) ++this.Projectile.localAI[0] > (double) (10 * this.Projectile.MaxUpdates * 2))
      {
        this.Projectile.tileCollide = true;
      }
      else
      {
        if ((double) this.Projectile.localAI[0] != (double) (10 * this.Projectile.MaxUpdates + 1))
          return;
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(24f / (float) this.Projectile.MaxUpdates, Vector2.Normalize(((Entity) this.Projectile).velocity));
        this.Projectile.localAI[1] = 1f;
      }
    }

    public virtual void OnKill(int timeleft)
    {
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 80;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      SoundStyle soundStyle = SoundID.Item25;
      ((SoundStyle) ref soundStyle).Volume = 0.5f;
      ((SoundStyle) ref soundStyle).Pitch = 0.0f;
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 2; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 3.14159274101257), (float) Main.rand.NextDouble()), (float) ((Entity) this.Projectile).width), 2f));
      }
      for (int index3 = 0; index3 < 10; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 176, 0.0f, 0.0f, 200, new Color(), 3.7f);
        Main.dust[index4].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 3.14159274101257), (float) Main.rand.NextDouble()), (float) ((Entity) this.Projectile).width), 2f));
        Main.dust[index4].noGravity = true;
        Dust dust = Main.dust[index4];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
      }
      for (int index5 = 0; index5 < 10; ++index5)
      {
        int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 180, 0.0f, 0.0f, 0, new Color(), 2.7f);
        Main.dust[index6].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 3.14159274101257), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()), (float) ((Entity) this.Projectile).width), 2f));
        Main.dust[index6].noGravity = true;
        Dust dust = Main.dust[index6];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
      }
      for (int index7 = 0; index7 < 5; ++index7)
      {
        int index8 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index8].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 3.14159274101257), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()), (float) ((Entity) this.Projectile).width), 2f));
        Main.dust[index8].noGravity = true;
        Dust dust = Main.dust[index8];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      if ((double) this.Projectile.localAI[1] == 1.0)
      {
        Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantEye_Glow", (AssetRequestMode) 1).Value;
        int num1 = texture2D.Height / Main.projFrames[this.Projectile.type];
        int num2 = num1 * this.Projectile.frame;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
        Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
        Color color1 = Color.Lerp(new Color(29, 171, 239, 0), Color.Transparent, 0.7f);
        Vector2 vector2_2 = Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 18f);
        float scale = this.Projectile.scale;
        for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
        {
          Color color2 = Color.op_Multiply(color1, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Subtraction(Vector2.op_Addition(this.Projectile.oldPos[index], Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, scale, (SpriteEffects) 0, 0.0f);
        }
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Subtraction(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color1, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, scale, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }

    public virtual void PostDraw(Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
    }
  }
}
