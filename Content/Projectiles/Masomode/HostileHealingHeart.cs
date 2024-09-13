// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.HostileHealingHeart
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class HostileHealingHeart : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/FakeHeart";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 12;
      ((Entity) this.Projectile).height = 12;
      this.Projectile.timeLeft = 600;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.aiStyle = -1;
      this.Projectile.extraUpdates = 1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      if ((double) --this.Projectile.ai[1] < 0.0)
      {
        NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0]);
        if (npc.Alive() && !npc.friendly)
        {
          for (int index = 0; index < 3; ++index)
          {
            Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), 2f);
            ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 29f), vector2), 30f);
          }
          if ((double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) < 16.0 && FargoSoulsUtil.HostCheck)
          {
            npc.life += this.Projectile.damage;
            npc.HealEffect(this.Projectile.damage, true);
            if (npc.life > npc.lifeMax)
              npc.life = npc.lifeMax;
            this.Projectile.Kill();
          }
        }
        else
          this.Projectile.Kill();
      }
      for (int index = 0; index < 3; ++index)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
      }
      if (!Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero))
        return;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 15; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 86, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) ((Color) ref lightColor).G, (int) ((Color) ref lightColor).B, (int) ((Color) ref lightColor).A), this.Projectile.Opacity));
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
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
