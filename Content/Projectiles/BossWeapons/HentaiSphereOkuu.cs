// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSphereOkuu
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HentaiSphereOkuu : MutantSphereRing
  {
    public override string Texture => "Terraria/Images/Projectile_454";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.penetrate = 2;
      this.Projectile.timeLeft = 240;
    }

    public override void AI()
    {
      base.AI();
      if (this.Projectile.timeLeft % this.Projectile.MaxUpdates == 0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Subtraction(((Entity) Main.player[this.Projectile.owner]).position, ((Entity) Main.player[this.Projectile.owner]).oldPosition));
      }
      if (this.Projectile.owner != Main.myPlayer || Main.player[this.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<HentaiSpearSpinBoundary>()] >= 1)
        return;
      this.Projectile.Kill();
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      this.Projectile.timeLeft = 0;
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, false);
    }
  }
}
