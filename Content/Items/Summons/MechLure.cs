// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Summons.MechLure
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Summons
{
  public class MechLure : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 5;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 32;
      ((Entity) this.Item).height = 32;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 13;
      this.Item.rare = 3;
      this.Item.consumable = true;
      this.Item.maxStack = 20;
      this.Item.noUseGraphic = true;
      this.Item.shoot = ModContent.ProjectileType<MechLureProjectile>();
      this.Item.shootSpeed = 10f;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.value = Item.sellPrice(0, 2, 0, 0);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddRecipeGroup("FargowiltasSouls:AnyMythrilBar", 4).AddIngredient(3191, 3).AddIngredient(575, 5).AddIngredient(2303, 1).AddTile(134).Register();
    }

    public virtual bool CanUseItem(Player Player)
    {
      return Player.ZoneBeach && ((Entity) Player).wet && !NPC.AnyNPCs(ModContent.NPCType<FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron>()) && Player.ownedProjectileCounts[this.Item.shoot] <= 0;
    }

    public virtual bool? UseItem(Player player) => new bool?(true);

    public override void SafeModifyTooltips(List<TooltipLine> tooltips)
    {
      string textValue = Language.GetTextValue("Mods.FargowiltasSouls.Items.MechLure.TooltipTravelingMerchant");
      tooltips.Add(new TooltipLine(((ModType) this).Mod, "TooltipTravelingMerchant", "[i:Fargowiltas/TravellingMerchant] [c/AAAAAA:" + textValue + "]"));
    }
  }
}
