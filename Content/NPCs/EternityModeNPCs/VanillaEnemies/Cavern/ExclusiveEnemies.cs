// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.ExclusiveEnemies
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class ExclusiveEnemies : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(494, 496, 498, 499, 500, 501, 497, 502, 503, 504, 505, 495);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      switch (npc.type)
      {
        case 494:
        case 496:
          if (!Utils.NextBool(Main.rand, 5))
            break;
          npc.Transform(Main.rand.Next(498, 507));
          break;
        case 495:
        case 502:
        case 503:
        case 504:
        case 505:
          if (!Utils.NextBool(Main.rand, 5))
            break;
          npc.Transform(Main.rand.Next(496, 498));
          break;
        case 497:
        case 498:
        case 499:
        case 500:
        case 501:
          if (!Utils.NextBool(Main.rand, 5))
            break;
          npc.Transform(Main.rand.Next(494, 496));
          break;
      }
    }
  }
}
