// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Souls.CorruptedBuffForce
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Souls
{
  public class CorruptedBuffForce : ModBuff
  {
    public virtual string Texture => "FargowiltasSouls/Content/Buffs/Souls/CorruptedBuff";

    public virtual void Update(NPC npc, ref int buffIndex)
    {
      if (((IEnumerable<Player>) Main.player).Any<Player>((Func<Player, bool>) (p => p.Alive() && p.HasEffect<EbonwoodEffect>())))
        npc.buffTime[buffIndex] = 60;
      npc.FargoSouls().CorruptedForce = true;
    }
  }
}
