// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hell.FireImp
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hell
{
  public class FireImp : Teleporters
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(24);

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (!Utils.NextBool(Main.rand, 5))
        return;
      npc.TargetClosest(false);
      if (!npc.HasValidTarget || !Main.player[npc.target].ZoneUnderworldHeight || !npc.FargoSouls().CanHordeSplit)
        return;
      EModeGlobalNPC.Horde(npc, Main.rand.Next(8) + 1);
    }
  }
}
