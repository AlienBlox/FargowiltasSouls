// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomSickleSplit1
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.AbomBoss
{
  public class AbomSickleSplit1 : AbomSickle
  {
    public virtual string Texture => "FargowiltasSouls/Content/Bosses/AbomBoss/AbomSickle";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft = 90;
    }

    public override void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      this.Projectile.rotation += 0.8f;
    }

    public virtual void OnKill(int timeLeft)
    {
      if (!FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < 8; ++index)
      {
        Vector2 vector2 = Utils.RotatedBy(Vector2.Normalize(((Entity) this.Projectile).velocity), Math.PI / 4.0 * (double) index, new Vector2());
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2, ModContent.ProjectileType<AbomSickleSplit2>(), this.Projectile.damage, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      }
    }
  }
}
