// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Challengers.EnchantedLifeblade
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Projectiles.ChallengerItems;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.Challengers
{
  public class EnchantedLifeblade : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(5, 5, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
      ItemID.Sets.BonusAttackSpeedMultiplier[this.Item.type] = 0.25f;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 80;
      ((Entity) this.Item).height = 80;
      this.Item.damage = 53;
      this.Item.knockBack = 3f;
      this.Item.useStyle = 1;
      this.Item.useAnimation = this.Item.useTime = 40;
      this.Item.DamageType = DamageClass.Melee;
      this.Item.autoReuse = true;
      this.Item.noUseGraphic = true;
      this.Item.noMelee = true;
      this.Item.rare = 5;
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
      this.Item.shoot = ModContent.ProjectileType<EnchantedLifebladeProjectile>();
      this.Item.shootSpeed = 30f;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient<LifelightBag>(2).AddTile(220).DisableDecraft().Register();
    }
  }
}
