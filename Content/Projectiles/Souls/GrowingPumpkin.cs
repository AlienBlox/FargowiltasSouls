// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.GrowingPumpkin
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class GrowingPumpkin : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 5;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.penetrate = 1;
      this.Projectile.friendly = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = true;
      this.Projectile.DamageType = DamageClass.Generic;
    }

    public virtual bool? CanDamage() => new bool?(this.Projectile.frame == 4);

    public virtual void AI()
    {
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.5f, 0.5f, 0.5f);
      ++this.Projectile.ai[0];
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!fargoSoulsPlayer.ForceEffect<PumpkinEnchant>())
      {
        ((Entity) this.Projectile).velocity.Y = ((Entity) this.Projectile).velocity.Y + 0.2f;
        if ((double) ((Entity) this.Projectile).velocity.Y > 16.0)
          ((Entity) this.Projectile).velocity.Y = 16f;
      }
      this.Projectile.damage = FargoSoulsUtil.HighestDamageTypeScaling(player, fargoSoulsPlayer.ForceEffect<PumpkinEnchant>() ? 45 : 15);
      if (this.Projectile.frame != 4)
      {
        ++this.Projectile.frameCounter;
        if (this.Projectile.frameCounter >= 60)
        {
          this.Projectile.frameCounter = 0;
          this.Projectile.frame = (this.Projectile.frame + 1) % 5;
          if (this.Projectile.frame == 4)
          {
            for (int index1 = 0; index1 < 16; ++index1)
            {
              Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitY, 5f), (double) (index1 - 7) * 6.2831854820251465 / 16.0, new Vector2()), ((Entity) this.Projectile).Center);
              Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
              int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 2, 0.0f, 0.0f, 0, new Color(), 1.5f);
              Main.dust[index2].noGravity = true;
              Main.dust[index2].velocity = vector2_2;
            }
          }
        }
      }
      else if (((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
      {
        Rectangle hitbox = ((Entity) Main.LocalPlayer).Hitbox;
        if (((Rectangle) ref hitbox).Intersects(((Entity) this.Projectile).Hitbox))
        {
          int amount = 25;
          if (fargoSoulsPlayer.ForceEffect<PumpkinEnchant>())
            amount *= 2;
          Main.LocalPlayer.FargoSouls().HealPlayer(amount);
          SoundEngine.PlaySound(ref SoundID.Item2, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
          this.Projectile.Kill();
        }
      }
      ((Entity) this.Projectile).width = (int) (32.0 * (double) this.Projectile.scale);
      ((Entity) this.Projectile).height = (int) (32.0 * (double) this.Projectile.scale);
      if ((double) this.Projectile.ai[0] > 1200.0)
        this.Projectile.scale -= 0.01f;
      if ((double) this.Projectile.scale > 0.0)
        return;
      this.Projectile.Kill();
    }

    private void SpawnFire(FargoSoulsPlayer modPlayer)
    {
      FargoSoulsUtil.XWay(5, ((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, ModContent.ProjectileType<PumpkinFlame>(), 3f, this.Projectile.damage, 0.0f);
      SoundEngine.PlaySound(ref SoundID.Item74, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = false;
      return true;
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      Projectile projectile = this.Projectile;
      ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
      ((Entity) this.Projectile).velocity = Vector2.Zero;
      return false;
    }

    public virtual void OnKill(int timeLeft)
    {
      if ((double) this.Projectile.scale <= 0.0 || this.Projectile.frame != 4)
        return;
      this.SpawnFire(Main.player[this.Projectile.owner].FargoSouls());
      for (int index1 = 0; index1 < 16; ++index1)
      {
        Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitY, 5f), (double) (index1 - 7) * 6.2831854820251465 / 16.0, new Vector2()), ((Entity) this.Projectile).Center);
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
        int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 2, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity = vector2_2;
      }
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      if (this.Projectile.frame != 4)
        return true;
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      if ((double) this.Projectile.ai[0] % 10.0 < 5.0)
        ((Color) ref alpha).A = (byte) 0;
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
