// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar.LunarTowerSolar
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar
{
  public class LunarTowerSolar : LunarTowers
  {
    private Vector2 OriginalLocation;
    private bool HitFloor;
    private bool HitFloorEffect;
    private const int IdleTime = 120;

    public override int ShieldStrength
    {
      get => NPC.ShieldStrengthTowerSolar;
      set => NPC.ShieldStrengthTowerSolar = value;
    }

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(517);

    public LunarTowerSolar()
      : base(ModContent.BuffType<AtrophiedBuff>(), 259)
    {
    }

    public override int MaxHP => 70000;

    public override int Damage => 80;

    public override List<int> RandomAttacks
    {
      get
      {
        List<int> randomAttacks = new List<int>();
        CollectionsMarshal.SetCount<int>(randomAttacks, 3);
        Span<int> span = CollectionsMarshal.AsSpan<int>(randomAttacks);
        int num1 = 0;
        span[num1] = 1;
        int num2 = num1 + 1;
        span[num2] = 2;
        int num3 = num2 + 1;
        span[num3] = 3;
        int num4 = num3 + 1;
        return randomAttacks;
      }
    }

    public override void ShieldsDownAI(NPC npc)
    {
      Player player = Main.player[npc.target];
      if (!npc.HasPlayerTarget || !((Entity) player).active)
        return;
      switch (this.Attack)
      {
        case 0:
          this.Idle(npc, player);
          break;
        case 1:
          this.PillarSlam(npc, player);
          break;
        case 2:
          this.FireballVomit(npc, player);
          break;
        case 3:
          this.MeteorRain(npc, player);
          break;
      }
    }

    private void PillarSlam(NPC npc, Player player)
    {
      if (this.AttackTimer <= 60)
        Windup();
      else if (this.AttackTimer <= 360)
        Attack();
      if (this.AttackTimer <= 360)
        return;
      ((Entity) npc).velocity = Vector2.Zero;
      ((Entity) npc).Center = this.OriginalLocation;
      this.EndAttack(npc);

      void Windup()
      {
        if (this.AttackTimer <= 1)
        {
          this.OriginalLocation = ((Entity) npc).Center;
          this.HitFloor = false;
        }
        else
        {
          Vector2 vector2 = Vector2.op_Subtraction(this.OriginalLocation, Vector2.op_Multiply(Vector2.UnitY, 200f));
          ((Entity) npc).velocity = Vector2.op_Multiply(Vector2.op_Subtraction(vector2, ((Entity) npc).Center), 0.01f);
        }
      }

      void Attack()
      {
        if (this.AttackTimer <= 62)
        {
          ((Entity) npc).velocity = Vector2.Zero;
        }
        else
        {
          if (Collision.SolidCollision(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height))
          {
            this.HitFloor = true;
            this.HitFloorEffect = false;
          }
          if (this.HitFloor && !this.HitFloorEffect)
          {
            SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            for (int index = 0; index < 16; ++index)
            {
              int num1 = index;
              if (num1 >= 8)
                num1 = 7 - num1;
              if (FargoSoulsUtil.HostCheck)
              {
                float num2 = (float) Main.rand.Next(-32, 40);
                Vector2 vector2_1 = Vector2.op_Addition(this.OriginalLocation, Vector2.op_Multiply(Vector2.UnitX, (float) (160 * num1) + num2));
                Vector2 vector2_2 = Vector2.op_Multiply(Vector2.UnitY, 16f);
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_1, vector2_2, ModContent.ProjectileType<PillarSpawner>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 3f, Main.myPlayer, 1f, 0.0f, 0.0f);
              }
            }
            this.HitFloorEffect = true;
          }
          if (!this.HitFloor)
            ((Entity) npc).velocity.Y = 25f;
          else
            ((Entity) npc).velocity = Vector2.op_Multiply(Vector2.op_Subtraction(this.OriginalLocation, ((Entity) npc).Center), 0.05f);
        }
      }
    }

    private void FireballVomit(NPC npc, Player player)
    {
      if (this.AttackTimer <= 300)
        Attack();
      if (this.AttackTimer > 300)
        Endlag();
      if (this.AttackTimer <= 420)
        return;
      this.EndAttack(npc);

      void Attack()
      {
        if ((double) Math.Abs(npc.rotation) < Math.PI / 7.0)
        {
          int num = Math.Sign(((Entity) player).Center.X - ((Entity) npc).Center.X);
          npc.rotation += (float) ((double) num * (Math.PI / 16.0) / 120.0);
        }
        Fireball();
      }

      void Endlag()
      {
        npc.rotation *= 0.97f;
        if ((double) Math.Abs(npc.rotation) < Math.PI / 400.0)
          npc.rotation = 0.0f;
        Fireball();
      }

      void Fireball()
      {
        float num1 = (float) this.AttackTimer / 300f;
        if (this.AttackTimer % 18 != 0)
          return;
        SoundEngine.PlaySound(ref SoundID.Item45, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        if (!FargoSoulsUtil.HostCheck)
          return;
        double num2 = 16.0 * (double) Math.Min(1f, num1 * 1.5f);
        float num3 = npc.rotation - 1.57079637f;
        Vector2 vector2 = Utils.RotatedByRandom(Utils.ToRotationVector2(num3), Math.PI / 8.0);
        Vector2 velocity = Vector2.op_Multiply((float) num2, vector2);
        Vector2 spawnPos = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(num3), (float) ((Entity) npc).height), 0.45f));
        int index = FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), spawnPos, 519, velocity: velocity);
        if (!((Entity) Main.npc[index]).active)
          return;
        Main.npc[index].damage = npc.damage;
      }
    }

    private void MeteorRain(NPC npc, Player player)
    {
      if (this.AttackTimer <= 300)
        Attack();
      if (this.AttackTimer > 300)
        Endlag();
      if (this.AttackTimer <= 420)
        return;
      this.EndAttack(npc);

      void Attack()
      {
        float num = Math.Min(1f, (float) this.AttackTimer / 300f * 4f);
        npc.rotation = num * Utils.NextFloat(Main.rand, -1f * (float) Math.PI / 200f, (float) Math.PI / 200f);
        Meteor();
      }

      static void Endlag()
      {
      }

      void Meteor()
      {
        if (this.AttackTimer % 8 != 0 || !FargoSoulsUtil.HostCheck)
          return;
        float num1 = 0.3926991f;
        Vector2 vector2_1 = Utils.RotatedBy(new Vector2(0.0f, 8f), -(double) num1, new Vector2());
        int num2 = (int) (1000.0 * Math.Tan((double) num1));
        Vector2 vector2_2 = Vector2.op_Addition(((Entity) player).Center, new Vector2((float) Main.rand.Next(-2400 - num2, 2400 - num2), -1000f));
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_2, vector2_1, ModContent.ProjectileType<SolarMeteor>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 3f, Main.myPlayer, 0.0f, vector2_1.X, vector2_1.Y);
      }
    }

    private void Idle(NPC npc, Player player)
    {
      if (this.AttackTimer == 90 && FargoSoulsUtil.HostCheck)
      {
        Vector2 vector2 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) npc).Center);
        ((Vector2) ref vector2).Normalize();
        vector2 = Vector2.op_Multiply(vector2, 5f);
        for (int index = -2; index <= 2; ++index)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(vector2, (double) index * 0.78539818525314331, new Vector2()), 467, 40, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        if (NPC.CountNPCS(412) <= 0 && WorldSavingSystem.MasochistModeReal)
        {
          int index = NPC.NewNPC(((Entity) npc).GetSource_FromThis((string) null), (int) ((Entity) npc).Center.X, (int) ((double) ((Entity) npc).Center.Y - (double) ((Entity) npc).height * 0.44999998807907104), 412, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          if (((Entity) Main.npc[index]).active)
          {
            Main.npc[index].GivenName = "John Crawltipede";
            Main.npc[index].life *= 6;
          }
        }
      }
      if (this.AttackTimer <= 120)
        return;
      this.RandomAttack(npc);
    }

    public enum Attacks
    {
      Idle,
      PillarSlam,
      FireballVomit,
      MeteorRain,
    }
  }
}
