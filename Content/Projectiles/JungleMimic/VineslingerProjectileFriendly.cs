// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.JungleMimic.VineslingerProjectileFriendly
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.JungleMimic
{
  public class VineslingerProjectileFriendly : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 18;
      ((Entity) this.Projectile).height = 18;
      this.Projectile.friendly = true;
      this.Projectile.hostile = false;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.ignoreWater = false;
      this.Projectile.tileCollide = false;
      this.AIType = 88;
      this.Projectile.timeLeft = 120;
    }

    public virtual void OnKill(int timeLeft)
    {
      Collision.HitTiles(Vector2.op_Addition(((Entity) this.Projectile).position, ((Entity) this.Projectile).velocity), ((Entity) this.Projectile).velocity, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height);
      SoundEngine.PlaySound(ref SoundID.Grass, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index = 0; index < 35; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 39, ((Entity) this.Projectile).velocity.X * 0.25f, ((Entity) this.Projectile).velocity.Y * 0.25f, 150, new Color(), 0.7f);
    }

    public virtual bool? CanDamage() => new bool?((double) this.Projectile.ai[0] >= 20.0);

    public virtual void AI()
    {
      if (Utils.NextBool(Main.rand, 3))
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 3, ((Entity) this.Projectile).velocity.X * 0.25f, ((Entity) this.Projectile).velocity.Y * 0.25f, 150, new Color(), 0.7f);
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + MathHelper.ToRadians(90f);
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      if (++this.Projectile.frameCounter >= 5)
      {
        this.Projectile.frameCounter = 0;
        this.Projectile.frame = ++this.Projectile.frame % Main.projFrames[this.Projectile.type];
      }
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] < 20.0)
        return;
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        VineslingerProjectileFriendly.AdjustMagnitude(ref ((Entity) this.Projectile).velocity);
        this.Projectile.localAI[0] = 1f;
      }
      Vector2 vector = Vector2.Zero;
      float num1 = 400f;
      bool flag = false;
      for (int index = 0; index < 200; ++index)
      {
        if (((Entity) Main.npc[index]).active && !Main.npc[index].dontTakeDamage && !Main.npc[index].friendly && Main.npc[index].lifeMax > 5)
        {
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.npc[index]).Center, ((Entity) this.Projectile).Center);
          float num2 = (float) Math.Sqrt((double) vector2.X * (double) vector2.X + (double) vector2.Y * (double) vector2.Y);
          if ((double) num2 < (double) num1)
          {
            vector = vector2;
            num1 = num2;
            flag = true;
          }
        }
      }
      if (!flag)
        return;
      VineslingerProjectileFriendly.AdjustMagnitude(ref vector);
      ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(10f, ((Entity) this.Projectile).velocity), vector), 10f);
      VineslingerProjectileFriendly.AdjustMagnitude(ref ((Entity) this.Projectile).velocity);
    }

    private static void AdjustMagnitude(ref Vector2 vector)
    {
      float num = (float) Math.Sqrt((double) vector.X * (double) vector.X + (double) vector.Y * (double) vector.Y);
      if ((double) num <= 18.0)
        return;
      vector = Vector2.op_Multiply(vector, 18f / num);
    }
  }
}
