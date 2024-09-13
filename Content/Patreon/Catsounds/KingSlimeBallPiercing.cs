// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Catsounds.KingSlimeBallPiercing
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Catsounds
{
  public class KingSlimeBallPiercing : SlimeBall
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/BossWeapons/SlimeBall";

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.penetrate = 2;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 10;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
    }
  }
}
