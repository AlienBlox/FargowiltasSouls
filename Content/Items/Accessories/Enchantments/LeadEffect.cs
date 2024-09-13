// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.LeadEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class LeadEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) null;

    public static void ProcessLeadEffectLifeRegen(Player player)
    {
      if (!player.HasEffect<LeadEffect>())
        return;
      if (player.FargoSouls().ForceEffect<LeadEnchant>())
        player.lifeRegen = (int) ((double) player.lifeRegen * 0.40000000596046448);
      else
        player.lifeRegen = (int) ((double) player.lifeRegen * 0.60000002384185791);
    }
  }
}
