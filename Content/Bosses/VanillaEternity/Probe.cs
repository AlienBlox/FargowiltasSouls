// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.Probe
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class Probe : EModeNPCBehaviour
  {
    public int OrbitChangeTimer;
    public int OrbitDirection;
    public int AttackTimer;
    public float TargetOrbitRotation;
    public bool ShootLaser;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(139);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.OrbitChangeTimer);
      binaryWriter.Write7BitEncodedInt(this.OrbitDirection);
      binaryWriter.Write7BitEncodedInt(this.AttackTimer);
      binaryWriter.Write(this.TargetOrbitRotation);
      bitWriter.WriteBit(this.ShootLaser);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.OrbitChangeTimer = binaryReader.Read7BitEncodedInt();
      this.OrbitDirection = binaryReader.Read7BitEncodedInt();
      this.AttackTimer = binaryReader.Read7BitEncodedInt();
      this.TargetOrbitRotation = binaryReader.ReadSingle();
      this.ShootLaser = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.destroyBoss, 134))
        return;
      npc.lifeMax = (int) ((double) npc.lifeMax * 1.5);
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot)
    {
      return WorldSavingSystem.SwarmActive || !FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.destroyBoss, 134) || WorldSavingSystem.MasochistModeReal;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[68] = true;
      if (!Utils.NextBool(Main.rand, 4) || Luminance.Common.Utilities.Utilities.AnyBosses() || !npc.FargoSouls().CanHordeSplit)
        return;
      EModeGlobalNPC.Horde(npc, 8);
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      if (WorldSavingSystem.SwarmActive || !FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.destroyBoss, 134))
        return flag;
      if ((!WorldSavingSystem.MasochistModeReal || !Main.getGoodWorld) && (double) npc.localAI[0] > 30.0)
        npc.localAI[0] = -30f;
      if (WorldSavingSystem.MasochistModeReal && !this.ShootLaser)
        return flag;
      if (++this.OrbitChangeTimer > 120)
      {
        this.OrbitChangeTimer = 0;
        this.OrbitDirection = Utils.NextBool(Main.rand) ? 1 : -1;
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if (Main.npc[EModeGlobalNPC.destroyBoss].GetGlobalNPC<Destroyer>().IsCoiling)
        this.ShootLaser = false;
      if (npc.HasValidTarget)
      {
        if (this.ShootLaser)
        {
          if (this.AttackTimer == 0)
          {
            this.TargetOrbitRotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) Main.player[npc.target], ((Entity) npc).Center));
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
          }
          Vector2 vector2 = Vector2.op_Multiply(6f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center));
          int num1 = !WorldSavingSystem.EternityMode || !SoulConfig.Instance.BossRecolors ? 90 : 88;
          float num2 = (float) (0.5 + 2.5 * (double) this.AttackTimer / 110.0);
          int index = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, num1, 2f * vector2.X, 2f * vector2.Y, 0, new Color(), num2);
          Main.dust[index].noGravity = true;
          if (++this.AttackTimer > 110)
          {
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, 100, (int) (1.1 * (double) FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage)), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            this.AttackTimer = 0;
            this.ShootLaser = false;
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
          }
        }
        float num3 = this.ShootLaser ? 440f : 220f;
        Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center), Vector2.op_Multiply(num3, this.ShootLaser ? Utils.RotatedBy(Vector2.UnitX, (double) this.TargetOrbitRotation, new Vector2()) : Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) Main.player[npc.target], ((Entity) npc).Center), (double) MathHelper.ToRadians(22f) * (double) this.OrbitDirection, new Vector2())));
        NPC npc1 = npc;
        ((Entity) npc1).position = Vector2.op_Addition(((Entity) npc1).position, Vector2.op_Division(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).position, ((Entity) Main.player[npc.target]).oldPosition), 3f));
        if ((double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) < 150.0)
        {
          ((Entity) npc).velocity = Vector2.op_Division(vector2_1, 20f);
        }
        else
        {
          ((Vector2) ref vector2_1).Normalize();
          Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 12f);
          float num4 = 0.7f;
          if (this.ShootLaser)
          {
            vector2_2 = Vector2.op_Multiply(vector2_2, 1.5f);
            num4 *= 1.5f;
          }
          if ((double) ((Entity) npc).velocity.X < (double) vector2_2.X)
          {
            ((Entity) npc).velocity.X += num4;
            if ((double) ((Entity) npc).velocity.X < 0.0 && (double) vector2_2.X > 0.0)
              ((Entity) npc).velocity.X += num4;
          }
          else if ((double) ((Entity) npc).velocity.X > (double) vector2_2.X)
          {
            ((Entity) npc).velocity.X -= num4;
            if ((double) ((Entity) npc).velocity.X > 0.0 && (double) vector2_2.X < 0.0)
              ((Entity) npc).velocity.X -= num4;
          }
          if ((double) ((Entity) npc).velocity.Y < (double) vector2_2.Y)
          {
            ((Entity) npc).velocity.Y += num4;
            if ((double) ((Entity) npc).velocity.Y < 0.0 && (double) vector2_2.Y > 0.0)
              ((Entity) npc).velocity.Y += num4;
          }
          else if ((double) ((Entity) npc).velocity.Y > (double) vector2_2.Y)
          {
            ((Entity) npc).velocity.Y -= num4;
            if ((double) ((Entity) npc).velocity.Y > 0.0 && (double) vector2_2.Y < 0.0)
              ((Entity) npc).velocity.Y -= num4;
          }
        }
      }
      return flag;
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.destroyBoss, 134))
        return base.CheckDead(npc);
      ((Entity) npc).active = false;
      if (npc.DeathSound.HasValue)
      {
        SoundStyle soundStyle = npc.DeathSound.Value;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      }
      return false;
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadSpecial(recolor, ref TextureAssets.Probe, ref FargowiltasSouls.FargowiltasSouls.TextureBuffer.Probe, nameof (Probe));
    }
  }
}
