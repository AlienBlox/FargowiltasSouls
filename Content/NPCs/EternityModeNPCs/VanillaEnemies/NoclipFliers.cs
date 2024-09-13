// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.NoclipFliers
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using System.IO;
using Terraria;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies
{
  public class NoclipFliers : EModeNPCBehaviour
  {
    public int MPSyncSpawnTimer = 30;
    public bool CanNoclip;

    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(48, 6, -12, -11, 173);
    }

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.MPSyncSpawnTimer);
      bitWriter.WriteBit(this.CanNoclip);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.MPSyncSpawnTimer = binaryReader.Read7BitEncodedInt();
      this.CanNoclip = bitReader.ReadBit();
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (this.MPSyncSpawnTimer > 0 && --this.MPSyncSpawnTimer == 0)
      {
        if (FargoSoulsUtil.HostCheck && Utils.NextBool(Main.rand))
          this.CanNoclip = npc.type != 6 || NPC.downedBoss2;
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      npc.noTileCollide = this.CanNoclip && npc.HasPlayerTarget && !Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0);
    }
  }
}
