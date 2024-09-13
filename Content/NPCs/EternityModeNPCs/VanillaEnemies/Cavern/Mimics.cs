// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.Mimics
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class Mimics : EModeNPCBehaviour
  {
    public int InvulFrameTimer;
    public int AttackTimer;
    public int Attack;
    public int FlightCD;
    public bool Flying;
    public Vector2 LockVector = Vector2.Zero;

    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(85, 341, 629, 473, 474, 475, 476);
    }

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.AttackTimer);
      binaryWriter.Write7BitEncodedInt(this.Attack);
      binaryWriter.Write7BitEncodedInt(this.FlightCD);
      binaryWriter.Write(this.LockVector.X);
      binaryWriter.Write(this.LockVector.Y);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.AttackTimer = binaryReader.Read7BitEncodedInt();
      this.Attack = binaryReader.Read7BitEncodedInt();
      this.FlightCD = binaryReader.Read7BitEncodedInt();
      this.LockVector.X = binaryReader.ReadSingle();
      this.LockVector.Y = binaryReader.ReadSingle();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      if (Main.hardMode)
        return;
      npc.damage = (int) Math.Round((double) npc.damage * 0.5);
    }

    public override bool SafePreAI(NPC npc)
    {
      Player player = Main.player[npc.target];
      bool flag = base.SafePreAI(npc);
      if (!Main.hardMode && npc.life > npc.lifeMax / 2 || npc.type != 85 && npc.type != 341 && npc.type != 629 || npc.life >= npc.lifeMax || (double) npc.ai[0] != 1.0 || !((Entity) player).active || player.dead)
        return flag;
      if (this.AttackTimer < 180)
      {
        if (this.FlightCD == 0 && ((double) ((Entity) npc).Distance(((Entity) player).Center) > 1000.0 || (double) ((Entity) npc).Distance(((Entity) player).Center) > 200.0 && !Collision.CanHitLine(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, ((Entity) player).position, ((Entity) player).width, ((Entity) player).height)))
          this.Flying = true;
        if (this.Flying)
        {
          Flight();
          if (this.AttackTimer < 175)
            ++this.AttackTimer;
          return false;
        }
      }
      if (this.FlightCD > 0)
        --this.FlightCD;
      if (this.AttackTimer == 175)
      {
        this.Attack = Main.rand.Next(3);
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if (this.AttackTimer >= 180)
      {
        switch (this.Attack)
        {
          case 0:
            TeleportPunchAttack();
            break;
          case 1:
            DaggerAttack();
            break;
          case 2:
            StarveilAttack();
            break;
        }
        flag = false;
      }
      ++this.AttackTimer;
      return flag;

      void TeleportPunchAttack()
      {
        int num = this.AttackTimer - 180;
        if (num == 1)
        {
          this.LockVector = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) player).velocity, 3f));
          ((Entity) npc).velocity.X = 0.0f;
          SoundEngine.PlaySound(ref SoundID.Item6, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        }
        if (num > 1 && num < 60)
        {
          for (int index = 0; index < 10; ++index)
            Dust.NewDust(Vector2.op_Subtraction(this.LockVector, new Vector2((float) (((Entity) npc).width / 2), (float) (((Entity) npc).height / 2))), ((Entity) npc).width, ((Entity) npc).height, 15, 0.0f, 0.0f, 0, new Color(), 1f);
        }
        if (num == 60)
        {
          ((Entity) npc).position = Vector2.op_Subtraction(this.LockVector, new Vector2((float) (((Entity) npc).width / 2), (float) (((Entity) npc).height / 2)));
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<MimicTitanGlove>(), 0, 1f, Main.myPlayer, 0.0f, (float) ((Entity) npc).whoAmI, 0.0f);
        }
        if (num == 120)
        {
          ((Entity) npc).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) player).Center), 20f);
          SoundEngine.PlaySound(ref SoundID.DD2_SonicBoomBladeSlash, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          npc.noGravity = true;
        }
        NPC npc = npc;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.99f);
        if (num == 140)
          npc.noGravity = false;
        if (num < 160)
          return;
        this.AttackTimer = 0;
      }

      void DaggerAttack()
      {
        int num1 = this.AttackTimer - 180;
        ((Entity) npc).velocity.X = 0.0f;
        if (num1 % 20 == 0 && num1 <= 20 && FargoSoulsUtil.HostCheck)
        {
          float num2 = 4f;
          float radians = MathHelper.ToRadians(Utils.NextFloat(Main.rand, -6f, 6f));
          Vector2 vector2_1 = Vector2.op_Addition(((Entity) npc).Center, new Vector2((float) Main.rand.Next(-10, 10), (float) Main.rand.Next(-10, 10)));
          Vector2 vector2_2 = Utils.RotatedBy(Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) player).Center, vector2_1)), num2), (double) radians, new Vector2());
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_1, vector2_2, ModContent.ProjectileType<MimicDagger>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 1f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
        if (num1 < 100)
          return;
        this.AttackTimer = 0;
      }

      void StarveilAttack()
      {
        int num1 = this.AttackTimer - 180;
        ((Entity) npc).velocity.X = 0.0f;
        if (num1 == 5 && FargoSoulsUtil.HostCheck)
        {
          for (int index1 = 0; index1 < 3; ++index1)
          {
            float num2 = 12f;
            float radians = MathHelper.ToRadians(1f);
            Vector2 vector2_1 = Vector2.op_Addition(((Entity) npc).Center, new Vector2((float) (Main.rand.Next(800) - 300), -1000f));
            Vector2 vector2_2 = Utils.RotatedBy(Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) player).Center, vector2_1)), num2), (double) Utils.NextFloat(Main.rand, -radians, radians), new Vector2());
            int index2 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_1, vector2_2, ModContent.ProjectileType<MimicHallowStar>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 1f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            Main.projectile[index2].friendly = false;
            Main.projectile[index2].hostile = true;
            Main.projectile[index2].tileCollide = false;
          }
        }
        if (num1 < 60)
          return;
        this.AttackTimer = 0;
      }

      void Flight()
      {
        FlyToward(((Entity) player).Center);
        npc.noTileCollide = true;
        npc.noGravity = true;
        if ((double) npc.localAI[3] % 10.0 == 0.0)
          SoundEngine.PlaySound(ref SoundID.Run, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        Dust.NewDust(Vector2.op_Addition(((Entity) npc).Center, new Vector2(0.0f, (float) (((Entity) npc).height / 2))), 0, 0, 16, 0.0f, 0.0f, 0, new Color(), 1.5f);
        ++npc.localAI[3];
        if ((double) ((Entity) npc).Distance(((Entity) player).Center) >= 200.0 || !Collision.CanHitLine(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, ((Entity) player).position, ((Entity) player).width, ((Entity) player).height))
          return;
        this.FlightCD = 900;
        this.Flying = false;
        npc.noGravity = false;
        npc.noTileCollide = false;
        npc.localAI[3] = 0.0f;
        ((Entity) npc).velocity = Vector2.Zero;
      }

      void FlyToward(Vector2 v)
      {
        float num1 = 5f;
        float num2 = 25f;
        Vector2 vector2_1 = Vector2.op_Subtraction(v, ((Entity) npc).Center);
        if ((double) ((Vector2) ref vector2_1).Length() > (double) num2)
        {
          ((Vector2) ref vector2_1).Normalize();
          Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 6f);
          ((Entity) npc).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) npc).velocity, num1 - 1f), vector2_2), num1);
        }
        else
        {
          if (!Vector2.op_Equality(((Entity) npc).velocity, Vector2.Zero))
            return;
          ((Entity) npc).velocity.X = -0.15f;
          ((Entity) npc).velocity.Y = -0.05f;
        }
      }
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (npc.type != 85 && npc.type != 341)
        return;
      npc.dontTakeDamage = false;
      if (npc.justHit && Main.hardMode)
        this.InvulFrameTimer = 15;
      if (this.InvulFrameTimer <= 0)
        return;
      --this.InvulFrameTimer;
      npc.dontTakeDamage = true;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<MidasBuff>(), 600, true, false);
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      if (!FargoSoulsUtil.HostCheck)
        return;
      int num = 5;
      for (int index = 0; index < num; ++index)
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).position.X + (float) Main.rand.Next(((Entity) npc).width), ((Entity) npc).position.Y + (float) Main.rand.Next(((Entity) npc).height), (float) Main.rand.Next(-30, 31) * 0.1f, (float) Main.rand.Next(-40, -15) * 0.1f, ModContent.ProjectileType<FakeHeart>(), 20, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }
  }
}
