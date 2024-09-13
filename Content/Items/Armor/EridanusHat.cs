// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Armor.EridanusHat
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Projectiles.Minions;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
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
  public class EridanusHat : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 18;
      ((Entity) this.Item).height = 18;
      this.Item.rare = 11;
      this.Item.value = Item.sellPrice(0, 14, 0, 0);
      this.Item.defense = 20;
    }

    public virtual void UpdateEquip(Player player)
    {
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 0.05f);
      player.GetCritChance(DamageClass.Generic) += 5f;
      player.maxMinions += 4;
      ++player.maxTurrets;
    }

    public virtual bool IsArmorSet(Item head, Item body, Item legs)
    {
      return body.type == ModContent.ItemType<EridanusBattleplate>() && legs.type == ModContent.ItemType<EridanusLegwear>();
    }

    public virtual void ArmorSetShadows(Player player) => player.armorEffectDrawOutlines = true;

    public virtual void UpdateArmorSet(Player player)
    {
      player.setBonus = EridanusHat.getSetBonusString(player);
      EridanusHat.EridanusSetBonus(player, this.Item);
    }

    public static string getSetBonusString(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      string textValue = Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN");
      return Language.GetTextValue("Mods.FargowiltasSouls.SetBonus.Eridanus" + (fargoSoulsPlayer.EridanusEmpower ? "On" : "Off"), (object) textValue);
    }

    public static void EridanusSetBonusKey(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!fargoSoulsPlayer.EridanusSet)
        return;
      fargoSoulsPlayer.EridanusEmpower = !fargoSoulsPlayer.EridanusEmpower;
    }

    public static void EridanusSetBonus(Player player, Item item)
    {
      FargoSoulsPlayer fargoPlayer = player.FargoSouls();
      fargoPlayer.EridanusSet = true;
      if (fargoPlayer.EridanusEmpower)
      {
        if (fargoPlayer.EridanusTimer % 600 == 1)
        {
          SoundEngine.PlaySound(ref SoundID.Item4, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
          int num;
          switch (fargoPlayer.EridanusTimer / 600)
          {
            case 0:
              num = (int) sbyte.MaxValue;
              break;
            case 1:
              num = 229;
              break;
            case 2:
              num = 242;
              break;
            default:
              num = 135;
              player.Eternity().MasomodeMinionNerfTimer = 0;
              break;
          }
          for (int index1 = 0; index1 < 100; ++index1)
          {
            Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitY, 20f), (double) (index1 - 49) * 6.2831854820251465 / 100.0, new Vector2()), ((Entity) player).Center);
            Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) player).Center);
            int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, num, 0.0f, 0.0f, 0, new Color(), 3f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity = vector2_2;
          }
          for (int index3 = 0; index3 < 50; ++index3)
          {
            int index4 = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, num, 0.0f, 0.0f, 0, new Color(), 2.5f);
            Main.dust[index4].noGravity = true;
            Main.dust[index4].noLight = true;
            Dust dust = Main.dust[index4];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 24f);
          }
        }
        if (++fargoPlayer.EridanusTimer > 2400)
          fargoPlayer.EridanusTimer = 0;
        switch (fargoPlayer.EridanusTimer / 600)
        {
          case 0:
            Bonuses(DamageClass.Melee);
            break;
          case 1:
            Bonuses(DamageClass.Ranged);
            break;
          case 2:
            Bonuses(DamageClass.Magic);
            break;
          default:
            Bonuses(DamageClass.Summon);
            break;
        }
        if (((Entity) player).whoAmI != Main.myPlayer)
          return;
        if (player.ownedProjectileCounts[ModContent.ProjectileType<EridanusMinion>()] < 1)
          FargoSoulsUtil.NewSummonProjectile(player.GetSource_Accessory(item, (string) null), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<EridanusMinion>(), 300, 12f, ((Entity) player).whoAmI, -1f);
        if (player.ownedProjectileCounts[ModContent.ProjectileType<EridanusRitual>()] >= 1)
          return;
        Projectile.NewProjectile(player.GetSource_Accessory(item, (string) null), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<EridanusRitual>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
      else
      {
        DamageClass damageClass = player.ProcessDamageTypeFromHeldItem();
        ref StatModifier local = ref player.GetDamage(damageClass);
        local = StatModifier.op_Addition(local, 0.2f);
        player.GetCritChance(damageClass) += 10f;
      }

      void Bonuses(DamageClass damageClass)
      {
        ref StatModifier local = ref player.GetDamage(damageClass);
        local = StatModifier.op_Addition(local, 0.8f);
        if (damageClass == DamageClass.Summon)
          fargoPlayer.MinionCrits = true;
        player.GetCritChance(damageClass) += 30f;
        if (!player.HeldItem.CountsAsClass(damageClass))
          return;
        fargoPlayer.AttackSpeed += 0.3f;
      }
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<Eridanium>(), 5).AddIngredient(3458, 5).AddIngredient(3456, 5).AddIngredient(3457, 5).AddIngredient(3459, 5).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
