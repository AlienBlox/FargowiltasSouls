// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Challengers.CrystallineCongregation
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Projectiles.ChallengerItems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.Challengers
{
  public class CrystallineCongregation : SoulsItem
  {
    private int delay;
    private bool lastLMouse;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 56;
      this.Item.DamageType = DamageClass.Magic;
      ((Entity) this.Item).width = 56;
      ((Entity) this.Item).height = 56;
      this.Item.useTime = 5;
      this.Item.useAnimation = 5;
      this.Item.channel = true;
      this.Item.useStyle = 5;
      this.Item.knockBack = 1f;
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
      this.Item.rare = 5;
      this.Item.UseSound = new SoundStyle?(SoundID.Item101);
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<CrystallineCongregationProj>();
      this.Item.shootSpeed = 1f;
      this.Item.noMelee = true;
      this.Item.mana = 7;
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(0.0f, 0.0f));

    public virtual void HoldItem(Player player)
    {
      if (this.lastLMouse && !Main.mouseLeft && this.delay == 0)
        this.delay = 70;
      if (this.delay > 0)
        --this.delay;
      if (this.delay == 1)
      {
        if (((Entity) player).whoAmI == Main.myPlayer)
        {
          SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ChargeSound", (SoundType) 0);
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        }
        double num = Math.PI / 18.0;
        for (int index1 = 0; index1 < 36; ++index1)
        {
          Vector2 vector2 = Utils.RotatedBy(new Vector2(2f, 2f), num * (double) index1, new Vector2());
          int index2 = Dust.NewDust(((Entity) player).Center, 0, 0, 70, vector2.X, vector2.Y, 100, new Color(), 1f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].noLight = true;
        }
      }
      this.lastLMouse = Main.mouseLeft;
      base.HoldItem(player);
    }

    public virtual bool CanUseItem(Player player)
    {
      if (player.ownedProjectileCounts[this.Item.shoot] >= 30)
      {
        this.Item.useTime = 30;
        this.Item.useAnimation = 30;
        Item obj = this.Item;
        SoundStyle soundStyle = SoundID.Item43;
        ((SoundStyle) ref soundStyle).Volume = 0.25f;
        ((SoundStyle) ref soundStyle).MaxInstances = 10;
        SoundStyle? nullable = new SoundStyle?(soundStyle);
        obj.UseSound = nullable;
      }
      else
      {
        this.Item.useTime = 5;
        this.Item.useAnimation = 5;
        this.Item.UseSound = new SoundStyle?(SoundID.Item101);
      }
      return this.delay <= 0 && base.CanUseItem(player);
    }

    public virtual bool CanShoot(Player player)
    {
      return player.ownedProjectileCounts[this.Item.shoot] < 30 && base.CanShoot(player);
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
      int num = 35;
      Vector2 vector2 = Vector2.op_Addition(Main.MouseWorld, new Vector2((float) Main.rand.Next(-num, num), (float) Main.rand.Next(-num, num)));
      Projectile.NewProjectile((IEntitySource) source, vector2.X, vector2.Y, 0.0f, 0.0f, type, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient<LifelightBag>(2).AddTile(220).DisableDecraft().Register();
    }
  }
}
