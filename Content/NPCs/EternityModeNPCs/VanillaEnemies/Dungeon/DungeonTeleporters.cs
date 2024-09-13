// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon.DungeonTeleporters
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.NPCMatching;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon
{
  public class DungeonTeleporters : Teleporters
  {
    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(285, 286, 283, 284, 281, 282);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      switch (npc.type)
      {
        case 281:
          if (Utils.NextBool(Main.rand, 4))
          {
            npc.Transform(Utils.NextBool(Main.rand) ? 285 : 283);
            break;
          }
          break;
        case 282:
          if (Utils.NextBool(Main.rand, 4))
          {
            npc.Transform(Utils.NextBool(Main.rand) ? 286 : 284);
            break;
          }
          break;
        case 283:
          if (Utils.NextBool(Main.rand, 4))
          {
            npc.Transform(Utils.NextBool(Main.rand) ? 285 : 281);
            break;
          }
          break;
        case 284:
          if (Utils.NextBool(Main.rand, 4))
          {
            npc.Transform(Utils.NextBool(Main.rand) ? 286 : 282);
            break;
          }
          break;
        case 285:
          if (Utils.NextBool(Main.rand, 4))
          {
            npc.Transform(Utils.NextBool(Main.rand) ? 283 : 281);
            break;
          }
          break;
        case 286:
          if (Utils.NextBool(Main.rand, 4))
          {
            npc.Transform(Utils.NextBool(Main.rand) ? 284 : 282);
            break;
          }
          break;
      }
      this.DoTeleport = true;
      this.TeleportTimer = this.TeleportThreshold - 6;
    }

    public override void AI(NPC npc)
    {
      if (npc.HasValidTarget && !Main.player[npc.target].ZoneDungeon && !this.DoTeleport)
      {
        this.DoTeleport = true;
        this.TeleportTimer = this.TeleportThreshold - 420;
      }
      base.AI(npc);
    }
  }
}
