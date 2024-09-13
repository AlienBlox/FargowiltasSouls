// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hell.BoneSerpent
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hell
{
  public class BoneSerpent : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(39);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.Counter >= 300 && this.Counter % 20 == 0)
      {
        int index = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
        if (index != -1 && (double) ((Entity) npc).Distance(((Entity) Main.player[index]).Center) < 800.0 && FargoSoulsUtil.HostCheck)
          FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 25, velocity: new Vector2());
      }
      if (this.Counter <= 420)
        return;
      this.Counter = 0;
    }
  }
}
