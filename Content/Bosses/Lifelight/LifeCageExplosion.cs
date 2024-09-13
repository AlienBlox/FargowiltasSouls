// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeCageExplosion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeCageExplosion : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Assets/ExtraTextures/LifelightParts/Rune1";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 184;
      ((Entity) this.Projectile).height = 184;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 2;
      this.Projectile.hide = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      return new bool?((double) ((Entity) this.Projectile).Distance(FargoSoulsUtil.ClosestPointInHitbox(targetHitbox, ((Entity) this.Projectile).Center)) < (double) (projHitbox.Width / 2));
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] != 0.0)
        return;
      this.Projectile.localAI[0] = 1f;
      SoundEngine.PlaySound(ref SoundID.Item41, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 30; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 297, 0.0f, 0.0f, 0, new Color(), 4f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 9f);
      }
      for (int index3 = 0; index3 < 4; ++index3)
      {
        for (int index4 = 0; index4 < 30; ++index4)
        {
          int index5 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 297, 0.0f, 0.0f, 0, new Color(), 2.5f);
          Main.dust[index5].noGravity = true;
          Dust dust = Main.dust[index5];
          dust.velocity = Vector2.op_Addition(dust.velocity, Vector2.op_Multiply(Utils.NextFloat(Main.rand, 32f), Utils.RotatedBy(Vector2.UnitX, 1.5707963705062866 * (double) index3, new Vector2())));
        }
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<SmiteBuff>(), 180, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor) => false;
  }
}
