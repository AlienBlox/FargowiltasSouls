﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Toggler.Content.ColossusHeader
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.Toggler.Content
{
  public class ColossusHeader : SoulHeader
  {
    public override int Item => ModContent.ItemType<ColossusSoul>();

    public override float Priority => 2.3f;
  }
}
