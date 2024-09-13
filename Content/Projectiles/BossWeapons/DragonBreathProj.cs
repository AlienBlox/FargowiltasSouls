// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.DragonBreathProj
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
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class DragonBreathProj : ModProjectile
  {
    public int timer;
    public const float lerp = 0.18f;
    public const float halfRange = 500f;
    public const float halfRangeReduced = 50f;

    public virtual string Texture => "Terraria/Images/Projectile_687";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.penetrate = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.tileCollide = false;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (Main.myPlayer != ((Entity) player).whoAmI)
        return;
      if (player.dead || !((Entity) player).active)
        this.Projectile.Kill();
      ((Entity) this.Projectile).Center = player.MountedCenter;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      int num1 = ++this.timer;
      if (num1 > 24)
        this.timer = 0;
      if (player.channel)
      {
        ((Entity) this.Projectile).velocity = Vector2.Lerp(Vector2.Normalize(((Entity) this.Projectile).velocity), Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, player.MountedCenter)), 0.18f);
        ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
        if (this.timer == 0 || this.timer == 12)
        {
          float num2;
          int num3;
          float num4;
          int num5;
          player.PickAmmo(player.inventory[player.selectedItem], ref num1, ref num2, ref num3, ref num4, ref num5, false);
        }
        ++this.Projectile.timeLeft;
      }
      float num6 = ((Entity) this.Projectile).direction < 0 ? 3.14159274f : 0.0f;
      player.itemRotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + num6;
      player.itemRotation = MathHelper.WrapAngle(player.itemRotation);
      player.ChangeDir(((Entity) this.Projectile).direction);
      player.heldProj = ((Entity) this.Projectile).whoAmI;
      player.itemTime = 2;
      player.itemAnimation = 2;
      Vector2 vector2 = Utils.RotatedBy(new Vector2(60f, 0.0f), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2());
      Projectile projectile = this.Projectile;
      ((Entity) projectile).Center = Vector2.op_Addition(((Entity) projectile).Center, vector2);
      DelegateMethods.v3_1 = new Vector3(1.2f, 1f, 0.3f);
      float num7 = Math.Min(this.Projectile.ai[0] / 50f, 0.75f) * 2f;
      double num8 = (double) Math.Max((float) (((double) this.Projectile.ai[0] - 47.5) / 50.0), 0.0f) * 2.0;
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] > 55.0 && player.channel && player.HasAmmo(player.inventory[player.selectedItem]))
        this.Projectile.ai[0] = 45f;
      if ((double) this.Projectile.ai[0] <= 55.0 && this.timer == 0)
        SoundEngine.PlaySound(ref SoundID.DD2_BetsyFlameBreath, new Vector2?(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 600f))), (SoundUpdateCallback) null);
      if ((double) this.Projectile.ai[0] < 97.5)
        return;
      this.Projectile.Kill();
    }

    public virtual bool? Colliding(Rectangle myRect, Rectangle targetRect)
    {
      float num1 = 0.0f;
      float num2 = this.Projectile.ai[0] / 45f;
      if ((double) num2 > 1.0)
        num2 = 1f;
      float num3 = (float) (((double) this.Projectile.ai[0] - 47.5) / 50.0);
      if ((double) num3 < 0.0)
        num3 = 0.0f;
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 500f), num3));
      Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 500f), 2f), num2));
      for (int index = -1; index <= 1; ++index)
      {
        Vector2 vector2_3 = Vector2.op_Addition(vector2_1, Utils.RotatedBy(Vector2.op_Subtraction(vector2_2, vector2_1), (double) MathHelper.ToRadians((float) (5 * index)), new Vector2()));
        if (Collision.CheckAABBvLineCollision(Utils.TopLeft(targetRect), Utils.Size(targetRect), vector2_1, vector2_3, 40f * this.Projectile.scale, ref num1))
          return new bool?(true);
      }
      return new bool?(false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition);
      float num1 = 100f;
      float num2 = this.Projectile.ai[0] / 50f;
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      Color transparent = Color.Transparent;
      Color alpha1 = this.Projectile.GetAlpha(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0));
      Color alpha2 = this.Projectile.GetAlpha(new Color(180, 30, 30, 200));
      Color alpha3 = this.Projectile.GetAlpha(new Color(30, 0, 0, 30));
      ulong num3 = 1;
      for (float num4 = 0.0f; (double) num4 < 30.0; num4 += 0.66f)
      {
        Vector2 rotationVector2 = Utils.ToRotationVector2(this.Projectile.rotation + (float) ((double) Utils.RandomFloat(ref num3) * 0.25 - 0.125));
        Vector2 vector2_2 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.op_Multiply(rotationVector2, 500f), 2f));
        float num5 = num2 + num4 * 0.06666667f;
        int num6 = (int) ((double) num5 / 0.066666670143604279);
        float num7 = num5 % 1f;
        if (((double) num7 <= (double) num2 % 1.0 || (double) this.Projectile.ai[0] >= 50.0) && ((double) num7 >= (double) num2 % 1.0 || (double) this.Projectile.ai[0] < (double) num1 - 50.0))
        {
          Color color = (double) num7 >= 0.10000000149011612 ? ((double) num7 >= 0.30000001192092896 ? ((double) num7 >= 0.699999988079071 ? ((double) num7 >= 0.89999997615814209 ? ((double) num7 >= 1.0 ? Color.Transparent : Color.Lerp(alpha3, Color.Transparent, Utils.GetLerpValue(0.9f, 1f, num7, true))) : Color.Lerp(alpha2, alpha3, Utils.GetLerpValue(0.7f, 0.9f, num7, true))) : Color.Lerp(alpha1, alpha2, Utils.GetLerpValue(0.3f, 0.7f, num7, true))) : alpha1) : Color.Lerp(Color.Transparent, alpha1, Utils.GetLerpValue(0.0f, 0.1f, num7, true));
          float num8 = (float) (0.89999997615814209 + (double) num7 * 0.800000011920929);
          float num9 = num8 * num8 * 0.8f;
          Vector2 vector2_3 = Vector2.SmoothStep(vector2_1, vector2_2, num7);
          Rectangle rectangle = Utils.Frame(texture2D, 1, 7, 0, (int) ((double) num7 * 7.0), 0, 0);
          Main.EntitySpriteDraw(texture2D, vector2_3, new Rectangle?(rectangle), color, (float) ((double) this.Projectile.rotation + 6.2831854820251465 * ((double) num7 + (double) Main.GlobalTimeWrappedHourly * 1.2000000476837158) * 0.20000000298023224 + (double) num6 * 1.2566370964050293), Vector2.op_Division(Utils.Size(rectangle), 2f), num9, (SpriteEffects) 0, 0.0f);
        }
      }
      return false;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 5;
      target.AddBuff(24, 180, false);
      target.AddBuff(204, 180, false);
      target.AddBuff(203, 180, false);
      SoundEngine.PlaySound(ref SoundID.DD2_BetsyFireballImpact, new Vector2?(((Entity) target).Center), (SoundUpdateCallback) null);
      Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 2.5f);
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) target).position, ((Entity) target).width, ((Entity) target).height, Utils.Next<int>(Main.rand, new int[3]
        {
          6,
          55,
          158
        }), vector2.X, vector2.Y, 0, new Color(), 1f);
        Main.dust[index2].alpha = 200;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2.4f);
        Main.dust[index2].scale += Utils.NextFloat(Main.rand, 0.5f);
        if (Main.dust[index2].type == 55)
          Main.dust[index2].color = Color.Lerp(Color.Red, Color.Gold, Utils.NextFloat(Main.rand));
        Main.dust[index2].noLight = true;
        int index3 = Dust.NewDust(((Entity) target).position, ((Entity) target).width, ((Entity) target).height, 55, vector2.X, vector2.Y, 0, new Color(), 1f);
        Main.dust[index3].alpha = 120;
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2.4f);
        Main.dust[index3].scale += Utils.NextFloat(Main.rand, 0.2f);
        Main.dust[index3].color = Color.Lerp(Color.Purple, Color.Black, Utils.NextFloat(Main.rand));
        Main.dust[index3].noLight = true;
        int index4 = Dust.NewDust(((Entity) target).position, ((Entity) target).width, ((Entity) target).height, 55, vector2.X, vector2.Y, 0, new Color(), 1f);
        Main.dust[index4].alpha = 80;
        Dust dust3 = Main.dust[index4];
        dust3.velocity = Vector2.op_Multiply(dust3.velocity, 0.45f);
        Main.dust[index4].scale += Utils.NextFloat(Main.rand, 0.2f);
        Main.dust[index4].color = Color.Lerp(Color.Purple, Color.Black, Utils.NextFloat(Main.rand));
        Main.dust[index4].noLight = true;
      }
      for (int index5 = 0; index5 < 30; ++index5)
      {
        int index6 = Dust.NewDust(((Entity) target).position, ((Entity) target).width, ((Entity) target).height, 228, vector2.X, vector2.Y, 0, new Color(), 1f);
        Main.dust[index6].noGravity = true;
        Main.dust[index6].scale = 1.25f + Utils.NextFloat(Main.rand);
        Main.dust[index6].fadeIn = 1.5f;
        Dust dust4 = Main.dust[index6];
        dust4.velocity = Vector2.op_Multiply(dust4.velocity, 0.5f);
        Dust dust5 = Main.dust[index6];
        dust5.velocity = Vector2.op_Addition(dust5.velocity, vector2);
        Dust dust6 = Main.dust[index6];
        dust6.velocity = Vector2.op_Multiply(dust6.velocity, Utils.NextFloat(Main.rand, 6f));
        Main.dust[index6].noLight = true;
      }
    }
  }
}
