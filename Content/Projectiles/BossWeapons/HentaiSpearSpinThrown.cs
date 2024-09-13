// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSpearSpinThrown
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
  public class HentaiSpearSpinThrown : ModProjectile
  {
    private const int maxTime = 45;

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
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.alpha = 0;
      this.Projectile.timeLeft = 45;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public virtual void AI()
    {
      int index1 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 15, 0.0f, 0.0f, 100, new Color(), 2f);
      Main.dust[index1].noGravity = true;
      int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 15, 0.0f, 0.0f, 100, new Color(), 2f);
      Main.dust[index2].noGravity = true;
      Player player = Main.player[this.Projectile.owner];
      if (player.dead || !((Entity) player).active)
      {
        this.Projectile.Kill();
      }
      else
      {
        player.heldProj = ((Entity) this.Projectile).whoAmI;
        player.itemTime = 2;
        player.itemAnimation = 2;
        if ((double) ++this.Projectile.localAI[0] > 10.0)
        {
          this.Projectile.localAI[0] = 0.0f;
          SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          if (this.Projectile.owner == Main.myPlayer)
          {
            Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI), Utils.NextFloat(Main.rand, 9f, 12f));
            float num = (float) Main.rand.Next(30, 60);
            int index3 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).position, Utils.NextVector2Square(Main.rand, 0.0f, (float) ((Entity) this.Projectile).width)), vector2, ModContent.ProjectileType<PhantasmalEyeHoming>(), this.Projectile.damage, this.Projectile.knockBack / 2f, this.Projectile.owner, -1f, num, 0.0f);
            if (index3 != Main.maxProjectiles)
              Main.projectile[index3].DamageType = DamageClass.Ranged;
          }
        }
        for (int index4 = 0; index4 < Main.maxProjectiles; ++index4)
        {
          if (((Entity) Main.projectile[index4]).active && Main.projectile[index4].hostile && Main.projectile[index4].damage > 0 && this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) Main.projectile[index4]).Hitbox) && FargoSoulsUtil.CanDeleteProjectile(Main.projectile[index4]))
          {
            if (this.Projectile.owner == Main.myPlayer)
              Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) Main.projectile[index4]).Center, Vector2.Zero, ModContent.ProjectileType<IronParry>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            Main.projectile[index4].hostile = false;
            Main.projectile[index4].friendly = true;
            Main.projectile[index4].owner = ((Entity) player).whoAmI;
            ((Entity) Main.projectile[index4]).velocity = Vector2.op_Multiply(((Entity) Main.projectile[index4]).DirectionFrom(((Entity) this.Projectile).Center), ((Vector2) ref ((Entity) Main.projectile[index4]).velocity).Length());
            Main.projectile[index4].netUpdate = true;
          }
        }
        if ((double) this.Projectile.localAI[1] == 0.0)
        {
          this.Projectile.localAI[0] = (float) Main.rand.Next(10);
          this.Projectile.rotation = Utils.NextFloat(Main.rand, 6.28318548f);
        }
        ++this.Projectile.localAI[1];
        float num1 = -0.5f * (float) Math.Cos(2.0 * Math.PI / 45.0 * (double) this.Projectile.localAI[1]);
        float num2 = 0.5f * (float) Math.Sin(2.0 * Math.PI / 45.0 * (double) this.Projectile.localAI[1]) * (float) ((Entity) player).direction;
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector(this.Projectile.ai[0], this.Projectile.ai[1]);
        Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, num1);
        Vector2 vector2_3 = Vector2.op_Multiply(Utils.RotatedBy(vector2_1, Math.PI / 2.0, new Vector2()), num2);
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Division(vector2_1, 2f));
        ((Entity) this.Projectile).velocity = Vector2.op_Addition(vector2_2, vector2_3);
        this.Projectile.rotation += 0.4586267f * (float) -((Entity) player).direction;
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

    public virtual void OnKill(int timeLeft)
    {
      Player player = Main.player[this.Projectile.owner];
      if (this.Projectile.owner != Main.myPlayer || !player.controlUseTile || player.altFunctionUse != 2 || player.controlUp && player.controlDown || player.HeldItem.type != ModContent.ItemType<FargowiltasSouls.Content.Items.Weapons.FinalUpgrades.HentaiSpear>() || player.ownedProjectileCounts[this.Projectile.type] != 1)
        return;
      Vector2 mountedCenter = player.MountedCenter;
      Vector2 vector2 = Vector2.op_Subtraction(Main.MouseWorld, mountedCenter);
      if ((double) ((Vector2) ref vector2).Length() < 360.0)
        vector2 = Vector2.op_Multiply(Vector2.Normalize(vector2), 360f);
      int weaponDamage = player.GetWeaponDamage(player.HeldItem, false);
      this.Projectile.CritChance = player.GetWeaponCrit(player.HeldItem);
      float weaponKnockback = player.GetWeaponKnockback(player.HeldItem, player.HeldItem.knockBack);
      Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), mountedCenter, Vector2.Normalize(vector2), this.Projectile.type, weaponDamage, weaponKnockback, this.Projectile.owner, vector2.X, vector2.Y, 0.0f);
      player.ChangeDir(Math.Sign(vector2.X));
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 1;
      if (this.Projectile.owner == Main.myPlayer)
      {
        int index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) target).position, new Vector2((float) Main.rand.Next(((Entity) target).width), (float) Main.rand.Next(((Entity) target).height))), Vector2.Zero, ModContent.ProjectileType<PhantasmalBlast>(), this.Projectile.damage, this.Projectile.knockBack * 3f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
        if (index != Main.maxProjectiles)
          Main.projectile[index].DamageType = DamageClass.Ranged;
      }
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
      float num3 = (float) ((double) this.Projectile.localAI[1] * 3.1415927410125732 / 45.0);
      for (float index = 0.0f; (double) index < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 0.1f)
      {
        Player player = Main.player[this.Projectile.owner];
        Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/BossWeapons/HentaiSpearSpinGlow", (AssetRequestMode) 1).Value;
        Color color = Color.op_Multiply(Color.Lerp(new Color(51, (int) byte.MaxValue, 191, 210), Color.Transparent, (float) (Math.Cos((double) num3) / 3.0 + 0.30000001192092896)), ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        float num4 = (this.Projectile.scale - (float) Math.Cos((double) num3) / 5f) * (((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Math.Max((int) index - 1, 0);
        Vector2 position = ((Entity) this.Projectile).position;
        float num5 = (float) ((double) index % 1.0 * 3.1415927410125732 / 6.8499999046325684);
        bool flag = (double) this.Projectile.rotation > -1.0 * Math.PI / 2.0 && (double) this.Projectile.rotation < Math.PI / 2.0;
        if (flag && ((Entity) player).direction == 1)
          num5 *= -1f;
        else if (!flag && ((Entity) player).direction == -1)
          num5 *= -1f;
        Vector2 vector2_2 = Vector2.op_Addition(position, Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
        Vector2 vector2_3 = Utils.RotatedBy(Vector2.op_Division(((Entity) this.Projectile).Size, 4f), (double) this.Projectile.oldRot[(int) index] - (double) num5 * (double) -((Entity) this.Projectile).direction, new Vector2());
        Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Subtraction(vector2_2, vector2_3), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(), color, this.Projectile.rotation, Vector2.op_Division(Utils.Size(texture2D2), 2f), num4 * 0.4f, (SpriteEffects) 0, 0.0f);
      }
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 position = ((Entity) this.Projectile).position;
        float num6 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(position, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num6, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
