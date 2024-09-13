// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Minions.BrainMinionBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Minions;
using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Minions
{
  public class BrainMinionBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoSave[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.ownedProjectileCounts[ModContent.ProjectileType<BrainMinion>()] > 0)
        fargoSoulsPlayer.BrainMinion = true;
      if (!fargoSoulsPlayer.BrainMinion)
      {
        player.DelBuff(buffIndex);
        --buffIndex;
      }
      else
        player.buffTime[buffIndex] = 18000;
    }
  }
}
