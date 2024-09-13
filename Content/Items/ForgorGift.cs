// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.ForgorGift
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;

#nullable disable
namespace FargowiltasSouls.Content.Items
{
  public class ForgorGift : SoulsItem
  {
    public virtual string Texture => "FargowiltasSouls/Content/Items/Placeholder";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 1;
      this.Item.rare = 1;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
      this.Item.consumable = false;
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool CanRightClick() => true;

    public virtual void RightClick(Player player)
    {
      base.RightClick(player);
      WorldSavingSystem.SkipMutantP1 = 0;
      FargoSoulsUtil.PrintLocalization("Mods.FargowiltasSouls.Items.ForgorGift.Message", Color.White);
      if (Main.netMode != 2)
        return;
      NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }

    public virtual bool? UseItem(Player player)
    {
      if (player.altFunctionUse == 2)
      {
        FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
        fargoSoulsPlayer.MutantsPactSlot = false;
        fargoSoulsPlayer.MutantsDiscountCard = false;
        fargoSoulsPlayer.MutantsCreditCard = false;
        fargoSoulsPlayer.ReceivedMasoGift = false;
        fargoSoulsPlayer.RabiesVaccine = false;
        fargoSoulsPlayer.DeerSinew = false;
        fargoSoulsPlayer.HasClickedWrench = false;
      }
      else
      {
        Main.hardMode = false;
        NPC.downedAncientCultist = false;
        NPC.downedBoss1 = false;
        NPC.downedBoss2 = false;
        NPC.downedBoss3 = false;
        NPC.downedChristmasIceQueen = false;
        NPC.downedChristmasSantank = false;
        NPC.downedChristmasTree = false;
        NPC.downedClown = false;
        NPC.downedDeerclops = false;
        NPC.downedEmpressOfLight = false;
        NPC.downedFishron = false;
        NPC.downedFrost = false;
        NPC.downedGoblins = false;
        NPC.downedGolemBoss = false;
        NPC.downedHalloweenKing = false;
        NPC.downedHalloweenTree = false;
        NPC.downedMartians = false;
        NPC.downedMechBoss1 = false;
        NPC.downedMechBoss2 = false;
        NPC.downedMechBoss3 = false;
        NPC.downedMechBossAny = false;
        NPC.downedMoonlord = false;
        NPC.downedPirates = false;
        NPC.downedPlantBoss = false;
        NPC.downedQueenBee = false;
        NPC.downedQueenSlime = false;
        NPC.downedSlimeKing = false;
        NPC.downedTowerNebula = false;
        NPC.downedTowerSolar = false;
        NPC.downedTowerStardust = false;
        NPC.downedTowerVortex = false;
        WorldSavingSystem.DownedAbom = false;
        WorldSavingSystem.DownedBetsy = false;
        WorldSavingSystem.DownedDevi = false;
        WorldSavingSystem.DownedFishronEX = false;
        WorldSavingSystem.DownedMutant = false;
        for (int index = 0; index < WorldSavingSystem.DownedBoss.Length; ++index)
          WorldSavingSystem.DownedBoss[index] = false;
      }
      FargoSoulsUtil.PrintLocalization("Mods.FargowiltasSouls.Items.ForgorGift.Message", Color.White);
      if (Main.netMode == 2)
        NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      return new bool?(true);
    }
  }
}
