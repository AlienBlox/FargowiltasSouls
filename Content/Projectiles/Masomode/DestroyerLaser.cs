// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.DestroyerLaser
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class DestroyerLaser : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_658";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 8;
      ((Entity) this.Projectile).height = 8;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.scale = 1.8f;
      this.Projectile.tileCollide = false;
      this.Projectile.extraUpdates = 4;
      this.Projectile.timeLeft = 190 * this.Projectile.extraUpdates + 1;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (((Rectangle) ref projHitbox).Intersects(targetHitbox))
        return new bool?(true);
      double num1 = (double) Math.Max((float) ((Entity) this.Projectile).width, ((Vector2) ref ((Entity) this.Projectile).velocity).Length() * 2f);
      float num2 = 0.0f;
      Vector2 vector2_1 = Vector2.op_Multiply((float) (num1 / 2.0) * this.Projectile.scale, Utils.ToRotationVector2(this.Projectile.rotation - MathHelper.ToRadians(135f)));
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, vector2_1);
      Vector2 vector2_3 = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1);
      return Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), vector2_2, vector2_3, (float) (((Entity) this.Projectile).width / 2), ref num2) ? new bool?(true) : new bool?(false);
    }

    public virtual void AI()
    {
      int num = this.Projectile.extraUpdates + 1;
      if ((double) this.Projectile.ai[0] == 1.0 && this.Projectile.timeLeft > 91 * num)
        this.Projectile.timeLeft = 91 * num;
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      if (this.Projectile.timeLeft % num != 0)
        return;
      if ((double) ++this.Projectile.localAI[1] > 20.0 && (double) this.Projectile.localAI[1] < 90.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.06f);
      }
      if (this.Projectile.alpha > 0 && this.Projectile.timeLeft > 10 * num)
      {
        this.Projectile.alpha -= 14;
        if (this.Projectile.alpha >= 0)
          return;
        this.Projectile.alpha = 0;
      }
      else
      {
        if (this.Projectile.timeLeft > 10 * num)
          return;
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.7f);
        this.Projectile.alpha += 25;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.destroyBoss, 134))
        target.AddBuff(144, 60, true, false);
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.brainBoss, 266) || !WorldSavingSystem.MasochistModeReal)
        return;
      target.AddBuff(20, 120, true, false);
      target.AddBuff(22, 120, true, false);
      target.AddBuff(30, 120, true, false);
      target.AddBuff(32, 120, true, false);
      target.AddBuff(33, 120, true, false);
      target.AddBuff(36, 120, true, false);
    }

    public virtual bool? CanDamage()
    {
      return new bool?(this.Projectile.timeLeft > 10 * (this.Projectile.extraUpdates + 1));
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply((!WorldSavingSystem.EternityMode || !SoulConfig.Instance.BossRecolors ? 0 : ((double) this.Projectile.ai[1] == 134.0 ? 1 : 0)) != 0 ? Color.Cyan : Color.Red, this.Projectile.Opacity));
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
      Vector2 vector2_2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_2).\u002Ector(1f, (float) (1.0 + (double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() / 5.0 * (double) (this.Projectile.extraUpdates + 1)));
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, vector2_2, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
