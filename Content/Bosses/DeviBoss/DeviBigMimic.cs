// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.DeviBoss.DeviBigMimic
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.DeviBoss
{
  public class DeviBigMimic : DeviMimic
  {
    public override void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 8;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      ((Entity) this.Projectile).width = 48;
      ((Entity) this.Projectile).height = 42;
    }

    public override void AI()
    {
      base.AI();
      Player player = FargoSoulsUtil.PlayerExists(this.Projectile.ai[0]);
      if (player == null)
        return;
      this.Projectile.tileCollide = (double) ((Entity) this.Projectile).position.Y + (double) ((Entity) this.Projectile).height >= (double) ((Entity) player).position.Y + (double) ((Entity) player).height - 32.0;
    }

    public override void OnKill(int timeLeft)
    {
      if (FargoSoulsUtil.HostCheck)
      {
        for (int index1 = 0; index1 < 5; ++index1)
        {
          int index2 = Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).position.X + (float) Main.rand.Next(((Entity) this.Projectile).width), ((Entity) this.Projectile).position.Y + (float) Main.rand.Next(((Entity) this.Projectile).height), (float) Main.rand.Next(-30, 31) * 0.1f, (float) Main.rand.Next(-40, -15) * 0.1f, ModContent.ProjectileType<FakeHeart>(), 20, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          if (index2 != Main.maxProjectiles && !WorldSavingSystem.MasochistModeReal)
            Main.projectile[index2].timeLeft = 120 + Main.rand.Next(60);
        }
      }
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 200;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      base.OnKill(timeLeft);
    }
  }
}
