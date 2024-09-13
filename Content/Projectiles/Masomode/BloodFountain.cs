// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.BloodFountain
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class BloodFountain : ModProjectile
  {
    public virtual string Texture => FargoSoulsUtil.EmptyTexture;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.aiStyle = -1;
      this.Projectile.timeLeft = 60;
      this.Projectile.penetrate = -1;
      this.Projectile.hide = true;
    }

    public virtual void AI()
    {
      if ((double) ++this.Projectile.localAI[0] == 4.0 && (double) --this.Projectile.ai[0] > 0.0 && FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(16f, Vector2.UnitY)), Vector2.Zero, this.Projectile.type, this.Projectile.damage, 0.0f, Main.myPlayer, this.Projectile.ai[0], 0.0f, 0.0f);
      if (!Utils.NextBool(Main.rand, 10))
        return;
      int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, ModContent.DustType<BloodDust>(), (float) Main.rand.Next(-50, 50) * 1f, -3f, 0, new Color(), 1f);
      Main.dust[index].scale = 2f;
      Main.dust[index].noGravity = true;
      Dust dust1 = Main.dust[index];
      dust1.velocity = Vector2.op_Multiply(dust1.velocity, 0.1f);
      Dust dust2 = Main.dust[index];
      dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.1f));
    }
  }
}
