// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.OOA.DD2DarkMage
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.OOA
{
  public class DD2DarkMage : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(564, 565);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      int radius = npc.type == 564 ? 600 : 900;
      EModeGlobalNPC.Aura(npc, (float) radius, ModContent.BuffType<LethargicBuff>(), dustid: 254, color: new Color());
      foreach (NPC npc1 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && !n.friendly && n.type != npc.type && (double) ((Entity) n).Distance(((Entity) npc).Center) < (double) radius)))
      {
        npc1.Eternity().PaladinsShield = true;
        if (Utils.NextBool(Main.rand))
        {
          int index = Dust.NewDust(((Entity) npc1).position, ((Entity) npc1).width, ((Entity) npc1).height, 254, 0.0f, -3f, 0, new Color(), 1.5f);
          Main.dust[index].noGravity = true;
          Main.dust[index].noLight = true;
        }
      }
    }
  }
}
