// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.RainbowSlimeSpikeFriendly
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class RainbowSlimeSpikeFriendly : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_605";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 6;
      ((Entity) this.Projectile).height = 6;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.timeLeft = 300;
    }

    public virtual void AI()
    {
      this.Projectile.alpha -= 50;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      if (this.Projectile.alpha == 0 && Utils.NextBool(Main.rand, 3))
      {
        int index = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 3f)), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 4, 0.0f, 0.0f, 50, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 150), 1.2f);
        Dust dust1 = Main.dust[index];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 0.3f);
        Dust dust2 = Main.dust[index];
        dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.3f));
        Main.dust[index].noGravity = true;
      }
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item17, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      }
      ((Entity) this.Projectile).velocity.Y += 0.15f;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(ModContent.BuffType<FlamesoftheUniverseBuff>(), 120, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 200));
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
