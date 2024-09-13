// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.ChallengerItems.EnchantedLifebladeProjectile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

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
  public class EnchantedLifebladeProjectile : ModProjectile
  {
    public bool PlayedSound;
    private Vector2 Aim = Vector2.Zero;
    private Vector2 AimDir = Vector2.Zero;
    private Vector2 Position = Vector2.Zero;
    private const int ProjSpriteWidth = 74;
    private const int ProjSpriteHeight = 74;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 4;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 8;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 54;
      ((Entity) this.Projectile).height = 54;
      this.Projectile.aiStyle = 0;
      this.AIType = 14;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 20;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual void AI()
    {
      if (this.Projectile.frameCounter > 4)
      {
        this.Projectile.frame %= Main.projFrames[this.Type];
        this.Projectile.frameCounter = 0;
      }
      ++this.Projectile.frameCounter;
      Player player = Main.player[this.Projectile.owner];
      Vector2 vector2_1 = Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center));
      if (Main.myPlayer == this.Projectile.owner)
      {
        if ((double) this.Projectile.ai[1] < 5.0)
        {
          if ((double) this.Projectile.ai[0] == 0.0)
            this.Projectile.ai[0] = 16f;
          if ((double) this.Projectile.ai[0] == 16.0)
          {
            Vector2 vector2_2 = vector2_1;
            Vector2 vector2_3 = Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center);
            double num = (double) Math.Min(((Vector2) ref vector2_3).Length(), 450f);
            this.Aim = Vector2.op_Multiply(vector2_2, (float) num);
            this.AimDir = Vector2.Normalize(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) player).Center, this.Aim), ((Entity) this.Projectile).Center));
            if (Utils.HasNaNs(this.AimDir))
              this.AimDir = Vector2.op_Multiply(Vector2.UnitX, (float) ((Entity) player).direction);
            this.AimDir = Utils.RotatedByRandom(this.AimDir, (double) MathHelper.ToRadians(10f));
            SoundEngine.PlaySound(ref SoundID.DD2_SonicBoomBladeSlash, new Vector2?(Vector2.op_Addition(((Entity) player).Center, this.Position)), (SoundUpdateCallback) null);
          }
          if ((double) this.Projectile.ai[0] >= 16.0)
          {
            ((Entity) this.Projectile).velocity = Vector2.op_Multiply(this.AimDir, 30f);
            this.Projectile.friendly = true;
          }
          if ((double) this.Projectile.ai[0] == 31.0)
          {
            if ((double) this.Projectile.ai[1] != 4.0)
              ((Entity) this.Projectile).velocity = Vector2.Zero;
            for (int index1 = 0; index1 < 30; ++index1)
            {
              int index2 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, new Vector2(37f, 37f)), 74, 74, 70, ((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y, 100, new Color(), 1f);
              Main.dust[index2].noGravity = true;
            }
            this.Projectile.ai[0] = 1f;
            ++this.Projectile.ai[1];
          }
        }
        else
        {
          SoundEngine.PlaySound(ref SoundID.NPCDeath3, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          this.Projectile.Kill();
        }
      }
      this.DrawOffsetX = -(74 - ((Entity) this.Projectile).width);
      if ((double) this.Projectile.ai[0] < 16.0)
        this.Projectile.rotation += MathHelper.ToRadians(36f);
      if ((double) this.Projectile.ai[0] == 16.0)
        this.Projectile.rotation = Utils.ToRotation(this.AimDir) + 0.7853982f;
      player.ChangeDir((double) this.Aim.X < 0.0 ? -1 : 1);
      Tile tileSafely = Framing.GetTileSafely(((Entity) this.Projectile).Center);
      if (!((Tile) ref tileSafely).HasUnactuatedTile || !Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] || Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType])
        Lighting.AddLight(((Entity) this.Projectile).Center, 15);
      ++this.Projectile.ai[0];
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 610 - (int) Main.mouseTextColor * 2), this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.5f)
      {
        Color color1 = Color.op_Multiply(Color.op_Multiply(new Color((int) byte.MaxValue, 51, 153), this.Projectile.Opacity), 0.5f);
        ((Color) ref color1).A = (byte) ((uint) ((Color) ref alpha).A / 2U);
        float num3 = ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
        Color color2 = Color.op_Multiply(color1, num3 * num3);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          float num4 = this.Projectile.oldRot[index2];
          Vector2 vector2_2 = Vector2.op_Addition(Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, num4, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
