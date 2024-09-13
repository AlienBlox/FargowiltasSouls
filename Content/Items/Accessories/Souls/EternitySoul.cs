// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.EternitySoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  [AutoloadEquip]
  public class EternitySoul : FlightMasteryWings
  {
    public override bool HasSupersonicSpeed => true;

    public override bool Eternity => true;

    public override int NumFrames => 10;

    public static int WingSlotID { get; private set; }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      EternitySoul.WingSlotID = this.Item.wingSlot;
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(6, 10, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
    }

    public override void SafeModifyTooltips(List<TooltipLine> tooltips)
    {
      if (this.Item.social)
        return;
      string str1 = Language.GetTextValue("Mods.FargowiltasSouls.Items.EternitySoul.Extra.Additional") + "                                                                                                                                               ";
      if (Main.GameUpdateCount % 5U == 0U || EternitySoulSystem.TooltipLines == null)
      {
        EternitySoulSystem.TooltipLines = new List<string>();
        for (int index = 0; index < 7; ++index)
        {
          string str2 = Utils.NextFromCollection<string>(Main.rand, EternitySoulSystem.Tooltips);
          if (EternitySoulSystem.TooltipLines.Contains(str2))
            --index;
          else
            EternitySoulSystem.TooltipLines.Add(str2);
        }
      }
      for (int index = 0; index < EternitySoulSystem.TooltipLines.Count; ++index)
        str1 = str1 + "\n" + EternitySoulSystem.TooltipLines[index];
      tooltips.Add(new TooltipLine(((ModType) this).Mod, "tooltip", str1));
      tooltips.Add(new TooltipLine(((ModType) this).Mod, "FlavorText", Language.GetTextValue("Mods.FargowiltasSouls.Items.EternitySoul.Extra.Flavor")));
    }

    public virtual bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
    {
      if ((!(((TooltipLine) line).Mod == "Terraria") || !(((TooltipLine) line).Name == "ItemName")) && !(((TooltipLine) line).Name == "FlavorText"))
        return true;
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 1, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
      shader.TrySetParameter("mainColor", (object) new Color(42, 42, 99));
      shader.TrySetParameter("secondaryColor", (object) FargowiltasSouls.FargowiltasSouls.EModeColor());
      shader.Apply("PulseUpwards");
      Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2((float) line.X, (float) line.Y), Color.White, 1f, 0.0f, 0.0f, -1);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
      return false;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 10;
      this.Item.value = 200000000;
      this.Item.shieldSlot = 5;
      this.Item.defense = 100;
      this.Item.useStyle = 4;
      this.Item.useTime = 180;
      this.Item.useAnimation = 180;
      this.Item.UseSound = new SoundStyle?(SoundID.Item6);
    }

    public virtual void UseItemFrame(Player player) => SandsofTime.Use(player);

    public virtual bool? UseItem(Player player) => new bool?(true);

    private void PassiveEffect(Player player)
    {
      BionomicCluster.PassiveEffect(player, this.Item);
      AshWoodEnchant.PassiveEffect(player);
      player.FargoSouls().CanAmmoCycle = true;
      player.FargoSouls().WoodEnchantDiscount = true;
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
    }

    public virtual void UpdateInventory(Player player) => this.PassiveEffect(player);

    public virtual void UpdateVanity(Player player) => this.PassiveEffect(player);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      this.PassiveEffect(player);
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.Eternity = true;
      player.AddEffect<EternityTin>(this.Item);
      fargoSoulsPlayer.UniverseSoul = true;
      fargoSoulsPlayer.UniverseCore = true;
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 2.5f);
      player.AddEffect<UniverseSpeedEffect>(this.Item);
      player.maxMinions += 20;
      player.maxTurrets += 10;
      player.counterWeight = 556 + Main.rand.Next(6);
      player.yoyoGlove = true;
      player.yoyoString = true;
      player.AddEffect<SniperScopeEffect>(this.Item);
      player.manaFlower = true;
      player.manaMagnet = true;
      player.magicCuffs = true;
      player.manaCost -= 0.5f;
      player.statLifeMax2 *= 5;
      player.buffImmune[88] = true;
      ColossusSoul.AddEffects(player, this.Item, 0, 0.4f, 15);
      SupersonicSoul.AddEffects(player, this.Item, hideVisual);
      FlightMasterySoul.AddEffects(player, this.Item);
      TrawlerSoul.AddEffects(player, this.Item, hideVisual);
      WorldShaperSoul.AddEffects(player, this.Item, hideVisual);
      ((ModItem) ModContent.GetInstance<TerrariaSoul>()).UpdateAccessory(player, hideVisual);
      ((ModItem) ModContent.GetInstance<MasochistSoul>()).UpdateAccessory(player, hideVisual);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "UniverseSoul", 1).AddIngredient((Mod) null, "DimensionSoul", 1).AddIngredient((Mod) null, "TerrariaSoul", 1).AddIngredient((Mod) null, "MasochistSoul", 1).AddIngredient((Mod) null, "EternalEnergy", 30).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
