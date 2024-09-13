// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.Hungry
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class Hungry : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      Main.projFrames[this.Type] = 6;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 24;
      ((Entity) this.Projectile).height = 24;
      this.Projectile.aiStyle = 1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.penetrate = 4;
      this.Projectile.timeLeft = 240;
      this.AIType = 14;
    }

    public virtual void AI()
    {
      if (++this.Projectile.frameCounter > 4)
      {
        if (++this.Projectile.frame >= Main.projFrames[this.Type])
          this.Projectile.frame = 0;
        this.Projectile.frameCounter = 0;
      }
      int index1 = Dust.NewDust(new Vector2(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).position.Y + 2f), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height + 5, 60, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index1].noGravity = true;
      int index2 = Dust.NewDust(new Vector2(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).position.Y + 2f), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height + 5, 60, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index2].noGravity = true;
      float num = 40f;
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] <= 15.0)
        return;
      this.Projectile.ai[0] = 15f;
      NPC npc = FargoSoulsUtil.NPCExists(FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 600f, true), Array.Empty<int>());
      if (!npc.Alive())
        return;
      ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), 60f), 1f / num);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      if ((double) ((Entity) this.Projectile).velocity.X != (double) oldVelocity.X)
        ((Entity) this.Projectile).velocity.X = -oldVelocity.X;
      if ((double) ((Entity) this.Projectile).velocity.Y != (double) oldVelocity.Y)
        ((Entity) this.Projectile).velocity.Y = -oldVelocity.Y;
      return --this.Projectile.penetrate <= 0;
    }

    public virtual void OnKill(int timeleft)
    {
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).Center, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 60, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).Center, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 60, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
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
      Color alpha = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
