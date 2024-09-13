// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.FlamingJack
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
  public class FlamingJack : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_321";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 3;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 600;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if (++this.Projectile.frameCounter > 0)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame > 2)
          this.Projectile.frame = 0;
      }
      if ((double) --this.Projectile.ai[1] > -60.0 && (double) this.Projectile.ai[1] < 0.0)
      {
        Player player = FargoSoulsUtil.PlayerExists(this.Projectile.ai[0]);
        if (player != null)
        {
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.Projectile).Center);
          ((Vector2) ref vector2).Normalize();
          vector2 = Vector2.op_Multiply(vector2, 8f);
          ((Entity) this.Projectile).velocity.X = (float) (((double) ((Entity) this.Projectile).velocity.X * 40.0 + (double) vector2.X) / 41.0);
          ((Entity) this.Projectile).velocity.Y = (float) (((double) ((Entity) this.Projectile).velocity.Y * 40.0 + (double) vector2.Y) / 41.0);
        }
        else
        {
          this.Projectile.ai[0] = -1f;
          this.Projectile.netUpdate = true;
        }
      }
      if ((double) ((Entity) this.Projectile).velocity.X < 0.0)
      {
        this.Projectile.spriteDirection = -1;
        this.Projectile.rotation = -Utils.ToRotation(((Entity) this.Projectile).velocity);
      }
      else
      {
        this.Projectile.spriteDirection = 1;
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      }
      int index = Dust.NewDust(Vector2.op_Addition(((Entity) this.Projectile).position, new Vector2(8f, 8f)), ((Entity) this.Projectile).width - 16, ((Entity) this.Projectile).height - 16, 6, 0.0f, 0.0f, 0, new Color(), 1f);
      Dust dust1 = Main.dust[index];
      dust1.velocity = Vector2.op_Multiply(dust1.velocity, 0.5f);
      Dust dust2 = Main.dust[index];
      dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.5f));
      Main.dust[index].noGravity = true;
      Main.dust[index].noLight = true;
      Main.dust[index].scale = 1.4f;
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item10, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(24, 600, true, false);
      target.AddBuff(ModContent.BuffType<LivingWastelandBuff>(), 600, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(new Color(200, 200, 200, 0));

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
