// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Desert.Vulture
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.NPCMatching;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Desert
{
  public class Vulture : Shooters
  {
    public int SwoopCount;
    public int RandomTime;

    public Vulture()
      : base(150, ModContent.ProjectileType<VultureFeather>(), 10f, dustType: 32, distance: 500f)
    {
    }

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(61);

    public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.SwoopCount);
      binaryWriter.Write7BitEncodedInt(this.RandomTime);
    }

    public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.SwoopCount = binaryReader.Read7BitEncodedInt();
      this.RandomTime = binaryReader.Read7BitEncodedInt();
    }

    public override void AI(NPC npc)
    {
      base.AI(npc);
      if ((double) npc.ai[0] != 0.0)
        return;
      this.AttackTimer = 0;
    }
  }
}
