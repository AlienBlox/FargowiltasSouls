// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.BloodScytheFriendly
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class BloodScytheFriendly : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(45);
      this.AIType = 45;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.penetrate = 2;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 10;
      this.Projectile.FargoSouls().CanSplit = false;
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
  }
}
