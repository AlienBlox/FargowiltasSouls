// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSwordBlast
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HentaiSwordBlast : MoonLordSunBlast
  {
    public override string Texture => "Terraria/Images/Projectile_645";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.friendly = true;
      this.Projectile.hostile = false;
      this.Projectile.DamageType = DamageClass.Melee;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, false);
      target.immune[this.Projectile.owner] = 1;
    }
  }
}
