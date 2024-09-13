// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Sharks
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
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies
{
  public class Sharks : EModeNPCBehaviour
  {
    public int JumpTimer;
    public int BleedCheckTimer;
    public int BleedCounter;

    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(65, 542, 543, 544, 545);
    }

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.JumpTimer);
      binaryWriter.Write7BitEncodedInt(this.BleedCheckTimer);
      binaryWriter.Write7BitEncodedInt(this.BleedCounter);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.JumpTimer = binaryReader.Read7BitEncodedInt();
      this.BleedCheckTimer = binaryReader.Read7BitEncodedInt();
      this.BleedCounter = binaryReader.Read7BitEncodedInt();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      this.JumpTimer = Main.rand.Next(60);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (npc.type != 65 || !Utils.NextBool(Main.rand, 3) || !npc.FargoSouls().CanHordeSplit)
        return;
      EModeGlobalNPC.Horde(npc, Main.rand.Next(1, 5));
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (npc.type == 65)
      {
        if (npc.life < npc.lifeMax / 2 && --this.JumpTimer < 0)
        {
          this.JumpTimer = 360;
          int index = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
          if (index != -1 && FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2;
            if (((Entity) Main.player[index]).active && !Main.player[index].dead && !Main.player[index].ghost)
            {
              vector2 = Vector2.op_Subtraction(((Entity) Main.player[index]).Center, ((Entity) npc).Center);
            }
            else
            {
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2).\u002Ector((double) ((Entity) npc).Center.X < (double) ((Entity) Main.player[index]).Center.X ? -300f : 300f, -100f);
            }
            vector2.X /= 90f;
            vector2.Y = (float) ((double) vector2.Y / 90.0 - 13.500000953674316);
            npc.ai[1] = 90f;
            npc.ai[2] = vector2.X;
            npc.ai[3] = vector2.Y;
            npc.netUpdate = true;
          }
        }
        if ((double) npc.ai[1] > 0.0)
        {
          --npc.ai[1];
          npc.noTileCollide = true;
          ((Entity) npc).velocity.X = npc.ai[2];
          ((Entity) npc).velocity.Y = npc.ai[3];
          npc.ai[3] += 0.3f;
          int num = 5;
          for (int index1 = 0; index1 < num; ++index1)
          {
            Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (Main.rand.NextDouble() * 3.14159274101257) - 1.57079637f), (float) Main.rand.Next(3, 8));
            int index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 172, vector2.X * 2f, vector2.Y * 2f, 100, new Color(), 1.4f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].noLight = true;
            Dust dust1 = Main.dust[index2];
            dust1.velocity = Vector2.op_Division(dust1.velocity, 4f);
            Dust dust2 = Main.dust[index2];
            dust2.velocity = Vector2.op_Subtraction(dust2.velocity, ((Entity) npc).velocity);
          }
        }
        else if (npc.noTileCollide)
          npc.noTileCollide = Collision.SolidCollision(Vector2.op_Addition(((Entity) npc).position, Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitX, (float) ((Entity) npc).width), 4f)), ((Entity) npc).width / 2, ((Entity) npc).height);
      }
      if (++this.BleedCheckTimer >= 240)
      {
        this.BleedCheckTimer = 0;
        int index = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
        if (index != -1 && this.BleedCounter < 5 && Main.player[index].bleed && FargoSoulsUtil.HostCheck)
        {
          ++this.BleedCounter;
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
        }
      }
      if (this.BleedCounter <= 0)
        return;
      npc.damage = (int) ((double) npc.defDamage * (1.0 + (double) this.BleedCounter / 2.0));
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(30, 240, true, false);
      target.FargoSouls().MaxLifeReduction += 50;
      target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 600, true, false);
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      if (npc.type != 65)
        return;
      if (Main.hardMode && Utils.NextBool(Main.rand, 4) && Collision.CanHitLine(((Entity) npc).Top, 0, 0, Vector2.op_Subtraction(((Entity) npc).Top, Vector2.op_Multiply(480f, Vector2.UnitY)), 0, 0) && FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, 384, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.5f), 0.0f, Main.myPlayer, 15f, 15f, 0.0f);
      if (Main.dedServ || !Utils.NextBool(Main.rand, 1000))
        return;
      SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/a", (SoundType) 0);
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      CombatText.NewText(((Entity) npc).Hitbox, Color.Blue, "a", true, false);
      for (int index1 = 0; index1 < 100; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, Utils.Next<int>(Main.rand, new int[5]
        {
          139,
          139,
          140,
          141,
          142
        }), 0.0f, 0.0f, 0, new Color(), 2.5f);
        Main.dust[index2].noGravity = Utils.NextBool(Main.rand, 3);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 6f);
      }
    }

    public virtual Color? GetAlpha(NPC npc, Color drawColor)
    {
      if (this.BleedCounter <= 0)
        return base.GetAlpha(npc, drawColor);
      ((Color) ref drawColor).R = (byte) (this.BleedCounter * 20 + 155);
      ref Color local1 = ref drawColor;
      ((Color) ref local1).G = (byte) ((uint) ((Color) ref local1).G / (uint) (byte) (this.BleedCounter + 1));
      ref Color local2 = ref drawColor;
      ((Color) ref local2).B = (byte) ((uint) ((Color) ref local2).B / (uint) (byte) (this.BleedCounter + 1));
      return new Color?(drawColor);
    }
  }
}
