// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MoonLordMoonBlast
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MoonLordMoonBlast : MoonLordSunBlast
  {
    public override string Texture => "Terraria/Images/Projectile_645";

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, true, false);
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()))
        return;
      target.FargoSouls().MaxLifeReduction += 100;
      target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
      target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
    }
  }
}
