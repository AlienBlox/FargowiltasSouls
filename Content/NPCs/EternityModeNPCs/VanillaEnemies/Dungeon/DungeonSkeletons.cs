// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon.DungeonSkeletons
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon
{
  public class DungeonSkeletons : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(273, 274, 275, 276, 277, 279, 278, 280, 269, 270, 271, 272, 291, 292, 293, 285, 286, 283, 284, 281, 282);
    }

    public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
      base.ModifyNPCLoot(npc, npcLoot);
      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(154, 1, 1, 1));
    }
  }
}
