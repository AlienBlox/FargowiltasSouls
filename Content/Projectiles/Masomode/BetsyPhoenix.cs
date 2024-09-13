// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.BetsyPhoenix
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Systems;
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
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class BetsyPhoenix : ModProjectile
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
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.tileCollide = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 600;
      this.Projectile.hostile = true;
    }

    public virtual void AI()
    {
      if ((double) --this.Projectile.ai[1] < 0.0 && (double) this.Projectile.ai[1] > -300.0)
      {
        Player player = FargoSoulsUtil.PlayerExists(this.Projectile.ai[0]);
        if (player != null)
        {
          ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 18f);
          Vector2 center = ((Entity) player).Center;
          if ((double) ((Entity) this.Projectile).Distance(center) > 200.0)
          {
            double num = (double) Utils.ToRotation(Vector2.op_Subtraction(center, ((Entity) this.Projectile).Center)) - (double) Utils.ToRotation(((Entity) this.Projectile).velocity);
            if (num > Math.PI)
              num -= 2.0 * Math.PI;
            if (num < -1.0 * Math.PI)
              num += 2.0 * Math.PI;
            ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, num * 0.2, new Vector2());
          }
          else
            this.Projectile.ai[1] = -300f;
        }
        else
          this.Projectile.ai[0] = (float) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
      }
      bool flag = WorldSavingSystem.EternityMode && SoulConfig.Instance.BossRecolors;
      int num1 = flag ? 27 : 6;
      if (this.Projectile.alpha <= 0)
      {
        for (int index = 0; index < 2; ++index)
        {
          if (!Utils.NextBool(Main.rand, 4))
          {
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).Size, 4f));
            int num2 = ((Entity) this.Projectile).width / 2;
            int num3 = ((Entity) this.Projectile).height / 2;
            int num4;
            if (!flag)
              num4 = Utils.SelectRandom<int>(Main.rand, new int[3]
              {
                6,
                31,
                31
              });
            else
              num4 = num1;
            Color color = new Color();
            Dust dust = Dust.NewDustDirect(vector2, num2, num3, num4, 0.0f, 0.0f, 0, color, 1f);
            dust.noGravity = true;
            dust.velocity = Vector2.op_Multiply(dust.velocity, 2.3f);
            dust.fadeIn = 1.5f;
            dust.noLight = true;
          }
        }
        Vector2 vector2_1 = Vector2.op_Multiply(16f, Utils.RotatedBy(new Vector2(0.0f, (float) Math.Cos((double) this.Projectile.frameCounter * 6.28318548202515 / 40.0 - 1.5707963705062866)), (double) this.Projectile.rotation, new Vector2()));
        Vector2 vector2_2 = Vector2.Normalize(((Entity) this.Projectile).velocity);
        Dust dust1 = Dust.NewDustDirect(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).Size, 4f)), ((Entity) this.Projectile).width / 2, ((Entity) this.Projectile).height / 2, num1, 0.0f, 0.0f, 0, new Color(), 1f);
        dust1.noGravity = true;
        dust1.position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1);
        dust1.velocity = Vector2.Zero;
        dust1.fadeIn = 1.4f;
        dust1.scale = 1.15f;
        dust1.noLight = true;
        dust1.position = Vector2.op_Addition(dust1.position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 1.2f));
        dust1.velocity = Vector2.op_Addition(dust1.velocity, Vector2.op_Multiply(vector2_2, 2f));
        Dust dust2 = Dust.NewDustDirect(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).Size, 4f)), ((Entity) this.Projectile).width / 2, ((Entity) this.Projectile).height / 2, num1, 0.0f, 0.0f, 0, new Color(), 1f);
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
        {
          this.Projectile.alpha = 0;
          float num5 = 16f;
          for (int index1 = 0; (double) index1 < (double) num5; ++index1)
          {
            Vector2 vector2 = Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) index1 * (6.28318548202515 / (double) num5), new Vector2())), new Vector2(1f, 4f)), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2());
            int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, num1, 0.0f, 0.0f, 0, new Color(), 1f);
            Main.dust[index2].scale = 1.5f;
            Main.dust[index2].noLight = true;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2);
            Main.dust[index2].velocity = Vector2.op_Addition(Vector2.op_Multiply(Main.dust[index2].velocity, 4f), Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.3f));
          }
        }
      }
      DelegateMethods.v3_1 = new Vector3(1f, 0.6f, 0.2f);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 4f)), 40f, BetsyPhoenix.\u003C\u003EO.\u003C0\u003E__CastLightOpen ?? (BetsyPhoenix.\u003C\u003EO.\u003C0\u003E__CastLightOpen = new Utils.TileActionAttempt((object) null, __methodptr(CastLightOpen))));
      ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X < 0.0 ? -1 : 1;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      if (((Entity) this.Projectile).direction >= 0)
        return;
      this.Projectile.rotation += 3.14159274f;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(195, 300, true, false);
      target.AddBuff(196, 300, true, false);
      target.AddBuff(67, 300, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 15; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      int num = !WorldSavingSystem.EternityMode || !SoulConfig.Instance.BossRecolors ? 6 : 27;
      for (int index3 = 0; index3 < 10; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num, 0.0f, 0.0f, 100, new Color(), 3f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num, 0.0f, 0.0f, 100, new Color(), 1f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      if (WorldSavingSystem.EternityMode && SoulConfig.Instance.BossRecolors)
        texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/Masomode/BetsyPhoenix_Recolor", (AssetRequestMode) 2).Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      this.Projectile.GetAlpha(lightColor);
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
  }
}
