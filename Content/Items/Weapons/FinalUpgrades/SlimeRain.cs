// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.FinalUpgrades.SlimeRain
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.FinalUpgrades
{
  public class SlimeRain : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 6000;
      this.Item.DamageType = DamageClass.Melee;
      ((Entity) this.Item).width = 72;
      ((Entity) this.Item).height = 90;
      this.Item.useStyle = 1;
      this.Item.DamageType = DamageClass.Melee;
      this.Item.knockBack = 6f;
      this.Item.value = Item.sellPrice(1, 0, 0, 0);
      this.Item.rare = 11;
      this.Item.UseSound = new SoundStyle?(SoundID.Item34);
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<SlimeRainBall>();
      this.Item.shootSpeed = 16f;
      this.Item.useTime = 4;
      this.Item.useAnimation = 12;
      this.Item.reuseDelay = 0;
    }

    public override void SafeModifyTooltips(List<TooltipLine> list)
    {
      foreach (TooltipLine tooltipLine in list)
      {
        if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
          tooltipLine.OverrideColor = new Color?(new Color(0, Main.DiscoG, (int) byte.MaxValue));
      }
    }

    public virtual void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(137, 240, false);
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
      float num1 = ((Entity) player).Center.Y - Utils.NextFloat(Main.rand, 600f, 700f);
      for (int index1 = 0; index1 < 5; ++index1)
      {
        float num2 = ((Entity) player).Center.X + 2f * Utils.NextFloat(Main.rand, -400f, 400f);
        float num3 = (float) Main.rand.Next(90);
        int index2 = Projectile.NewProjectile((IEntitySource) source, num2, num1, Utils.NextFloat(Main.rand, -4f, 4f), Utils.NextFloat(Main.rand, 15f, 20f), type, damage, knockback, ((Entity) player).whoAmI, 0.0f, num3, 0.0f);
        if (index2 != Main.maxProjectiles)
          Main.projectile[index2].timeLeft = 90;
      }
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<SlimeSlingingSlasher>(), 1).AddIngredient(ModContent.ItemType<EternalEnergy>(), 15).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
