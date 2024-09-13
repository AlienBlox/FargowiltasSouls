// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Mushroom.MushroomEnemies
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Mushroom
{
  public class MushroomEnemies : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(259, 260, 257, 258, 634, 254, (int) byte.MaxValue, 635, 256);
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      if (!FargoSoulsUtil.HostCheck || !Main.hardMode || !Utils.NextBool(Main.rand))
        return;
      if (NPC.CountNPCS(261) < 24)
      {
        for (int index = 0; index < 8; ++index)
          FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_Death((string) null), ((Entity) npc).Center, 261, velocity: Vector2.op_Multiply(0.5f, new Vector2(Utils.NextFloat(Main.rand, -5f, 5f), Utils.NextFloat(Main.rand, -5f, 5f))));
      }
      else
      {
        if (npc.type == 634)
          return;
        FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_Death((string) null), ((Entity) npc).Center, 634, velocity: Vector2.op_Multiply(0.5f, new Vector2(Utils.NextFloat(Main.rand, -5f, 5f), Utils.NextFloat(Main.rand, -5f, 5f))));
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(20, 300, true, false);
    }
  }
}
