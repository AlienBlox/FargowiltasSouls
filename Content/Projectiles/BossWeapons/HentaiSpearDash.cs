// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSpearDash
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HentaiSpearDash : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/BossWeapons/HentaiSpear";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 30;
      this.Projectile.alpha = 0;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.extraUpdates = 1;
      this.Projectile.localNPCHitCooldown = 0;
      this.Projectile.usesLocalNPCImmunity = true;
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
      Player player = Main.player[this.Projectile.owner];
      ((Entity) player).Center = ((Entity) this.Projectile).Center;
      ((Entity) player).velocity = Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.5f);
      ((Entity) player).direction = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
      player.controlLeft = false;
      player.controlRight = false;
      player.controlJump = false;
      player.controlDown = false;
      player.controlUseItem = false;
      player.controlUseTile = false;
      player.controlHook = false;
      player.controlMount = false;
      player.heldProj = ((Entity) this.Projectile).whoAmI;
      if (player.mount.Active)
        player.mount.Dismount(player);
      player.immune = true;
      player.immuneTime = 2;
      player.hurtCooldowns[0] = 2;
      player.hurtCooldowns[1] = 2;
      int index1 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 15, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index1].noGravity = true;
      int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 15, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index2].noGravity = true;
      if ((double) --this.Projectile.localAI[0] < 0.0)
      {
        this.Projectile.localAI[0] = 3f;
        if (this.Projectile.owner == Main.myPlayer)
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<PhantasmalSphere>(), this.Projectile.damage, this.Projectile.knockBack / 2f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      }
      if (!Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero))
        return;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + MathHelper.ToRadians(135f);
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      width = 20;
      height = 42;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      ((Entity) this.Projectile).velocity = oldVelocity;
      return false;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (this.Projectile.owner == Main.myPlayer)
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) target).position, new Vector2((float) Main.rand.Next(((Entity) target).width), (float) Main.rand.Next(((Entity) target).height))), Vector2.Zero, ModContent.ProjectileType<PhantasmalBlast>(), this.Projectile.damage, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, false);
    }

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
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 2)
      {
        Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
