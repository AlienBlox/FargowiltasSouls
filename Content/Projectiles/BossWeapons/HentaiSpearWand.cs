// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSpearWand
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HentaiSpearWand : ModProjectile
  {
    private int syncTimer;
    private Vector2 mousePos;

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
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.hide = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.alpha = 0;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.netImportant = true;
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

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.mousePos.X);
      writer.Write(this.mousePos.Y);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      Vector2 vector2;
      vector2.X = reader.ReadSingle();
      vector2.Y = reader.ReadSingle();
      if (this.Projectile.owner == Main.myPlayer)
        return;
      this.mousePos = vector2;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (this.Projectile.owner == Main.myPlayer && (double) this.Projectile.localAI[0] > 5.0 && player.ownedProjectileCounts[ModContent.ProjectileType<HentaiSpearBigDeathray>()] < 1)
        this.Projectile.Kill();
      else if (player.dead || player.ghost || !((Entity) player).active)
      {
        this.Projectile.Kill();
      }
      else
      {
        if (player.HeldItem.type == ModContent.ItemType<FargowiltasSouls.Content.Items.Weapons.FinalUpgrades.HentaiSpear>())
        {
          this.Projectile.damage = Main.player[this.Projectile.owner].GetWeaponDamage(Main.player[this.Projectile.owner].HeldItem, false);
          this.Projectile.CritChance = player.GetWeaponCrit(player.HeldItem);
          this.Projectile.knockBack = Main.player[this.Projectile.owner].GetWeaponKnockback(Main.player[this.Projectile.owner].HeldItem, Main.player[this.Projectile.owner].HeldItem.knockBack);
        }
        if ((double) this.Projectile.localAI[0]++ == 0.0 && this.Projectile.owner == Main.myPlayer)
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Normalize(((Entity) this.Projectile).velocity), ModContent.ProjectileType<HentaiSpearBigDeathray>(), this.Projectile.damage, this.Projectile.knockBack, ((Entity) player).whoAmI, 0.0f, (float) this.Projectile.identity, 0.0f);
        Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, false, true);
        player.heldProj = ((Entity) this.Projectile).whoAmI;
        player.itemTime = 2;
        player.itemAnimation = 2;
        ((Entity) this.Projectile).Center = vector2;
        this.Projectile.timeLeft = 2;
        if (this.Projectile.owner == Main.myPlayer)
        {
          this.mousePos = Main.MouseWorld;
          if (++this.syncTimer > 20)
          {
            this.syncTimer = 0;
            this.Projectile.netUpdate = true;
          }
        }
        ((Entity) this.Projectile).velocity = Vector2.Lerp(Vector2.Normalize(((Entity) this.Projectile).velocity), Vector2.Normalize(Vector2.op_Subtraction(this.mousePos, player.MountedCenter)), 0.06f);
        ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
        Projectile projectile1 = this.Projectile;
        ((Entity) projectile1).position = Vector2.op_Addition(((Entity) projectile1).position, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 164f), 1.3f), 4f));
        double num1 = (double) ((Entity) this.Projectile).direction * (double) player.gravDir;
        float num2 = ((Entity) this.Projectile).direction < 0 ? 3.14159274f : 0.0f;
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + MathHelper.ToRadians(135f);
        Projectile projectile2 = this.Projectile;
        ((Entity) projectile2).position = Vector2.op_Subtraction(((Entity) projectile2).position, ((Entity) this.Projectile).velocity);
        player.itemRotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + num2;
        player.itemRotation = MathHelper.WrapAngle(player.itemRotation);
        player.ChangeDir(Math.Sign(((Entity) this.Projectile).velocity.X));
      }
    }

    public virtual void OnKill(int timeLeft) => base.OnKill(timeLeft);

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
