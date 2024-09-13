// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.WorldShaperSoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public class WorldShaperSoul : BaseSoul
  {
    public static readonly Color ItemColor = new Color((int) byte.MaxValue, 239, 2);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.value = 750000;
      this.Item.useStyle = 4;
      this.Item.UseSound = new SoundStyle?(SoundID.Item6);
      this.Item.useTime = this.Item.useAnimation = 90;
    }

    protected override Color? nameColor => new Color?(WorldShaperSoul.ItemColor);

    public virtual bool? UseItem(Player player) => new bool?(true);

    public virtual void UseItemFrame(Player player)
    {
      if (player.itemTime != player.itemTimeMax / 2)
        return;
      player.Spawn((PlayerSpawnContext) 2);
      for (int index = 0; index < 70; ++index)
        Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 15, 0.0f, 0.0f, 150, new Color(), 1.5f);
    }

    public virtual void UpdateInventory(Player player)
    {
      player.accWatch = 3;
      player.accDepthMeter = 1;
      player.accCompass = 1;
      player.accFishFinder = true;
      player.accDreamCatcher = true;
      player.accOreFinder = true;
      player.accStopwatch = true;
      player.accCritterGuide = true;
      player.accJarOfSouls = true;
      player.accThirdEye = true;
      player.accCalendar = true;
      player.accWeatherRadio = true;
      player.chiselSpeed = true;
      player.treasureMagnet = true;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      WorldShaperSoul.AddEffects(player, this.Item, hideVisual);
    }

    public static void AddEffects(Player player, Item item, bool hideVisual)
    {
      Player player1 = player;
      player.FargoSouls().WorldShaperSoul = true;
      MinerEnchant.AddEffects(player1, 0.66f, item);
      player1.tileSpeed += 0.5f;
      player1.wallSpeed += 0.5f;
      if (((Entity) player1).whoAmI == Main.myPlayer)
      {
        Player.tileRangeX += 10;
        Player.tileRangeY += 10;
      }
      player1.autoPaint = true;
      player1.autoActuator = true;
      player1.npcTypeNoAggro[1] = true;
      player1.npcTypeNoAggro[16] = true;
      player1.npcTypeNoAggro[59] = true;
      player1.npcTypeNoAggro[71] = true;
      player1.npcTypeNoAggro[81] = true;
      player1.npcTypeNoAggro[138] = true;
      player1.npcTypeNoAggro[121] = true;
      player1.npcTypeNoAggro[122] = true;
      player1.npcTypeNoAggro[141] = true;
      player1.npcTypeNoAggro[147] = true;
      player1.npcTypeNoAggro[183] = true;
      player1.npcTypeNoAggro[184] = true;
      player1.npcTypeNoAggro[204] = true;
      player1.npcTypeNoAggro[225] = true;
      player1.npcTypeNoAggro[244] = true;
      player1.npcTypeNoAggro[302] = true;
      player1.npcTypeNoAggro[333] = true;
      player1.npcTypeNoAggro[335] = true;
      player1.npcTypeNoAggro[334] = true;
      player1.npcTypeNoAggro[336] = true;
      player1.npcTypeNoAggro[537] = true;
      player.AddEffect<BuilderEffect>(item);
      player1.accWatch = 3;
      player1.accDepthMeter = 1;
      player1.accCompass = 1;
      player1.accFishFinder = true;
      player1.accDreamCatcher = true;
      player1.accOreFinder = true;
      player1.accStopwatch = true;
      player1.accCritterGuide = true;
      player1.accJarOfSouls = true;
      player1.accThirdEye = true;
      player1.accCalendar = true;
      player1.accWeatherRadio = true;
      player1.chiselSpeed = true;
      player1.treasureMagnet = true;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "MinerEnchant", 1).AddIngredient(407, 1).AddIngredient(1923, 1).AddIngredient(5126, 1).AddIngredient(3624, 1).AddIngredient(2799, 1).AddIngredient(3090, 1).AddRecipeGroup("FargowiltasSouls:AnyShellphone", 1).AddRecipeGroup("FargowiltasSouls:AnyDrax", 1).AddIngredient(2176, 1).AddIngredient(2768, 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
