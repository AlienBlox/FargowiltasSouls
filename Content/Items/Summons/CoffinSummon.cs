// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Summons.CoffinSummon
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Summons
{
  public class CoffinSummon : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 5;
    }

    public virtual bool IsLoadingEnabled(Mod mod) => false;

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 32;
      ((Entity) this.Item).height = 32;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
      this.Item.rare = 3;
      this.Item.consumable = true;
      this.Item.maxStack = 20;
      this.Item.noUseGraphic = false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(133, 15).AddIngredient(3380, 8).AddIngredient(177, 2).AddTile(26).Register();
    }

    public virtual bool CanUseItem(Player Player)
    {
      return Player.ZoneDesert && (Player.ZoneDirtLayerHeight || Player.ZoneRockLayerHeight) && !NPC.AnyNPCs(ModContent.NPCType<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin>());
    }

    public virtual bool? UseItem(Player Player)
    {
      NPC.SpawnOnPlayer(((Entity) Player).whoAmI, ModContent.NPCType<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin>());
      SoundEngine.PlaySound(ref SoundID.Shatter, new Vector2?(((Entity) Player).Center), (SoundUpdateCallback) null);
      return new bool?(true);
    }
  }
}
