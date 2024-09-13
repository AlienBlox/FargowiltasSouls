// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.MeteorHead
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies
{
  public class MeteorHead : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(23);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.Counter);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.Counter = binaryReader.Read7BitEncodedInt();
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (!NPC.downedGolemBoss || !Utils.NextBool(Main.rand, 4))
        return;
      npc.Transform(418);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.Counter > 120)
      {
        this.Counter = 0;
        int index = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
        if (index != -1 && (double) ((Entity) npc).Distance(((Entity) Main.player[index]).Center) < 600.0 && FargoSoulsUtil.HostCheck)
        {
          NPC npc1 = npc;
          ((Entity) npc1).velocity = Vector2.op_Multiply(((Entity) npc1).velocity, 5f);
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
        }
      }
      EModeGlobalNPC.Aura(npc, 100f, 67, dustid: 6, color: new Color());
    }
  }
}
