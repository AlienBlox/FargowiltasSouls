// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.FakeHeart2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class FakeHeart2 : FakeHeart
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/FakeHeart";

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ProjectileID.Sets.TrailCacheLength[this.Type] = 7;
      ProjectileID.Sets.TrailingMode[this.Type] = 2;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
    }

    public override void AI()
    {
      float num = (float) ((double) Main.rand.Next(90, 111) * 0.0099999997764825821 * ((double) Main.essScale * 0.5));
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.5f * num, 0.1f * num, 0.1f * num);
      --this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] > 0.0)
        this.Projectile.rotation = -Utils.ToRotation(((Entity) this.Projectile).velocity);
      else if ((double) this.Projectile.ai[0] == 0.0)
      {
        ((Entity) this.Projectile).velocity = Vector2.Zero;
      }
      else
      {
        --this.Projectile.ai[1];
        if ((double) this.Projectile.ai[1] == 0.0)
        {
          Player player = FargoSoulsUtil.PlayerExists((int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0));
          if (player != null)
          {
            ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center), 20f);
            this.Projectile.netUpdate = true;
          }
        }
        if ((double) this.Projectile.ai[1] <= 0.0)
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      }
      this.Projectile.rotation -= 1.57079637f;
    }

    public override bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color hotPink = Color.HotPink;
        ((Color) ref hotPink).A = (byte) 50;
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(hotPink, 0.75f), this.Projectile.Opacity), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      return base.PreDraw(ref lightColor);
    }
  }
}
