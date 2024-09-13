// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.HuntressEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class HuntressEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<WillHeader>();

    public override int ToggleItemType => ModContent.ItemType<HuntressEnchant>();

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.HuntressCD <= 0)
        return;
      --fargoSoulsPlayer.HuntressCD;
    }

    public override void ModifyHitNPCWithProj(
      Player player,
      Projectile proj,
      NPC target,
      ref NPC.HitModifiers modifiers)
    {
      FargoSoulsGlobalProjectile globalProjectile = proj.FargoSouls();
      if (globalProjectile.HuntressProj != 1 || target.type == 488)
        return;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      globalProjectile.HuntressProj = 2;
      bool flag = player.HasEffect<RedRidingEffect>();
      if (fargoSoulsPlayer.HuntressCD == 0)
      {
        ++fargoSoulsPlayer.HuntressStage;
        if (fargoSoulsPlayer.HuntressStage >= 10)
        {
          fargoSoulsPlayer.HuntressStage = 10;
          if (flag && fargoSoulsPlayer.RedRidingArrowCD == 0)
            RedRidingEffect.SpawnArrowRain(fargoSoulsPlayer.Player, target);
        }
        fargoSoulsPlayer.HuntressCD = 30;
      }
      int num = fargoSoulsPlayer.ForceEffect<HuntressEnchant>() | flag ? 5 : 3;
      proj.ArmorPenetration = num * 2 * fargoSoulsPlayer.HuntressStage;
      modifiers.SourceDamage.Flat += (float) (num * fargoSoulsPlayer.HuntressStage);
    }
  }
}
