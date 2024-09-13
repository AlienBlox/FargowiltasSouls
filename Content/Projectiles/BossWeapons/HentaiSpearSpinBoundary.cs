// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSpearSpinBoundary
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HentaiSpearSpinBoundary : HentaiSpearSpin
  {
    public override string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/BossWeapons/HentaiSpear";
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.DamageType = DamageClass.Ranged;
    }

    public override void AI()
    {
      int index1 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 15, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index1].noGravity = true;
      int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 15, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index2].noGravity = true;
      Player player = Main.player[this.Projectile.owner];
      if (this.Projectile.owner == Main.myPlayer && (!player.controlUseTile || player.altFunctionUse != 2 || player.controlUp && player.controlDown))
        this.Projectile.Kill();
      else if (player.dead || !((Entity) player).active)
      {
        this.Projectile.Kill();
      }
      else
      {
        Vector2 vector2_1 = player.RotatedRelativePoint(player.MountedCenter, false, true);
        ((Entity) this.Projectile).direction = ((Entity) player).direction;
        player.heldProj = ((Entity) this.Projectile).whoAmI;
        player.itemTime = 2;
        player.itemAnimation = 2;
        ((Entity) this.Projectile).Center = vector2_1;
        this.Projectile.timeLeft = 2;
        this.Projectile.rotation = (float) Math.Atan2((double) ((Entity) this.Projectile).velocity.Y, (double) ((Entity) this.Projectile).velocity.X);
        this.Projectile.rotation += 0.4586267f * (float) ((Entity) player).direction;
        this.Projectile.ai[0] += (float) Math.PI / 45f;
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(this.Projectile.rotation);
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
        player.itemRotation = this.Projectile.rotation;
        player.itemRotation = MathHelper.WrapAngle(player.itemRotation);
        if ((double) this.Projectile.ai[2] == 0.0)
        {
          if ((double) ++this.Projectile.localAI[0] <= 2.0)
            return;
          SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          this.Projectile.localAI[0] = 0.0f;
          this.Projectile.localAI[1] += (float) Math.PI / 720f * ++this.Projectile.ai[1] * (float) ((Entity) player).direction;
          if ((double) this.Projectile.localAI[1] > 3.1415927410125732)
            this.Projectile.localAI[1] -= 6.28318548f;
          if ((double) this.Projectile.localAI[1] < 3.1415927410125732)
            this.Projectile.localAI[1] += 6.28318548f;
          if (this.Projectile.owner != Main.myPlayer)
            return;
          for (int index3 = 0; index3 < 6; ++index3)
            Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) player).Center, Utils.RotatedBy(new Vector2(0.0f, -9f), (double) this.Projectile.localAI[1] + Math.PI / 3.0 * (double) index3, new Vector2()), ModContent.ProjectileType<PhantasmalEyeBoundary>(), this.Projectile.damage, this.Projectile.knockBack / 2f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
        }
        else
        {
          if ((double) ++this.Projectile.localAI[0] <= 5.0)
            return;
          SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          this.Projectile.localAI[0] = -5f;
          for (int index4 = -1; index4 <= 1; index4 += 2)
          {
            float num1 = 0.7853982f;
            int num2 = ModContent.ProjectileType<HentaiSphereOkuu>();
            int damage = this.Projectile.damage;
            float num3 = (float) ((double) MathHelper.ToRadians(60f) * (double) this.Projectile.localAI[1] / 240.0);
            for (int index5 = 0; index5 < 8; ++index5)
            {
              Vector2 vector2_2 = Vector2.op_Multiply(10f, Utils.RotatedBy(Vector2.UnitY, (double) num1 * (double) index5 + (double) num3, new Vector2()));
              if (this.Projectile.owner == Main.myPlayer)
                Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2_2, num2, damage, this.Projectile.knockBack / 2f, this.Projectile.owner, (float) index4, 10f, 0.0f);
            }
          }
        }
      }
    }

    public override bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
