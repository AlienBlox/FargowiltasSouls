// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.VortexEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class VortexEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(0, 242, 170);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 11;
      this.Item.value = 400000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      VortexEnchant.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      player.AddEffect<VortexStealthEffect>(item);
      player.AddEffect<VortexVortexEffect>(item);
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.mount.Active)
        fargoSoulsPlayer.VortexStealth = false;
      if (!fargoSoulsPlayer.VortexStealth)
        return;
      player.moveSpeed *= 0.3f;
      player.aggro -= 1200;
      player.setVortex = true;
      player.stealth = 0.0f;
    }

    public static void ActivateVortex(Player player)
    {
      if (player != Main.LocalPlayer)
        return;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      bool flag1 = player.HasEffect<VortexStealthEffect>();
      bool flag2 = player.HasEffect<VortexVortexEffect>();
      if (!(flag1 | flag2))
        return;
      fargoSoulsPlayer.VortexStealth = !fargoSoulsPlayer.VortexStealth;
      if (!flag1)
        fargoSoulsPlayer.VortexStealth = false;
      if (Main.netMode == 1)
        NetMessage.SendData(4, -1, -1, (NetworkText) null, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      if (!(fargoSoulsPlayer.VortexStealth & flag2) || player.HasBuff(ModContent.BuffType<VortexCDBuff>()))
        return;
      int index = Projectile.NewProjectile(player.GetSource_EffectItem<VortexVortexEffect>(), ((Entity) player).Center.X, ((Entity) player).Center.Y, 0.0f, 0.0f, ModContent.ProjectileType<Void>(), FargoSoulsUtil.HighestDamageTypeScaling(player, 60), 5f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      Main.projectile[index].FargoSouls().CanSplit = false;
      Main.projectile[index].netUpdate = true;
      player.AddBuff(ModContent.BuffType<VortexCDBuff>(), 3600, true, false);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(2757, 1).AddIngredient(2758, 1).AddIngredient(2759, 1).AddIngredient(3475, 1).AddIngredient(3540, 1).AddIngredient(1553, 1).AddTile(412).Register();
    }
  }
}
