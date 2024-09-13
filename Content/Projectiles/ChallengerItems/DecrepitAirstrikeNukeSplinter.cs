// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.ChallengerItems.DecrepitAirstrikeNukeSplinter
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.ChallengerItems
{
  public class DecrepitAirstrikeNukeSplinter : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Content/Bosses/BanishedBaron/BaronScrap";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 18;
      ((Entity) this.Projectile).height = 18;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.ai[2] == 0.0)
        this.Projectile.ai[2] = Utils.NextBool(Main.rand) ? 1f : -1f;
      this.Projectile.rotation += (float) ((double) this.Projectile.ai[2] * 6.2831854820251465 / 90.0);
      if ((double) this.Projectile.ai[0] == 0.0)
        this.Projectile.ai[1] = -1f;
      if ((double) this.Projectile.ai[0] > 10.0)
      {
        if ((double) this.Projectile.ai[0] % 30.0 == 11.0)
          this.Projectile.ai[1] = (float) FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 600f, true);
        if ((double) this.Projectile.ai[1] != -1.0 && ((Entity) Main.npc[(int) this.Projectile.ai[1]]).active)
        {
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.npc[(int) this.Projectile.ai[1]]).Center, ((Entity) this.Projectile).Center);
          double num1 = (double) ((Vector2) ref vector2).Length();
          float num2 = 15f;
          float num3 = 15f;
          if (num1 > 2.0)
          {
            ((Vector2) ref vector2).Normalize();
            vector2 = Vector2.op_Multiply(vector2, num2);
            ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, num3 - 1f), vector2), num3);
          }
          else if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
          {
            ((Entity) this.Projectile).velocity.X = -0.15f;
            ((Entity) this.Projectile).velocity.Y = -0.05f;
          }
        }
      }
      ++this.Projectile.ai[0];
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 8, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
      }
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
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
