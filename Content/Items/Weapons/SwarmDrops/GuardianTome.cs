// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.GuardianTome
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.SwarmDrops
{
  public class GuardianTome : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 1499;
      this.Item.DamageType = DamageClass.Magic;
      ((Entity) this.Item).width = 24;
      ((Entity) this.Item).height = 28;
      this.Item.useTime = 50;
      this.Item.useAnimation = 50;
      this.Item.useStyle = 5;
      this.Item.useTurn = false;
      this.Item.noMelee = true;
      this.Item.knockBack = 2f;
      this.Item.value = Item.sellPrice(0, 70, 0, 0);
      this.Item.rare = 11;
      this.Item.mana = 100;
      this.Item.UseSound = new SoundStyle?(SoundID.Item21);
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<DungeonGuardian>();
      this.Item.shootSpeed = 18f;
    }

    public override void SafeModifyTooltips(List<TooltipLine> tooltips)
    {
      tooltips.FirstOrDefault<TooltipLine>((Func<TooltipLine, bool>) (line => line.Name == "ItemName" && line.Mod == "Terraria")).OverrideColor = new Color?(new Color((int) byte.MaxValue, Main.DiscoG, 0));
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerDG"), 1).AddIngredient(ModContent.ItemType<EternalEnergy>(), 15).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
