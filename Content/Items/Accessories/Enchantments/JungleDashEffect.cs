// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.JungleDashEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class JungleDashEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<NatureHeader>();

    public override int ToggleItemType => ModContent.ItemType<JungleEnchant>();

    public override bool IgnoresMutantPresence => true;

    public static void AddDash(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.HasDash)
        return;
      fargoSoulsPlayer.HasDash = true;
      fargoSoulsPlayer.FargoDash = DashManager.DashType.Jungle;
    }

    public static void JungleDash(Player player, int direction)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      float num = fargoSoulsPlayer.ChlorophyteEnchantActive ? 12f : 9f;
      ((Entity) player).velocity.X = num * (float) direction;
      if (fargoSoulsPlayer.IsDashingTimer < 10)
        fargoSoulsPlayer.IsDashingTimer = 10;
      player.dashDelay = 60;
      if (Main.netMode != 1)
        return;
      NetMessage.SendData(13, -1, -1, (NetworkText) null, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }
  }
}
