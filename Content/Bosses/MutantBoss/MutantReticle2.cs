// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantReticle2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantReticle2 : ModProjectile
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Masomode/TargetingReticle";
    }

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
      this.Projectile.timeLeft = 60;
      this.Projectile.extraUpdates = 1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()) && !Main.npc[EModeGlobalNPC.mutantBoss].dontTakeDamage)
      {
        int num = 60 - this.Projectile.timeLeft;
        this.Projectile.scale = (float) (4.0 - 0.05000000074505806 * (double) num);
        this.Projectile.rotation = 0.209439516f * (float) num;
      }
      else
        this.Projectile.Kill();
      if (this.Projectile.timeLeft % 15 == 0 && !Main.dedServ)
      {
        SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ReticleBeep", (SoundType) 0);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if (this.Projectile.timeLeft == 10 && !Main.dedServ)
      {
        SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ReticleLockOn", (SoundType) 0);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if (this.Projectile.timeLeft < 10)
      {
        this.Projectile.alpha += 25;
      }
      else
      {
        this.Projectile.alpha -= 4;
        if (this.Projectile.alpha >= 0)
          return;
        this.Projectile.alpha = 0;
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 128), (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue)));
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
