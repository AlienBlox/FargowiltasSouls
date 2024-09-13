// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.FrostMoon.Krampus
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
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.FrostMoon
{
  public class Krampus : EModeNPCBehaviour
  {
    public int JumpTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(351);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.JumpTimer);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.JumpTimer = binaryReader.Read7BitEncodedInt();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      this.JumpTimer = Main.rand.Next(60);
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      if (++this.JumpTimer > 600)
      {
        this.JumpTimer = 0;
        int index = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
        if (index != -1 && FargoSoulsUtil.HostCheck && (double) ((Entity) npc).Distance(((Entity) Main.player[index]).Center) < 900.0)
        {
          Vector2 vector2;
          if (((Entity) Main.player[index]).active && !Main.player[index].dead && !Main.player[index].ghost)
          {
            vector2 = Vector2.op_Subtraction(((Entity) Main.player[index]).Center, ((Entity) npc).Bottom);
          }
          else
          {
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2).\u002Ector((double) ((Entity) npc).Center.X < (double) ((Entity) Main.player[index]).Center.X ? -300f : 300f, -100f);
          }
          vector2.X /= 90f;
          vector2.Y = (float) ((double) vector2.Y / 90.0 - 18.0);
          npc.ai[1] = 90f;
          npc.ai[2] = vector2.X;
          npc.ai[3] = vector2.Y;
          npc.netUpdate = true;
        }
        return false;
      }
      if (this.JumpTimer == 540)
      {
        if (npc.HasValidTarget && (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) < 900.0)
        {
          FargoSoulsUtil.DustRing(((Entity) npc).Center, 64, 60, 12f, new Color(), 4f);
        }
        else
        {
          this.JumpTimer -= 120;
          EModeNPCBehaviour.NetSync(npc);
        }
      }
      if (this.JumpTimer > 540)
      {
        ((Entity) npc).velocity.X = 0.0f;
        return false;
      }
      if ((double) npc.ai[1] > 0.0)
      {
        --npc.ai[1];
        npc.noTileCollide = true;
        ((Entity) npc).velocity.X = npc.ai[2];
        ((Entity) npc).velocity.Y = npc.ai[3];
        npc.ai[3] += 0.4f;
        int num = 5;
        for (int index1 = 0; index1 < num; ++index1)
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (Main.rand.NextDouble() * 3.14159274101257) - 1.57079637f), (float) Main.rand.Next(3, 8));
          int index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 60, vector2.X * 2f, vector2.Y * 2f, 100, new Color(), 1.4f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].noLight = true;
          Dust dust1 = Main.dust[index2];
          dust1.velocity = Vector2.op_Division(dust1.velocity, 4f);
          Dust dust2 = Main.dust[index2];
          dust2.velocity = Vector2.op_Subtraction(dust2.velocity, ((Entity) npc).velocity);
        }
        this.JumpTimer = 0;
        return false;
      }
      if (!npc.noTileCollide)
        return flag;
      this.JumpTimer = 0;
      npc.noTileCollide = Collision.SolidCollision(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height);
      return false;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<MidasBuff>(), 600, true, false);
      target.AddBuff(ModContent.BuffType<UnluckyBuff>(), 1800, true, false);
    }
  }
}
