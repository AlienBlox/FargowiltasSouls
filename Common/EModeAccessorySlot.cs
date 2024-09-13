// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Common.EModeAccessorySlot
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Patreon.ParadoxWolf;
using FargowiltasSouls.Core.Systems;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Common
{
  public abstract class EModeAccessorySlot : ModAccessorySlot
  {
    private int[] AllowedItemExceptions = new int[8]
    {
      ModContent.ItemType<ParadoxWolfSoul>(),
      1576,
      520,
      521,
      575,
      547,
      549,
      548
    };

    public abstract int Loadout { get; }

    public virtual bool CanAcceptItem(Item checkItem, AccessorySlotType context)
    {
      if (context != 10 && context != 11 || !base.CanAcceptItem(checkItem, context) && !((IEnumerable<int>) this.AllowedItemExceptions).Contains<int>(checkItem.type))
        return base.CanAcceptItem(checkItem, context);
      return checkItem.ModItem != null && (checkItem.ModItem is BaseEnchant || checkItem.ModItem is BaseForce || checkItem.ModItem is BaseSoul) || ((IEnumerable<int>) this.AllowedItemExceptions).Contains<int>(checkItem.type);
    }

    public virtual bool IsVisibleWhenNotEnabled() => false;

    public virtual bool IsEnabled()
    {
      return WorldSavingSystem.EternityMode && ModAccessorySlot.Player.FargoSouls().MutantsPactSlot && ModAccessorySlot.Player.CurrentLoadoutIndex == this.Loadout;
    }

    public virtual string FunctionalTexture => "FargowiltasSouls/Assets/UI/EnchantSlotIcon";

    public virtual void OnMouseHover(AccessorySlotType context)
    {
      if (context != 10)
      {
        if (context != 11)
          return;
        Main.hoverItemName = Language.GetTextValue("Mods.FargowiltasSouls.Common.AccessorySlot.EModeSlotVanity");
      }
      else
        Main.hoverItemName = Language.GetTextValue("Mods.FargowiltasSouls.Common.AccessorySlot.EModeSlotFunctional");
    }
  }
}
