// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.DicerYoyo
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  internal class DicerYoyo : ModProjectile
  {
    public int Counter = 1;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.YoyosLifeTimeMultiplier[this.Projectile.type] = 16f;
      ProjectileID.Sets.YoyosMaximumRange[this.Projectile.type] = 400f;
      ProjectileID.Sets.YoyosTopSpeed[this.Projectile.type] = 15f;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(554);
      this.Projectile.extraUpdates = 0;
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.aiStyle = 99;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.scale = 1f;
      this.Projectile.extraUpdates = 1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void AI()
    {
      if (++this.Counter < 60)
        return;
      this.Counter = 0;
      if (this.Projectile.owner != Main.myPlayer)
        return;
      int num = ModContent.ProjectileType<DicerMine>();
      Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center.X, ((Entity) this.Projectile).Center.Y, 0.0f, 0.0f, num, this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public virtual void PostAI()
    {
    }
  }
}
