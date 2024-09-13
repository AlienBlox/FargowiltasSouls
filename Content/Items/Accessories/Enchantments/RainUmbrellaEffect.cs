// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.RainUmbrellaEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class RainUmbrellaEffect : AccessoryEffect
  {
    public override int ToggleItemType => ModContent.ItemType<RainEnchant>();

    public override Header ToggleHeader => (Header) Header.GetHeader<NatureHeader>();

    public override bool MinionEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      if (player.HasBuff(ModContent.BuffType<RainCDBuff>()))
        return;
      player.FargoSouls().AddMinion(this.EffectItem(player), true, ModContent.ProjectileType<RainUmbrella>(), 0, 0.0f);
      if (player.controlDown)
        return;
      player.slowFall = true;
    }
  }
}
