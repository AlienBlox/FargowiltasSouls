// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.BrainIllusionAttack
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs
{
  public class BrainIllusionAttack : ModNPC
  {
    private const int attackDelay = 120;

    public virtual string Texture => "Terraria/Images/NPC_266";

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = Main.npcFrameCount[266];
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 6;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 5);
      Span<int> span = CollectionsMarshal.AsSpan<int>(debuffs);
      int num1 = 0;
      span[num1] = 24;
      int num2 = num1 + 1;
      span[num2] = 31;
      int num3 = num2 + 1;
      span[num3] = 68;
      int num4 = num3 + 1;
      span[num4] = 39;
      int num5 = num4 + 1;
      span[num5] = 67;
      int num6 = num5 + 1;
      npc.AddDebuffImmunities(debuffs);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 120;
      ((Entity) this.NPC).height = 80;
      this.NPC.scale += 0.25f;
      this.NPC.damage = 30;
      this.NPC.defense = 14;
      this.NPC.lifeMax = 1;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit9);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath11);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot) => this.NPC.alpha == 0;

    public virtual bool CanHitNPC(NPC target) => this.NPC.alpha == 0;

    public virtual void AI()
    {
      if ((double) this.NPC.localAI[1] == 0.0)
      {
        this.NPC.localAI[1] = 1f;
        this.NPC.localAI[2] = ((Entity) this.NPC).Center.X;
        this.NPC.localAI[3] = ((Entity) this.NPC).Center.Y;
      }
      NPC npc1 = FargoSoulsUtil.NPCExists(this.NPC.ai[0], 266);
      if (npc1 == null)
      {
        this.NPC.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
        this.NPC.HitEffect(0, 10.0, new bool?());
        ((Entity) this.NPC).active = false;
      }
      else
      {
        this.NPC.target = npc1.target;
        this.NPC.damage = this.NPC.defDamage = (int) ((double) npc1.defDamage * 4.0 / 3.0);
        this.NPC.defense = this.NPC.defDefense = npc1.defDefense;
        this.NPC.alpha = 0;
        this.NPC.dontTakeDamage = true;
        if ((double) ++this.NPC.localAI[0] < 120.0)
        {
          this.NPC.ai[1] = MathHelper.Lerp(this.NPC.ai[1], 0.0f, 0.03f);
          this.NPC.alpha = Math.Min((int) byte.MaxValue, (int) this.NPC.ai[1] + 1);
          NPC npc2 = this.NPC;
          ((Entity) npc2).position = Vector2.op_Addition(((Entity) npc2).position, Vector2.op_Multiply(Vector2.op_Multiply(0.5f, Vector2.op_Subtraction(((Entity) Main.player[this.NPC.target]).position, ((Entity) Main.player[this.NPC.target]).oldPosition)), (float) (1.0 - (double) this.NPC.localAI[0] / 120.0)));
          float num = (float) (16.0 * (double) this.NPC.localAI[0] / 120.0);
          ((Entity) this.NPC).Center = Vector2.op_Addition(new Vector2(this.NPC.localAI[2], this.NPC.localAI[3]), Utils.NextVector2Circular(Main.rand, num, num));
        }
        else if ((double) this.NPC.localAI[0] == 120.0)
        {
          ((Entity) this.NPC).Center = new Vector2(this.NPC.localAI[2], this.NPC.localAI[3]);
          ((Entity) this.NPC).velocity = Vector2.op_Multiply(18f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) Main.player[this.NPC.target]).Center));
          this.NPC.ai[2] = ((Entity) Main.player[this.NPC.target]).Center.X;
          this.NPC.ai[3] = ((Entity) Main.player[this.NPC.target]).Center.Y;
          this.NPC.netUpdate = true;
        }
        else if ((double) this.NPC.localAI[0] > 300.0)
        {
          this.NPC.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
          this.NPC.HitEffect(0, 10.0, new bool?());
          ((Entity) this.NPC).active = false;
        }
        else
        {
          NPC npc3 = this.NPC;
          ((Entity) npc3).velocity = Vector2.op_Multiply(((Entity) npc3).velocity, 1.015f);
          this.NPC.dontTakeDamage = false;
        }
      }
    }

    public virtual void FindFrame(int frameHeight)
    {
      if (!FargoSoulsUtil.NPCExists(this.NPC.ai[0], 266).Alive())
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

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      for (int index1 = 0; index1 < 30; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 5, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
        Main.dust[index2].scale += 2f;
      }
    }

    public virtual bool CheckActive() => false;

    public virtual bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
      return new bool?(false);
    }

    public virtual bool CheckDead() => false;

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      if (!TextureAssets.Npc[266].IsLoaded)
        return false;
      Texture2D texture2D = TextureAssets.Npc[266].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color alpha = this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      if (this.NPC.alpha == 0)
      {
        for (int index = 0; index < NPCID.Sets.TrailCacheLength[this.NPC.type]; ++index)
        {
          Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (NPCID.Sets.TrailCacheLength[this.NPC.type] - index) / (float) NPCID.Sets.TrailCacheLength[this.NPC.type]);
          Vector2 oldPo = this.NPC.oldPos[index];
          float rotation = this.NPC.rotation;
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), color, rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), alpha, this.NPC.rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
