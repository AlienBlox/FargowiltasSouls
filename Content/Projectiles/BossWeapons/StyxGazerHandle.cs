// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.StyxGazerHandle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.AbomBoss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class StyxGazerHandle : BaseDeathray
  {
    public int counter;

    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/AbomDeathray";

    public StyxGazerHandle()
      : base(120f)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.extraUpdates = 1;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void AI()
    {
      this.Projectile.maxPenetrate = 1;
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      int projectileByIdentity = FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, (int) this.Projectile.ai[1], new int[2]
      {
        ModContent.ProjectileType<StyxGazer>(),
        ModContent.ProjectileType<StyxGazerArmor>()
      });
      if (projectileByIdentity != -1)
      {
        ((Entity) this.Projectile).Center = ((Entity) Main.projectile[projectileByIdentity]).Center;
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Multiply(((Entity) Main.projectile[projectileByIdentity]).velocity, 75f));
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) Main.projectile[projectileByIdentity]).velocity, (double) this.Projectile.ai[0], new Vector2());
      }
      else if (this.Projectile.owner == Main.myPlayer && (double) this.Projectile.localAI[0] > 5.0)
      {
        this.Projectile.Kill();
        return;
      }
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      float num1 = 1f;
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.scale = (float) (Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * (double) num1 * 6.0);
        if ((double) this.Projectile.scale > (double) num1)
          this.Projectile.scale = num1;
        float rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
        this.Projectile.rotation = rotation - 1.57079637f;
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(rotation);
        float length = 3f;
        int width = ((Entity) this.Projectile).width;
        Vector2 center = ((Entity) this.Projectile).Center;
        if (nullable.HasValue)
        {
          Vector2 vector2 = nullable.Value;
        }
        float[] numArray = new float[(int) length];
        for (int index = 0; index < numArray.Length; ++index)
          numArray[index] = 100f;
        float num2 = 0.0f;
        for (int index = 0; index < numArray.Length; ++index)
          num2 += numArray[index];
        this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num2 / length, 0.5f);
        if (!Utils.NextBool(Main.rand))
          return;
        int index1 = Dust.NewDust(Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, Utils.NextFloat(Main.rand, 100f))), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index1].noGravity = true;
        Dust dust = Main.dust[index1];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      }
    }

    public virtual bool? CanDamage()
    {
      this.Projectile.maxPenetrate = 1;
      return new bool?(true);
    }

    public virtual bool? CanHitNPC(NPC target)
    {
      return this.Projectile.localNPCImmunity[((Entity) target).whoAmI] >= 15 ? new bool?(false) : new bool?();
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      ++this.Projectile.localNPCImmunity[((Entity) target).whoAmI];
      if (this.Projectile.owner == Main.myPlayer)
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) target).Center, Utils.NextVector2Circular(Main.rand, 100f, 100f)), Vector2.Zero, ModContent.ProjectileType<AbomBlast>(), 0, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      target.AddBuff(153, 300, false);
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 300, false);
    }

    public float WidthFunction(float _)
    {
      return (float) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale * 2.0);
    }

    public static Color ColorFunction(float _) => new Color(253, 254, 32, 100);

    public override bool PreDraw(ref Color lightColor)
    {
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        return false;
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.WillBigDeathray");
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitY), this.Projectile.localAI[1]));
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitY), this.Projectile.localAI[1]));
      Vector2[] vector2Array = new Vector2[8];
      for (int index = 0; index < vector2Array.Length; ++index)
        vector2Array[index] = Vector2.Lerp(vector2_2, vector2_1, (float) index / ((float) vector2Array.Length - 1f));
      Color black = Color.Black;
      shader.TrySetParameter("mainColor", (object) black);
      ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/Trails/WillStreak", (AssetRequestMode) 2).Value.SetTexture1();
      for (int index1 = 0; index1 < 2; ++index1)
      {
        // ISSUE: method pointer
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        PrimitiveSettings primitiveSettings = new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), StyxGazerHandle.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (StyxGazerHandle.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), (PrimitiveSettings.VertexOffsetFunction) null, true, false, shader, new int?(), new int?(), false, new (Vector2, Vector2)?());
        PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, primitiveSettings, new int?(30));
        for (int index2 = 0; index2 < vector2Array.Length / 2; ++index2)
        {
          Vector2 vector2_3 = vector2Array[index2];
          int index3 = vector2Array.Length - 1 - index2;
          vector2Array[index2] = vector2Array[index3];
          vector2Array[index3] = vector2_3;
        }
        PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, primitiveSettings, new int?(30));
      }
      return false;
    }
  }
}
