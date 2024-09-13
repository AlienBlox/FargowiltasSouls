// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.MahoganyEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class MahoganyEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<TimberHeader>();

    public override int ToggleItemType => ModContent.ItemType<RichMahoganyEnchant>();

    public override bool IgnoresMutantPresence => true;

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      bool flag = fargoSoulsPlayer.ForceEffect<RichMahoganyEnchant>();
      if (player.grapCount > 0)
      {
        player.thorns += flag ? 5f : 0.5f;
        if (!fargoSoulsPlayer.MahoganyCanUseDR)
          return;
        player.endurance += flag ? 0.3f : 0.1f;
      }
      else
        fargoSoulsPlayer.MahoganyCanUseDR = true;
    }

    public static void MahoganyHookAI(Projectile projectile, FargoSoulsPlayer modPlayer)
    {
      if (projectile.extraUpdates >= 1)
        return;
      projectile.extraUpdates = 1;
    }
  }
}
