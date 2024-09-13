// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.CrimsonEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class CrimsonEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<NatureHeader>();

    public override int ToggleItemType => ModContent.ItemType<CrimsonEnchant>();

    public override void OnHurt(Player player, Player.HurtInfo info)
    {
      if (player.HasBuff<CrimsonRegenBuff>())
      {
        player.ClearBuff(ModContent.BuffType<CrimsonRegenBuff>());
      }
      else
      {
        FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
        if (((Player.HurtInfo) ref info).Damage < 10)
          return;
        fargoSoulsPlayer.CrimsonRegenTime = 0;
        float num = 0.5f;
        fargoSoulsPlayer.CrimsonRegenAmount = (int) ((double) ((Player.HurtInfo) ref info).Damage * (double) num);
        player.AddBuff(ModContent.BuffType<CrimsonRegenBuff>(), fargoSoulsPlayer.ForceEffect<CrimsonEnchant>() ? 900 : 430, true, false);
      }
    }
  }
}
