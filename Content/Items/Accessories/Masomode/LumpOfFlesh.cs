// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.LumpOfFlesh
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Minions;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  [AutoloadEquip]
  public class LumpOfFlesh : SoulsItem
  {
    public override bool Eternity => true;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = 7;
      this.Item.value = Item.sellPrice(0, 7, 0, 0);
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.buffImmune[80] = true;
      player.buffImmune[163] = true;
      player.buffImmune[160] = true;
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 0.15f);
      player.aggro -= 400;
      player.FargoSouls().SkullCharm = true;
      player.FargoSouls().LumpOfFlesh = true;
      player.AddEffect<PungentEyeballCursor>(this.Item);
      player.FargoSouls().PungentEyeball = true;
      if (player.AddEffect<PungentMinion>(this.Item))
      {
        player.buffImmune[ModContent.BuffType<CrystalSkullBuff>()] = true;
        player.AddBuff(ModContent.BuffType<PungentEyeballBuff>(), 5, true, false);
      }
      player.buffImmune[ModContent.BuffType<AnticoagulationBuff>()] = true;
      player.noKnockback = true;
      player.AddEffect<DreadShellEffect>(this.Item);
      player.buffImmune[32] = true;
      player.buffImmune[47] = true;
      player.AddEffect<DeerclawpsDive>(this.Item);
      player.AddEffect<DeerclawpsEffect>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<PungentEyeball>(), 1).AddIngredient(ModContent.ItemType<SkullCharm>(), 1).AddIngredient(ModContent.ItemType<DreadShell>(), 1).AddIngredient(ModContent.ItemType<Deerclawps>(), 1).AddIngredient(3261, 10).AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 10).AddTile(134).Register();
    }
  }
}
