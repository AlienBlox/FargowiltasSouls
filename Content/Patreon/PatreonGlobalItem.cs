// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.PatreonGlobalItem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon
{
  public class PatreonGlobalItem : GlobalItem
  {
    public virtual bool CanUseItem(Item item, Player player)
    {
      if (item.IsWeapon() && player.GetModPlayer<PatreonPlayer>().CompOrb && item.DamageType != DamageClass.Magic && item.DamageType != DamageClass.Summon)
      {
        if (!player.CheckMana(10, true, false))
          return false;
        player.GetModPlayer<PatreonPlayer>().CompOrbDrainCooldown = item.useTime + item.reuseDelay + 30;
      }
      return true;
    }
  }
}
