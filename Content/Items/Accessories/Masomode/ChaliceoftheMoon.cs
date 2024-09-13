// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.ChaliceoftheMoon
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
  public class ChaliceoftheMoon : SoulsItem
  {
    public override bool Eternity => true;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 32;
      ((Entity) this.Item).height = 54;
      this.Item.accessory = true;
      this.Item.rare = 10;
      this.Item.value = Item.sellPrice(0, 7, 0, 0);
      this.Item.defense = 8;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      MagicalBulb.AddEffects(player, this.Item);
      player.buffImmune[67] = true;
      player.buffImmune[ModContent.BuffType<FusedBuff>()] = true;
      player.buffImmune[ModContent.BuffType<LihzahrdCurseBuff>()] = true;
      player.buffImmune[ModContent.BuffType<LowGroundBuff>()] = true;
      fargoSoulsPlayer.LihzahrdTreasureBoxItem = this.Item;
      player.AddEffect<LihzahrdGroundPound>(this.Item);
      player.AddEffect<LihzahrdBoulders>(this.Item);
      player.buffImmune[ModContent.BuffType<MarkedforDeathBuff>()] = true;
      player.AddEffect<CelestialRuneAttacks>(this.Item);
      player.buffImmune[ModContent.BuffType<AtrophiedBuff>()] = true;
      player.buffImmune[ModContent.BuffType<JammedBuff>()] = true;
      player.buffImmune[ModContent.BuffType<ReverseManaFlowBuff>()] = true;
      player.buffImmune[ModContent.BuffType<AntisocialBuff>()] = true;
      fargoSoulsPlayer.MoonChalice = true;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<MagicalBulb>(), 1).AddIngredient(ModContent.ItemType<LihzahrdTreasureBox>(), 1).AddIngredient(ModContent.ItemType<CelestialRune>(), 1).AddIngredient(3458, 1).AddIngredient(3456, 1).AddIngredient(3457, 1).AddIngredient(3459, 1).AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 10).AddTile(412).Register();
    }
  }
}
