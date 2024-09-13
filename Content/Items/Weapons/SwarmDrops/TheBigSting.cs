// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.TheBigSting
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.Items.Summons.SwarmSummons.Energizers;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Weapons.BossDrops;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.SwarmDrops
{
  public class TheBigSting : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 22;
      ((Entity) this.Item).height = 22;
      this.Item.damage = 1064;
      this.Item.useTime = 44;
      this.Item.useAnimation = 44;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 2.2f;
      this.Item.value = 500000;
      this.Item.rare = 11;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<BigStinger>();
      this.Item.useAmmo = AmmoID.Dart;
      this.Item.UseSound = new SoundStyle?(SoundID.Item97);
      this.Item.shootSpeed = 15f;
    }

    public virtual void ModifyShootStats(
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      type = this.Item.shoot;
      float num = 1f;
      if (player.strongBees)
        num += 0.1f;
      damage = (int) ((double) damage * (double) num);
      knockback = (float) (int) ((double) knockback * (double) num);
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(-30f, 0.0f));

    public virtual bool CanConsumeAmmo(Item ammo, Player player) => Utils.NextBool(Main.rand, 3);

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<TheSmallSting>(), 1).AddIngredient(ModContent.ItemType<EnergizerBee>(), 1).AddIngredient(3467, 10).AddTile(ModContent.TileType<CrucibleCosmosSheet>()).Register();
    }
  }
}
