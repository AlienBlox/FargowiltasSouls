// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.HeartoftheMasochist
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  [AutoloadEquip]
  public class HeartoftheMasochist : SoulsItem
  {
    public override bool Eternity => true;

    public virtual void SetStaticDefaults()
    {
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(4, 5, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = 11;
      this.Item.value = Item.sellPrice(0, 9, 0, 0);
      this.Item.defense = 10;
    }

    public virtual void UpdateInventory(Player player) => player.FargoSouls().CanAmmoCycle = true;

    public virtual void UpdateVanity(Player player) => player.FargoSouls().CanAmmoCycle = true;

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.FargoSouls().CanAmmoCycle = true;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 0.1f);
      player.GetCritChance(DamageClass.Generic) += 10f;
      fargoSoulsPlayer.MasochistHeart = true;
      player.endurance += 0.05f;
      player.buffImmune[ModContent.BuffType<LivingWastelandBuff>()] = true;
      player.AddEffect<PumpkingsCapeEffect>(this.Item);
      player.buffImmune[ModContent.BuffType<HypothermiaBuff>()] = true;
      IceQueensCrown.AddEffects(player, this.Item);
      player.buffImmune[144] = true;
      player.buffImmune[164] = true;
      player.AddEffect<UfoMinionEffect>(this.Item);
      player.buffImmune[197] = true;
      player.buffImmune[196] = true;
      player.buffImmune[195] = true;
      fargoSoulsPlayer.BetsysHeartItem = this.Item;
      player.buffImmune[103] = true;
      player.buffImmune[148] = true;
      player.buffImmune[ModContent.BuffType<MutantNibbleBuff>()] = true;
      player.buffImmune[ModContent.BuffType<OceanicMaulBuff>()] = true;
      fargoSoulsPlayer.MutantAntibodies = true;
      if (player.mount.Active && player.mount.Type == 12)
        player.dripping = true;
      player.buffImmune[ModContent.BuffType<FlippedBuff>()] = true;
      player.buffImmune[ModContent.BuffType<FlippedHallowBuff>()] = true;
      player.buffImmune[ModContent.BuffType<UnstableBuff>()] = true;
      player.buffImmune[ModContent.BuffType<CurseoftheMoonBuff>()] = true;
      player.AddEffect<MasoGravEffect>(this.Item);
      player.AddEffect<MasoTrueEyeMinion>(this.Item);
      fargoSoulsPlayer.GravityGlobeEXItem = this.Item;
      ++fargoSoulsPlayer.WingTimeModifier;
      player.buffImmune[ModContent.BuffType<SmiteBuff>()] = true;
      fargoSoulsPlayer.PrecisionSeal = true;
      player.AddEffect<PrecisionSealHurtbox>(this.Item);
      player.buffImmune[145] = true;
      player.buffImmune[ModContent.BuffType<NullificationCurseBuff>()] = true;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<PumpkingsCape>(), 1).AddIngredient(ModContent.ItemType<IceQueensCrown>(), 1).AddIngredient(ModContent.ItemType<SaucerControlConsole>(), 1).AddIngredient(ModContent.ItemType<BetsysHeart>(), 1).AddIngredient(ModContent.ItemType<MutantAntibodies>(), 1).AddIngredient(ModContent.ItemType<PrecisionSeal>(), 1).AddIngredient(ModContent.ItemType<GalacticGlobe>(), 1).AddIngredient(3467, 15).AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 10).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
