// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Summons.SigilOfChampions
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Cosmos;
using FargowiltasSouls.Content.Bosses.Champions.Earth;
using FargowiltasSouls.Content.Bosses.Champions.Life;
using FargowiltasSouls.Content.Bosses.Champions.Nature;
using FargowiltasSouls.Content.Bosses.Champions.Shadow;
using FargowiltasSouls.Content.Bosses.Champions.Spirit;
using FargowiltasSouls.Content.Bosses.Champions.Terra;
using FargowiltasSouls.Content.Bosses.Champions.Timber;
using FargowiltasSouls.Content.Bosses.Champions.Will;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Summons
{
  public class SigilOfChampions : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      ItemID.Sets.SortingPriorityBossSpawns[this.Type] = 12;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 40;
      ((Entity) this.Item).height = 40;
      this.Item.rare = 11;
      this.Item.maxStack = 1;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
      this.Item.consumable = false;
      this.Item.value = Item.buyPrice(1, 0, 0, 0);
    }

    public virtual bool CanUseItem(Player player)
    {
      List<int> intList = new List<int>((IEnumerable<int>) new \u003C\u003Ez__ReadOnlyArray<int>(new int[9]
      {
        ModContent.NPCType<CosmosChampion>(),
        ModContent.NPCType<EarthChampion>(),
        ModContent.NPCType<LifeChampion>(),
        ModContent.NPCType<NatureChampion>(),
        ModContent.NPCType<ShadowChampion>(),
        ModContent.NPCType<SpiritChampion>(),
        ModContent.NPCType<TerraChampion>(),
        ModContent.NPCType<TimberChampion>(),
        ModContent.NPCType<WillChampion>()
      }));
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        if (((Entity) Main.npc[index]).active && index == EModeGlobalNPC.championBoss && intList.Contains(Main.npc[index].type))
          return false;
      }
      return true;
    }

    public virtual bool AltFunctionUse(Player player) => true;

    private void PrintChampMessage(string key)
    {
      Main.NewText((object) Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Items.SigilOfChampions.Message." + key), new Color?(new Color(175, 75, (int) byte.MaxValue)));
    }

    public virtual bool? UseItem(Player player)
    {
      if (player.ZoneUndergroundDesert)
      {
        if (player.altFunctionUse == 2)
          this.PrintChampMessage("Spirit");
        else
          FargoSoulsUtil.SpawnBossNetcoded(player, ModContent.NPCType<SpiritChampion>());
      }
      else if (player.ZoneUnderworldHeight)
      {
        if (player.altFunctionUse == 2)
          this.PrintChampMessage("Earth");
        else
          FargoSoulsUtil.SpawnBossNetcoded(player, ModContent.NPCType<EarthChampion>());
      }
      else if ((double) ((Entity) player).Center.Y >= Main.worldSurface * 16.0)
      {
        if (player.ZoneSnow)
        {
          if (player.altFunctionUse == 2)
            this.PrintChampMessage("Nature");
          else
            FargoSoulsUtil.SpawnBossNetcoded(player, ModContent.NPCType<NatureChampion>());
        }
        else if (player.altFunctionUse == 2)
          this.PrintChampMessage("Terra");
        else
          FargoSoulsUtil.SpawnBossNetcoded(player, ModContent.NPCType<TerraChampion>());
      }
      else if (player.ZoneSkyHeight)
      {
        if (player.altFunctionUse == 2)
          this.PrintChampMessage("Cosmos");
        else
          FargoSoulsUtil.SpawnBossNetcoded(player, ModContent.NPCType<CosmosChampion>());
      }
      else if (player.ZoneBeach)
      {
        if (player.altFunctionUse == 2)
          this.PrintChampMessage("Will");
        else
          FargoSoulsUtil.SpawnBossNetcoded(player, ModContent.NPCType<WillChampion>());
      }
      else if (player.ZoneHallow && Main.dayTime)
      {
        if (player.altFunctionUse == 2)
          this.PrintChampMessage("Life");
        else
          FargoSoulsUtil.SpawnBossNetcoded(player, ModContent.NPCType<LifeChampion>());
      }
      else if ((player.ZoneCorrupt || player.ZoneCrimson) && !Main.dayTime)
      {
        if (player.altFunctionUse == 2)
          this.PrintChampMessage("Shadow");
        else
          FargoSoulsUtil.SpawnBossNetcoded(player, ModContent.NPCType<ShadowChampion>());
      }
      else if (!player.ZoneHallow && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneDesert && !player.ZoneSnow && !player.ZoneJungle && Main.dayTime)
      {
        if (player.altFunctionUse == 2)
          this.PrintChampMessage("Timber");
        else
          FargoSoulsUtil.SpawnBossNetcoded(player, ModContent.NPCType<TimberChampion>());
      }
      else if (player.altFunctionUse == 2)
        this.PrintChampMessage("Nothing");
      return new bool?(true);
    }

    public override void SafeModifyTooltips(List<TooltipLine> list)
    {
      foreach (TooltipLine tooltipLine in list)
      {
        if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
          tooltipLine.OverrideColor = new Color?(Main.DiscoColor);
      }
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(27, 5).AddRecipeGroup("IronBar", 5).AddIngredient(175, 5).AddIngredient(2161, 5).AddIngredient(520, 5).AddIngredient(521, 5).AddIngredient(3783, 5).AddIngredient(275, 5).AddIngredient(3467, 5).AddTile(412).Register();
    }
  }
}
