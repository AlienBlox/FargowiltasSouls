// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Pets.BabyLifelight
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
  public class BabyLifelight : ModProjectile
  {
    private int realFrameCounter;
    private int realFrame;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 4;
      Main.projPet[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(886);
      this.AIType = 886;
      ((Entity) this.Projectile).width = 82;
      ((Entity) this.Projectile).height = 38;
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
        fargoSoulsPlayer.BabyLifelight = false;
      if (fargoSoulsPlayer.BabyLifelight)
        this.Projectile.timeLeft = 2;
      if (++this.realFrameCounter > 2)
      {
        this.realFrameCounter = 0;
        if (++this.realFrame >= Main.projFrames[this.Projectile.type])
          this.realFrame = 0;
      }
      this.Projectile.frame = this.realFrame;
      Vector2 center = ((Entity) this.Projectile).Center;
      Color pink1 = Color.Pink;
      double num1 = (double) ((Color) ref pink1).R / (double) byte.MaxValue;
      Color pink2 = Color.Pink;
      double num2 = (double) ((Color) ref pink2).G / (double) byte.MaxValue;
      Color pink3 = Color.Pink;
      double num3 = (double) ((Color) ref pink3).B / (double) byte.MaxValue;
      Lighting.AddLight(center, (float) num1, (float) num2, (float) num3);
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
