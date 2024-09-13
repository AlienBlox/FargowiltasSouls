// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiNuke
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HentaiNuke : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_645";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 16;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 470;
      ((Entity) this.Projectile).height = 624;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.tileCollide = false;
      this.Projectile.penetrate = -1;
      this.Projectile.scale = 1.5f;
      this.Projectile.alpha = 0;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.hide = true;
    }

    public virtual bool? CanDamage() => new bool?((double) this.Projectile.localAI[0] != 0.0);

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      projHitbox.X += projHitbox.Width / 2;
      projHitbox.Y += projHitbox.Height / 2;
      projHitbox.Width = (int) (420.0 * (double) this.Projectile.scale);
      projHitbox.Height = (int) (420.0 * (double) this.Projectile.scale);
      projHitbox.X -= projHitbox.Width / 2;
      projHitbox.Y -= projHitbox.Height / 2;
      return new bool?();
    }

    public virtual void AI()
    {
      if (Utils.HasNaNs(((Entity) this.Projectile).position))
      {
        this.Projectile.Kill();
      }
      else
      {
        if (++this.Projectile.frameCounter >= 3)
        {
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          {
            --this.Projectile.frame;
            this.Projectile.Kill();
          }
        }
        if ((double) this.Projectile.localAI[0] != 0.0)
          return;
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item88, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        this.Projectile.hide = false;
        ((Entity) this.Projectile).position.Y -= 144f * this.Projectile.scale;
        if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
          ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
        if (!Main.dedServ)
        {
          SoundStyle soundStyle;
          // ISSUE: explicit constructor call
          ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/Thunder", (SoundType) 0);
          ((SoundStyle) ref soundStyle).Volume = 0.8f;
          ((SoundStyle) ref soundStyle).Pitch = 0.5f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        for (int index1 = 0; index1 < 4; ++index1)
        {
          for (int index2 = 0; index2 < 3; ++index2)
          {
            int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
            Main.dust[index3].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
          }
          for (int index4 = 0; index4 < 10; ++index4)
          {
            int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
            Main.dust[index5].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
            Main.dust[index5].noGravity = true;
            Dust dust1 = Main.dust[index5];
            dust1.velocity = Vector2.op_Multiply(dust1.velocity, 1f);
            int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 1.5f);
            Main.dust[index6].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
            Dust dust2 = Main.dust[index6];
            dust2.velocity = Vector2.op_Multiply(dust2.velocity, 1f);
            Main.dust[index6].noGravity = true;
          }
          for (int index7 = 0; index7 < 10; ++index7)
          {
            int index8 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 3f);
            Dust dust = Main.dust[index8];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
          }
          for (int index9 = 0; index9 < 10; ++index9)
          {
            int index10 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
            Main.dust[index10].noGravity = true;
            Dust dust3 = Main.dust[index10];
            dust3.velocity = Vector2.op_Multiply(dust3.velocity, 7f);
            int index11 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
            Dust dust4 = Main.dust[index11];
            dust4.velocity = Vector2.op_Multiply(dust4.velocity, 3f);
          }
          for (int index12 = 0; index12 < 10; ++index12)
          {
            int index13 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 2f);
            Main.dust[index13].noGravity = true;
            Dust dust5 = Main.dust[index13];
            dust5.velocity = Vector2.op_Multiply(dust5.velocity, 21f * this.Projectile.scale);
            Main.dust[index13].noLight = true;
            int index14 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 1f);
            Dust dust6 = Main.dust[index14];
            dust6.velocity = Vector2.op_Multiply(dust6.velocity, 12f);
            Main.dust[index14].noGravity = true;
            Main.dust[index14].noLight = true;
          }
          for (int index15 = 0; index15 < 10; ++index15)
          {
            int index16 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), Utils.NextFloat(Main.rand, 2f, 3.5f));
            if (Utils.NextBool(Main.rand, 3))
              Main.dust[index16].noGravity = true;
            Dust dust = Main.dust[index16];
            dust.velocity = Vector2.op_Multiply(dust.velocity, Utils.NextFloat(Main.rand, 9f, 12f));
            Main.dust[index16].position = ((Entity) Main.player[this.Projectile.owner]).Center;
          }
        }
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, false);
      target.immune[this.Projectile.owner] = 1;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 200));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/HentaiNuke/HentaiNuke_" + this.Projectile.frame.ToString(), (AssetRequestMode) 1).Value;
      Rectangle bounds = texture2D.Bounds;
      Vector2 vector2 = Vector2.op_Division(Utils.Size(bounds), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(bounds), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
