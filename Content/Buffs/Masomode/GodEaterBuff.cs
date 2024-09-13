// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.GodEaterBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class GodEaterBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = true;
      BuffID.Sets.IsATagBuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.FargoSouls().GodEater = true;
      player.FargoSouls().noDodge = true;
      player.FargoSouls().MutantPresence = true;
      player.moonLeech = true;
    }

    public virtual void Update(NPC npc, ref int buffIndex)
    {
      npc.defense = 0;
      npc.defDefense = 0;
      npc.FargoSouls().GodEater = true;
      npc.FargoSouls().HellFire = true;
    }
  }
}
