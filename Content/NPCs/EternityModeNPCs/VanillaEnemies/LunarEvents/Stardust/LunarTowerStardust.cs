// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Stardust.LunarTowerStardust
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.BossBars;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Stardust
{
  public class LunarTowerStardust : LunarTowers
  {
    private List<int> DragonParts;
    private bool gotBossBar;
    public const int CellAmount = 20;
    public float CellRotation;
    private int DragonTimer;
    private const int IdleTime = 60;

    public override int ShieldStrength
    {
      get => NPC.ShieldStrengthTowerStardust;
      set => NPC.ShieldStrengthTowerStardust = value;
    }

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(493);

    public LunarTowerStardust()
    {
      List<int> intList = new List<int>();
      CollectionsMarshal.SetCount<int>(intList, 6);
      Span<int> span = CollectionsMarshal.AsSpan<int>(intList);
      int num1 = 0;
      span[num1] = 454;
      int num2 = num1 + 1;
      span[num2] = 455;
      int num3 = num2 + 1;
      span[num3] = 456;
      int num4 = num3 + 1;
      span[num4] = 457;
      int num5 = num4 + 1;
      span[num5] = 458;
      int num6 = num5 + 1;
      span[num6] = 459;
      int num7 = num6 + 1;
      this.DragonParts = intList;
      // ISSUE: explicit constructor call
      base.\u002Ector(ModContent.BuffType<AntisocialBuff>(), 20);
    }

    public override int MaxHP => 20000;

    public override int Damage => 0;

    private string DragonName
    {
      get => Language.GetTextValue("Mods.FargowiltasSouls.NPCs.StardustDragon.DisplayName");
    }

    public virtual bool CheckDead(NPC npc)
    {
      foreach (NPC npc1 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && this.DragonParts.Contains(n.type))))
      {
        npc1.StrikeInstantKill();
        npc1.checkDead();
      }
      return base.CheckDead(npc);
    }

    public override List<int> RandomAttacks
    {
      get
      {
        List<int> randomAttacks = new List<int>();
        CollectionsMarshal.SetCount<int>(randomAttacks, 4);
        Span<int> span = CollectionsMarshal.AsSpan<int>(randomAttacks);
        int num1 = 0;
        span[num1] = 1;
        int num2 = num1 + 1;
        span[num2] = 2;
        int num3 = num2 + 1;
        span[num3] = 3;
        int num4 = num3 + 1;
        span[num4] = 4;
        int num5 = num4 + 1;
        return randomAttacks;
      }
    }

    public override void ShieldsDownAI(NPC npc)
    {
      if (!this.gotBossBar)
      {
        npc.BossBar = (IBigProgressBar) ModContent.GetInstance<CompositeBossBar>();
        this.gotBossBar = true;
      }
      int num1 = 0;
      int num2 = 0;
      foreach (NPC npc1 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == ModContent.NPCType<StardustMinion>() && (double) n.ai[2] == (double) ((Entity) npc).whoAmI)))
      {
        if (npc1.frame.Y == 0)
          ++num2;
        ++num1;
      }
      foreach (NPC npc2 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && this.DragonParts.Contains(n.type) && n.GivenName != this.DragonName)))
        npc2.GivenName = this.DragonName;
      if (num1 < 20)
      {
        for (int i = 0; i < 20; i++)
        {
          bool flag = false;
          using (IEnumerator<NPC> enumerator = ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == ModContent.NPCType<StardustMinion>() && (double) n.ai[2] == (double) ((Entity) npc).whoAmI && (double) n.ai[3] == (double) i)).GetEnumerator())
          {
            if (enumerator.MoveNext())
            {
              NPC current = enumerator.Current;
              flag = true;
            }
          }
          if (!flag)
          {
            this.SpawnMinion(npc, i);
            ++num2;
          }
        }
      }
      if (NPC.CountNPCS(454) <= 0 && WorldSavingSystem.MasochistModeReal && this.DragonTimer <= 0)
      {
        if (FargoSoulsUtil.HostCheck)
        {
          int index = NPC.NewNPC(((Entity) npc).GetSource_FromThis((string) null), (int) ((Entity) npc).Center.X, (int) ((double) ((Entity) npc).Center.Y - (double) ((Entity) npc).height * 0.44999998807907104), 454, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          if (((Entity) Main.npc[index]).active)
            Main.npc[index].GivenName = this.DragonName;
        }
        SoundEngine.PlaySound(ref SoundID.Item119, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        this.DragonTimer = 0;
      }
      ++this.DragonTimer;
      if (num2 > 0)
      {
        npc.defense = 99999999;
        npc.life = npc.lifeMax;
        Player player = Main.player[npc.target];
        if (!npc.HasPlayerTarget || !((Entity) player).active)
          return;
        switch (this.Attack)
        {
          case 0:
            this.Idle(npc, player);
            ++this.CellRotation;
            break;
          case 1:
            this.CellExpandContract(npc, player);
            ++this.CellRotation;
            break;
          case 2:
            this.CellRush(npc, player);
            ++this.CellRotation;
            break;
          case 3:
            this.CellCurves(npc, player);
            ++this.CellRotation;
            break;
          case 4:
            this.CellScissor(npc, player);
            break;
        }
      }
      else
      {
        if (npc.defense != 0)
        {
          this.CellState(1);
          npc.defense = 0;
          npc.ai[3] = 1f;
          npc.netUpdate = true;
          PillarBehaviour.NetSync(npc);
        }
        if (NPC.CountNPCS(454) > 0)
          return;
        if (FargoSoulsUtil.HostCheck)
        {
          int index = NPC.NewNPC(((Entity) npc).GetSource_FromThis((string) null), (int) ((Entity) npc).Center.X, (int) ((double) ((Entity) npc).Center.Y - (double) ((Entity) npc).height * 0.44999998807907104), 454, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          if (((Entity) Main.npc[index]).active)
          {
            Main.npc[index].GivenName = this.DragonName;
            Main.npc[index].dontTakeDamage = true;
          }
        }
        SoundEngine.PlaySound(ref SoundID.Item119, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      }
    }

    private void CellExpandContract(NPC npc, Player player)
    {
      if (this.AttackTimer <= 90)
        Windup();
      else if (this.AttackTimer <= 450)
        Attack();
      else
        Endlag();
      if (this.AttackTimer <= 510)
        return;
      this.EndAttack(npc);

      void Windup()
      {
        if (this.AttackTimer != 1)
          return;
        SoundEngine.PlaySound(ref SoundID.Item77, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        this.CellState(2);
      }

      void Attack()
      {
        if (this.AttackTimer - 90 != 1)
          return;
        SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        this.CellState(3);
      }

      static void Endlag()
      {
      }
    }

    private void CellRush(NPC npc, Player player)
    {
      Attack();
      if (this.AttackTimer <= 540)
        return;
      this.EndAttack(npc);

      void Attack()
      {
        if (this.AttackTimer == 1)
        {
          SoundEngine.PlaySound(ref SoundID.Item44, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          this.CellState(5);
        }
        if (this.AttackTimer != 240)
          return;
        SoundEngine.PlaySound(ref SoundID.Item96, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == ModContent.NPCType<StardustMinion>() && (double) n.ai[2] == (double) ((Entity) npc).whoAmI && (double) n.ai[1] != 6.0 && (double) n.ai[1] != 4.0)))
          npc.ai[1] = 6f;
      }
    }

    private void CellCurves(NPC npc, Player player)
    {
      if (this.AttackTimer <= 90)
        Windup();
      else if (this.AttackTimer <= 290)
        Attack();
      else
        Endlag();
      if (this.AttackTimer <= 650)
        return;
      this.EndAttack(npc);

      static void Endlag()
      {
      }

      void Windup()
      {
        if (this.AttackTimer != 1)
          return;
        SoundEngine.PlaySound(ref SoundID.Item88, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        this.CellState(1);
      }

      void Attack()
      {
        if (this.AttackTimer % 10 != 9)
          return;
        SoundEngine.PlaySound(ref SoundID.Item115, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        using (IEnumerator<NPC> enumerator = ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == ModContent.NPCType<StardustMinion>() && (double) n.ai[2] == (double) ((Entity) npc).whoAmI && (double) n.ai[1] != 10.0)).GetEnumerator())
        {
          if (!enumerator.MoveNext())
            return;
          enumerator.Current.ai[1] = 10f;
        }
      }
    }

    private void CellScissor(NPC npc, Player player)
    {
      if (this.AttackTimer <= 90)
        Windup();
      else if (this.AttackTimer <= 350)
        Attack();
      else
        Endlag();
      if (this.AttackTimer <= 470)
        return;
      this.EndAttack(npc);

      void Windup()
      {
        if (this.AttackTimer == 1)
        {
          SoundEngine.PlaySound(ref SoundID.Item113, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          this.CellState(7);
        }
        this.CellRotation = 45f;
      }

      void Attack()
      {
        if (this.AttackTimer - 90 != 1)
          return;
        SoundEngine.PlaySound(ref SoundID.Item113, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        this.CellState(8);
      }

      void Endlag()
      {
        if (this.AttackTimer - 90 - 260 != 1)
          return;
        this.CellState(9);
        this.CellRotation = 0.0f;
      }
    }

    private void CellState(int state)
    {
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        if (((Entity) Main.npc[index]).active && Main.npc[index].type == ModContent.NPCType<StardustMinion>())
          Main.npc[index].ai[1] = (float) state;
      }
    }

    private void SpawnMinion(NPC npc, int cell)
    {
      if (!FargoSoulsUtil.HostCheck)
        return;
      NPC npc1 = NPC.NewNPCDirect(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply((float) Main.rand.Next(-20, 20), Vector2.UnitX)), Vector2.op_Multiply((float) Main.rand.Next(-20, 20), Vector2.UnitY)), ModContent.NPCType<StardustMinion>(), 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
      npc1.ai[1] = 1f;
      npc1.ai[2] = (float) ((Entity) npc).whoAmI;
      npc1.ai[3] = (float) cell;
    }

    private void Idle(NPC npc, Player player)
    {
      if (this.AttackTimer == 1)
        this.CellState(1);
      if (this.AttackTimer <= 60)
        return;
      int num = 0;
      while (num < Main.maxNPCs)
        ++num;
      this.RandomAttack(npc);
    }

    public enum Attacks
    {
      Idle,
      CellExpandContract,
      CellRush,
      CellCurves,
      CellScissor,
    }
  }
}
