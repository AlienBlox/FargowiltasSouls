// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.LethargicBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class LethargicBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.FargoSouls().AttackSpeed -= 0.25f;
    }

    public virtual void Update(NPC npc, ref int buffIndex)
    {
      if (npc.boss)
        return;
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.realLife, Array.Empty<int>());
      if (npc1 != null && npc1.boss)
        return;
      npc.FargoSouls().Lethargic = true;
    }
  }
}
