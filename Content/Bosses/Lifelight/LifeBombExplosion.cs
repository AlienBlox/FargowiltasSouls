// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeBombExplosion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeBombExplosion : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 1;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 50;
      ((Entity) this.Projectile).height = 50;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 3600;
      if (!Main.getGoodWorld)
        return;
      this.Projectile.timeLeft *= 10;
    }

    public virtual bool? CanDamage() => new bool?(this.Projectile.alpha < 100);

    public static int MaxTime => !Main.getGoodWorld ? 2400 : 24000;

    public virtual void AI()
    {
      this.Projectile.rotation += 2f;
      if (Utils.NextBool(Main.rand, 6))
      {
        int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index].noGravity = true;
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
      }
      if ((double) this.Projectile.localAI[0] == 0.0)
        this.Projectile.localAI[0] += (float) Main.rand.Next(60);
      this.Projectile.scale = (float) (1.1000000238418579 + 0.10000000149011612 * Math.Sin(0.41887903213500977 * (double) ++this.Projectile.localAI[1]));
      if ((double) this.Projectile.ai[0] > (double) (LifeBombExplosion.MaxTime - 30))
      {
        this.Projectile.alpha += 8;
        if (this.Projectile.alpha > (int) byte.MaxValue)
          this.Projectile.alpha = (int) byte.MaxValue;
      }
      if ((double) this.Projectile.ai[0] > (double) LifeBombExplosion.MaxTime || NPC.CountNPCS(ModContent.NPCType<LifeChallenger>()) < 1)
      {
        for (int index1 = 0; index1 < 20; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, 0.0f, 0.0f, 0, new Color(), 1f);
          Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
        }
        this.Projectile.Kill();
      }
      ++this.Projectile.ai[0];
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<SmiteBuff>(), 180, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 610 - (int) Main.mouseTextColor * 2), this.Projectile.Opacity), 0.9f));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY));
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      Vector2.op_Division(Utils.Size(new Rectangle(0, num1 * this.Projectile.frame, texture2D1.Width, num1)), 2f);
      float num2 = this.Projectile.scale * 1.5f;
      float num3 = 1f;
      Texture2D texture2D2 = FargosTextureRegistry.BloomParticleTexture.Value;
      SpriteBatch spriteBatch1 = Main.spriteBatch;
      Texture2D texture2D3 = texture2D2;
      Vector2 vector2_2 = vector2_1;
      Rectangle? nullable1 = new Rectangle?();
      Color darkGoldenrod = Color.DarkGoldenrod;
      ((Color) ref darkGoldenrod).A = (byte) 0;
      Color color1 = Color.op_Multiply(darkGoldenrod, num3);
      double rotation1 = (double) this.Projectile.rotation;
      Vector2 vector2_3 = Vector2.op_Multiply(Utils.Size(texture2D2), 0.5f);
      double num4 = (double) num2;
      spriteBatch1.Draw(texture2D3, vector2_2, nullable1, color1, (float) rotation1, vector2_3, (float) num4, (SpriteEffects) 0, 0.0f);
      SpriteBatch spriteBatch2 = Main.spriteBatch;
      Texture2D texture2D4 = texture2D2;
      Vector2 vector2_4 = vector2_1;
      Rectangle? nullable2 = new Rectangle?();
      Color gold = Color.Gold;
      ((Color) ref gold).A = (byte) 0;
      Color color2 = Color.op_Multiply(Color.op_Multiply(gold, 0.4f), num3);
      double rotation2 = (double) this.Projectile.rotation;
      Vector2 vector2_5 = Vector2.op_Multiply(Utils.Size(texture2D2), 0.5f);
      double num5 = (double) num2 * 0.6600000262260437;
      spriteBatch2.Draw(texture2D4, vector2_4, nullable2, color2, (float) rotation2, vector2_5, (float) num5, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
