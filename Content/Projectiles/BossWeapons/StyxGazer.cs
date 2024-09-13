// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.StyxGazer
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
using Terraria.Audio;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class StyxGazer : AbomSpecialDeathray
  {
    public int counter;
    public bool spawnedHandle;

    public StyxGazer()
      : base(120)
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
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public override void AI()
    {
      base.AI();
      this.Projectile.maxPenetrate = 1;
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      if (((Entity) Main.player[this.Projectile.owner]).active && !Main.player[this.Projectile.owner].dead)
      {
        ((Entity) this.Projectile).Center = ((Entity) Main.player[this.Projectile.owner]).Center;
        if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
          ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
        if ((double) this.Projectile.localAI[0] == 0.0 && !Main.dedServ)
        {
          SoundStyle soundStyle;
          // ISSUE: explicit constructor call
          ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/StyxGazer", (SoundType) 0);
          ((SoundStyle) ref soundStyle).Volume = 1.5f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/RetinazerDeathray", (SoundType) 0);
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
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
          float num2 = Utils.ToRotation(((Entity) this.Projectile).velocity) + this.Projectile.ai[0];
          this.Projectile.rotation = num2 - 1.57079637f;
          ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(num2);
          float length = 3f;
          int width = ((Entity) this.Projectile).width;
          Vector2 center = ((Entity) this.Projectile).Center;
          if (nullable.HasValue)
          {
            Vector2 vector2_1 = nullable.Value;
          }
          float[] numArray = new float[(int) length];
          for (int index = 0; index < numArray.Length; ++index)
            numArray[index] = 1500f;
          float num3 = 0.0f;
          for (int index = 0; index < numArray.Length; ++index)
            num3 += numArray[index];
          this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num3 / length, 0.5f);
          Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1] - 14f));
          for (int index1 = 0; index1 < 2; ++index1)
          {
            float num4 = Utils.ToRotation(((Entity) this.Projectile).velocity) + (float) ((Utils.NextBool(Main.rand, 2) ? -1.0 : 1.0) * 1.5707963705062866);
            float num5 = (float) (Main.rand.NextDouble() * 2.0 + 2.0);
            Vector2 vector2_3;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2_3).\u002Ector((float) Math.Cos((double) num4) * num5, (float) Math.Sin((double) num4) * num5);
            int index2 = Dust.NewDust(vector2_2, 0, 0, 244, vector2_3.X, vector2_3.Y, 0, new Color(), 1f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].scale = 1.7f;
          }
          if (Utils.NextBool(Main.rand, 5))
          {
            Vector2 vector2_4 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.Projectile).velocity, 1.5707963705062866, new Vector2()), (float) Main.rand.NextDouble() - 0.5f), (float) ((Entity) this.Projectile).width);
            int index = Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, vector2_4), Vector2.op_Multiply(Vector2.One, 4f)), 8, 8, 244, 0.0f, 0.0f, 100, new Color(), 1.5f);
            Dust dust = Main.dust[index];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
            Main.dust[index].velocity.Y = -Math.Abs(Main.dust[index].velocity.Y);
          }
          if (--this.counter < 0)
          {
            this.counter = 8;
            if (this.Projectile.owner == Main.myPlayer)
            {
              Vector2 vector2_5 = ((Entity) this.Projectile).Center;
              Vector2 vector2_6 = Utils.RotatedBy(((Entity) this.Projectile).velocity, Math.PI / 2.0 * (double) Math.Sign(this.Projectile.ai[0]), new Vector2());
              for (int index = 1; index <= 8; ++index)
              {
                vector2_5 = Vector2.op_Addition(vector2_5, Vector2.op_Division(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 1500f), 8f));
                Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_5, vector2_6, ModContent.ProjectileType<StyxSickle>(), this.Projectile.damage, this.Projectile.knockBack / 10f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
              }
            }
          }
          if (!this.spawnedHandle)
          {
            this.spawnedHandle = true;
            if (this.Projectile.owner == Main.myPlayer)
            {
              Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<StyxGazerHandle>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 1.57079637f, (float) this.Projectile.identity, 0.0f);
              Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<StyxGazerHandle>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, -1.57079637f, (float) this.Projectile.identity, 0.0f);
            }
          }
          for (int index3 = 0; index3 < 10; ++index3)
          {
            int index4 = Dust.NewDust(Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, Utils.NextFloat(Main.rand, 1500f))), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, 0.0f, 0.0f, 0, new Color(), 1.5f);
            Main.dust[index4].noGravity = true;
            Dust dust = Main.dust[index4];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
          }
          int num6 = (double) ((Entity) this.Projectile).velocity.X < 0.0 ? -1 : 1;
          Main.player[this.Projectile.owner].heldProj = ((Entity) this.Projectile).whoAmI;
          Main.player[this.Projectile.owner].itemTime = 17;
          Main.player[this.Projectile.owner].itemAnimation = 17;
          Main.player[this.Projectile.owner].ChangeDir(num6);
          Main.player[this.Projectile.owner].itemRotation = (float) Math.Atan2((double) ((Entity) this.Projectile).velocity.Y * (double) ((Entity) this.Projectile).direction, (double) ((Entity) this.Projectile).velocity.X * (double) ((Entity) this.Projectile).direction);
        }
      }
      else
        this.Projectile.Kill();
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
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitY), (float) this.drawDistance));
      Vector2 center = ((Entity) this.Projectile).Center;
      Vector2[] vector2Array = new Vector2[8];
      for (int index = 0; index < vector2Array.Length; ++index)
        vector2Array[index] = Vector2.Lerp(center, vector2_1, (float) index / ((float) vector2Array.Length - 1f));
      Color black = Color.Black;
      shader.TrySetParameter("mainColor", (object) black);
      ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/Trails/WillStreak", (AssetRequestMode) 2).Value.SetTexture1();
      for (int index1 = 0; index1 < 2; ++index1)
      {
        // ISSUE: method pointer
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        PrimitiveSettings primitiveSettings = new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), StyxGazer.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (StyxGazer.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), (PrimitiveSettings.VertexOffsetFunction) null, true, false, shader, new int?(), new int?(), false, new (Vector2, Vector2)?());
        PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, primitiveSettings, new int?(30));
        for (int index2 = 0; index2 < vector2Array.Length / 2; ++index2)
        {
          Vector2 vector2_2 = vector2Array[index2];
          int index3 = vector2Array.Length - 1 - index2;
          vector2Array[index2] = vector2Array[index3];
          vector2Array[index3] = vector2_2;
        }
        PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, primitiveSettings, new int?(30));
      }
      return false;
    }
  }
}
