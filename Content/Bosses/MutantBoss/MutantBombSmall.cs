// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantBombSmall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantBombSmall : MutantBomb
  {
    public override string Texture
    {
      get => "Terraria/Images/Projectile_" + (FargoSoulsUtil.AprilFools ? "687" : "645");
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      ((Entity) this.Projectile).width = 275;
      ((Entity) this.Projectile).height = 275;
      this.Projectile.scale = 0.75f;
      this.Projectile.FargoSouls().TimeFreezeImmune = false;
    }

    public virtual bool? CanDamage()
    {
      if (this.Projectile.frame <= 2 || this.Projectile.frame > 4)
        return new bool?(true);
      this.Projectile.FargoSouls().GrazeCD = 1;
      return new bool?(false);
    }

    public override void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 6.28318548f);
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if (++this.Projectile.frameCounter < 3)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame < Main.projFrames[this.Projectile.type])
        return;
      --this.Projectile.frame;
      this.Projectile.Kill();
    }
  }
}
