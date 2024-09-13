// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Systems.RecipeSystem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Misc;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.Systems
{
  public class RecipeSystem : ModSystem
  {
    public static readonly Recipe.ConsumeItemCallback IronBonusBars = (Recipe.ConsumeItemCallback) ((Recipe recipe, int type, ref int amount) =>
    {
      Player localPlayer = Main.LocalPlayer;
      FargoSoulsPlayer fargoSoulsPlayer = localPlayer.FargoSouls();
      if (!localPlayer.HasEffect<IronEffect>() || WorldSavingSystem.IronUsedList.Contains(type))
        return;
      int num1 = 3;
      if (fargoSoulsPlayer.ForceEffect<IronEnchant>())
        num1 = 2;
      WorldSavingSystem.IronUsedList.Add(type);
      int num2 = 0;
      for (int index = 0; index < amount; ++index)
      {
        if (!Utils.NextBool(Main.rand, num1))
          ++num2;
      }
      amount = num2;
    });

    public static string AnyItem(int id)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
      interpolatedStringHandler.AppendFormatted<LocalizedText>(Lang.misc[37]);
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted<LocalizedText>(Lang.GetItemName(id));
      return interpolatedStringHandler.ToStringAndClear();
    }

    public static string AnyItem(string fargoSoulsLocalizationKey)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
      interpolatedStringHandler.AppendFormatted<LocalizedText>(Lang.misc[37]);
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted(Language.GetTextValue("Mods.FargowiltasSouls.RecipeGroups." + fargoSoulsLocalizationKey));
      return interpolatedStringHandler.ToStringAndClear();
    }

    public static string ItemXOrY(int id1, int id2)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
      interpolatedStringHandler.AppendFormatted<LocalizedText>(Lang.GetItemName(id1));
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted(Language.GetTextValue("Mods.FargowiltasSouls.RecipeGroups.Or"));
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted<LocalizedText>(Lang.GetItemName(id2));
      return interpolatedStringHandler.ToStringAndClear();
    }

    public virtual void AddRecipeGroups()
    {
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyDrax", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(579)), new int[2]
      {
        579,
        990
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBonesBanner", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem("BonesBanner")), new int[4]
      {
        3451,
        2900,
        2930,
        2970
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltRepeater", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(435)), new int[2]
      {
        435,
        1187
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilRepeater", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(436)), new int[2]
      {
        436,
        1194
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamantiteRepeater", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(481)), new int[2]
      {
        481,
        1201
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilWood", new RecipeGroup((Func<string>) (() => RecipeSystem.ItemXOrY(619, 911)), new int[2]
      {
        619,
        911
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamantite", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(391)), new int[2]
      {
        391,
        1198
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyShroomHead", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(1548)), new int[3]
      {
        1548,
        1547,
        1546
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyOriHead", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(1211)), new int[3]
      {
        1211,
        1210,
        1212
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyPallaHead", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(1206)), new int[3]
      {
        1206,
        1205,
        1207
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltHead", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(372)), new int[3]
      {
        372,
        371,
        373
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilHead", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(377)), new int[3]
      {
        377,
        378,
        376
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyTitaHead", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(1216)), new int[3]
      {
        1216,
        1215,
        1217
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyHallowHead", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(553)), new int[4]
      {
        553,
        559,
        558,
        4873
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAncientHallowHead", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(4897)), new int[4]
      {
        4897,
        4898,
        4899,
        4896
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamHead", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(401)), new int[3]
      {
        401,
        402,
        400
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyChloroHead", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(1002)), new int[3]
      {
        1002,
        1001,
        1003
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnySpectreHead", new RecipeGroup((Func<string>) (() => RecipeSystem.ItemXOrY(1503, 2189)), new int[2]
      {
        1503,
        2189
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBeetle", new RecipeGroup((Func<string>) (() => RecipeSystem.ItemXOrY(2201, 2200)), new int[2]
      {
        2201,
        2200
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnySquirrel", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(2018)), new int[12]
      {
        2018,
        3563,
        3564,
        4837,
        4831,
        4836,
        4834,
        4835,
        4833,
        4832,
        ModContent.ItemType<TopHatSquirrelCaught>(),
        ModContent.Find<ModItem>("Fargowiltas", "Squirrel").Type
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBird", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(2015)), new int[9]
      {
        2015,
        2016,
        2017,
        2889,
        2123,
        2122,
        4374,
        2205,
        4359
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyRottenChunk", new RecipeGroup((Func<string>) (() => RecipeSystem.ItemXOrY(68, 1330)), new int[2]
      {
        68,
        1330
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyGoldOre", new RecipeGroup((Func<string>) (() => RecipeSystem.ItemXOrY(13, 702)), new int[2]
      {
        13,
        702
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyGoldBar", new RecipeGroup((Func<string>) (() => RecipeSystem.ItemXOrY(19, 706)), new int[2]
      {
        19,
        706
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyDemoniteBar", new RecipeGroup((Func<string>) (() => RecipeSystem.ItemXOrY(57, 1257)), new int[2]
      {
        57,
        1257
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilBar", new RecipeGroup((Func<string>) (() => RecipeSystem.ItemXOrY(382, 1191)), new int[2]
      {
        382,
        1191
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:VilethornOrCrimsonRod", new RecipeGroup((Func<string>) (() => RecipeSystem.ItemXOrY(64, 1256)), new int[2]
      {
        64,
        1256
      }));
      RecipeGroup.RegisterGroup("FargowiltasSouls:AnyShellphone", new RecipeGroup((Func<string>) (() => RecipeSystem.AnyItem(5358)), new int[5]
      {
        5358,
        5437,
        5361,
        5360,
        5359
      }));
    }

    public virtual void PostAddRecipes()
    {
      foreach (Recipe recipe in ((IEnumerable<Recipe>) Main.recipe).Where<Recipe>((Func<Recipe, bool>) (recipe => !recipe.requiredTile.Contains(ModContent.TileType<CrucibleCosmosSheet>()))))
        recipe.AddConsumeItemCallback(RecipeSystem.IronBonusBars);
      foreach (Recipe recipe in ((IEnumerable<Recipe>) Main.recipe).Where<Recipe>((Func<Recipe, bool>) (recipe =>
      {
        if (recipe.createItem.ModItem == null)
          return false;
        return recipe.createItem.ModItem is BaseEnchant || recipe.createItem.ModItem is BaseForce || recipe.createItem.ModItem is BaseSoul;
      })))
        recipe.DisableDecraft();
    }
  }
}
