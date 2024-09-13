// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MechElectricOrbDestroyer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MechElectricOrbDestroyer : MechElectricOrb
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Masomode/MechElectricOrb";
    }

    public override void AI()
    {
      if ((double) this.Projectile.localAI[1] == 0.0)
        this.Projectile.localAI[1] = ((Vector2) ref ((Entity) this.Projectile).velocity).Length() / Math.Abs(this.Projectile.ai[1]);
      base.AI();
      if ((double) ++this.Projectile.ai[1] > 0.0)
      {
        if ((double) this.Projectile.ai[1] > 30.0)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.06f);
        }
        else if ((double) this.Projectile.ai[1] == 30.0)
        {
          ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(this.Projectile.ai[0]);
          this.Projectile.timeLeft = 180;
          this.Projectile.netUpdate = true;
        }
        else
          ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Utils.ToRotationVector2(this.Projectile.ai[0]), 0.1f);
      }
      else
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), ((Vector2) ref ((Entity) this.Projectile).velocity).Length() - this.Projectile.localAI[1]);
    }
  }
}
