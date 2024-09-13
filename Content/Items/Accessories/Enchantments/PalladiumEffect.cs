// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.PalladiumEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class PalladiumEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<EarthHeader>();

    public override int ToggleItemType => ModContent.ItemType<PalladiumEnchant>();

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      int num = player.statLife - fargoSoulsPlayer.StatLifePrevious;
      if (num > 0)
      {
        fargoSoulsPlayer.PalladCounter += num;
        if (fargoSoulsPlayer.PalladCounter > 80)
        {
          fargoSoulsPlayer.PalladCounter = 0;
          if (((Entity) player).whoAmI == Main.myPlayer && player.statLife < player.statLifeMax2)
          {
            int dmg = player.ForceEffect<PalladiumEffect>() ? 100 : 50;
            Projectile.NewProjectile(player.GetSource_Accessory(player.EffectItem<PalladiumEffect>(), (string) null), ((Entity) player).Center, Vector2.op_UnaryNegation(Vector2.UnitY), ModContent.ProjectileType<PalladOrb>(), FargoSoulsUtil.HighestDamageTypeScaling(player, dmg), 10f, ((Entity) player).whoAmI, -1f, 0.0f, 0.0f);
          }
        }
      }
      if (!player.ForceEffect<PalladiumEffect>() && !fargoSoulsPlayer.TerrariaSoul)
        return;
      player.onHitRegen = true;
    }
  }
}
