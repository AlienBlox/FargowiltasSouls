// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.NeurotoxinBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class NeurotoxinBuff : ModBuff
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
      player.slowOgreSpit = true;
      player.ClearBuff(ModContent.BuffType<InfestedBuff>());
      fargoSoulsPlayer.MaxInfestTime = 2;
      fargoSoulsPlayer.FirstInfection = false;
      fargoSoulsPlayer.Infested = true;
    }
  }
}
