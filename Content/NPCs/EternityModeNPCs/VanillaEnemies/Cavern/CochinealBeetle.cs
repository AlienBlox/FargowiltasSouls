// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.CochinealBeetle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.NPCMatching;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class CochinealBeetle : Beetles
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(217);

    protected override int DustType => 60;

    protected override void BeetleEffect(NPC affectedNPC)
    {
      affectedNPC.Eternity().BeetleOffenseAura = true;
    }
  }
}
