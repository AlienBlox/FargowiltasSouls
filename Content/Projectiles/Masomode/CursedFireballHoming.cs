// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.CursedFireballHoming
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
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class CursedFireballHoming : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 5;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 600;
    }

    public virtual void AI()
    {
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter > 4)
      {
        ++this.Projectile.frame;
        this.Projectile.frameCounter = 1;
      }
      if (this.Projectile.frame > 4)
        this.Projectile.frame = 0;
      SoundStyle soundStyle;
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        soundStyle = SoundID.Item20;
        ((SoundStyle) ref soundStyle).Volume = 2f;
        ((SoundStyle) ref soundStyle).Pitch = 0.0f;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if (Utils.NextBool(Main.rand, 3) && (double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() > 0.0)
      {
        int index = Dust.NewDust(Vector2.op_Addition(((Entity) this.Projectile).position, ((Entity) this.Projectile).velocity), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 75, ((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y, 100, new Color(), 3f * this.Projectile.scale);
        Main.dust[index].noGravity = true;
      }
      if ((double) this.Projectile.ai[0] <= -1.0 || (double) this.Projectile.ai[0] >= (double) byte.MaxValue)
      {
        this.Projectile.Kill();
      }
      else
      {
        if ((double) this.Projectile.localAI[1] > 20.0 && (double) this.Projectile.localAI[1] < 60.0)
          ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.Zero, (float) (0.023499999195337296 + (double) this.Projectile.localAI[1] / 30000.0));
        if ((double) ++this.Projectile.localAI[1] == 60.0)
          ((Entity) this.Projectile).velocity = Vector2.Zero;
        else if ((double) this.Projectile.localAI[1] == 120.0 + (double) this.Projectile.ai[1])
        {
          soundStyle = SoundID.Item20;
          ((SoundStyle) ref soundStyle).Volume = 2f;
          ((SoundStyle) ref soundStyle).Pitch = 0.0f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          float num = 24f;
          for (int index1 = 0; (double) index1 < (double) num; ++index1)
          {
            Vector2 vector2 = Vector2.op_Multiply(2f, Utils.RotatedBy(Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitX, 0.0f), Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) index1 * (6.28318548202515 / (double) num), new Vector2())), new Vector2(1f, 4f))), (double) Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[(int) this.Projectile.ai[0]]).Center)), new Vector2()));
            int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 75, 0.0f, 0.0f, 200, new Color(), 1f);
            Main.dust[index2].scale = 2f;
            Main.dust[index2].fadeIn = 1.3f;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2);
            Main.dust[index2].velocity = Vector2.op_Multiply(Utils.SafeNormalize(vector2, Vector2.UnitY), 1.5f);
          }
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[(int) this.Projectile.ai[0]]).Center), 16f);
        }
        if ((double) this.Projectile.localAI[1] < 120.0 + (double) this.Projectile.ai[1])
          this.Projectile.alpha = 175;
        else
          this.Projectile.alpha = 0;
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item10, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(new Vector2(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).position.Y), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 75, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.200000002980232), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.200000002980232), 100, new Color(), 2f * this.Projectile.scale);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(new Vector2(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).position.Y), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 75, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.200000002980232), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.200000002980232), 100, new Color(), 1f * this.Projectile.scale);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool? CanDamage()
    {
      return (double) this.Projectile.localAI[1] < 120.0 + (double) this.Projectile.ai[1] ? new bool?(false) : new bool?(true);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(39, 120, true, false);
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
      this.Projectile.GetAlpha(lightColor);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
