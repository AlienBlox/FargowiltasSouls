// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Deathrays.DeerclopsDeathray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Deathrays
{
  public class DeerclopsDeathray : BaseDeathray
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Deathrays/PhantasmalDeathray";
    }

    public DeerclopsDeathray()
      : base(300f)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.MaxUpdates = 5;
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      this.Projectile.MaxUpdates *= 2;
    }

    public virtual void AI()
    {
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      this.maxTime = (float) (int) this.Projectile.ai[1];
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        SoundEngine.PlaySound(ref SoundID.DeerclopsScream, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        SoundEngine.PlaySound(ref SoundID.DeerclopsRubbleAttack, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      float num1 = 0.25f;
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.scale = (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * 5f * num1;
        if ((double) this.Projectile.scale > (double) num1)
          this.Projectile.scale = num1;
        float num2 = Utils.ToRotation(((Entity) this.Projectile).velocity) + this.Projectile.ai[0];
        this.Projectile.rotation = num2 - 1.57079637f;
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(num2);
        float length = 3f;
        float width = (float) ((Entity) this.Projectile).width;
        Vector2 center = ((Entity) this.Projectile).Center;
        if (nullable.HasValue)
          center = nullable.Value;
        float[] numArray = new float[(int) length];
        Collision.LaserScan(center, ((Entity) this.Projectile).velocity, width * this.Projectile.scale, 3000f, numArray);
        float num3 = 0.0f;
        for (int index = 0; index < numArray.Length; ++index)
          num3 += numArray[index];
        this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num3 / length, 0.5f);
        if ((double) Math.Abs(MathHelper.WrapAngle(1.57079637f - Utils.ToRotation(((Entity) this.Projectile).velocity))) > 0.78539818525314331)
        {
          Vector2 vector2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1]));
          for (int index1 = 0; index1 < 2; ++index1)
          {
            int index2 = Dust.NewDust(vector2, 0, 0, 90, 0.0f, 0.0f, 0, new Color(), 1f);
            Main.dust[index2].scale = 1f;
            Dust dust = Main.dust[index2];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
            Main.dust[index2].velocity.Y -= 6f;
          }
        }
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(44, 90, true, false);
      if (WorldSavingSystem.MasochistModeReal)
        target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 600, true, false);
      target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 1200, true, false);
    }
  }
}
