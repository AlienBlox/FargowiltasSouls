// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.BigBrainIllusion
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
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class BigBrainIllusion : ModProjectile
  {
    private const int maxTime = 60;

    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Minions/BigBrainProj";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 12;
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 80;
      ((Entity) this.Projectile).height = 80;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 60;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.extraUpdates = 1;
      this.Projectile.alpha = 125;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
        ((Entity) this.Projectile).width = (int) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.ai[0] / (double) this.Projectile.scale);
        ((Entity) this.Projectile).height = (int) ((double) ((Entity) this.Projectile).height * (double) this.Projectile.ai[0] / (double) this.Projectile.scale);
        this.Projectile.scale = this.Projectile.ai[0];
        ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
        SoundStyle soundStyle = SoundID.Item104;
        ((SoundStyle) ref soundStyle).Volume = 0.5f;
        ((SoundStyle) ref soundStyle).Pitch = -0.2f;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter >= 5)
      {
        this.Projectile.frameCounter = 0;
        this.Projectile.frame = (this.Projectile.frame + 1) % 12;
      }
      this.Projectile.alpha = (int) byte.MaxValue - (int) (Math.Cos((double) ++this.Projectile.localAI[0] * 3.1415927410125732 / 2.0 / 60.0) * 200.0);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
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
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 2)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
