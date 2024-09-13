// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.SupremeDeathbringerFairy
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  [AutoloadEquip]
  public class SupremeDeathbringerFairy : SoulsItem
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
      this.Item.rare = 4;
      this.Item.value = Item.sellPrice(0, 4, 0, 0);
      this.Item.defense = 2;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.SupremeDeathbringerFairy = true;
      player.buffImmune[137] = true;
      player.AddEffect<SlimeFallEffect>(this.Item);
      if (player.AddEffect<SlimyShieldEffect>(this.Item))
        player.FargoSouls().SlimyShieldItem = this.Item;
      player.buffImmune[ModContent.BuffType<BerserkedBuff>()] = true;
      player.AddEffect<AgitatingLensEffect>(this.Item);
      player.AddEffect<AgitatingLensInstall>(this.Item);
      player.buffImmune[ModContent.BuffType<InfestedBuff>()] = true;
      player.npcTypeNoAggro[210] = true;
      player.npcTypeNoAggro[211] = true;
      player.npcTypeNoAggro[42] = true;
      player.npcTypeNoAggro[231] = true;
      player.npcTypeNoAggro[232] = true;
      player.npcTypeNoAggro[233] = true;
      player.npcTypeNoAggro[234] = true;
      player.npcTypeNoAggro[235] = true;
      fargoSoulsPlayer.QueenStingerItem = this.Item;
      if (player.honey)
        player.GetArmorPenetration(DamageClass.Generic) += 5f;
      player.buffImmune[ModContent.BuffType<LethargicBuff>()] = true;
      fargoSoulsPlayer.NecromanticBrewItem = this.Item;
      player.AddEffect<NecroBrewSpin>(this.Item);
      player.AddEffect<SkeleMinionEffect>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<SlimyShield>(), 1).AddIngredient(ModContent.ItemType<AgitatingLens>(), 1).AddIngredient(ModContent.ItemType<QueenStinger>(), 1).AddIngredient(ModContent.ItemType<NecromanticBrew>(), 1).AddIngredient(175, 10).AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 5).AddTile(26).Register();
    }
  }
}
