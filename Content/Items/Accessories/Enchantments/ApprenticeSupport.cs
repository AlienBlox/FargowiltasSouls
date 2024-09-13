// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.ApprenticeSupport
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class ApprenticeSupport : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<ShadowHeader>();

    public override int ToggleItemType => ModContent.ItemType<ApprenticeEnchant>();

    public override bool ExtraAttackEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      bool flag = fargoSoulsPlayer.ForceEffect<ApprenticeEnchant>();
      for (int index = 0; index < 10; ++index)
      {
        int apprenticeItemCd = fargoSoulsPlayer.ApprenticeItemCDs[index];
        if (apprenticeItemCd > 0)
        {
          int num = apprenticeItemCd - 1;
          fargoSoulsPlayer.ApprenticeItemCDs[index] = num;
        }
      }
      if (!player.controlUseItem)
        return;
      int num1 = 1;
      if (fargoSoulsPlayer.DarkArtistEnchantActive & flag)
        num1 = 3;
      else if (fargoSoulsPlayer.DarkArtistEnchantActive | flag)
        num1 = 2;
      if (!player.controlUseItem)
        return;
      Item heldItem = player.HeldItem;
      if (heldItem.damage <= 0 || !player.HasAmmo(heldItem) || heldItem.mana > 0 && player.statMana < heldItem.mana || heldItem.createTile != -1 || heldItem.createWall != -1 || heldItem.ammo != AmmoID.None || heldItem.hammer != 0 || heldItem.pick != 0 || heldItem.axe != 0)
        return;
      int num2 = 0;
      for (int index = 0; index < 10; ++index)
      {
        if (player.inventory[index].type == heldItem.type)
        {
          num2 = index;
          break;
        }
      }
      int num3 = 0;
      for (int index1 = num2; index1 < 10; ++index1)
      {
        Item obj = player.inventory[index1];
        if (obj != null && obj.damage > 0 && obj.shoot > 0 && obj.ammo <= 0 && heldItem.type != obj.type && !obj.channel && player.HasAmmo(obj) && (obj.mana <= 0 || player.statMana >= obj.mana) && !obj.sentry && !ContentSamples.ProjectilesByType[obj.shoot].minion)
        {
          ++num3;
          if (num3 > num1)
            break;
          if (fargoSoulsPlayer.ApprenticeItemCDs[index1] <= 0)
          {
            Vector2 vector2_1;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2_1).\u002Ector(((Entity) player).Center.X + (float) Main.rand.Next(-50, 50), ((Entity) player).Center.Y + (float) Main.rand.Next(-50, 50));
            Vector2 vector2_2 = Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, vector2_1));
            int shoot = obj.shoot;
            float shootSpeed = obj.shootSpeed;
            int num4 = obj.damage / 2;
            float knockBack = obj.knockBack;
            if (obj.useAmmo > 0)
            {
              int num5;
              player.PickAmmo(obj, ref shoot, ref shootSpeed, ref num4, ref knockBack, ref num5, ItemID.Sets.gunProj[obj.type]);
            }
            if (obj.mana > 0 && player.CheckMana(obj.mana / 2, true, false))
              player.manaRegenDelay = 300f;
            if (obj.consumable)
              --obj.stack;
            fargoSoulsPlayer.ApprenticeItemCDs[index1] = obj.useAnimation * 4;
            if (shoot == 250 || shoot == 251)
            {
              foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => (p.TypeAlive(250) || p.TypeAlive(251)) && p.owner == ((Entity) player).whoAmI)))
                projectile.Kill();
            }
            int index2 = Projectile.NewProjectile(player.GetSource_ItemUse(heldItem, (string) null), vector2_1, Vector2.op_Multiply(Vector2.Normalize(vector2_2), shootSpeed), shoot, num4, knockBack, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
            Main.projectile[index2].noDropItem = true;
          }
        }
      }
    }
  }
}
