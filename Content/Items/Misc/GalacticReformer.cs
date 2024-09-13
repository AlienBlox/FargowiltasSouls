// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Misc.GalacticReformer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Misc
{
  public class GalacticReformer : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 99;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 10;
      ((Entity) this.Item).height = 32;
      this.Item.maxStack = 99;
      this.Item.consumable = true;
      this.Item.useStyle = 1;
      this.Item.rare = 4;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.useAnimation = 20;
      this.Item.useTime = 20;
      this.Item.value = Item.buyPrice(0, 0, 3, 0);
      this.Item.noUseGraphic = true;
      this.Item.noMelee = true;
      this.Item.shoot = ModContent.ProjectileType<GalacticReformerProj>();
      this.Item.shootSpeed = 5f;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(167, 500).AddTile(77).Register();
    }
  }
}
