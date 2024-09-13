// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Common.Utilities.EModeUtils
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Common.Utilities
{
  public static class EModeUtils
  {
    public static void DropSummon(
      NPC npc,
      int itemType,
      bool downed,
      ref bool droppedSummonFlag,
      bool prerequisite = true)
    {
      if (!(WorldSavingSystem.EternityMode & prerequisite) || downed || !FargoSoulsUtil.HostCheck || !npc.HasPlayerTarget || droppedSummonFlag)
        return;
      Player player = Main.player[npc.target];
      Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) player).Hitbox, itemType, 1, false, 0, false, false);
      droppedSummonFlag = true;
    }

    public static void DropSummon(
      NPC npc,
      string itemName,
      bool downed,
      ref bool droppedSummonFlag,
      bool prerequisite = true)
    {
      if (!(WorldSavingSystem.EternityMode & prerequisite) || downed || !FargoSoulsUtil.HostCheck || !npc.HasPlayerTarget || droppedSummonFlag)
        return;
      Player player = Main.player[npc.target];
      ModItem modItem;
      if (ModContent.TryFind<ModItem>("Fargowiltas", itemName, ref modItem))
        Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) player).Hitbox, modItem.Type, 1, false, 0, false, false);
      droppedSummonFlag = true;
    }
  }
}
