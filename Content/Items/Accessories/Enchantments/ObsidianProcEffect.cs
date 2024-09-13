// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.ObsidianProcEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class ObsidianProcEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<TerraHeader>();

    public override int ToggleItemType => ModContent.ItemType<ObsidianEnchant>();

    public override bool ExtraAttackEffect => true;

    public override void OnHitNPCEither(
      Player player,
      NPC target,
      NPC.HitInfo hitInfo,
      DamageClass damageClass,
      int baseDamage,
      Projectile projectile,
      Item item)
    {
      if (player.FargoSouls().ObsidianCD != 0)
        return;
      int num1 = baseDamage;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      int num2 = Math.Min(num1, FargoSoulsUtil.HighestDamageTypeScaling(player, 300));
      if (((Entity) player).lavaWet || fargoSoulsPlayer.LavaWet)
        num2 = (int) ((double) num2 * 1.2999999523162842);
      Projectile.NewProjectile(this.GetSource_EffectItem(player), ((Entity) target).Center, Vector2.Zero, ModContent.ProjectileType<ObsidianExplosion>(), num2, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      if (fargoSoulsPlayer.ForceEffect<ObsidianEnchant>())
        fargoSoulsPlayer.ObsidianCD = 20;
      else
        fargoSoulsPlayer.ObsidianCD = 40;
    }
  }
}
