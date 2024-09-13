// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSpearSpin
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Souls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HentaiSpearSpin : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/BossWeapons/HentaiSpear";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 214;
      ((Entity) this.Projectile).height = 214;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.hide = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.alpha = 0;
      this.Projectile.timeLeft = 45;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public virtual void AI()
    {
      int index1 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 15, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index1].noGravity = true;
      int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 15, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index2].noGravity = true;
      Player player = Main.player[this.Projectile.owner];
      if (this.Projectile.owner == Main.myPlayer && (!player.controlUseItem || player.controlUp && player.controlDown))
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
        player.reuseDelay = 10;
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
        if ((double) ++this.Projectile.localAI[0] > 10.0)
        {
          this.Projectile.localAI[0] = 0.0f;
          SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          if (this.Projectile.owner == Main.myPlayer && !Main.LocalPlayer.controlUseTile)
          {
            Vector2 vector2_2 = Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedByRandom(Vector2.UnitY, Math.PI / 2.0)), Utils.NextFloat(Main.rand, 9f, 12f));
            float num = (float) Main.rand.Next(30, 60);
            Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).position, Utils.NextVector2Square(Main.rand, 0.0f, (float) ((Entity) this.Projectile).width)), vector2_2, ModContent.ProjectileType<PhantasmalEyeHoming>(), this.Projectile.damage, this.Projectile.knockBack / 2f, this.Projectile.owner, -1f, num, 0.0f);
          }
        }
        for (int index3 = 0; index3 < Main.maxProjectiles; ++index3)
        {
          if (((Entity) Main.projectile[index3]).active && Main.projectile[index3].hostile && Main.projectile[index3].damage > 0 && this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) Main.projectile[index3]).Hitbox) && FargoSoulsUtil.CanDeleteProjectile(Main.projectile[index3]))
          {
            if (this.Projectile.owner == Main.myPlayer)
              Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) Main.projectile[index3]).Center, Vector2.Zero, ModContent.ProjectileType<IronParry>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            Main.projectile[index3].hostile = false;
            Main.projectile[index3].friendly = true;
            Main.projectile[index3].owner = ((Entity) player).whoAmI;
            ((Entity) Main.projectile[index3]).velocity = Vector2.op_Multiply(((Entity) Main.projectile[index3]).DirectionFrom(((Entity) player).Center), ((Vector2) ref ((Entity) Main.projectile[index3]).velocity).Length());
            Main.projectile[index3].netUpdate = true;
          }
        }
      }
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      int num1 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X;
      int num2 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y;
      if (Math.Abs(num1) > targetHitbox.Width / 2)
        num1 = targetHitbox.Width / 2 * Math.Sign(num1);
      if (Math.Abs(num2) > targetHitbox.Height / 2)
        num2 = targetHitbox.Height / 2 * Math.Sign(num2);
      int num3 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X - num1;
      int num4 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y - num2;
      return new bool?(Math.Sqrt((double) (num3 * num3 + num4 * num4)) <= (double) (((Entity) this.Projectile).width / 2));
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 1;
      if (this.Projectile.owner == Main.myPlayer)
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) target).position, new Vector2((float) Main.rand.Next(((Entity) target).width), (float) Main.rand.Next(((Entity) target).height))), Vector2.Zero, ModContent.ProjectileType<PhantasmalBlast>(), this.Projectile.damage, this.Projectile.knockBack * 3f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.1f)
      {
        Player player = Main.player[this.Projectile.owner];
        Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/BossWeapons/HentaiSpearSpinGlow", (AssetRequestMode) 1).Value;
        Color color = Color.op_Multiply(Color.Lerp(new Color(51, (int) byte.MaxValue, 191, 210), Color.Transparent, (float) (Math.Cos((double) this.Projectile.ai[0]) / 3.0 + 0.30000001192092896)), ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        float num3 = (this.Projectile.scale - (float) Math.Cos((double) this.Projectile.ai[0]) / 5f) * (((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        int index2 = Math.Max((int) index1 - 1, 0);
        Vector2 vector2_2 = Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0));
        float num4 = (float) ((double) index1 % 1.0 * 3.1415927410125732 / 6.8499999046325684);
        bool flag = (double) this.Projectile.rotation > -1.0 * Math.PI / 2.0 && (double) this.Projectile.rotation < Math.PI / 2.0;
        if (flag && ((Entity) player).direction == 1)
          num4 *= -1f;
        else if (!flag && ((Entity) player).direction == -1)
          num4 *= -1f;
        Vector2 vector2_3 = Vector2.op_Addition(vector2_2, Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
        Vector2 vector2_4 = Utils.RotatedBy(Vector2.op_Division(((Entity) this.Projectile).Size, 4f), (double) this.Projectile.oldRot[(int) index1] - (double) num4 * (double) -((Entity) this.Projectile).direction, new Vector2());
        Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Subtraction(vector2_3, vector2_4), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(), color, this.Projectile.rotation, Vector2.op_Division(Utils.Size(texture2D2), 2f), num3 * 0.4f, (SpriteEffects) 0, 0.0f);
      }
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num5 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num5, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
