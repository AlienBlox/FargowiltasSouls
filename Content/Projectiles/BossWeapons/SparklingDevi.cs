// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.SparklingDevi
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
  public class SparklingDevi : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Assets/ExtraTextures/Eternals/DevianttSoul";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 4;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 34;
      ((Entity) this.Projectile).height = 50;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.netImportant = true;
      this.Projectile.timeLeft = 115;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void AI()
    {
      this.Projectile.scale = 1f;
      Player player = Main.player[this.Projectile.owner];
      int prioritizingMinionFocus = FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 2000f, center: new Vector2());
      if ((double) ++this.Projectile.ai[0] == 50.0)
      {
        this.Projectile.netUpdate = true;
        if (this.Projectile.owner == Main.myPlayer)
        {
          Vector2 vector2 = Utils.RotatedBy(new Vector2(0.0f, -275f), Math.PI / 4.0 * (double) this.Projectile.spriteDirection, new Vector2());
          FargoSoulsUtil.NewSummonProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).Center, vector2), Vector2.Zero, ModContent.ProjectileType<SparklingLoveBig>(), this.Projectile.originalDamage, this.Projectile.knockBack, this.Projectile.owner, ai1: (float) this.Projectile.identity);
        }
      }
      else if ((double) this.Projectile.ai[0] < 100.0)
      {
        Vector2 targetPos;
        if (prioritizingMinionFocus != -1 && Main.npc[prioritizingMinionFocus].CanBeChasedBy((object) this.Projectile, false))
        {
          targetPos = ((Entity) Main.npc[prioritizingMinionFocus]).Center;
          ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).Center.X > (double) targetPos.X ? 1 : -1;
          targetPos.X += (float) (500 * ((Entity) this.Projectile).direction);
          targetPos.Y -= 200f;
        }
        else
        {
          ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = -((Entity) Main.player[this.Projectile.owner]).direction;
          targetPos = Vector2.op_Addition(((Entity) Main.player[this.Projectile.owner]).Center, new Vector2((float) (100 * ((Entity) this.Projectile).direction), -100f));
        }
        if ((double) ((Entity) this.Projectile).Distance(targetPos) > 50.0)
          this.Movement(targetPos, 1f);
      }
      else if ((double) this.Projectile.ai[0] == 99.0 || (double) this.Projectile.ai[0] == 100.0)
      {
        this.Projectile.netUpdate = true;
        if (this.Projectile.owner == Main.myPlayer)
        {
          Vector2 vector2 = prioritizingMinionFocus == -1 || !Main.npc[prioritizingMinionFocus].CanBeChasedBy((object) this.Projectile, false) ? Main.MouseWorld : Vector2.op_Addition(((Entity) Main.npc[prioritizingMinionFocus]).Center, Vector2.op_Multiply(((Entity) Main.npc[prioritizingMinionFocus]).velocity, 10f));
          ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).Center.X > (double) vector2.X ? 1 : -1;
          vector2.X += (float) (360 * ((Entity) this.Projectile).direction);
          if ((double) this.Projectile.ai[0] == 100.0)
          {
            ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Subtraction(vector2, ((Entity) this.Projectile).Center), (float) this.Projectile.timeLeft);
            Projectile projectile = this.Projectile;
            ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
          }
        }
      }
      if (++this.Projectile.frameCounter > 4)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= 4)
          this.Projectile.frame = 0;
      }
      int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 86, ((Entity) this.Projectile).velocity.X / 2f, ((Entity) this.Projectile).velocity.Y / 2f, 0, new Color(), 1.5f);
      Main.dust[index].noGravity = true;
    }

    private void Movement(Vector2 targetPos, float speedModifier)
    {
      if ((double) ((Entity) this.Projectile).Center.X < (double) targetPos.X)
      {
        ((Entity) this.Projectile).velocity.X += speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.X < 0.0)
          ((Entity) this.Projectile).velocity.X += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.Projectile).velocity.X -= speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.X > 0.0)
          ((Entity) this.Projectile).velocity.X -= speedModifier * 2f;
      }
      if ((double) ((Entity) this.Projectile).Center.Y < (double) targetPos.Y)
      {
        ((Entity) this.Projectile).velocity.Y += speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.Y < 0.0)
          ((Entity) this.Projectile).velocity.Y += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.Projectile).velocity.Y -= speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.Y > 0.0)
          ((Entity) this.Projectile).velocity.Y -= speedModifier * 2f;
      }
      if ((double) Math.Abs(((Entity) this.Projectile).velocity.X) > 24.0)
        ((Entity) this.Projectile).velocity.X = (float) (24 * Math.Sign(((Entity) this.Projectile).velocity.X));
      if ((double) Math.Abs(((Entity) this.Projectile).velocity.Y) <= 24.0)
        return;
      ((Entity) this.Projectile).velocity.Y = (float) (24 * Math.Sign(((Entity) this.Projectile).velocity.Y));
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(119, 300, false);
      target.immune[this.Projectile.owner] = 1;
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
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 150), this.Projectile.Opacity), 0.75f));
    }
  }
}
