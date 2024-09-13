// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.DragonBreath2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.SwarmDrops
{
  public class DragonBreath2 : SoulsItem
  {
    public int skullTimer;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 210;
      this.Item.knockBack = 1f;
      this.Item.shootSpeed = 12f;
      this.Item.useStyle = 5;
      this.Item.autoReuse = true;
      this.Item.useAnimation = 30;
      this.Item.useTime = 3;
      ((Entity) this.Item).width = 54;
      ((Entity) this.Item).height = 14;
      this.Item.shoot = ModContent.ProjectileType<HellFlame>();
      this.Item.useAmmo = AmmoID.Gel;
      this.Item.UseSound = new SoundStyle?(SoundID.DD2_BetsyFlameBreath);
      this.Item.noMelee = true;
      this.Item.value = Item.sellPrice(0, 15, 0, 0);
      this.Item.rare = 11;
      this.Item.DamageType = DamageClass.Ranged;
    }

    public virtual bool Shoot(
      Player player,
      EntitySource_ItemUse_WithAmmo source,
      Vector2 position,
      Vector2 velocity,
      int type,
      int damage,
      float knockback)
    {
      Projectile.NewProjectile((IEntitySource) source, Vector2.op_Addition(position, Vector2.op_Multiply(Vector2.Normalize(velocity), 60f)), Utils.RotatedByRandom(velocity, (double) MathHelper.ToRadians(5f)), type, damage, knockback, ((Entity) player).whoAmI, (float) Main.rand.Next(3), 0.0f, 0.0f);
      if (--this.skullTimer < 0)
      {
        this.skullTimer = 5;
        SoundEngine.PlaySound(ref SoundID.DD2_BetsyFireballShot, new Vector2?(), (SoundUpdateCallback) null);
        Projectile.NewProjectile((IEntitySource) source, position, Vector2.op_Multiply(2f, velocity), ModContent.ProjectileType<DragonFireball>(), damage, knockback * 6f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
      return false;
    }

    public virtual bool CanConsumeAmmo(Item ammo, Player player) => Utils.NextBool(Main.rand, 3);

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(-30f, 0.0f));

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "DragonBreath", 1).AddIngredient((Mod) null, "AbomEnergy", 10).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerBetsy"), 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
