// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.AshWoodFireballs
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

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
  public class AshWoodFireballs : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<TerraHeader>();

    public override int ToggleItemType => ModContent.ItemType<AshWoodEnchant>();

    public override bool ExtraAttackEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.AshwoodCD <= 0)
        return;
      --fargoSoulsPlayer.AshwoodCD;
    }

    public override void TryAdditionalAttacks(Player player, int damage, DamageClass damageType)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      bool flag = player.onFire || player.onFire2 || player.onFire3;
      if (fargoSoulsPlayer.AshwoodCD > 0 || !flag && !player.HasEffect<ObsidianProcEffect>())
        return;
      fargoSoulsPlayer.AshwoodCD = fargoSoulsPlayer.ForceEffect<AshWoodEnchant>() ? 15 : (player.HasEffect<ObsidianProcEffect>() ? 20 : 30);
      int num = damage;
      Vector2 vector2 = Utils.RotatedByRandom(Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center)), 17f), Math.PI / 10.0);
      if (!fargoSoulsPlayer.TerrariaSoul)
        num = Math.Min(num, FargoSoulsUtil.HighestDamageTypeScaling(player, 60));
      if (flag)
        num = (int) ((double) num * 1.2999999523162842);
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      Projectile.NewProjectile(this.GetSource_EffectItem(player), ((Entity) player).Center, vector2, 15, num, 1f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }
  }
}
