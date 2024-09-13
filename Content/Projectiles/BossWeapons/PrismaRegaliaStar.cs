// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.PrismaRegaliaStar
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class PrismaRegaliaStar : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_79";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 240;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = false;
      this.Projectile.DamageType = DamageClass.MeleeNoSpeed;
      this.Projectile.penetrate = 1;
      this.Projectile.extraUpdates = 1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.ai[1] > 20.0)
      {
        this.Projectile.friendly = true;
        this.Projectile.tileCollide = true;
        if ((double) this.Projectile.ai[0] == -1.0)
        {
          if ((double) this.Projectile.ai[1] % 6.0 == 0.0)
          {
            this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 2000f, true);
            this.Projectile.netUpdate = true;
            if ((double) this.Projectile.ai[0] == -1.0)
            {
              this.Projectile.Kill();
              return;
            }
          }
        }
        else
        {
          NPC npc = Main.npc[(int) this.Projectile.ai[0]];
          if (((Entity) npc).active && npc.CanBeChasedBy((object) null, false))
          {
            double num1 = (double) Utils.ToRotation(Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center)) - (double) Utils.ToRotation(((Entity) this.Projectile).velocity);
            if (num1 > Math.PI)
              num1 -= 2.0 * Math.PI;
            if (num1 < -1.0 * Math.PI)
              num1 += 2.0 * Math.PI;
            float num2 = Math.Min(((Vector2) ref ((Entity) this.Projectile).velocity).Length() / 100f, 1f);
            ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, num1 * (double) num2, new Vector2());
          }
          else
          {
            this.Projectile.ai[0] = -1f;
            this.Projectile.netUpdate = true;
          }
        }
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), MathHelper.Lerp(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), 24f, 0.02f));
      }
      ++this.Projectile.ai[1];
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
      float num1 = Utils.NextFloat(Main.rand, 0.9f, 1.1f);
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector((float) (texture2D.Width / 2) + num1, (float) (texture2D.Height / 2) + num1);
      float num2 = 6f;
      Vector2 vector2_2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_2).\u002Ector(Utils.NextFloat(Main.rand, -num2, num2), Utils.NextFloat(Main.rand, -num2, num2));
      Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.White, 0.0f, vector2_1, num1, (SpriteEffects) 0, 0.0f);
      float num3 = 3f;
      Vector2 vector2_3;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_3).\u002Ector(Utils.NextFloat(Main.rand, -num3, num3), Utils.NextFloat(Main.rand, -num3, num3));
      Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_3), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.Pink, 0.0f, vector2_1, num1, (SpriteEffects) 0, 0.0f);
      DrawData drawData;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData).\u002Ector(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.Pink, 0.0f, vector2_1, num1, (SpriteEffects) 0, 0.0f);
      GameShaders.Misc["LCWingShader"].UseColor(Color.Pink).UseSecondaryColor(Color.LightPink);
      GameShaders.Misc["LCWingShader"].Apply(new DrawData?());
      ((DrawData) ref drawData).Draw(Main.spriteBatch);
      return false;
    }
  }
}
