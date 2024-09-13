// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Vortex.VortexVortexProjectile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Vortex
{
  public class VortexVortexProjectile : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 9;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.penetrate = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.timeLeft = 3600;
      this.Projectile.scale = 2f;
    }

    private int GetBiome()
    {
      double num = (double) this.Projectile.localAI[0];
      ref float local = ref this.Projectile.ai[0];
      Player localPlayer = Main.LocalPlayer;
      if (((Entity) Main.projectile[(int) local]).active && (double) ((Entity) Main.projectile[(int) local]).Center.Y - (double) ((Entity) this.Projectile).Center.Y > 0.0)
        return 8;
      if (localPlayer.ZoneCorrupt)
        return 1;
      if (localPlayer.ZoneCrimson)
        return 2;
      if (localPlayer.ZoneHallow)
        return 3;
      if (localPlayer.ZoneSnow)
        return 4;
      if (localPlayer.ZoneDesert)
        return 5;
      if (localPlayer.ZoneJungle)
        return 6;
      if (localPlayer.ZoneBeach)
        return 5;
      return localPlayer.ZoneDungeon ? 7 : 0;
    }

    public virtual void AI()
    {
      ref float local1 = ref this.Projectile.ai[0];
      ref float local2 = ref this.Projectile.ai[1];
      ref float local3 = ref this.Projectile.localAI[0];
      ref float local4 = ref this.Projectile.ai[1];
      if ((double) local2 < 60.0)
      {
        if ((double) local3 <= 0.0)
        {
          local3 = (float) (this.GetBiome() + 1);
          this.Projectile.frame = (int) local3 - 1;
          local4 = Utils.NextFloat(Main.rand, -0.209439516f, 0.209439516f);
        }
        if (this.Projectile.alpha > 0)
          this.Projectile.alpha -= 12;
      }
      else
      {
        Projectile projectile = Main.projectile[(int) local1];
        if (projectile.TypeAlive<VortexVortex>())
        {
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, ((Entity) projectile).Center);
          if ((double) ((Vector2) ref vector2).LengthSquared() < 256.0)
            this.Projectile.Kill();
          int num = 12;
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) projectile).Center, ((Entity) this.Projectile).Center)), (float) num);
        }
        else
          this.Projectile.Kill();
      }
      this.Projectile.rotation += local4;
      ++local2;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      FargoSoulsUtil.GenericProjectileDraw(this.Projectile, lightColor);
      return false;
    }

    public enum Biomes
    {
      Purity,
      Corruption,
      Crimson,
      Hallow,
      Snow,
      Desert,
      Jungle,
      Dungeon,
      Cloud,
    }
  }
}
