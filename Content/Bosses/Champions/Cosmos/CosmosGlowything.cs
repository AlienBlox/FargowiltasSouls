// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Cosmos.CosmosGlowything
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Cosmos
{
  public class CosmosGlowything : ModProjectile
  {
    private float scalefactor;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 18;
      ((Entity) this.Projectile).height = 18;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.aiStyle = -1;
      this.Projectile.scale = 0.5f;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.ai[0] == 0.0)
        this.scalefactor = 0.07f;
      else
        this.scalefactor -= 0.005f;
      this.Projectile.scale += this.scalefactor;
      if ((double) this.Projectile.scale > 2.0)
      {
        ++this.Projectile.ai[0];
        this.scalefactor = 0.0f;
      }
      if ((double) this.Projectile.scale > 0.0)
        return;
      this.Projectile.Kill();
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int height = texture2D.Height;
      int num = 0;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num, texture2D.Width, height);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color = Color.Lerp(new Color(196, 247, (int) byte.MaxValue, 0), Color.Transparent, 0.8f);
      float scale = this.Projectile.scale;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2, scale * 2f, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
