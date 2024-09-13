// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Life.ChampionBee
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Life
{
  public class ChampionBee : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_566";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 4;
      ((Entity) this.Projectile).height = 4;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 360;
      this.Projectile.aiStyle = -1;
      this.CooldownSlot = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.scale = 1.5f;
    }

    public virtual void AI()
    {
      this.Projectile.spriteDirection = Math.Sign(((Entity) this.Projectile).velocity.X);
      this.Projectile.rotation = ((Entity) this.Projectile).velocity.X * 0.1f;
      if (++this.Projectile.frameCounter >= 3)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= 3)
          this.Projectile.frame = 0;
      }
      if (this.Projectile.timeLeft < 120)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.05f);
      }
      if (!((Entity) this.Projectile).wet && !((Entity) this.Projectile).lavaWet || ((Entity) this.Projectile).honeyWet)
        return;
      this.Projectile.Kill();
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(36, 300, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<InfestedBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<SwarmingBuff>(), 300, true, false);
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
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
