// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Timber.TimberPalmTree
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Timber
{
  public class TimberPalmTree : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Minions/PalmTreeSentry";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 70;
      ((Entity) this.Projectile).height = 70;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.penetrate = -1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<TimberChampion>());
      if (npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        Player player = Main.player[npc.target];
        this.Projectile.timeLeft = 2;
        if ((double) this.Projectile.ai[1] == 0.0)
        {
          this.Projectile.tileCollide = true;
          if ((double) ((Entity) this.Projectile).Distance(((Entity) player).Center) > 1200.0)
          {
            this.Projectile.ai[1] = 1f;
            this.Projectile.netUpdate = true;
          }
          else
          {
            if ((double) ((Entity) this.Projectile).velocity.Y == 0.0 && (double) --this.Projectile.localAI[1] < 0.0)
            {
              this.Projectile.localAI[1] = 120f;
              if (FargoSoulsUtil.HostCheck)
              {
                float num = 90f;
                Vector2 vector2 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.Projectile).Center);
                vector2.X /= num;
                vector2.Y = (float) ((double) vector2.Y / (double) num - 0.10000000149011612 * (double) num);
                for (int index = 0; index < 10; ++index)
                  Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Addition(vector2, Vector2.op_Multiply(Utils.NextVector2Square(Main.rand, -0.5f, 0.5f), 2f)), ModContent.ProjectileType<TimberAcorn>(), this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
            ((Entity) this.Projectile).velocity.X *= 0.95f;
            ((Entity) this.Projectile).velocity.Y = ((Entity) this.Projectile).velocity.Y + 0.2f;
            if ((double) ((Entity) this.Projectile).velocity.Y <= 16.0)
              return;
            ((Entity) this.Projectile).velocity.Y = 16f;
          }
        }
        else
        {
          this.Projectile.tileCollide = false;
          this.Projectile.localAI[1] = 0.0f;
          if ((double) ((Entity) this.Projectile).Distance(((Entity) player).Center) < 500.0)
          {
            this.Projectile.ai[1] = 0.0f;
            this.Projectile.netUpdate = true;
          }
          else
          {
            Vector2 center = ((Entity) player).Center;
            if ((double) ((Entity) this.Projectile).Center.X < (double) center.X)
            {
              ((Entity) this.Projectile).velocity.X += 0.35f;
              if ((double) ((Entity) this.Projectile).velocity.X < 0.0)
                ((Entity) this.Projectile).velocity.X += 0.7f;
            }
            else
            {
              ((Entity) this.Projectile).velocity.X -= 0.35f;
              if ((double) ((Entity) this.Projectile).velocity.X > 0.0)
                ((Entity) this.Projectile).velocity.X -= 0.7f;
            }
            if ((double) ((Entity) this.Projectile).Center.Y < (double) center.Y)
            {
              ((Entity) this.Projectile).velocity.Y += 0.35f;
              if ((double) ((Entity) this.Projectile).velocity.Y < 0.0)
                ((Entity) this.Projectile).velocity.Y += 0.7f;
            }
            else
            {
              ((Entity) this.Projectile).velocity.Y -= 0.35f;
              if ((double) ((Entity) this.Projectile).velocity.Y > 0.0)
                ((Entity) this.Projectile).velocity.Y -= 0.7f;
            }
            if ((double) Math.Abs(((Entity) this.Projectile).velocity.X) > 20.0)
              ((Entity) this.Projectile).velocity.X = 20f * (float) Math.Sign(((Entity) this.Projectile).velocity.X);
            if ((double) Math.Abs(((Entity) this.Projectile).velocity.Y) <= 20.0)
              return;
            ((Entity) this.Projectile).velocity.Y = 20f * (float) Math.Sign(((Entity) this.Projectile).velocity.Y);
          }
        }
      }
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = false;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity) => false;

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
