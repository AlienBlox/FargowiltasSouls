// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.SlimeSpike2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class SlimeSpike2 : SlimeSpike
  {
    public override string Texture
    {
      get => "FargowiltasSouls/Content/Bosses/MutantBoss/MutantSlimeBall_3";
    }

    public override void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public override void SetDefaults()
    {
      base.SetDefaults();
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 14;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1.5f;
    }

    public override void AI()
    {
      base.AI();
      if (++this.Projectile.frameCounter >= 6)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          this.Projectile.frame = 0;
      }
      this.Projectile.rotation += 3.14159274f;
      if (Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
        return;
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.0f, 0.0f, 0.8f);
    }
  }
}
