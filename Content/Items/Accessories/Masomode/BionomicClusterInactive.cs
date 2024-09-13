// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.BionomicClusterInactive
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Consumables;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class BionomicClusterInactive : SoulsItem
  {
    public override bool Eternity => true;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 0;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = 8;
      this.Item.value = Item.sellPrice(0, 6, 0, 0);
      this.Item.defense = 6;
      this.Item.useTime = 180;
      this.Item.useAnimation = 180;
      this.Item.useStyle = 4;
      this.Item.useTurn = true;
      this.Item.UseSound = new SoundStyle?(SoundID.Item6);
    }

    public static void PassiveEffect(Player player, Item item)
    {
      player.buffImmune[194] = true;
      player.buffImmune[68] = true;
      player.buffImmune[46] = true;
      player.buffImmune[ModContent.BuffType<GuiltyBuff>()] = true;
      player.buffImmune[ModContent.BuffType<LoosePocketsBuff>()] = true;
      player.nightVision = true;
      player.manaMagnet = true;
      player.manaFlower = true;
      player.AddEffect<MasoCarrotEffect>(item);
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.SandsofTime = true;
      fargoSoulsPlayer.SecurityWallet = true;
      fargoSoulsPlayer.TribalCharm = true;
      fargoSoulsPlayer.NymphsPerfumeRespawn = true;
      fargoSoulsPlayer.ConcentratedRainbowMatter = true;
      player.AddEffect<RainbowHealEffect>(item);
      fargoSoulsPlayer.FrigidGemstoneItem = item;
      player.AddEffect<StabilizedGravity>(item);
    }

    public virtual void UpdateInventory(Player player)
    {
    }

    public virtual void UpdateVanity(Player player)
    {
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      BionomicClusterInactive.PassiveEffect(player, this.Item);
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      player.buffImmune[ModContent.BuffType<FlamesoftheUniverseBuff>()] = true;
      player.AddEffect<RainbowSlimeMinion>(this.Item);
      player.buffImmune[ModContent.BuffType<ClippedWingsBuff>()] = true;
      player.buffImmune[ModContent.BuffType<CrippledBuff>()] = true;
      player.AddEffect<ClippedEffect>(this.Item);
      player.buffImmune[44] = true;
      player.buffImmune[153] = true;
      player.buffImmune[ModContent.BuffType<ShadowflameBuff>()] = true;
      player.AddEffect<WretchedPouchEffect>(this.Item);
      player.buffImmune[194] = true;
      fargoSoulsPlayer.SandsofTime = true;
      player.buffImmune[ModContent.BuffType<SqueakyToyBuff>()] = true;
      player.buffImmune[ModContent.BuffType<GuiltyBuff>()] = true;
      player.AddEffect<SqueakEffect>(this.Item);
      player.buffImmune[149] = true;
      player.buffImmune[ModContent.BuffType<PurifiedBuff>()] = true;
      fargoSoulsPlayer.TribalCharm = true;
      fargoSoulsPlayer.TribalCharmEquipped = true;
      player.AddEffect<TribalCharmClickBonus>(this.Item);
      player.buffImmune[68] = true;
      player.manaMagnet = true;
      player.manaFlower = true;
      player.buffImmune[ModContent.BuffType<MidasBuff>()] = true;
      fargoSoulsPlayer.SecurityWallet = true;
      player.nightVision = true;
      player.AddEffect<MasoCarrotEffect>(this.Item);
      player.buffImmune[119] = true;
      player.buffImmune[ModContent.BuffType<LovestruckBuff>()] = true;
      player.buffImmune[ModContent.BuffType<HexedBuff>()] = true;
      player.buffImmune[120] = true;
      fargoSoulsPlayer.NymphsPerfumeRespawn = true;
      player.AddEffect<NymphPerfumeEffect>(this.Item);
      player.AddEffect<TimsConcoctionEffect>(this.Item);
    }

    public virtual void UseItemFrame(Player player) => SandsofTime.Use(player);

    public virtual bool? UseItem(Player player) => new bool?(true);

    public virtual bool CanRightClick() => true;

    public virtual void RightClick(Player player)
    {
      player.ReplaceItem(this.Item, ModContent.ItemType<BionomicCluster>());
    }
  }
}
