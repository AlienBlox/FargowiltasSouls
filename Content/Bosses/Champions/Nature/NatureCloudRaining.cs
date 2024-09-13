// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Nature.NatureCloudRaining
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Nature
{
  public class NatureCloudRaining : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_238";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 6;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 54;
      ((Entity) this.Projectile).height = 28;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 300;
      this.Projectile.tileCollide = false;
      this.Projectile.scale = 1.5f;
      this.CooldownSlot = 1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.5f, 0.75f, 1f);
      if ((double) ++this.Projectile.ai[0] > 8.0)
      {
        this.Projectile.ai[0] = 0.0f;
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).position.X + 14f + (float) Main.rand.Next(((Entity) this.Projectile).width - 28), (float) ((double) ((Entity) this.Projectile).position.Y + (double) ((Entity) this.Projectile).height + 4.0), 0.0f, 5f, ModContent.ProjectileType<NatureRain>(), this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      if ((double) ++this.Projectile.ai[1] > 600.0)
      {
        this.Projectile.alpha += 5;
        if (this.Projectile.alpha > (int) byte.MaxValue)
        {
          this.Projectile.alpha = (int) byte.MaxValue;
          this.Projectile.Kill();
        }
      }
      if (++this.Projectile.frameCounter <= 8)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame <= 5)
        return;
      this.Projectile.frame = 0;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(103, 300, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(44, 300, true, false);
    }
  }
}
