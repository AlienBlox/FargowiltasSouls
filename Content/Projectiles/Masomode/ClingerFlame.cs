// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.ClingerFlame
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class ClingerFlame : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_482";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.projFrames[482];
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(482);
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.friendly = false;
      this.Projectile.DamageType = DamageClass.Generic;
      this.Projectile.timeLeft = 600;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool? CanDamage() => new bool?((double) this.Projectile.localAI[0] >= 60.0);

    public virtual void AI()
    {
      if ((double) ++this.Projectile.localAI[0] < 60.0)
      {
        int index1 = Dust.NewDust(((Entity) this.Projectile).Bottom, 0, 0, 75, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index1].velocity.Y -= 2f;
        Dust dust = Main.dust[index1];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
        if (Utils.NextBool(Main.rand, 4))
        {
          Main.dust[index1].scale += 0.5f;
          Main.dust[index1].noGravity = true;
        }
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        for (int index2 = 0; index2 < 2; ++index2)
        {
          Tile tileSafely = Framing.GetTileSafely(((Entity) this.Projectile).Bottom);
          ((Entity) this.Projectile).position.Y += !((Tile) ref tileSafely).HasUnactuatedTile || !Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] && !Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType] ? 16f : -16f;
        }
      }
      else
      {
        int num = (int) ((double) (((Entity) this.Projectile).width * ((Entity) this.Projectile).height) * 0.0044999998062849045);
        for (int index = 0; index < num; ++index)
          new ExpandingBloomParticle(Utils.NextVector2FromRectangle(Main.rand, ((Entity) this.Projectile).Hitbox), new Vector2(Utils.NextFloat(Main.rand, -2f, 2f), Utils.NextFloat(Main.rand, -6f, 6f)), new Color(96, 248, 2), Vector2.op_Multiply(Vector2.One, 0.5f), Vector2.Zero, 10).Spawn();
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(39, 180, true, false);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity) => false;

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = false;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }
  }
}
