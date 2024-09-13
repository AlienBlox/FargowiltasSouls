// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Summons.MutantsCurse
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Summons
{
  public class MutantsCurse : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(3, 11, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
      ItemID.Sets.ItemNoGravity[this.Item.type] = true;
      ItemID.Sets.SortingPriorityBossSpawns[this.Type] = 12;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
    }

    public override int NumFrames => 11;

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 52;
      ((Entity) this.Item).height = 52;
      this.Item.rare = 11;
      this.Item.maxStack = 20;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
      this.Item.consumable = true;
      this.Item.value = Item.buyPrice(1, 0, 0, 0);
    }

    public virtual bool CanUseItem(Player player)
    {
      return (double) ((Entity) player).Center.Y / 16.0 < Main.worldSurface;
    }

    public virtual bool? UseItem(Player player)
    {
      FargoSoulsUtil.SpawnBossNetcoded(player, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>());
      return new bool?(true);
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);
  }
}
