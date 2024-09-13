// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.SilverSword
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class SilverSword : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionTargettingFeature[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      this.Projectile.CloneDefaults(533);
      this.AIType = 533;
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 18000;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.minionSlots = 0.0f;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      player.FargoSouls();
      if (((Entity) player).whoAmI == Main.myPlayer && !player.HasEffect<SilverEffect>())
      {
        this.Projectile.Kill();
      }
      else
      {
        int index1 = Dust.NewDust(new Vector2(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).position.Y + 2f), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height + 5, 245, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 1f);
        Main.dust[index1].noGravity = true;
        int index2 = Dust.NewDust(new Vector2(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).position.Y + 2f), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height + 5, 245, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 1f);
        Main.dust[index2].noGravity = true;
      }
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      if ((double) ((Entity) this.Projectile).velocity.X != (double) oldVelocity.X)
        ((Entity) this.Projectile).velocity.X = oldVelocity.X;
      if ((double) ((Entity) this.Projectile).velocity.Y != (double) oldVelocity.Y)
        ((Entity) this.Projectile).velocity.Y = oldVelocity.Y;
      return false;
    }
  }
}
