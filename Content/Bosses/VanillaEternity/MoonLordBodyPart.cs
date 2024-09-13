// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.MoonLordBodyPart
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.NPCMatching;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class MoonLordBodyPart : MoonLord
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(396, 397, 401);

    public override int GetVulnerabilityState(NPC npc)
    {
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.ai[3], 398);
      return npc1 != null ? npc1.GetGlobalNPC<MoonLordCore>().VulnerabilityState : -1;
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
    }
  }
}
