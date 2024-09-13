// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.QueenSlimeMinion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class QueenSlimeMinion : EModeNPCBehaviour
  {
    public bool TimeToFly;
    public bool Landed;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(658, 659, 660);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      bitWriter.WriteBit(this.TimeToFly);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.TimeToFly = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      npc.knockBackResist = 0.0f;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (WorldSavingSystem.MasochistModeReal)
      {
        if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.queenSlimeBoss, 657))
        {
          Vector2 top = ((Entity) Main.player[Main.npc[EModeGlobalNPC.queenSlimeBoss].target]).Top;
          if (this.TimeToFly)
          {
            ((Entity) npc).velocity = Vector2.op_Multiply(Math.Min(((Vector2) ref ((Entity) npc).velocity).Length(), 20f), Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, top));
            NPC npc1 = npc;
            ((Entity) npc1).position = Vector2.op_Addition(((Entity) npc1).position, Vector2.op_Multiply(8f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, top)));
            if ((double) ((Entity) npc).Distance(top) < 300.0)
            {
              this.TimeToFly = false;
              EModeNPCBehaviour.NetSync(npc);
              NPC npc2 = npc;
              ((Entity) npc2).velocity = Vector2.op_Addition(((Entity) npc2).velocity, Vector2.op_Multiply(8f, Utils.RotatedByRandom(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, top), 0.78539818525314331)));
              npc.netUpdate = true;
            }
          }
          else if ((double) ((Entity) npc).Distance(top) > 900.0)
          {
            this.TimeToFly = true;
            EModeNPCBehaviour.NetSync(npc);
          }
        }
        else
          this.TimeToFly = false;
        npc.noTileCollide = this.TimeToFly;
      }
      else
      {
        npc.localAI[0] = 30f;
        if (npc.type == 660)
        {
          NPC npc3 = npc;
          ((Entity) npc3).position = Vector2.op_Subtraction(((Entity) npc3).position, Vector2.op_Multiply(((Entity) npc).velocity, 0.5f));
        }
        else
        {
          if (this.Landed)
            return;
          if ((double) ((Entity) npc).velocity.Y == 0.0)
            this.Landed = true;
          Player player = FargoSoulsUtil.PlayerExists((int) Player.FindClosest(((Entity) npc).Center, 0, 0));
          if (player == null)
            return;
          float num = ((Entity) npc).Center.X - ((Entity) player).Center.X;
          if ((double) Math.Abs(num) >= 128.0)
            return;
          ((Entity) npc).velocity.X *= 0.95f;
          ((Entity) npc).velocity.X += (float) ((128.0 - (double) Math.Abs(num)) * (double) Math.Sign(num) * 0.05000000074505806);
        }
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(137, 180, true, false);
      target.AddBuff(ModContent.BuffType<SmiteBuff>(), 360, true, false);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadGore(recolor, 1260);
    }
  }
}
