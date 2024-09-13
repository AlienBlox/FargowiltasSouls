// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.StardustMinionEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class StardustMinionEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<CosmoHeader>();

    public override int ToggleItemType => ModContent.ItemType<StardustEnchant>();

    public override bool MinionEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer || player.ownedProjectileCounts[623] >= 1)
        return;
      FargoSoulsUtil.NewSummonProjectile(this.GetSource_EffectItem(player), ((Entity) player).Center, Vector2.Zero, 623, 30, 10f, Main.myPlayer);
    }
  }
}
