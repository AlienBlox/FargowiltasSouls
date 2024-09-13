// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.BetsyFury
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

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
  public class BetsyFury : ModProjectile
  {
    private Vector2 spawn;

    public virtual string Texture => "Terraria/Images/Projectile_709";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.projFrames[709];
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 300;
      this.Projectile.hostile = true;
      this.Projectile.scale = 1.2f;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        SoundEngine.PlaySound(ref SoundID.DD2_SkyDragonsFuryShot, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        this.spawn = ((Entity) this.Projectile).Center;
      }
      if ((double) ((Entity) this.Projectile).Distance(this.spawn) > 1200.0)
        this.Projectile.Kill();
      if ((double) ++this.Projectile.localAI[0] < 120.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.03f);
      }
      this.Projectile.alpha -= 30;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      if (++this.Projectile.frameCounter >= 3)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= 3)
          this.Projectile.frame = 0;
      }
      Lighting.AddLight((int) ((Entity) this.Projectile).Center.X / 16, (int) ((Entity) this.Projectile).Center.Y / 16, 0.4f, 0.85f, 0.9f);
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(195, 300, true, false);
      target.AddBuff(196, 300, true, false);
      target.AddBuff(144, 300, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      int num1 = 3;
      int num2 = 10;
      for (int index1 = 0; index1 < num1; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index2].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
      }
      for (int index3 = 0; index3 < num2; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 226, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index4].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
        Main.dust[index4].noGravity = true;
      }
      if (FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, ModContent.ProjectileType<BetsyElectrosphere>(), this.Projectile.damage, 0.0f, Main.myPlayer, this.spawn.X, this.spawn.Y, 0.0f);
      SoundEngine.PlaySound(ref SoundID.DD2_SkyDragonsFuryCircle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
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
      this.Projectile.GetAlpha(lightColor);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
