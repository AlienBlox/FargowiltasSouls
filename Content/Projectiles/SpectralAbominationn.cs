// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.SpectralAbominationn
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class SpectralAbominationn : ModProjectile
  {
    private const float PI = 3.14159274f;
    private const float rotationPerTick = 0.0551156625f;
    private const float threshold = 150f;
    private float ringRotation;
    private float scytheRotation;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Assets/ExtraTextures/Eternals/AbominationnSoul";
    }

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 4;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 120;
      ((Entity) this.Projectile).height = 120;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.friendly = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 180;
      this.Projectile.FargoSouls().CanSplit = false;
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      base.OnSpawn(source);
      this.Projectile.ArmorPenetration += 600;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[1] == 0.0)
      {
        this.Projectile.localAI[1] = this.Projectile.ai[1] + 1f;
        SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        this.Projectile.ai[1] = 0.0f;
        this.Projectile.netUpdate = true;
      }
      if ((double) this.Projectile.localAI[0]++ > 30.0)
      {
        this.Projectile.localAI[0] = 0.0f;
        ++this.Projectile.ai[1];
      }
      if ((double) this.Projectile.ai[1] % 2.0 == 1.0)
      {
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
        int num = 7;
        for (int index1 = 0; index1 < num; ++index1)
        {
          Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), new Vector2((float) (((Entity) this.Projectile).width + 50) / 2f, (float) ((Entity) this.Projectile).height)), 0.75f), (double) (index1 - (num / 2 - 1)) * Math.PI / (double) num, new Vector2()), ((Entity) this.Projectile).Center);
          Vector2 vector2_2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (Main.rand.NextDouble() * 3.14159274101257) - 1.57079637f), (float) Main.rand.Next(3, 8));
          Vector2 vector2_3 = vector2_2;
          int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_3), 0, 0, 27, vector2_2.X * 2f, vector2_2.Y * 2f, 100, new Color(), 1.4f);
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
        if ((double) this.Projectile.localAI[0] == 30.0)
        {
          if ((double) this.Projectile.ai[0] >= 0.0 && Main.npc[index].CanBeChasedBy((object) null, false))
          {
            ((Entity) this.Projectile).velocity = Vector2.op_Subtraction(((Entity) Main.npc[index]).Center, ((Entity) this.Projectile).Center);
            ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
            Projectile projectile = this.Projectile;
            ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 27f);
            ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
          }
          else
          {
            this.Projectile.localAI[0] = -1f;
            this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 1000f);
            this.Projectile.netUpdate = true;
          }
        }
        else if ((double) this.Projectile.ai[0] >= 0.0 && Main.npc[index].CanBeChasedBy((object) null, false))
        {
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.npc[index]).Center, ((Entity) this.Projectile).Center);
          if ((double) vector2.X > 0.0)
          {
            vector2.X -= 300f;
            ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = 1;
          }
          else
          {
            vector2.X += 300f;
            ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = -1;
          }
          vector2.Y -= 200f;
          ((Vector2) ref vector2).Normalize();
          vector2 = Vector2.op_Multiply(vector2, 12f);
          if ((double) ((Entity) this.Projectile).velocity.X < (double) vector2.X)
          {
            ++((Entity) this.Projectile).velocity.X;
            if ((double) ((Entity) this.Projectile).velocity.X < 0.0 && (double) vector2.X > 0.0)
              ++((Entity) this.Projectile).velocity.X;
          }
          else if ((double) ((Entity) this.Projectile).velocity.X > (double) vector2.X)
          {
            --((Entity) this.Projectile).velocity.X;
            if ((double) ((Entity) this.Projectile).velocity.X > 0.0 && (double) vector2.X < 0.0)
              --((Entity) this.Projectile).velocity.X;
          }
          if ((double) ((Entity) this.Projectile).velocity.Y < (double) vector2.Y)
          {
            ++((Entity) this.Projectile).velocity.Y;
            if ((double) ((Entity) this.Projectile).velocity.Y < 0.0 && (double) vector2.Y > 0.0)
              ++((Entity) this.Projectile).velocity.Y;
          }
          else if ((double) ((Entity) this.Projectile).velocity.Y > (double) vector2.Y)
          {
            --((Entity) this.Projectile).velocity.Y;
            if ((double) ((Entity) this.Projectile).velocity.Y > 0.0 && (double) vector2.Y < 0.0)
              --((Entity) this.Projectile).velocity.Y;
          }
        }
        else
        {
          if ((double) ((Entity) this.Projectile).velocity.X < -1.0)
            ++((Entity) this.Projectile).velocity.X;
          else if ((double) ((Entity) this.Projectile).velocity.X > 1.0)
            --((Entity) this.Projectile).velocity.X;
          if ((double) ((Entity) this.Projectile).velocity.Y > -8.0)
            --((Entity) this.Projectile).velocity.Y;
          else if ((double) ((Entity) this.Projectile).velocity.Y < -10.0)
            ++((Entity) this.Projectile).velocity.Y;
          this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 1000f);
          this.Projectile.netUpdate = true;
        }
      }
      Projectile projectile1 = this.Projectile;
      ((Entity) projectile1).position = Vector2.op_Addition(((Entity) projectile1).position, Vector2.op_Division(((Entity) this.Projectile).velocity, 4f));
      if (++this.Projectile.frameCounter > 4)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= 4)
          this.Projectile.frame = 0;
      }
      this.ringRotation += (float) Math.PI / 57f;
      this.scytheRotation += 0.5f;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 0;
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 900, false);
    }

    public virtual void OnKill(int timeleft)
    {
      SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if (this.Projectile.owner != Main.myPlayer)
        return;
      this.SpawnRazorbladeRing(12, 12.5f, 0.75f);
      this.SpawnRazorbladeRing(12, 10f, -2f);
    }

    private void SpawnRazorbladeRing(int max, float speed, float rotationModifier)
    {
      float num1 = 6.28318548f / (float) max;
      Vector2 velocity = ((Entity) this.Projectile).velocity;
      ((Vector2) ref velocity).Normalize();
      Vector2 vector2 = Vector2.op_Multiply(velocity, speed);
      int num2 = ModContent.ProjectileType<AbomScytheFriendly>();
      for (int index1 = 0; index1 < max; ++index1)
      {
        vector2 = Utils.RotatedBy(vector2, (double) num1, new Vector2());
        int index2 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2, num2, this.Projectile.damage / 3, this.Projectile.knockBack / 4f, this.Projectile.owner, rotationModifier * (float) this.Projectile.spriteDirection, 0.0f, 0.0f);
        if (index2 != Main.maxProjectiles)
          Main.projectile[index2].DamageType = this.Projectile.DamageType;
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), 0.75f), 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      this.DrawRing();
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    private void DrawRing()
    {
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/AbomScytheFriendly", (AssetRequestMode) 1).Value;
      int num1 = texture2D.Height / 4;
      int num2 = 0;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color = Color.op_Multiply(Color.White, this.Projectile.Opacity);
      for (int index = 0; index < 6; ++index)
      {
        Vector2 vector2_2 = Utils.RotatedBy(Utils.RotatedBy(new Vector2(75f, 0.0f), (double) this.ringRotation, new Vector2()), 1.0471975803375244 * (double) index, new Vector2());
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, index % 2 == 0 ? this.scytheRotation : -this.scytheRotation, vector2_1, this.Projectile.scale, index % 2 == 0 ? (SpriteEffects) 0 : (SpriteEffects) 1, 0.0f);
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), 0.75f));
    }
  }
}
