// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Boss.MutantFangBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Boss
{
  public class MutantFangBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      player.poisoned = true;
      player.venom = true;
      player.ichor = true;
      player.onFire2 = true;
      player.electrified = true;
      fargoSoulsPlayer.OceanicMaul = true;
      fargoSoulsPlayer.CurseoftheMoon = true;
      if (fargoSoulsPlayer.FirstInfection)
      {
        fargoSoulsPlayer.MaxInfestTime = player.buffTime[buffIndex];
        fargoSoulsPlayer.FirstInfection = false;
      }
      fargoSoulsPlayer.Infested = true;
      fargoSoulsPlayer.Rotting = true;
      fargoSoulsPlayer.MutantNibble = true;
      fargoSoulsPlayer.noDodge = true;
      fargoSoulsPlayer.noSupersonic = true;
      fargoSoulsPlayer.MutantPresence = true;
      fargoSoulsPlayer.MutantFang = true;
      player.moonLeech = true;
    }
  }
}
