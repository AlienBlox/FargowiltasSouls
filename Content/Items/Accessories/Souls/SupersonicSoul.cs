// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.SupersonicSoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public class SupersonicSoul : BaseSoul
  {
    public static readonly Color ItemColor = new Color(238, 0, 69);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.value = 750000;
    }

    protected override Color? nameColor => new Color?(SupersonicSoul.ItemColor);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.FargoSouls();
      SupersonicSoul.AddEffects(player, this.Item, hideVisual);
    }

    public static void AddEffects(Player player, Item item, bool hideVisual)
    {
      Player player1 = player;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      player.AddEffect<MasoAeolusFrog>(item);
      player.AddEffect<MasoAeolusFlower>(item);
      player.AddEffect<ZephyrJump>(item);
      fargoSoulsPlayer.SupersonicSoul = true;
      if (player1.AddEffect<SupersonicSpeedEffect>(item) && !fargoSoulsPlayer.noSupersonic && !Luminance.Common.Utilities.Utilities.AnyBosses())
      {
        player1.runAcceleration += 0.5f;
        player1.maxRunSpeed += 10f;
        if (player.HasEffect<MasoAeolusFrog>())
          player1.autoJump = true;
        player1.jumpSpeedBoost += 2.4f;
        player1.maxFallSpeed += 5f;
        player1.jumpBoost = true;
      }
      else
        player1.accRunSpeed = player.AddEffect<RunSpeed>(item) ? 15.6f : 6.75f;
      if (player.AddEffect<NoMomentum>(item))
        fargoSoulsPlayer.NoMomentum = true;
      player1.moveSpeed += 0.5f;
      if (player.AddEffect<SupersonicRocketBoots>(item))
      {
        player1.rocketBoots = player1.vanityRocketBoots = 4;
        player1.rocketTimeMax = 10;
      }
      player1.iceSkate = true;
      player1.waterWalk = true;
      player1.fireWalk = true;
      player1.lavaImmune = true;
      player1.noFallDmg = true;
      if (player1.AddEffect<SupersonicJumps>(item) && (double) player1.wingTime == 0.0)
      {
        ((ExtraJumpState) ref player1.GetJumpState<ExtraJump>(ExtraJump.CloudInABottle)).Enable();
        ((ExtraJumpState) ref player1.GetJumpState<ExtraJump>(ExtraJump.SandstormInABottle)).Enable();
        ((ExtraJumpState) ref player1.GetJumpState<ExtraJump>(ExtraJump.BlizzardInABottle)).Enable();
        ((ExtraJumpState) ref player1.GetJumpState<ExtraJump>(ExtraJump.FartInAJar)).Enable();
      }
      if (((Entity) player1).whoAmI == Main.myPlayer && player1.AddEffect<SupersonicCarpet>(item))
      {
        player1.carpet = true;
        if (Main.netMode == 1)
          NetMessage.SendData(4, -1, -1, (NetworkText) null, ((Entity) player1).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        if (player1.canCarpet)
          fargoSoulsPlayer.extraCarpetDuration = true;
        else if (fargoSoulsPlayer.extraCarpetDuration)
        {
          fargoSoulsPlayer.extraCarpetDuration = false;
          player1.carpetTime = 1000;
        }
      }
      if (player1.AddEffect<CthulhuShield>(item))
        player1.dashType = 2;
      if (player1.AddEffect<SupersonicTabi>(item))
        player1.dashType = 1;
      if (player1.AddEffect<BlackBelt>(item))
        player1.blackBelt = true;
      if (player1.AddEffect<BlackBelt>(item))
        player1.spikedBoots = 2;
      if (player1.HasEffect<DefenseBeeEffect>() || player1.AddEffect<DefenseBeeEffect>(item))
        player1.honeyCombItem = item;
      if (!player1.AddEffect<SupersonicPanic>(item))
        return;
      player1.panic = true;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<AeolusBoots>(), 1).AddIngredient(934, 1).AddIngredient(1578, 1).AddIngredient(3251, 1).AddIngredient(5331, 1).AddIngredient(3097, 1).AddIngredient(984, 1).AddIngredient(3353, 1).AddIngredient(3260, 1).AddIngredient(3771, 1).AddIngredient(1914, 1).AddIngredient(2771, 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
