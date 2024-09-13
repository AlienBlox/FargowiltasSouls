// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomPhoenix
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.AbomBoss
{
  public class AbomPhoenix : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_706";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
      Main.projFrames[this.Projectile.type] = Main.projFrames[706];
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 40;
      this.Projectile.alpha = 100;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 300;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      if ((double) ++this.Projectile.localAI[2] < 90.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.045f);
      }
      int num = 6;
      if (this.Projectile.alpha <= 0)
      {
        Vector2 vector2_1 = Vector2.op_Multiply(16f, Utils.RotatedBy(new Vector2(0.0f, (float) Math.Cos((double) this.Projectile.frameCounter * 6.28318548202515 / 40.0 - 1.5707963705062866)), (double) this.Projectile.rotation, new Vector2()));
        Vector2 vector2_2 = Vector2.Normalize(((Entity) this.Projectile).velocity);
        Dust dust1 = Dust.NewDustDirect(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).Size, 4f)), ((Entity) this.Projectile).width / 2, ((Entity) this.Projectile).height / 2, num, 0.0f, 0.0f, 0, new Color(), 1f);
        dust1.noGravity = true;
        dust1.position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1);
        dust1.velocity = Vector2.Zero;
        dust1.fadeIn = 1.4f;
        dust1.scale = 1.15f;
        dust1.noLight = true;
        dust1.position = Vector2.op_Addition(dust1.position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 1.2f));
        dust1.velocity = Vector2.op_Addition(dust1.velocity, Vector2.op_Multiply(vector2_2, 2f));
        Dust dust2 = Dust.NewDustDirect(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).Size, 4f)), ((Entity) this.Projectile).width / 2, ((Entity) this.Projectile).height / 2, num, 0.0f, 0.0f, 0, new Color(), 1f);
        dust2.noGravity = true;
        dust2.position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1);
        dust2.velocity = Vector2.Zero;
        dust2.fadeIn = 1.4f;
        dust2.scale = 1.15f;
        dust2.noLight = true;
        dust2.position = Vector2.op_Addition(dust2.position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.5f));
        dust2.position = Vector2.op_Addition(dust2.position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 1.2f));
        dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(vector2_2, 2f));
      }
      if (++this.Projectile.frameCounter >= 40)
        this.Projectile.frameCounter = 0;
      this.Projectile.frame = this.Projectile.frameCounter / 5;
      if (this.Projectile.alpha > 0)
      {
        this.Projectile.alpha -= 55;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
      }
      DelegateMethods.v3_1 = new Vector3(1f, 0.6f, 0.2f);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 4f)), 40f, AbomPhoenix.\u003C\u003EO.\u003C0\u003E__CastLightOpen ?? (AbomPhoenix.\u003C\u003EO.\u003C0\u003E__CastLightOpen = new Utils.TileActionAttempt((object) null, __methodptr(CastLightOpen))));
      ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X < 0.0 ? -1 : 1;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      if (((Entity) this.Projectile).direction >= 0)
        return;
      this.Projectile.rotation += 3.14159274f;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), 0.75f), 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.AddBuff(ModContent.BuffType<AbomFangBuff>(), 300, true, false);
        target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 120, true, false);
      }
      target.AddBuff(30, 600, true, false);
    }
  }
}
