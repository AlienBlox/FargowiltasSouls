// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.LunarCultistIceMist
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
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class LunarCultistIceMist : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_464";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 60;
      ((Entity) this.Projectile).height = 60;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.extraUpdates = 1;
      this.Projectile.timeLeft = 180;
      this.Projectile.penetrate = -1;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
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
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item120, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      }
      this.Projectile.alpha += this.Projectile.timeLeft > 20 ? -10 : 10;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      if (this.Projectile.alpha > (int) byte.MaxValue)
        this.Projectile.alpha = (int) byte.MaxValue;
      if (this.Projectile.timeLeft % 60 == 0)
      {
        SoundEngine.PlaySound(ref SoundID.Item120, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
        Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.rotation, new Vector2()), 12f);
        for (int index = 0; index < 6; ++index)
        {
          vector2 = Utils.RotatedBy(vector2, 1.0471975803375244, new Vector2());
          if (this.Projectile.owner == Main.myPlayer)
            Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2, ModContent.ProjectileType<LunarCultistIceSpike>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, ((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y, 0.0f);
        }
      }
      this.Projectile.rotation += (float) Math.PI / 40f;
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
