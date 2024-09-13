// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.LunarCultistLight
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class LunarCultistLight : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/NPC_522";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = 0;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = true;
      this.Projectile.timeLeft = 240;
      this.Projectile.penetrate = 1;
    }

    public virtual void AI()
    {
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.ai[0] = -1f;
        for (int index1 = 0; index1 < 13; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 261, ((Entity) this.Projectile).velocity.X * 0.5f, ((Entity) this.Projectile).velocity.Y * 0.5f, 90, new Color(), 2.5f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].fadeIn = 1f;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
          Main.dust[index2].noLight = true;
        }
      }
      for (int index3 = 0; index3 < 2; ++index3)
      {
        if (Main.rand.Next(10 - (int) Math.Min(7f, ((Vector2) ref ((Entity) this.Projectile).velocity).Length())) < 1)
        {
          int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 261, ((Entity) this.Projectile).velocity.X * 0.5f, ((Entity) this.Projectile).velocity.Y * 0.5f, 90, new Color(), 2.5f);
          Main.dust[index4].noGravity = true;
          Dust dust1 = Main.dust[index4];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 0.2f);
          Main.dust[index4].fadeIn = 0.4f;
          if (Utils.NextBool(Main.rand, 6))
          {
            Dust dust2 = Main.dust[index4];
            dust2.velocity = Vector2.op_Multiply(dust2.velocity, 5f);
            Main.dust[index4].noLight = true;
          }
          else
            Main.dust[index4].velocity = Vector2.op_Multiply(((Entity) this.Projectile).DirectionFrom(Main.dust[index4].position), ((Vector2) ref Main.dust[index4].velocity).Length());
        }
      }
      if (this.Projectile.timeLeft < 180)
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) this.Projectile.ai[1], new Vector2());
      if (this.Projectile.timeLeft < 120)
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.98f);
      if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 0.20000000298023224)
        ((Entity) this.Projectile).velocity = Vector2.Zero;
      if ((double) this.Projectile.ai[0] > -1.0 && (double) this.Projectile.ai[0] < 200.0)
      {
        Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.npc[(int) this.Projectile.ai[0]]).Center, ((Entity) this.Projectile).Center);
        ((Vector2) ref vector2_1).Normalize();
        Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 9f);
        ((Entity) this.Projectile).velocity.X += vector2_2.X / 100f;
        if ((double) ((Entity) this.Projectile).velocity.X > 9.0)
          ((Entity) this.Projectile).velocity.X = 9f;
        else if ((double) ((Entity) this.Projectile).velocity.X < -9.0)
          ((Entity) this.Projectile).velocity.X = -9f;
        ((Entity) this.Projectile).velocity.Y += vector2_2.Y / 100f;
        if ((double) ((Entity) this.Projectile).velocity.Y > 9.0)
        {
          ((Entity) this.Projectile).velocity.Y = 9f;
        }
        else
        {
          if ((double) ((Entity) this.Projectile).velocity.Y >= -9.0)
            return;
          ((Entity) this.Projectile).velocity.Y = -9f;
        }
      }
      else
      {
        if ((double) this.Projectile.localAI[1]++ <= 15.0)
          return;
        this.Projectile.localAI[1] = 0.0f;
        float num1 = 2000f;
        int num2 = -1;
        for (int index = 0; index < 200; ++index)
        {
          NPC npc = Main.npc[index];
          if (npc.CanBeChasedBy((object) null, false))
          {
            float num3 = ((Entity) this.Projectile).Distance(((Entity) npc).Center);
            if ((double) num3 < (double) num1)
            {
              num1 = num3;
              num2 = index;
            }
          }
        }
        if (num2 <= -1)
          return;
        this.Projectile.ai[0] = (float) num2;
        this.Projectile.netUpdate = true;
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 13; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 262, 0.0f, 0.0f, 90, new Color(), 2.5f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].fadeIn = 1f;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
        Main.dust[index2].noLight = true;
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color((int) byte.MaxValue - this.Projectile.alpha, (int) byte.MaxValue - this.Projectile.alpha, (int) byte.MaxValue - this.Projectile.alpha, (int) byte.MaxValue - this.Projectile.alpha));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      Rectangle bounds = texture2D.Bounds;
      Vector2 vector2 = Vector2.op_Division(Utils.Size(bounds), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(bounds), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
