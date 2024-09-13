// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.BetsyElectrosphere
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class BetsyElectrosphere : ModProjectile
  {
    public int p = -1;
    public int timer;
    public float rotation;
    public Vector2 spawn;

    public virtual string Texture => "Terraria/Images/Projectile_443";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.projFrames[443];
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 80;
      ((Entity) this.Projectile).height = 80;
      this.Projectile.aiStyle = 77;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 240;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool PreAI()
    {
      if (this.Projectile.timeLeft == 240)
      {
        this.spawn.X = this.Projectile.ai[0];
        this.spawn.Y = this.Projectile.ai[1];
        this.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
        this.timer = Main.rand.Next(60);
      }
      return true;
    }

    public virtual void AI()
    {
      ((Entity) this.Projectile).velocity = Vector2.Zero;
      if (this.p == -1)
      {
        this.p = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
      }
      else
      {
        if ((double) ((Entity) this.Projectile).Distance(((Entity) Main.player[this.p]).Center) < 600.0)
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(2f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[this.p]).Center));
        if (++this.timer <= 60)
          return;
        this.timer = 0;
        if ((double) ((Entity) Main.player[this.p]).Distance(this.spawn) <= (double) ((Entity) this.Projectile).Distance(this.spawn) || !FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Utils.RotatedByRandom(Utils.ToRotationVector2(this.rotation), (double) MathHelper.ToRadians(30f)), ModContent.ProjectileType<BetsyFury2>(), this.Projectile.damage, 0.0f, Main.myPlayer, (float) this.p, 0.0f, 0.0f);
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(195, Main.rand.Next(60, 300), true, false);
      target.AddBuff(196, Main.rand.Next(60, 300), true, false);
      target.AddBuff(144, 300, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 128), this.Projectile.Opacity));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
