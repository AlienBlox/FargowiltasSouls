// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.BanishedBaron.BaronScrap
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.BanishedBaron
{
  public class BaronScrap : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Type] = 4;
      ProjectileID.Sets.TrailingMode[this.Type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 18;
      ((Entity) this.Projectile).height = 18;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.light = 1f;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(30, 240, true, false);
    }

    public virtual void AI()
    {
      if (Collision.WetCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.97f);
        if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).LengthSquared() < 9.0)
          this.Projectile.Opacity -= 0.05f;
        if ((double) this.Projectile.Opacity < 0.30000001192092896)
          this.Projectile.Kill();
      }
      if ((double) this.Projectile.localAI[0] == 1.0)
        this.Projectile.ai[1] = Utils.NextBool(Main.rand) ? 1f : -1f;
      this.Projectile.rotation += (float) ((double) this.Projectile.ai[1] * 6.2831854820251465 / 90.0);
      if ((double) ++this.Projectile.localAI[0] > 600.0)
        this.Projectile.Kill();
      if (this.Projectile.tileCollide || (double) this.Projectile.localAI[0] <= (double) (60 * this.Projectile.MaxUpdates))
        return;
      Tile tileSafely = Framing.GetTileSafely(((Entity) this.Projectile).Center);
      if (((Tile) ref tileSafely).HasUnactuatedTile && Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] && !Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType])
        return;
      this.Projectile.tileCollide = true;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 8, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
      }
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
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(lightColor, 0.75f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
