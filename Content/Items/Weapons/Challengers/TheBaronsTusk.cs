// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Challengers.TheBaronsTusk
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Projectiles.ChallengerItems;
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
  public class TheBaronsTusk : SoulsItem
  {
    private int Timer;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
      ItemID.Sets.BonusAttackSpeedMultiplier[this.Type] = 0.25f;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 70;
      this.Item.DamageType = DamageClass.Melee;
      ((Entity) this.Item).width = 66;
      ((Entity) this.Item).height = 64;
      this.Item.useTime = 30;
      this.Item.useAnimation = 30;
      this.Item.useStyle = 1;
      this.Item.knockBack = 6f;
      this.Item.value = Item.sellPrice(0, 4, 0, 0);
      this.Item.rare = 4;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<BaronTuskShrapnel>();
      this.Item.shootSpeed = 15f;
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
      return false;
    }

    public virtual void HoldItem(Player player)
    {
      if (player.itemAnimation == 0)
      {
        this.Timer = 0;
      }
      else
      {
        if (player.itemAnimation == player.itemAnimationMax)
          this.Timer = player.itemAnimationMax;
        if (player.itemAnimation > 0)
          --this.Timer;
        if (this.Timer == player.itemAnimationMax / 2)
        {
          SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
          SoundEngine.PlaySound(ref SoundID.Item39, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
          for (int index = 0; index < 3; ++index)
          {
            Vector2 vector2 = Vector2.op_Multiply(this.Item.shootSpeed + (float) Main.rand.Next(-2, 2), Utils.RotatedByRandom(Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, player.itemLocation)), 0.22439947724342346));
            Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), player.itemLocation, vector2, this.Item.shoot, (int) ((double) player.ActualClassDamage(DamageClass.Melee) * (double) this.Item.damage / 3.0), this.Item.knockBack, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
          }
        }
        if (this.Timer > 2 * player.itemAnimationMax / 3)
        {
          player.itemAnimation = player.itemAnimationMax;
          this.Item.noMelee = true;
        }
        else
        {
          this.Item.noMelee = false;
          float x = (float) this.Timer / (float) (2 * player.itemAnimationMax / 3);
          player.itemAnimation = (int) ((double) player.itemAnimationMax * Math.Pow((double) TheBaronsTusk.MomentumProgress(x), 2.0));
        }
      }
    }

    public virtual void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
    {
      IEnumerable<Projectile> source = ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => p.TypeAlive<BaronTuskShrapnel>() && p.owner == ((Entity) player).whoAmI && Luminance.Common.Utilities.Utilities.As<BaronTuskShrapnel>(p).EmbeddedNPC == target));
      int num = source.Count<Projectile>();
      if (num < 15)
        return;
      SoundEngine.PlaySound(ref SoundID.Item68, new Vector2?(((Entity) target).Center), (SoundUpdateCallback) null);
      ref AddableFloat local = ref modifiers.FlatBonusDamage;
      local = AddableFloat.op_Addition(local, (float) (15 * this.Item.damage) / 2.5f + (float) (num * this.Item.damage / 6));
      ((NPC.HitModifiers) ref modifiers).SetCrit();
      foreach (Projectile projectile in source)
        projectile.ai[1] = 2f;
    }

    public static float MomentumProgress(float x)
    {
      return (float) ((double) x * (double) x * 3.0 - (double) x * (double) x * (double) x * 2.0);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient<BanishedBaronBag>(2).AddTile(220).DisableDecraft().Register();
    }
  }
}
