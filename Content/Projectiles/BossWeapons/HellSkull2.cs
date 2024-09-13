// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HellSkull2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
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
  public class HellSkull2 : ModProjectile
  {
    public float targetRotation;

    public virtual string Texture => "Terraria/Images/Projectile_585";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 20;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
      Main.projFrames[this.Projectile.type] = Main.projFrames[585];
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 26;
      ((Entity) this.Projectile).height = 26;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 120;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.penetrate = -1;
      this.Projectile.scale = 2f;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.localAI[1] = 50f;
        this.targetRotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
        SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 3; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, (int) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale), (int) ((double) ((Entity) this.Projectile).height * (double) this.Projectile.scale), 27, ((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y, 0, new Color(), 2f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].velocity = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, Main.dust[index2].position);
          Dust dust1 = Main.dust[index2];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, -5f);
          Dust dust2 = Main.dust[index2];
          dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Division(((Entity) this.Projectile).velocity, 2f));
          Main.dust[index2].noLight = true;
        }
      }
      float num1 = ((Vector2) ref ((Entity) this.Projectile).velocity).Length();
      float num2 = this.targetRotation + (float) (0.78539818525314331 * Math.Sin(6.2831854820251465 * (double) this.Projectile.localAI[1] / 60.0)) * this.Projectile.ai[1];
      if ((double) ++this.Projectile.localAI[1] > 60.0)
        this.Projectile.localAI[1] = 0.0f;
      ((Entity) this.Projectile).velocity = Vector2.op_Multiply(num1, Utils.ToRotationVector2(num2));
      if (this.Projectile.alpha > 0)
        this.Projectile.alpha -= 50;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      if (++this.Projectile.frameCounter >= 12)
        this.Projectile.frameCounter = 0;
      this.Projectile.frame = this.Projectile.frameCounter / 2;
      if (this.Projectile.frame > 3)
        this.Projectile.frame = 6 - this.Projectile.frame;
      Lighting.AddLight(((Entity) this.Projectile).Center, ((Color) ref NPCID.Sets.MagicAuraColor[54]).ToVector3());
      this.Projectile.spriteDirection = ((Entity) this.Projectile).direction = (double) ((Entity) this.Projectile).velocity.X < 0.0 ? -1 : 1;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      if (((Entity) this.Projectile).direction >= 0)
        return;
      this.Projectile.rotation += 3.14159274f;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 8;
      target.AddBuff(ModContent.BuffType<HellFireBuff>(), 30, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundStyle npcDeath52 = SoundID.NPCDeath52;
      ((SoundStyle) ref npcDeath52).Volume = 0.5f;
      ((SoundStyle) ref npcDeath52).Pitch = 0.2f;
      SoundEngine.PlaySound(ref npcDeath52, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 15; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 10; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 27, 0.0f, 0.0f, 100, new Color(), 3f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 27, 0.0f, 0.0f, 100, new Color(), 1f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(new Color(212, 148, (int) byte.MaxValue), this.Projectile.Opacity), 0.75f), 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        float num3 = this.Projectile.scale * ((float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num4 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num4, vector2, num3, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), base.GetAlpha(Color.White).Value, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
