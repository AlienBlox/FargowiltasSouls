// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Jungle.FlyingSnake
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Jungle
{
  public class FlyingSnake : EModeNPCBehaviour
  {
    public bool Phase2;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(226);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      bitWriter.WriteBit(this.Phase2);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.Phase2 = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.trapImmune = true;
      npc.lifeMax *= 2;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[20] = true;
      npc.buffImmune[70] = true;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (!this.Phase2 && npc.life < npc.lifeMax / 2)
      {
        this.Phase2 = true;
        FargoSoulsUtil.DustRing(((Entity) npc).Center, 32, 6, 10f, new Color(), 3f);
        EModeNPCBehaviour.NetSync(npc);
      }
      if (!this.Phase2)
        return;
      NPC npc1 = npc;
      ((Entity) npc1).position = Vector2.op_Addition(((Entity) npc1).position, ((Entity) npc).velocity);
      npc.knockBackResist = 0.0f;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<InfestedBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<ClippedWingsBuff>(), 300, true, false);
    }

    public virtual Color? GetAlpha(NPC npc, Color drawColor)
    {
      if (!this.Phase2)
        return base.GetAlpha(npc, drawColor);
      ((Color) ref drawColor).R = byte.MaxValue;
      ref Color local1 = ref drawColor;
      ((Color) ref local1).G = (byte) ((uint) ((Color) ref local1).G / 2U);
      ref Color local2 = ref drawColor;
      ((Color) ref local2).B = (byte) ((uint) ((Color) ref local2).B / 2U);
      return new Color?(drawColor);
    }
  }
}
