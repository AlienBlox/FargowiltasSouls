// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.ClownBomb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class ClownBomb : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_75";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(75);
      this.AIType = 75;
      this.Projectile.timeLeft = 300;
      this.Projectile.tileCollide = true;
      this.CooldownSlot = 1;
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.DarkRed);

    public virtual void OnKill(int timeLeft)
    {
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<FusedExplosion>(), this.Projectile.damage * 4, this.Projectile.knockBack, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }
  }
}
