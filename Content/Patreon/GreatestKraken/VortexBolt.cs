// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.GreatestKraken.VortexBolt
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.GreatestKraken
{
  public class VortexBolt : LightningArc
  {
    public override string Texture => "Terraria/Images/Projectile_466";

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.usesIDStaticNPCImmunity = false;
      this.Projectile.idStaticNPCHitCooldown = 0;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = false;
      this.Projectile.timeLeft = 30 * (this.Projectile.extraUpdates + 1);
    }
  }
}
