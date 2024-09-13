// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Pets.SeekerOfTreasures
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Pets
{
  public class SeekerOfTreasures : ModProjectile
  {
    private const int segments = 8;
    private const int distance = 16;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 3;
      Main.projPet[this.Projectile.type] = true;
      ProjectileID.Sets.LightPet[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 71;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 0;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(883);
      this.AIType = 883;
    }

    public virtual bool PreAI()
    {
      Main.player[this.Projectile.owner].petFlagEaterOfWorldsPet = false;
      return true;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.dead)
        fargoSoulsPlayer.SeekerOfAncientTreasures = false;
      if (fargoSoulsPlayer.SeekerOfAncientTreasures)
        this.Projectile.timeLeft = 2;
      Vector2 center = ((Entity) this.Projectile).Center;
      int projFrame = Main.projFrames[this.Projectile.type];
      for (int index = 1; index < 8; ++index)
      {
        Vector2 vector2 = Vector2.op_Addition(this.Projectile.oldPos[index * 10], Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
        float rotation = Utils.ToRotation(Vector2.op_Subtraction(center, vector2));
        Lighting.AddLight(Vector2.op_Subtraction(center, Utils.RotatedBy(new Vector2(16f, 0.0f), (double) rotation, Vector2.Zero)), 0.8f, 0.8f, 0.0f);
      }
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.8f, 0.8f, 0.0f);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      Texture2D texture2D2 = ModContent.Request<Texture2D>(this.Texture + "_Glow", (AssetRequestMode) 1).Value;
      SpriteEffects spriteEffects1 = this.Projectile.spriteDirection != 1 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Rectangle rectangle1 = Utils.Frame(texture2D1, 1, Main.projFrames[this.Projectile.type], 0, 0, 0, 0);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle1), 2f);
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition);
      Color alpha = this.Projectile.GetAlpha(Lighting.GetColor(Utils.ToTileCoordinates(((Entity) this.Projectile).Center)));
      Color color = Color.op_Multiply(Color.White, (float) Main.mouseTextColor / (float) byte.MaxValue);
      Vector2 vector2_3 = ((Entity) this.Projectile).Center;
      int num1 = 1;
      int num2 = Main.projFrames[this.Projectile.type] - 1;
      for (int index = 1; index < 8; ++index)
      {
        int num3 = num1;
        if (index == 7)
          num3 = num2;
        Rectangle rectangle2 = Utils.Frame(texture2D1, 1, Main.projFrames[this.Projectile.type], 0, num3, 0, 0);
        Vector2 vector2_4 = Vector2.op_Addition(this.Projectile.oldPos[index * 10], Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
        float rotation = Utils.ToRotation(Vector2.op_Subtraction(vector2_3, vector2_4));
        Vector2 vector2_5 = Vector2.op_Subtraction(vector2_3, Utils.RotatedBy(new Vector2(16f, 0.0f), (double) rotation, Vector2.Zero));
        float num4 = Utils.ToRotation(Vector2.op_Subtraction(vector2_3, vector2_5)) + 1.57079637f;
        Vector2 vector2_6 = Vector2.op_Subtraction(vector2_5, Main.screenPosition);
        SpriteEffects spriteEffects2 = (double) vector2_5.X >= (double) vector2_3.X ? (SpriteEffects) 1 : (SpriteEffects) 0;
        vector2_3 = vector2_5;
        Main.EntitySpriteDraw(texture2D1, vector2_6, new Rectangle?(rectangle2), this.Projectile.GetAlpha(Lighting.GetColor(Utils.ToTileCoordinates(vector2_5))), num4, vector2_1, this.Projectile.scale, spriteEffects2, 0.0f);
        Main.EntitySpriteDraw(texture2D2, vector2_6, new Rectangle?(rectangle2), this.Projectile.GetAlpha(color), num4, vector2_1, this.Projectile.scale, spriteEffects2, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D1, vector2_2, new Rectangle?(rectangle1), alpha, this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects1, 0.0f);
      Main.EntitySpriteDraw(texture2D2, vector2_2, new Rectangle?(rectangle1), color, this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects1, 0.0f);
      return false;
    }
  }
}
