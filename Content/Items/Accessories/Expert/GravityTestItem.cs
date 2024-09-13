// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Expert.GravityTestItem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Expert
{
  public class GravityTestItem : SoulsItem
  {
    public virtual bool IsLoadingEnabled(Mod mod) => true;

    public virtual string Texture => "FargowiltasSouls/Content/Items/Placeholder";

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = -12;
      this.Item.value = Item.sellPrice(0, 1, 0, 0);
      this.Item.expert = true;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.AddEffect<VortexProjGravity>(this.Item);
    }
  }
}
