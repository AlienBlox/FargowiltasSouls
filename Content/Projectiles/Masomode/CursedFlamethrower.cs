// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.CursedFlamethrower
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class CursedFlamethrower : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_101";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(101);
      this.AIType = 101;
      this.Projectile.DamageType = DamageClass.Generic;
      this.Projectile.tileCollide = false;
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 400;
    }

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (Utils.NextBool(Main.rand, 6))
        target.AddBuff(39, 480, true, false);
      else if (Utils.NextBool(Main.rand, 4))
        target.AddBuff(39, 300, true, false);
      else if (Utils.NextBool(Main.rand))
        target.AddBuff(39, 180, true, false);
      target.AddBuff(24, 300, true, false);
    }
  }
}
