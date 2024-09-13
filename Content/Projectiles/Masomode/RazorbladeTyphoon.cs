// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.RazorbladeTyphoon
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class RazorbladeTyphoon : ModProjectile
  {
    private float originalSpeed;
    private bool spawned;

    public virtual string Texture => "Terraria/Images/Projectile_409";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 3;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 8;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 600;
      this.Projectile.alpha = 100;
    }

    public virtual bool CanHitPlayer(Player target) => true;

    public virtual void AI()
    {
      if (!this.spawned)
      {
        this.spawned = true;
        this.originalSpeed = ((Vector2) ref ((Entity) this.Projectile).velocity).Length();
      }
      ((Entity) this.Projectile).velocity = Vector2.op_Multiply(this.originalSpeed, Utils.RotatedBy(Vector2.Normalize(((Entity) this.Projectile).velocity), (double) this.Projectile.ai[1] / (2.0 * Math.PI * (double) this.Projectile.ai[0] * (double) ++this.Projectile.localAI[0]), new Vector2()));
      int num = Main.rand.Next(3);
      for (int index1 = 0; index1 < num; ++index1)
      {
        Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) ((double) Utils.NextFloat(Main.rand) * 3.1415927410125732 - 1.5707963705062866)), (float) Main.rand.Next(3, 8));
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 172, vector2.X * 2f, vector2.Y * 2f, 100, new Color(), 1.4f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Division(dust1.velocity, 4f);
        Dust dust2 = Main.dust[index2];
        dust2.velocity = Vector2.op_Subtraction(dust2.velocity, ((Entity) this.Projectile).velocity);
      }
      this.Projectile.rotation += (float) (0.20000000298023224 * ((double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1.0 : -1.0));
      ++this.Projectile.frame;
      if (this.Projectile.frame <= 2)
        return;
      this.Projectile.frame = 0;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.FargoSouls().MaxLifeReduction += FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.fishBossEX, 370) ? 100 : 25;
      target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 1800, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      int num = 36;
      for (int index1 = 0; index1 < num; ++index1)
      {
        Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), new Vector2((float) ((Entity) this.Projectile).width / 2f, (float) ((Entity) this.Projectile).height)), 0.75f), (double) (index1 - (num / 2 - 1)) * 6.28318548202515 / (double) num, new Vector2()), ((Entity) this.Projectile).Center);
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
        int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, new Color(), 1.4f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Main.dust[index2].velocity = vector2_2;
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
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 2)
      {
        Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(new Color(100, 100, 250, 50));
  }
}
