// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MechElectricOrbTwins
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using System;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MechElectricOrbTwins : MechElectricOrb
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Masomode/MechElectricOrb";
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft = 180;
    }

    public override void AI()
    {
      base.AI();
      if ((double) ++this.Projectile.ai[1] < 75.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.06f);
      }
      Player player = FargoSoulsUtil.PlayerExists(this.Projectile.ai[0]);
      if (player == null)
        return;
      float rotation1 = Utils.ToRotation(((Entity) this.Projectile).velocity);
      float rotation2 = Utils.ToRotation(Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.Projectile).Center));
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.retiBoss, 125) || (double) Math.Abs(MathHelper.WrapAngle(rotation2 - rotation1)) >= 1.5707963705062866)
        return;
      float num1 = (float) (((double) ((Entity) Main.npc[EModeGlobalNPC.retiBoss]).Distance(((Entity) player).Center) - 600.0) / 1200.0);
      float num2 = num1 * num1;
      if ((double) num2 < 0.0)
        num2 = 0.0f;
      if ((double) num2 > 1.0)
        num2 = 1f;
      float num3 = 0.5f * num2;
      ((Entity) this.Projectile).velocity = Utils.RotatedBy(new Vector2(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), 0.0f), (double) Utils.AngleLerp(rotation1, rotation2, num3), new Vector2());
    }
  }
}
