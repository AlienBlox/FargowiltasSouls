// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Slimes
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies
{
  public class Slimes : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(1, -6, -4, 335, 336, 333, 334, 302, -1, 121, -2, 535, -5, -6, 81, 71, 71, 667, -3, 147, -10, 138, 59, 16, -7, 659, 660, 244, -8, 537, 184, 204, 225, -9, 183, -25, -24, 141);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.TargetClosest(true);
      if (NPC.AnyNPCs(50))
      {
        ((Entity) npc).active = false;
      }
      else
      {
        if (npc.type == -10 && Utils.NextBool(Main.rand, 5))
          npc.Transform(204);
        if (npc.type == 147 && Utils.NextBool(Main.rand, 5))
          npc.Transform(184);
        if (npc.type != 1)
          return;
        if (Utils.NextBool(Main.rand, 500))
        {
          npc.Transform(667);
        }
        else
        {
          if (!Main.slimeRain || !Utils.NextBool(Main.rand, 8))
            return;
          npc.SetDefaults(Utils.Next<int>(Main.rand, new int[5]
          {
            -8,
            -7,
            -9,
            -6,
            535
          }), new NPCSpawnParams());
          if (Main.netMode != 2)
            return;
          NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
      }
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (npc.type == 1 && npc.netID == -4)
        npc.ai[0] = -2000f;
      if (npc.type != 225)
        return;
      if (((Entity) npc).wet)
        this.Counter = 30;
      if (this.Counter > 0)
        --this.Counter;
      if (this.Counter > 0 || (double) ((Entity) npc).velocity.Y <= 0.0)
        return;
      ((Entity) npc).velocity.Y /= 10f;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(137, 60, true, false);
      switch (npc.type)
      {
        case -10:
        case 204:
          target.AddBuff(20, 180, true, false);
          break;
        case 1:
          if (npc.type == -6)
            target.AddBuff(22, 300, true, false);
          if (npc.netID != -4)
            break;
          target.FargoSouls().AddBuffNoStack(ModContent.BuffType<StunnedBuff>(), 60);
          ((Entity) target).velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) target).Center, ((Entity) npc).Center)), 30f);
          break;
        case 16:
          target.AddBuff(ModContent.BuffType<AntisocialBuff>(), 1200, true, false);
          break;
        case 81:
          target.AddBuff(ModContent.BuffType<RottingBuff>(), 1200, true, false);
          break;
        case 138:
          target.AddBuff(ModContent.BuffType<PurifiedBuff>(), 300, true, false);
          break;
        case 141:
          target.AddBuff(ModContent.BuffType<InfestedBuff>(), 360, true, false);
          break;
        case 147:
        case 184:
          target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 300, true, false);
          break;
        case 183:
          target.AddBuff(ModContent.BuffType<BloodthirstyBuff>(), 300, true, false);
          break;
        case 225:
          target.AddBuff(103, 600, true, false);
          break;
        case 667:
          target.AddBuff(ModContent.BuffType<MidasBuff>(), 600, true, false);
          break;
      }
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      if (npc.type != 1)
        return;
      switch (npc.netID)
      {
        case -9:
          SplitIntoSlimes(-7, 2);
          break;
        case -8:
          SplitIntoSlimes(-3, 2);
          break;
        case -7:
          SplitIntoSlimes(-8, 2);
          break;
        case -4:
          SplitIntoSlimes(-9, 2);
          SplitIntoSlimes(16, 1);
          break;
      }

      void SplitIntoSlimes(int type, int amount)
      {
        if (!FargoSoulsUtil.HostCheck)
          return;
        for (int index1 = 0; index1 < amount; ++index1)
        {
          if (Utils.NextBool(Main.rand, 3))
          {
            int index2 = FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, type, velocity: new Vector2(((Entity) npc).velocity.X * 2f, ((Entity) npc).velocity.Y));
            if (index2 != Main.maxNPCs)
            {
              ((Entity) Main.npc[index2]).velocity.X += (float) ((double) Main.rand.Next(-20, 20) * 0.10000000149011612 + (double) (index1 * ((Entity) npc).direction) * 0.30000001192092896);
              ((Entity) Main.npc[index2]).velocity.Y -= (float) Main.rand.Next(0, 10) * 0.1f + (float) index1;
            }
          }
        }
      }
    }
  }
}
