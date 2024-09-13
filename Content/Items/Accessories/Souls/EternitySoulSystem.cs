// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.EternitySoulSystem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public class EternitySoulSystem : ModSystem
  {
    public static List<string> Tooltips = new List<string>();
    public static List<string> TooltipLines = new List<string>();

    public virtual void OnLocalizationsLoaded()
    {
      EternitySoulSystem.Tooltips.Clear();
      base.PostAddRecipes();
    }

    public virtual void PostAddRecipes()
    {
      string[] startsWithFilter = Language.GetTextValue("Mods.FargowiltasSouls.Items.EternitySoul.Extra.StartsWithFilter").Split("|", StringSplitOptions.RemoveEmptyEntries);
      string[] containsFilter = Language.GetTextValue("Mods.FargowiltasSouls.Items.EternitySoul.Extra.ContainsFilter").Split("|", StringSplitOptions.RemoveEmptyEntries);
      foreach (Recipe recipe in ((IEnumerable<Recipe>) Main.recipe).Where<Recipe>((Func<Recipe, bool>) (r => r.createItem != null && r.createItem.ModItem is BaseSoul)))
      {
        foreach (Item obj in recipe.requiredItem)
        {
          ModItem modItem = obj.ModItem;
          if (modItem != null)
          {
            IEnumerable<string> collection = ((IEnumerable<string>) modItem.Tooltip.Value.Split('\n', (StringSplitOptions) 3)).Where<string>((Func<string, bool>) (line => !((IEnumerable<string>) startsWithFilter).Any<string>(new Func<string, bool>(line.StartsWith)) && !((IEnumerable<string>) containsFilter).Any<string>(new Func<string, bool>(line.Contains))));
            EternitySoulSystem.Tooltips.AddRange(collection);
          }
        }
      }
    }
  }
}
