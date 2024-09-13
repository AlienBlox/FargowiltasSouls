// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.SlimeBall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class SlimeBall : ModProjectile
  {
    private bool oil;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 14;
      this.Projectile.aiStyle = 14;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.timeLeft = 180;
      Mod mod;
      if (!Terraria.ModLoader.ModLoader.TryGetMod("Fargowiltas", ref mod))
        return;
      mod.Call(new object[2]
      {
        (object) "LowRenderProj",
        (object) this.Projectile
      });
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      if (!(source.Context == "SlimyShield"))
        return;
      this.oil = true;
    }

    public virtual void AI()
    {
      int index = Dust.NewDust(((Entity) this.Projectile).Center, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 59, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index].noGravity = true;
    }

    public virtual void OnKill(int timeleft)
    {
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).Center, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 59, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).Center, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 59, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(137, 240, false);
      if (!this.oil)
        return;
      target.AddBuff(204, 240, false);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(137, 240, true, false);
    }
  }
}
