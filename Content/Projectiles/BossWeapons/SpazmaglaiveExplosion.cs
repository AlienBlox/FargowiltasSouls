// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.SpazmaglaiveExplosion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class SpazmaglaiveExplosion : ModProjectile
  {
    public int timer;
    public float lerp = 0.12f;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.alpha = 0;
      this.Projectile.penetrate = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.tileCollide = false;
      this.Projectile.FargoSouls().CanSplit = false;
    }

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void AI()
    {
      NPC npc = Main.npc[(int) this.Projectile.ai[1]];
      if (!((Entity) npc).active)
        this.Projectile.Kill();
      ((Entity) this.Projectile).Center = ((Entity) npc).Center;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      DelegateMethods.v3_1 = new Vector3(1.2f, 1f, 0.3f);
      double num1 = (double) this.Projectile.localAI[0] / 40.0;
      double num2 = ((double) this.Projectile.localAI[0] - 38.0) / 40.0;
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] < (double) this.Projectile.ai[0])
        return;
      this.Projectile.Kill();
    }

    public virtual bool? Colliding(Rectangle myRect, Rectangle targetRect)
    {
      float num1 = 0.0f;
      float num2 = this.Projectile.localAI[0] / 25f;
      if ((double) num2 > 1.0)
        num2 = 1f;
      float num3 = (float) (((double) this.Projectile.localAI[0] - 38.0) / 40.0);
      if ((double) num3 < 0.0)
        num3 = 0.0f;
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 100f), num3));
      Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 200f), num2));
      return Collision.CheckAABBvLineCollision(Utils.TopLeft(targetRect), Utils.Size(targetRect), vector2_1, vector2_2, 40f * this.Projectile.scale, ref num1) ? new bool?(true) : new bool?(false);
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(new Color(13, 219, 83));

    public virtual bool PreDraw(ref Color lightColor)
    {
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition);
      float num1 = 40f;
      float num2 = num1 * 2f;
      float num3 = this.Projectile.localAI[0] / num1;
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      Color white = Color.White;
      Color color1;
      // ISSUE: explicit constructor call
      ((Color) ref color1).\u002Ector((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0);
      Color color2;
      // ISSUE: explicit constructor call
      ((Color) ref color2).\u002Ector(30, 180, 30, 0);
      Color color3;
      // ISSUE: explicit constructor call
      ((Color) ref color3).\u002Ector(0, 30, 0, 0);
      ulong num4 = 1;
      for (float num5 = 0.0f; (double) num5 < 15.0; num5 += 0.66f)
      {
        Vector2 rotationVector2 = Utils.ToRotationVector2(this.Projectile.rotation + (float) ((double) Utils.RandomFloat(ref num4) * 0.25 - 0.125));
        Vector2 vector2_2 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(rotationVector2, 200f));
        float num6 = num3 + num5 * 0.06666667f;
        int num7 = (int) ((double) num6 / 0.066666670143604279);
        float num8 = num6 % 1f;
        if (((double) num8 <= (double) num3 % 1.0 || (double) this.Projectile.localAI[0] >= (double) num1) && ((double) num8 >= (double) num3 % 1.0 || (double) this.Projectile.localAI[0] < (double) num2 - (double) num1))
        {
          Color color4 = (double) num8 >= 0.10000000149011612 ? ((double) num8 >= 0.34999999403953552 ? ((double) num8 >= 0.699999988079071 ? ((double) num8 >= 0.89999997615814209 ? ((double) num8 >= 1.0 ? Color.Transparent : Color.Lerp(color3, Color.Transparent, Utils.GetLerpValue(0.9f, 1f, num8, true))) : Color.Lerp(color2, color3, Utils.GetLerpValue(0.7f, 0.9f, num8, true))) : Color.Lerp(color1, color2, Utils.GetLerpValue(0.35f, 0.7f, num8, true))) : color1) : Color.Lerp(Color.Transparent, color1, Utils.GetLerpValue(0.0f, 0.1f, num8, true));
          ((Color) ref color4).A = (byte) ((double) byte.MaxValue - (double) num8 * (double) byte.MaxValue);
          float num9 = (float) (0.89999997615814209 + (double) num8 * 0.800000011920929);
          float num10 = num9 * num9 * 0.8f;
          Vector2 vector2_3 = Vector2.SmoothStep(vector2_1, vector2_2, num8);
          Rectangle rectangle = Utils.Frame(texture2D, 1, 7, 0, (int) ((double) num8 * 7.0), 0, 0);
          Main.EntitySpriteDraw(texture2D, vector2_3, new Rectangle?(rectangle), color4, (float) ((double) this.Projectile.rotation + 6.2831854820251465 * ((double) num8 + (double) Main.GlobalTimeWrappedHourly * 1.2000000476837158) * 0.20000000298023224 + (double) num7 * 1.2566370964050293), Vector2.op_Division(Utils.Size(rectangle), 2f), num10 / 2f, (SpriteEffects) 0, 0.0f);
        }
      }
      return false;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(39, 180, false);
    }
  }
}
