﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.InfestedBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class InfestedBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.FirstInfection)
      {
        fargoSoulsPlayer.MaxInfestTime = player.buffTime[buffIndex];
        fargoSoulsPlayer.FirstInfection = false;
      }
      fargoSoulsPlayer.Infested = true;
    }

    public virtual void Update(NPC npc, ref int buffIndex) => npc.FargoSouls().Infested = true;
  }
}
