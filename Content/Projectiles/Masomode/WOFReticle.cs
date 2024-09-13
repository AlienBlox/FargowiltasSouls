// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.WOFReticle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class WOFReticle : ModProjectile
  {
    private int additive = 130;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 110;
      ((Entity) this.Projectile).height = 110;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.hostile = true;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.timeLeft = 300;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
        this.Projectile.localAI[0] = Utils.NextBool(Main.rand) ? 1f : -1f;
      if ((double) ++this.Projectile.ai[0] < 130.0)
      {
        this.Projectile.alpha -= 2;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
        int num = Math.Min(110, (int) this.Projectile.ai[0]);
        this.Projectile.scale = (float) (4.0 - 3.0 / 110.0 * (double) num);
        this.Projectile.rotation = 0.114239737f * (float) num * this.Projectile.localAI[0];
        if ((double) this.Projectile.ai[0] % 30.0 != 0.0 || Main.dedServ)
          return;
        SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ReticleBeep", (SoundType) 0);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      else
      {
        this.additive -= 7;
        if (this.additive < 0)
          this.additive = 0;
        this.Projectile.alpha += 15;
        if (this.Projectile.alpha > (int) byte.MaxValue)
        {
          this.Projectile.alpha = (int) byte.MaxValue;
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.scale = (float) (4.0 - 3.0 * (double) this.Projectile.Opacity);
          if ((double) this.Projectile.ai[0] % 6.0 != 0.0 || (double) this.Projectile.localAI[1]++ >= 3.0)
            return;
          float radians = MathHelper.ToRadians(15f);
          Vector2 vector2_1;
          vector2_1.Y = ((double) this.Projectile.localAI[0] > 0.0 ? (float) (Main.maxTilesY * 16 - 16) : (float) (Main.maxTilesY * 16 - 3200)) - ((Entity) this.Projectile).Center.Y;
          vector2_1.X = vector2_1.Y * (float) Math.Tan((double) Utils.NextFloat(Main.rand, -radians, radians));
          vector2_1 = Vector2.op_Addition(vector2_1, ((Entity) this.Projectile).Center);
          Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(Utils.NextFloat(Main.rand, 0.8f, 1.2f), Vector2.op_Subtraction(((Entity) this.Projectile).Center, vector2_1)), 90f);
          if ((double) ((Vector2) ref vector2_2).Length() < 10.0)
            vector2_2 = Vector2.op_Multiply(Vector2.Normalize(vector2_2), Utils.NextFloat(Main.rand, 10f, 15f));
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), vector2_1, vector2_2, ModContent.ProjectileType<WOFChain>(), this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          FargoSoulsUtil.ScreenshakeRumble(4f);
          SoundStyle npcDeath13 = SoundID.NPCDeath13;
          ((SoundStyle) ref npcDeath13).Volume = 0.5f;
          SoundEngine.PlaySound(ref npcDeath13, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          this.Projectile.localAI[0] *= -1f;
        }
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, this.additive), this.Projectile.Opacity));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
