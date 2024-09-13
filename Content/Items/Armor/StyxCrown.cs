// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Armor.StyxCrown
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Projectiles;
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
  public class StyxCrown : SoulsItem
  {
    public const int MINIMUM_CHARGE_TIME = 2400;
    public const int MAX_SCYTHES = 12;
    public const int METER_THRESHOLD = 125000;
    public const int MINIMUM_DPS = 37500;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
      ArmorIDs.Head.Sets.DrawHatHair[this.Item.headSlot] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 18;
      ((Entity) this.Item).height = 18;
      this.Item.rare = 11;
      this.Item.value = Item.sellPrice(0, 20, 0, 0);
      this.Item.defense = 20;
    }

    public virtual void UpdateEquip(Player player)
    {
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 0.1f);
      player.GetCritChance(DamageClass.Generic) += 10f;
      player.maxMinions += 3;
      ++player.maxTurrets;
    }

    public virtual bool IsArmorSet(Item head, Item body, Item legs)
    {
      return body.type == ModContent.ItemType<StyxChestplate>() && legs.type == ModContent.ItemType<StyxLeggings>();
    }

    public virtual void ArmorSetShadows(Player player)
    {
      player.armorEffectDrawOutlinesForbidden = true;
    }

    public virtual void UpdateArmorSet(Player player)
    {
      player.setBonus = StyxCrown.getSetBonusString();
      StyxCrown.StyxSetBonus(player, this.Item);
    }

    public static string getSetBonusString()
    {
      return Language.GetTextValue("Mods.FargowiltasSouls.SetBonus.Styx", (object) Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN"));
    }

    public static void StyxSetBonusKey(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!fargoSoulsPlayer.StyxSet || ((Entity) player).whoAmI != Main.myPlayer || player.ownedProjectileCounts[ModContent.ProjectileType<StyxGazerArmor>()] > 0)
        return;
      int num = ModContent.ProjectileType<StyxArmorScythe>();
      bool flag1 = fargoSoulsPlayer.StyxAttackReadyTimer > 0;
      for (int index = 0; index < Main.maxProjectiles; ++index)
      {
        if (((Entity) Main.projectile[index]).active && Main.projectile[index].friendly && Main.projectile[index].type == num && Main.projectile[index].owner == ((Entity) player).whoAmI)
        {
          if (!flag1)
            Projectile.NewProjectile(((Entity) Main.projectile[index]).GetSource_FromThis((string) null), ((Entity) Main.projectile[index]).Center, Vector2.op_Multiply(Vector2.Normalize(((Entity) Main.projectile[index]).velocity), 24f), ModContent.ProjectileType<StyxArmorScythe2>(), Main.projectile[index].damage, Main.projectile[index].knockBack, ((Entity) player).whoAmI, -1f, -1f, 0.0f);
          Main.projectile[index].Kill();
        }
      }
      if (!flag1)
        return;
      Vector2 vector2_1 = Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center));
      bool flag2 = (double) vector2_1.X < 0.0;
      Vector2 vector2_2 = Utils.RotatedBy(vector2_1, 1.5707963705062866 * (flag2 ? 1.0 : -1.0), new Vector2());
      Projectile.NewProjectile(((Entity) player).GetSource_Misc(""), ((Entity) player).Center, vector2_2, ModContent.ProjectileType<StyxGazerArmor>(), 0, 14f, ((Entity) player).whoAmI, (float) (0.02617993950843811 * (flag2 ? -1.0 : 1.0)), 0.0f, 0.0f);
      player.controlUseItem = false;
      player.releaseUseItem = true;
      fargoSoulsPlayer.StyxAttackReadyTimer = 0;
    }

    public static void StyxSetBonus(Player player, Item item)
    {
      ref StatModifier local = ref player.GetDamage(player.ProcessDamageTypeFromHeldItem());
      local = StatModifier.op_Addition(local, 0.2f);
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.StyxSet = true;
      int index = ModContent.ProjectileType<StyxArmorScythe>();
      if (fargoSoulsPlayer.StyxMeter > 125000)
      {
        fargoSoulsPlayer.StyxMeter -= 125000;
        if (((Entity) player).whoAmI == Main.myPlayer && player.ownedProjectileCounts[index] < 12)
        {
          Projectile.NewProjectile(player.GetSource_Accessory(item, (string) null), ((Entity) player).Center, Vector2.Zero, index, 0, 10f, ((Entity) player).whoAmI, (float) player.ownedProjectileCounts[index], -1f, 0.0f);
          if (++player.ownedProjectileCounts[index] >= 12 && !Main.dedServ)
          {
            SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/FullMeter", (SoundType) 0);
            SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
          }
        }
      }
      if (((Entity) player).whoAmI != Main.myPlayer || player.ownedProjectileCounts[index] < 12)
        return;
      fargoSoulsPlayer.StyxMeter = 0;
      fargoSoulsPlayer.StyxTimer = 0;
      fargoSoulsPlayer.StyxAttackReadyTimer = 30;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(549, 15).AddIngredient(3467, 5).AddIngredient(ModContent.ItemType<AbomEnergy>(), 10).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
