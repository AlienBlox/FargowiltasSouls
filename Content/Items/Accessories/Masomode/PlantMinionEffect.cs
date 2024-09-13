// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.PlantMinionEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Minions;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class PlantMinionEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<ChaliceHeader>();

    public override int ToggleItemType => ModContent.ItemType<MagicalBulb>();

    public override bool MinionEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      if (player.HasBuff<SouloftheMasochistBuff>())
        return;
      player.AddBuff(ModContent.BuffType<PlanterasChildBuff>(), 2, true, false);
    }
  }
}
