// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Duck.ScientificRailgun
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Duck
{
  public class ScientificRailgun : PatreonModItem
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void SetDefaults()
    {
      this.Item.damage = 2160;
      this.Item.crit = 26;
      this.Item.DamageType = DamageClass.Ranged;
      this.Item.noMelee = true;
      ((Entity) this.Item).width = 64;
      ((Entity) this.Item).height = 26;
      this.Item.useTime = 120;
      this.Item.useAnimation = 120;
      this.Item.useStyle = 5;
      this.Item.knockBack = 20f;
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
      this.Item.rare = 11;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<RailgunBlast>();
      this.Item.shootSpeed = 1000f;
      this.Item.useAmmo = AmmoID.Coin;
    }

    public virtual void ModifyShootStats(
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      velocity = Utils.SafeNormalize(velocity, Vector2.Zero);
      type = this.Item.shoot;
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(-10f, 0.0f));

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(905, 1).AddIngredient(2882, 1).AddIngredient(3541, 1).AddIngredient(3467, 10).AddIngredient(2860, 100).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
