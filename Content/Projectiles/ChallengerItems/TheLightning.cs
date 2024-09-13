// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.ChallengerItems.TheLightning
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.ChallengerItems
{
  public class TheLightning : LightningArc
  {
    private float collideHeight;

    public override string Texture => "Terraria/Images/Projectile_466";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.tileCollide = false;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.usesIDStaticNPCImmunity = false;
      this.Projectile.idStaticNPCHitCooldown = 0;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = false;
    }

    public virtual bool PreAI()
    {
      if ((double) this.collideHeight == 0.0)
      {
        this.collideHeight = this.Projectile.ai[1];
        this.Projectile.ai[1] = (float) Main.rand.Next(80);
        this.Projectile.netUpdate = true;
      }
      return base.PreAI();
    }

    public override void AI()
    {
      base.AI();
      if ((double) ((Entity) this.Projectile).Center.Y <= (double) this.collideHeight)
        return;
      this.Projectile.tileCollide = true;
    }
  }
}
