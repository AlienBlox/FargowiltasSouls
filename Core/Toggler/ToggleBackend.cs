// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Toggler.ToggleBackend
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items.Consumables;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.IO;
using Terraria.Localization;

#nullable disable
namespace FargowiltasSouls.Core.Toggler
{
  public class ToggleBackend
  {
    public static readonly string ConfigPath = Path.Combine(Main.SavePath, "ModConfigs", "FargowiltasSouls_Toggles.json");
    public Preferences Config;
    public Dictionary<AccessoryEffect, Toggle> Toggles = new Dictionary<AccessoryEffect, Toggle>();
    public bool CanPlayMaso;
    public const int CustomPresetCount = 3;
    public List<AccessoryEffect>[] CustomPresets = new List<AccessoryEffect>[3];
    public bool Initialized;

    public void TryLoad()
    {
      if (this.Initialized)
        return;
      this.Initialized = true;
      this.Config = new Preferences(ToggleBackend.ConfigPath, false, false);
      this.Toggles = ToggleLoader.LoadedToggles;
      if (!Main.dedServ && !this.Config.Load())
        this.Save();
      this.CanPlayMaso = this.Config.Get<bool>("CanPlayMaso", false);
      for (int index = 0; index < this.CustomPresets.Length; ++index)
      {
        Preferences config = this.Config;
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 1);
        interpolatedStringHandler.AppendLiteral("CustomPresetsOff");
        interpolatedStringHandler.AppendFormatted<int>(index + 1);
        string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        Dictionary<string, bool> dictionary = config.Get<Dictionary<string, bool>>(stringAndClear, (Dictionary<string, bool>) null);
        if (dictionary != null)
        {
          List<AccessoryEffect> accessoryEffectList = new List<AccessoryEffect>();
          foreach (AccessoryEffect accessoryEffect in ToggleLoader.LoadedToggles.Keys.ToList<AccessoryEffect>())
          {
            if (dictionary.ContainsKey(accessoryEffect.Name))
              accessoryEffectList.Add(accessoryEffect);
          }
          this.CustomPresets[index] = accessoryEffectList;
        }
      }
    }

