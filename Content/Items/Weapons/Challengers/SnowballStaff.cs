// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Challengers.SnowballStaff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Projectiles.ChallengerItems;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.Challengers
{
  public class SnowballStaff : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 14;
      this.Item.DamageType = DamageClass.Magic;
      ((Entity) this.Item).width = 40;
      ((Entity) this.Item).height = 40;
      this.Item.useTime = 60;
      this.Item.useAnimation = 60;
      this.Item.useStyle = 1;
      this.Item.knockBack = 1f;
      this.Item.value = Item.sellPrice(0, 0, 50, 0);
      this.Item.rare = 1;
      this.Item.UseSound = new SoundStyle?(SoundID.Item8);
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<RollingSnowball>();
      this.Item.shootSpeed = 6f;
      this.Item.noMelee = true;
      this.Item.channel = true;
      this.Item.mana = 5;
    }

    public virtual bool CanShoot(Player player)
    {
      return player.ownedProjectileCounts[this.Item.shoot] < 1;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient<TrojanSquirrelBag>(2).AddTile(220).DisableDecraft().Register();
    }
  }
}
