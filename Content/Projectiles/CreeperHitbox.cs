// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.CreeperHitbox
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class CreeperHitbox : ModProjectile
  {
    public virtual string Texture => FargoSoulsUtil.EmptyTexture;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 60;
      ((Entity) this.Projectile).height = 60;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.extraUpdates = 1;
      this.Projectile.hide = true;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 0;
    }

    public virtual bool? CanDamage()
    {
      this.Projectile.maxPenetrate = 1;
      return new bool?(true);
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      ((NPC.HitModifiers) ref modifiers).HitDirectionOverride = new int?((double) ((Entity) Main.player[this.Projectile.owner]).Center.X > (double) ((Entity) target).Center.X ? -1 : 1);
      if ((double) this.Projectile.ai[0] == 0.0)
        target.AddBuff(69, 600, false);
      else
        target.AddBuff(ModContent.BuffType<SublimationBuff>(), 600, false);
    }
  }
}
