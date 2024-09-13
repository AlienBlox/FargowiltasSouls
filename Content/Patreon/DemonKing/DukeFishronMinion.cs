// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.DemonKing.DukeFishronMinion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.DemonKing
{
  public class DukeFishronMinion : ModProjectile
  {
    private const float PI = 3.14159274f;
    private float rotationOffset;
    private bool spawn;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 8;
      ProjectileID.Sets.MinionTargettingFeature[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.MinionSacrificable[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 150;
      ((Entity) this.Projectile).height = 100;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.alpha = 100;
      this.Projectile.minionSlots = 1f;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 10;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.scale *= 0.75f;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.rotationOffset);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.rotationOffset = reader.ReadSingle();
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      base.OnSpawn(source);
      this.Projectile.ArmorPenetration += 400;
    }

    public virtual void AI()
    {
      if (!this.spawn)
      {
        this.spawn = true;
        this.Projectile.ai[0] = -1f;
      }
      if (((Entity) Main.player[this.Projectile.owner]).active && !Main.player[this.Projectile.owner].dead && Main.player[this.Projectile.owner].FargoSouls().DukeFishron)
        this.Projectile.timeLeft = 2;
      if ((double) ((Entity) this.Projectile).Distance(((Entity) Main.player[this.Projectile.owner]).Center) > 3000.0)
        ((Entity) this.Projectile).Center = ((Entity) Main.player[this.Projectile.owner]).Center;
      if ((double) this.Projectile.localAI[0]++ > 30.0)
      {
        this.Projectile.localAI[0] = 0.0f;
        this.rotationOffset = Utils.NextFloat(Main.rand, -1.57079637f, 1.57079637f);
        ++this.Projectile.ai[1];
      }
      if ((double) this.Projectile.localAI[1] > 0.0)
        --this.Projectile.localAI[1];
      if ((double) this.Projectile.ai[1] % 2.0 == 1.0)
      {
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
        this.Projectile.frameCounter = 5;
        this.Projectile.frame = 6;
        if (this.Projectile.spriteDirection < 0)
          this.Projectile.rotation += 3.14159274f;
        int num = 7;
        for (int index1 = 0; index1 < num; ++index1)
        {
          Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), new Vector2((float) (((Entity) this.Projectile).width + 50) / 2f, (float) ((Entity) this.Projectile).height)), 0.75f), (double) (index1 - (num / 2 - 1)) * Math.PI / (double) num, new Vector2()), ((Entity) this.Projectile).Center);
          Vector2 vector2_2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (Main.rand.NextDouble() * 3.14159274101257) - 1.57079637f), (float) Main.rand.Next(3, 8));
          Vector2 vector2_3 = vector2_2;
          int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_3), 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, new Color(), 1.4f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].noLight = true;
          Dust dust1 = Main.dust[index2];
          dust1.velocity = Vector2.op_Division(dust1.velocity, 4f);
          Dust dust2 = Main.dust[index2];
          dust2.velocity = Vector2.op_Subtraction(dust2.velocity, ((Entity) this.Projectile).velocity);
        }
      }
      else
      {
        int index = (int) this.Projectile.ai[0];
        float num1 = 1f;
        if ((double) this.Projectile.localAI[0] == 30.0)
        {
          if ((double) this.Projectile.ai[0] >= 0.0 && Main.npc[index].CanBeChasedBy((object) null, false))
          {
            ((Entity) this.Projectile).velocity = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) Main.npc[index]).Center, ((Entity) this.Projectile).Center), Vector2.op_Multiply(((Entity) Main.npc[index]).velocity, 10f));
            ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
            Projectile projectile = this.Projectile;
            ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 27f);
            this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
            ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
            this.Projectile.frameCounter = 5;
            this.Projectile.frame = 6;
            if (this.Projectile.spriteDirection < 0)
              this.Projectile.rotation += 3.14159274f;
          }
          else
          {
            this.Projectile.localAI[0] = -1f;
            this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1500f, center: new Vector2());
            this.Projectile.netUpdate = true;
            if (++this.Projectile.frameCounter > 5)
            {
              this.Projectile.frameCounter = 0;
              if (++this.Projectile.frame > 5)
                this.Projectile.frame = 0;
            }
          }
        }
        else
        {
          if ((double) this.Projectile.localAI[0] == 0.0)
            this.Projectile.localAI[0] = (float) Main.rand.Next(10);
          if ((double) this.Projectile.ai[0] >= 0.0 && Main.npc[index].CanBeChasedBy((object) null, false))
          {
            float num2 = num1 * 1.5f;
            Vector2 vector2_4 = Vector2.op_Subtraction(((Entity) Main.npc[index]).Center, ((Entity) this.Projectile).Center);
            this.Projectile.rotation = Utils.ToRotation(vector2_4);
            Vector2 zero = Vector2.Zero;
            if ((double) vector2_4.X > 0.0)
            {
              zero.X = -360f;
              ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = 1;
            }
            else
            {
              zero.X = 360f;
              ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = -1;
            }
            Vector2 vector2_5 = Utils.RotatedBy(zero, (double) this.rotationOffset, new Vector2());
            Vector2 vector2_6 = Vector2.op_Addition(vector2_4, vector2_5);
            ((Vector2) ref vector2_6).Normalize();
            Vector2 vector2_7 = Vector2.op_Multiply(vector2_6, 24f);
            if ((double) ((Entity) this.Projectile).velocity.X < (double) vector2_7.X)
            {
              ((Entity) this.Projectile).velocity.X += num2;
              if ((double) ((Entity) this.Projectile).velocity.X < 0.0 && (double) vector2_7.X > 0.0)
                ((Entity) this.Projectile).velocity.X += num2;
            }
            else if ((double) ((Entity) this.Projectile).velocity.X > (double) vector2_7.X)
            {
              ((Entity) this.Projectile).velocity.X -= num2;
              if ((double) ((Entity) this.Projectile).velocity.X > 0.0 && (double) vector2_7.X < 0.0)
                ((Entity) this.Projectile).velocity.X -= num2;
            }
            if ((double) ((Entity) this.Projectile).velocity.Y < (double) vector2_7.Y)
            {
              ((Entity) this.Projectile).velocity.Y += num2;
              if ((double) ((Entity) this.Projectile).velocity.Y < 0.0 && (double) vector2_7.Y > 0.0)
                ((Entity) this.Projectile).velocity.Y += num2;
            }
            else if ((double) ((Entity) this.Projectile).velocity.Y > (double) vector2_7.Y)
            {
              ((Entity) this.Projectile).velocity.Y -= num2;
              if ((double) ((Entity) this.Projectile).velocity.Y > 0.0 && (double) vector2_7.Y < 0.0)
                ((Entity) this.Projectile).velocity.Y -= num2;
            }
            if (this.Projectile.spriteDirection < 0)
              this.Projectile.rotation += 3.14159274f;
          }
          else
          {
            Vector2 center = ((Entity) Main.player[this.Projectile.owner]).Center;
            center.X -= (float) (60 * ((Entity) Main.player[this.Projectile.owner]).direction * this.Projectile.minionPos);
            center.Y -= 40f;
            if ((double) ((Entity) this.Projectile).Distance(center) > 25.0)
            {
              float num3 = num1 * 0.5f;
              Vector2 vector2_8 = Vector2.op_Subtraction(center, ((Entity) this.Projectile).Center);
              this.Projectile.rotation = 0.0f;
              ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = ((Entity) Main.player[this.Projectile.owner]).direction;
              ((Vector2) ref vector2_8).Normalize();
              Vector2 vector2_9 = Vector2.op_Multiply(vector2_8, 24f);
              if ((double) ((Entity) this.Projectile).velocity.X < (double) vector2_9.X)
              {
                ((Entity) this.Projectile).velocity.X += num3;
                if ((double) ((Entity) this.Projectile).velocity.X < 0.0 && (double) vector2_9.X > 0.0)
                  ((Entity) this.Projectile).velocity.X += num3;
              }
              else if ((double) ((Entity) this.Projectile).velocity.X > (double) vector2_9.X)
              {
                ((Entity) this.Projectile).velocity.X -= num3;
                if ((double) ((Entity) this.Projectile).velocity.X > 0.0 && (double) vector2_9.X < 0.0)
                  ((Entity) this.Projectile).velocity.X -= num3;
              }
              if ((double) ((Entity) this.Projectile).velocity.Y < (double) vector2_9.Y)
              {
                ((Entity) this.Projectile).velocity.Y += num3;
                if ((double) ((Entity) this.Projectile).velocity.Y < 0.0 && (double) vector2_9.Y > 0.0)
                  ((Entity) this.Projectile).velocity.Y += num3;
              }
              else if ((double) ((Entity) this.Projectile).velocity.Y > (double) vector2_9.Y)
              {
                ((Entity) this.Projectile).velocity.Y -= num3;
                if ((double) ((Entity) this.Projectile).velocity.Y > 0.0 && (double) vector2_9.Y < 0.0)
                  ((Entity) this.Projectile).velocity.Y -= num3;
              }
            }
            if ((double) this.Projectile.ai[0] != -1.0)
            {
              this.Projectile.ai[0] = -1f;
              this.Projectile.localAI[0] = 0.0f;
              this.Projectile.netUpdate = true;
            }
            if ((double) this.Projectile.localAI[0] > 6.0)
            {
              this.Projectile.localAI[0] = 0.0f;
              this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1500f, center: new Vector2());
              this.Projectile.netUpdate = true;
            }
          }
          if (++this.Projectile.frameCounter > 5)
          {
            this.Projectile.frameCounter = 0;
            if (++this.Projectile.frame > 5)
              this.Projectile.frame = 0;
          }
        }
      }
      Projectile projectile1 = this.Projectile;
      ((Entity) projectile1).position = Vector2.op_Addition(((Entity) projectile1).position, Vector2.op_Division(((Entity) this.Projectile).velocity, 4f));
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 900, false);
      if ((double) this.Projectile.localAI[1] > 0.0)
        return;
      this.Projectile.localAI[1] = 60f;
      SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if (this.Projectile.owner != Main.myPlayer)
        return;
      int num = Utils.NextBool(Main.rand) ? -1 : 1;
      this.SpawnRazorbladeRing(7, 24f, -0.75f * (float) num);
      this.SpawnRazorbladeRing(7, 24f, 1.5f * (float) num);
    }

    private void SpawnRazorbladeRing(int max, float speed, float rotationModifier)
    {
      float num1 = 6.28318548f / (float) max;
      Vector2 velocity = ((Entity) this.Projectile).velocity;
      ((Vector2) ref velocity).Normalize();
      Vector2 vector2 = Vector2.op_Multiply(velocity, speed);
      int num2 = ModContent.ProjectileType<RazorbladeTyphoonFriendly2>();
      int num3 = (int) ((double) this.Projectile.damage / 3.5999999046325684);
      for (int index = 0; index < max; ++index)
      {
        vector2 = Utils.RotatedBy(vector2, (double) num1, new Vector2());
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2, num2, num3, this.Projectile.knockBack / 4f, this.Projectile.owner, rotationModifier * (float) this.Projectile.spriteDirection, 0.0f, 0.0f);
      }
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
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.Blue, this.Projectile.Opacity), 0.2f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
