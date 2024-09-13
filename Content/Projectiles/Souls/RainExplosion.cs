// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.RainExplosion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class RainExplosion : CobaltExplosion
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Souls/CobaltExplosion";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.friendly = true;
      this.Projectile.hostile = true;
      this.Projectile.DamageType = DamageClass.Default;
      this.Projectile.scale = 3f;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      return new bool?((double) ((Entity) this.Projectile).Distance(FargoSoulsUtil.ClosestPointInHitbox(targetHitbox, ((Entity) this.Projectile).Center)) < (double) projHitbox.Width * 0.89999997615814209 / 2.0);
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.SourceDamage;
      local = StatModifier.op_Multiply(local, 10f);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(144, 120, false);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(144, 120, true, false);
    }
  }
}
