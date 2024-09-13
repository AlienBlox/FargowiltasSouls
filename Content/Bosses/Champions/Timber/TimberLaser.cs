// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Timber.TimberLaser
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Timber
{
  public class TimberLaser : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 6;
      ((Entity) this.Projectile).height = 6;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 600;
      this.Projectile.extraUpdates = 2;
      this.Projectile.ignoreWater = true;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.CooldownSlot = 1;
      this.Projectile.scale = 2f;
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<TimberChampionHead>());
      if (npc != null && this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) npc).Hitbox))
      {
        SoundEngine.PlaySound(ref SoundID.NPCHit4, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        for (int index = 0; index < 10; ++index)
          Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 226, ((Entity) this.Projectile).velocity.X * 0.4f, (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.40000000596046448), 0, new Color(), 1f);
        this.Projectile.Kill();
      }
      else
      {
        if (this.Projectile.alpha > 0)
        {
          this.Projectile.alpha -= 10;
          if (this.Projectile.alpha < 0)
            this.Projectile.alpha = 0;
        }
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<GuiltyBuff>(), 300, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return this.Projectile.alpha < 200 ? new Color?(new Color((int) byte.MaxValue - this.Projectile.alpha, (int) byte.MaxValue - this.Projectile.alpha, (int) byte.MaxValue - this.Projectile.alpha, 0)) : new Color?(Color.Transparent);
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
