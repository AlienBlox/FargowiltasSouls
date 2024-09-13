// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Consumables.DeerSinewEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Consumables
{
  public class DeerSinewEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<DeviEnergyHeader>();

    public override int ToggleItemType => ModContent.ItemType<DeerSinew>();

    public override bool IgnoresMutantPresence => true;

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.DeerSinewFreezeCD <= 0)
        return;
      --fargoSoulsPlayer.DeerSinewFreezeCD;
    }

    public static void AddDash(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.HasDash || player.mount.Active)
        return;
      fargoSoulsPlayer.HasDash = true;
      fargoSoulsPlayer.FargoDash = DashManager.DashType.DeerSinew;
      fargoSoulsPlayer.DeerSinewNerf = true;
      if (fargoSoulsPlayer.IsDashingTimer <= 0)
        return;
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 88, 0.0f, 0.0f, 0, new Color(), 1.25f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.2f);
      }
    }
  }
}
