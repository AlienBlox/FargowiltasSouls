// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.FusedExplosion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class FusedExplosion : MoonLordSunBlast
  {
    public override string Texture => "Terraria/Images/Projectile_687";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.scale = 5f;
      this.Projectile.friendly = true;
      this.CooldownSlot = -1;
    }

    public override void AI()
    {
      if (Utils.HasNaNs(((Entity) this.Projectile).position))
      {
        this.Projectile.Kill();
      }
      else
      {
        if (++this.Projectile.frameCounter >= 2)
        {
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          {
            --this.Projectile.frame;
            this.Projectile.Kill();
          }
        }
        if ((double) this.Projectile.localAI[1] == 0.0)
          SoundEngine.PlaySound(ref SoundID.Item88, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        if ((double) ++this.Projectile.localAI[1] != 6.0)
          return;
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 45; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
        }
        for (int index3 = 0; index3 < 30; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
          Main.dust[index4].noGravity = true;
          Dust dust1 = Main.dust[index4];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
          int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust2 = Main.dust[index5];
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
        }
        for (int index6 = 0; index6 < 3; ++index6)
        {
          float num = 0.4f;
          if (index6 == 1)
            num = 0.8f;
          int index7 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
          Gore gore1 = Main.gore[index7];
          gore1.velocity = Vector2.op_Multiply(gore1.velocity, num);
          ++Main.gore[index7].velocity.X;
          ++Main.gore[index7].velocity.Y;
          int index8 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
          Gore gore2 = Main.gore[index8];
          gore2.velocity = Vector2.op_Multiply(gore2.velocity, num);
          --Main.gore[index8].velocity.X;
          ++Main.gore[index8].velocity.Y;
          int index9 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
          Gore gore3 = Main.gore[index9];
          gore3.velocity = Vector2.op_Multiply(gore3.velocity, num);
          ++Main.gore[index9].velocity.X;
          --Main.gore[index9].velocity.Y;
          int index10 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
          Gore gore4 = Main.gore[index10];
          gore4.velocity = Vector2.op_Multiply(gore4.velocity, num);
          --Main.gore[index10].velocity.X;
          --Main.gore[index10].velocity.Y;
        }
      }
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
    }

    public override bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color white = Color.White;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), white, this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
