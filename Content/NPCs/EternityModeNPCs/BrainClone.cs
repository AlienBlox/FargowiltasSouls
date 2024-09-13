// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.BrainClone
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs
{
  public class BrainClone : ModNPC
  {
    private int trueAlpha;

    public virtual string Texture => "Terraria/Images/NPC_266";

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = Main.npcFrameCount[266];
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 160;
      ((Entity) this.NPC).height = 110;
      this.NPC.scale += 0.25f;
      this.NPC.damage = 30;
      this.NPC.defense = 14;
      this.NPC.lifeMax = 1000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit9);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath11);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot) => this.trueAlpha == 0;

    public virtual void AI()
    {
      if ((double) EModeGlobalNPC.brainBoss < 0.0 || EModeGlobalNPC.brainBoss >= Main.maxNPCs)
      {
        this.NPC.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
        ((Entity) this.NPC).active = false;
      }
      else
      {
        NPC npc1 = Main.npc[EModeGlobalNPC.brainBoss];
        if (!((Entity) npc1).active || npc1.type != 266)
        {
          this.NPC.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
          ((Entity) this.NPC).active = false;
        }
        else
        {
          if (this.NPC.buffType[0] != 0)
          {
            this.NPC.buffImmune[this.NPC.buffType[0]] = true;
            this.NPC.DelBuff(0);
          }
          this.NPC.target = npc1.target;
          this.NPC.damage = npc1.damage;
          this.NPC.defDamage = npc1.defDamage;
          this.NPC.defense = npc1.defense;
          this.NPC.defDefense = npc1.defDefense;
          this.NPC.life = npc1.life;
          this.NPC.lifeMax = npc1.lifeMax;
          this.NPC.knockBackResist = npc1.knockBackResist;
          if (WorldSavingSystem.MasochistModeReal || (double) ((Entity) Main.player[this.NPC.target]).Distance(FargoSoulsUtil.ClosestPointInHitbox((Entity) this.NPC, ((Entity) Main.player[this.NPC.target]).Center)) > 360.0)
            this.NPC.knockBackResist = 0.0f;
          if (this.trueAlpha > 0 && ((double) this.NPC.ai[0] == 2.0 || (double) this.NPC.ai[0] == -3.0) && this.NPC.HasValidTarget)
          {
            Vector2 center = ((Entity) Main.player[this.NPC.target]).Center;
            if ((double) ((Entity) this.NPC).Distance(center) < 360.0)
              ((Entity) this.NPC).Center = Vector2.op_Addition(center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(center), 360f));
          }
          Vector2 center1 = ((Entity) this.NPC).Center;
          float num1 = ((Entity) Main.player[this.NPC.target]).Center.X - center1.X;
          float num2 = ((Entity) Main.player[this.NPC.target]).Center.Y - center1.Y;
          float num3 = (float) (((double) ((Entity) this.NPC).Distance(((Entity) Main.player[this.NPC.target]).Center) > 500.0 ? 8.0 : 4.0) / Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2));
          float num4 = num1 * num3;
          float num5 = num2 * num3;
          ((Entity) this.NPC).velocity.X = (float) (((double) ((Entity) this.NPC).velocity.X * 50.0 + (double) num4) / 51.0);
          ((Entity) this.NPC).velocity.Y = (float) (((double) ((Entity) this.NPC).velocity.Y * 50.0 + (double) num5) / 51.0);
          if (WorldSavingSystem.MasochistModeReal)
          {
            if ((double) this.NPC.ai[0] == -2.0)
            {
              NPC npc2 = this.NPC;
              ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, 0.9f);
              if (Main.netMode != 0)
                this.NPC.ai[3] += 15f;
              else
                this.NPC.ai[3] += 25f;
              if ((double) this.NPC.ai[3] >= (double) byte.MaxValue)
              {
                this.NPC.ai[3] = (float) byte.MaxValue;
                ((Entity) this.NPC).position.X = this.NPC.ai[1] * 16f - (float) (((Entity) this.NPC).width / 2);
                ((Entity) this.NPC).position.Y = this.NPC.ai[2] * 16f - (float) (((Entity) this.NPC).height / 2);
                SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
                this.NPC.ai[0] = -3f;
                this.NPC.netUpdate = true;
                this.NPC.netSpam = 0;
              }
              this.trueAlpha = (int) this.NPC.ai[3];
            }
            else if ((double) this.NPC.ai[0] == -3.0)
            {
              if (Main.netMode != 0)
                this.NPC.ai[3] -= 15f;
              else
                this.NPC.ai[3] -= 25f;
              if ((double) this.NPC.ai[3] <= 0.0)
              {
                this.NPC.ai[3] = 0.0f;
                this.NPC.ai[0] = -1f;
                this.NPC.netUpdate = true;
                this.NPC.netSpam = 0;
              }
              this.trueAlpha = (int) this.NPC.ai[3];
            }
            else if (FargoSoulsUtil.HostCheck)
            {
              ++this.NPC.localAI[1];
              if (this.NPC.justHit)
                this.NPC.localAI[1] -= (float) Main.rand.Next(5);
              int num6 = 60 + Main.rand.Next(120);
              if (Main.netMode != 0)
                num6 += Main.rand.Next(30, 90);
              if ((double) this.NPC.localAI[1] >= (double) num6)
              {
                this.NPC.localAI[1] = 0.0f;
                this.NPC.TargetClosest(true);
                int num7 = 0;
                do
                {
                  ++num7;
                  int num8 = (int) ((Entity) Main.player[this.NPC.target]).Center.X / 16;
                  int num9 = (int) ((Entity) Main.player[this.NPC.target]).Center.Y / 16;
                  int num10 = !Utils.NextBool(Main.rand, 2) ? num8 - Main.rand.Next(7, 13) : num8 + Main.rand.Next(7, 13);
                  int num11 = !Utils.NextBool(Main.rand, 2) ? num9 - Main.rand.Next(7, 13) : num9 + Main.rand.Next(7, 13);
                  if (!WorldGen.SolidTile(num10, num11, false))
                  {
                    this.NPC.ai[3] = 0.0f;
                    this.NPC.ai[0] = -2f;
                    this.NPC.ai[1] = (float) num10;
                    this.NPC.ai[2] = (float) num11;
                    this.NPC.netUpdate = true;
                    this.NPC.netSpam = 0;
                    break;
                  }
                }
                while (num7 <= 100);
              }
            }
          }
          this.NPC.alpha = this.trueAlpha;
          if (WorldSavingSystem.MasochistModeReal)
            return;
          this.NPC.Opacity *= (float) (0.5 + (1.0 - (double) this.NPC.life / (double) this.NPC.lifeMax) / 2.0);
        }
      }
    }

    public virtual void FindFrame(int frameHeight)
    {
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.brainBoss, 266))
        return;
      this.NPC.frame.Y = Main.npc[EModeGlobalNPC.brainBoss].frame.Y;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      target.AddBuff(20, 120, true, false);
      target.AddBuff(22, 120, true, false);
      target.AddBuff(30, 120, true, false);
      target.AddBuff(32, 120, true, false);
      target.AddBuff(33, 120, true, false);
      target.AddBuff(36, 120, true, false);
    }

    public virtual void ModifyIncomingHit(ref NPC.HitModifiers modifiers) => modifiers.Null();

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      for (int index1 = 0; index1 < 40; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 5, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2.5f);
        Main.dust[index2].scale += 0.5f;
      }
    }

    public virtual bool CheckActive() => false;

    public virtual bool CheckDead()
    {
      this.NPC.FargoSouls().Needled = false;
      NPC npc = FargoSoulsUtil.NPCExists(EModeGlobalNPC.brainBoss, new int[1]
      {
        266
      });
      if (npc.Alive())
      {
        ((Entity) this.NPC).active = true;
        this.NPC.life = npc.life;
      }
      return false;
    }

    public virtual bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
      return new bool?(false);
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      if (!TextureAssets.Npc[266].IsLoaded)
        return false;
      Texture2D texture2D = TextureAssets.Npc[266].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color alpha = this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), alpha, this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      if (this.NPC.HasPlayerTarget && WorldSavingSystem.MasochistModeReal)
      {
        Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.NPC).Center, ((Entity) Main.player[this.NPC.target]).Center);
        Vector2 center = ((Entity) Main.player[this.NPC.target]).Center;
        float num = (float) (1.0 - (double) this.NPC.life / (double) this.NPC.lifeMax);
        if ((double) num >= 0.0 && (double) num <= 1.0)
        {
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(new Vector2(center.X + vector2_2.X, center.Y - vector2_2.Y), Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.op_Multiply(alpha, num), this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(new Vector2(center.X - vector2_2.X, center.Y + vector2_2.Y), Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.op_Multiply(alpha, num), this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(new Vector2(center.X - vector2_2.X, center.Y - vector2_2.Y), Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.op_Multiply(alpha, num), this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
        }
      }
      return false;
    }
  }
}
