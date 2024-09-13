// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.PlanterasHook
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class PlanterasHook : PlanteraPart
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(263);

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      if (WorldSavingSystem.SwarmActive)
        return flag;
      npc.damage = 0;
      npc.defDamage = 0;
      NPC npc1 = FargoSoulsUtil.NPCExists(NPC.plantBoss, new int[1]
      {
        262
      });
      if (npc1 != null && npc1.life < npc1.lifeMax / 2 && npc1.HasValidTarget)
      {
        if ((double) ((Entity) npc).Distance(((Entity) Main.player[npc1.target]).Center) > 600.0)
        {
          Vector2 vector2 = Vector2.op_Division(((Entity) Main.player[npc1.target]).Center, 16f);
          vector2.X += (float) Main.rand.Next(-25, 26);
          vector2.Y += (float) Main.rand.Next(-25, 26);
          Framing.GetTileSafely((int) vector2.X, (int) vector2.Y);
          npc.localAI[0] = 600f;
          if (FargoSoulsUtil.HostCheck)
            npc.netUpdate = true;
          npc.ai[0] = vector2.X;
          npc.ai[1] = vector2.Y;
        }
        if ((double) ((Entity) npc).Distance(new Vector2((float) ((double) npc.ai[0] * 16.0 + 8.0), (float) ((double) npc.ai[1] * 16.0 + 8.0))) > 32.0)
        {
          NPC npc2 = npc;
          ((Entity) npc2).position = Vector2.op_Addition(((Entity) npc2).position, ((Entity) npc).velocity);
        }
      }
      return flag;
    }
  }
}
