// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeBlaster
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeBlaster : ModProjectile
  {
    private float glowIntensity = 1f;

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 11;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 106;
      ((Entity) this.Projectile).height = 56;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.alpha = (int) byte.MaxValue;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      this.Projectile.rotation = this.Projectile.ai[0] + 1.57079637f;
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] >= 5.0 && ++this.Projectile.frameCounter >= 5)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          this.Projectile.frame = Main.projFrames[this.Projectile.type] - 1;
      }
      if ((double) this.Projectile.localAI[0] < 60.0)
        this.glowIntensity += 0.0333333351f;
      if ((double) this.Projectile.localAI[0] >= 60.0 && (double) this.Projectile.ai[1] <= 1.0 || (double) this.Projectile.localAI[0] >= 120.0 && (double) this.Projectile.ai[1] == 2.0)
      {
        this.glowIntensity = 4f;
        if ((double) this.Projectile.localAI[0] == 60.0 && (double) this.Projectile.ai[1] <= 1.0 || (double) this.Projectile.localAI[0] == 120.0 && (double) this.Projectile.ai[1] == 2.0)
        {
          SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          SoundEngine.PlaySound(ref SoundID.Item66, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          Vector2 rotationVector2 = Utils.ToRotationVector2(this.Projectile.ai[0]);
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, rotationVector2, ModContent.ProjectileType<LifeChalBlasterDeathray>(), this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      int num = (double) this.Projectile.ai[1] == 2.0 ? 155 : 95;
      if ((double) this.Projectile.localAI[0] >= (double) num)
        this.Projectile.Kill();
      if ((double) this.Projectile.localAI[0] > (double) (num - 15))
      {
        this.Projectile.alpha += 15;
        if (this.Projectile.alpha <= (int) byte.MaxValue)
          return;
        this.Projectile.alpha = (int) byte.MaxValue;
      }
      else
      {
        this.Projectile.alpha -= 15;
        if (this.Projectile.alpha >= 0)
          return;
        this.Projectile.alpha = 0;
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue - this.Projectile.alpha), this.Projectile.Opacity));
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<SmiteBuff>(), 180, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Lifelight/LifeBlasterGlow", (AssetRequestMode) 1).Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < 12; ++index)
      {
        Vector2 vector2_2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (6.2831854820251465 * (double) index / 12.0)), this.glowIntensity);
        Color white = Color.White;
        ((Color) ref white).A = (byte) 0;
        Main.spriteBatch.Draw(texture2D2, Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), vector2_2), new Rectangle?(rectangle), this.Projectile.GetAlpha(white), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
