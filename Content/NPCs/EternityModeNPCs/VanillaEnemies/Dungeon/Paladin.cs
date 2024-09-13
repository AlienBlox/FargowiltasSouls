// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon.Paladin
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon
{
  public class Paladin : EModeNPCBehaviour
  {
    public int Counter;
    public bool IsSmallPaladin;
    public bool FinishedSpawning;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(290);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      bitWriter.WriteBit(this.IsSmallPaladin);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.IsSmallPaladin = bitReader.ReadBit();
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (this.IsSmallPaladin && Main.netMode == 2 && ++this.Counter <= 65 && this.Counter % 15 == 5)
      {
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if (this.IsSmallPaladin && !this.FinishedSpawning)
      {
        this.FinishedSpawning = true;
        ((Entity) npc).Center = ((Entity) npc).Bottom;
        ((Entity) npc).width = (int) ((double) ((Entity) npc).width * 0.64999997615814209);
        ((Entity) npc).height = (int) ((double) ((Entity) npc).height * 0.64999997615814209);
        npc.scale = 0.65f;
        npc.lifeMax /= 2;
        if (npc.life > npc.lifeMax)
          npc.life = npc.lifeMax;
        ((Entity) npc).Bottom = ((Entity) npc).Center;
      }
      EModeGlobalNPC.Aura(npc, 800f, 36, dustid: 246, color: new Color());
      foreach (NPC npc1 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && !n.friendly && n.type != 290 && (double) ((Entity) n).Distance(((Entity) npc).Center) < 800.0)))
      {
        npc1.Eternity().PaladinsShield = true;
        if (Utils.NextBool(Main.rand))
        {
          int index = Dust.NewDust(((Entity) npc1).position, ((Entity) npc1).width, ((Entity) npc1).height, 246, 0.0f, -1.5f, 0, new Color(), 1f);
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
          Main.dust[index].noLight = true;
        }
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<LethargicBuff>(), 600, true, false);
    }

    public virtual void OnKill(NPC npc)
    {
      if (!FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < 5; ++index)
        FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, 288, velocity: Utils.NextVector2Circular(Main.rand, 16f, 16f));
    }
  }
}
