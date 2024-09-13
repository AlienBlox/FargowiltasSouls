// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.UniverseSoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Expert;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public class UniverseSoul : BaseSoul
  {
    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(6, 7, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
    }

    public override int NumFrames => 7;

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.value = 5000000;
      this.Item.rare = -12;
      this.Item.expert = true;
      this.Item.defense = 4;
      ((Entity) this.Item).width = 5;
      ((Entity) this.Item).height = 5;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      DamageClass damageClass = player.ProcessDamageTypeFromHeldItem();
      ref StatModifier local = ref player.GetDamage(damageClass);
      local = StatModifier.op_Addition(local, 0.66f);
      player.GetCritChance(damageClass) += 25f;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.UniverseSoul = true;
      fargoSoulsPlayer.UniverseCore = true;
      player.AddEffect<UniverseSpeedEffect>(this.Item);
      player.maxMinions += 2;
      ++player.maxTurrets;
      player.AddEffect<MagmaStoneEffect>(this.Item);
      player.kbGlove = true;
      player.autoReuseGlove = true;
      player.meleeScaleGlove = true;
      player.counterWeight = 556 + Main.rand.Next(6);
      player.yoyoGlove = true;
      player.yoyoString = true;
      player.wolfAcc = true;
      player.accMerman = true;
      if (hideVisual)
      {
        player.hideMerman = true;
        player.hideWolf = true;
      }
      player.lifeRegen += 2;
      player.AddEffect<SniperScopeEffect>(this.Item);
      player.manaFlower = true;
      player.manaMagnet = true;
      player.magicCuffs = true;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient<UniverseCore>(1).AddIngredient<BerserkerSoul>(1).AddIngredient<SnipersSoul>(1).AddIngredient<ArchWizardsSoul>(1).AddIngredient<ConjuristsSoul>(1).AddIngredient<AbomEnergy>(10).AddTile<CrucibleCosmosSheet>().Register();
    }
  }
}
