// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.Spore
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.NPCMatching;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class Spore : PlanteraPart
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(265);
  }
}
