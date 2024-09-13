// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Purified.PrimeMinionBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Purified
{
  public class PrimeMinionBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoSave[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      PatreonPlayer modPlayer = player.GetModPlayer<PatreonPlayer>();
      if (player.ownedProjectileCounts[ModContent.ProjectileType<PrimeMinionProj>()] > 0)
        modPlayer.PrimeMinion = true;
      if (!modPlayer.PrimeMinion)
      {
        player.DelBuff(buffIndex);
        --buffIndex;
      }
      else
        player.buffTime[buffIndex] = 18000;
    }
  }
}
