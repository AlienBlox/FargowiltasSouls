// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.Betsy
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class Betsy : EModeNPCBehaviour
  {
    public int EntranceTimer;
    public int FuryRingTimer;
    public int FuryRingShotRotationCounter;
    public bool DoFuryRingAttack;
    public bool InFuryRingAttackCooldown;
    public bool InPhase2;
    public bool DroppedSummon;
    private static readonly List<LocalizedText> MasoTexts;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(551);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      binaryWriter.Write7BitEncodedInt(this.FuryRingTimer);
      binaryWriter.Write7BitEncodedInt(this.FuryRingShotRotationCounter);
      bitWriter.WriteBit(this.DoFuryRingAttack);
      bitWriter.WriteBit(this.InFuryRingAttackCooldown);
      bitWriter.WriteBit(this.InPhase2);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      this.FuryRingTimer = binaryReader.Read7BitEncodedInt();
      this.FuryRingShotRotationCounter = binaryReader.Read7BitEncodedInt();
      this.DoFuryRingAttack = bitReader.ReadBit();
      this.InFuryRingAttackCooldown = bitReader.ReadBit();
      this.InPhase2 = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      npc.boss = true;
      npc.lifeMax = (int) Math.Round((double) npc.lifeMax * 4.0 / 3.0);
    }

    public override bool SafePreAI(NPC npc)
    {
      EModeGlobalNPC.betsyBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return true;
      if (this.EntranceTimer == 0)
        SoundEngine.PlaySound(ref SoundID.DD2_BetsyScream, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      if (this.EntranceTimer < 120)
      {
        ++this.EntranceTimer;
        npc.dontTakeDamage = true;
        npc.TargetClosest(false);
        npc.spriteDirection = Math.Sign(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center).X);
        npc.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center));
        if (npc.spriteDirection == -1)
          npc.rotation += 3.14159274f;
        return false;
      }
      if (this.EntranceTimer == 120)
        npc.dontTakeDamage = false;
      if (WorldSavingSystem.MasochistModeReal && Main.getGoodWorld)
      {
        for (int index1 = 0; index1 < 3; ++index1)
        {
          Rectangle rectangle;
          // ISSUE: explicit constructor call
          ((Rectangle) ref rectangle).\u002Ector((int) Main.screenPosition.X + Main.screenWidth / 3, (int) Main.screenPosition.Y + Main.screenHeight / 3, Main.screenWidth / 3, Main.screenHeight / 3);
          int index2 = Main.rand.Next(0, 27);
          string str1;
          if (index2 >= 25)
          {
            LocalizedText masoText = Betsy.MasoTexts[index2];
            object[] objArray = new object[1];
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 5);
            interpolatedStringHandler.AppendFormatted<int>(Main.rand.Next(10));
            interpolatedStringHandler.AppendFormatted<int>(Main.rand.Next(10));
            interpolatedStringHandler.AppendFormatted<int>(Main.rand.Next(10));
            interpolatedStringHandler.AppendFormatted<int>(Main.rand.Next(10));
            interpolatedStringHandler.AppendFormatted<int>(Main.rand.Next(10));
            objArray[0] = (object) interpolatedStringHandler.ToStringAndClear();
            str1 = masoText.Format(objArray);
          }
          else
            str1 = Betsy.MasoTexts[index2].Value;
          string str2 = str1;
          CombatText.NewText(rectangle, new Color(100 + Main.rand.Next(150), 100 + Main.rand.Next(150), 100 + Main.rand.Next(150)), str2, Utils.NextBool(Main.rand), Utils.NextBool(Main.rand));
        }
        if (Utils.NextBool(Main.rand, 30) && npc.HasPlayerTarget)
        {
          switch (Main.rand.Next(12))
          {
            case 0:
              if (!Main.dedServ)
              {
                SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Thunder", (SoundType) 0);
                SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
                break;
              }
              break;
            case 1:
              SoundEngine.PlaySound(ref SoundID.ScaryScream, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
              break;
            case 2:
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
              break;
            case 3:
              SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
              break;
            case 4:
              if (!Main.dedServ)
              {
                SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Monster94", (SoundType) 0);
                SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
                break;
              }
              break;
            case 5:
              if (!Main.dedServ)
              {
                SoundStyle soundStyle;
                // ISSUE: explicit constructor call
                ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/Monster5", (SoundType) 0);
                ((SoundStyle) ref soundStyle).Volume = 1.5f;
                SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
                break;
              }
              break;
            case 6:
              if (!Main.dedServ)
              {
                SoundStyle soundStyle;
                // ISSUE: explicit constructor call
                ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/Thunder", (SoundType) 0);
                ((SoundStyle) ref soundStyle).Volume = 1.5f;
                ((SoundStyle) ref soundStyle).Pitch = 1.5f;
                SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
                break;
              }
              break;
            case 7:
              if (!Main.dedServ)
              {
                SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Zombie_104", (SoundType) 0);
                SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
                break;
              }
              break;
            case 8:
              if (!Main.dedServ)
              {
                SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Monster70", (SoundType) 0);
                SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
                break;
              }
              break;
            case 9:
              if (!Main.dedServ)
              {
                SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Railgun", (SoundType) 0);
                SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
                break;
              }
              break;
            case 10:
              if (!Main.dedServ)
              {
                SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Navi", (SoundType) 0);
                SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
                break;
              }
              break;
            case 11:
              if (!Main.dedServ)
              {
                SoundStyle soundStyle;
                // ISSUE: explicit constructor call
                ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/ZaWarudo", (SoundType) 0);
                ((SoundStyle) ref soundStyle).Volume = 1.5f;
                SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
                break;
              }
              break;
            default:
              SoundEngine.PlaySound(ref SoundID.NPCDeath10, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
              break;
          }
        }
      }
      if (!this.InPhase2 && npc.life < npc.lifeMax / 2)
      {
        this.InPhase2 = true;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      }
      if ((double) npc.ai[0] == 6.0)
      {
        if ((double) npc.ai[1] == 0.0)
        {
          NPC npc1 = npc;
          ((Entity) npc1).position = Vector2.op_Addition(((Entity) npc1).position, ((Entity) npc).velocity);
        }
        else if ((double) npc.ai[1] == 1.0)
          this.DoFuryRingAttack = true;
      }
      if (this.DoFuryRingAttack)
      {
        ((Entity) npc).velocity = Vector2.Zero;
        if (this.FuryRingTimer == 0)
        {
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 1.33333337f), 0.0f, Main.myPlayer, 4f, 0.0f, 0.0f);
          if (WorldSavingSystem.MasochistModeReal && NPC.CountNPCS(565) < 3)
            FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 565, target: npc.target, velocity: new Vector2());
        }
        ++this.FuryRingTimer;
        if (this.FuryRingTimer % 2 == 0)
        {
          if (FargoSoulsUtil.HostCheck)
          {
            float shotRotationCounter = (float) this.FuryRingShotRotationCounter;
            if (WorldSavingSystem.MasochistModeReal && this.FuryRingTimer >= 30 && this.FuryRingTimer <= 60)
              ++shotRotationCounter;
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, Math.PI / 15.0 * (double) shotRotationCounter, new Vector2())), ModContent.ProjectileType<BetsyFury>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 1.33333337f), 0.0f, Main.myPlayer, (float) npc.target, 0.0f, 0.0f);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, Math.PI / 15.0 * -(double) shotRotationCounter, new Vector2())), ModContent.ProjectileType<BetsyFury>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 1.33333337f), 0.0f, Main.myPlayer, (float) npc.target, 0.0f, 0.0f);
          }
          ++this.FuryRingShotRotationCounter;
        }
        if (this.FuryRingTimer > (this.InPhase2 ? 90 : 30) + 2)
        {
          this.DoFuryRingAttack = false;
          this.InFuryRingAttackCooldown = true;
          this.FuryRingTimer = 0;
          this.FuryRingShotRotationCounter = 0;
        }
        EModeGlobalNPC.Aura(npc, 1200f, 196, true, 226, new Color());
        EModeGlobalNPC.Aura(npc, 1200f, 195, true, 226, new Color());
      }
      if (this.InFuryRingAttackCooldown)
      {
        EModeGlobalNPC.Aura(npc, 1200f, 196, true, 226, new Color());
        EModeGlobalNPC.Aura(npc, 1200f, 195, true, 226, new Color());
        if (++this.FuryRingShotRotationCounter > 90)
        {
          this.InFuryRingAttackCooldown = false;
          this.FuryRingTimer = 0;
          this.FuryRingShotRotationCounter = 0;
        }
        NPC npc2 = npc;
        ((Entity) npc2).position = Vector2.op_Subtraction(((Entity) npc2).position, Vector2.op_Multiply(((Entity) npc).velocity, 0.5f));
        if (this.FuryRingTimer % 2 == 0)
          return false;
      }
      if (!DD2Event.Ongoing && npc.HasPlayerTarget && (!((Entity) Main.player[npc.target]).active || Main.player[npc.target].dead || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > 3000.0))
      {
        int closest = (int) Player.FindClosest(((Entity) npc).Center, 0, 0);
        if (closest < 0 || !((Entity) Main.player[closest]).active || Main.player[closest].dead || (double) ((Entity) npc).Distance(((Entity) Main.player[closest]).Center) > 3000.0)
          ((Entity) npc).active = false;
      }
      EModeUtils.DropSummon(npc, "BetsyEgg", WorldSavingSystem.DownedBetsy, ref this.DroppedSummon, NPC.downedGolemBoss);
      return true;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      target.AddBuff(195, 600, true, false);
      target.AddBuff(196, 600, true, false);
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 600, true, false);
    }

    public virtual bool SpecialOnKill(NPC npc)
    {
      npc.boss = false;
      return base.SpecialOnKill(npc);
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      WorldSavingSystem.DownedBetsy = true;
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      if (npc.HasNPCTarget)
      {
        modifiers.Null();
        SoundEngine.PlaySound(ref SoundID.NPCHit4, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      }
      base.ModifyIncomingHit(npc, ref modifiers);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, 551);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 34);
      EModeNPCBehaviour.LoadGoreRange(recolor, 1079, 1086);
      EModeNPCBehaviour.LoadExtra(recolor, 81);
      EModeNPCBehaviour.LoadExtra(recolor, 82);
      EModeNPCBehaviour.LoadGlowMask(recolor, 226);
      EModeNPCBehaviour.LoadProjectile(recolor, 686);
      EModeNPCBehaviour.LoadProjectile(recolor, 687);
    }

    static Betsy()
    {
      List<LocalizedText> localizedTextList = new List<LocalizedText>();
      CollectionsMarshal.SetCount<LocalizedText>(localizedTextList, 27);
      Span<LocalizedText> span = CollectionsMarshal.AsSpan<LocalizedText>(localizedTextList);
      int num1 = 0;
      span[num1] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy1");
      int num2 = num1 + 1;
      span[num2] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy2");
      int num3 = num2 + 1;
      span[num3] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy3");
      int num4 = num3 + 1;
      span[num4] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy4");
      int num5 = num4 + 1;
      span[num5] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy5");
      int num6 = num5 + 1;
      span[num6] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy6");
      int num7 = num6 + 1;
      span[num7] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy7");
      int num8 = num7 + 1;
      span[num8] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy8");
      int num9 = num8 + 1;
      span[num9] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy9");
      int num10 = num9 + 1;
      span[num10] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy10");
      int num11 = num10 + 1;
      span[num11] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy11");
      int num12 = num11 + 1;
      span[num12] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy12");
      int num13 = num12 + 1;
      span[num13] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy13");
      int num14 = num13 + 1;
      span[num14] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy14");
      int num15 = num14 + 1;
      span[num15] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy15");
      int num16 = num15 + 1;
      span[num16] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy16");
      int num17 = num16 + 1;
      span[num17] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy17");
      int num18 = num17 + 1;
      span[num18] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy18");
      int num19 = num18 + 1;
      span[num19] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy19");
      int num20 = num19 + 1;
      span[num20] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy20");
      int num21 = num20 + 1;
      span[num21] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy21");
      int num22 = num21 + 1;
      span[num22] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy22");
      int num23 = num22 + 1;
      span[num23] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy23");
      int num24 = num23 + 1;
      span[num24] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy24");
      int num25 = num24 + 1;
      span[num25] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy25");
      int num26 = num25 + 1;
      span[num26] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy26");
      int num27 = num26 + 1;
      span[num27] = Language.GetText("Mods.FargowiltasSouls.NPCs.EMode.Betsy27");
      int num28 = num27 + 1;
      Betsy.MasoTexts = localizedTextList;
    }
  }
}
