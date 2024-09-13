// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Terra.TerraLightningOrbDeathray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Terra
{
  public class TerraLightningOrbDeathray : BaseDeathray
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Deathrays/TerraLightningOrbDeathray";
    }

    public TerraLightningOrbDeathray()
      : base(1000f, 0.8f)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      Projectile projectile = FargoSoulsUtil.ProjectileExists(this.Projectile.ai[1], ModContent.ProjectileType<TerraLightningOrb2>());
      if (projectile == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        ((Entity) this.Projectile).Center = ((Entity) projectile).Center;
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.ai[0] + (double) projectile.rotation, new Vector2());
        ++this.Projectile.localAI[0];
        if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
        {
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.scale = (float) (0.40000000596046448 + Math.Sin((double) this.Projectile.localAI[0] / 4.0) * 0.15000000596046448);
          float rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          this.Projectile.rotation = rotation - 1.57079637f;
          ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(rotation);
          float length = 3f;
          float[] numArray = new float[(int) length];
          float width = (float) ((Entity) this.Projectile).width;
          Vector2 center = ((Entity) this.Projectile).Center;
          if (nullable.HasValue)
            center = nullable.Value;
          Collision.LaserScan(center, ((Entity) this.Projectile).velocity, width * this.Projectile.scale, 2400f, numArray);
          float num = 0.0f;
          for (int index = 0; index < numArray.Length; ++index)
            num += numArray[index];
          this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num / length, 0.5f);
          Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1] - 14f));
        }
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(44, 300, true, false);
      target.AddBuff(24, 300, true, false);
      target.AddBuff(ModContent.BuffType<InfestedBuff>(), 300, true, false);
    }
  }
}
