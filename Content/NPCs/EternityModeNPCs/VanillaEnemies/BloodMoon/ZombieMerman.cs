// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.BloodMoon.ZombieMerman
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
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.BloodMoon
{
  public class ZombieMerman : EModeNPCBehaviour
  {
    public int JumpTimer;
    public bool Jumped;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(586);

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

    public override void OnFirstTick(NPC npc)
    {
      for (int index = 0; index < 9; ++index)
        FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 3, velocity: Utils.NextVector2Circular(Main.rand, 8f, 8f));
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      if (this.JumpTimer > 120)
      {
        this.JumpTimer = 0;
        this.Jumped = true;
        int index = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
        if (index != -1 && FargoSoulsUtil.HostCheck)
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
          vector2.X /= 60f;
          vector2.Y = (float) ((double) vector2.Y / 60.0 - 12.0);
          npc.ai[1] = 60f;
          npc.ai[2] = vector2.X;
          npc.ai[3] = vector2.Y;
          npc.netUpdate = true;
        }
        return false;
      }
      if ((double) npc.ai[1] > 0.0)
      {
        --npc.ai[1];
        npc.noTileCollide = true;
        ((Entity) npc).velocity.X = npc.ai[2];
        ((Entity) npc).velocity.Y = npc.ai[3];
        npc.ai[3] += 0.4f;
        int num = 2;
        for (int index1 = 0; index1 < num; ++index1)
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (Main.rand.NextDouble() * 3.14159274101257) - 1.57079637f), (float) Main.rand.Next(3, 8));
          int index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 60, vector2.X * 2f, vector2.Y * 2f, 100, new Color(), 1.4f);
          Main.dust[index2].noGravity = true;
          Dust dust1 = Main.dust[index2];
          dust1.velocity = Vector2.op_Division(dust1.velocity, 4f);
          Dust dust2 = Main.dust[index2];
          dust2.velocity = Vector2.op_Subtraction(dust2.velocity, ((Entity) npc).velocity);
        }
        this.JumpTimer = 0;
        ++this.JumpTimer;
        return false;
      }
      if (npc.noTileCollide)
      {
        ((Entity) npc).direction = Math.Sign(((Entity) npc).velocity.X);
        this.JumpTimer = 0;
        npc.noTileCollide = Collision.SolidCollision(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height);
        return false;
      }
      if (npc.HasValidTarget && npc.life < npc.lifeMax / 2 && (double) ((Entity) npc).velocity.Y == 0.0)
        ++this.JumpTimer;
      if ((double) ((Entity) npc).velocity.Y == 0.0 && this.Jumped)
      {
        this.Jumped = false;
        if (FargoSoulsUtil.HostCheck)
        {
          for (int index3 = -1; index3 <= 1; index3 += 2)
          {
            for (int index4 = 0; index4 <= 3; ++index4)
            {
              Vector2 vector2 = Vector2.op_Multiply(16f * (float) index3, Utils.RotatedBy(Vector2.UnitX, 0.2617993950843811 * (double) index4 * (double) -index3, new Vector2()));
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, 756, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, Utils.NextFloat(Main.rand, 0.5f, 1f), 0.0f);
            }
          }
        }
      }
      return flag;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<AnticoagulationBuff>(), 600, true, false);
    }
  }
}
