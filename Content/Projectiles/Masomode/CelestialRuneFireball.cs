// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.CelestialRuneFireball
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class CelestialRuneFireball : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_467";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 4;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 40;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.ignoreWater = true;
      this.Projectile.extraUpdates = 1;
      this.Projectile.timeLeft = 360;
      this.Projectile.penetrate = 1;
      Mod mod;
      if (!Terraria.ModLoader.ModLoader.TryGetMod("Fargowiltas", ref mod))
        return;
      mod.Call(new object[2]
      {
        (object) "LowRenderProj",
        (object) this.Projectile
      });
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1]);
      if (npc != null)
      {
        if (npc.CanBeChasedBy((object) null, false))
        {
          float rotation1 = Utils.ToRotation(((Entity) this.Projectile).velocity);
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center);
          if ((double) ((Vector2) ref vector2).Length() < 20.0)
          {
            this.Projectile.Kill();
            return;
          }
          float rotation2 = Utils.ToRotation(vector2);
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(new Vector2(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), 0.0f), (double) Utils.AngleLerp(rotation1, rotation2, (float) (0.00800000037997961 + 0.079999998211860657 * (double) Math.Min(1f, this.Projectile.ai[0] / 180f))), new Vector2());
          if (this.Projectile.timeLeft % this.Projectile.MaxUpdates == 0)
            ++this.Projectile.ai[0];
        }
        else
          this.Projectile.ai[1] = -1f;
      }
      else if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.ai[1] = (float) FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 1000f, true);
        this.Projectile.netUpdate = true;
      }
      this.Projectile.alpha -= 40;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter > 2)
      {
        this.Projectile.frameCounter = 0;
        ++this.Projectile.frame;
        if (this.Projectile.frame > 3)
          this.Projectile.frame = 0;
      }
      Lighting.AddLight(((Entity) this.Projectile).Center, 1.1f, 0.9f, 0.4f);
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] == 12.0)
      {
        this.Projectile.localAI[0] = 0.0f;
        for (int index1 = 0; index1 < 12; ++index1)
        {
          Vector2 vector2 = Utils.RotatedBy(Vector2.op_Addition(Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitX, (float) -((Entity) this.Projectile).width), 2f), Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) index1 * 3.14159274101257 / 6.0, new Vector2())), new Vector2(8f, 16f))), (double) this.Projectile.rotation - 1.5707963705062866, new Vector2());
          int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 6, 0.0f, 0.0f, 160, new Color(), 1f);
          Main.dust[index2].scale = 1.1f;
          Main.dust[index2].noGravity = true;
          Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2);
          Main.dust[index2].velocity = Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.1f);
          Main.dust[index2].velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 3f)), Main.dust[index2].position)), 1.25f);
        }
      }
      if (Utils.NextBool(Main.rand, 4))
      {
        for (int index3 = 0; index3 < 1; ++index3)
        {
          Vector2 vector2 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.196349546313286), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
          int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1f);
          Dust dust = Main.dust[index4];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.1f);
          Main.dust[index4].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2, (float) ((Entity) this.Projectile).width), 2f));
          Main.dust[index4].fadeIn = 0.9f;
        }
      }
      if (Utils.NextBool(Main.rand, 32))
      {
        for (int index5 = 0; index5 < 1; ++index5)
        {
          Vector2 vector2 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.392699092626572), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
          int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 155, new Color(), 0.8f);
          Dust dust = Main.dust[index6];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.3f);
          Main.dust[index6].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2, (float) ((Entity) this.Projectile).width), 2f));
          if (Utils.NextBool(Main.rand))
            Main.dust[index6].fadeIn = 1.4f;
        }
      }
      if (!Utils.NextBool(Main.rand))
        return;
      for (int index7 = 0; index7 < 2; ++index7)
      {
        Vector2 vector2 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.785398185253143), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
        int index8 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 0, new Color(), 1.2f);
        Dust dust = Main.dust[index8];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.3f);
        Main.dust[index8].noGravity = true;
        Main.dust[index8].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2, (float) ((Entity) this.Projectile).width), 2f));
        if (Utils.NextBool(Main.rand))
          Main.dust[index8].fadeIn = 1.4f;
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(189, 240, false);
      if (this.Projectile.penetrate != -1)
        return;
      target.immune[this.Projectile.owner] = 0;
    }

    public virtual void OnKill(int timeLeft)
    {
      if ((double) this.Projectile.localAI[1] != 0.0)
        return;
      this.Projectile.localAI[1] = 1f;
      this.Projectile.penetrate = -1;
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 176;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      this.Projectile.Damage();
      for (int index1 = 0; index1 < 4; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 3.14159274101257), (float) Main.rand.NextDouble()), (float) ((Entity) this.Projectile).width), 2f));
      }
      for (int index3 = 0; index3 < 30; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 200, new Color(), 3.7f);
        Main.dust[index4].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 3.14159274101257), (float) Main.rand.NextDouble()), (float) ((Entity) this.Projectile).width), 2f));
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 3f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index5].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 3.14159274101257), (float) Main.rand.NextDouble()), (float) ((Entity) this.Projectile).width), 2f));
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
        Main.dust[index5].noGravity = true;
        Main.dust[index5].fadeIn = 2.5f;
      }
      for (int index6 = 0; index6 < 10; ++index6)
      {
        int index7 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 0, new Color(), 2.7f);
        Main.dust[index7].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 3.14159274101257), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()), (float) ((Entity) this.Projectile).width), 2f));
        Main.dust[index7].noGravity = true;
        Dust dust = Main.dust[index7];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
      }
      for (int index8 = 0; index8 < 10; ++index8)
      {
        int index9 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index9].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 3.14159274101257), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()), (float) ((Entity) this.Projectile).width), 2f));
        Main.dust[index9].noGravity = true;
        Dust dust = Main.dust[index9];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
      }
      for (int index10 = 0; index10 < 2; ++index10)
      {
        int index11 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).position, new Vector2((float) (((Entity) this.Projectile).width * Main.rand.Next(100)) / 100f, (float) (((Entity) this.Projectile).height * Main.rand.Next(100)) / 100f)), Vector2.op_Multiply(Vector2.One, 10f)), new Vector2(), Main.rand.Next(61, 64), 1f);
        Main.gore[index11].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 3.14159274101257), (float) Main.rand.NextDouble()), (float) ((Entity) this.Projectile).width), 2f));
        Gore gore = Main.gore[index11];
        gore.velocity = Vector2.op_Multiply(gore.velocity, 0.3f);
        Main.gore[index11].velocity.X += (float) Main.rand.Next(-10, 11) * 0.05f;
        Main.gore[index11].velocity.Y += (float) Main.rand.Next(-10, 11) * 0.05f;
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, 150, 150, (int) byte.MaxValue), (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue)));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
