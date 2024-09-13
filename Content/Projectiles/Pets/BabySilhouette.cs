// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Pets.BabySilhouette
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Pets
{
  public class BabySilhouette : ModProjectile
  {
    private int realFrameCounter;
    private int realFrame;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 6;
      Main.projPet[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(886);
      this.AIType = 886;
      ((Entity) this.Projectile).width = 60;
      ((Entity) this.Projectile).height = 50;
    }

    public virtual bool PreAI()
    {
      Main.player[this.Projectile.owner].petFlagQueenBeePet = false;
      return true;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.dead)
        fargoSoulsPlayer.BabySilhouette = false;
      if (fargoSoulsPlayer.BabySilhouette)
        this.Projectile.timeLeft = 2;
      if (++this.realFrameCounter > 4)
      {
        this.realFrameCounter = 0;
        if (++this.realFrame >= Main.projFrames[this.Projectile.type])
          this.realFrame = 0;
      }
      this.Projectile.frame = this.realFrame;
      if (!Main.dayTime || (double) Utils.ToTileCoordinates(((Entity) this.Projectile).Center).Y > Main.worldSurface)
        return;
      bool[] wallLight = Main.wallLight;
      Tile tileSafely = Framing.GetTileSafely(((Entity) this.Projectile).Center);
      int index1 = (int) ((Tile) ref tileSafely).WallType;
      if (!wallLight[index1])
        return;
      for (int index2 = 0; index2 < 5; ++index2)
      {
        int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 27, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index3].noGravity = true;
        Dust dust1 = Main.dust[index3];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.9f));
      }
      for (int index4 = 0; index4 < 5; ++index4)
      {
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 54, 0.0f, 0.0f, 0, new Color(), 2f);
        Main.dust[index5].noGravity = true;
        Dust dust3 = Main.dust[index5];
        dust3.velocity = Vector2.op_Multiply(dust3.velocity, 0.5f);
        Dust dust4 = Main.dust[index5];
        dust4.velocity = Vector2.op_Addition(dust4.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.9f));
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
