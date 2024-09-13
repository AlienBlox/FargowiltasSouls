// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.EyeFireFriendly
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class EyeFireFriendly : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_101";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(101);
      this.AIType = 101;
      this.Projectile.friendly = true;
      this.Projectile.hostile = false;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.tileCollide = true;
      this.Projectile.penetrate = 2;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(39, 120, false);
    }
  }
}
