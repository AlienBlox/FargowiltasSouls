// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.GolemGeyser
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class GolemGeyser : ModProjectile
  {
    public virtual string Texture => FargoSoulsUtil.EmptyTexture;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 2;
      ((Entity) this.Projectile).height = 2;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 600;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.hide = true;
      this.Projectile.extraUpdates = 14;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      Tile tileSafely = Framing.GetTileSafely(((Entity) this.Projectile).Center);
      if ((double) this.Projectile.ai[1] == 0.0)
      {
        ((Entity) this.Projectile).position.Y += 16f;
        if (((Tile) ref tileSafely).HasUnactuatedTile && Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] && ((Tile) ref tileSafely).TileType != (ushort) 19 && ((Tile) ref tileSafely).TileType != (ushort) 380)
          return;
        this.Projectile.ai[1] = 1f;
        this.Projectile.netUpdate = true;
      }
      else if (((Tile) ref tileSafely).HasUnactuatedTile && Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] && ((Tile) ref tileSafely).TileType != (ushort) 19 && ((Tile) ref tileSafely).TileType != (ushort) 380)
        this.Projectile.Kill();
      else
        ((Entity) this.Projectile).position.Y -= 16f;
    }

    public virtual void OnKill(int timeLeft)
    {
      if (!FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.UnitY, ModContent.ProjectileType<GolemDeathraySmall>(), this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }
  }
}
