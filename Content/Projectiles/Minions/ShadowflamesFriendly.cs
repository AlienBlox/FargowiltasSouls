// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.ShadowflamesFriendly
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class ShadowflamesFriendly : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_299";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(299);
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.hostile = false;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.timeLeft = 180 * (this.Projectile.extraUpdates + 1);
      this.Projectile.tileCollide = true;
      this.Projectile.penetrate = 2;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 10;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 10; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 181, 0.0f, 0.0f, 100, new Color(), 1f);
          Dust dust1 = Main.dust[index2];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 3f);
          Dust dust2 = Main.dust[index2];
          dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.5f));
          Main.dust[index2].noGravity = true;
        }
      }
      for (int index3 = 0; index3 < 2; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 181, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 1f);
        Dust dust = Main.dust[index4];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.6f);
        Main.dust[index4].noGravity = true;
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 30; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 181, 0.0f, 0.0f, 100, new Color(), 1f);
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 3f);
        Dust dust2 = Main.dust[index2];
        dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.op_UnaryNegation(((Entity) this.Projectile).velocity), 0.75f));
        Main.dust[index2].scale *= 1.2f;
        Main.dust[index2].noGravity = true;
      }
    }
  }
}
