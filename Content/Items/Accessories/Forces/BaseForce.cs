// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Forces.BaseForce
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Forces
{
  public abstract class BaseForce : SoulsItem
  {
    public static Dictionary<int, int[]> Enchants = new Dictionary<int, int[]>();

    public static int[] EnchantsIn<T>() where T : BaseForce
    {
      return BaseForce.Enchants[ModContent.ItemType<T>()];
    }

    public void SetActive(Player player) => player.FargoSouls().ForceEffects.Add(this.Type);

    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      ItemID.Sets.ItemNoGravity[this.Type] = true;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      base.SetDefaults();
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = 11;
      this.Item.value = 600000;
    }
  }
}
