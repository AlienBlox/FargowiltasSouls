// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PirateInvasion.PirateCaptain
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PirateInvasion
{
  public class PirateCaptain : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(216);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if ((double) npc.ai[2] <= 0.0 || (double) npc.localAI[2] < 20.0 || (double) npc.ai[1] > 30.0)
        return;
      if (FargoSoulsUtil.HostCheck)
      {
        Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
        vector2_1.Y -= Math.Abs(vector2_1.X) * 0.2f;
        vector2_1.X += (float) Main.rand.Next(-20, 21);
        vector2_1.Y += (float) Main.rand.Next(-20, 21);
        ((Vector2) ref vector2_1).Normalize();
        vector2_1 = Vector2.op_Multiply(vector2_1, 12f);
        npc.localAI[2] = 0.0f;
        for (int index = 0; index < 15; ++index)
        {
          Vector2 vector2_2 = vector2_1;
          vector2_2.X += (float) Main.rand.Next(-10, 11) * 0.3f;
          vector2_2.Y += (float) Main.rand.Next(-10, 11) * 0.3f;
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2_2, 240, Main.expertMode ? 80 : 100, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      npc.netUpdate = true;
    }
  }
}
