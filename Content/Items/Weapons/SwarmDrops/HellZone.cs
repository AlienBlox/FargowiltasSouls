// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.HellZone
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.SwarmDrops
{
  public class HellZone : SoulsItem
  {
    public int skullTimer;
    private static readonly int[] RiffVariants = new int[4]
    {
      1,
      2,
      3,
      4
    };
    private static readonly SoundStyle badtothebone;
    private int counter;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 250;
      this.Item.knockBack = 4f;
      this.Item.shootSpeed = 12f;
      this.Item.useStyle = 5;
      this.Item.autoReuse = true;
      this.Item.useAnimation = 4;
      this.Item.useTime = 4;
      ((Entity) this.Item).width = 54;
      ((Entity) this.Item).height = 14;
      this.Item.shoot = ModContent.ProjectileType<HellSkull2>();
      this.Item.useAmmo = 154;
      this.Item.UseSound = new SoundStyle?(HellZone.badtothebone);
      this.Item.noMelee = true;
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
      this.Item.rare = 11;
      this.Item.DamageType = DamageClass.Ranged;
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool CanUseItem(Player player)
    {
      if (player.altFunctionUse == 2)
      {
        this.Item.useAnimation = 40;
        this.Item.useTime = 40;
      }
      else
      {
        this.Item.useAnimation = 4;
        this.Item.useTime = 4;
      }
      return base.CanUseItem(player);
    }

    public virtual void ModifyShootStats(
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      position = Vector2.op_Addition(position, Vector2.op_Multiply(Vector2.Normalize(velocity), 40f));
      if (player.altFunctionUse != 2)
        return;
      type = ModContent.ProjectileType<HellGuardian>();
      damage *= 4;
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
        return true;
      int num1 = Main.rand.Next(1, 4);
      float num2 = (float) (0.78539818525314331 / (double) num1 * (double) Utils.NextFloat(Main.rand, 0.25f, 0.75f) * 0.75);
      ++this.counter;
      for (int index = -num1; index <= num1; ++index)
      {
        int num3;
        switch (Main.rand.Next(3))
        {
          case 0:
            num3 = ModContent.ProjectileType<HellBone>();
            break;
          case 1:
            num3 = ModContent.ProjectileType<HellBonez>();
            break;
          default:
            num3 = ModContent.ProjectileType<HellSkeletron>();
            break;
        }
        int num4 = num3;
        Projectile.NewProjectile((IEntitySource) source, position, Vector2.op_Multiply(Utils.NextFloat(Main.rand, 0.8f, 1.2f), Utils.RotatedBy(velocity, (double) num2 * (double) index + (double) Utils.NextFloat(Main.rand, -num2, num2), new Vector2())), num4, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
      if (this.counter > 4)
      {
        for (int index = -1; index <= 1; index += 2)
          Projectile.NewProjectile((IEntitySource) source, position, Vector2.op_Multiply(velocity, 1.25f), ModContent.ProjectileType<HellSkull2>(), damage, knockback, ((Entity) player).whoAmI, 0.0f, (float) index, 0.0f);
        this.counter = 0;
      }
      return false;
    }

    public virtual bool CanConsumeAmmo(Item ammo, Player player) => Utils.NextBool(Main.rand, 5);

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(-30f, -5f));

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "BoneZone", 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerSkele"), 1).AddIngredient(3467, 10).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }

    static HellZone()
    {
      SoundStyle soundStyle;
      // ISSUE: explicit constructor call
      ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/Boneriff/boneriff", (SoundType) 0);
      ((SoundStyle) ref soundStyle).Variants = ReadOnlySpan<int>.op_Implicit(HellZone.RiffVariants);
      ((SoundStyle) ref soundStyle).Volume = 0.2f;
      ((SoundStyle) ref soundStyle).MaxInstances = 10;
      ((SoundStyle) ref soundStyle).SoundLimitBehavior = (SoundLimitBehavior) 0;
      HellZone.badtothebone = soundStyle;
    }
  }
}
