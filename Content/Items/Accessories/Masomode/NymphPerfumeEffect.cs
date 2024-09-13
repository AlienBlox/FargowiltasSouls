// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.NymphPerfumeEffect
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
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class NymphPerfumeEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<BionomicHeader>();

    public override int ToggleItemType => ModContent.ItemType<NymphsPerfume>();

    public override bool ExtraAttackEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.NymphsPerfume = true;
      if (fargoSoulsPlayer.NymphsPerfumeCD <= 0)
        return;
      fargoSoulsPlayer.NymphsPerfumeCD -= fargoSoulsPlayer.MasochistSoul ? 10 : 1;
    }
  }
}
