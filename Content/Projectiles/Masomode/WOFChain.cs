// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.WOFChain
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class WOFChain : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/NPC_115";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.npcFrameCount[115];
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = 2400;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 900;
      this.Projectile.tileCollide = false;
      this.Projectile.hostile = true;
      this.Projectile.extraUpdates = 2;
    }

    public virtual bool? CanDamage()
    {
      return this.Projectile.timeLeft > 30 && (double) this.Projectile.ai[2] != 1.0 ? base.CanDamage() : new bool?(false);
    }

    public virtual void AI()
    {
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.1f, 0.5f, 0.7f);
      if (this.Projectile.timeLeft <= 30 || (double) this.Projectile.ai[2] == 1.0)
      {
        this.Projectile.Opacity = MathHelper.Lerp(this.Projectile.Opacity, 0.0f, 0.05f);
        if ((double) this.Projectile.Opacity < 0.10000000149011612)
        {
          this.Projectile.Kill();
          return;
        }
      }
      if ((double) this.Projectile.ai[0] == 0.0)
      {
        this.Projectile.ai[0] = 1f;
        this.Projectile.localAI[0] = ((Entity) this.Projectile).Center.X;
        this.Projectile.localAI[1] = ((Entity) this.Projectile).Center.Y;
      }
      if (Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero) && Utils.NextBool(Main.rand, 3))
      {
        int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 88, ((Entity) this.Projectile).velocity.X * 0.4f, ((Entity) this.Projectile).velocity.Y * 0.4f, 114, new Color(), 2f);
        Main.dust[index].noGravity = true;
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.8f);
        Main.dust[index].velocity.Y -= 0.5f;
      }
      if ((double) ((Entity) this.Projectile).velocity.Y > 0.0 && (double) ((Entity) this.Projectile).Center.Y / 16.0 >= (double) Main.maxTilesY || (double) ((Entity) this.Projectile).velocity.Y < 0.0 && (double) ((Entity) this.Projectile).Center.Y / 16.0 <= (double) (Main.maxTilesY - 200))
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2f));
        ((Entity) this.Projectile).velocity = Vector2.Zero;
      }
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.wallBoss, 113) && (double) Math.Abs(((Entity) this.Projectile).Center.X - ((Entity) Main.npc[EModeGlobalNPC.wallBoss]).Center.X) < 50.0)
        this.Projectile.ai[2] = 1f;
      if (!Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero))
        return;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      if (++this.Projectile.frameCounter <= 6 * (this.Projectile.extraUpdates + 1))
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame < Main.projFrames[this.Projectile.type])
        return;
      this.Projectile.frame = 0;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.MasochistModeReal)
      {
        if (!target.tongued)
          SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) target).Center), (SoundUpdateCallback) null);
        target.AddBuff(38, 10, true, false);
      }
      target.AddBuff(24, 300, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      if (!TextureAssets.Chain12.IsLoaded)
        return false;
      if ((double) this.Projectile.ai[0] != 0.0)
      {
        Texture2D texture2D = TextureAssets.Chain12.Value;
        Vector2 vector2_1 = ((Entity) this.Projectile).Center;
        Vector2 vector2_2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_2).\u002Ector(this.Projectile.localAI[0], this.Projectile.localAI[1]);
        Rectangle? nullable = new Rectangle?();
        Vector2 vector2_3;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_3).\u002Ector((float) texture2D.Width * 0.5f, (float) texture2D.Height * 0.5f);
        float height = (float) texture2D.Height;
        Vector2 vector2_4 = Vector2.op_Subtraction(vector2_2, vector2_1);
        float num = (float) Math.Atan2((double) vector2_4.Y, (double) vector2_4.X) - 1.57f;
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
            Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(vector2_1, Main.screenPosition), nullable, alpha, num, vector2_3, 1f, (SpriteEffects) 0, 0.0f);
          }
        }
      }
      if (!TextureAssets.Npc[115].IsLoaded)
        return false;
      Texture2D texture2D1 = TextureAssets.Npc[115].Value;
      int num1 = texture2D1.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
