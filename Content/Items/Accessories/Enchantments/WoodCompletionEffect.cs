// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.WoodCompletionEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class WoodCompletionEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<TimberHeader>();

    public override int ToggleItemType => ModContent.ItemType<WoodEnchant>();

    public static void WoodCheckDead(FargoSoulsPlayer modPlayer, NPC npc)
    {
      if (npc.ExcludedFromDeathTally())
        return;
      int index1 = Item.NPCtoBanner(npc.BannerID());
      if (index1 <= 0)
        return;
      int num1 = 1;
      if (modPlayer.ForceEffect<WoodEnchant>())
        num1 = 4;
      int num2 = ItemID.Sets.KillsToBanner[Item.BannerToItem(index1)];
      for (int index2 = 0; index2 < num1; ++index2)
      {
        if (NPC.killCount[index1] % num2 != num2 - 1)
        {
          ++NPC.killCount[index1];
          Main.BestiaryTracker.Kills.RegisterKill(npc);
        }
      }
    }
  }
}
