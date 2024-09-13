// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.EaterofWorldsHead
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class EaterofWorldsHead : EModeNPCBehaviour
  {
    public int FlamethrowerCDOrUTurnStoredTargetX;
    public int UTurnTotalSpacingDistance;
    public int UTurnIndividualSpacingPosition;
    public int UTurnAITimer;
    public static int UTurnCountdownTimer;
    public static int CursedFlameTimer;
    public static int HaveSpawnDR;
    public bool UTurn;
    public static bool DoTheWave;
    public bool DroppedSummon;
    public int NoSelfDestructTimer = 15;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(13);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.FlamethrowerCDOrUTurnStoredTargetX);
      binaryWriter.Write7BitEncodedInt(this.UTurnTotalSpacingDistance);
      binaryWriter.Write7BitEncodedInt(this.UTurnIndividualSpacingPosition);
      binaryWriter.Write7BitEncodedInt(this.UTurnAITimer);
      binaryWriter.Write7BitEncodedInt(EaterofWorldsHead.UTurnCountdownTimer);
      binaryWriter.Write7BitEncodedInt(EaterofWorldsHead.CursedFlameTimer);
      bitWriter.WriteBit(this.UTurn);
      bitWriter.WriteBit(EaterofWorldsHead.DoTheWave);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.FlamethrowerCDOrUTurnStoredTargetX = binaryReader.Read7BitEncodedInt();
      this.UTurnTotalSpacingDistance = binaryReader.Read7BitEncodedInt();
      this.UTurnIndividualSpacingPosition = binaryReader.Read7BitEncodedInt();
      this.UTurnAITimer = binaryReader.Read7BitEncodedInt();
      EaterofWorldsHead.UTurnCountdownTimer = binaryReader.Read7BitEncodedInt();
      EaterofWorldsHead.CursedFlameTimer = binaryReader.Read7BitEncodedInt();
      this.UTurn = bitReader.ReadBit();
      EaterofWorldsHead.DoTheWave = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.damage = (int) ((double) npc.damage * 4.0 / 3.0);
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
    {
      if (Main.getGoodWorld)
        cooldownSlot = 1;
      return base.CanHitPlayer(npc, target, ref cooldownSlot);
    }

    public override bool SafePreAI(NPC npc)
    {
      EModeGlobalNPC.eaterBoss = ((Entity) npc).whoAmI;
      FargoSoulsGlobalNPC.boss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return true;
      if (!npc.HasValidTarget || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > 3000.0)
      {
        ((Entity) npc).velocity.Y += 0.25f;
        if (npc.timeLeft > 120)
          npc.timeLeft = 120;
      }
      int firstNpc = NPC.FindFirstNPC(npc.type);
      if (((Entity) npc).whoAmI == firstNpc)
      {
        ++EaterofWorldsHead.UTurnCountdownTimer;
        if (EaterofWorldsHead.HaveSpawnDR > 0)
          --EaterofWorldsHead.HaveSpawnDR;
      }
      if (FargoSoulsUtil.HostCheck && ((Entity) npc).whoAmI == firstNpc && ++EaterofWorldsHead.CursedFlameTimer > 300)
      {
        bool flag = true;
        if (!WorldSavingSystem.MasochistModeReal)
        {
          for (int index = 0; index < Main.maxNPCs; ++index)
          {
            if (((Entity) Main.npc[index]).active && Main.npc[index].type == npc.type && Main.npc[index].GetGlobalNPC<EaterofWorldsHead>().UTurn)
            {
              flag = false;
              EaterofWorldsHead.CursedFlameTimer -= 30;
            }
          }
        }
        if (flag)
        {
          EaterofWorldsHead.CursedFlameTimer = 0;
          int num1 = WorldSavingSystem.MasochistModeReal ? 18 : 6;
          int num2 = 0;
          int num3 = 0;
          for (int index = 0; index < Main.maxNPCs; ++index)
          {
            if (((Entity) Main.npc[index]).active && (Main.npc[index].type == 13 || Main.npc[index].type == 14 || Main.npc[index].type == 15) && ++num2 > (WorldSavingSystem.MasochistModeReal ? 2 : 6))
            {
              num2 = 0;
              --num1;
              Vector2 vector2 = Vector2.op_Division(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) Main.npc[index]).Center), 45f);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) Main.npc[index]).Center, vector2, ModContent.ProjectileType<CursedFireballHoming>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, (float) npc.target, (float) num3, 0.0f);
              num3 += WorldSavingSystem.MasochistModeReal ? 4 : 10;
            }
          }
          for (int index = 0; index < num1; ++index)
          {
            Vector2 vector2 = Vector2.op_Division(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center), 45f);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<CursedFireballHoming>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, (float) npc.target, (float) num3, 0.0f);
            num3 += WorldSavingSystem.MasochistModeReal ? 4 : 8;
          }
        }
      }
      if (this.NoSelfDestructTimer <= 0)
      {
        if (FargoSoulsUtil.HostCheck && EaterofWorldsHead.UTurnCountdownTimer % 6 == 3)
        {
          int index = (int) npc.ai[0];
          if (index <= -1 || index >= Main.maxNPCs || !((Entity) Main.npc[index]).active || (double) Main.npc[index].ai[1] != (double) ((Entity) npc).whoAmI || Main.npc[index].type != 14 && Main.npc[index].type != 15)
          {
            npc.life = 0;
            npc.HitEffect(0, 10.0, new bool?());
            npc.checkDead();
            ((Entity) npc).active = false;
            npc.netUpdate = false;
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            return false;
          }
        }
      }
      else
        --this.NoSelfDestructTimer;
      if (!this.UTurn)
      {
        if (++this.FlamethrowerCDOrUTurnStoredTargetX >= 6)
        {
          this.FlamethrowerCDOrUTurnStoredTargetX = 0;
          if (WorldSavingSystem.MasochistModeReal && FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2 = Utils.RotatedBy(new Vector2(5f, 0.0f), (double) npc.rotation - Math.PI / 2.0 + (double) MathHelper.ToRadians((float) Main.rand.Next(-15, 16)), new Vector2());
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, 101, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
        if (((Entity) npc).whoAmI == firstNpc)
        {
          if (EaterofWorldsHead.UTurnCountdownTimer < -206 && WorldSavingSystem.MasochistModeReal)
            ++EaterofWorldsHead.UTurnCountdownTimer;
          if (EaterofWorldsHead.UTurnCountdownTimer == 610)
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
          if (EaterofWorldsHead.UTurnCountdownTimer > 700 && FargoSoulsUtil.HostCheck)
          {
            EaterofWorldsHead.UTurnCountdownTimer = 0;
            if (npc.HasValidTarget && (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) < 2400.0)
            {
              this.UTurn = true;
              EaterofWorldsHead.DoTheWave = !EaterofWorldsHead.DoTheWave;
              this.UTurnTotalSpacingDistance = NPC.CountNPCS(npc.type) / 2;
              if (WorldSavingSystem.MasochistModeReal)
                this.UTurnTotalSpacingDistance /= 2;
              int num = 0;
              bool flag = true;
              for (int index = 0; index < Main.maxNPCs; ++index)
              {
                if (((Entity) Main.npc[index]).active && Main.npc[index].type == npc.type)
                {
                  if (WorldSavingSystem.MasochistModeReal && index != ((Entity) npc).whoAmI)
                  {
                    flag = !flag;
                    if (!flag)
                      continue;
                  }
                  Main.npc[index].GetGlobalNPC<EaterofWorldsHead>().UTurnAITimer = !EaterofWorldsHead.DoTheWave || this.UTurnTotalSpacingDistance == 0 ? 0 : num * 90 / this.UTurnTotalSpacingDistance / 2 - 60;
                  if (WorldSavingSystem.MasochistModeReal)
                    Main.npc[index].GetGlobalNPC<EaterofWorldsHead>().UTurnAITimer += 60;
                  Main.npc[index].GetGlobalNPC<EaterofWorldsHead>().UTurnTotalSpacingDistance = this.UTurnTotalSpacingDistance;
                  Main.npc[index].GetGlobalNPC<EaterofWorldsHead>().UTurnIndividualSpacingPosition = num;
                  Main.npc[index].GetGlobalNPC<EaterofWorldsHead>().UTurn = true;
                  Main.npc[index].netUpdate = true;
                  EModeNPCBehaviour.NetSync(Main.npc[index]);
                  num *= -1;
                  if (num >= 0)
                    ++num;
                }
              }
              npc.netUpdate = true;
            }
          }
        }
        if (npc.HasPlayerTarget && !this.DroppedSummon)
        {
          Player player = Main.player[npc.target];
          if (!player.dead && player.FargoSouls().FreeEaterSummon)
          {
            player.FargoSouls().FreeEaterSummon = false;
            ModItem modItem;
            if (!NPC.downedBoss2 && FargoSoulsUtil.HostCheck && ModContent.TryFind<ModItem>("Fargowiltas", "WormyFood", ref modItem))
              Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) player).Hitbox, modItem.Type, 1, false, 0, false, false);
            this.DroppedSummon = true;
            EaterofWorldsHead.UTurnCountdownTimer = 0;
            EaterofWorldsHead.HaveSpawnDR = 180;
            ((Entity) npc).velocity.Y += 6f;
          }
        }
        return true;
      }
      if (++this.UTurnAITimer < 120)
      {
        Vector2 center = ((Entity) Main.player[npc.target]).Center;
        if (this.UTurnTotalSpacingDistance != 0)
          center.X += 900f / (float) this.UTurnTotalSpacingDistance * (float) this.UTurnIndividualSpacingPosition;
        center.Y += 600f;
        float num4 = 0.6f;
        float num5 = 24f;
        if ((double) ((Entity) npc).Top.Y > (double) ((Entity) Main.player[npc.target]).Bottom.Y + (double) ((Entity) npc).height)
        {
          num4 *= 1.5f;
          num5 *= 1.5f;
          NPC npc1 = npc;
          ((Entity) npc1).position = Vector2.op_Addition(((Entity) npc1).position, Vector2.op_Division(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).position, ((Entity) Main.player[npc.target]).oldPosition), 2f));
        }
        if ((double) ((Entity) npc).Center.X < (double) center.X)
        {
          ((Entity) npc).velocity.X += num4;
          if ((double) ((Entity) npc).velocity.X < 0.0)
            ((Entity) npc).velocity.X += num4 * 2f;
        }
        else
        {
          ((Entity) npc).velocity.X -= num4;
          if ((double) ((Entity) npc).velocity.X > 0.0)
            ((Entity) npc).velocity.X -= num4 * 2f;
        }
        if ((double) ((Entity) npc).Center.Y < (double) center.Y)
        {
          ((Entity) npc).velocity.Y += num4;
          if ((double) ((Entity) npc).velocity.Y < 0.0)
            ((Entity) npc).velocity.Y += num4 * 2f;
        }
        else
        {
          ((Entity) npc).velocity.Y -= num4;
          if ((double) ((Entity) npc).velocity.Y > 0.0)
            ((Entity) npc).velocity.Y -= num4 * 2f;
        }
        if ((double) Math.Abs(((Entity) npc).velocity.X) > (double) num5)
          ((Entity) npc).velocity.X = num5 * (float) Math.Sign(((Entity) npc).velocity.X);
        if ((double) Math.Abs(((Entity) npc).velocity.Y) > (double) num5)
          ((Entity) npc).velocity.Y = num5 * (float) Math.Sign(((Entity) npc).velocity.Y);
        npc.localAI[0] = 1f;
        if (Main.netMode == 2 && --npc.netSpam < 0)
        {
          npc.netSpam = 5;
          NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
      }
      else if (this.UTurnAITimer == 120)
      {
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
        ((Entity) npc).velocity = Vector2.op_Multiply(Vector2.UnitY, -15f);
        this.FlamethrowerCDOrUTurnStoredTargetX = (int) ((Entity) Main.player[npc.target]).Center.X;
        npc.netUpdate = true;
      }
      else if (this.UTurnAITimer < 240)
      {
        if ((double) ((Entity) npc).Center.Y < (double) ((Entity) Main.player[npc.target]).Center.Y - (WorldSavingSystem.MasochistModeReal ? 200.0 : 450.0))
          this.UTurnAITimer = 239;
      }
      else if (this.UTurnAITimer == 240)
      {
        Vector2 vector2;
        vector2.X = ((Entity) Main.player[npc.target]).Center.X;
        if (this.UTurnTotalSpacingDistance != 0)
          vector2.X += 900f / (float) this.UTurnTotalSpacingDistance * (float) this.UTurnIndividualSpacingPosition;
        vector2.Y = ((Entity) npc).Center.Y;
        float num = (float) (3.1415927410125732 * (double) (Math.Abs(vector2.X - ((Entity) npc).Center.X) / 2f) / 30.0);
        if ((double) num < 8.0)
          num = 8f;
        ((Entity) npc).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) npc).velocity), num);
        this.FlamethrowerCDOrUTurnStoredTargetX = Math.Sign(((Entity) Main.player[npc.target]).Center.X - (float) this.FlamethrowerCDOrUTurnStoredTargetX);
        npc.netUpdate = true;
      }
      else if (this.UTurnAITimer < 270)
        ((Entity) npc).velocity = Utils.RotatedBy(((Entity) npc).velocity, (double) MathHelper.ToRadians(6f) * (double) this.FlamethrowerCDOrUTurnStoredTargetX, new Vector2());
      else if (this.UTurnAITimer == 270)
      {
        ((Entity) npc).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) npc).velocity), 15f);
        npc.netUpdate = true;
      }
      else if (this.UTurnAITimer > 300)
      {
        this.UTurnAITimer = 0;
        EaterofWorldsHead.UTurnCountdownTimer = 0;
        this.UTurnTotalSpacingDistance = 0;
        this.UTurnIndividualSpacingPosition = 0;
        this.UTurn = false;
        npc.netUpdate = true;
      }
      npc.rotation = (float) Math.Atan2((double) ((Entity) npc).velocity.Y, (double) ((Entity) npc).velocity.X) + 1.57f;
      if (npc.netUpdate)
      {
        if (Main.netMode == 2)
        {
          NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          EModeNPCBehaviour.NetSync(npc);
        }
        npc.netUpdate = false;
      }
      return false;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      if (!Main.getGoodWorld)
        return;
      target.KillMe(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.FargowiltasSouls.DeathMessage.EOW", (object) target.name)), 999999.0, 0, false);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 2);
      EModeNPCBehaviour.LoadGoreRange(recolor, 24, 29);
    }
  }
}
