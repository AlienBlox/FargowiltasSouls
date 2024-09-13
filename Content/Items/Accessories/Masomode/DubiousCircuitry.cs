// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.DubiousCircuitry
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class DubiousCircuitry : SoulsItem
  {
    public override bool Eternity => true;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = 6;
      this.Item.value = Item.sellPrice(0, 5, 0, 0);
      this.Item.defense = 10;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.buffImmune[39] = true;
      player.buffImmune[69] = true;
      player.buffImmune[ModContent.BuffType<DefenselessBuff>()] = true;
      player.buffImmune[ModContent.BuffType<NanoInjectionBuff>()] = true;
      player.buffImmune[ModContent.BuffType<LightningRodBuff>()] = true;
      player.FargoSouls().FusedLens = true;
      player.AddEffect<FusedLensInstall>(this.Item);
      if (player.onFire2)
        player.FargoSouls().AttackSpeed += 0.15f;
      if (player.ichor)
        player.GetCritChance(DamageClass.Generic) += 15f;
      player.AddEffect<ProbeMinionEffect>(this.Item);
      player.AddEffect<GroundStickDR>(this.Item);
      player.endurance += 0.05f;
      player.noKnockback = true;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<FusedLens>(), 1).AddIngredient(ModContent.ItemType<GroundStick>(), 1).AddIngredient(ModContent.ItemType<ReinforcedPlating>(), 1).AddIngredient(1225, 10).AddIngredient(547, 5).AddIngredient(548, 5).AddIngredient(549, 5).AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 10).AddTile(134).Register();
    }
  }
}