    public void Save()
    {
      if (!this.Initialized || Main.dedServ)
        return;
      this.Config.Put("CanPlayMaso", (object) this.CanPlayMaso);
      for (int index = 0; index < this.CustomPresets.Length; ++index)
      {
        if (this.CustomPresets[index] != null)
        {
          Dictionary<string, bool> dictionary1 = new Dictionary<string, bool>(this.CustomPresets.Length);
          foreach (AccessoryEffect accessoryEffect in this.CustomPresets[index])
            dictionary1[accessoryEffect.Name] = false;
          Preferences config = this.Config;
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 1);
          interpolatedStringHandler.AppendLiteral("CustomPresetsOff");
          interpolatedStringHandler.AppendFormatted<int>(index + 1);
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          Dictionary<string, bool> dictionary2 = dictionary1;
          config.Put(stringAndClear, (object) dictionary2);
        }
      }
      this.Config.Save(true);
    }

    public void LoadPlayerToggles(FargoSoulsPlayer modPlayer)
    {
      if (!this.Initialized)
        return;
      this.Toggles = ToggleLoader.LoadedToggles;
      this.SetAll(true);
      foreach (AccessoryEffect disabledToggle in modPlayer.disabledToggles)
        Main.LocalPlayer.SetToggleValue(disabledToggle, false);
      foreach (KeyValuePair<AccessoryEffect, Toggle> toggle in this.Toggles)
        modPlayer.TogglesToSync[toggle.Key] = toggle.Value.ToggleBool;
    }

    public void SetAll(bool value)
    {
      foreach (Toggle toggle in this.Toggles.Values)
        Main.LocalPlayer.SetToggleValue(toggle.Effect, value);
    }

    public void SomeEffects()
    {
      Player localPlayer = Main.LocalPlayer;
      this.SetAll(true);
      localPlayer.SetToggleValue<BorealEffect>(false);
      localPlayer.SetToggleValue<ShadewoodEffect>(false);
      localPlayer.SetToggleValue<PearlwoodEffect>(false);
      localPlayer.SetToggleValue<CobaltEffect>(false);
      localPlayer.SetToggleValue<AncientCobaltEffect>(false);
      localPlayer.SetToggleValue<ObsidianProcEffect>(false);
      localPlayer.SetToggleValue<CopperEffect>(false);
      localPlayer.SetToggleValue<AshWoodFireballs>(false);
      localPlayer.SetToggleValue<GladiatorSpears>(false);
      localPlayer.SetToggleValue<RedRidingEffect>(false);
      localPlayer.SetToggleValue<BeeEffect>(false);
      localPlayer.SetToggleValue<CactusEffect>(false);
      localPlayer.SetToggleValue<PumpkinEffect>(false);
      localPlayer.SetToggleValue<ChloroMinion>(false);
      localPlayer.SetToggleValue<RainUmbrellaEffect>(false);
      localPlayer.SetToggleValue<RainInnerTubeEffect>(false);
      localPlayer.SetToggleValue<JungleJump>(false);
      localPlayer.SetToggleValue<MoltenEffect>(false);
      localPlayer.SetToggleValue<ShroomiteShroomEffect>(false);
      localPlayer.SetToggleValue<DarkArtistMinion>(false);
      localPlayer.SetToggleValue<NecroEffect>(false);
      localPlayer.SetToggleValue<ShadowBalls>(false);
      localPlayer.SetToggleValue<SpookyEffect>(false);
      localPlayer.SetToggleValue<FossilBones>(false);
      localPlayer.SetToggleValue<FossilBones>(false);
      localPlayer.SetToggleValue<AncientHallowMinion>(false);
      localPlayer.SetToggleValue<SpectreEffect>(false);
      localPlayer.SetToggleValue<MeteorEffect>(false);
      localPlayer.SetToggleValue<ZephyrJump>(false);
      localPlayer.SetToggleValue<DevianttHearts>(false);
      localPlayer.SetToggleValue<MasoAeolusFlower>(false);
      localPlayer.SetToggleValue<SlimyShieldEffect>(false);
      localPlayer.SetToggleValue<AgitatingLensEffect>(false);
      localPlayer.SetToggleValue<SkeleMinionEffect>(false);
      localPlayer.SetToggleValue<MasoCarrotEffect>(false);
      localPlayer.SetToggleValue<RainbowSlimeMinion>(false);
      localPlayer.SetToggleValue<NymphPerfumeEffect>(false);
      localPlayer.SetToggleValue<WretchedPouchEffect>(false);
      localPlayer.SetToggleValue<ProbeMinionEffect>(false);
      localPlayer.SetToggleValue<GelicWingSpikes>(false);
      localPlayer.SetToggleValue<PungentMinion>(false);
      localPlayer.SetToggleValue<DeerclawpsEffect>(false);
      localPlayer.SetToggleValue<LihzahrdBoulders>(false);
      localPlayer.SetToggleValue<PlantMinionEffect>(false);
      localPlayer.SetToggleValue<CelestialRuneAttacks>(false);
      localPlayer.SetToggleValue<UfoMinionEffect>(false);
      localPlayer.SetToggleValue<MasoTrueEyeMinion>(false);
      localPlayer.SetToggleValue<MasoAbom>(false);
      localPlayer.SetToggleValue<MasoRing>(false);
      localPlayer.SetToggleValue<MagmaStoneEffect>(false);
      localPlayer.SetToggleValue<SniperScopeEffect>(false);
      localPlayer.SetToggleValue<BuilderEffect>(false);
      localPlayer.SetToggleValue<DefenseStarEffect>(false);
      localPlayer.SetToggleValue<DefenseBeeEffect>(false);
      localPlayer.SetToggleValue<SupersonicClimbing>(false);
      localPlayer.SetToggleValue<SupersonicSpeedEffect>(false);
      localPlayer.SetToggleValue<TrawlerSporeSac>(false);
      foreach (Toggle toggle in this.Toggles.Values.Where<Toggle>((Func<Toggle, bool>) (toggle => toggle.Effect.Name.Contains("Pet"))))
        localPlayer.SetToggleValue(toggle.Effect, false);
    }

    public void MinimalEffects()
    {
      Player localPlayer = Main.LocalPlayer;
      this.SetAll(false);
      localPlayer.SetToggleValue<MythrilEffect>(true);
      localPlayer.SetToggleValue<PalladiumEffect>(true);
      localPlayer.SetToggleValue<IronEffect>(true);
      localPlayer.SetToggleValue<CthulhuShield>(true);
      localPlayer.SetToggleValue<BeetleEffect>(true);
      localPlayer.SetToggleValue<SpiderEffect>(true);
      localPlayer.SetToggleValue<GoldToPiggy>(true);
      localPlayer.SetToggleValue<JungleDashEffect>(true);
      localPlayer.SetToggleValue<SupersonicTabi>(true);
      localPlayer.SetToggleValue<ValhallaDash>(true);
      localPlayer.SetToggleValue<SquireMountJump>(true);
      localPlayer.SetToggleValue<SquireMountSpeed>(true);
      localPlayer.SetToggleValue<NebulaEffect>(true);
      localPlayer.SetToggleValue<SolarEffect>(true);
      localPlayer.SetToggleValue<HuntressEffect>(true);
      localPlayer.SetToggleValue<CrystalAssassinDash>(true);
      localPlayer.SetToggleValue<GladiatorBanner>(true);
      localPlayer.SetToggleValue<EternityTin>(true);
      localPlayer.SetToggleValue<DeerSinewEffect>(true);
      localPlayer.SetToggleValue<MasoGraze>(true);
      localPlayer.SetToggleValue<SinisterIconDropsEffect>(true);
      localPlayer.SetToggleValue<RainbowHealEffect>(true);
      localPlayer.SetToggleValue<TribalCharmClickBonus>(true);
      localPlayer.SetToggleValue<StabilizedGravity>(true);
      localPlayer.SetToggleValue<PrecisionSealHurtbox>(true);
      localPlayer.SetToggleValue<AgitatingLensInstall>(true);
      localPlayer.SetToggleValue<FusedLensInstall>(true);
      localPlayer.SetToggleValue<MiningHunt>(true);
      localPlayer.SetToggleValue<MiningDanger>(true);
      localPlayer.SetToggleValue<MiningSpelunk>(true);
      localPlayer.SetToggleValue<MiningShine>(true);
      localPlayer.SetToggleValue<RunSpeed>(true);
      localPlayer.SetToggleValue<SupersonicRocketBoots>(true);
      localPlayer.SetToggleValue<NoMomentum>(true);
      localPlayer.SetToggleValue<MeteorMomentumEffect>(true);
      localPlayer.SetToggleValue<FlightMasteryInsignia>(true);
      localPlayer.SetToggleValue<FlightMasteryGravity>(true);
      localPlayer.SetToggleValue<UniverseSpeedEffect>(true);
      localPlayer.SetToggleValue<PaladinShieldEffect>(true);
      localPlayer.SetToggleValue<ShimmerImmunityEffect>(true);
      localPlayer.SetToggleValue<MasoAeolusFrog>(true);
      localPlayer.SetToggleValue<TimsConcoctionEffect>(true);
    }

    public void SaveCustomPreset(int slot)
    {
      List<AccessoryEffect> accessoryEffectList = new List<AccessoryEffect>();
      foreach (KeyValuePair<AccessoryEffect, Toggle> toggle in this.Toggles)
      {
        if (!this.Toggles[toggle.Key].ToggleBool)
          accessoryEffectList.Add(toggle.Key);
      }
      if (Main.dedServ)
        return;
      this.CustomPresets[slot - 1] = accessoryEffectList;
      Main.NewText((object) Language.GetTextValue("Mods.FargowiltasSouls.UI.SavedToSlot", (object) slot), new Color?(Color.Yellow));
    }

    public void LoadCustomPreset(int slot)
    {
      List<AccessoryEffect> customPreset = this.CustomPresets[slot - 1];
      if (customPreset == null)
      {
        Main.NewText((object) Language.GetTextValue("Mods.FargowiltasSouls.UI.NoTogglesFound", (object) slot), new Color?(Color.Yellow));
      }
      else
      {
        FargoSoulsPlayer modPlayer = Main.LocalPlayer.FargoSouls();
        modPlayer.disabledToggles = new List<AccessoryEffect>((IEnumerable<AccessoryEffect>) customPreset);
        this.LoadPlayerToggles(modPlayer);
        modPlayer.disabledToggles.Clear();
      }
    }
  }
}
