// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.ShroomiteShroomEffect
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
  public class ShroomiteShroomEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<NatureHeader>();

    public override int ToggleItemType => ModContent.ItemType<ShroomiteEnchant>();

    public override bool ExtraAttackEffect => true;

    public override void MeleeEffects(Player player, Item item, Rectangle hitbox)
    {
      Player player1 = player;
      if (item.noMelee || player1.itemAnimation != (int) ((double) player1.itemAnimationMax * 0.1) && player1.itemAnimation != (int) ((double) player1.itemAnimationMax * 0.3) && player1.itemAnimation != (int) ((double) player1.itemAnimationMax * 0.5) && player1.itemAnimation != (int) ((double) player1.itemAnimationMax * 0.7) && player1.itemAnimation != (int) ((double) player1.itemAnimationMax * 0.9))
        return;
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      float num4 = 0.0f;
      if (player1.itemAnimation == (int) ((double) player1.itemAnimationMax * 0.9))
        num1 = -7f;
      if (player1.itemAnimation == (int) ((double) player1.itemAnimationMax * 0.7))
      {
        num1 = -6f;
        num2 = 2f;
      }
      if (player1.itemAnimation == (int) ((double) player1.itemAnimationMax * 0.5))
      {
        num1 = -4f;
        num2 = 4f;
      }
      if (player1.itemAnimation == (int) ((double) player1.itemAnimationMax * 0.3))
      {
        num1 = -2f;
        num2 = 6f;
      }
      if (player1.itemAnimation == (int) ((double) player1.itemAnimationMax * 0.1))
        num2 = 7f;
      if (player1.itemAnimation == (int) ((double) player1.itemAnimationMax * 0.7))
        num4 = 26f;
      if (player1.itemAnimation == (int) ((double) player1.itemAnimationMax * 0.3))
      {
        num4 -= 4f;
        num3 -= 20f;
      }
      if (player1.itemAnimation == (int) ((double) player1.itemAnimationMax * 0.1))
        num3 += 6f;
      if (((Entity) player1).direction == -1)
      {
        if (player1.itemAnimation == (int) ((double) player1.itemAnimationMax * 0.9))
          num4 -= 8f;
        if (player1.itemAnimation == (int) ((double) player1.itemAnimationMax * 0.7))
          num4 -= 6f;
      }
      float num5 = num1 * 1.5f;
      float num6 = num2 * 1.5f;
      float num7 = num4 * (float) ((Entity) player1).direction;
      float num8 = num3 * player1.gravDir;
      Projectile.NewProjectile(player1.GetSource_ItemUse(item, (string) null), (float) (hitbox.X + hitbox.Width / 2) + num7, (float) (hitbox.Y + hitbox.Height / 2) + num8, (float) ((Entity) player1).direction * num6, num5 * player1.gravDir, ModContent.ProjectileType<ShroomiteShroom>(), item.damage / 4, 0.0f, ((Entity) player1).whoAmI, 0.0f, 0.0f, 0.0f);
    }
  }
}
