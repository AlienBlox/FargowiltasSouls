// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Nature.NatureCloudMoving
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Nature
{
  public class NatureCloudMoving : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_237";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 28;
      ((Entity) this.Projectile).height = 28;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 40;
      this.Projectile.tileCollide = false;
      this.CooldownSlot = 1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.5f, 0.75f, 1f);
      this.Projectile.rotation += ((Entity) this.Projectile).velocity.X * 0.02f;
      if (++this.Projectile.frameCounter <= 4)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame <= 3)
        return;
      this.Projectile.frame = 0;
    }

    public virtual void OnKill(int timeLeft)
    {
      if (!FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<NatureCloudRaining>(), this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
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
