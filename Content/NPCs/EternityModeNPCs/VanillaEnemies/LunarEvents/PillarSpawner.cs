// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.PillarSpawner
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents
{
  public class PillarSpawner : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 12;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual string Texture => FargoSoulsUtil.EmptyTexture;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.DamageType = DamageClass.Default;
      this.Projectile.friendly = false;
      this.Projectile.hostile = false;
      this.Projectile.tileCollide = true;
      this.Projectile.timeLeft = 1800;
      this.Projectile.scale = 1f;
      this.AIType = 0;
      this.Projectile.aiStyle = 0;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.ai[0] != 2.0)
        return;
      this.Projectile.Kill();
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      if ((double) this.Projectile.ai[0] == 2.0)
      {
        int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
        if (closest.IsWithinBounds((int) byte.MaxValue))
        {
          Player player = Main.player[closest];
          if (((Entity) player).active && (double) ((Entity) player).Center.Y > (double) ((Entity) this.Projectile).Center.Y)
            return false;
        }
      }
      this.Projectile.Kill();
      return true;
    }

    public virtual void OnKill(int timeLeft)
    {
      if ((double) this.Projectile.ai[0] == 1.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item34, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        if (!FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<SolarFlamePillar>(), this.Projectile.damage, this.Projectile.knockBack, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      else
        Main.NewText("you shouldn't be seeing this, show javyz", byte.MaxValue, byte.MaxValue, byte.MaxValue);
    }
  }
}
