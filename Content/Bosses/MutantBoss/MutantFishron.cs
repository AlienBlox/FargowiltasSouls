// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantFishron
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantFishron : ModProjectile
  {
    protected int p = -1;

    public virtual string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "FargowiltasSouls/Assets/ExtraTextures/Resprites/NPC_370" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantFishron_April";
      }
    }

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 8;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 11;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 90;
      ((Entity) this.Projectile).height = 90;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 240;
      this.Projectile.alpha = 100;
      this.CooldownSlot = 1;
    }

    public virtual bool CanHitPlayer(Player target) => target.hurtCooldowns[1] == 0;

    public virtual void SendExtraAI(BinaryWriter writer) => writer.Write(this.p);

    public virtual void ReceiveExtraAI(BinaryReader reader) => this.p = reader.ReadInt32();

    public virtual bool? CanDamage() => new bool?((double) this.Projectile.localAI[0] > 85.0);

    public virtual bool PreAI()
    {
      if ((double) this.Projectile.localAI[0] > 85.0)
      {
        int num = 5;
        for (int index1 = 0; index1 < num; ++index1)
        {
          Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), new Vector2((float) (((Entity) this.Projectile).width + 50) / 2f, (float) ((Entity) this.Projectile).height)), 0.75f), (double) (index1 - (num / 2 - 1)) * Math.PI / (double) num, new Vector2()), ((Entity) this.Projectile).Center);
          Vector2 vector2_2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (Main.rand.NextDouble() * 3.14159274101257) - 1.57079637f), (float) Main.rand.Next(3, 8));
          Vector2 vector2_3 = vector2_2;
          int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_3), 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, new Color(), 1.4f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].noLight = true;
          Main.dust[index2].shader = GameShaders.Armor.GetSecondaryShader(41, Main.LocalPlayer);
          Dust dust1 = Main.dust[index2];
          dust1.velocity = Vector2.op_Division(dust1.velocity, 4f);
          Dust dust2 = Main.dust[index2];
          dust2.velocity = Vector2.op_Subtraction(dust2.velocity, ((Entity) this.Projectile).velocity);
        }
      }
      return true;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[1] == 0.0)
      {
        this.Projectile.localAI[1] = 1f;
        SoundEngine.PlaySound(ref SoundID.Zombie20, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        this.p = Luminance.Common.Utilities.Utilities.AnyBosses() ? Main.npc[FargoSoulsGlobalNPC.boss].target : (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
        this.Projectile.netUpdate = true;
      }
      if ((double) ++this.Projectile.localAI[0] > 85.0)
      {
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
        this.Projectile.frameCounter = 5;
        this.Projectile.frame = 6;
      }
      else
      {
        int p = this.p;
        if ((double) this.Projectile.localAI[0] == 85.0)
        {
          ((Entity) this.Projectile).velocity = Vector2.op_Subtraction(((Entity) Main.player[p]).Center, ((Entity) this.Projectile).Center);
          ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, this.Projectile.type == ModContent.ProjectileType<MutantFishron>() ? 24f : 20f);
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
          this.Projectile.frameCounter = 5;
          this.Projectile.frame = 6;
        }
        else
        {
          Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[p]).Center, ((Entity) this.Projectile).Center);
          this.Projectile.rotation = Utils.ToRotation(vector2_1);
          if ((double) vector2_1.X > 0.0)
          {
            vector2_1.X -= 300f;
            ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = 1;
          }
          else
          {
            vector2_1.X += 300f;
            ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = -1;
          }
          Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) Main.player[p]).Center, new Vector2(this.Projectile.ai[0], this.Projectile.ai[1])), ((Entity) this.Projectile).Center), 4f);
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 19f), vector2_2), 20f);
          Projectile projectile = this.Projectile;
          ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Division(((Entity) Main.player[p]).velocity, 2f));
          if (++this.Projectile.frameCounter <= 5)
            return;
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame <= 5)
            return;
          this.Projectile.frame = 0;
        }
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      }
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 900, true, false);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 900, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      if ((double) this.Projectile.localAI[0] > 85.0)
      {
        for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 2)
        {
          Color color = Color.op_Multiply(Color.Lerp(alpha, Color.Pink, 0.25f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (1.5f * (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]));
          Vector2 oldPo = this.Projectile.oldPos[index];
          float num3 = this.Projectile.oldRot[index];
          if (this.Projectile.spriteDirection < 0)
            num3 += 3.14159274f;
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      float rotation = this.Projectile.rotation;
      if (this.Projectile.spriteDirection < 0)
        rotation += 3.14159274f;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      float num1 = (float) ((int) byte.MaxValue - this.Projectile.alpha) / (float) byte.MaxValue;
      float num2 = MathHelper.Lerp(num1, 1f, 0.25f);
      if ((double) num2 > 1.0)
        num2 = 1f;
      return new Color?(new Color((int) ((double) ((Color) ref lightColor).R * (double) num2), (int) ((double) ((Color) ref lightColor).G * (double) num2), (int) ((double) ((Color) ref lightColor).B * (double) num2), (int) ((double) ((Color) ref lightColor).A * (double) num1)));
    }
  }
}
