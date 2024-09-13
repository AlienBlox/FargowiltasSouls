// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Earth.EarthGeyser
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Earth
{
  public class EarthGeyser : ModProjectile
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
        ((Entity) this.Projectile).position.Y -= 16f;
        if (!((Tile) ref tileSafely).HasUnactuatedTile || !Main.tileSolid[(int) ((Tile) ref tileSafely).TileType])
        {
          this.Projectile.ai[1] = 1f;
          this.Projectile.netUpdate = true;
        }
      }
      else if (((Tile) ref tileSafely).HasUnactuatedTile && Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] && ((Tile) ref tileSafely).TileType != (ushort) 19 && ((Tile) ref tileSafely).TileType != (ushort) 380)
      {
        if (this.Projectile.timeLeft > 90)
          this.Projectile.timeLeft = 90;
        this.Projectile.extraUpdates = 0;
        ((Entity) this.Projectile).position.Y -= 16f;
        int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, -8f, 0, new Color(), 1f);
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
      }
      else
        ((Entity) this.Projectile).position.Y += 16f;
      if (this.Projectile.timeLeft > 120)
        return;
      Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 0, new Color(), 1f);
    }

    public virtual void OnKill(int timeLeft)
    {
      if (!FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.UnitY, -8f), 654, this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }
  }
}
