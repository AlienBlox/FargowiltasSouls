// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.PalmwoodEffect
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
  public class PalmwoodEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<TimberHeader>();

    public override int ToggleItemType => ModContent.ItemType<PalmWoodEnchant>();

    public override bool MinionEffect => true;

    public static void ActivatePalmwoodSentry(Player player)
    {
      if (!player.HasEffect<PalmwoodEffect>() || ((Entity) player).whoAmI != Main.myPlayer)
        return;
      bool flag = player.FargoSouls().ForceEffect<PalmWoodEnchant>();
      Vector2 mouseWorld = Main.MouseWorld;
      int num = 1;
      if (player.ownedProjectileCounts[ModContent.ProjectileType<PalmTreeSentry>()] > num - 1)
      {
        for (int index = 0; index < Main.maxProjectiles; ++index)
        {
          Projectile projectile = Main.projectile[index];
          if (((Entity) projectile).active && projectile.type == ModContent.ProjectileType<PalmTreeSentry>() && projectile.owner == ((Entity) player).whoAmI)
          {
            projectile.Kill();
            break;
          }
        }
      }
      Vector2 vector2 = flag ? Vector2.op_Addition(Vector2.op_Multiply(-40f, Vector2.UnitX), Vector2.op_Multiply(-120f, Vector2.UnitY)) : Vector2.op_Multiply(-41f, Vector2.UnitY);
      FargoSoulsUtil.NewSummonProjectile(((Entity) player).GetSource_Misc(""), Vector2.op_Addition(mouseWorld, vector2), Vector2.Zero, ModContent.ProjectileType<PalmTreeSentry>(), flag ? 100 : 15, 0.0f, ((Entity) player).whoAmI);
    }
  }
}
