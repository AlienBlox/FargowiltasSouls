// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.CursedCoffin.CoffinSlamShockwave
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.CursedCoffin
{
  public class CoffinSlamShockwave : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Type] = 3;
      ProjectileID.Sets.TrailCacheLength[this.Type] = 12;
      ProjectileID.Sets.TrailingMode[this.Type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 52;
      ((Entity) this.Projectile).height = 70;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.light = 1f;
      this.Projectile.timeLeft = 180;
    }

    public virtual void AI()
    {
      this.Projectile.Animate((int) Math.Round(12.0 - (double) MathHelper.Clamp((float) (6.0 * (double) ((Entity) this.Projectile).velocity.X / 60.0), 0.0f, 6f)));
      if ((double) Math.Abs(((Entity) this.Projectile).velocity.X) < 15.0)
        ((Entity) this.Projectile).velocity.X *= 1.035f;
      int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
      if (closest.IsWithinBounds((int) byte.MaxValue))
      {
        Player player = Main.player[closest];
        if (player != null && player.Alive())
        {
          float num = Math.Abs(((Entity) player).Center.X - ((Entity) this.Projectile).Center.X);
          this.Projectile.light = (double) num < 500.0 ? (float) ((500.0 - (double) num) / 500.0) : 0.0f;
        }
      }
      ((Entity) this.Projectile).position.Y = MathF.Floor((float) (((double) ((Entity) this.Projectile).position.Y + (double) ((Entity) this.Projectile).height) / 16.0)) * 16f - (float) ((Entity) this.Projectile).height;
      int num1 = 0;
      do
      {
        ++num1;
        Point tileCoordinates = Utils.ToTileCoordinates(((Entity) this.Projectile).Bottom);
        Tile tile1 = ((Tilemap) ref Main.tile)[tileCoordinates.X, tileCoordinates.Y - 1];
        Tile tile2 = ((Tilemap) ref Main.tile)[tileCoordinates.X, tileCoordinates.Y];
        bool flag = ((Tile) ref tile1).HasUnactuatedTile && (Main.tileSolid[(int) ((Tile) ref tile1).TileType] || Main.tileSolidTop[(int) ((Tile) ref tile1).TileType]);
        if ((!((Tile) ref tile2).HasUnactuatedTile ? 0 : (Main.tileSolid[(int) ((Tile) ref tile2).TileType] ? 1 : (Main.tileSolidTop[(int) ((Tile) ref tile2).TileType] ? 1 : 0))) == 0 || flag)
        {
          if (flag)
          {
            Projectile projectile = this.Projectile;
            ((Entity) projectile).Center = Vector2.op_Subtraction(((Entity) projectile).Center, Vector2.op_Multiply(Vector2.UnitY, 16f));
          }
          else
          {
            Projectile projectile = this.Projectile;
            ((Entity) projectile).Center = Vector2.op_Addition(((Entity) projectile).Center, Vector2.op_Multiply(Vector2.UnitY, 16f));
          }
        }
        else
          break;
      }
      while (num1 < 10);
      if (num1 < 9)
        return;
      this.Projectile.Kill();
    }

    public virtual void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
      modifiers.Null();
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<StunnedBuff>(), 120, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      float rotation = this.Projectile.rotation;
      Vector2 center = ((Entity) this.Projectile).Center;
      Texture2D texture2D = TextureAssets.Projectile[this.Type].Value;
      Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitX, (float) ((double) this.Projectile.scale / 2.0 + (double) Math.Abs(((Entity) this.Projectile).velocity.X) / 13.0)), Vector2.op_Multiply(Vector2.UnitY, this.Projectile.scale));
      int num1 = texture2D.Height / Main.projFrames[this.Type];
      int num2 = this.Projectile.frame * num1;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = (double) ((Entity) this.Projectile).velocity.X >= 0.0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect) null, Main.GameViewMatrix.TransformationMatrix);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(lightColor, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 vector2_3 = Vector2.op_Addition(this.Projectile.oldPos[index], Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_3, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(color), num3, vector2_2, vector2_1, spriteEffects, 0.0f);
      }
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), rotation, vector2_2, vector2_1, spriteEffects, 0.0f);
      return false;
    }
  }
}
