// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.SparklingLoveDeathray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Deathrays;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class SparklingLoveDeathray : BaseDeathray
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/DeviDeathray";

    public SparklingLoveDeathray()
      : base(600f)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.hide = true;
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      behindProjectiles.Add(index);
    }

    public virtual void AI()
    {
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      int projectileByIdentity = FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, (int) this.Projectile.ai[1], new int[1]
      {
        ModContent.ProjectileType<SparklingLove>()
      });
      if (projectileByIdentity != -1)
        ((Entity) this.Projectile).Center = ((Entity) Main.projectile[projectileByIdentity]).Center;
      else if (this.Projectile.owner == Main.myPlayer && (double) this.Projectile.localAI[0] > 5.0)
      {
        this.Projectile.Kill();
        return;
      }
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      if ((double) this.Projectile.localAI[0] == 0.0)
        SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      float num1 = 0.5f;
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.scale = (float) (Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * (double) num1 * 20.0);
        if ((double) this.Projectile.scale > (double) num1)
          this.Projectile.scale = num1;
        float num2 = Utils.ToRotation(((Entity) this.Projectile).velocity) + this.Projectile.ai[0];
        this.Projectile.rotation = num2 - 1.57079637f;
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(num2);
        float length = 3f;
        int width = ((Entity) this.Projectile).width;
        Vector2 center = ((Entity) this.Projectile).Center;
        if (nullable.HasValue)
        {
          Vector2 vector2 = nullable.Value;
        }
        float[] numArray = new float[(int) length];
        for (int index = 0; index < numArray.Length; ++index)
          numArray[index] = 1000f;
        float num3 = 0.0f;
        for (int index = 0; index < numArray.Length; ++index)
          num3 += numArray[index];
        this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num3 / length, 0.5f);
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(119, 300, false);
      target.immune[this.Projectile.owner] = 6;
    }

    public override Color? GetAlpha(Color lightColor)
    {
      Color? alpha = base.GetAlpha(lightColor);
      float num = 0.5f;
      return !alpha.HasValue ? new Color?() : new Color?(Color.op_Multiply(alpha.GetValueOrDefault(), num));
    }
  }
}
