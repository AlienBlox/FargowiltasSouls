// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.DemonKing.DukeFishronBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.DemonKing
{
  public class DukeFishronBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoSave[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.ownedProjectileCounts[ModContent.ProjectileType<DukeFishronMinion>()] > 0)
        fargoSoulsPlayer.DukeFishron = true;
      if (!fargoSoulsPlayer.DukeFishron)
      {
        player.DelBuff(buffIndex);
        --buffIndex;
      }
      else
        player.buffTime[buffIndex] = 18000;
    }
  }
}
