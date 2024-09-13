// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hallow.IlluminantBat
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hallow
{
  public class IlluminantBat : EModeNPCBehaviour
  {
    public int Counter;
    public bool IsFakeBat;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(137);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      bitWriter.WriteBit(this.IsFakeBat);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.IsFakeBat = bitReader.ReadBit();
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      ++this.Counter;
      if (this.IsFakeBat)
      {
        if (Main.netMode == 2 && this.Counter <= 65 && this.Counter % 15 == 5)
        {
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
        }
        if (npc.alpha > 200)
          npc.alpha = 200;
        if (npc.lifeMax > 100)
          npc.lifeMax = 100;
        if (npc.life <= npc.lifeMax)
          return;
        npc.life = npc.lifeMax;
      }
      else
      {
        if (this.Counter <= 600)
          return;
        this.Counter = 0;
        if (!npc.HasPlayerTarget || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) >= 1000.0 || !FargoSoulsUtil.HostCheck || NPC.CountNPCS(137) >= 10)
          return;
        int index = FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, npc.type, velocity: new Vector2(Utils.NextFloat(Main.rand, -5f, 5f), Utils.NextFloat(Main.rand, -5f, 5f)));
        if (index == Main.maxNPCs)
          return;
        Main.npc[index].GetGlobalNPC<IlluminantBat>().IsFakeBat = true;
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 600, true, false);
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (!this.IsFakeBat)
        return base.CheckDead(npc);
      ((Entity) npc).active = false;
      if (npc.DeathSound.HasValue)
      {
        SoundStyle soundStyle = npc.DeathSound.Value;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      }
      return false;
    }
  }
}
