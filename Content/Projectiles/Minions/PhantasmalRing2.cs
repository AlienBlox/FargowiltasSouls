// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.PhantasmalRing2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class PhantasmalRing2 : ModProjectile
  {
    private const float PI = 3.14159274f;
    private const float rotationPerTick = 0.0668424f;
    private const float threshold = 200f;

    public virtual string Texture => "Terraria/Images/Projectile_454";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 2;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 46;
      ((Entity) this.Projectile).height = 46;
      this.Projectile.scale *= 2f;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.alpha = (int) byte.MaxValue;
    }

    public virtual void AI()
    {
      if (((Entity) Main.player[this.Projectile.owner]).active && !Main.player[this.Projectile.owner].dead && !Main.player[this.Projectile.owner].ghost && Main.player[this.Projectile.owner].FargoSouls().MutantEyeItem != null && Main.player[this.Projectile.owner].FargoSouls().MutantEyeVisual && Main.player[this.Projectile.owner].FargoSouls().MutantEyeCD <= 0)
      {
        this.Projectile.alpha = 0;
        ((Entity) this.Projectile).Center = ((Entity) Main.player[this.Projectile.owner]).Center;
        this.Projectile.timeLeft = 2;
        this.Projectile.scale = (float) ((1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue) * 0.5);
        this.Projectile.ai[0] -= (float) Math.PI / 47f;
        if ((double) this.Projectile.ai[0] < 3.1415927410125732)
        {
          this.Projectile.ai[0] += 6.28318548f;
          this.Projectile.netUpdate = true;
        }
        ++this.Projectile.frameCounter;
        if (this.Projectile.frameCounter < 6)
          return;
        this.Projectile.frameCounter = 0;
        ++this.Projectile.frame;
        if (this.Projectile.frame <= 1)
          return;
        this.Projectile.frame = 0;
      }
      else
        this.Projectile.Kill();
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index1 = 0; index1 < 7; ++index1)
      {
        Vector2 vector2_2 = Utils.RotatedBy(Utils.RotatedBy(new Vector2((float) (200.0 * (double) this.Projectile.scale / 2.0), 0.0f), (double) this.Projectile.ai[0], new Vector2()), 0.89759790897369385 * (double) index1, new Vector2());
        for (int index2 = 0; index2 < 4; ++index2)
        {
          Color color = Color.op_Multiply(alpha, (float) (4 - index2) / 4f);
          Vector2 vector2_3 = Vector2.op_Addition(((Entity) this.Projectile).Center, Utils.RotatedBy(vector2_2, 0.066842399537563324 * (double) index2, new Vector2()));
          float rotation = this.Projectile.rotation;
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_3, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
        }
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), 0.3f));
    }
  }
}
