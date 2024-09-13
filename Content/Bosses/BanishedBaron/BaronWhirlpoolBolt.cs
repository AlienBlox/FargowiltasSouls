// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.BanishedBaron.BaronWhirlpoolBolt
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.BanishedBaron
{
  public class BaronWhirlpoolBolt : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_385";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Type] = 3;
      ProjectileID.Sets.TrailCacheLength[this.Type] = 10;
      ProjectileID.Sets.TrailingMode[this.Type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 62;
      ((Entity) this.Projectile).height = 62;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.light = 1f;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(30, 360, true, false);
      int num = WorldSavingSystem.EternityMode ? 1 : 0;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      return new bool?((double) ((Entity) this.Projectile).Distance(FargoSoulsUtil.ClosestPointInHitbox(targetHitbox, ((Entity) this.Projectile).Center)) < (double) (projHitbox.Width / 2));
    }

    public virtual void AI()
    {
      ref float local = ref this.Projectile.ai[0];
      if ((double) ++this.Projectile.localAI[0] > 600.0)
        this.Projectile.Kill();
      if (!this.Projectile.tileCollide && (double) this.Projectile.localAI[0] > (double) (60 * this.Projectile.MaxUpdates) && (double) local == 1.0 && !WorldSavingSystem.MasochistModeReal)
      {
        Tile tileSafely = Framing.GetTileSafely(((Entity) this.Projectile).Center);
        if (!((Tile) ref tileSafely).HasUnactuatedTile || !Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] || Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType])
          this.Projectile.tileCollide = true;
      }
      if (++this.Projectile.frameCounter > 2)
      {
        if (++this.Projectile.frame >= Main.projFrames[this.Type])
          this.Projectile.frame = 0;
        this.Projectile.frameCounter = 0;
      }
      float num1 = local;
      if ((double) num1 != 1.0)
      {
        if ((double) num1 != 2.0)
          return;
        int num2 = Math.Sign(this.Projectile.ai[1]);
        int num3 = WorldSavingSystem.MasochistModeReal ? 14 : (WorldSavingSystem.EternityMode ? 12 : 10);
        if ((double) Math.Abs(((Entity) this.Projectile).velocity.X) >= (double) num3)
          return;
        ((Entity) this.Projectile).velocity.X += (float) num2 * 0.12f;
      }
      else
      {
        int num4 = Math.Sign(((Entity) this.Projectile).velocity.X);
        int num5 = WorldSavingSystem.MasochistModeReal ? 16 : (WorldSavingSystem.EternityMode ? 14 : 12);
        if ((double) Math.Abs(((Entity) this.Projectile).velocity.X) >= (double) num5)
          return;
        ((Entity) this.Projectile).velocity.X += (float) num4 * 0.06f;
      }
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      FargoSoulsUtil.ProjectileWithTrailDraw(this.Projectile, lightColor, additiveTrail: true, alsoAdditiveMainSprite: false);
      return false;
    }
  }
}
