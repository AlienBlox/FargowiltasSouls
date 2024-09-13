// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.FinalUpgrades.PhantasmalLeashOfCthulhu
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
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.FinalUpgrades
{
  public class PhantasmalLeashOfCthulhu : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 2800;
      ((Entity) this.Item).width = 30;
      ((Entity) this.Item).height = 10;
      this.Item.value = Item.sellPrice(1, 0, 0, 0);
      this.Item.rare = 11;
      this.Item.noMelee = true;
      this.Item.useStyle = 5;
      this.Item.autoReuse = true;
      this.Item.useAnimation = 25;
      this.Item.useTime = 25;
      this.Item.knockBack = 6f;
      this.Item.noUseGraphic = true;
      this.Item.shoot = ModContent.ProjectileType<PhantasmalFlail>();
      this.Item.shootSpeed = 45f;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.DamageType = DamageClass.Melee;
    }

    public override void SafeModifyTooltips(List<TooltipLine> list)
    {
      foreach (TooltipLine tooltipLine in list)
      {
        if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
          tooltipLine.OverrideColor = new Color?(new Color(0, Main.DiscoG, (int) byte.MaxValue));
      }
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<MechanicalLeashOfCthulhu>(), 1).AddIngredient(ModContent.ItemType<EternalEnergy>(), 15).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
