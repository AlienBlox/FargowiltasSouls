// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Armor.GaiaHelmet
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

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
  public class GaiaHelmet : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 18;
      ((Entity) this.Item).height = 18;
      this.Item.rare = 8;
      this.Item.value = Item.sellPrice(0, 5, 0, 0);
      this.Item.defense = 15;
    }

    public virtual void UpdateEquip(Player player)
    {
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 0.1f);
      player.GetCritChance(DamageClass.Generic) += 5f;
      ++player.maxMinions;
    }

    public virtual bool IsArmorSet(Item head, Item body, Item legs)
    {
      return body.type == ModContent.ItemType<GaiaPlate>() && legs.type == ModContent.ItemType<GaiaGreaves>();
    }

    public virtual void ArmorSetShadows(Player player)
    {
      if (!player.FargoSouls().GaiaOffense)
        return;
      player.armorEffectDrawOutlinesForbidden = true;
      player.armorEffectDrawShadow = true;
    }

    public virtual void UpdateArmorSet(Player player)
    {
      player.setBonus = GaiaHelmet.getSetBonusString();
      GaiaHelmet.GaiaSetBonus(player);
    }

    public static string getSetBonusString()
    {
      return Language.GetTextValue("Mods.FargowiltasSouls.SetBonus.Gaia", (object) Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN"));
    }

    public static void GaiaSetBonusKey(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!fargoSoulsPlayer.GaiaSet)
        return;
      fargoSoulsPlayer.GaiaOffense = !fargoSoulsPlayer.GaiaOffense;
      if (fargoSoulsPlayer.GaiaOffense)
        SoundEngine.PlaySound(ref SoundID.Item4, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
      Vector2 vector2_1 = Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI);
      for (int index1 = 0; index1 < 36; ++index1)
      {
        Vector2 vector2_2 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(vector2_1, 6f), (double) (index1 - 17) * 6.2831854820251465 / 36.0, new Vector2()), ((Entity) player).Center);
        Vector2 vector2_3 = Vector2.op_Subtraction(vector2_2, ((Entity) player).Center);
        int index2 = Dust.NewDust(Vector2.op_Addition(vector2_2, vector2_3), 0, 0, Utils.NextBool(Main.rand) ? 107 : 110, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].scale = 2.5f;
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity = vector2_3;
      }
    }

    public static void GaiaSetBonus(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.GaiaSet = true;
      player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
      player.manaCost -= 0.1f;
      player.maxMinions += 4;
      if (!fargoSoulsPlayer.GaiaOffense)
        return;
      DamageClass damageClass = player.ProcessDamageTypeFromHeldItem();
      ref StatModifier local = ref player.GetDamage(damageClass);
      local = StatModifier.op_Addition(local, 0.3f);
      player.GetCritChance(damageClass) += 15f;
      player.GetArmorPenetration(DamageClass.Generic) += 20f;
      Player player1 = player;
      player1.statDefense = Player.DefenseStat.op_Subtraction(player1.statDefense, 20);
      player.statLifeMax2 -= player.statLifeMax / 10;
      player.endurance -= 0.15f;
      Lighting.AddLight(((Entity) player).Center, new Vector3(1f, 1f, 1f));
      if (!Utils.NextBool(Main.rand, 3))
        return;
      float num1 = 2f;
      int num2 = Utils.NextBool(Main.rand) ? 107 : 110;
      int index = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, num2, ((Entity) player).velocity.X * 0.4f, ((Entity) player).velocity.Y * 0.4f, 87, new Color(), num1);
      Main.dust[index].noGravity = true;
      --Main.dust[index].velocity.Y;
      Dust dust = Main.dust[index];
      dust.velocity = Vector2.op_Multiply(dust.velocity, 1.8f);
      if (!Utils.NextBool(Main.rand, 4))
        return;
      Main.dust[index].noGravity = false;
      Main.dust[index].scale *= 0.5f;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(2218, 3).AddIngredient(1552, 6).AddIngredient(3261, 6).AddIngredient(1729, 100).AddTile(412).Register();
    }
  }
}
