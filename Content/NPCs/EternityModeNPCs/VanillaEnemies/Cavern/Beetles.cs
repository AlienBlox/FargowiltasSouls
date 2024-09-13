// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.Beetles
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public abstract class Beetles : EModeNPCBehaviour
  {
    protected virtual int DustType { get; }

    protected virtual void BeetleEffect(NPC affectedNPC)
    {
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      for (int index = 0; index < 10; ++index)
      {
        Vector2 vector2 = new Vector2();
        double num = Main.rand.NextDouble() * 2.0 * Math.PI;
        vector2.X += (float) (Math.Sin(num) * 400.0);
        vector2.Y += (float) (Math.Cos(num) * 400.0);
        if (!Collision.SolidCollision(Vector2.op_Addition(((Entity) npc).Center, vector2), 0, 0))
        {
          Dust dust1 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) npc).Center, vector2), new Vector2(4f, 4f)), 0, 0, this.DustType, 0.0f, 0.0f, 100, Color.White, 0.5f)];
          dust1.velocity = ((Entity) npc).velocity;
          if (Utils.NextBool(Main.rand, 3))
          {
            Dust dust2 = dust1;
            dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2), -5f));
          }
          dust1.noGravity = true;
        }
      }
      foreach (NPC npc1 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && !n.friendly && n.type != 217 && (double) ((Entity) n).Distance(((Entity) npc).Center) < 400.0)))
      {
        this.BeetleEffect(npc1);
        npc1.Eternity().BeetleTimer = 60;
        if (Utils.NextBool(Main.rand))
        {
          int index = Dust.NewDust(((Entity) npc1).position, ((Entity) npc1).width, ((Entity) npc1).height, 60, 0.0f, -1.5f, 0, new Color(), 1f);
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
          Main.dust[index].noLight = true;
        }
      }
    }
  }
}
