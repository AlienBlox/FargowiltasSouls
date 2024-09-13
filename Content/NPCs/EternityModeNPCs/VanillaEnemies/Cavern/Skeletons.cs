// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.Skeletons
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
  public class Skeletons : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(21, 201, 202, 203, 322, 323, 324, 449, 450, 451, 452);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      switch (npc.type)
      {
        case 21:
          if (!Utils.NextBool(Main.rand, 5))
            break;
          npc.Transform(449);
          break;
        case 201:
          if (!Utils.NextBool(Main.rand, 5))
            break;
          npc.Transform(450);
          break;
        case 202:
          if (!Utils.NextBool(Main.rand, 5))
            break;
          npc.Transform(451);
          break;
        case 203:
          if (!Utils.NextBool(Main.rand, 5))
            break;
          npc.Transform(452);
          break;
      }
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.skeleBoss, 35))
        return base.CheckDead(npc);
      npc.life = 0;
      npc.HitEffect(0, 10.0, new bool?());
      return false;
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      if (!FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < 10; ++index)
      {
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector((float) Main.rand.Next(-50, 51), (float) Main.rand.Next(-100, 1));
        ((Vector2) ref vector2).Normalize();
        vector2 = Vector2.op_Multiply(vector2, Utils.NextFloat(Main.rand, 3f, 6f));
        vector2.Y -= Math.Abs(vector2.X) * 0.2f;
        vector2.Y -= 3f;
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, 471, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
    }
  }
}
