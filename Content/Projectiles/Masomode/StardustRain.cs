// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.StardustRain
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class StardustRain : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_539";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 18;
      ((Entity) this.Projectile).height = 18;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.aiStyle = -1;
      this.Projectile.hide = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      if ((double) ++this.Projectile.ai[0] <= 2.0)
        return;
      this.Projectile.ai[0] = 0.0f;
      if (FargoSoulsUtil.HostCheck)
      {
        for (int index = -1; index <= 1; ++index)
        {
          if (index != 0)
          {
            Vector2 center = ((Entity) this.Projectile).Center;
            center.X += 160f * this.Projectile.ai[1] * (float) index;
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), center, Vector2.op_Multiply(Vector2.UnitY, 7f), 539, this.Projectile.damage, 0.0f, Main.myPlayer, 210f, 0.0f, 0.0f);
          }
        }
      }
      if ((double) ++this.Projectile.ai[1] <= 10.0)
        return;
      this.Projectile.Kill();
    }
  }
}
