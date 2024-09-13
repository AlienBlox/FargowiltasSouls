// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.SkyAndRain.FlyingFish
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.SkyAndRain
{
  public class FlyingFish : Shooters
  {
    public FlyingFish()
      : base(70, 22, 10f, dustType: 33, distance: 250f)
    {
    }

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(224);

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (!Utils.NextBool(Main.rand, 4) || !npc.FargoSouls().CanHordeSplit || !WorldSavingSystem.DownedAnyBoss)
        return;
      EModeGlobalNPC.Horde(npc, Main.rand.Next(1, 5));
    }

    public override bool SafePreAI(NPC npc) => base.SafePreAI(npc);
  }
}
