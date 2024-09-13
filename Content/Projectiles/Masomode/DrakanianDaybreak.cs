// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.DrakanianDaybreak
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class DrakanianDaybreak : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(636);
      this.AIType = 636;
      this.Projectile.friendly = false;
      this.Projectile.hostile = true;
      this.Projectile.DamageType = DamageClass.Default;
    }

    public virtual void AI()
    {
      this.Projectile.alpha = 0;
      this.Projectile.hide = false;
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(24, 900, true, false);
      target.AddBuff(67, 180, true, false);
    }
  }
}
