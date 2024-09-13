// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.NecroGrave
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class NecroGrave : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 48;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 1800;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void OnSpawn(IEntitySource source)
    {
      ((Entity) this.Projectile).Bottom = ((Entity) this.Projectile).Center;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.5f, 0.5f, 0.5f);
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item2, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if (player.FargoSouls().ForceEffect<NecroEnchant>())
      {
        this.Projectile.scale = 2f;
        ((Entity) this.Projectile).width = 48 * (int) this.Projectile.scale;
        ((Entity) this.Projectile).height = 32 * (int) this.Projectile.scale;
        if ((double) ((Entity) this.Projectile).Distance(((Entity) player).Center) < 300.0)
        {
          float num = 1f;
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center), num);
        }
        else
          ((Entity) this.Projectile).velocity = Vector2.Zero;
        for (int index1 = 0; index1 < 4; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).BottomLeft, ((Entity) this.Projectile).width, 0, 54, 0.0f, 0.0f, 0, new Color(), 1f);
          Main.dust[index2].position.Y -= 4f;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
          Main.dust[index2].noGravity = true;
        }
      }
      else
      {
        ((Entity) this.Projectile).velocity.Y += 0.2f;
        if ((double) ((Entity) this.Projectile).velocity.Y > 16.0)
          ((Entity) this.Projectile).velocity.Y = 16f;
      }
      if (!Main.LocalPlayer.Alive())
        return;
      Rectangle hitbox = ((Entity) Main.LocalPlayer).Hitbox;
      if (!((Rectangle) ref hitbox).Intersects(((Entity) this.Projectile).Hitbox) || !player.HasEffect<NecroEffect>())
        return;
      Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(0.0f, -12f), ModContent.ProjectileType<DungeonGuardianNecro>(), (int) this.Projectile.ai[0], 1f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index3 = 0; index3 < 36; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 5, 0.0f, -3f, 0, new Color(), 2f);
        Dust dust = Main.dust[index4];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
        Main.dust[index4].noGravity = Utils.NextBool(Main.rand);
      }
      this.Projectile.Kill();
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
      if (this.Projectile.timeLeft % 10 < 5)
        ((Color) ref alpha).A = (byte) 0;
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
