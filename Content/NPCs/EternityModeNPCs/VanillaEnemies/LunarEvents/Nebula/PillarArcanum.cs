// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Nebula.PillarArcanum
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
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Nebula
{
  public class PillarArcanum : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_617";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 240;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual bool? CanDamage() => new bool?((double) this.Projectile.scale >= 3.0);

    public virtual void AI()
    {
      int num1 = 360;
      int num2 = 6;
      if ((double) this.Projectile.ai[1] == 0.0)
        num1 = 30;
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] > 50.0)
      {
        if ((double) this.Projectile.ai[0] <= 90.0)
        {
          this.Projectile.scale = (float) (((double) this.Projectile.ai[0] - 50.0) / 40.0) * (float) num2;
          this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale / (double) num2);
          this.Projectile.rotation -= 0.1570796f;
          if (Utils.NextBool(Main.rand))
          {
            Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
            Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 242, 0.0f, 0.0f, 0, new Color(), 1f)];
            dust.noGravity = true;
            dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, (float) Main.rand.Next(10, 21)));
            dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, 1.5707963705062866, new Vector2()), 6f);
            dust.scale = 0.5f + Utils.NextFloat(Main.rand);
            dust.fadeIn = 0.5f;
            dust.customData = (object) ((Entity) this.Projectile).Center;
          }
          if (Utils.NextBool(Main.rand))
          {
            Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
            Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 242, 0.0f, 0.0f, 0, new Color(), 1f)];
            dust.noGravity = true;
            dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f));
            dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, -1.5707963705062866, new Vector2()), 3f);
            dust.scale = 0.5f + Utils.NextFloat(Main.rand);
            dust.fadeIn = 0.5f;
            dust.customData = (object) ((Entity) this.Projectile).Center;
          }
        }
        else
        {
          this.Projectile.scale = (float) num2;
          this.Projectile.alpha = 0;
          this.Projectile.rotation -= (float) ((double) MathHelper.Lerp(1.5f, 0.4f, (float) this.Projectile.timeLeft / 180f) * 3.1415927410125732 / 60.0);
          if (Utils.NextBool(Main.rand))
          {
            Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
            Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 242, 0.0f, 0.0f, 0, new Color(), 1f)];
            dust.noGravity = true;
            dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, (float) Main.rand.Next(10, 21)));
            dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, 1.5707963705062866, new Vector2()), 6f);
            dust.scale = 0.5f + Utils.NextFloat(Main.rand);
            dust.fadeIn = 0.5f;
            dust.customData = (object) ((Entity) this.Projectile).Center;
          }
          int num3 = 2;
          if ((double) this.Projectile.localAI[0] % (double) num3 == 0.0 && this.Projectile.timeLeft > 30 && FargoSoulsUtil.HostCheck)
          {
            float num4 = Utils.NextFloat(Main.rand, 4f, 6f);
            float num5 = this.Projectile.rotation + (float) (int) ((double) this.Projectile.localAI[0] / (double) num3 % 4.0) * 1.57079637f;
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(num4, Utils.ToRotationVector2(num5)), ModContent.ProjectileType<PillarNebulaBlaze>(), this.Projectile.damage, this.Projectile.knockBack, -1, 0.0f, 0.0f, 0.0f);
            this.Projectile.localAI[1] += MathHelper.ToRadians(0.8f * (float) num3) * this.Projectile.ai[1];
          }
          if (this.Projectile.timeLeft == 29)
          {
            SoundStyle soundStyle = SoundID.Item20;
            ((SoundStyle) ref soundStyle).Pitch = -1f;
            SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          }
          ++this.Projectile.localAI[0];
        }
      }
      if ((double) this.Projectile.localAI[0] % 30.0 == 0.0 && NPC.CountNPCS(507) <= 0)
      {
        this.Projectile.scale = (float) (1.0 - ((double) this.Projectile.ai[0] - (double) num1) / 60.0) * (float) num2;
        this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale / (double) num2);
        this.Projectile.rotation -= (float) Math.PI / 30f;
        if (this.Projectile.alpha >= (int) byte.MaxValue)
          this.Projectile.Kill();
        Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
        Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 242, 0.0f, 0.0f, 0, new Color(), 1f)];
        dust.noGravity = true;
        dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, (float) Main.rand.Next(10, 21)));
        dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, 1.5707963705062866, new Vector2()), 6f);
        dust.scale = 0.5f + Utils.NextFloat(Main.rand);
        dust.fadeIn = 0.5f;
        dust.customData = (object) ((Entity) this.Projectile).Center;
      }
      if (Utils.NextBool(Main.rand))
      {
        Dust dust = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 242, 0.0f, 0.0f, 0, new Color(), 1f)];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
        dust.fadeIn = 1f;
        dust.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
        dust.noGravity = true;
      }
      float num6 = 0.5f;
      for (int index = 0; index < 5; ++index)
      {
        if ((double) Utils.NextFloat(Main.rand) >= (double) num6)
        {
          float num7 = Utils.NextFloat(Main.rand) * 6.283185f;
          float num8 = Utils.NextFloat(Main.rand);
          Dust dust = Dust.NewDustPerfect(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.ToRotationVector2(num7), (float) (110.0 + 600.0 * (double) num8))), 242, new Vector2?(Vector2.op_Multiply(Utils.ToRotationVector2(num7 - 3.141593f), (float) (14.0 + 8.0 * (double) num8))), 0, new Color(), 1f);
          dust.scale = 0.9f;
          dust.fadeIn = (float) (1.1499999761581421 + (double) num8 * 0.30000001192092896);
          dust.noGravity = true;
        }
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index = 0; index < 40; ++index)
      {
        float num1 = Utils.NextFloat(Main.rand, 4f, 6f);
        float num2 = (float) (((double) index / 40.0 + (double) Utils.NextFloat(Main.rand, 0.0166666675f)) * 6.2831854820251465);
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(num1, Utils.ToRotationVector2(num2)), ModContent.ProjectileType<PillarNebulaBlaze>(), this.Projectile.damage, this.Projectile.knockBack, -1, 0.0f, 0.0f, 0.0f);
      }
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(Color.Black, this.Projectile.Opacity), -this.Projectile.rotation, vector2, this.Projectile.scale * 1.25f, (SpriteEffects) 1, 0.0f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
