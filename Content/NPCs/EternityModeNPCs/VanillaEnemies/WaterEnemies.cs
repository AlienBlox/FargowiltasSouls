// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.WaterEnemies
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies
{
  public class WaterEnemies : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(63, 67, 64, 58, 220, 65, 221, 102, 157, 241, 242, 256, 103, 55, 57, 465, 33, 361, 445, 485, 486, 487, 616, 617, 625, 362, 363, 364, 365, 608, 609, 615, 602, 603);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.Eternity().isWaterEnemy = true;
    }
  }
}
