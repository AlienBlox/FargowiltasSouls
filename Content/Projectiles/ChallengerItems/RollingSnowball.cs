// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.ChallengerItems.RollingSnowball
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Weapons.Challengers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.ChallengerItems
{
  public class RollingSnowball : ModProjectile
  {
    private int width;
    private int height;

    public virtual string Texture => "Terraria/Images/Projectile_166";

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(166);
      this.Projectile.aiStyle = -1;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.penetrate = -1;
      this.width = ((Entity) this.Projectile).width = 14;
      this.height = ((Entity) this.Projectile).height = 14;
      this.Projectile.netImportant = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      return new bool?((double) ((Entity) this.Projectile).Distance(FargoSoulsUtil.ClosestPointInHitbox(targetHitbox, ((Entity) this.Projectile).Center)) < (double) (Math.Min(((Entity) this.Projectile).width, ((Entity) this.Projectile).height) / 2));
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (player.dead || !((Entity) player).active || player.HeldItem.type != ModContent.ItemType<SnowballStaff>() || !player.channel || !player.CheckMana(player.HeldItem.mana, false, false))
      {
        this.Projectile.Kill();
      }
      else
      {
        if (((Entity) player).whoAmI == Main.myPlayer)
        {
          Vector2 vector2 = Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center);
          player.ChangeDir(Math.Sign(vector2.X));
          if (player.controlUseTile && (double) this.Projectile.localAI[1] <= 0.0)
          {
            this.Projectile.localAI[1] = 60f;
            ((Entity) this.Projectile).velocity.X = Math.Abs(((Entity) this.Projectile).velocity.X) * (float) Math.Sign(vector2.X);
            this.Dusts();
            ((Entity) this.Projectile).Bottom = Vector2.op_Addition(((Entity) player).Bottom, Vector2.op_Multiply(Vector2.op_Multiply((float) (((Entity) this.Projectile).width / 2), Vector2.UnitX), (float) ((Entity) player).direction));
            this.Dusts();
            this.Projectile.netUpdate = true;
          }
          --this.Projectile.localAI[1];
          if ((double) this.Projectile.localAI[1] == 0.0)
          {
            for (int index1 = 0; index1 < 30; ++index1)
            {
              int index2 = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 88, 0.0f, 0.0f, 0, new Color(), 2f);
              Main.dust[index2].noGravity = true;
              Dust dust = Main.dust[index2];
              dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
            }
          }
        }
        if ((double) ((Entity) this.Projectile).velocity.X != 0.0)
          this.Projectile.spriteDirection = ((Entity) this.Projectile).direction = Math.Sign(((Entity) this.Projectile).velocity.X);
        float num1 = ((Entity) this.Projectile).velocity.X / (3.14159274f * (float) this.width);
        this.Projectile.rotation += num1;
        if ((double) ((Entity) this.Projectile).velocity.Y == 0.0)
        {
          ((Entity) this.Projectile).velocity.X *= 0.98f;
          this.Projectile.scale = Math.Min(this.Projectile.scale + Math.Abs(num1) * 0.2f, 15f);
        }
        ((Entity) this.Projectile).velocity.Y += 0.4f;
        if ((double) ((Entity) this.Projectile).velocity.Y > 16.0)
          ((Entity) this.Projectile).velocity.Y = 16f;
        if (this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) player).Hitbox))
        {
          float num2 = 0.6f * (float) (1.0 - (double) Math.Abs(((Entity) this.Projectile).Center.X - FargoSoulsUtil.ClosestPointInHitbox((Entity) player, ((Entity) this.Projectile).Center).X) / (double) (((Entity) this.Projectile).width / 2));
          ((Entity) this.Projectile).velocity.X += num2 * ((double) ((Entity) this.Projectile).Center.X < (double) ((Entity) player).Center.X ? -1f : 1f);
          ((Entity) this.Projectile).velocity.Y += num2 * ((double) ((Entity) this.Projectile).Center.Y < (double) ((Entity) player).Center.Y ? -1f : 1f);
        }
        ((Entity) this.Projectile).position = ((Entity) this.Projectile).Bottom;
        ((Entity) this.Projectile).width = (int) ((double) this.width * (double) this.Projectile.scale);
        ((Entity) this.Projectile).height = (int) ((double) this.height * (double) this.Projectile.scale);
        ((Entity) this.Projectile).Bottom = ((Entity) this.Projectile).position;
        this.Projectile.timeLeft = 2;
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Dig, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      this.Dusts();
    }

    private void Dusts()
    {
      int num1 = (int) (10.0 * (double) this.Projectile.scale);
      for (int index1 = 0; index1 < num1; ++index1)
      {
        Vector2 position = ((Entity) this.Projectile).position;
        int width = ((Entity) this.Projectile).width;
        int height = ((Entity) this.Projectile).height;
        float num2 = (float) (1.0 + (double) this.Projectile.scale / 10.0);
        Color color = new Color();
        double num3 = (double) num2;
        int index2 = Dust.NewDust(position, width, height, 51, 0.0f, 0.0f, 0, color, (float) num3);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.8f);
        Main.dust[index2].noGravity = true;
      }
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = false;
      width = 20;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      if ((double) ((Entity) this.Projectile).velocity.X != (double) oldVelocity.X && (double) Math.Abs(oldVelocity.X) > 1.0)
      {
        ((Entity) this.Projectile).velocity.X = (float) (-(double) oldVelocity.X * 0.5);
        this.Projectile.netUpdate = true;
      }
      if ((double) ((Entity) this.Projectile).velocity.Y != (double) oldVelocity.Y && (double) Math.Abs(oldVelocity.Y) > 4.0)
      {
        ((Entity) this.Projectile).velocity.Y = (float) (-(double) oldVelocity.Y * 0.5);
        this.Projectile.netUpdate = true;
      }
      return false;
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      float num = Math.Abs(((Entity) this.Projectile).velocity.X) / 3f;
      if ((double) num < 0.10000000149011612)
        num = 0.1f;
      if ((double) num > 1.5)
        num = 1.5f;
      ref StatModifier local1 = ref modifiers.FinalDamage;
      local1 = StatModifier.op_Multiply(local1, (float) Math.Sqrt((double) this.Projectile.scale) * num);
      ref StatModifier local2 = ref modifiers.Knockback;
      local2 = StatModifier.op_Multiply(local2, Math.Max(1f, Math.Abs(((Entity) this.Projectile).velocity.X) * 1.5f));
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      float num = (float) (1.0 - (double) Math.Abs(((Entity) this.Projectile).velocity.X) / 4.0);
      if ((double) num < 0.0)
        num = 0.0f;
      target.immune[this.Projectile.owner] = 10 + (int) (50.0 * (double) num);
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
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
