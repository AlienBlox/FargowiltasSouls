// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantSpazmarang
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantSpazmarang : MutantRetirang
  {
    public override string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "FargowiltasSouls/Content/Projectiles/BossWeapons/Spazmarang" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantSpazmarang_April";
      }
    }

    public override void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 12;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(39, 120, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
    }
  }
}
