// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.VultureFeather
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class VultureFeather : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(38);
      this.Projectile.aiStyle = 1;
      this.Projectile.hostile = true;
      this.Projectile.friendly = false;
      this.Projectile.timeLeft = 600;
      this.Projectile.tileCollide = true;
      this.CooldownSlot = 1;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index = 0; index < 10; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 42, ((Entity) this.Projectile).velocity.X * 0.1f, ((Entity) this.Projectile).velocity.Y * 0.1f, 0, Color.SandyBrown, 1f);
    }
  }
}
