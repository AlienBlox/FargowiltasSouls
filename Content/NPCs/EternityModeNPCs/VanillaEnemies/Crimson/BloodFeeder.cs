// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Crimson.BloodFeeder
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Crimson
{
  public class BloodFeeder : EModeNPCBehaviour
  {
    public int AttackTimer;
    public int TrueMaxLife;
    public float DamageMultiplier = 1f;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(241);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.TrueMaxLife);
      binaryWriter.Write(this.DamageMultiplier);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.TrueMaxLife = binaryReader.Read7BitEncodedInt();
      this.DamageMultiplier = binaryReader.ReadSingle();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax *= 2;
      this.TrueMaxLife = npc.lifeMax;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      npc.damage = (int) ((double) npc.defDamage * (double) this.DamageMultiplier);
      if (++this.AttackTimer <= 60)
        return;
      this.AttackTimer = 0;
      if (!FargoSoulsUtil.HostCheck)
        return;
      bool flag = false;
      foreach (NPC npc1 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n =>
      {
        if (!((Entity) n).active || n.type == npc.type || n.boss || !n.CanBeChasedBy((object) null, false) || (double) ((Entity) npc).Distance(((Entity) n).Center) >= 300.0)
          return false;
        Rectangle hitbox = ((Entity) npc).Hitbox;
        return ((Rectangle) ref hitbox).Intersects(((Entity) n).Hitbox);
      })))
      {
        npc1.SimpleStrikeNPC(npc.damage, Math.Sign(((Entity) npc1).Center.X - ((Entity) npc).Center.X), false, 4f, (DamageClass) null, false, 0.0f, false);
        this.OnHitSomething(npc, npc.damage);
        flag = true;
      }
      if (!flag)
        return;
      EModeNPCBehaviour.NetSync(npc);
    }

    private void OnHitSomething(NPC npc, int damage)
    {
      npc.life += damage;
      if ((double) this.DamageMultiplier < 10.0)
      {
        npc.lifeMax += damage;
        this.TrueMaxLife += damage;
      }
      if (npc.life > npc.lifeMax)
        npc.life = npc.lifeMax;
      CombatText.NewText(((Entity) npc).Hitbox, CombatText.HealLife, damage * 2, false, false);
      this.DamageMultiplier += 0.2f;
      if ((double) this.DamageMultiplier > 10.0)
        this.DamageMultiplier = 10f;
      npc.damage = (int) ((double) npc.defDamage * (double) this.DamageMultiplier);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(30, 300, true, false);
      this.OnHitSomething(npc, ((Player.HurtInfo) ref hurtInfo).Damage);
      npc.netUpdate = true;
      EModeNPCBehaviour.NetSync(npc, false);
    }
  }
}
