// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantReticle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantReticle : ModProjectile
  {
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
      this.Projectile.timeLeft = WorldSavingSystem.MasochistModeReal ? 120 : 150;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()) && !Main.npc[EModeGlobalNPC.mutantBoss].dontTakeDamage)
      {
        if ((double) this.Projectile.localAI[0] == 0.0)
        {
          this.Projectile.localAI[0] = Utils.NextBool(Main.rand) ? -1f : 1f;
          this.Projectile.rotation = Utils.NextFloat(Main.rand, 6.28318548f);
        }
        this.Projectile.scale = (float) (1.5 - 0.0083333337679505348 * (double) Math.Min(60, 90 - this.Projectile.timeLeft));
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        this.Projectile.rotation += MathHelper.ToRadians(6f) * this.Projectile.localAI[0];
      }
      else
        this.Projectile.Kill();
      if (this.Projectile.timeLeft < 15)
      {
        this.Projectile.alpha += 17;
      }
      else
      {
        this.Projectile.alpha -= 4;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
        if (this.Projectile.timeLeft % 15 != 0 || Main.dedServ)
          return;
        SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ReticleBeep", (SoundType) 0);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 128), (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue)));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = (double) this.Projectile.ai[2] == 0.0 ? TextureAssets.Projectile[this.Projectile.type].Value : ModContent.Request<Texture2D>(this.Texture + "_Queen", (AssetRequestMode) 1).Value;
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
