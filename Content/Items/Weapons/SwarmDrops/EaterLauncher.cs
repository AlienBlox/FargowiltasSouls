// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.EaterLauncher
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.Items.Summons.SwarmSummons.Energizers;
using FargowiltasSouls.Content.Items.Weapons.BossDrops;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.SwarmDrops
{
  public class EaterLauncher : SoulsItem
  {
    public const int BaseDistance = 600;
    public const int IncreasedDistance = 1200;
    public const int CooldownTime = 1440;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 190;
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 24;
      ((Entity) this.Item).height = 24;
      this.Item.useTime = 24;
      this.Item.useAnimation = 24;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 6f;
      this.Item.UseSound = new SoundStyle?(SoundID.Item62);
      this.Item.useAmmo = AmmoID.Rocket;
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
      this.Item.rare = 11;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<EaterRocket>();
      this.Item.shootSpeed = 24f;
      this.Item.scale = 0.7f;
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(-12f, -2f));

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool CanUseItem(Player player)
    {
      if (player.altFunctionUse == 2)
      {
        this.Item.useAnimation = 48;
        this.Item.useTime = 48;
        this.Item.UseSound = new SoundStyle?(SoundID.Item117);
      }
      else
      {
        this.Item.useAnimation = 24;
        this.Item.useTime = 24;
        this.Item.UseSound = new SoundStyle?(SoundID.Item62);
      }
      return base.CanUseItem(player);
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
      if (player.altFunctionUse == 2)
      {
        player.FargoSouls().RockeaterDistance = 1200;
        return false;
      }
      for (int index = 0; index < 3; ++index)
      {
        Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(velocity, (double) MathHelper.ToRadians(15f)), Utils.NextFloat(Main.rand, 0.9f, 1.1f));
        Projectile.NewProjectile((IEntitySource) source, Vector2.op_Addition(position, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(velocity), (float) ((Entity) this.Item).width), 0.9f)), vector2, type, damage, knockback, -1, 0.0f, 0.0f, 0.0f);
      }
      return false;
    }

    public virtual void HoldItem(Player player)
    {
      if (player.itemTime <= 0)
        return;
      for (int index = 0; index < 10; ++index)
      {
        Vector2 vector2_1 = new Vector2();
        double num1 = Main.rand.NextDouble() * 2.0 * Math.PI;
        vector2_1.X += (float) (Math.Sin(num1) * 300.0);
        vector2_1.Y += (float) (Math.Cos(num1) * 300.0);
        Dust dust1 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) player).Center, vector2_1), new Vector2(4f, 4f)), 0, 0, 70, 0.0f, 0.0f, 100, Color.White, 1f)];
        dust1.velocity = ((Entity) player).velocity;
        if (Utils.NextBool(Main.rand, 3))
        {
          Dust dust2 = dust1;
          dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2_1), 5f));
        }
        dust1.noGravity = true;
        dust1.scale = 1f;
        Vector2 vector2_2 = new Vector2();
        double num2 = Main.rand.NextDouble() * 2.0 * Math.PI;
        vector2_2.X += (float) Math.Sin(num2) * (float) player.FargoSouls().RockeaterDistance;
        vector2_2.Y += (float) Math.Cos(num2) * (float) player.FargoSouls().RockeaterDistance;
        Dust dust3 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) player).Center, vector2_2), new Vector2(4f, 4f)), 0, 0, 70, 0.0f, 0.0f, 100, Color.White, 1f)];
        dust3.velocity = ((Entity) player).velocity;
        if (Utils.NextBool(Main.rand, 3))
        {
          Dust dust4 = dust3;
          dust4.velocity = Vector2.op_Addition(dust4.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2_2), -5f));
        }
        dust3.noGravity = true;
        dust3.scale = 1f;
      }
    }

    public virtual void ModifyShootStats(
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      type = ModContent.ProjectileType<EaterRocket>();
    }

    public virtual bool CanConsumeAmmo(Item ammo, Player player) => Utils.NextBool(Main.rand);

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<EaterLauncherJr>(), 1).AddIngredient(ModContent.ItemType<EnergizerWorm>(), 1).AddIngredient(3467, 10).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
