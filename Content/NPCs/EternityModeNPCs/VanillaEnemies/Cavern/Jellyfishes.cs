// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.Jellyfishes
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class Jellyfishes : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(242, 63, 103, 64, 256);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (!((Entity) npc).wet || (double) npc.ai[1] != 1.0)
        return;
      Player localPlayer = Main.LocalPlayer;
      if ((double) ((Entity) npc).Distance(((Entity) localPlayer).Center) < 200.0 && ((Entity) localPlayer).wet && Collision.CanHitLine(((Entity) localPlayer).Center, 2, 2, ((Entity) npc).Center, 2, 2))
        localPlayer.AddBuff(144, 2, true, false);
      for (int index = 0; index < 10; ++index)
      {
        Vector2 vector2 = new Vector2();
        double num = Main.rand.NextDouble() * 2.0 * Math.PI;
        vector2.X += (float) (Math.Sin(num) * 200.0);
        vector2.Y += (float) (Math.Cos(num) * 200.0);
        Tile tileSafely = Framing.GetTileSafely(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) npc).Center, vector2), new Vector2(4f, 4f)));
        if (((Tile) ref tileSafely).LiquidAmount != (byte) 0)
        {
          Dust dust1 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) npc).Center, vector2), new Vector2(4f, 4f)), 0, 0, 226, 0.0f, 0.0f, 100, Color.White, 1f)];
          dust1.velocity = ((Entity) npc).velocity;
          if (Utils.NextBool(Main.rand, 3))
          {
            Dust dust2 = dust1;
            dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2), -5f));
          }
          dust1.noGravity = true;
        }
      }
    }
  }
}
