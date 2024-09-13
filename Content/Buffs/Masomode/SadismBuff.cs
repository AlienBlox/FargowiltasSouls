// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.SadismBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class SadismBuff : ModBuff
  {
    public virtual string Texture => "FargowiltasSouls/Content/Buffs/PlaceholderBuff";

    public virtual void SetStaticDefaults() => BuffID.Sets.IsATagBuff[this.Type] = true;

    public virtual void Update(Player player, ref int buffIndex)
    {
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
      player.buffImmune[ModContent.BuffType<GodEaterBuff>()] = true;
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

    public virtual void Update(NPC npc, ref int buffIndex)
    {
      FargoSoulsGlobalNPC fargoSoulsGlobalNpc = npc.FargoSouls();
      npc.ichor = true;
      npc.betsysCurse = true;
      npc.midas = true;
      fargoSoulsGlobalNpc.OceanicMaul = true;
      fargoSoulsGlobalNpc.CurseoftheMoon = true;
      fargoSoulsGlobalNpc.Rotting = true;
      fargoSoulsGlobalNpc.MutantNibble = true;
      fargoSoulsGlobalNpc.Sadism = true;
    }
  }
}
