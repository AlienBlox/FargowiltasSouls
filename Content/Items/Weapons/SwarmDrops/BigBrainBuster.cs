// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.BigBrainBuster
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Minions;
using FargowiltasSouls.Content.Projectiles.Minions;
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
  public class BigBrainBuster : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
      ItemID.Sets.StaffMinionSlotsRequired[this.Item.type] = 1f;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 170;
      this.Item.DamageType = DamageClass.Summon;
      this.Item.mana = 10;
      ((Entity) this.Item).width = 26;
      ((Entity) this.Item).height = 28;
      this.Item.useTime = 36;
      this.Item.useAnimation = 36;
      this.Item.useStyle = 1;
      this.Item.noMelee = true;
      this.Item.knockBack = 3f;
      this.Item.rare = 11;
      this.Item.UseSound = new SoundStyle?(SoundID.Item44);
      this.Item.shoot = ModContent.ProjectileType<BigBrainProj>();
      this.Item.shootSpeed = 10f;
      this.Item.autoReuse = true;
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
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
      player.AddBuff(ModContent.BuffType<BigBrainMinionBuff>(), 2, true, false);
      Vector2 vector2 = Vector2.op_Subtraction(((Entity) player).Center, Main.MouseWorld);
      if (player.ownedProjectileCounts[type] == 0)
      {
        FargoSoulsUtil.NewSummonProjectile((IEntitySource) source, ((Entity) player).Center, Vector2.Zero, type, this.Item.damage, knockback, ((Entity) player).whoAmI, ai1: Utils.ToRotation(vector2));
      }
      else
      {
        float num = 0.0f;
        for (int index = 0; index < Main.projectile.Length; ++index)
        {
          Projectile projectile = Main.projectile[index];
          if (projectile.owner == ((Entity) player).whoAmI && (double) projectile.minionSlots > 0.0 && ((Entity) projectile).active)
          {
            num += projectile.minionSlots;
            if (projectile.type == type && (double) num < (double) player.maxMinions && (double) projectile.minionSlots < 16.0)
              ++projectile.minionSlots;
          }
        }
      }
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "BrainStaff", 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerBrain"), 1).AddIngredient(3467, 10).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
