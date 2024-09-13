// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.EnchantSystem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Forces;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class EnchantSystem : ModSystem
  {
    public virtual void PostSetupRecipes()
    {
      SetFactory factory = ItemID.Sets.Factory;
      BaseEnchant.CraftsInto = factory.CreateIntSet(Array.Empty<int>());
      foreach (BaseEnchant baseEnchant in ModContent.GetContent<BaseEnchant>())
      {
        BaseEnchant modItem = baseEnchant;
        Recipe recipe = Enumerable.FirstOrDefault<Recipe>((IEnumerable<Recipe>) Main.recipe, (Func<Recipe, bool>) (r => r.ContainsIngredient(modItem.Type) && r.createItem.ModItem != null && r.createItem.ModItem is BaseEnchant), (Recipe) null);
        if (recipe != null)
          BaseEnchant.CraftsInto[modItem.Type] = recipe.createItem.type;
      }
      BaseEnchant.Force = factory.CreateIntSet(Array.Empty<int>());
      foreach (KeyValuePair<int, int[]> enchant in BaseForce.Enchants)
      {
        foreach (int index in enchant.Value)
          BaseEnchant.Force[index] = enchant.Key;
      }
    }
  }
}
