// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.MasochistSoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Minions;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Consumables;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  [AutoloadEquip]
  public class MasochistSoul : BaseSoul
  {
    public static readonly Color ItemColor = new Color((int) byte.MaxValue, 51, 153, 0);

    public override bool Eternity => true;

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.value = 5000000;
      this.Item.defense = 30;
      this.Item.useTime = 180;
      this.Item.useAnimation = 180;
      this.Item.useStyle = 4;
      this.Item.useTurn = true;
      this.Item.UseSound = new SoundStyle?(SoundID.Item6);
    }

    protected override Color? nameColor => new Color?(MasochistSoul.ItemColor);

    public virtual void UseItemFrame(Player player) => SandsofTime.Use(player);

    public virtual bool? UseItem(Player player) => new bool?(true);

    private void PassiveEffect(Player player, Item item)
    {
      BionomicCluster.PassiveEffect(player, this.Item);
      player.FargoSouls().CanAmmoCycle = true;
    }

    public virtual void UpdateInventory(Player player) => this.PassiveEffect(player, this.Item);

    public virtual void UpdateVanity(Player player) => this.PassiveEffect(player, this.Item);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      BionomicCluster.PassiveEffect(player, this.Item);
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.MasochistSoul = true;
      fargoSoulsPlayer.MasochistSoulItem = this.Item;
      player.AddBuff(ModContent.BuffType<SouloftheMasochistBuff>(), 2, true, false);
      DamageClass damageClass = player.ProcessDamageTypeFromHeldItem();
      ref StatModifier local = ref player.GetDamage(damageClass);
      local = StatModifier.op_Addition(local, 0.5f);
      player.endurance += 0.1f;
      player.GetArmorPenetration(DamageClass.Generic) += 50f;
      player.statLifeMax2 += player.statLifeMax;
      if (!fargoSoulsPlayer.MutantPresence)
      {
        player.lifeRegen += 7;
        player.lifeRegenTime += 7f;
        player.lifeRegenCount += 7;
      }
      fargoSoulsPlayer.WingTimeModifier += 2f;
      player.moveSpeed += 0.2f;
      player.buffImmune[137] = true;
      player.AddEffect<SlimeFallEffect>(this.Item);
      if (player.AddEffect<SlimyShieldEffect>(this.Item))
        player.FargoSouls().SlimyShieldItem = this.Item;
      player.AddEffect<AgitatingLensEffect>(this.Item);
      player.AddEffect<AgitatingLensInstall>(this.Item);
      player.npcTypeNoAggro[210] = true;
      player.npcTypeNoAggro[211] = true;
      player.npcTypeNoAggro[42] = true;
      player.npcTypeNoAggro[176] = true;
      player.npcTypeNoAggro[231] = true;
      player.npcTypeNoAggro[232] = true;
      player.npcTypeNoAggro[233] = true;
      player.npcTypeNoAggro[234] = true;
      player.npcTypeNoAggro[235] = true;
      fargoSoulsPlayer.QueenStingerItem = this.Item;
      fargoSoulsPlayer.NecromanticBrewItem = this.Item;
      player.AddEffect<NecroBrewSpin>(this.Item);
      fargoSoulsPlayer.SupremeDeathbringerFairy = true;
      fargoSoulsPlayer.PureHeart = true;
      fargoSoulsPlayer.DarkenedHeartItem = this.Item;
      player.AddEffect<DarkenedHeartEaters>(this.Item);
      player.hasMagiluminescence = true;
      if (fargoSoulsPlayer.DarkenedHeartCD > 0)
        fargoSoulsPlayer.DarkenedHeartCD -= 2;
      player.AddEffect<GuttedHeartEffect>(this.Item);
      player.AddEffect<GuttedHeartMinions>(this.Item);
      fargoSoulsPlayer.GuttedHeartCD -= 2;
      player.FargoSouls().GelicWingsItem = this.Item;
      player.AddEffect<GelicWingJump>(this.Item);
      player.buffImmune[103] = true;
      player.buffImmune[148] = true;
      fargoSoulsPlayer.MutantAntibodies = true;
      if (player.mount.Active && player.mount.Type == 12)
        player.dripping = true;
      player.buffImmune[80] = true;
      player.buffImmune[163] = true;
      player.buffImmune[160] = true;
      fargoSoulsPlayer.SkullCharm = true;
      fargoSoulsPlayer.LumpOfFlesh = true;
      fargoSoulsPlayer.PungentEyeball = true;
      player.AddEffect<PungentEyeballCursor>(this.Item);
      player.buffImmune[ModContent.BuffType<CrystalSkullBuff>()] = true;
      player.AddEffect<SinisterIconEffect>(this.Item);
      player.AddEffect<SinisterIconDropsEffect>(this.Item);
      player.AddEffect<ClippedEffect>(this.Item);
      player.buffImmune[44] = true;
      player.buffImmune[153] = true;
      player.buffImmune[ModContent.BuffType<ShadowflameBuff>()] = true;
      player.AddEffect<WretchedPouchEffect>(this.Item);
      player.buffImmune[194] = true;
      fargoSoulsPlayer.SandsofTime = true;
      player.buffImmune[68] = true;
      player.manaFlower = true;
      fargoSoulsPlayer.SecurityWallet = true;
      player.nightVision = true;
      player.AddEffect<MasoCarrotEffect>(this.Item);
      player.AddEffect<SqueakEffect>(this.Item);
      player.buffImmune[149] = true;
      fargoSoulsPlayer.TribalCharm = true;
      fargoSoulsPlayer.TribalCharmEquipped = true;
      player.buffImmune[119] = true;
      player.buffImmune[120] = true;
      fargoSoulsPlayer.NymphsPerfumeRespawn = true;
      player.AddEffect<NymphPerfumeEffect>(this.Item);
      player.AddEffect<TimsConcoctionEffect>(this.Item);
      player.AddEffect<WyvernBalls>(this.Item);
      player.buffImmune[39] = true;
      player.buffImmune[69] = true;
      fargoSoulsPlayer.FusedLens = true;
      player.AddEffect<FusedLensInstall>(this.Item);
      player.AddEffect<GroundStickDR>(this.Item);
      player.noKnockback = true;
      if (player.onFire2)
        player.FargoSouls().AttackSpeed += 0.15f;
      if (player.ichor)
        player.GetCritChance(DamageClass.Generic) += 15f;
      player.buffImmune[70] = true;
      fargoSoulsPlayer.MagicalBulb = true;
      IceQueensCrown.AddEffects(player, this.Item);
      player.buffImmune[67] = true;
      fargoSoulsPlayer.LihzahrdTreasureBoxItem = this.Item;
      player.AddEffect<LihzahrdGroundPound>(this.Item);
      player.AddEffect<LihzahrdBoulders>(this.Item);
      player.buffImmune[144] = true;
      player.buffImmune[197] = true;
      player.buffImmune[196] = true;
      player.buffImmune[195] = true;
      fargoSoulsPlayer.BetsysHeartItem = this.Item;
      player.AddEffect<PumpkingsCapeEffect>(this.Item);
      player.AddEffect<CelestialRuneAttacks>(this.Item);
      if (fargoSoulsPlayer.AdditionalAttacksTimer > 0)
        fargoSoulsPlayer.AdditionalAttacksTimer -= 2;
      fargoSoulsPlayer.MoonChalice = true;
      player.buffImmune[164] = true;
      fargoSoulsPlayer.GravityGlobeEXItem = this.Item;
      player.AddEffect<MasoGravEffect>(this.Item);
      fargoSoulsPlayer.MasochistHeart = true;
      player.buffImmune[145] = true;
      fargoSoulsPlayer.PrecisionSeal = true;
      player.AddEffect<PrecisionSealHurtbox>(this.Item);
      player.AddEffect<DreadShellEffect>(this.Item);
      player.buffImmune[32] = true;
      player.buffImmune[47] = true;
      player.AddEffect<DeerclawpsDive>(this.Item);
      player.AddEffect<DeerclawpsEffect>(this.Item);
      player.buffImmune[ModContent.BuffType<AnticoagulationBuff>()] = true;
      player.buffImmune[ModContent.BuffType<AntisocialBuff>()] = true;
      player.buffImmune[ModContent.BuffType<AtrophiedBuff>()] = true;
      player.buffImmune[ModContent.BuffType<BerserkedBuff>()] = true;
      player.buffImmune[ModContent.BuffType<BloodthirstyBuff>()] = true;
      player.buffImmune[ModContent.BuffType<ClippedWingsBuff>()] = true;
      player.buffImmune[ModContent.BuffType<CrippledBuff>()] = true;
      player.buffImmune[ModContent.BuffType<CurseoftheMoonBuff>()] = true;
      player.buffImmune[ModContent.BuffType<DefenselessBuff>()] = true;
      player.buffImmune[ModContent.BuffType<FlamesoftheUniverseBuff>()] = true;
      player.buffImmune[ModContent.BuffType<FlippedBuff>()] = true;
      player.buffImmune[ModContent.BuffType<FlippedHallowBuff>()] = true;
      player.buffImmune[ModContent.BuffType<FusedBuff>()] = true;
      player.buffImmune[ModContent.BuffType<GuiltyBuff>()] = true;
      player.buffImmune[ModContent.BuffType<HexedBuff>()] = true;
      player.buffImmune[ModContent.BuffType<HypothermiaBuff>()] = true;
      player.buffImmune[ModContent.BuffType<InfestedBuff>()] = true;
      player.buffImmune[ModContent.BuffType<IvyVenomBuff>()] = true;
      player.buffImmune[ModContent.BuffType<JammedBuff>()] = true;
      player.buffImmune[ModContent.BuffType<LethargicBuff>()] = true;
      player.buffImmune[ModContent.BuffType<LihzahrdCurseBuff>()] = true;
      player.buffImmune[ModContent.BuffType<LightningRodBuff>()] = true;
      player.buffImmune[ModContent.BuffType<LivingWastelandBuff>()] = true;
      player.buffImmune[ModContent.BuffType<LoosePocketsBuff>()] = true;
      player.buffImmune[ModContent.BuffType<LovestruckBuff>()] = true;
      player.buffImmune[ModContent.BuffType<LowGroundBuff>()] = true;
      player.buffImmune[ModContent.BuffType<MarkedforDeathBuff>()] = true;
      player.buffImmune[ModContent.BuffType<MidasBuff>()] = true;
      player.buffImmune[ModContent.BuffType<MutantNibbleBuff>()] = true;
      player.buffImmune[ModContent.BuffType<NanoInjectionBuff>()] = true;
      player.buffImmune[ModContent.BuffType<NullificationCurseBuff>()] = true;
      player.buffImmune[ModContent.BuffType<OiledBuff>()] = true;
      player.buffImmune[ModContent.BuffType<OceanicMaulBuff>()] = true;
      player.buffImmune[ModContent.BuffType<PurifiedBuff>()] = true;
      player.buffImmune[ModContent.BuffType<ReverseManaFlowBuff>()] = true;
      player.buffImmune[ModContent.BuffType<RottingBuff>()] = true;
      player.buffImmune[ModContent.BuffType<ShadowflameBuff>()] = true;
      player.buffImmune[ModContent.BuffType<SmiteBuff>()] = true;
      player.buffImmune[ModContent.BuffType<SqueakyToyBuff>()] = true;
      player.buffImmune[ModContent.BuffType<SwarmingBuff>()] = true;
      player.buffImmune[ModContent.BuffType<StunnedBuff>()] = true;
      player.buffImmune[ModContent.BuffType<UnluckyBuff>()] = true;
      player.buffImmune[ModContent.BuffType<UnstableBuff>()] = true;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<SinisterIcon>(), 1).AddIngredient(ModContent.ItemType<SupremeDeathbringerFairy>(), 1).AddIngredient(ModContent.ItemType<BionomicCluster>(), 1).AddIngredient(ModContent.ItemType<DubiousCircuitry>(), 1).AddIngredient(ModContent.ItemType<PureHeart>(), 1).AddIngredient(ModContent.ItemType<LumpOfFlesh>(), 1).AddIngredient(ModContent.ItemType<ChaliceoftheMoon>(), 1).AddIngredient(ModContent.ItemType<HeartoftheMasochist>(), 1).AddIngredient(ModContent.ItemType<AbomEnergy>(), 15).AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 15).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
