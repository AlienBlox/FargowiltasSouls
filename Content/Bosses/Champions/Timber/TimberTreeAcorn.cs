// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Timber.TimberTreeAcorn
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Timber
{
  public class TimberTreeAcorn : TimberAcorn
  {
    public virtual string Texture => "FargowiltasSouls/Content/Bosses/Champions/Timber/TimberAcorn";

    public override void SetStaticDefaults()
    {
    }

    public override void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 90;
      this.Projectile.tileCollide = false;
      this.Projectile.extraUpdates = 1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public override void OnKill(int timeLeft)
    {
      base.OnKill(timeLeft);
      if (!FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Subtraction(Vector2.op_Subtraction(((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity), Vector2.op_Multiply(Vector2.UnitY, 160f)), Vector2.Zero, ModContent.ProjectileType<TimberTree>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, this.Projectile.ai[0], 0.0f, 0.0f);
    }
  }
}
