// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.ProbeLaser
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class ProbeLaser : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_389";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 4;
      ((Entity) this.Projectile).height = 4;
      this.Projectile.aiStyle = 1;
      this.AIType = 389;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = 3;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.extraUpdates = 2;
      this.Projectile.scale = 1.2f;
      this.Projectile.timeLeft = 600;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.tileCollide = false;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
    }

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual void AI()
    {
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.56f, 0.0f, 0.35f);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }
  }
}
