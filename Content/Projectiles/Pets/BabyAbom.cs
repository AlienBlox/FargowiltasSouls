// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Pets.BabyAbom
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
  public class BabyAbom : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 8;
      Main.projPet[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 50;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = 26;
      this.AIType = 198;
      this.Projectile.netImportant = true;
      this.Projectile.friendly = true;
    }

    public virtual bool PreAI()
    {
      Main.player[this.Projectile.owner].hornet = false;
      return true;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.dead)
        fargoSoulsPlayer.BabyAbom = false;
      if (fargoSoulsPlayer.BabyAbom)
        this.Projectile.timeLeft = 2;
      if ((double) ((Entity) this.Projectile).Distance(((Entity) player).Center) > 3000.0)
        ((Entity) this.Projectile).Center = ((Entity) player).Center;
      for (int index1 = 0; index1 < 2; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, (int) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale), (int) ((double) ((Entity) this.Projectile).height * (double) this.Projectile.scale), 27, ((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y, 100, new Color(), 1.5f);
        Vector2 position = ((Entity) this.Projectile).position;
        if (((Entity) this.Projectile).direction >= 0)
          position.X += (float) ((Entity) this.Projectile).width;
        position.Y += (float) (((Entity) this.Projectile).height / 2);
        Main.dust[index2].position = Vector2.op_Division(Vector2.op_Addition(Main.dust[index2].position, position), 2f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity = Vector2.op_Multiply(Main.dust[index2].velocity, 0.3f);
        Main.dust[index2].velocity = Vector2.op_Subtraction(Main.dust[index2].velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.1f));
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
