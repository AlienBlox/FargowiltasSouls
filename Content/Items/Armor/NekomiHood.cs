// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Armor.NekomiHood
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Content.Projectiles.Minions;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Armor
{
  [AutoloadEquip]
  public class NekomiHood : SoulsItem
  {
    public const int MAX_METER = 3600;
    public const int MAX_HEARTS = 9;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
      ArmorIDs.Head.Sets.DrawHatHair[this.Item.headSlot] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 18;
      ((Entity) this.Item).height = 18;
      this.Item.rare = 4;
      this.Item.value = Item.sellPrice(0, 1, 50, 0);
      this.Item.defense = 7;
    }

    public virtual void UpdateEquip(Player player)
    {
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 0.07f);
      player.maxMinions += 2;
    }

    public virtual bool IsArmorSet(Item head, Item body, Item legs)
    {
      return body.type == ModContent.ItemType<NekomiHoodie>() && legs.type == ModContent.ItemType<NekomiLeggings>();
    }

    public virtual void UpdateArmorSet(Player player)
    {
      player.setBonus = NekomiHood.getSetBonusString();
      NekomiHood.NekomiSetBonus(player, this.Item);
    }

    public static string getSetBonusString()
    {
      return Language.GetTextValue("Mods.FargowiltasSouls.SetBonus.Nekomi", (object) Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN"));
    }

    public static void NekomiSetBonusKey(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!fargoSoulsPlayer.NekomiSet || ((Entity) player).whoAmI != Main.myPlayer)
        return;
      if (fargoSoulsPlayer.NekomiAttackReadyTimer > 0)
      {
        int rawBaseDamage = 740;
        if (!Main.hardMode)
          rawBaseDamage /= 2;
        FargoSoulsUtil.NewSummonProjectile(((Entity) player).GetSource_Misc(""), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<NekomiDevi>(), rawBaseDamage, 16f, ((Entity) player).whoAmI);
        SoundEngine.PlaySound(ref SoundID.Item43, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        fargoSoulsPlayer.NekomiMeter = 0;
        fargoSoulsPlayer.NekomiAttackReadyTimer = 0;
      }
      else
      {
        int num = (int) ((double) fargoSoulsPlayer.NekomiMeter / 3600.0 * 9.0);
        for (int index = 0; index < num; ++index)
        {
          Vector2 vector2 = Vector2.op_Multiply(-150f, Utils.RotatedBy(Vector2.UnitY, 6.2831854820251465 / (double) num * (double) index, new Vector2()));
          Vector2 spawn = Vector2.op_Addition(((Entity) player).Center, vector2);
          Vector2 velocity = Vector2.op_Multiply(12f, ((Entity) player).DirectionFrom(spawn));
          int rawBaseDamage = 17;
          FargoSoulsUtil.NewSummonProjectile(((Entity) player).GetSource_Misc(""), spawn, velocity, ModContent.ProjectileType<FriendHeart>(), rawBaseDamage, 3f, ((Entity) player).whoAmI, -1f, 12.5f);
        }
        if (num <= 0)
          return;
        fargoSoulsPlayer.NekomiMeter = 0;
      }
    }

    public static void NekomiSetBonus(Player player, Item item)
    {
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 0.07f);
      player.GetCritChance(DamageClass.Generic) += 7f;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.Graze = true;
      fargoSoulsPlayer.NekomiSet = true;
      player.AddEffect<MasoGrazeRing>(item);
      if (fargoSoulsPlayer.Graze && ((Entity) player).whoAmI == Main.myPlayer && player.HasEffect<MasoGrazeRing>() && player.ownedProjectileCounts[ModContent.ProjectileType<GrazeRing>()] < 1)
        Projectile.NewProjectile(player.GetSource_Accessory(item, (string) null), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<GrazeRing>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      if (fargoSoulsPlayer.NekomiTimer > 0)
      {
        int num = fargoSoulsPlayer.NekomiTimer / 90 + 1;
        fargoSoulsPlayer.NekomiTimer -= num;
        fargoSoulsPlayer.NekomiMeter += num;
        if (fargoSoulsPlayer.NekomiMeter > 3600)
          fargoSoulsPlayer.NekomiMeter = 3600;
      }
      else if (--fargoSoulsPlayer.NekomiTimer < -420)
      {
        if (fargoSoulsPlayer.NekomiTimer < -840)
          fargoSoulsPlayer.NekomiTimer = -840;
        int num = -420 - fargoSoulsPlayer.NekomiTimer;
        fargoSoulsPlayer.NekomiMeter -= (int) MathHelper.Lerp(1f, 8f, (float) (num / 420));
        if (fargoSoulsPlayer.NekomiMeter < 0)
          fargoSoulsPlayer.NekomiMeter = 0;
      }
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      if (fargoSoulsPlayer.NekomiMeter >= 3600)
        fargoSoulsPlayer.NekomiAttackReadyTimer = 30;
      int index = ModContent.ProjectileType<NekomiRitual>();
      if (player.ownedProjectileCounts[index] >= 1)
        return;
      Projectile.NewProjectile(player.GetSource_Accessory(item, (string) null), ((Entity) player).Center, Vector2.Zero, index, 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }

    public static void OnGraze(FargoSoulsPlayer fargoPlayer, int damage)
    {
      if (fargoPlayer.NekomiSet)
        fargoPlayer.NekomiTimer = Math.Clamp(fargoPlayer.NekomiTimer + 60, 0, 420);
      if (Main.dedServ)
        return;
      SoundStyle soundStyle;
      // ISSUE: explicit constructor call
      ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/Graze", (SoundType) 0);
      ((SoundStyle) ref soundStyle).Volume = 0.5f;
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(225, 10).AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 5).AddTile(86).Register();
    }
  }
}
