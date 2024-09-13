// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.SlimeSpikeFriendly
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class SlimeSpikeFriendly : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_605";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.DamageType = DamageClass.Melee;
      ((Entity) this.Projectile).height = 6;
      ((Entity) this.Projectile).width = 6;
      this.Projectile.aiStyle = 1;
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.timeLeft = 30;
      this.Projectile.penetrate = 2;
    }

    public virtual void AI()
    {
      if (this.Projectile.alpha == 0 && Utils.NextBool(Main.rand, 3))
      {
        int index = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 3f)), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 4, 0.0f, 0.0f, 50, new Color(78, 136, (int) byte.MaxValue, 150), 1.2f);
        Dust dust1 = Main.dust[index];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 0.3f);
        Dust dust2 = Main.dust[index];
        dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.3f));
        Main.dust[index].noGravity = true;
      }
      this.Projectile.alpha -= 50;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      if ((double) this.Projectile.ai[1] != 0.0)
        return;
      this.Projectile.ai[1] = 1f;
      SoundEngine.PlaySound(ref SoundID.Item17, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(137, 150, false);
      this.Projectile.timeLeft = 0;
    }
  }
}
