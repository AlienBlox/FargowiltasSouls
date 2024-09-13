// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.CursedCoffin.CoffinRandomStuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.CursedCoffin
{
  public class CoffinRandomStuff : ModProjectile
  {
    public const int Frames = 7;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Type] = 3;
      ProjectileID.Sets.TrailingMode[this.Type] = 2;
      Main.projFrames[this.Type] = 7;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.timeLeft = 360;
    }

    public ref float ObjectType => ref this.Projectile.ai[0];

    public ref float StartHeight => ref this.Projectile.ai[1];

    public static float Gravity(float objectType)
    {
      return (double) objectType == 5.0 ? 0.15f : ((double) objectType == 6.0 ? 0.17f : 0.2f);
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      this.StartHeight = ((Entity) this.Projectile).Center.Y;
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      Projectile projectile1 = this.Projectile;
      Projectile projectile2 = this.Projectile;
      float num1 = this.ObjectType;
      int num2 = (double) num1 == 5.0 ? 28 : ((double) num1 == 6.0 ? 16 : 32);
      int num3 = ((Entity) projectile2).height = num2;
      ((Entity) projectile1).width = num3;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[1] < 12.0)
      {
        ++this.Projectile.localAI[1];
        this.Projectile.scale = MathHelper.Lerp(0.0f, 1f, this.Projectile.localAI[1] / 12f);
      }
      if ((double) this.Projectile.localAI[0] == 0.0)
        this.Projectile.localAI[0] = Utils.NextBool(Main.rand) ? 1f : -1f;
      this.Projectile.rotation += (float) (6.2831854820251465 * (double) this.Projectile.localAI[0] / 33.0);
      ((Entity) this.Projectile).velocity.Y += CoffinRandomStuff.Gravity(this.ObjectType);
      this.Projectile.tileCollide = (double) ((Entity) this.Projectile).Center.Y > (double) this.StartHeight;
      this.Projectile.frame = (int) this.ObjectType;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      this.Projectile.frame = (int) this.ObjectType;
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), (float) (texture2D.Width - ((Entity) this.Projectile).width)), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(lightColor, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(oldPo, vector2_2), Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
