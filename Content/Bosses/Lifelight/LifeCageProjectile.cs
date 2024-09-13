// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeCageProjectile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeCageProjectile : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 24;
      ((Entity) this.Projectile).height = 24;
      this.Projectile.aiStyle = 0;
      this.Projectile.hostile = false;
      this.AIType = 14;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.light = 0.5f;
    }

    public virtual void AI()
    {
      if (((Entity) Main.LocalPlayer).active && this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) Main.LocalPlayer).Hitbox))
      {
        float num = this.Projectile.ai[0];
        if ((double) num != 0.0)
        {
          if ((double) num != 1.0)
          {
            if ((double) num != 2.0)
            {
              if ((double) num == 3.0)
              {
                ((Entity) Main.LocalPlayer).velocity.Y = (float) (((double) ((Entity) Main.LocalPlayer).Center.Y - (double) ((Entity) this.Projectile).Center.Y) * 0.20000000298023224);
                Main.LocalPlayer.wingTime = (float) Main.LocalPlayer.wingTimeMax;
              }
            }
            else
              ((Entity) Main.LocalPlayer).velocity.Y = (float) (((double) ((Entity) Main.LocalPlayer).Center.Y - (double) ((Entity) this.Projectile).Center.Y) * 0.20000000298023224);
          }
          else
            ((Entity) Main.LocalPlayer).velocity.X = (float) (((double) ((Entity) Main.LocalPlayer).Center.X - (double) ((Entity) this.Projectile).Center.X) * 0.20000000298023224);
        }
        else
          ((Entity) Main.LocalPlayer).velocity.X = (float) (((double) ((Entity) Main.LocalPlayer).Center.X - (double) ((Entity) this.Projectile).Center.X) * 0.20000000298023224);
        if (Main.LocalPlayer.grapCount > 0)
          Main.LocalPlayer.RemoveAllGrapplingHooks();
        SoundEngine.PlaySound(ref SoundID.Item56, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if (NPC.CountNPCS(ModContent.NPCType<LifeChallenger>()) >= 1)
        return;
      this.Projectile.Kill();
    }
  }
}
