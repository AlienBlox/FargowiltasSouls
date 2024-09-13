// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.FrostFireball
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
  public class FrostFireball : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_253";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 360;
      Mod mod;
      if (!Terraria.ModLoader.ModLoader.TryGetMod("Fargowiltas", ref mod))
        return;
      mod.Call(new object[2]
      {
        (object) "LowRenderProj",
        (object) this.Projectile
      });
    }

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual void AI()
    {
      if (!Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
      {
        int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 135, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
        Main.dust[index].noGravity = true;
        Main.dust[index].velocity.X *= 0.3f;
        Main.dust[index].velocity.Y *= 0.3f;
      }
      if ((double) this.Projectile.ai[0] >= 0.0 && (double) this.Projectile.ai[0] < (double) Main.maxNPCs)
      {
        int index = (int) this.Projectile.ai[0];
        if (Main.npc[index].CanBeChasedBy((object) index, false))
        {
          Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.npc[index]).Center, ((Entity) this.Projectile).Center);
          ((Vector2) ref vector2_1).Normalize();
          Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 8f);
          ((Entity) this.Projectile).velocity.X = (float) (((double) ((Entity) this.Projectile).velocity.X * 14.0 + (double) vector2_2.X) / 15.0);
          ((Entity) this.Projectile).velocity.Y = (float) (((double) ((Entity) this.Projectile).velocity.Y * 14.0 + (double) vector2_2.Y) / 15.0);
        }
        else
        {
          this.Projectile.ai[0] = -1f;
          this.Projectile.netUpdate = true;
        }
      }
      else if ((double) --this.Projectile.localAI[0] < 0.0)
      {
        this.Projectile.localAI[0] = 10f;
        float num1 = 1000f;
        int num2 = -1;
        for (int index = 0; index < 200; ++index)
        {
          NPC npc = Main.npc[index];
          if (npc.CanBeChasedBy((object) this.Projectile, false))
          {
            float num3 = ((Entity) this.Projectile).Distance(((Entity) npc).Center);
            if ((double) num3 < (double) num1)
            {
              num1 = num3;
              num2 = index;
            }
          }
        }
        this.Projectile.ai[0] = (float) num2;
        this.Projectile.netUpdate = true;
      }
      this.Projectile.spriteDirection = ((Entity) this.Projectile).direction = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
      this.Projectile.rotation += 0.3f * (float) ((Entity) this.Projectile).direction;
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item10, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 10; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 135, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 135, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(44, 240, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color(200, 200, 200, 25), this.Projectile.Opacity));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
