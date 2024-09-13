// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Nature.NatureFireball
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Will;
using FargowiltasSouls.Core.Systems;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Nature
{
  public class NatureFireball : WillFireball
  {
    public override string Texture => "Terraria/Images/Projectile_711";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.CooldownSlot = 1;
      this.Projectile.tileCollide = false;
    }

    public override void AI()
    {
      base.AI();
      if (this.Projectile.tileCollide || Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
        return;
      this.Projectile.tileCollide = true;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
        target.AddBuff(67, 300, true, false);
      target.AddBuff(24, 300, true, false);
    }
  }
}
