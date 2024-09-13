// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Sasha.FishMinionBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Sasha
{
  public class FishMinionBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoSave[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      PatreonPlayer modPlayer = player.GetModPlayer<PatreonPlayer>();
      if (player.ownedProjectileCounts[ModContent.ProjectileType<FishMinion>()] > 0)
        modPlayer.FishMinion = true;
      if (!modPlayer.FishMinion)
      {
        player.DelBuff(buffIndex);
        --buffIndex;
      }
      else
        player.buffTime[buffIndex] = 18000;
    }
  }
}
