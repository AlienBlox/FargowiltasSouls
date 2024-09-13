// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Jungle.DoctorBones
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.NPCMatching;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Jungle
{
  public class DoctorBones : Shooters
  {
    public DoctorBones()
      : base(480, 99, 14f, 4f)
    {
    }

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(52);

    public override void SetDefaults(NPC npc)
    {
      base.SetDefaults(npc);
      npc.trapImmune = true;
    }
  }
}
