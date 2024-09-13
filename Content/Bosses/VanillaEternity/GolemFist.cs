// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.GolemFist
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class GolemFist : GolemPart
  {
    public int FistAttackRateSlowdownTimer;
    public bool DoAttackOnFistImpact;

    public GolemFist()
      : base(9999)
    {
    }

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(247, 248);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.FistAttackRateSlowdownTimer);
      bitWriter.WriteBit(this.DoAttackOnFistImpact);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.FistAttackRateSlowdownTimer = binaryReader.Read7BitEncodedInt();
      this.DoAttackOnFistImpact = bitReader.ReadBit();
    }

    public override void SetDefaults(NPC npc)
    {
      base.SetDefaults(npc);
      npc.lifeMax *= 2;
      npc.damage = (int) ((double) npc.damage * 1.3);
    }

    public virtual bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
      return ProjectileID.Sets.IsAWhip[projectile.type] ? new bool?(false) : base.CanBeHitByProjectile(npc, projectile);
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      if (WorldSavingSystem.SwarmActive)
        return flag;
      if (npc.HasValidTarget && Golem.CheckTempleWalls(((Entity) Main.player[npc.target]).Center))
      {
        if ((double) npc.ai[0] == 1.0)
          this.FistAttackRateSlowdownTimer = 0;
        else if (++this.FistAttackRateSlowdownTimer < 90)
          npc.ai[1] = 0.0f;
        if ((double) ((Vector2) ref ((Entity) npc).velocity).Length() > 10.0)
        {
          NPC npc1 = npc;
          ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, Vector2.op_Multiply(Vector2.Normalize(((Entity) npc).velocity), ((Vector2) ref ((Entity) npc).velocity).Length() - 10f));
        }
      }
      if ((double) npc.ai[0] == 0.0 && this.DoAttackOnFistImpact)
      {
        this.DoAttackOnFistImpact = false;
        if ((!Golem.CheckTempleWalls(((Entity) Main.player[npc.target]).Center) || WorldSavingSystem.MasochistModeReal) && FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<MoonLordSunBlast>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      this.DoAttackOnFistImpact = (double) npc.ai[0] != 0.0;
      NPC npc2 = FargoSoulsUtil.NPCExists(NPC.golemBoss, new int[1]
      {
        245
      });
      if (npc2 != null && (double) npc.ai[0] == 0.0 && (double) ((Entity) npc).Distance(((Entity) npc2).Center) < (double) ((Entity) npc2).width * 1.5)
      {
        NPC npc3 = npc;
        ((Entity) npc3).position = Vector2.op_Addition(((Entity) npc3).position, ((Entity) npc2).velocity);
      }
      return flag;
    }

    public virtual bool? DrawHealthBar(
      NPC npc,
      byte hbPosition,
      ref float scale,
      ref Vector2 position)
    {
      return new bool?(false);
    }

    public override void SafeOnHitByProjectile(
      NPC npc,
      Projectile projectile,
      NPC.HitInfo hit,
      int damageDone)
    {
      base.SafeOnHitByProjectile(npc, projectile, hit, damageDone);
      if (!WorldSavingSystem.MasochistModeReal || projectile.maxPenetrate == 1 || !FargoSoulsUtil.CanDeleteProjectile(projectile))
        return;
      projectile.timeLeft = 0;
    }
  }
}
