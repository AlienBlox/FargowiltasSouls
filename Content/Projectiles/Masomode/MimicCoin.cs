// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MimicCoin
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
  public class MimicCoin : ModProjectile
  {
    public int coinType = -1;

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 4;
      ((Entity) this.Projectile).height = 4;
      this.Projectile.aiStyle = 1;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 600;
      this.Projectile.extraUpdates = 1;
      this.Projectile.alpha = (int) byte.MaxValue;
    }

    public virtual bool PreAI()
    {
      if (this.coinType == -1)
      {
        this.coinType = (int) this.Projectile.ai[0];
        switch (this.coinType)
        {
          case 0:
            this.AIType = 158;
            this.Projectile.frame = 0;
            break;
          case 1:
            this.AIType = 159;
            this.Projectile.frame = 1;
            break;
          case 2:
            this.AIType = 160;
            this.Projectile.frame = 2;
            break;
          default:
            this.AIType = 161;
            this.Projectile.frame = 3;
            break;
        }
      }
      return true;
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Dig, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      int num1;
      switch (this.coinType)
      {
        case 0:
          num1 = 9;
          break;
        case 1:
          num1 = 11;
          break;
        case 2:
          num1 = 19;
          break;
        default:
          num1 = 11;
          break;
      }
      int num2 = num1;
      for (int index1 = 0; index1 < 10; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num2, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Subtraction(dust.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.5f));
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<MidasBuff>(), 300, true, false);
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
