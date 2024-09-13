// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.SaucerLaser
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
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class SaucerLaser : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_466";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 8;
      ((Entity) this.Projectile).height = 8;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.extraUpdates = 1;
      this.Projectile.timeLeft = 120;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
    }

    public virtual void OnKill(int timeLeft)
    {
      int num = Main.rand.Next(3, 7);
      for (int index1 = 0; index1 < num; ++index1)
      {
        int index2 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).velocity, 2f)), 0, 0, 228, 0.0f, 0.0f, 100, new Color(), 2.1f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
        Main.dust[index2].noGravity = true;
      }
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      Rectangle bounds = texture2D.Bounds;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(bounds), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index1 = 1; index1 < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index1)
      {
        if (!Vector2.op_Equality(this.Projectile.oldPos[index1], Vector2.Zero) && !Vector2.op_Equality(this.Projectile.oldPos[index1 - 1], this.Projectile.oldPos[index1]))
        {
          Vector2 vector2_2 = Vector2.op_Subtraction(this.Projectile.oldPos[index1 - 1], this.Projectile.oldPos[index1]);
          int num = (int) ((Vector2) ref vector2_2).Length();
          ((Vector2) ref vector2_2).Normalize();
          Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          for (int index2 = 0; index2 < num; index2 += 3)
          {
            Vector2 vector2_3 = Vector2.op_Addition(this.Projectile.oldPos[index1], Vector2.op_Multiply(vector2_2, (float) index2));
            Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_3, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(bounds), color, this.Projectile.rotation, vector2_1, this.Projectile.scale * 0.25f, (SpriteEffects) 0, 0.0f);
          }
        }
      }
      return false;
    }
  }
}
