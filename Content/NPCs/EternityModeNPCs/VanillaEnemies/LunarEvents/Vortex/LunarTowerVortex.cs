// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Vortex.LunarTowerVortex
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Vortex
{
  public class LunarTowerVortex : LunarTowers
  {
    private int Vortex = -1;
    private const int IdleTime = 120;
    private readonly SoundStyle NukeBeep = new SoundStyle("FargowiltasSouls/Assets/Sounds/NukeBeep", (SoundType) 0);

    public override int ShieldStrength
    {
      get => NPC.ShieldStrengthTowerVortex;
      set => NPC.ShieldStrengthTowerVortex = value;
    }

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(422);

    public LunarTowerVortex()
      : base(ModContent.BuffType<JammedBuff>(), 229)
    {
    }

    public override int MaxHP => 65000;

    public override int Damage => 80;

    public override List<int> RandomAttacks
    {
      get
      {
        List<int> randomAttacks = new List<int>();
        CollectionsMarshal.SetCount<int>(randomAttacks, 4);
        Span<int> span = CollectionsMarshal.AsSpan<int>(randomAttacks);
        int num1 = 0;
        span[num1] = 2;
        int num2 = num1 + 1;
        span[num2] = 3;
        int num3 = num2 + 1;
        span[num3] = 4;
        int num4 = num3 + 1;
        span[num4] = 5;
        int num5 = num4 + 1;
        return randomAttacks;
      }
    }

    public override void ShieldsDownAI(NPC npc)
    {
      Player player = Main.player[npc.target];
      npc.ai[1] += 0.5f;
      if (npc.HasPlayerTarget && ((Entity) player).active)
      {
        switch (this.Attack)
        {
          case 0:
            this.Idle(npc, player);
            break;
          case 1:
            this.VortexVortex(npc, player);
            break;
          case 2:
            this.LightningBall(npc, player);
            break;
          case 3:
            this.SkyLightning(npc, player);
            break;
          case 4:
            this.LightningElderHu(npc, player);
            break;
          case 5:
            this.VortexShield(npc, player);
            break;
        }
      }
      else
      {
        if (this.Attack != 1)
          return;
        this.EndAttack(npc);
        foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => p.TypeAlive<FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Vortex.VortexVortex>())))
          projectile.Kill();
      }
    }

    private void VortexVortex(NPC npc, Player player)
    {
      Attack();

      void Attack()
      {
        if (FargoSoulsUtil.HostCheck && this.AttackTimer % 20 == 1)
        {
          bool flag = false;
          for (int index = 0; index < Main.maxProjectiles; ++index)
          {
            if (Main.projectile[index].type == ModContent.ProjectileType<FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Vortex.VortexVortex>() && ((Entity) Main.projectile[index]).whoAmI == this.Vortex)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) npc).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, (float) ((Entity) npc).height), 0.8f));
            this.Vortex = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2, Vector2.Zero, ModContent.ProjectileType<FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Vortex.VortexVortex>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 1f, 0.0f);
          }
        }
        if (!this.Vortex.IsWithinBounds(Main.maxProjectiles) || !((Entity) Main.projectile[this.Vortex]).active)
          return;
        ((Entity) Main.projectile[this.Vortex]).velocity = Vector2.op_Subtraction(Vector2.op_Subtraction(((Entity) npc).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, (float) ((Entity) npc).height), 0.8f)), ((Entity) Main.projectile[this.Vortex]).Center);
      }
    }

    private void LightningBall(NPC npc, Player player)
    {
      if (this.AttackTimer <= 60)
        Windup();
      else if (this.AttackTimer <= 300)
        Attack();
      else
        Endlag();
      if (this.AttackTimer <= 420)
        return;
      this.EndAttack(npc);

      static void Windup()
      {
      }

      void Attack()
      {
        if ((this.AttackTimer - 60) % 180 != 1)
          return;
        bool flag = false;
        if ((this.AttackTimer - 60) % 360 == 181)
          flag = true;
        if (!FargoSoulsUtil.HostCheck)
          return;
        Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) npc).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, (float) ((Entity) npc).height), 0.8f));
        Vector2 vector2_2 = flag ? Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo(vector2_1, ((Entity) player).Center), 6f) : Vector2.Zero;
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_1, Utils.RotatedBy(vector2_2, Math.PI / 2.0, new Vector2()), 465, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 3f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        if (!flag)
          return;
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_1, Utils.RotatedBy(vector2_2, -1.0 * Math.PI / 2.0, new Vector2()), 465, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 3f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }

      static void Endlag()
      {
      }
    }

    private void SkyLightning(NPC npc, Player player)
    {
      if (this.AttackTimer <= 0)
        Windup();
      else if (this.AttackTimer <= 312)
        Attack();
      else
        Endlag();
      if (this.AttackTimer <= 402)
        return;
      this.EndAttack(npc);

      static void Windup()
      {
      }

      void Attack()
      {
        if (this.AttackTimer % 120 != 1)
          return;
        bool flag = false;
        if (this.AttackTimer % 240 == 121)
          flag = true;
        SoundEngine.PlaySound(ref this.NukeBeep, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        for (int index = 0; index < 24; ++index)
        {
          int num1 = index;
          if (num1 >= 12)
            num1 = 11 - num1;
          if (FargoSoulsUtil.HostCheck)
          {
            int num2 = flag ? 90 : 0;
            Vector2 position = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(Vector2.UnitX, (float) (180 * num1 + num2)));
            this.SpawnLightning(npc, position);
          }
        }
      }

      static void Endlag()
      {
      }
    }

    private void LightningElderHu(NPC npc, Player player)
    {
      if (this.AttackTimer <= 0)
        Windup();
      else if (this.AttackTimer <= 360)
        Attack();
      else
        Endlag();
      if (this.AttackTimer <= 360)
        return;
      this.EndAttack(npc);

      static void Windup()
      {
      }

      void Attack()
      {
        if (this.AttackTimer % 30 != 0)
          return;
        int num = 12 - this.AttackTimer / 30;
        for (int index = -1; index < 2; index += 2)
        {
          Vector2 position = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(Vector2.UnitX, (float) (130 * num * index)));
          SoundEngine.PlaySound(ref this.NukeBeep, new Vector2?(position), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
            this.SpawnLightning(npc, position);
        }
      }

      static void Endlag()
      {
      }
    }

    private void VortexShield(NPC npc, Player player)
    {
      if (this.AttackTimer == 1)
      {
        npc.ai[3] = 1f;
        npc.netUpdate = true;
        PillarBehaviour.NetSync(npc);
      }
      npc.reflectsProjectiles = this.AttackTimer >= 40;
      if (npc.reflectsProjectiles)
      {
        for (int index = 0; index < 20; ++index)
        {
          Vector2 vector2 = new Vector2();
          double num = Main.rand.NextDouble() * 2.0 * Math.PI;
          vector2.X += (float) (Math.Sin(num) * (double) ((Entity) npc).height / 2.0);
          vector2.Y += (float) (Math.Cos(num) * (double) ((Entity) npc).height / 2.0);
          Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) npc).Center, vector2), new Vector2(4f, 4f)), 0, 0, 229, 0.0f, 0.0f, 100, Color.White, 1f)].noGravity = true;
        }
      }
      if (this.AttackTimer <= 180)
        return;
      npc.reflectsProjectiles = false;
      this.EndAttack(npc);
    }

    private void Idle(NPC npc, Player player)
    {
      if (this.AttackTimer <= 120)
        return;
      this.RandomAttack(npc);
    }

    private void SpawnLightning(NPC parent, Vector2 position)
    {
      if (!FargoSoulsUtil.HostCheck)
        return;
      Vector2 vector2_1 = position;
      vector2_1.Y = ((Entity) Main.player[parent.target]).Center.Y + 1050f;
      for (int index = 0; index < 14; ++index)
      {
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, 150f), (float) index));
        Projectile.NewProjectile(((Entity) parent).GetSource_FromThis((string) null), vector2_2, Vector2.Zero, ModContent.ProjectileType<LightningTelegraph>(), FargoSoulsUtil.ScaledProjectileDamage(parent.damage), 2f, Main.myPlayer, (float) index, 0.0f, 0.0f);
      }
    }

    public enum Attacks
    {
      Idle,
      VortexVortex,
      LightningBall,
      SkyLightning,
      LightningElderHu,
      VortexShield,
    }
  }
}
