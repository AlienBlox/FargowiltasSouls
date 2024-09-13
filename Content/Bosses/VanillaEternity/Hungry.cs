// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.Hungry
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class Hungry : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(115, 116);

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[24] = true;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (WorldSavingSystem.SwarmActive)
        return;
      NPC npc1 = FargoSoulsUtil.NPCExists(EModeGlobalNPC.wallBoss, new int[1]
      {
        113
      });
      if (!npc.HasValidTarget || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) >= 200.0 || npc1 == null || !npc1.GetGlobalNPC<WallofFlesh>().UseCorruptAttack || npc1.GetGlobalNPC<WallofFlesh>().WorldEvilAttackCycleTimer >= 240 || WorldSavingSystem.MasochistModeReal)
        return;
      NPC npc2 = npc;
      ((Entity) npc2).position = Vector2.op_Addition(((Entity) npc2).position, Vector2.op_Division(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).position, ((Entity) Main.player[npc.target]).oldPosition), 3f));
      Vector2 vector2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center), Vector2.op_Multiply(200f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) Main.player[npc.target], ((Entity) npc).Center)));
      ((Entity) npc).velocity = Vector2.op_Division(vector2, 15f);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(24, 300, true, false);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
    }
  }
}
