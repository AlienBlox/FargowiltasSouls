// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Nebula.LunarTowerNebula
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Nebula
{
  public class LunarTowerNebula : LunarTowers
  {
    private Vector2 tpPos = Vector2.Zero;

    public override int ShieldStrength
    {
      get => NPC.ShieldStrengthTowerNebula;
      set => NPC.ShieldStrengthTowerNebula = value;
    }

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(507);

    public LunarTowerNebula()
      : base(ModContent.BuffType<ReverseManaFlowBuff>(), 58)
    {
    }

    public override int MaxHP => 60000;

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
          this.MirageDeathray(npc, player);
          break;
        case 2:
          this.TeleportJumpscare(npc, player);
          break;
        case 3:
          this.MassiveNebulaArcanum(npc, player);
          break;
      }
    }

    private void MirageDeathray(NPC npc, Player player)
    {
      if (this.AttackTimer <= 60)
        Windup();
      else if (this.AttackTimer <= 180)
        Attack();
      else
        Endlag();
      if (this.AttackTimer <= 180)
        return;
      this.EndAttack(npc);

      void Windup()
      {
        if (this.AttackTimer != 1 || !FargoSoulsUtil.HostCheck)
          return;
        int num1 = Main.rand.Next(4);
        float num2 = Utils.NextFloat(Main.rand);
        for (int index = 0; index < 4; ++index)
        {
          float num3 = (float) index + num2;
          int num4 = num1 != index ? 1 : 0;
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<NebulaPillarProj>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage * 2), 3f, Main.myPlayer, num3, (float) num4, (float) ((Entity) npc).whoAmI);
        }
      }

      static void Attack()
      {
      }

      static void Endlag()
      {
      }
    }

    private void TeleportJumpscare(NPC npc, Player player)
    {
      if (this.AttackTimer <= 100)
        Windup();
      else if (this.AttackTimer <= 145)
        Attack();
      else
        Endlag();
      if (this.AttackTimer <= 205)
        return;
      this.EndAttack(npc);

      void Windup()
      {
        if (this.AttackTimer == 1)
        {
          this.tpPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Vector2.UnitY, Utils.NextFloat(Main.rand, 0.0f, 300f))), Vector2.op_Multiply(Utils.NextFloat(Main.rand, -300f, 300f), Vector2.UnitX));
          SoundEngine.PlaySound(ref SoundID.NPCDeath58, new Vector2?(this.tpPos), (SoundUpdateCallback) null);
          npc.netUpdate = true;
          PillarBehaviour.NetSync(npc);
        }
        if (this.AttackTimer != 10)
          return;
        npc.netUpdate = true;
        PillarBehaviour.NetSync(npc);
        if (!FargoSoulsUtil.HostCheck)
          return;
        int num = 90;
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), this.tpPos, Vector2.Zero, ModContent.ProjectileType<NebulaTelegraph>(), 0, 0.0f, Main.myPlayer, (float) num, 0.0f, 0.0f);
        for (int index = -1; index < 2; index += 2)
        {
          float rotation = Utils.ToRotation(Utils.RotatedBy(Vector2.op_UnaryNegation(Vector2.UnitY), (double) index * (double) MathHelper.ToRadians(30f), new Vector2()));
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), this.tpPos, Vector2.Zero, ModContent.ProjectileType<BloomLine>(), 0, 0.0f, Main.myPlayer, 5f, rotation, (float) num);
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), this.tpPos, Vector2.Zero, ModContent.ProjectileType<BloomLine>(), 0, 0.0f, Main.myPlayer, 6f, (float) index, (float) num);
        }
      }

      void Attack()
      {
        if (this.AttackTimer - 100 == 1)
        {
          ((Entity) npc).Center = this.tpPos;
          npc.netUpdate = true;
          PillarBehaviour.NetSync(npc);
          SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(this.tpPos), (SoundUpdateCallback) null);
        }
        float num = (float) (((double) this.AttackTimer - 100.0) / 45.0);
        if ((this.AttackTimer - 100) % 2 != 0)
          return;
        SoundEngine.PlaySound(ref SoundID.Item20, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        if (!FargoSoulsUtil.HostCheck)
          return;
        for (int index = -1; index < 2; index += 2)
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitY, (double) index * (double) num * (double) MathHelper.ToRadians(150f), new Vector2()), 16f);
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<PillarNebulaBlaze>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 3f, Main.myPlayer, 0.02f, 0.0f, (float) ((Entity) npc).whoAmI);
        }
      }

      static void Endlag()
      {
      }
    }

    private void MassiveNebulaArcanum(NPC npc, Player player)
    {
      if (this.AttackTimer > 0)
      {
        if (this.AttackTimer <= 240)
          Attack();
        else
          Endlag();
      }
      if (this.AttackTimer <= 300)
        return;
      this.EndAttack(npc);

      void Attack()
      {
        if (this.AttackTimer != 1)
          return;
        SoundEngine.PlaySound(ref SoundID.Item117, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        if (!FargoSoulsUtil.HostCheck)
          return;
        Vector2 vector2_1 = Vector2.op_UnaryNegation(Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitY, (float) ((Entity) npc).height), 2f));
        Vector2 vector2_2 = Vector2.Normalize(vector2_1);
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, vector2_1), vector2_2, ModContent.ProjectileType<PillarArcanum>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 3f, Main.myPlayer, 0.0f, 1f, 0.0f);
      }

      static void Endlag()
      {
      }
    }

    private void Idle(NPC npc, Player player)
    {
      if (this.AttackTimer <= 0)
        Windup();
      else if (this.AttackTimer <= 120)
        Attack();
      else
        Endlag();
      if (this.AttackTimer <= 240)
        return;
      this.RandomAttack(npc);

      static void Windup()
      {
      }

      void Attack()
      {
        if (this.AttackTimer != 5 && this.AttackTimer != 115)
          return;
        SoundEngine.PlaySound(ref SoundID.Item20, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        if (!FargoSoulsUtil.HostCheck)
          return;
        for (int index = 0; index < 5; ++index)
        {
          float num1 = (float) index;
          if (this.AttackTimer == 115)
            num1 += 0.5f;
          Vector2 vector2_1 = Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, 6.2831854820251465 * (double) num1 / 5.0, new Vector2()), 1300f);
          float num2 = Utils.NextFloat(Main.rand, 5f, 6f) * 0.9f;
          Vector2 vector2_2 = Vector2.op_Addition(((Entity) player).Center, vector2_1);
          Vector2 vector2_3 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo(vector2_2, ((Entity) player).Center), num2);
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_2, vector2_3, ModContent.ProjectileType<PillarNebulaBlaze>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 3f, Main.myPlayer, 0.03f, 0.0f, (float) ((Entity) npc).whoAmI);
        }
      }

      static void Endlag()
      {
      }
    }

    public enum Attacks
    {
      Idle,
      MirageDeathray,
      TeleportJumpscare,
      MassiveNebulaArcanum,
    }
  }
}
