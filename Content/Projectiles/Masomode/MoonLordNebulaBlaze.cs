// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MoonLordNebulaBlaze
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MoonLordNebulaBlaze : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_634";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.aiStyle = -1;
      this.Projectile.timeLeft = 3600;
      this.Projectile.tileCollide = false;
      this.Projectile.hostile = true;
      this.Projectile.extraUpdates = 2;
      this.Projectile.scale = 1.5f;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      float num1 = 5f;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector(8f, 10f);
      float num2 = 1.2f;
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector(0.7f, 0.1f, 0.5f);
      int num3 = 4 * this.Projectile.MaxUpdates;
      int num4 = Utils.SelectRandom<int>(Main.rand, new int[5]
      {
        242,
        73,
        72,
        71,
        (int) byte.MaxValue
      });
      int maxValue = (int) byte.MaxValue;
      if ((double) this.Projectile.ai[1] == 0.0)
      {
        this.Projectile.ai[1] = 1f;
        this.Projectile.localAI[0] = (float) -Main.rand.Next(48);
        SoundEngine.PlaySound(ref SoundID.Item34, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      }
      else if ((double) this.Projectile.ai[1] != 1.0 || this.Projectile.owner != Main.myPlayer)
      {
        double num5 = (double) this.Projectile.ai[1];
      }
      if ((double) this.Projectile.ai[1] >= 1.0 && (double) this.Projectile.ai[1] < (double) num1)
      {
        ++this.Projectile.ai[1];
        if ((double) this.Projectile.ai[1] == (double) num1)
          this.Projectile.ai[1] = 1f;
      }
      this.Projectile.alpha -= 40;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      this.Projectile.spriteDirection = ((Entity) this.Projectile).direction;
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter >= num3)
      {
        ++this.Projectile.frame;
        this.Projectile.frameCounter = 0;
        if (this.Projectile.frame >= 4)
          this.Projectile.frame = 0;
      }
      Lighting.AddLight(((Entity) this.Projectile).Center, vector3);
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] == 48.0)
        this.Projectile.localAI[0] = 0.0f;
      else if (this.Projectile.alpha == 0)
      {
        for (int index1 = 0; index1 < 2; ++index1)
        {
          Vector2.op_Multiply(Vector2.UnitX, -30f);
          Vector2 vector2_2 = Vector2.op_Subtraction(Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) this.Projectile.localAI[0] * 0.130899697542191 + (double) index1 * 3.14159274101257, new Vector2())), vector2_1), Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 10f));
          int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, maxValue, 0.0f, 0.0f, 160, new Color(), 1f);
          Main.dust[index2].scale = num2;
          Main.dust[index2].noGravity = true;
          Main.dust[index2].position = Vector2.op_Addition(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2f));
          Main.dust[index2].velocity = Vector2.op_Addition(Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2f), 8f)), Main.dust[index2].position)), 2f), Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2f));
        }
      }
      if (Utils.NextBool(Main.rand, 12))
      {
        for (int index3 = 0; index3 < 1; ++index3)
        {
          Vector2 vector2_3 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.196349546313286), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
          int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1f);
          Dust dust = Main.dust[index4];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.1f);
          Main.dust[index4].position = Vector2.op_Addition(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2_3, (float) ((Entity) this.Projectile).width), 2f)), Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2f));
          Main.dust[index4].fadeIn = 0.9f;
        }
      }
      if (Utils.NextBool(Main.rand, 64))
      {
        for (int index5 = 0; index5 < 1; ++index5)
        {
          Vector2 vector2_4 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.392699092626572), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
          int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 155, new Color(), 0.8f);
          Dust dust = Main.dust[index6];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.3f);
          Main.dust[index6].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2_4, (float) ((Entity) this.Projectile).width), 2f));
          if (Utils.NextBool(Main.rand))
            Main.dust[index6].fadeIn = 1.4f;
        }
      }
      if (Utils.NextBool(Main.rand, 4))
      {
        for (int index7 = 0; index7 < 2; ++index7)
        {
          Vector2 vector2_5 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.785398185253143), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
          int index8 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num4, 0.0f, 0.0f, 0, new Color(), 1.2f);
          Dust dust = Main.dust[index8];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.3f);
          Main.dust[index8].noGravity = true;
          Main.dust[index8].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2_5, (float) ((Entity) this.Projectile).width), 2f));
          if (Utils.NextBool(Main.rand))
            Main.dust[index8].fadeIn = 1.4f;
        }
      }
      if (Utils.NextBool(Main.rand, 12) && this.Projectile.type == 634)
      {
        Vector2 vector2_6 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.196349546313286), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
        int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, maxValue, 0.0f, 0.0f, 100, new Color(), 1f);
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.3f);
        Main.dust[index].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2_6, (float) ((Entity) this.Projectile).width), 2f));
        Main.dust[index].fadeIn = 0.9f;
        Main.dust[index].noGravity = true;
      }
      if (!Utils.NextBool(Main.rand, 3) || this.Projectile.type != 635)
        return;
      Vector2 vector2_7 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.196349546313286), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
      int index9 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, maxValue, 0.0f, 0.0f, 100, new Color(), 1f);
      Dust dust1 = Main.dust[index9];
      dust1.velocity = Vector2.op_Multiply(dust1.velocity, 0.3f);
      Main.dust[index9].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2_7, (float) ((Entity) this.Projectile).width), 2f));
      Main.dust[index9].fadeIn = 1.2f;
      Main.dust[index9].scale = 1.5f;
      Main.dust[index9].noGravity = true;
    }

    public virtual void OnKill(int timeLeft)
    {
      Utils.SelectRandom<int>(Main.rand, new int[5]
      {
        242,
        73,
        72,
        71,
        (int) byte.MaxValue
      });
      int num1 = (int) byte.MaxValue;
      int num2 = (int) byte.MaxValue;
      int num3 = 50;
      float num4 = 1.7f;
      float num5 = 0.8f;
      float num6 = 2f;
      Vector2 vector2 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation - 1.57079637f), ((Vector2) ref ((Entity) this.Projectile).velocity).Length()), (float) this.Projectile.MaxUpdates);
      if (this.Projectile.type == 635)
      {
        num1 = 88;
        num2 = 88;
        Utils.SelectRandom<int>(Main.rand, new int[3]
        {
          242,
          59,
          88
        });
        num4 = 3.7f;
        num5 = 1.5f;
        num6 = 2.2f;
        vector2 = Vector2.op_Multiply(vector2, 0.5f);
      }
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = num3;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      for (int index1 = 0; index1 < 40; ++index1)
      {
        int num7 = Utils.SelectRandom<int>(Main.rand, new int[5]
        {
          242,
          73,
          72,
          71,
          (int) byte.MaxValue
        });
        if (this.Projectile.type == 635)
          num7 = Utils.SelectRandom<int>(Main.rand, new int[3]
          {
            242,
            59,
            88
          });
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num7, 0.0f, 0.0f, 200, new Color(), num4);
        Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 3.14159274101257), (float) Main.rand.NextDouble()), (float) ((Entity) this.Projectile).width), 2f));
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 3f);
        Dust dust2 = Main.dust[index2];
        dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(vector2, Utils.NextFloat(Main.rand)));
        int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num1, 0.0f, 0.0f, 100, new Color(), num5);
        Main.dust[index3].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 3.14159274101257), (float) Main.rand.NextDouble()), (float) ((Entity) this.Projectile).width), 2f));
        Dust dust3 = Main.dust[index3];
        dust3.velocity = Vector2.op_Multiply(dust3.velocity, 2f);
        Main.dust[index3].noGravity = true;
        Main.dust[index3].fadeIn = 1f;
        Main.dust[index3].color = Color.op_Multiply(Color.Crimson, 0.5f);
        Dust dust4 = Main.dust[index3];
        dust4.velocity = Vector2.op_Addition(dust4.velocity, Vector2.op_Multiply(vector2, Utils.NextFloat(Main.rand)));
      }
      for (int index4 = 0; index4 < 20; ++index4)
      {
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num2, 0.0f, 0.0f, 0, new Color(), num6);
        Main.dust[index5].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 3.14159274101257), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()), (float) ((Entity) this.Projectile).width), 3f));
        Main.dust[index5].noGravity = true;
        Dust dust5 = Main.dust[index5];
        dust5.velocity = Vector2.op_Multiply(dust5.velocity, 0.5f);
        Dust dust6 = Main.dust[index5];
        dust6.velocity = Vector2.op_Addition(dust6.velocity, Vector2.op_Multiply(vector2, (float) (0.600000023841858 + 0.600000023841858 * (double) Utils.NextFloat(Main.rand))));
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<LethargicBuff>(), 300, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
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
