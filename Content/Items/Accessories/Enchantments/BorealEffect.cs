// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.BorealEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
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
  public class BorealEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<TimberHeader>();

    public override int ToggleItemType => ModContent.ItemType<BorealWoodEnchant>();

    public override bool ExtraAttackEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.BorealCD <= 0)
        return;
      --fargoSoulsPlayer.BorealCD;
    }

    public override void TryAdditionalAttacks(Player player, int damage, DamageClass damageType)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.BorealCD > 0 || ((Entity) player).whoAmI != Main.myPlayer)
        return;
      Item obj = this.EffectItem(player);
      bool flag = fargoSoulsPlayer.ForceEffect(new int?(obj.type));
      fargoSoulsPlayer.BorealCD = flag ? 30 : 60;
      Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center)), 17f);
      int num = damage / 2;
      if (!fargoSoulsPlayer.TerrariaSoul)
        num = Math.Min(num, FargoSoulsUtil.HighestDamageTypeScaling(player, flag ? 300 : 30));
      int index = Projectile.NewProjectile(player.GetSource_Accessory(obj, (string) null), ((Entity) player).Center, vector2, 166, num, 1f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      int number = flag ? 7 : 3;
      if (index == Main.maxProjectiles)
        return;
      FargoSoulsGlobalProjectile.SplitProj(Main.projectile[index], number, 0.314159274f, 1f);
    }
  }
}
