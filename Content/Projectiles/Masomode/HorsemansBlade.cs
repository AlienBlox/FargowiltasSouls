// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.HorsemansBlade
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
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
  public class HorsemansBlade : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Item_1826";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.scale = 1.15f;
      this.Projectile.timeLeft = 300;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      ++this.Projectile.ai[1];
      if ((double) this.Projectile.ai[1] > 60.0)
      {
        ((Entity) this.Projectile).velocity.X *= 0.97f;
        ((Entity) this.Projectile).velocity.Y += 0.45f;
      }
      else if ((double) this.Projectile.ai[1] == 60.0 && FargoSoulsUtil.HostCheck)
      {
        for (int index = 0; index < 4; ++index)
          Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(((Entity) this.Projectile).velocity), Math.PI / 2.0 * (double) index, new Vector2()), 8f), ModContent.ProjectileType<FlamingJack>(), this.Projectile.damage, 0.0f, Main.myPlayer, this.Projectile.ai[0], 30f, 0.0f);
      }
      this.Projectile.rotation += ((Vector2) ref ((Entity) this.Projectile).velocity).Length() / ((double) ((Entity) this.Projectile).velocity.X > 0.0 ? 30f : -30f);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(24, 600, true, false);
      target.AddBuff(ModContent.BuffType<LivingWastelandBuff>(), 600, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(new Color(200, 200, 200, 0));

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
