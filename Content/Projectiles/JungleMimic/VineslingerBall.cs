// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.JungleMimic.VineslingerBall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.JungleMimic
{
  public class VineslingerBall : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 34;
      ((Entity) this.Projectile).height = 34;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.aiStyle = 15;
    }

    public virtual void PostAI()
    {
      if ((double) this.Projectile.ai[0] != 6.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.25f));
      }
      base.PostAI();
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/JungleMimic/VineslingerChain", (AssetRequestMode) 1).Value;
      Vector2 vector2_1 = ((Entity) this.Projectile).Center;
      Vector2 vector2_2 = Vector2.op_Addition(Main.player[this.Projectile.owner].MountedCenter, Vector2.op_Multiply(Vector2.UnitY, 6f));
      Rectangle? nullable = new Rectangle?();
      Vector2 vector2_3;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_3).\u002Ector((float) texture2D1.Width * 0.5f, (float) texture2D1.Height * 0.5f);
      float height = (float) texture2D1.Height;
      Vector2 vector2_4 = Vector2.op_Subtraction(vector2_2, vector2_1);
      float num1 = (float) Math.Atan2((double) vector2_4.Y, (double) vector2_4.X) - 1.57f;
      bool flag = true;
      if (float.IsNaN(vector2_1.X) && float.IsNaN(vector2_1.Y))
        flag = false;
      if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
        flag = false;
      while (flag)
      {
        if ((double) ((Vector2) ref vector2_4).Length() < (double) height + 1.0)
        {
          flag = false;
        }
        else
        {
          Vector2 vector2_5 = vector2_4;
          ((Vector2) ref vector2_5).Normalize();
          vector2_1 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(vector2_5, height));
          vector2_4 = Vector2.op_Subtraction(vector2_2, vector2_1);
          Color alpha = this.Projectile.GetAlpha(Lighting.GetColor((int) vector2_1.X / 16, (int) ((double) vector2_1.Y / 16.0)));
          Main.EntitySpriteDraw(texture2D1, Vector2.op_Subtraction(vector2_1, Main.screenPosition), nullable, alpha, num1, vector2_3, 1f, (SpriteEffects) 0, 0.0f);
        }
      }
      Texture2D texture2D2 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num2 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num3 = num2 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num3, texture2D2.Width, num2);
      Vector2 vector2_6 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      this.Projectile.GetAlpha(lightColor);
      Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_6, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (this.Projectile.owner != Main.myPlayer)
        return;
      for (int index = 0; index < 2; ++index)
      {
        float num1 = -((Entity) this.Projectile).velocity.X * Utils.NextFloat(Main.rand, 0.2f, 0.3f) + Utils.NextFloat(Main.rand, -4f, 4f);
        float num2 = -((Entity) this.Projectile).velocity.Y * Utils.NextFloat(Main.rand, 0.2f, 0.3f) + Utils.NextFloat(Main.rand, -4f, 4f);
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).position.X + num1, ((Entity) this.Projectile).position.Y + num2, num1, num2, ModContent.ProjectileType<VineslingerProjectileFriendly>(), (int) ((double) this.Projectile.damage * 0.5), 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
        SoundEngine.PlaySound(ref SoundID.Grass, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      }
    }
  }
}
