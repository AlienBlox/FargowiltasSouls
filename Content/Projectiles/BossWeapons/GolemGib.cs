// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.GolemGib
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class GolemGib : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/BossWeapons/GolemGib1";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.penetrate = 1;
      this.Projectile.timeLeft = 600;
      this.Projectile.ignoreWater = true;
      this.Projectile.extraUpdates = 1;
    }

    public virtual bool PreAI()
    {
      if ((double) this.Projectile.ai[1] == 2.0)
      {
        ((Entity) this.Projectile).width = 34;
        ((Entity) this.Projectile).height = 36;
      }
      else if ((double) this.Projectile.ai[1] == 3.0)
      {
        ((Entity) this.Projectile).width = 24;
        ((Entity) this.Projectile).height = 36;
      }
      else if ((double) this.Projectile.ai[1] == 4.0)
      {
        ((Entity) this.Projectile).width = 32;
        ((Entity) this.Projectile).height = 28;
      }
      else if ((double) this.Projectile.ai[1] == 5.0)
      {
        ((Entity) this.Projectile).width = 36;
        ((Entity) this.Projectile).height = 38;
      }
      else if ((double) this.Projectile.ai[1] == 6.0)
      {
        ((Entity) this.Projectile).width = 52;
        ((Entity) this.Projectile).height = 54;
      }
      else if ((double) this.Projectile.ai[1] == 7.0)
      {
        ((Entity) this.Projectile).width = 40;
        ((Entity) this.Projectile).height = 26;
      }
      else if ((double) this.Projectile.ai[1] == 8.0)
      {
        ((Entity) this.Projectile).width = 62;
        ((Entity) this.Projectile).height = 42;
      }
      else if ((double) this.Projectile.ai[1] == 9.0)
      {
        ((Entity) this.Projectile).width = 14;
        ((Entity) this.Projectile).height = 16;
      }
      else if ((double) this.Projectile.ai[1] == 10.0)
      {
        ((Entity) this.Projectile).width = 34;
        ((Entity) this.Projectile).height = 32;
      }
      else if ((double) this.Projectile.ai[1] == 11.0)
      {
        ((Entity) this.Projectile).width = 18;
        ((Entity) this.Projectile).height = 12;
      }
      else
      {
        ((Entity) this.Projectile).width = 30;
        ((Entity) this.Projectile).height = 42;
      }
      return true;
    }

    public virtual void AI()
    {
      this.Projectile.rotation += (float) (((double) Math.Abs(((Entity) this.Projectile).velocity.X) + (double) Math.Abs(((Entity) this.Projectile).velocity.Y)) * 0.029999999329447746) * (float) ((Entity) this.Projectile).direction;
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] >= 20.0)
      {
        ((Entity) this.Projectile).velocity.Y = ((Entity) this.Projectile).velocity.Y + 0.4f;
        ((Entity) this.Projectile).velocity.X = ((Entity) this.Projectile).velocity.X * 0.97f;
      }
      if ((double) ((Entity) this.Projectile).velocity.Y <= 16.0)
        return;
      ((Entity) this.Projectile).velocity.Y = 16f;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 6;
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      width = 16;
      height = 16;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      if (this.Projectile.owner == Main.myPlayer)
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<GibExplosion>(), this.Projectile.damage, this.Projectile.knockBack * 2f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      return true;
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if (this.Projectile.owner != Main.myPlayer)
        return;
      Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<GibExplosion>(), this.Projectile.damage, this.Projectile.knockBack * 2f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture = (double) this.Projectile.ai[1] == 0.0 ? ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/BossWeapons/" + ((object) this).GetType().Name + 1.ToString(), (AssetRequestMode) 2).Value : ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/BossWeapons/" + ((object) this).GetType().Name + this.Projectile.ai[1].ToString(), (AssetRequestMode) 2).Value;
      FargoSoulsUtil.DrawTexture((object) Main.spriteBatch, texture, 0, (Entity) this.Projectile, new Color?(lightColor), true, new Vector2());
      return false;
    }
  }
}
