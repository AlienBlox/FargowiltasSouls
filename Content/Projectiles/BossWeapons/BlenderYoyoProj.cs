// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.BlenderYoyoProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
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
  internal class BlenderYoyoProj : ModProjectile
  {
    public bool yoyosSpawned;
    public bool checkedYoyos;
    private int soundtimer;
    private int hitcounter;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.YoyosLifeTimeMultiplier[this.Projectile.type] = -1f;
      ProjectileID.Sets.YoyosMaximumRange[this.Projectile.type] = 750f;
      ProjectileID.Sets.YoyosTopSpeed[this.Projectile.type] = 25f;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(554);
      ((Entity) this.Projectile).width = 46;
      ((Entity) this.Projectile).height = 46;
      this.Projectile.aiStyle = 99;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.scale = 1f;
      this.Projectile.extraUpdates = 1;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 15;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
    }

    public virtual void AI()
    {
      if (!this.yoyosSpawned && this.Projectile.owner == Main.myPlayer)
      {
        float num1 = 0.0f;
        int index = 0;
        while (index < Main.maxProjectiles && (!((Entity) Main.projectile[index]).active || !Main.projectile[index].friendly || Main.projectile[index].owner != this.Projectile.owner || Main.projectile[index].type != ModContent.ProjectileType<BlenderOrbital>()))
          ++index;
        int num2 = 5;
        for (int ai0 = 0; ai0 < num2; ++ai0)
        {
          float ai1 = (float) (360.0 / (double) num2 * (double) ai0 * (Math.PI / 180.0));
          Projectile projectile = FargoSoulsUtil.NewProjectileDirectSafe(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<BlenderOrbital>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, (float) ai0, ai1);
          if (projectile != null)
          {
            projectile.localAI[0] = (float) this.Projectile.identity;
            projectile.localAI[1] = num1;
          }
        }
        this.yoyosSpawned = true;
      }
      if (this.soundtimer > 0)
        --this.soundtimer;
      if (Main.player[this.Projectile.owner].HeldItem.type != ModContent.ItemType<Blender>())
        return;
      this.Projectile.damage = Main.player[this.Projectile.owner].GetWeaponDamage(Main.player[this.Projectile.owner].HeldItem, false);
      this.Projectile.knockBack = Main.player[this.Projectile.owner].GetWeaponKnockback(Main.player[this.Projectile.owner].HeldItem, Main.player[this.Projectile.owner].HeldItem.knockBack);
    }

    public virtual void PostAI()
    {
      int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, Utils.NextBool(Main.rand) ? 107 : 157, 0.0f, 0.0f, 0, new Color(), 1f);
      Main.dust[index].noGravity = true;
      Dust dust1 = Main.dust[index];
      dust1.velocity = Vector2.op_Multiply(dust1.velocity, 0.2f);
      Dust dust2 = Main.dust[index];
      dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.8f));
      Main.dust[index].scale = 1.5f;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (this.Projectile.owner == Main.myPlayer)
      {
        Player player = Main.player[this.Projectile.owner];
        ++this.hitcounter;
        if (player.ownedProjectileCounts[556] < 5)
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) player).Center, Utils.NextVector2Circular(Main.rand, 10f, 10f), 556, this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
        if (this.hitcounter % 5 == 0)
        {
          Vector2 vector2_1 = Utils.RotatedByRandom(Vector2.UnitY, Math.PI / 4.0);
          for (int index = 0; index < 8; ++index)
          {
            Vector2 vector2_2 = Utils.RotatedBy(vector2_1, (double) index * Math.PI / 4.0, new Vector2());
            Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_2, 8f), ModContent.ProjectileType<BlenderPetal>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      if (this.soundtimer != 0)
        return;
      this.soundtimer = 15;
      SoundStyle soundStyle = SoundID.Item22;
      ((SoundStyle) ref soundStyle).Volume = 1.5f;
      ((SoundStyle) ref soundStyle).Pitch = 1f;
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      width = 30;
      height = 30;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
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
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.5f)
      {
        Color color1 = Color.op_Multiply(Color.op_Multiply(Color.LightGreen, this.Projectile.Opacity), 0.5f);
        ((Color) ref color1).A = (byte) 100;
        Color color2 = Color.op_Multiply(color1, ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          float num3 = this.Projectile.oldRot[index2];
          Vector2 vector2_2 = Vector2.op_Addition(Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, num3, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
