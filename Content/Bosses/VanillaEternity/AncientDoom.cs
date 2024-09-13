// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.AncientDoom
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class AncientDoom : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(523);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax *= 4;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[68] = true;
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot)
    {
      return base.CanHitPlayer(npc, target, ref CooldownSlot) && (double) npc.localAI[3] > 120.0;
    }

    public override bool SafePreAI(NPC npc)
    {
      int num = base.SafePreAI(npc) ? 1 : 0;
      if ((double) npc.localAI[3] == 0.0 && FargoSoulsUtil.HostCheck)
      {
        Vector2 vector2 = Vector2.op_Addition(((Entity) npc).Center, Utils.RotatedByRandom(new Vector2(250f, 0.0f), 2.0 * Math.PI));
        npc.ai[2] = vector2.X;
        npc.ai[3] = vector2.Y;
        npc.netUpdate = true;
      }
      ++npc.localAI[3];
      if ((double) npc.ai[2] <= 0.0)
        return num != 0;
      if ((double) npc.ai[3] <= 0.0)
        return num != 0;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector(npc.ai[2], npc.ai[3]);
      ((Entity) npc).velocity = Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(Vector2.op_Subtraction(vector2_1, ((Entity) npc).Center)), Math.PI / 2.0, new Vector2()), 6f);
      return num != 0;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300, true, false);
    }
  }
}
