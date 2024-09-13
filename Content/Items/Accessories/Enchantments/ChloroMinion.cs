// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.ChloroMinion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class ChloroMinion : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<NatureHeader>();

    public override int ToggleItemType => ModContent.ItemType<ChlorophyteEnchant>();

    public override bool MinionEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer || player.ownedProjectileCounts[ModContent.ProjectileType<Chlorofuck>()] != 0)
        return;
      int rawBaseDamage = player.FargoSouls().ForceEffect<ChlorophyteEnchant>() ? 65 : 35;
      float num = 1.2566371f;
      for (int index = 0; index < 5; ++index)
      {
        Vector2 spawn = Vector2.op_Addition(((Entity) player).Center, Utils.RotatedBy(new Vector2(60f, 0.0f), (double) num * (double) index, new Vector2()));
        FargoSoulsUtil.NewSummonProjectile(this.GetSource_EffectItem(player), spawn, Vector2.Zero, ModContent.ProjectileType<Chlorofuck>(), rawBaseDamage, 10f, ((Entity) player).whoAmI, 50f, num * (float) index);
      }
    }
  }
}
