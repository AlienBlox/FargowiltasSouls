// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.PhantasmalBlast
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
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
  public class PhantasmalBlast : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_645";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.projFrames[645];
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 100;
      ((Entity) this.Projectile).height = 100;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.tileCollide = false;
      this.Projectile.penetrate = -1;
      this.Projectile.scale = 1f;
      this.Projectile.alpha = 0;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void AI()
    {
      if (Utils.HasNaNs(((Entity) this.Projectile).position))
      {
        this.Projectile.Kill();
      }
      else
      {
        if (++this.Projectile.frameCounter >= 3)
        {
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          {
            --this.Projectile.frame;
            this.Projectile.Kill();
          }
        }
        if ((double) this.Projectile.localAI[0] != 0.0)
          return;
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item88, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        this.Projectile.scale *= Utils.NextFloat(Main.rand, 1f, 3f);
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 6.28318548f);
      }
    }

    public virtual bool? CanDamage() => new bool?(this.Projectile.frame < 4);

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, false);
      target.immune[this.Projectile.owner] = 1;
    }

    public virtual void OnKill(int timeLeft)
    {
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) sbyte.MaxValue), this.Projectile.Opacity));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
