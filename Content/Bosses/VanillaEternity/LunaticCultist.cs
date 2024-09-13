// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.LunaticCultist
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class LunaticCultist : EModeNPCBehaviour
  {
    public float RitualRotation;
    public int MagicDamageCounter;
    public int MeleeDamageCounter;
    public int RangedDamageCounter;
    public int MinionDamageCounter;
    public bool EnteredPhase2;
    public bool DroppedSummon;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(439);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write(this.RitualRotation);
      binaryWriter.Write7BitEncodedInt(this.MeleeDamageCounter);
      binaryWriter.Write7BitEncodedInt(this.RangedDamageCounter);
      binaryWriter.Write7BitEncodedInt(this.MagicDamageCounter);
      binaryWriter.Write7BitEncodedInt(this.MinionDamageCounter);
      bitWriter.WriteBit(this.EnteredPhase2);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.RitualRotation = binaryReader.ReadSingle();
      this.MeleeDamageCounter = binaryReader.Read7BitEncodedInt();
      this.RangedDamageCounter = binaryReader.Read7BitEncodedInt();
      this.MagicDamageCounter = binaryReader.Read7BitEncodedInt();
      this.MinionDamageCounter = binaryReader.Read7BitEncodedInt();
      this.EnteredPhase2 = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax = (int) ((double) npc.lifeMax * 5.0 / 3.0);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[68] = true;
    }

    public virtual bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
      return (double) npc.ai[3] == -1.0 && FargoSoulsUtil.IsSummonDamage(projectile) && !ProjectileID.Sets.IsAWhip[projectile.type] ? new bool?(false) : base.CanBeHitByProjectile(npc, projectile);
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      EModeGlobalNPC.cultBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return flag;
      if ((double) npc.ai[3] == -1.0)
      {
        if ((double) npc.ai[0] == 5.0)
        {
          if ((double) npc.ai[1] == 1.0)
          {
            this.RitualRotation = (float) Main.rand.Next(360);
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
          }
          if ((double) npc.ai[1] > 30.0 && (double) npc.ai[1] < 330.0)
          {
            this.RitualRotation += 2f - Math.Min(1f, 2f * (float) npc.life / (float) npc.lifeMax);
            ((Entity) npc).Center = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(180f, Utils.RotatedBy(Vector2.UnitX, (double) MathHelper.ToRadians(this.RitualRotation), new Vector2())));
          }
        }
      }
      else
      {
        if (!this.EnteredPhase2 && npc.life < npc.lifeMax / 2)
        {
          this.EnteredPhase2 = true;
          npc.ai[0] = 5f;
          npc.ai[1] = 0.0f;
          npc.ai[2] = 0.0f;
          npc.ai[3] = -1f;
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
          }
        }
        int num1 = Math.Max(75, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage));
        switch (npc.ai[0])
        {
          case -1f:
            if ((double) npc.ai[1] == 419.0)
            {
              npc.ai[0] = 0.0f;
              npc.ai[1] = 0.0f;
              npc.ai[3] = 11f;
              npc.netUpdate = true;
              break;
            }
            break;
          case 2f:
            if (this.EnteredPhase2)
            {
              if ((double) npc.ai[1] < 60.0 && (double) npc.ai[1] % 4.0 == 3.0)
              {
                int num2 = 14 - (int) ((double) npc.ai[1] - 3.0) / 4;
                for (int index1 = -1; index1 <= 1; index1 += 2)
                {
                  for (int index2 = -1; index2 <= 1; index2 += 2)
                  {
                    if (index2 != 0)
                    {
                      Vector2 center = ((Entity) Main.player[npc.target]).Center;
                      center.X += (float) (Math.Sign(index2) * 150 * 2 + index2 * 120 * num2);
                      center.Y -= (float) ((700 + Math.Abs(index2) * 50) * index1);
                      float num3 = (float) (8.0 + (double) num2 * 0.800000011920929);
                      if (FargoSoulsUtil.HostCheck)
                        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, num3), (float) index1), 348, num1 / 3, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                    }
                  }
                }
                break;
              }
              break;
            }
            if ((double) npc.ai[1] == (WorldSavingSystem.MasochistModeReal ? 5.0 : 60.0) && FargoSoulsUtil.HostCheck)
            {
              for (int index = 0; index < Main.maxNPCs; ++index)
              {
                if (((Entity) Main.npc[index]).active && Main.npc[index].type == 440)
                {
                  Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) Main.npc[index]).Center);
                  ((Vector2) ref vector2_1).Normalize();
                  Vector2 vector2_2 = Utils.RotatedByRandom(Vector2.op_Multiply(vector2_1, Utils.NextFloat(Main.rand, 8f, 9f)), Math.PI / 24.0);
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.npc[index]).Center, vector2_2, 348, num1 / 3, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
              }
              break;
            }
            break;
          case 3f:
            if (FargoSoulsUtil.HostCheck)
            {
              if (this.EnteredPhase2 && (double) npc.ai[1] == 3.0)
              {
                int num4 = NPC.CountNPCS(440) * 2 + 6;
                Vector2 vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center);
                for (int index = 0; index < num4; ++index)
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(1200f, Utils.RotatedBy(vector2, 2.0 * Math.PI / (double) num4 * (double) index, new Vector2()))), Vector2.op_Multiply(-7f, Utils.RotatedBy(vector2, 2.0 * Math.PI / (double) num4 * (double) index, new Vector2())), ModContent.ProjectileType<CultistFireball>(), num1 / 3, 0.0f, Main.myPlayer, 171.428574f, 0.0f, 0.0f);
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, (float) npc.type, 0.0f);
              }
              if ((double) npc.ai[1] % 20.0 == 6.0)
              {
                for (int index = 0; index < Main.maxNPCs; ++index)
                {
                  if (((Entity) Main.npc[index]).active && Main.npc[index].type == 440)
                    FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) Main.npc[index]).Center, 516, target: npc.target, velocity: new Vector2());
                }
                break;
              }
              break;
            }
            break;
          case 4f:
            if ((double) npc.ai[1] == 19.0 && npc.HasPlayerTarget && FargoSoulsUtil.HostCheck)
            {
              int num5 = 1;
              for (int index = 0; index < Main.maxNPCs; ++index)
              {
                if (((Entity) Main.npc[index]).active && Main.npc[index].type == 440)
                {
                  if (this.EnteredPhase2)
                  {
                    Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.npc[index]).Center, Utils.NextVector2Square(Main.rand, -15f, 15f), ModContent.ProjectileType<CultistVortex>(), num1 * 6 / 15, 0.0f, Main.myPlayer, 0.0f, (float) num5, 0.0f);
                    ++num5;
                  }
                  else if (WorldSavingSystem.MasochistModeReal)
                  {
                    Vector2 vector2_3 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) Main.npc[index]).Center);
                    float num6 = (float) Main.rand.Next(100);
                    Vector2 vector2_4 = Vector2.op_Multiply(Vector2.Normalize(Utils.RotatedByRandom(vector2_3, Math.PI / 4.0)), 24f);
                    Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.npc[index]).Center, vector2_4, ModContent.ProjectileType<HostileLightning>(), num1 * 6 / 15, 0.0f, Main.myPlayer, Utils.ToRotation(vector2_3), num6, 0.0f);
                  }
                  else
                  {
                    Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) Main.npc[index], ((Entity) Main.player[npc.target]).Center), (double) MathHelper.ToRadians(5f)), Utils.NextFloat(Main.rand, 4f, 6f));
                    Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.npc[index]).Center, vector2, ModContent.ProjectileType<LightningVortexHostile>(), num1 * 6 / 15, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  }
                }
              }
              break;
            }
            break;
          case 7f:
            if ((double) npc.ai[1] == 3.0 && FargoSoulsUtil.HostCheck)
            {
              for (int index = 0; index < Main.maxProjectiles; ++index)
              {
                if (((Entity) Main.projectile[index]).active && Main.projectile[index].type == ModContent.ProjectileType<CultistRitual>())
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), new Vector2(((Entity) Main.projectile[index]).Center.X, ((Entity) Main.player[npc.target]).Center.Y - 700f), Vector2.Zero, ModContent.ProjectileType<StardustRain>(), num1 / 3, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
              break;
            }
            break;
          case 8f:
            if ((double) npc.ai[1] == 3.0)
            {
              int index3 = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
              if (index3 != -1 && ((Entity) Main.player[index3]).active)
              {
                for (int index4 = 0; index4 < Main.maxNPCs; ++index4)
                {
                  if (((Entity) Main.npc[index4]).active && Main.npc[index4].type == 440)
                    Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.npc[index4]).Center, Vector2.Zero, 575, num1 * 6 / 15, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
                break;
              }
              break;
            }
            break;
        }
      }
      npc.defense = npc.defDefense;
      Lighting.AddLight(((Entity) npc).Center, 1f, 1f, 1f);
      EModeUtils.DropSummon(npc, "CultistSummon", NPC.downedAncientCultist, ref this.DroppedSummon, NPC.downedGolemBoss);
      return flag;
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot) => false;

    public override void SafeOnHitByItem(
      NPC npc,
      Player player,
      Item item,
      NPC.HitInfo hit,
      int damageDone)
    {
      base.SafeOnHitByItem(npc, player, item, hit, damageDone);
      if (item.CountsAsClass(DamageClass.Melee) || item.CountsAsClass(DamageClass.Throwing))
        this.MeleeDamageCounter += ((NPC.HitInfo) ref hit).Damage;
      if (item.CountsAsClass(DamageClass.Ranged))
        this.RangedDamageCounter += ((NPC.HitInfo) ref hit).Damage;
      if (item.CountsAsClass(DamageClass.Magic))
        this.MagicDamageCounter += ((NPC.HitInfo) ref hit).Damage;
      if (!item.CountsAsClass(DamageClass.Summon))
        return;
      this.MinionDamageCounter += ((NPC.HitInfo) ref hit).Damage;
    }

    public override void SafeOnHitByProjectile(
      NPC npc,
      Projectile projectile,
      NPC.HitInfo hit,
      int damageDone)
    {
      base.SafeOnHitByProjectile(npc, projectile, hit, damageDone);
      if (projectile.CountsAsClass(DamageClass.Melee) || projectile.CountsAsClass(DamageClass.Throwing))
        this.MeleeDamageCounter += ((NPC.HitInfo) ref hit).Damage;
      if (projectile.CountsAsClass(DamageClass.Ranged))
        this.RangedDamageCounter += ((NPC.HitInfo) ref hit).Damage;
      if (projectile.CountsAsClass(DamageClass.Magic))
        this.MagicDamageCounter += ((NPC.HitInfo) ref hit).Damage;
      if (!FargoSoulsUtil.IsSummonDamage(projectile))
        return;
      this.MinionDamageCounter += ((NPC.HitInfo) ref hit).Damage;
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 24);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 31);
      EModeNPCBehaviour.LoadGoreRange(recolor, 902, 903);
      EModeNPCBehaviour.LoadExtra(recolor, 30);
    }
  }
}
