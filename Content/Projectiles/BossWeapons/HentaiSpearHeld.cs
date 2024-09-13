// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSpearHeld
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HentaiSpearHeld : ModProjectile
  {
    public const int useTime = 90;

    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/BossWeapons/HentaiSpear";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = 0;
      this.Projectile.DamageType = DamageClass.Throwing;
      this.Projectile.hide = true;
      this.Projectile.FargoSouls().CanSplit = false;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (((Rectangle) ref projHitbox).Intersects(targetHitbox))
        return new bool?(true);
      float num = 0.0f;
      Vector2 vector2_1 = Vector2.op_Multiply((float) (200.0 / 2.0) * this.Projectile.scale, Utils.ToRotationVector2(this.Projectile.rotation - MathHelper.ToRadians(135f)));
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, vector2_1);
      Vector2 vector2_3 = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1);
      return Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), vector2_2, vector2_3, 8f * this.Projectile.scale, ref num) ? new bool?(true) : new bool?(false);
    }

    public virtual void AI()
    {
      this.Projectile.hide = false;
      this.Projectile.timeLeft = 2;
      ++this.Projectile.ai[0];
      Player player = Main.player[this.Projectile.owner];
      ((Entity) this.Projectile).Center = ((Entity) player).Center;
      player.itemAnimation = 90;
      player.itemTime = 90;
      player.phantasmTime = 90;
      player.heldProj = ((Entity) this.Projectile).whoAmI;
      if (((Entity) player).whoAmI == Main.myPlayer)
      {
        this.Projectile.netUpdate = true;
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, Main.MouseWorld), ((Vector2) ref ((Entity) this.Projectile).velocity).Length());
        if (player.altFunctionUse != 2)
          this.Projectile.Kill();
      }
      ((Entity) player).direction = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
      player.itemRotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + MathHelper.ToRadians(135f);
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + MathHelper.ToRadians(135f);
      if ((double) ++this.Projectile.localAI[0] <= 45.0)
        return;
      this.Projectile.localAI[0] = 0.0f;
      for (int index1 = 0; index1 < 36; ++index1)
      {
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(50f, Utils.RotatedBy(Vector2.Normalize(((Entity) this.Projectile).velocity), (double) (index1 - 17) * 6.2831854820251465 / 36.0, new Vector2())));
        Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) player).Center, vector2_1);
        int index2 = Dust.NewDust(vector2_1, 0, 0, 15, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity = Vector2.op_Multiply(vector2_2, 0.1f);
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      if (this.Projectile.owner != Main.myPlayer)
        return;
      int num = (int) ((double) this.Projectile.damage * (1.0 + (double) this.Projectile.ai[0] / 90.0));
      Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, ModContent.ProjectileType<HentaiSpearThrown>(), num, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
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
