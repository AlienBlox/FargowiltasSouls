// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.LunarCultistIceSpike
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class LunarCultistIceSpike : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 34;
      ((Entity) this.Projectile).height = 34;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.extraUpdates = 1;
      this.Projectile.timeLeft = 90;
      this.Projectile.penetrate = -1;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 10;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
      Mod mod;
      if (!Terraria.ModLoader.ModLoader.TryGetMod("Fargowiltas", ref mod))
        return;
      mod.Call(new object[2]
      {
        (object) "LowRenderProj",
        (object) this.Projectile
      });
    }

    public virtual void AI()
    {
      this.Projectile.alpha += this.Projectile.timeLeft > 20 ? -10 : 10;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      if (this.Projectile.alpha > (int) byte.MaxValue)
        this.Projectile.alpha = (int) byte.MaxValue;
      ((Entity) this.Projectile).position.X += this.Projectile.ai[0];
      ((Entity) this.Projectile).position.Y += this.Projectile.ai[1];
      int index = Dust.NewDust(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Utils.RandomVector2(Main.rand, -8f, 8f), 2f)), 8, 8, 197, 0.0f, 0.0f, 100, Color.Transparent, 1f);
      Main.dust[index].noGravity = true;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.3f, 0.75f, 0.9f);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(44, 240, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 100), this.Projectile.Opacity), 0.6f));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      Rectangle bounds = texture2D.Bounds;
      Vector2 vector2 = Vector2.op_Division(Utils.Size(bounds), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(bounds), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
