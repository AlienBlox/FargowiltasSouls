// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Summons.AbominationnVoodooDoll
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Summons
{
  public class AbominationnVoodooDoll : SoulsItem
  {
    private bool hasDeclaredTeleport;

    public virtual void SetStaticDefaults()
    {
      ItemID.Sets.SortingPriorityBossSpawns[this.Type] = 12;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.rare = 11;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
      this.Item.maxStack = 20;
      this.Item.value = Item.sellPrice(0, 1, 0, 0);
    }

    public virtual bool CanUseItem(Player player)
    {
      ModNPC modNpc;
      return ModContent.TryFind<ModNPC>("Fargowiltas", "Abominationn", ref modNpc) && !NPC.AnyNPCs(modNpc.Type);
    }

    public virtual bool? UseItem(Player player)
    {
      ModNPC modNpc;
      if (ModContent.TryFind<ModNPC>("Fargowiltas", "Abominationn", ref modNpc))
        NPC.SpawnOnPlayer(((Entity) player).whoAmI, modNpc.Type);
      return new bool?(true);
    }

    public virtual void Update(ref float gravity, ref float maxFallSpeed)
    {
      ModNPC modNpc;
      if (!((Entity) this.Item).lavaWet || !FargoSoulsUtil.HostCheck || !ModContent.TryFind<ModNPC>("Fargowiltas", "Abominationn", ref modNpc))
        return;
      int closest = (int) Player.FindClosest(((Entity) this.Item).Center, 0, 0);
      NPC npc = FargoSoulsUtil.NPCExists(NPC.FindFirstNPC(modNpc.Type), Array.Empty<int>());
      if (closest == -1)
        return;
      if ((double) ((Entity) Main.player[closest]).Center.Y / 16.0 > Main.worldSurface)
      {
        if (!this.hasDeclaredTeleport)
        {
          this.hasDeclaredTeleport = true;
          FargoSoulsUtil.PrintLocalization("Mods.FargowiltasSouls.Items.AbominationnVoodooDoll.Fail", new Color(175, 75, (int) byte.MaxValue));
        }
        ((Entity) this.Item).Center = ((Entity) Main.player[closest]).Center;
        this.Item.noGrabDelay = 0;
      }
      else
      {
        if (npc == null)
          return;
        npc.life = 0;
        npc.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
        FargoSoulsUtil.SpawnBossNetcoded(Main.player[closest], ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>(), false);
        ((Entity) this.Item).active = false;
        this.Item.type = 0;
        this.Item.stack = 0;
      }
    }

    public virtual void UpdateInventory(Player player) => this.hasDeclaredTeleport = false;

    public override void SafeModifyTooltips(List<TooltipLine> tooltips)
    {
      TooltipLine tooltipLine;
      if (!tooltips.TryFindTooltipLine("ItemName", out tooltipLine))
        return;
      tooltipLine.OverrideColor = new Color?(new Color(Main.DiscoR, 51, (int) byte.MaxValue - (int) ((double) Main.DiscoR * 0.4)));
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(5).AddIngredient(ModContent.ItemType<AbomEnergy>(), 5).AddIngredient(267, 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
