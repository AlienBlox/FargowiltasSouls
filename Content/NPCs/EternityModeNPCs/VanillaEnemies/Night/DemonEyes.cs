// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Night.DemonEyes
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Night
{
  public class DemonEyes : EModeNPCBehaviour
  {
    public int AttackTimer;

    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(2, -43, 317, 318, 190, -38, 191, -39, 192, -40, 193, -41, 194, -42);
    }

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.AttackTimer);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.AttackTimer = binaryReader.Read7BitEncodedInt();
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (!Main.hardMode || !Utils.NextBool(Main.rand, 4))
        return;
      npc.Transform(133);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      ++this.AttackTimer;
      if (this.AttackTimer == 360)
      {
        FargoSoulsUtil.DustRing(((Entity) npc).Center, 32, 6, 5f, new Color(), 1.5f);
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      else if (this.AttackTimer >= 420)
      {
        npc.TargetClosest(true);
        Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center)), 10f);
        ((Entity) npc).velocity = vector2;
        this.AttackTimer = Main.rand.Next(-300, 0);
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if ((double) Math.Abs(((Entity) npc).velocity.Y) <= 5.0 && (double) Math.Abs(((Entity) npc).velocity.X) <= 5.0)
        return;
      int index1 = Dust.NewDust(new Vector2(((Entity) npc).position.X, ((Entity) npc).position.Y + 2f), ((Entity) npc).width, ((Entity) npc).height + 5, 1, ((Entity) npc).velocity.X * 0.2f, ((Entity) npc).velocity.Y * 0.2f, 100, new Color(), 1f);
      Main.dust[index1].noGravity = true;
      int index2 = Dust.NewDust(new Vector2(((Entity) npc).position.X, ((Entity) npc).position.Y + 2f), ((Entity) npc).width, ((Entity) npc).height + 5, 1, ((Entity) npc).velocity.X * 0.2f, ((Entity) npc).velocity.Y * 0.2f, 100, new Color(), 1f);
      Main.dust[index2].noGravity = true;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 300, true, false);
    }
  }
}
