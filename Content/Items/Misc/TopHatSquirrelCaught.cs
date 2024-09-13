// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Misc.TopHatSquirrelCaught
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.NPCs.Critters;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Misc
{
  public class TopHatSquirrelCaught : SoulsItem
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Items/Weapons/Misc/TophatSquirrelWeapon";
    }

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 5;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 10;
      this.Item.rare = 1;
      this.Item.useStyle = 1;
      this.Item.useAnimation = 15;
      this.Item.useTime = 10;
      this.Item.consumable = true;
      this.Item.noMelee = true;
      this.Item.noUseGraphic = true;
      this.Item.UseSound = new SoundStyle?(SoundID.Item44);
      this.Item.makeNPC = (int) (short) ModContent.NPCType<TophatSquirrelCritter>();
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddRecipeGroup("FargowiltasSouls:AnySquirrel", 1).AddIngredient(239, 1).Register();
    }
  }
}
