﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.MeleeSpeedEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public class MeleeSpeedEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<UniverseHeader>();

    public override int ToggleItemType => ModContent.ItemType<BerserkerSoul>();

    public override void PostUpdateEquips(Player player)
    {
      player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
    }
  }
}