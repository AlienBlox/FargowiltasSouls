// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Pets.AccursedSarcophagus
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Pets
{
  public class AccursedSarcophagus : ModProjectile
  {
    private int realFrameCounter;
    private int realFrame;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 5;
      Main.projPet[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(199);
      this.AIType = 199;
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 52;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.dead)
        fargoSoulsPlayer.AccursedSarcophagus = false;
      if (fargoSoulsPlayer.AccursedSarcophagus)
        this.Projectile.timeLeft = 2;
      if (++this.realFrameCounter > 6)
      {
        this.realFrameCounter = 0;
        if (++this.realFrame >= Main.projFrames[this.Projectile.type])
          this.realFrame = 0;
      }
      this.Projectile.frame = this.realFrame;
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
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(ModContent.Request<Texture2D>(this.Texture + "_Glow", (AssetRequestMode) 1).Value, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(Color.White, this.Projectile.Opacity), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
