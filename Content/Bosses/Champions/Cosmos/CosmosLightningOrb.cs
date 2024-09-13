// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Cosmos.CosmosLightningOrb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Terra;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Cosmos
{
  public class CosmosLightningOrb : TerraLightningOrb
  {
    public override string Texture => "Terraria/Images/Projectile_465";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft = 600;
    }

    public override bool? CanDamage() => new bool?(this.Projectile.alpha == 0);

    public override void AI()
    {
      if (this.Projectile.timeLeft > 30)
      {
        this.Projectile.alpha -= 10;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
      }
      else
      {
        this.Projectile.alpha += 10;
        if (this.Projectile.alpha > (int) byte.MaxValue)
        {
          this.Projectile.alpha = (int) byte.MaxValue;
          this.Projectile.Kill();
        }
      }
      if ((double) ++this.Projectile.localAI[1] > 120.0 && (double) this.Projectile.localAI[1] < 240.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.05f);
      }
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.4f, 0.85f, 0.9f);
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter <= 3)
        return;
      this.Projectile.frameCounter = 0;
      ++this.Projectile.frame;
      if (this.Projectile.frame <= 3)
        return;
      this.Projectile.frame = 0;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(144, 360, true, false);
    }
  }
}
