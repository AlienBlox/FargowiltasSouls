// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Shucks.Crimetroid
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Shucks
{
  public class Crimetroid : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 4;
      Main.projPet[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft *= 5;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
    }

    public virtual bool MinionContactDamage() => true;

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      PatreonPlayer modPlayer = player.GetModPlayer<PatreonPlayer>();
      if (!((Entity) player).active || player.dead || player.ghost)
        modPlayer.Crimetroid = false;
      if (modPlayer.Crimetroid)
        this.Projectile.timeLeft = 2;
      if (++this.Projectile.frameCounter > 6)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          this.Projectile.frame = 0;
      }
      double num1 = (double) ((Entity) this.Projectile).Distance(((Entity) player).Top);
      if (num1 > 400.0)
        this.Projectile.tileCollide = false;
      if (num1 > 2000.0)
        ((Entity) this.Projectile).Center = ((Entity) player).Center;
      Vector2 vector2_1 = ((Entity) player).Top;
      if (num1 < 60.0)
      {
        this.Projectile.localAI[0] = 0.0f;
        vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity);
        if (Vector2.op_Equality(vector2_1, ((Entity) this.Projectile).Center))
          --vector2_1.Y;
        if (!this.Projectile.tileCollide)
          this.Projectile.tileCollide = Collision.CanHitLine(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, ((Entity) player).position, ((Entity) player).width, ((Entity) player).height);
      }
      else if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = MathHelper.ToRadians(Utils.NextFloat(Main.rand, 45f));
        if (Utils.NextBool(Main.rand))
          this.Projectile.localAI[0] *= -1f;
      }
      this.Projectile.localAI[0] *= 0.99f;
      float num2 = 8f;
      if ((double) this.Projectile.localAI[1] > 0.0)
      {
        --this.Projectile.localAI[1];
        num2 *= 0.5f;
      }
      if (!Utils.HasNaNs(((Entity) player).velocity) && Vector2.op_Inequality(((Entity) player).velocity, Vector2.Zero))
        num2 += ((Vector2) ref ((Entity) player).velocity).Length() / 2f;
      Vector2 vector2_2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, vector2_1), num2);
      if ((double) this.Projectile.localAI[0] != 0.0)
        vector2_2 = Utils.RotatedBy(vector2_2, (double) this.Projectile.localAI[0], new Vector2());
      ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 21f), vector2_2), 22f);
      this.Projectile.rotation = ((Entity) this.Projectile).velocity.X * 0.05f;
      if ((double) Math.Abs(this.Projectile.rotation) <= (double) MathHelper.ToRadians(75f))
        return;
      this.Projectile.rotation = MathHelper.ToRadians(75f) * (float) Math.Sign(this.Projectile.rotation);
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = true;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      this.Projectile.localAI[1] = 15f;
      if ((double) ((Entity) this.Projectile).velocity.X != (double) oldVelocity.X)
        ((Entity) this.Projectile).velocity.X = (float) (-(double) oldVelocity.X * 0.75);
      if ((double) ((Entity) this.Projectile).velocity.Y != (double) oldVelocity.Y)
        ((Entity) this.Projectile).velocity.Y = (float) (-(double) oldVelocity.Y * 0.75);
      return false;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (this.Projectile.owner != Main.myPlayer)
        return;
      this.Projectile.vampireHeal((int) Math.Round(40.0 / 3.0, (MidpointRounding) 1) + 1, ((Entity) this.Projectile).Center, (Entity) target);
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Patreon/Shucks/CrimetroidJelly", (AssetRequestMode) 1).Value, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(alpha, 0.5f), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
