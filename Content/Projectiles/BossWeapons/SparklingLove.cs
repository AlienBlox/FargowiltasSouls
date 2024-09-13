// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.SparklingLove
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

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
  public class SparklingLove : ModProjectile
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Items/Weapons/FinalUpgrades/SparklingLove";
    }

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 100;
      ((Entity) this.Projectile).height = 100;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.penetrate = -1;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.extraUpdates = 2;
      this.Projectile.aiStyle = -1;
      this.Projectile.scale = 2f;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = Utils.NextBool(Main.rand) ? -1 : 1;
        if (this.Projectile.owner == Main.myPlayer)
        {
          for (int index = 0; index < 4; ++index)
            Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Utils.RotatedBy(Vector2.Normalize(((Entity) this.Projectile).velocity), Math.PI / 2.0 * (double) index, new Vector2()), ModContent.ProjectileType<SparklingLoveDeathray>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 1.68342245f * (float) ((Entity) this.Projectile).direction, (float) this.Projectile.identity, 0.0f);
        }
      }
      if ((double) this.Projectile.ai[0] == 0.0)
      {
        if ((double) ((Entity) this.Projectile).Distance(((Entity) Main.player[this.Projectile.owner]).Center) > 800.0)
        {
          this.Projectile.ai[0] = 1f;
          this.Projectile.netUpdate = true;
          if ((double) this.Projectile.localAI[1] == 0.0)
            this.Projectile.localAI[1] = 1f;
        }
      }
      else
      {
        this.Projectile.extraUpdates = 0;
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[this.Projectile.owner]).Center), ((Vector2) ref ((Entity) this.Projectile).velocity).Length() + 0.1f);
        if ((double) ((Entity) this.Projectile).Distance(((Entity) Main.player[this.Projectile.owner]).Center) <= (double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length())
          this.Projectile.Kill();
      }
      if ((double) this.Projectile.localAI[1] == 1.0)
      {
        this.Projectile.localAI[1] = 2f;
        this.HeartBurst(((Entity) this.Projectile).Center);
      }
      this.Projectile.rotation += (float) ((Entity) this.Projectile).direction * -0.4f;
      for (int index1 = 0; index1 < 2; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 86, ((Entity) this.Projectile).velocity.X / 2f, ((Entity) this.Projectile).velocity.Y / 2f, 0, new Color(), 1.7f);
        Main.dust[index2].noGravity = true;
      }
    }

    private void HeartBurst(Vector2 spawnPos)
    {
      if (this.Projectile.owner != Main.myPlayer)
        return;
      SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(spawnPos), (SoundUpdateCallback) null);
      for (int index = 0; index < 8; ++index)
      {
        Vector2 addedVel = Vector2.op_Multiply(14f, Utils.RotatedBy(Vector2.Normalize(((Entity) this.Projectile).velocity), Math.PI / 4.0 * ((double) index + 0.5), new Vector2()));
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), spawnPos, addedVel, ModContent.ProjectileType<SparklingLoveHeart>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, -1f, 45f, 0.0f);
        FargoSoulsUtil.HeartDust(spawnPos, Utils.ToRotation(addedVel), addedVel);
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if ((double) this.Projectile.localAI[1] == 0.0)
      {
        this.Projectile.localAI[1] = 2f;
        this.HeartBurst(((Entity) target).Center);
      }
      target.AddBuff(119, 300, false);
      target.immune[this.Projectile.owner] = 6;
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Items/Weapons/FinalUpgrades/SparklingLove_glow", (AssetRequestMode) 1).Value, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(Color.White, this.Projectile.Opacity), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
