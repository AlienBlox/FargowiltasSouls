// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.LunarTowers
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Nebula;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Stardust;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Vortex;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents
{
  public abstract class LunarTowers : PillarBehaviour
  {
    protected readonly int DebuffNotToInflict;
    protected readonly int AuraDust;
    public int AuraSize = 5000;
    public int AttackTimer;
    public int HealCounter;
    public int AuraSync;
    public bool SpawnedDuringLunarEvent;
    public int Attack;
    public int OldAttack;
    public bool spawned;

    public abstract int ShieldStrength { get; set; }

    public abstract int MaxHP { get; }

    public abstract int Damage { get; }

    protected LunarTowers(int debuffNotToInflict, int auraDust)
    {
      this.DebuffNotToInflict = debuffNotToInflict;
      this.AuraDust = auraDust;
    }

    public abstract void ShieldsDownAI(NPC npc);

    public abstract List<int> RandomAttacks { get; }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax = this.MaxHP;
      npc.damage = this.Damage;
    }

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      if (!WorldSavingSystem.EternityMode)
        return;
      binaryWriter.Write7BitEncodedInt(this.AttackTimer);
      binaryWriter.Write7BitEncodedInt(this.Attack);
      bitWriter.WriteBit(this.SpawnedDuringLunarEvent);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      if (!WorldSavingSystem.EternityMode)
        return;
      this.AttackTimer = binaryReader.Read7BitEncodedInt();
      this.Attack = binaryReader.Read7BitEncodedInt();
      this.SpawnedDuringLunarEvent = bitReader.ReadBit();
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (!WorldSavingSystem.EternityMode)
        return;
      if (npc.type == 493)
        npc.ai[1] = 1000f;
      npc.buffImmune[68] = true;
      npc.buffImmune[ModContent.BuffType<ClippedWingsBuff>()] = true;
    }

    public bool AnyPlayerWithin(NPC npc, int range)
    {
      foreach (Player player in ((IEnumerable<Player>) Main.player).Where<Player>((Func<Player, bool>) (x => ((Entity) x).active && !x.dead)))
      {
        if ((double) ((Entity) npc).Distance(((Entity) player).Center) <= (double) range)
          return true;
      }
      return false;
    }

    public virtual void AI(NPC npc)
    {
      if ((npc.type != 517 ? 0 : (this.Attack == 1 ? 1 : 0)) == 0)
        base.AI(npc);
      if (!WorldSavingSystem.EternityMode || !NPC.LunarApocalypseIsUp)
        return;
      if (npc.type == 493)
        npc.ai[1] = 1000f;
      if (!this.spawned)
      {
        npc.lifeMax = npc.life = this.MaxHP;
        npc.damage = this.Damage;
        this.spawned = true;
        this.SpawnedDuringLunarEvent = NPC.LunarApocalypseIsUp;
        npc.damage += 150;
        npc.defDamage += 150;
        npc.netUpdate = true;
        npc.buffImmune[ModContent.BuffType<ClippedWingsBuff>()] = true;
      }
      if (npc.dontTakeDamage && (double) ((Entity) npc).velocity.Y > 1.0)
        ((Entity) npc).velocity.Y = 1f;
      if (this.SpawnedDuringLunarEvent && this.ShieldStrength > NPC.LunarShieldPowerMax)
        this.ShieldStrength = NPC.LunarShieldPowerMax;
      if (this.SpawnedDuringLunarEvent)
      {
        Aura(ModContent.BuffType<AtrophiedBuff>());
        Aura(ModContent.BuffType<JammedBuff>());
        Aura(ModContent.BuffType<ReverseManaFlowBuff>());
        Aura(ModContent.BuffType<AntisocialBuff>());
        if (++this.AuraSync > 60)
        {
          this.AuraSync -= 600;
          PillarBehaviour.NetSync(npc);
        }
      }
      if (++this.HealCounter > 60)
      {
        this.HealCounter = 0;
        npc.TargetClosest(false);
        if (!npc.HasValidTarget || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > (double) this.AuraSize)
        {
          npc.life += 5000;
          if (npc.life > npc.lifeMax)
            npc.life = npc.lifeMax;
          CombatText.NewText(((Entity) npc).Hitbox, CombatText.HealLife, 5000, false, false);
        }
      }
      bool flag = this.AnyPlayerWithin(npc, this.AuraSize);
      if (flag)
      {
        ++this.AttackTimer;
        npc.defense = npc.defDefense;
      }
      if (npc.dontTakeDamage)
      {
        this.AuraSize = 5000;
        if (flag && this.ShieldStrength <= 70)
        {
          foreach (NPC npc1 in Main.npc)
          {
            int[] source;
            switch (npc.type)
            {
              case 422:
                source = VortexEnemies.VortexEnemyIDs;
                break;
              case 493:
                source = StardustEnemies.StardustEnemyIDs;
                break;
              case 507:
                source = NebulaEnemies.NebulaEnemyIDs;
                break;
              case 517:
                source = SolarEnemies.SolarEnemyIDs;
                break;
              default:
                ((ModType) this).Mod.Logger.Warn((object) ("Lunar Pillar eternity behavior: NPC type of " + npc.TypeName + " does not match any of the Pillars (why is this running?)"));
                return;
            }
            if (((IEnumerable<int>) source).Contains<int>(npc1.type))
              npc1.StrikeInstantKill();
          }
          this.ShieldStrength = 0;
        }
        if (npc.life >= npc.lifeMax)
          return;
        npc.life = npc.lifeMax;
      }
      else
      {
        if (this.AuraSize > 1500)
          this.AuraSize -= 40;
        else
          this.AuraSize = 1500;
        if (flag)
        {
          this.ShieldsDownAI(npc);
        }
        else
        {
          if (npc.type != 422 || this.Attack != 1)
            return;
          this.EndAttack(npc);
          foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => p.TypeAlive<VortexVortex>())))
            projectile.Kill();
        }
      }

      void Aura(int debuff)
      {
        if (this.DebuffNotToInflict == debuff)
          return;
        EModeGlobalNPC.Aura(npc, (float) this.AuraSize, debuff, dustid: this.AuraDust, color: new Color());
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, true, false);
    }

    public override void ModifyHitByAnything(
      NPC npc,
      Player player,
      ref NPC.HitModifiers modifiers)
    {
      base.ModifyHitByAnything(npc, player, ref modifiers);
      if (!WorldSavingSystem.EternityMode)
        return;
      if ((double) ((Entity) npc).Distance(((Entity) player).Center) > (double) this.AuraSize)
      {
        modifiers.Null();
      }
      else
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Division(local, 2f);
      }
    }

    public void RandomAttack(NPC npc)
    {
      npc.TargetClosest(false);
      this.Attack = Utils.Next<int>(Main.rand, (IList<int>) this.RandomAttacks);
      while (this.Attack == this.OldAttack)
        this.Attack = Utils.Next<int>(Main.rand, (IList<int>) this.RandomAttacks);
      this.OldAttack = this.Attack;
      if ((double) npc.life < (double) npc.lifeMax * 0.30000001192092896 && npc.type == 422)
        this.Attack = 1;
      this.AttackTimer = 0;
      PillarBehaviour.NetSync(npc);
    }

    public void EndAttack(NPC npc)
    {
      npc.TargetClosest(false);
      PillarBehaviour.NetSync(npc);
      this.Attack = 0;
      this.AttackTimer = 0;
    }
  }
}
