// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Spirit.SpiritChampionHand
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
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
namespace FargowiltasSouls.Content.Bosses.Champions.Spirit
{
  public class SpiritChampionHand : ModNPC
  {
    public virtual void SetStaticDefaults()
    {
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 6;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 7);
      Span<int> span = CollectionsMarshal.AsSpan<int>(debuffs);
      int num1 = 0;
      span[num1] = 31;
      int num2 = num1 + 1;
      span[num2] = 46;
      int num3 = num2 + 1;
      span[num3] = 24;
      int num4 = num3 + 1;
      span[num4] = 68;
      int num5 = num4 + 1;
      span[num5] = ModContent.BuffType<LethargicBuff>();
      int num6 = num5 + 1;
      span[num6] = ModContent.BuffType<ClippedWingsBuff>();
      int num7 = num6 + 1;
      span[num7] = ModContent.BuffType<LightningRodBuff>();
      int num8 = num7 + 1;
      npc.AddDebuffImmunities(debuffs);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 90;
      ((Entity) this.NPC).height = 90;
      this.NPC.damage = 125;
      this.NPC.defense = 140;
      this.NPC.lifeMax = 550000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit54);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath52);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      this.NPC.dontTakeDamage = true;
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * (double) balance);
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot)
    {
      CooldownSlot = 1;
      return (double) this.NPC.localAI[3] == 0.0;
    }

    public virtual void AI()
    {
      NPC head = FargoSoulsUtil.NPCExists(this.NPC.ai[1], ModContent.NPCType<SpiritChampion>());
      if (head == null)
      {
        this.NPC.life = 0;
        this.NPC.checkDead();
        ((Entity) this.NPC).active = false;
      }
      else
      {
        this.NPC.target = head.target;
        this.NPC.realLife = ((Entity) head).whoAmI;
        NPC npc = this.NPC;
        ((Entity) npc).position = Vector2.op_Addition(((Entity) npc).position, Vector2.op_Multiply(((Entity) head).velocity, 0.75f));
        Player player = Main.player[this.NPC.target];
        this.NPC.localAI[3] = 0.0f;
        switch ((int) this.NPC.ai[0])
        {
          case 0:
            Vector2 center = ((Entity) head).Center;
            float num1 = (double) head.ai[0] % 2.0 == 0.0 ? 50f : 150f;
            float num2 = (double) head.ai[0] % 2.0 == 0.0 ? 50f : 100f;
            if ((double) head.ai[0] % 2.0 == 0.0)
              this.NPC.localAI[3] = 1f;
            center.X += num1 * this.NPC.ai[2];
            center.Y += num1 * this.NPC.ai[3];
            if ((double) ((Entity) this.NPC).Distance(center) > (double) num2)
            {
              this.Movement(center, 0.8f, 24f);
              break;
            }
            break;
          case 1:
          case 3:
            if ((double) head.ai[0] != 3.0 && (double) head.ai[0] != -3.0)
            {
              this.NPC.ai[0] = 0.0f;
              this.NPC.netUpdate = true;
            }
            bool flag = Math.Sign(((Entity) player).Center.X - ((Entity) head).Center.X) * Math.Sign(((Entity) this.NPC).Center.X - ((Entity) head).Center.X) == 1 && Math.Sign(((Entity) player).Center.Y - ((Entity) head).Center.Y) * Math.Sign(((Entity) this.NPC).Center.Y - ((Entity) head).Center.Y) == 1;
            if ((double) head.ai[0] == -3.0)
              flag = false;
            if ((double) this.NPC.ai[0] == 3.0)
              flag = true;
            Vector2 targetPos = !flag ? Vector2.op_Addition(((Entity) head).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) head, ((Entity) this.NPC).Center), ((Entity) head).Distance(((Entity) player).Center))) : ((Entity) player).Center;
            if ((double) ((Entity) this.NPC).Distance(targetPos) > 50.0)
              this.Movement(targetPos, 0.15f, 7f);
            Rectangle hitbox1 = ((Entity) this.NPC).Hitbox;
            if (((Rectangle) ref hitbox1).Intersects(((Entity) player).Hitbox) && !player.HasBuff(ModContent.BuffType<GrabbedBuff>()) && player.FargoSouls().MashCounter <= 0)
            {
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              this.NPC.ai[0] = 2f;
              this.NPC.netUpdate = true;
              if ((double) head.ai[0] != -3.0)
              {
                head.ai[0] = -1f;
                head.ai[1] = 0.0f;
                head.netUpdate = true;
                break;
              }
              break;
            }
            break;
          case 2:
            if ((double) head.ai[0] != -1.0 && (double) head.ai[0] != -3.0 || !((Entity) player).active || player.dead || player.FargoSouls().MashCounter > 30)
            {
              Rectangle hitbox2 = ((Entity) this.NPC).Hitbox;
              if (((Rectangle) ref hitbox2).Intersects(((Entity) player).Hitbox))
              {
                player.FargoSouls().MashCounter += 30;
                ((Entity) player).velocity.X = (double) ((Entity) player).Center.X < (double) ((Entity) head).Center.X ? -15f : 15f;
                ((Entity) player).velocity.Y = -10f;
                SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              }
              this.NPC.ai[0] = (float) ((double) head.ai[0] == -3.0 ? 1 : 0);
              this.NPC.netUpdate = true;
              break;
            }
            Rectangle hitbox3 = ((Entity) this.NPC).Hitbox;
            if (((Rectangle) ref hitbox3).Intersects(((Entity) player).Hitbox))
            {
              Heal();
              ((Entity) player).Center = ((Entity) this.NPC).Center;
              ((Entity) player).velocity.X = 0.0f;
              ((Entity) player).velocity.Y = -0.4f;
              this.Movement(((Entity) head).Center, 0.8f, 24f);
              player.AddBuff(ModContent.BuffType<GrabbedBuff>(), 2, true, false);
              if (!player.immune || player.immuneTime < 2)
              {
                player.immune = true;
                player.immuneTime = 2;
                break;
              }
              break;
            }
            this.Movement(((Entity) player).Center, 2.4f, 48f);
            break;
          case 4:
            Rectangle hitbox4 = ((Entity) this.NPC).Hitbox;
            if (((Rectangle) ref hitbox4).Intersects(((Entity) player).Hitbox))
            {
              Heal();
              ((Entity) player).Center = ((Entity) this.NPC).Center;
              ((Entity) player).velocity.X = 0.0f;
              ((Entity) player).velocity.Y = -0.4f;
              this.Movement(((Entity) head).Center, 0.8f, 24f);
            }
            else
              this.Movement(((Entity) player).Center, 2.4f, 48f);
            if ((double) ++this.NPC.localAI[0] > 120.0)
            {
              hitbox4 = ((Entity) head).Hitbox;
              if (((Rectangle) ref hitbox4).Intersects(((Entity) player).Hitbox))
              {
                ((Entity) player).velocity.X = (double) ((Entity) player).Center.X < (double) ((Entity) head).Center.X ? -15f : 15f;
                ((Entity) player).velocity.Y = -10f;
                SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              }
              this.NPC.life = 0;
              this.NPC.SimpleStrikeNPC(this.NPC.lifeMax, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
              ((Entity) this.NPC).active = false;
              break;
            }
            break;
          default:
            this.NPC.ai[0] = 0.0f;
            goto case 0;
        }
        ((Entity) this.NPC).direction = this.NPC.spriteDirection = -(int) this.NPC.ai[2];
        this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).DirectionFrom(((Entity) head).Center));
        if (this.NPC.spriteDirection < 0)
          this.NPC.rotation += 3.14159274f;
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) head).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) head, ((Entity) this.NPC).Center), 50f));
        Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.NPC).Center, vector2_1);
        for (int index1 = 0; (double) index1 < (double) ((Vector2) ref vector2_2).Length(); index1 += 16)
        {
          if (!Utils.NextBool(Main.rand))
          {
            int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.Normalize(vector2_2), (float) index1)), 0, 0, 54, ((Entity) head).velocity.X * 0.4f, ((Entity) head).velocity.Y * 0.4f, 0, new Color(), 1.5f);
            Main.dust[index2].noGravity = true;
            Dust dust = Main.dust[index2];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
          }
        }
        Main.dust[Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 54, 0.0f, 0.0f, 0, new Color(), 2f)].noGravity = true;
        this.NPC.dontTakeDamage = (double) head.ai[0] < 0.0;
        if (this.NPC.dontTakeDamage)
          return;
        this.NPC.dontTakeDamage = head.dontTakeDamage;
      }

      void Heal()
      {
        if ((double) ++this.NPC.localAI[1] <= 10.0)
          return;
        this.NPC.localAI[1] = 0.0f;
        if (!FargoSoulsUtil.HostCheck)
          return;
        Vector2 vector2 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 1f, 2f), Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI));
        int num1 = (int) ((double) head.lifeMax / 100.0 * (double) Utils.NextFloat(Main.rand, 0.95f, 1.05f));
        float num2 = (float) (30 + Main.rand.Next(30));
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<SpiritHeal>(), num1, 0.0f, Main.myPlayer, (float) ((Entity) head).whoAmI, num2, 0.0f);
      }
    }

    public virtual bool CheckDead()
    {
      if ((double) this.NPC.ai[1] <= -1.0 || (double) this.NPC.ai[1] >= (double) Main.maxNPCs || !((Entity) Main.npc[(int) this.NPC.ai[1]]).active || Main.npc[(int) this.NPC.ai[1]].type != ModContent.NPCType<SpiritChampion>() || (double) Main.npc[(int) this.NPC.ai[1]].ai[0] == -3.0)
        return true;
      ((Entity) this.NPC).active = true;
      this.NPC.life = 1;
      return false;
    }

    public virtual void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Division(local, 2f);
    }

    private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f)
    {
      if ((double) ((Entity) this.NPC).Center.X < (double) targetPos.X)
      {
        ((Entity) this.NPC).velocity.X += speedModifier;
        if ((double) ((Entity) this.NPC).velocity.X < 0.0)
          ((Entity) this.NPC).velocity.X += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.NPC).velocity.X -= speedModifier;
        if ((double) ((Entity) this.NPC).velocity.X > 0.0)
          ((Entity) this.NPC).velocity.X -= speedModifier * 2f;
      }
      if ((double) ((Entity) this.NPC).Center.Y < (double) targetPos.Y)
      {
        ((Entity) this.NPC).velocity.Y += speedModifier;
        if ((double) ((Entity) this.NPC).velocity.Y < 0.0)
          ((Entity) this.NPC).velocity.Y += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.NPC).velocity.Y -= speedModifier;
        if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
          ((Entity) this.NPC).velocity.Y -= speedModifier * 2f;
      }
      if ((double) Math.Abs(((Entity) this.NPC).velocity.X) > (double) cap)
        ((Entity) this.NPC).velocity.X = cap * (float) Math.Sign(((Entity) this.NPC).velocity.X);
      if ((double) Math.Abs(((Entity) this.NPC).velocity.Y) <= (double) cap)
        return;
      ((Entity) this.NPC).velocity.Y = cap * (float) Math.Sign(((Entity) this.NPC).velocity.Y);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<InfestedBuff>(), 360, true, false);
      target.AddBuff(ModContent.BuffType<ClippedWingsBuff>(), 180, true, false);
    }

    public virtual bool CheckActive() => false;

    public virtual bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
      return new bool?(false);
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color alpha = this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects = this.NPC.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < NPCID.Sets.TrailCacheLength[this.NPC.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (NPCID.Sets.TrailCacheLength[this.NPC.type] - index) / (float) NPCID.Sets.TrailCacheLength[this.NPC.type]);
        Vector2 oldPo = this.NPC.oldPos[index];
        float rotation = this.NPC.rotation;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), color, rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
