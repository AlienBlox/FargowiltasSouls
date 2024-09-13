// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Armor.MasoAbom
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
namespace FargowiltasSouls.Content.Items.Armor
{
  public class MasoAbom : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<MutantArmorHeader>();

    public override int ToggleItemType => ModContent.ItemType<MutantMask>();

    public override void PostUpdateEquips(Player player)
    {
      player.FargoSouls().AbomMinion = true;
      if (player.ownedProjectileCounts[ModContent.ProjectileType<AbomMinion>()] >= 1)
        return;
      FargoSoulsUtil.NewSummonProjectile(((Entity) player).GetSource_Misc(""), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<AbomMinion>(), 900, 10f, ((Entity) player).whoAmI, -1f);
    }
  }
}
