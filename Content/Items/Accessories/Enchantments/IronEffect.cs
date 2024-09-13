﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.IronEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class IronEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<TerraHeader>();

    public override int ToggleItemType => ModContent.ItemType<IronEnchant>();

    public override bool IgnoresMutantPresence => true;
  }
}