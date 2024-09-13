// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.PhantasmalDeathrayPungent
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class PhantasmalDeathrayPungent : BaseDeathray
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Deathrays/PhantasmalDeathrayWOF";
    }

    public PhantasmalDeathrayPungent()
      : base(120f)
    {
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.friendly = true;
      this.Projectile.hostile = false;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 6;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
    }

    public virtual void AI()
    {
      this.Projectile.hide = false;
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      int projectileByIdentity = FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, (int) this.Projectile.ai[0], new int[1]
      {
        ModContent.ProjectileType<PungentEyeballMinion>()
      });
      if (projectileByIdentity != -1)
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) Main.projectile[projectileByIdentity]).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, (double) Main.projectile[projectileByIdentity].rotation, new Vector2()), 20f));
      else if (this.Projectile.owner == Main.myPlayer && (double) this.Projectile.localAI[0] > 5.0)
      {
        this.Projectile.Kill();
        return;
      }
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        SoundStyle soundStyle;
        // ISSUE: explicit constructor call
        ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/Zombie_104", (SoundType) 0);
        ((SoundStyle) ref soundStyle).Volume = 0.5f;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      float num1 = (double) this.Projectile.ai[1] == 0.0 ? 0.4f : 1f;
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.scale = (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * 10f * num1;
        if ((double) this.Projectile.scale > (double) num1)
          this.Projectile.scale = num1;
        this.Projectile.rotation = Main.projectile[projectileByIdentity].rotation - 1.57079637f;
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(Main.projectile[projectileByIdentity].rotation);
        float length = 3f;
        float width = (float) ((Entity) this.Projectile).width;
        Vector2 center = ((Entity) this.Projectile).Center;
        if (nullable.HasValue)
          center = nullable.Value;
        float[] numArray = new float[(int) length];
        Collision.LaserScan(center, ((Entity) this.Projectile).velocity, width * this.Projectile.scale, 3000f, numArray);
        float num2 = 0.0f;
        for (int index = 0; index < numArray.Length; ++index)
          num2 += numArray[index];
        this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num2 / length, 0.5f);
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1] - 14f));
        for (int index1 = 0; index1 < 2; ++index1)
        {
          float num3 = Utils.ToRotation(((Entity) this.Projectile).velocity) + (float) ((Utils.NextBool(Main.rand, 2) ? -1.0 : 1.0) * 1.5707963705062866);
          float num4 = (float) (Main.rand.NextDouble() * 2.0 + 2.0);
          Vector2 vector2_2;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_2).\u002Ector((float) Math.Cos((double) num3) * num4, (float) Math.Sin((double) num3) * num4);
          int index2 = Dust.NewDust(vector2_1, 0, 0, 244, vector2_2.X, vector2_2.Y, 0, new Color(), 1f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].scale = 1.7f;
        }
        if (Utils.NextBool(Main.rand, 5))
        {
          Vector2 vector2_3 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.Projectile).velocity, 1.5707963705062866, new Vector2()), (float) Main.rand.NextDouble() - 0.5f), (float) ((Entity) this.Projectile).width);
          int index = Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(vector2_1, vector2_3), Vector2.op_Multiply(Vector2.One, 4f)), 8, 8, 244, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
          Main.dust[index].velocity.Y = -Math.Abs(Main.dust[index].velocity.Y);
        }
        DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        Utils.PlotTileLine(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1])), (float) ((Entity) this.Projectile).width * this.Projectile.scale, PhantasmalDeathrayPungent.\u003C\u003EO.\u003C0\u003E__CastLight ?? (PhantasmalDeathrayPungent.\u003C\u003EO.\u003C0\u003E__CastLight = new Utils.TileActionAttempt((object) null, __methodptr(CastLight))));
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(31, 300, false);
      if (target.boss)
        return;
      target.AddBuff(ModContent.BuffType<ClippedWingsBuff>(), 300, false);
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      if ((double) this.Projectile.ai[1] == 0.0)
        return;
      ((NPC.HitModifiers) ref modifiers).SetCrit();
    }
  }
}
