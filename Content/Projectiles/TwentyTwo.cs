// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.TwentyTwo
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class TwentyTwo : ModProjectile
  {
    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 42;
      ((Entity) this.Projectile).height = 42;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 6;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] != 0.0)
        return;
      SoundEngine.PlaySound(ref SoundID.ScaryScream, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      if (this.Projectile.owner == Main.myPlayer)
      {
        Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
        int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
        int num2 = num1 * this.Projectile.frame;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
        Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
        Color alpha = this.Projectile.GetAlpha(lightColor);
        float num3 = (float) ((double) this.Projectile.scale * (double) Main.screenHeight / 254.0) / Main.GameZoomTarget;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, num3, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }
  }
}
