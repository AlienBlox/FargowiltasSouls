// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Sasha.MissDrakovisFishingPole
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Sasha
{
  public class MissDrakovisFishingPole : PatreonModItem
  {
    private int mode = 1;
    private int modeSwitchCD;

    public virtual string Texture => "Terraria/Images/Item_2296";

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void SetDefaults()
    {
      this.Item.damage = 360;
      ((Entity) this.Item).width = 24;
      ((Entity) this.Item).height = 28;
      this.Item.value = Item.sellPrice(0, 15, 0, 0);
      this.Item.rare = 10;
      this.Item.autoReuse = true;
      this.SetUpItem();
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual void HoldItem(Player player)
    {
      if (this.modeSwitchCD <= 0)
        return;
      --this.modeSwitchCD;
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
      if (player.altFunctionUse == 2 && this.modeSwitchCD <= 0)
      {
        if (++this.mode > 4)
          this.mode = 1;
        this.SetUpItem();
        this.modeSwitchCD = this.Item.useTime;
        return false;
      }
      switch (this.mode)
      {
        case 1:
          Projectile.NewProjectile((IEntitySource) source, position, velocity, ModContent.ProjectileType<PufferRang>(), damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
          break;
        case 2:
          Vector2 vector2_1 = velocity;
          int num1 = Utils.NextBool(Main.rand) ? 5 : 4;
          for (int index = 0; index < num1; ++index)
          {
            Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, Utils.NextFloat(Main.rand, 0.95f, 1.05f));
            vector2_2.X += Utils.NextFloat(Main.rand, -1f, 1f);
            vector2_2.Y += Utils.NextFloat(Main.rand, -1f, 1f);
            Projectile.NewProjectile((IEntitySource) source, position, vector2_2, type, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
          }
          for (int index = 0; index < Main.maxNPCs; ++index)
          {
            if (((Entity) Main.npc[index]).active && Main.npc[index].CanBeChasedBy((object) null, false) && (double) ((Entity) player).Distance(((Entity) Main.npc[index]).Center) < 1000.0)
            {
              Vector2 vector2_3 = Vector2.op_Multiply(2f * ((Vector2) ref vector2_1).Length(), Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, ((Entity) Main.npc[index]).Center));
              Projectile.NewProjectile((IEntitySource) source, position, vector2_3, type, damage / 2, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
            }
          }
          break;
        case 3:
          Vector2 vector2_4 = velocity;
          for (int index = -2; index <= 2; ++index)
          {
            float num2 = (float) (1.0 - 0.375 * (double) Math.Abs(index));
            Projectile.NewProjectile((IEntitySource) source, position, Vector2.op_Multiply(num2, Utils.RotatedBy(vector2_4, (double) MathHelper.ToRadians(9f) * (double) index, new Vector2())), ModContent.ProjectileType<Bubble>(), damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
          }
          break;
        case 4:
          FargoSoulsUtil.NewSummonProjectile((IEntitySource) source, position, velocity, ModContent.ProjectileType<FishMinion>(), this.Item.damage / 2 / 3, knockback, ((Entity) player).whoAmI);
          break;
        default:
          for (int index = 0; index < 10; ++index)
            Projectile.NewProjectile((IEntitySource) source, position, Vector2.op_Multiply(4f, new Vector2(velocity.X + (float) Main.rand.Next(-2, 2), velocity.Y + (float) Main.rand.Next(-2, 2))), ModContent.ProjectileType<SpikyLure>(), damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
          break;
      }
      return false;
    }

    private void SetUpItem()
    {
      this.ResetDamageType();
      switch (this.mode)
      {
        case 1:
          this.Item.DamageType = DamageClass.Melee;
          this.Item.shoot = ModContent.ProjectileType<PufferRang>();
          this.Item.useStyle = 1;
          this.Item.useTime = 12;
          this.Item.useAnimation = 12;
          this.Item.UseSound = new SoundStyle?(SoundID.Item1);
          this.Item.knockBack = 6f;
          this.Item.noMelee = false;
          this.Item.shootSpeed = 15f;
          break;
        case 2:
          this.Item.DamageType = DamageClass.Ranged;
          this.Item.shoot = 14;
          this.Item.knockBack = 6.5f;
          this.Item.useStyle = 5;
          this.Item.useAnimation = 50;
          this.Item.useTime = 50;
          this.Item.useAmmo = AmmoID.Bullet;
          this.Item.UseSound = new SoundStyle?(SoundID.Item36);
          this.Item.shootSpeed = 12f;
          this.Item.noMelee = true;
          break;
        case 3:
          this.Item.DamageType = DamageClass.Magic;
          this.Item.mana = 15;
          this.Item.shoot = ModContent.ProjectileType<Bubble>();
          this.Item.knockBack = 3f;
          this.Item.useStyle = 5;
          this.Item.useAnimation = 25;
          this.Item.useTime = 25;
          this.Item.UseSound = new SoundStyle?(SoundID.Item85);
          this.Item.shootSpeed = 30f;
          this.Item.noMelee = true;
          break;
        case 4:
          this.Item.DamageType = DamageClass.Summon;
          this.Item.mana = 10;
          this.Item.shoot = ModContent.ProjectileType<FishMinion>();
          this.Item.useTime = 36;
          this.Item.useAnimation = 36;
          this.Item.useStyle = 1;
          this.Item.noMelee = true;
          this.Item.knockBack = 3f;
          this.Item.UseSound = new SoundStyle?(SoundID.Item44);
          this.Item.shootSpeed = 10f;
          this.Item.buffType = ModContent.BuffType<FishMinionBuff>();
          this.Item.buffTime = 3600;
          this.Item.autoReuse = true;
          break;
        case 5:
          this.Item.DamageType = DamageClass.Throwing;
          this.Item.shoot = ModContent.ProjectileType<SpikyLure>();
          this.Item.useStyle = 1;
          this.Item.shootSpeed = 5f;
          this.Item.knockBack = 1f;
          this.Item.UseSound = new SoundStyle?(SoundID.Item1);
          this.Item.useAnimation = 15;
          this.Item.useTime = 15;
          this.Item.noMelee = true;
          break;
      }
    }

    private void ResetDamageType()
    {
      this.Item.DamageType = DamageClass.Generic;
      this.Item.mana = 0;
      this.Item.useAmmo = AmmoID.None;
    }

    public virtual void AddRecipes()
    {
      if (!SoulConfig.Instance.PatreonFishingRod)
        return;
      this.CreateRecipe(1).AddIngredient(2294, 1).AddIngredient(3225, 1).AddIngredient(2330, 1).AddIngredient(3197, 500).AddIngredient(2420, 1).AddIngredient(3210, 1).AddIngredient(3211, 1).AddIngredient(3209, 1).AddIngredient(2429, 1).AddIngredient(2331, 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
