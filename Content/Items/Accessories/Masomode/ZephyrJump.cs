// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.ZephyrJump
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class ZephyrJump : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<DeviEnergyHeader>();

    public override int ToggleItemType => ModContent.ItemType<ZephyrBoots>();

    public override bool IgnoresMutantPresence => true;

    public override void PostUpdateEquips(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      ((ExtraJumpState) ref player.GetJumpState<ExtraJump>(ExtraJump.FartInAJar)).Enable();
      if (Main.netMode != 1)
        return;
      NetMessage.SendData(4, -1, -1, (NetworkText) null, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }
  }
}
