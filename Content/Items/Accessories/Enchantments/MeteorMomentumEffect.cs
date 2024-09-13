// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.MeteorMomentumEffect
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
  public class MeteorMomentumEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<CosmoHeader>();

    public override int ToggleItemType => ModContent.ItemType<MeteorEnchant>();

    public override void PostUpdateEquips(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      if (!player.FargoSouls().NoMomentum && !player.mount.Active)
      {
        player.runAcceleration *= 1.3f;
        player.runSlowdown *= 1.3f;
      }
      player.hasMagiluminescence = true;
      if (!player.HasEffect<MeteorTrailEffect>())
        return;
      int num = (int) ((double) Main.GlobalTimeWrappedHourly * 60.0) % 60;
      if (!Vector2.op_Inequality(((Entity) player).velocity, Vector2.Zero) || num % 2 != 0)
        return;
      for (int index = -1; index < 2; index += 2)
      {
        Vector2 vector2_1 = Utils.RotatedByRandom(Utils.RotatedBy(Vector2.op_UnaryNegation(((Entity) player).velocity), (double) index * 3.1415927410125732 / 7.0, new Vector2()), 0.2617993950843811);
        int dmg = 22;
        Vector2 center = ((Entity) player).Center;
        Vector2 vector2_2 = Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(((Entity) player).velocity), 1.5707963705062866 * (double) -index, new Vector2()), (float) (((Entity) player).width / 2));
        Projectile.NewProjectile(this.GetSource_EffectItem(player), Vector2.op_Addition(center, vector2_2), vector2_1, ModContent.ProjectileType<MeteorFlame>(), FargoSoulsUtil.HighestDamageTypeScaling(player, dmg), 0.5f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
    }
  }
}
