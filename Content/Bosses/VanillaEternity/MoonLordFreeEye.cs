// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.MoonLordFreeEye
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class MoonLordFreeEye : MoonLord
  {
    public int OnSpawnCounter;
    public int RitualProj;
    public bool SpawnSynchronized;
    public bool SlowMode;
    public float LastState;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(400);

    public override int GetVulnerabilityState(NPC npc)
    {
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.ai[3], 398);
      return npc1 != null ? npc1.GetGlobalNPC<MoonLordCore>().VulnerabilityState : -1;
    }

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.OnSpawnCounter);
      binaryWriter.Write7BitEncodedInt(this.RitualProj);
      bitWriter.WriteBit(this.SpawnSynchronized);
      bitWriter.WriteBit(this.SlowMode);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.OnSpawnCounter = binaryReader.Read7BitEncodedInt();
      this.RitualProj = binaryReader.Read7BitEncodedInt();
      this.SpawnSynchronized = bitReader.ReadBit();
      this.SlowMode = bitReader.ReadBit();
    }

    public override bool SafePreAI(NPC npc)
    {
      if (WorldSavingSystem.SwarmActive)
        return true;
      NPC npc1 = FargoSoulsUtil.NPCExists(npc.ai[3], 398);
      if (npc1 == null || WorldSavingSystem.MasochistModeReal && Main.getGoodWorld)
        return true;
      if (!this.SpawnSynchronized && ++this.OnSpawnCounter > 2)
      {
        this.SpawnSynchronized = true;
        this.OnSpawnCounter = 0;
        for (int index = 0; index < Main.maxProjectiles; ++index)
        {
          if (((Entity) Main.projectile[index]).active && Main.projectile[index].type == ModContent.ProjectileType<LunarRitual>() && (double) Main.projectile[index].ai[1] == (double) npc.ai[3])
          {
            this.RitualProj = index;
            break;
          }
        }
        for (int index = 0; index < Main.maxNPCs; ++index)
        {
          if (((Entity) Main.npc[index]).active && Main.npc[index].type == 400 && (double) Main.npc[index].ai[3] == (double) npc.ai[3] && index != ((Entity) npc).whoAmI)
          {
            npc.ai[0] = Main.npc[index].ai[0];
            npc.ai[1] = Main.npc[index].ai[1];
            npc.ai[2] = Main.npc[index].ai[2];
            npc.ai[3] = Main.npc[index].ai[3];
            npc.localAI[0] = Main.npc[index].localAI[0];
            npc.localAI[1] = Main.npc[index].localAI[1];
            npc.localAI[2] = Main.npc[index].localAI[2];
            npc.localAI[3] = Main.npc[index].localAI[3];
            break;
          }
        }
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if (WorldSavingSystem.MasochistModeReal && (double) this.LastState != (double) npc.ai[0])
      {
        this.LastState = npc.ai[0];
        for (int index = 0; index < Main.maxNPCs; ++index)
        {
          if (((Entity) Main.npc[index]).active && Main.npc[index].type == 400 && (double) Main.npc[index].ai[3] == (double) npc.ai[3])
          {
            if (index != ((Entity) npc).whoAmI)
              ++npc.ai[1];
            else
              break;
          }
        }
      }
      if (npc1.dontTakeDamage && !WorldSavingSystem.MasochistModeReal)
      {
        this.SlowMode = !this.SlowMode;
        if (this.SlowMode)
        {
          NPC npc2 = npc;
          ((Entity) npc2).position = Vector2.op_Subtraction(((Entity) npc2).position, ((Entity) npc).velocity);
          return false;
        }
      }
      Projectile projectile = FargoSoulsUtil.ProjectileExists(this.RitualProj, new int[1]
      {
        ModContent.ProjectileType<LunarRitual>()
      });
      if (projectile != null && (double) projectile.ai[1] == (double) npc.ai[3])
      {
        int num = (int) projectile.localAI[0] - 150;
        if (this.GetVulnerabilityState(npc) == 4)
          num = 650;
        if ((double) ((Entity) npc).Distance(((Entity) projectile).Center) > (double) num)
          ((Entity) npc).Center = Vector2.Lerp(((Entity) npc).Center, Vector2.op_Addition(((Entity) projectile).Center, Vector2.op_Multiply(((Entity) npc).DirectionFrom(((Entity) projectile).Center), (float) num)), 0.05f);
      }
      return true;
    }
  }
}
