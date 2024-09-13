// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Challengers.MountedAcornGun
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
  public class MountedAcornGun : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 16;
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 54;
      ((Entity) this.Item).height = 26;
      this.Item.useTime = 64;
      this.Item.useAnimation = 64;
      this.Item.useStyle = 5;
      this.Item.knockBack = 0.5f;
      this.Item.value = Item.sellPrice(0, 0, 50, 0);
      this.Item.rare = 1;
      this.Item.UseSound = new SoundStyle?(SoundID.Item61);
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<SproutingAcorn>();
      this.Item.shootSpeed = 16f;
      this.Item.useAmmo = 27;
      this.Item.noMelee = true;
    }

    public virtual bool CanConsumeAmmo(Item ammo, Player player) => Utils.NextBool(Main.rand);

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient<TrojanSquirrelBag>(2).AddTile(220).DisableDecraft().Register();
    }
  }
}
