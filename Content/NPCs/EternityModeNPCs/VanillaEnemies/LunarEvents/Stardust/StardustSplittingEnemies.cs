// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Stardust.StardustSplittingEnemies
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;
using Terraria.Localization;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Stardust
{
  public class StardustSplittingEnemies : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(407, 411, 409, 402);
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (FargoSoulsUtil.HostCheck)
      {
        int firstNpc = NPC.FindFirstNPC(493);
        if (firstNpc != -1 && NPC.CountNPCS(406) < 10 && ((Entity) Main.npc[firstNpc]).active && (double) ((Entity) npc).Distance(((Entity) Main.npc[firstNpc]).Center) < 5000.0)
        {
          for (int index1 = 0; index1 < 3; ++index1)
          {
            int index2 = NPC.NewNPC(((Entity) npc).GetSource_FromAI((string) null), (int) ((Entity) npc).Center.X, (int) ((Entity) npc).Center.Y, 406, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
            if (index2 < Main.maxNPCs)
            {
              ((Entity) Main.npc[index2]).velocity.X = (float) Main.rand.Next(-10, 11);
              ((Entity) Main.npc[index2]).velocity.Y = (float) Main.rand.Next(-10, 11);
              if (Main.netMode == 2)
                NetMessage.SendData(23, -1, -1, (NetworkText) null, index2, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
          }
        }
      }
      return base.CheckDead(npc);
    }
  }
}
