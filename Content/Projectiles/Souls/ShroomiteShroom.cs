// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.ShroomiteShroom
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class ShroomiteShroom : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_131";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(131);
      this.AIType = 131;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 20;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
      this.Projectile.penetrate = 1;
    }

    public virtual void AI() => this.Projectile.alpha += 8;

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color((int) byte.MaxValue - this.Projectile.alpha, (int) byte.MaxValue - this.Projectile.alpha, (int) byte.MaxValue - this.Projectile.alpha, 0));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), (float) (texture2D.Width - ((Entity) this.Projectile).width)), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.75f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(oldPo, vector2_2), Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
