// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Armor.MutantMask
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Buffs.Minions;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Armor
{
  [AutoloadEquip]
  public class MutantMask : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 18;
      ((Entity) this.Item).height = 18;
      this.Item.rare = 11;
      this.Item.value = Item.sellPrice(0, 50, 0, 0);
      this.Item.defense = 50;
    }

    public virtual void UpdateEquip(Player player)
    {
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 0.5f);
      player.GetCritChance(DamageClass.Generic) += 20f;
      player.maxMinions += 10;
      player.maxTurrets += 10;
      player.manaCost -= 0.25f;
      player.ammoCost75 = true;
    }

    public virtual bool IsArmorSet(Item head, Item body, Item legs)
    {
      return body.type == ModContent.ItemType<MutantBody>() && legs.type == ModContent.ItemType<MutantPants>();
    }

    public virtual void ArmorSetShadows(Player player) => player.armorEffectDrawShadow = true;

    public virtual void UpdateArmorSet(Player player)
    {
      player.setBonus = MutantMask.GetSetBonusString();
      MutantMask.MutantSetBonus(player, this.Item);
    }

    public static string GetSetBonusString()
    {
      return Language.GetTextValue("Mods.FargowiltasSouls.SetBonus.Mutant");
    }

    public static void MutantSetBonus(Player player, Item item)
    {
      player.AddEffect<MasoAbom>(item);
      player.AddEffect<MasoRing>(item);
      player.AddBuff(ModContent.BuffType<MutantPowerBuff>(), 2, true, false);
      player.FargoSouls().MutantSetBonusItem = item;
      player.FargoSouls().GodEaterImbue = true;
      player.FargoSouls().AttackSpeed += 0.2f;
    }

    public override void SafeModifyTooltips(List<TooltipLine> list)
    {
      foreach (TooltipLine tooltipLine in list)
      {
        if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
          tooltipLine.OverrideColor = new Color?(new Color(Main.DiscoR, 51, (int) byte.MaxValue - (int) ((double) Main.DiscoR * 0.4)));
      }
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient<Fargowiltas.Items.Vanity.MutantMask>(1).AddIngredient<AbomEnergy>(10).AddIngredient<EternalEnergy>(10).AddTile<CrucibleCosmosSheet>().Register();
    }
  }
}
