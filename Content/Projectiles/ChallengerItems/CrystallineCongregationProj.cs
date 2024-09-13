// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.ChallengerItems.CrystallineCongregationProj
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
namespace FargowiltasSouls.Content.Projectiles.ChallengerItems
{
  public class CrystallineCongregationProj : ModProjectile
  {
    private int RotDirect = 1;
    private bool home = true;
    private bool homingonMouse;
    private bool chosenDirection;
    private Player player;
    private int RealDamage;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 18;
      ((Entity) this.Projectile).height = 18;
      this.Projectile.aiStyle = 0;
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Magic;
      this.AIType = 14;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 1200;
      this.Projectile.scale = 1f;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.ai[0] == 0.0)
      {
        this.Projectile.alpha = (int) byte.MaxValue;
        this.Projectile.rotation = (float) Main.rand.Next(100);
        this.RotDirect = Utils.NextBool(Main.rand, 2) ? -1 : 1;
        this.player = Main.player[this.Projectile.owner];
        this.Projectile.ai[1] = 0.0f;
        this.RealDamage = this.Projectile.damage;
      }
      Lighting.AddLight(((Entity) this.Projectile).Center, 4);
      this.Projectile.alpha -= 17;
      this.Projectile.rotation += 0.2f * (float) this.RotDirect;
      if (Utils.NextBool(Main.rand, 10))
      {
        int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 70, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 1f);
        Main.dust[index].noGravity = true;
        Main.dust[index].velocity.X *= 0.5f;
        Main.dust[index].velocity.Y *= 0.5f;
      }
      this.Projectile.tileCollide = this.Projectile.alpha <= 0;
      ((Entity) this.Projectile).velocity = Vector2.op_Multiply(((Entity) this.Projectile).velocity, 1.008f);
      float num1 = 5f;
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.player).Center, ((Entity) this.Projectile).Center);
      if ((double) ((Vector2) ref vector2_1).Length() < 50.0)
      {
        this.Projectile.friendly = false;
        this.Projectile.tileCollide = false;
      }
      Vector2 vector2_2;
      float num2;
      if (!this.player.channel || this.player.noItems || this.player.CCed)
      {
        vector2_1 = Vector2.op_Subtraction(((Entity) this.player).Center, ((Entity) this.Projectile).Center);
        if ((double) ((Vector2) ref vector2_1).Length() < 50.0)
        {
          this.Projectile.friendly = true;
          this.Projectile.tileCollide = true;
          this.Projectile.penetrate = 1;
          this.Projectile.maxPenetrate = 1;
          vector2_2 = Vector2.op_Subtraction(Main.MouseWorld, ((Entity) this.Projectile).Center);
          num2 = 18f;
          SoundEngine.PlaySound(ref SoundID.DD2_WitherBeastCrystalImpact, new Vector2?(((Entity) this.player).Center), (SoundUpdateCallback) null);
          this.Projectile.timeLeft = 60;
          this.homingonMouse = true;
          this.home = false;
          goto label_10;
        }
      }
      vector2_2 = Vector2.op_Subtraction(((Entity) this.player).Center, ((Entity) this.Projectile).Center);
      num2 = 24f;
label_10:
      double num3 = (double) ((Vector2) ref vector2_2).Length();
      if (num3 < 200.0 && this.homingonMouse)
        this.home = false;
      if (num3 > 20.0 && this.home)
      {
        ((Vector2) ref vector2_2).Normalize();
        vector2_2 = Vector2.op_Multiply(vector2_2, num2);
        ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, num1 - 1f), vector2_2), num1);
      }
      else if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
      {
        ((Entity) this.Projectile).velocity.X = -0.15f;
        ((Entity) this.Projectile).velocity.Y = -0.05f;
      }
      if (!this.home && this.homingonMouse && !this.chosenDirection)
      {
        double radians = (double) MathHelper.ToRadians((float) Main.rand.Next(-10, 10));
        ((Vector2) ref vector2_2).Normalize();
        Vector2 vector2_3 = vector2_2;
        double num4 = radians;
        vector2_1 = new Vector2();
        Vector2 vector2_4 = vector2_1;
        vector2_2 = Vector2.op_Multiply(Utils.RotatedBy(vector2_3, num4, vector2_4), num2);
        ((Entity) this.Projectile).velocity = vector2_2;
        this.chosenDirection = true;
      }
      ++this.Projectile.ai[0];
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item27, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).position, new Vector2((float) ((Entity) this.Projectile).width, (float) ((Entity) this.Projectile).height)), ((Entity) this.Projectile).width * 2, ((Entity) this.Projectile).height * 2, 70, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity.X *= 0.5f;
        Main.dust[index2].velocity.Y *= 0.5f;
        base.OnKill(timeLeft);
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 610 - (int) Main.mouseTextColor * 2), this.Projectile.Opacity));
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color1 = Color.op_Multiply(Color.op_Multiply(Color.DeepPink, this.Projectile.Opacity), 0.5f);
        ((Color) ref color1).A = ((Color) ref alpha).A;
        Color color2 = Color.op_Multiply(color1, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
