// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.NekomiRitual
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class NekomiRitual : ModProjectile
  {
    private const float threshold = 75f;
    private int oldHeartCount;

    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/FakeHeart";

    public virtual void SetStaticDefaults() => ((ModType) this).SetStaticDefaults();

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 8;
      ((Entity) this.Projectile).height = 8;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.friendly = true;
      this.Projectile.netImportant = true;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (((Entity) player).active && !player.dead && !player.ghost && player.FargoSouls().NekomiSet)
      {
        this.Projectile.alpha = 0;
        ((Entity) this.Projectile).Center = ((Entity) player).Center;
        this.Projectile.timeLeft = 2;
        this.Projectile.scale = (float) ((1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue) * 1.5 + ((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.5);
        this.Projectile.scale /= 2f;
        if ((double) this.Projectile.scale < 0.10000000149011612)
          this.Projectile.scale = 0.1f;
        this.Projectile.localAI[0] = (float) (int) ((double) fargoSoulsPlayer.NekomiMeter / 3600.0 * 9.0);
        if ((double) this.oldHeartCount != (double) this.Projectile.localAI[0])
        {
          int num1 = (double) this.Projectile.localAI[0] > (double) this.oldHeartCount ? (int) this.Projectile.localAI[0] : this.oldHeartCount;
          for (int index = 0; index < num1; ++index)
          {
            float num2 = 6.28318548f / (float) num1 * (float) index + this.Projectile.rotation;
            FargoSoulsUtil.HeartDust(Vector2.op_Addition(((Entity) this.Projectile).Center, Utils.RotatedBy(new Vector2(0.0f, -75f * this.Projectile.scale), (double) num2, new Vector2())), num2 + 1.57079637f, new Vector2(), 0.5f, 0.75f);
          }
          this.oldHeartCount = (int) this.Projectile.localAI[0];
          if ((double) this.Projectile.localAI[0] >= 9.0 && !Main.dedServ)
          {
            SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/FullMeter", (SoundType) 0);
            SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          }
        }
        if ((double) this.Projectile.localAI[0] >= 9.0)
        {
          this.Projectile.rotation += (float) Math.PI / 57f;
          if ((double) this.Projectile.rotation <= 6.2831854820251465)
            return;
          this.Projectile.rotation -= 6.28318548f;
        }
        else
          this.Projectile.rotation = 0.0f;
      }
      else
        this.Projectile.Kill();
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index = 0; (double) index < (double) this.Projectile.localAI[0]; ++index)
      {
        float num3 = 6.28318548f / this.Projectile.localAI[0] * (float) index + this.Projectile.rotation;
        Vector2 vector2_2 = Utils.RotatedBy(new Vector2(0.0f, -75f * this.Projectile.scale), (double) num3, new Vector2());
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, num3, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), 0.9f));
    }
  }
}
