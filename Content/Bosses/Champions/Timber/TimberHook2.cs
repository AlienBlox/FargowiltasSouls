// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Timber.TimberHook2
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
namespace FargowiltasSouls.Content.Bosses.Champions.Timber
{
  public class TimberHook2 : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_13";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = 4800;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 18;
      ((Entity) this.Projectile).height = 18;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.FargoSouls().GrazeCheck = (Func<Projectile, bool>) (projectile =>
      {
        float num = 0.0f;
        bool? nullable = base.CanDamage();
        bool flag = true;
        return nullable.GetValueOrDefault() == flag & nullable.HasValue && Collision.CheckAABBvLineCollision(Utils.TopLeft(((Entity) Main.LocalPlayer).Hitbox), Utils.Size(((Entity) Main.LocalPlayer).Hitbox), new Vector2(this.Projectile.localAI[0], this.Projectile.localAI[1]), ((Entity) this.Projectile).Center, (float) (22.0 * (double) this.Projectile.scale + (double) Main.LocalPlayer.FargoSouls().GrazeRadius * 2.0 + 42.0), ref num);
      });
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool? CanDamage() => new bool?(this.canHurt);

    private bool canHurt => (double) this.Projectile.ai[1] < 0.0;

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<TimberChampionHead>());
      if (npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        if ((double) --this.Projectile.ai[1] < 0.0)
        {
          if ((double) this.Projectile.ai[1] < -120.0)
          {
            this.Projectile.Kill();
            return;
          }
          this.Projectile.localAI[0] = ((Entity) npc).Center.X;
          this.Projectile.localAI[1] = ((Entity) npc).Center.Y;
          int num1 = (int) ((Entity) this.Projectile).Distance(((Entity) npc).Center);
          Vector2 vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center);
          for (int index1 = 2; index1 < num1; index1 += 150)
          {
            float num2 = (float) index1 + Utils.NextFloat(Main.rand, -150f, 150f);
            if ((double) num2 < 0.0)
              num2 = 0.0f;
            if ((double) num2 > (double) num1)
              num2 = (float) num1;
            int index2 = Dust.NewDust(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, num2)), 0, 0, 92, 0.0f, 0.0f, 0, new Color(), 1f);
            Main.dust[index2].scale = 0.75f;
            Main.dust[index2].noGravity = true;
          }
        }
        if ((double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) > 1500.0 + (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center))
          ((Entity) this.Projectile).velocity = Vector2.Zero;
        if (!this.Projectile.tileCollide && !Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
          this.Projectile.tileCollide = true;
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).DirectionFrom(((Entity) npc).Center)) + 1.57079637f;
      }
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      return new bool?(Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), new Vector2(this.Projectile.localAI[0], this.Projectile.localAI[1]), ((Entity) this.Projectile).Center));
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      ((Entity) this.Projectile).velocity = Vector2.Zero;
      return false;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      bool flag1 = this.canHurt && this.Projectile.timeLeft % 10 < 5;
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<TimberChampionHead>());
      if (npc != null && TextureAssets.Chain.IsLoaded)
      {
        Texture2D texture2D = TextureAssets.Chain.Value;
        Vector2 vector2_1 = ((Entity) this.Projectile).Center;
        Vector2 center = ((Entity) npc).Center;
        Rectangle? nullable = new Rectangle?();
        Vector2 vector2_2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_2).\u002Ector((float) texture2D.Width * 0.5f, (float) texture2D.Height * 0.5f);
        float height = (float) texture2D.Height;
        Vector2 vector2_3 = Vector2.op_Subtraction(center, vector2_1);
        float num = (float) Math.Atan2((double) vector2_3.Y, (double) vector2_3.X) - 1.57f;
        bool flag2 = true;
        if (float.IsNaN(vector2_1.X) && float.IsNaN(vector2_1.Y))
          flag2 = false;
        if (float.IsNaN(vector2_3.X) && float.IsNaN(vector2_3.Y))
          flag2 = false;
        while (flag2)
        {
          if ((double) ((Vector2) ref vector2_3).Length() < (double) height + 1.0)
          {
            flag2 = false;
          }
          else
          {
            Vector2 vector2_4 = vector2_3;
            ((Vector2) ref vector2_4).Normalize();
            vector2_1 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(vector2_4, height));
            vector2_3 = Vector2.op_Subtraction(center, vector2_1);
            Color color1 = Lighting.GetColor((int) vector2_1.X / 16, (int) ((double) vector2_1.Y / 16.0));
            Color color2 = flag1 ? Color.op_Multiply(Color.White, this.Projectile.Opacity) : this.Projectile.GetAlpha(color1);
            Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(vector2_1, Main.screenPosition), nullable, color2, num, vector2_2, 1f, (SpriteEffects) 0, 0.0f);
            if (flag1)
            {
              ((Color) ref color2).A = (byte) 0;
              Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(vector2_1, Main.screenPosition), nullable, color2, num, vector2_2, 1f, (SpriteEffects) 0, 0.0f);
            }
          }
        }
      }
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Color color = flag1 ? Color.op_Multiply(Color.White, this.Projectile.Opacity) : this.Projectile.GetAlpha(lightColor);
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      if (flag1)
      {
        ((Color) ref color).A = (byte) 0;
        Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      return false;
    }
  }
}
