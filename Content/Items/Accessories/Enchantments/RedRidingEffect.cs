// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.RedRidingEffect
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
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class RedRidingEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<WillHeader>();

    public override bool ExtraAttackEffect => true;

    public override int ToggleItemType => ModContent.ItemType<RedRidingEnchant>();

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.RedRidingArrowCD <= 0)
        return;
      --fargoSoulsPlayer.RedRidingArrowCD;
    }

    public static void SpawnArrowRain(Player player, NPC target)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      Item obj1 = player.EffectItem<RedRidingEffect>();
      Item obj2 = RedRidingEffect.PickAmmo(player);
      int shoot = obj2.shoot;
      int dmg = obj2.damage * (fargoSoulsPlayer.ForceEffect<RedRidingEnchant>() ? 7 : 5);
      int index = Projectile.NewProjectile(player.GetSource_Accessory(obj1, (string) null), ((Entity) player).Center, new Vector2(0.0f, -6f), 260, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      Main.projectile[index].tileCollide = false;
      Projectile.NewProjectile(player.GetSource_Accessory(obj1, (string) null), ((Entity) target).Center.X, ((Entity) player).Center.Y - 500f, 0.0f, 0.0f, ModContent.ProjectileType<ArrowRain>(), FargoSoulsUtil.HighestDamageTypeScaling(player, dmg), 0.0f, ((Entity) player).whoAmI, (float) shoot, (float) ((Entity) target).whoAmI, 0.0f);
      fargoSoulsPlayer.RedRidingArrowCD = fargoSoulsPlayer.ForceEffect<RedRidingEnchant>() ? 240 : 360;
    }

    private static Item PickAmmo(Player player)
    {
      Item obj = new Item();
      bool flag = false;
      for (int index = 54; index < 58; ++index)
      {
        if (player.inventory[index].ammo == AmmoID.Arrow && player.inventory[index].stack > 0)
        {
          obj = player.inventory[index];
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        for (int index = 0; index < 54; ++index)
        {
          if (player.inventory[index].ammo == AmmoID.Arrow && player.inventory[index].stack > 0)
          {
            obj = player.inventory[index];
            break;
          }
        }
      }
      if (obj.ammo != AmmoID.Arrow)
        obj.SetDefaults(1341);
      return obj;
    }
  }
}
