// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Deathrays.GolemBeam
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.Buffs.Masomode;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Deathrays
{
  public class GolemBeam : BaseDeathray
  {
    private const int descendTime = 30;

    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/GolemBeam";

    public GolemBeam()
      : base(300f)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void AI()
    {
      this.Projectile.alpha = 0;
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 249);
      if (npc != null && npc.GetGlobalNPC<GolemHead>().DoAttack)
      {
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
        if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
          ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
        if ((double) this.Projectile.localAI[0] == 0.0 && !Main.dedServ)
        {
          SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/GolemBeam", (SoundType) 0);
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        float num1 = 1.3f;
        ++this.Projectile.localAI[0];
        if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
        {
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.scale = num1;
          float rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          this.Projectile.rotation = rotation - 1.57079637f;
          ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(rotation);
          float length = 3f;
          float width = (float) ((Entity) this.Projectile).width;
          Vector2 center = ((Entity) this.Projectile).Center;
          if (nullable.HasValue)
            center = nullable.Value;
          float[] numArray = new float[(int) length];
          Collision.LaserScan(center, ((Entity) this.Projectile).velocity, width * this.Projectile.scale, 2400f, numArray);
          float num2 = 0.0f;
          for (int index = 0; index < numArray.Length; ++index)
            num2 += numArray[index];
          float num3 = num2 / length;
          Vector2 vector2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1]));
          if ((double) this.Projectile.localAI[0] <= 30.0)
          {
            this.Projectile.localAI[1] = MathHelper.Lerp(0.0f, Math.Max(num3, 320f), this.Projectile.localAI[0] / 30f);
            if (++this.Projectile.frameCounter > 3)
            {
              this.Projectile.frameCounter = 0;
              if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
                this.Projectile.frame = 0;
            }
            if ((double) this.Projectile.localAI[0] == 30.0 && !Main.dedServ && ((Entity) Main.LocalPlayer).active)
            {
              ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
              for (int index1 = -1; index1 <= 1; index1 += 2)
              {
                for (int index2 = 0; index2 < 50; ++index2)
                {
                  int index3 = Dust.NewDust(vector2, 0, 0, 31, (float) index1 * 3f, 0.0f, 50, new Color(), 3f);
                  Main.dust[index3].noGravity = Utils.NextBool(Main.rand);
                  Dust dust = Main.dust[index3];
                  dust.velocity = Vector2.op_Multiply(dust.velocity, Utils.NextFloat(Main.rand, 3f));
                }
                for (int index4 = 0; index4 < 15; ++index4)
                {
                  int index5 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2, new Vector2(), Main.rand.Next(61, 64), 1f);
                  Main.gore[index5].velocity.X += (float) index4 / 3f * (float) index1;
                  Main.gore[index5].velocity.Y += Utils.NextFloat(Main.rand, 2f);
                }
              }
            }
          }
          if (!Collision.SolidTiles(Vector2.op_Addition(vector2, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 16f)), 0, 0))
            return;
          for (int index6 = 0; index6 < 2; ++index6)
          {
            int index7 = Dust.NewDust(Vector2.op_Subtraction(vector2, Vector2.op_Division(new Vector2((float) ((Entity) this.Projectile).width, (float) ((Entity) this.Projectile).height), 2f)), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 50, new Color(), 4f);
            Main.dust[index7].noGravity = true;
            Dust dust1 = Main.dust[index7];
            dust1.velocity = Vector2.op_Subtraction(dust1.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 3f));
            Dust dust2 = Main.dust[index7];
            dust2.velocity = Vector2.op_Multiply(dust2.velocity, Utils.NextFloat(Main.rand, 3f));
          }
          if (!Utils.NextBool(Main.rand, 3))
            return;
          int index8 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2, new Vector2(), Main.rand.Next(61, 64), 0.5f);
          Gore gore1 = Main.gore[index8];
          gore1.velocity = Vector2.op_Subtraction(gore1.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 3f));
          Gore gore2 = Main.gore[index8];
          gore2.velocity = Vector2.op_Multiply(gore2.velocity, Utils.NextFloat(Main.rand, 2f));
        }
      }
      else
        this.Projectile.Kill();
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<StunnedBuff>(), 120, true, false);
      target.AddBuff(36, 600, true, false);
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
      target.AddBuff(195, 600, true, false);
    }

    private Rectangle Frame(Texture2D tex)
    {
      int num = tex.Height / Main.projFrames[this.Projectile.type];
      return new Rectangle(0, num * this.Projectile.frame, tex.Width, num);
    }

    public override bool PreDraw(ref Color lightColor)
    {
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        return false;
      SpriteEffects spriteEffects1 = (SpriteEffects) 0;
      Texture2D texture2D1 = ModContent.Request<Texture2D>(base.Texture, (AssetRequestMode) 1).Value;
      Texture2D texture2D2 = ModContent.Request<Texture2D>(base.Texture + "2", (AssetRequestMode) 1).Value;
      Texture2D texture2D3 = ModContent.Request<Texture2D>(base.Texture + "3", (AssetRequestMode) 1).Value;
      float num1 = this.Projectile.localAI[1];
      Texture2D texture2D4 = texture2D1;
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition);
      Rectangle bounds1 = texture2D1.Bounds;
      Vector2 vector2_2 = vector2_1;
      Rectangle? nullable1 = new Rectangle?(bounds1);
      Color alpha1 = this.Projectile.GetAlpha(lightColor);
      double rotation1 = (double) this.Projectile.rotation;
      Vector2 vector2_3 = Vector2.op_Division(Utils.Size(bounds1), 2f);
      double scale1 = (double) this.Projectile.scale;
      Main.EntitySpriteDraw(texture2D4, vector2_2, nullable1, alpha1, (float) rotation1, vector2_3, (float) scale1, (SpriteEffects) 0, 0.0f);
      float num2 = num1 - (float) (texture2D1.Height / 2 + texture2D3.Height) * this.Projectile.scale;
      Vector2 vector2_4 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.scale), (float) texture2D1.Height), 2f));
      if ((double) num2 > 0.0)
      {
        float num3 = 0.0f;
        Rectangle bounds2 = texture2D2.Bounds;
        while ((double) num3 < (double) num2)
        {
          Main.EntitySpriteDraw(texture2D2, Vector2.op_Subtraction(vector2_4, Main.screenPosition), new Rectangle?(bounds2), this.Projectile.GetAlpha(Lighting.GetColor((int) vector2_4.X / 16, (int) vector2_4.Y / 16)), this.Projectile.rotation, Vector2.op_Division(Utils.Size(bounds2), 2f), this.Projectile.scale, spriteEffects1, 0.0f);
          num3 += (float) bounds2.Height * this.Projectile.scale;
          vector2_4 = Vector2.op_Addition(vector2_4, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, (float) bounds2.Height), this.Projectile.scale));
        }
      }
      Texture2D texture2D5 = texture2D3;
      Vector2 vector2_5 = Vector2.op_Subtraction(vector2_4, Main.screenPosition);
      Rectangle bounds3 = texture2D3.Bounds;
      Vector2 vector2_6 = vector2_5;
      Rectangle? nullable2 = new Rectangle?(bounds3);
      Color alpha2 = this.Projectile.GetAlpha(Lighting.GetColor((int) vector2_4.X / 16, (int) vector2_4.Y / 16));
      double rotation2 = (double) this.Projectile.rotation;
      Vector2 vector2_7 = Vector2.op_Division(Utils.Size(bounds3), 2f);
      double scale2 = (double) this.Projectile.scale;
      SpriteEffects spriteEffects2 = spriteEffects1;
      Main.EntitySpriteDraw(texture2D5, vector2_6, nullable2, alpha2, (float) rotation2, vector2_7, (float) scale2, spriteEffects2, 0.0f);
      return false;
    }
  }
}
