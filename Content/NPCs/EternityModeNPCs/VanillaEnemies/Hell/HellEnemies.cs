// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hell.HellEnemies
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hell
{
  public class HellEnemies : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(60, 59, 24, 62, 66, 40, 39, 41, 151, 156, 25, 654, 655, 653, 534);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[24] = true;
      npc.buffImmune[323] = true;
    }
  }
}
