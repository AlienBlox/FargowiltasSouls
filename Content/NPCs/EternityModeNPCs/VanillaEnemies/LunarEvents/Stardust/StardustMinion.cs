// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Stardust.StardustMinion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Stardust
{
  public class StardustMinion : ModNPC
  {
    public List<int> NoDamage;
    private Vector2 initialLock;
    private Vector2 LockPos;
    private int HealCounter;

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 2;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      NPCID.Sets.SpecificDebuffImmunity[this.Type] = NPCID.Sets.SpecificDebuffImmunity[493];
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
    }

    public virtual void SetDefaults()
    {
      this.NPC.CloneDefaults(405);
      this.NPC.lifeMax = 8000;
      this.NPC.damage = 110;
      this.NPC.aiStyle = -1;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.timeLeft = 108000;
      this.NPC.noTileCollide = true;
      this.NPC.scale = 1f;
    }

    public virtual void SendExtraAI(BinaryWriter writer) => writer.Write(this.NPC.localAI[0]);

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.NPC.localAI[0] = reader.ReadSingle();
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot)
    {
      return !this.NoDamage.Contains((int) this.NPC.ai[1]);
    }

    public virtual void AI() => this.StardustMinionAI();

    public void StardustMinionAI()
    {
      ref float local1 = ref this.NPC.ai[0];
      ref float local2 = ref this.NPC.ai[1];
      ref float local3 = ref this.NPC.ai[2];
      ref float local4 = ref this.NPC.ai[3];
      ref float local5 = ref this.NPC.localAI[0];
      NPC parent = Main.npc[(int) local3];
      if (!((Entity) parent).active || parent.type != 493)
      {
        ((Entity) this.NPC).active = false;
      }
      else
      {
        LunarTowerStardust globalNpc = parent.GetGlobalNPC<LunarTowerStardust>();
        float num1 = (float) ((Entity) parent).height * 0.8f;
        if (++this.HealCounter > 60)
        {
          this.HealCounter = 0;
          if (!parent.HasValidTarget || (double) ((Entity) parent).Distance(((Entity) Main.player[parent.target]).Center) > (double) globalNpc.AuraSize)
          {
            this.NPC.life += 500;
            if (this.NPC.life > this.NPC.lifeMax)
              this.NPC.life = this.NPC.lifeMax;
            if (this.NPC.life >= this.NPC.lifeMax)
            {
              this.NPC.dontTakeDamage = false;
              ((Entity) this.NPC).position = ((Entity) this.NPC).Center;
              ((Entity) this.NPC).width = 48;
              ((Entity) this.NPC).height = 48;
              ((Entity) this.NPC).Center = ((Entity) this.NPC).position;
            }
            this.NPC.netUpdate = true;
            CombatText.NewText(((Entity) this.NPC).Hitbox, CombatText.HealLife, 500, false, false);
          }
        }
        float num2 = local2;
        if ((double) num2 != 1.0)
        {
          if ((double) num2 != 2.0)
          {
            if ((double) num2 != 3.0)
            {
              if ((double) num2 != 4.0)
              {
                if ((double) num2 != 5.0)
                {
                  if ((double) num2 != 6.0)
                  {
                    if ((double) num2 != 7.0)
                    {
                      if ((double) num2 != 8.0)
                      {
                        if ((double) num2 != 9.0)
                        {
                          if ((double) num2 != 10.0)
                            return;
                          Player player = Main.player[parent.target];
                          if ((double) local5 != 10.0 && (double) local5 != -1.0 && ((Entity) player).active && !player.ghost)
                          {
                            int num3 = Utils.NextBool(Main.rand) ? 1 : -1;
                            ((Entity) this.NPC).velocity = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), (double) (90 * num3), new Vector2());
                            this.LockPos = Vector2.op_Multiply(Vector2.op_Subtraction(((Entity) player).Center, ((Entity) parent).Center), Utils.NextFloat(Main.rand, 0.8f, 1.2f));
                            local5 = 10f;
                            this.NPC.netUpdate = true;
                            local1 = 0.0f;
                          }
                          int num4 = 16;
                          Vector2 target = Vector2.op_Addition(((Entity) parent).Center, this.LockPos);
                          ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), (float) num4);
                          this.RotateTowards(target, 2f);
                          ++local1;
                          if ((double) ((Entity) this.NPC).Distance(target) <= 300.0 && (double) local5 != -1.0 || (double) local1 > 180.0)
                          {
                            this.LockPos = Vector2.Zero;
                            local5 = -1f;
                          }
                          if ((double) ((Entity) this.NPC).Distance(((Entity) parent).Center) > (double) num1 || (double) local5 != -1.0)
                            return;
                          local5 = 0.0f;
                          local2 = 1f;
                        }
                        else
                        {
                          float num5 = ((Vector2) ref ((Entity) this.NPC).velocity).Length();
                          float num6 = (float) (0.52359879016876221 * (double) globalNpc.CellRotation / 60.0) + (float) (6.2831854820251465 * ((double) local4 / 20.0));
                          Vector2 target = Vector2.op_Addition(((Entity) parent).Center, Vector2.op_Multiply(num1, Utils.ToRotationVector2(num6)));
                          ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), num5);
                          this.RotateTowards(target, 3f);
                          if ((double) ((Entity) this.NPC).Distance(target) > 100.0)
                            return;
                          local2 = 1f;
                        }
                      }
                      else
                      {
                        local5 = 0.0f;
                        this.Scissor(parent, local4, globalNpc, false);
                      }
                    }
                    else
                    {
                      Player player = Main.player[parent.target];
                      if ((double) local5 != 7.0)
                      {
                        this.initialLock = ((Entity) player).Center;
                        local5 = 7f;
                      }
                      if (WorldSavingSystem.MasochistModeReal)
                        this.initialLock = ((Entity) player).Center;
                      this.Scissor(parent, local4, globalNpc, true);
                    }
                  }
                  else
                  {
                    Player player = Main.player[parent.target];
                    if ((double) local5 != 6.0 && ((Entity) player).active && !player.ghost)
                    {
                      ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 20f);
                      local5 = 6f;
                    }
                    if ((double) ((Entity) this.NPC).Distance(((Entity) parent).Center) <= (double) globalNpc.AuraSize)
                      return;
                    local5 = 0.0f;
                    local2 = 4f;
                  }
                }
                else
                {
                  Player player = Main.player[parent.target];
                  float num7 = (float) (2.0943951606750488 * (double) globalNpc.CellRotation / 60.0) + (float) (6.2831854820251465 * ((double) local4 / 20.0));
                  ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) parent).Center, Vector2.op_Multiply(num1, Utils.ToRotationVector2(num7))), ((Entity) this.NPC).Center), 0.05f);
                  float rotation1 = Utils.ToRotation(((Entity) this.NPC).DirectionFrom(((Entity) parent).Center));
                  float rotation2 = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
                  if (globalNpc.AttackTimer <= 60 || globalNpc.AttackTimer <= 60 || !((Entity) player).active || player.ghost || (double) Math.Abs(rotation2 - rotation1) >= 0.39269909262657166)
                    return;
                  local5 = 0.0f;
                  SoundEngine.PlaySound(ref SoundID.Item96, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
                  local2 = 6f;
                }
              }
              else
              {
                float num8 = (float) (0.52359879016876221 * (double) globalNpc.CellRotation / 60.0) + (float) (6.2831854820251465 * ((double) local4 / 20.0));
                Vector2 target = Vector2.op_Addition(((Entity) parent).Center, Vector2.op_Multiply(num1, Utils.ToRotationVector2(num8)));
                ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), 16f);
                this.RotateTowards(target, 2f);
                if ((double) ((Entity) this.NPC).Distance(target) > 100.0)
                  return;
                local2 = 1f;
              }
            }
            else
            {
              float num9 = (float) (0.52359879016876221 * (double) globalNpc.CellRotation / 60.0) + (float) (6.2831854820251465 * ((double) local4 / 20.0));
              ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) parent).Center, Vector2.op_Multiply((float) (globalNpc.AuraSize + 400), Utils.ToRotationVector2(num9))), ((Entity) this.NPC).Center)), 16f);
              if ((double) ((Entity) this.NPC).Distance(((Entity) parent).Center) <= (double) globalNpc.AuraSize)
                return;
              local2 = 4f;
            }
          }
          else
          {
            float num10 = (float) (0.52359879016876221 * (double) globalNpc.CellRotation / 60.0) + (float) (6.2831854820251465 * ((double) local4 / 20.0));
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) parent).Center, Vector2.op_Multiply(num1, Utils.ToRotationVector2(num10))), ((Entity) this.NPC).Center), 0.05f);
          }
        }
        else
        {
          float num11 = (float) (3.1415927410125732 * (double) globalNpc.CellRotation / 60.0) + (float) (6.2831854820251465 * ((double) local4 / 20.0));
          ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) parent).Center, Vector2.op_Multiply(num1, Utils.ToRotationVector2(num11))), ((Entity) this.NPC).Center), 0.05f);
        }
      }
    }

    public void Scissor(NPC parent, float num, LunarTowerStardust parentModNPC, bool telegraph)
    {
      int num1 = (double) num >= 10.0 ? 1 : -1;
      int num2 = num1 == 1 ? (int) num - 10 : (int) num;
      float num3 = (float) (1.0 + 0.079999998211860657 * (double) (5 - num2));
      int num4 = num2 * parentModNPC.AuraSize / 10 + ((Entity) parent).width / 2;
      if (num1 == 1)
        num4 += parentModNPC.AuraSize / 20;
      float num5;
      if (telegraph)
      {
        num5 = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) parent, this.initialLock)) + parentModNPC.CellRotation * (float) num1;
      }
      else
      {
        num5 = Utils.ToRotation(this.LockPos);
        this.LockPos = Utils.RotatedBy(this.LockPos, (double) MathHelper.ToRadians(-num3 * (float) num1), new Vector2());
      }
      ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) parent).Center, Vector2.op_Multiply((float) num4, Utils.ToRotationVector2(num5))), ((Entity) this.NPC).Center), 0.05f);
      if (!telegraph)
        return;
      this.LockPos = Utils.ToRotationVector2(Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) parent, this.initialLock)) + parentModNPC.CellRotation * (float) num1);
    }

    public virtual bool CheckDead()
    {
      double num = (double) this.NPC.ai[1];
      ref float local = ref this.NPC.ai[2];
      if (!((Entity) Main.npc[(int) local]).active)
        return true;
      this.NPC.life = this.NPC.lifeMax;
      this.NPC.dontTakeDamage = true;
      ((Entity) this.NPC).position = ((Entity) this.NPC).Center;
      ((Entity) this.NPC).width = 26;
      ((Entity) this.NPC).height = 26;
      ((Entity) this.NPC).Center = ((Entity) this.NPC).position;
      return false;
    }

    public virtual void DrawBehind(int index) => Main.instance.DrawCacheNPCProjectiles.Add(index);

    public virtual void FindFrame(int frameHeight)
    {
      this.NPC.frame.Y = this.NPC.dontTakeDamage ? frameHeight : 0;
    }

    public virtual bool CheckActive() => false;

    public virtual bool PreKill() => false;

    public virtual bool? DrawHealthBar(byte hbPos, ref float scale, ref Vector2 Pos)
    {
      return new bool?(false);
    }

    private void RotateTowards(Vector2 target, float speed)
    {
      Vector2 vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, target);
      Vector2 velocity = ((Entity) this.NPC).velocity;
      float num = (float) Math.Atan2((double) vector2.Y * (double) velocity.X - (double) vector2.X * (double) velocity.Y, (double) velocity.X * (double) vector2.X + (double) velocity.Y * (double) vector2.Y);
      ((Entity) this.NPC).velocity = Utils.RotatedBy(((Entity) this.NPC).velocity, (double) Math.Sign(num) * (double) Math.Min(Math.Abs(num), (float) ((double) speed * 3.1415927410125732 / 180.0)), new Vector2());
    }

    public virtual void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
    {
      ref float local = ref this.NPC.ai[2];
      NPC npc = Main.npc[(int) local];
      if (!((Entity) npc).active || npc.type != 493)
        return;
      LunarTowerStardust globalNpc = npc.GetGlobalNPC<LunarTowerStardust>();
      if (globalNpc.AnyPlayerWithin(npc, globalNpc.AuraSize))
        return;
      modifiers.Null();
    }

    public StardustMinion()
    {
      List<int> intList = new List<int>();
      CollectionsMarshal.SetCount<int>(intList, 1);
      Span<int> span = CollectionsMarshal.AsSpan<int>(intList);
      int num1 = 0;
      span[num1] = 7;
      int num2 = num1 + 1;
      this.NoDamage = intList;
      this.initialLock = Vector2.Zero;
      this.LockPos = Vector2.Zero;
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }

    public enum States
    {
      Idle = 1,
      PrepareExpand = 2,
      Expand = 3,
      Contract = 4,
      PrepareRush = 5,
      Rush = 6,
      PrepareScissor = 7,
      Scissor = 8,
      ScissorContract = 9,
      Curve = 10, // 0x0000000A
    }
  }
}
