﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Placables.Relics.AbomRelic
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Placables.Relics
{
  public class AbomRelic : BaseRelic
  {
    protected override int TileType => ModContent.TileType<FargowiltasSouls.Content.Tiles.Relics.AbomRelic>();

    public override void SetStaticDefaults() => base.SetStaticDefaults();
  }
}