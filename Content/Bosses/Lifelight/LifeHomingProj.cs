// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeHomingProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeHomingProj : ModProjectile
  {
    public bool home = true;
    public bool BeenOutside;

    public virtual string Texture => "Terraria/Images/NPC_75";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.npcFrameCount[75];
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.aiStyle = 0;
      this.Projectile.hostile = true;
      this.AIType = 14;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1.2f;
    }

    public virtual void AI()
    {
      ref float local1 = ref this.Projectile.ai[0];
      ref float local2 = ref this.Projectile.ai[1];
      ref float local3 = ref this.Projectile.ai[2];
      ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X < 0.0 ? 1 : -1;
      this.Projectile.rotation = (double) ((Entity) this.Projectile).velocity.X < 0.0 ? Utils.ToRotation(((Entity) this.Projectile).velocity) + 3.14159274f : Utils.ToRotation(((Entity) this.Projectile).velocity);
      if (Utils.NextBool(Main.rand, 6))
      {
        int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index].noGravity = true;
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
      }
      if (++this.Projectile.frameCounter >= 4)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          this.Projectile.frame = 0;
      }
      if ((double) local1 > 30.0)
      {
        if ((double) local1 == 31.0)
          local2 = (float) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
        if (((Entity) Main.player[(int) local2]).active && !Main.player[(int) local2].dead)
        {
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[(int) local2]).Center, ((Entity) this.Projectile).Center);
          double num1 = (double) ((Vector2) ref vector2).Length();
          float num2 = WorldSavingSystem.MasochistModeReal ? 24f : 22f;
          float num3 = 15f;
          float num4 = WorldSavingSystem.MasochistModeReal ? 150f : 180f;
          if (num1 > (double) num4 && this.home)
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
          if (num1 > (double) num4)
            this.BeenOutside = true;
          if (num1 < (double) num4 && this.BeenOutside)
            this.home = false;
        }
      }
      if ((double) local1 > 540.0)
        local3 = 1f;
      if ((double) local3 == 1.0)
      {
        this.Projectile.Opacity -= 0.0166666675f;
        if ((double) this.Projectile.Opacity < 0.05000000074505806)
          this.Projectile.Kill();
      }
      ++local1;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<SmiteBuff>(), 180, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 610 - (int) Main.mouseTextColor * 2), this.Projectile.Opacity));
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.3f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
