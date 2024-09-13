// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Challengers.KamikazeSquirrelStaff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.Challengers
{
  public class KamikazeSquirrelStaff : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 60;
      this.Item.DamageType = DamageClass.Summon;
      ((Entity) this.Item).width = 50;
      ((Entity) this.Item).height = 50;
      this.Item.useTime = 46;
      this.Item.useAnimation = 46;
      this.Item.useStyle = 1;
      this.Item.knockBack = 8f;
      this.Item.value = Item.sellPrice(0, 0, 50, 0);
      this.Item.rare = 1;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<KamikazeSquirrel>();
      this.Item.shootSpeed = 1f;
      this.Item.mana = 10;
      this.Item.noMelee = true;
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
      player.SpawnMinionOnCursor((IEntitySource) source, ((Entity) player).whoAmI, type, this.Item.damage, knockback, new Vector2(), velocity);
      return false;
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool CanShoot(Player player) => player.altFunctionUse != 2;

    public virtual float UseSpeedMultiplier(Player player) => base.UseSpeedMultiplier(player);

    public virtual bool? UseItem(Player player)
    {
      if (player.ItemTimeIsZero && player.altFunctionUse == 2)
      {
        foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.owner == ((Entity) player).whoAmI && p.type == this.Item.shoot)))
          projectile.Kill();
      }
      return new bool?(true);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient<TrojanSquirrelBag>(2).AddTile(220).DisableDecraft().Register();
    }
  }
}
