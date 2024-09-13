// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.DarkArtistMinion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Minions;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class DarkArtistMinion : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<ShadowHeader>();

    public override int ToggleItemType => ModContent.ItemType<DarkArtistEnchant>();

    public override bool MinionEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      if (player.ownedProjectileCounts[ModContent.ProjectileType<FlameburstMinion>()] != 0 || ((Entity) player).whoAmI != Main.myPlayer)
        return;
      Projectile.NewProjectileDirect(this.GetSource_EffectItem(player), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<FlameburstMinion>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f).netUpdate = true;
    }
  }
}
