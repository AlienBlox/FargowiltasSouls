// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.RefractorBlaster2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.SwarmDrops
{
  public class RefractorBlaster2 : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(3, 7, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
    }

    public override int NumFrames => 7;

    public virtual void SetDefaults()
    {
      this.Item.CloneDefaults(514);
      ((Entity) this.Item).width = 98;
      ((Entity) this.Item).height = 38;
      this.Item.damage = 577;
      this.Item.channel = true;
      this.Item.useTime = 24;
      this.Item.useAnimation = 24;
      this.Item.reuseDelay = 20;
      this.Item.shootSpeed = 15f;
      this.Item.UseSound = new SoundStyle?(SoundID.Item15);
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
      this.Item.rare = 11;
      this.Item.shoot = ModContent.ProjectileType<RefractorBlaster2Held>();
      this.Item.noUseGraphic = true;
      this.Item.mana = 18;
      this.Item.knockBack = 0.5f;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "RefractorBlaster", 1).AddIngredient((Mod) null, "AbomEnergy", 10).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerPrime"), 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
