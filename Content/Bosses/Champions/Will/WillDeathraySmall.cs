// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Will.WillDeathraySmall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Will
{
  public class WillDeathraySmall : BaseDeathray
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/AbomDeathray";

    public WillDeathraySmall()
      : base(60f, drawDistance: 3600)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<WillChampion>());
      if (npc != null && ((double) npc.ai[0] == 2.0 && (double) npc.ai[1] < 30.0 || (double) npc.ai[0] == -1.0 && (double) npc.ai[1] < 10.0))
      {
        this.Projectile.Kill();
      }
      else
      {
        if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
          ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
        float num1 = 0.2f;
        ++this.Projectile.localAI[0];
        if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
        {
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.scale = (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * 2.5f * num1;
          if ((double) this.Projectile.scale > (double) num1)
            this.Projectile.scale = num1;
          float num2 = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
          this.Projectile.rotation = num2;
          ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(num2 + 1.57079637f);
          float length = 3f;
          int width = ((Entity) this.Projectile).width;
          Vector2 center = ((Entity) this.Projectile).Center;
          if (nullable.HasValue)
          {
            Vector2 vector2_1 = nullable.Value;
          }
          float[] numArray = new float[(int) length];
          for (int index = 0; index < numArray.Length; ++index)
            numArray[index] = 4000f;
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
          ((Entity) this.Projectile).position.X = this.Projectile.ai[0];
          ((Entity) this.Projectile).position.X += (float) ((double) Utils.NextFloat(Main.rand, -1f, 1f) * (double) Utils.NextFloat(Main.rand, 160f) * (1.0 - (double) this.Projectile.localAI[0] / (double) this.maxTime));
          Projectile projectile = this.Projectile;
          ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
        }
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      if ((double) this.Projectile.ai[2] == 0.0)
      {
        if (!FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, ModContent.ProjectileType<WillDeathrayBig>(), this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, this.Projectile.ai[1], 0.0f);
      }
      else
      {
        SoundStyle zombie125 = SoundID.Zombie125;
        ((SoundStyle) ref zombie125).MaxInstances = 0;
        ((SoundStyle) ref zombie125).SoundLimitBehavior = (SoundLimitBehavior) 1;
        ((SoundStyle) ref zombie125).Volume = 1.5f;
        SoundEngine.PlaySound(ref zombie125, new Vector2?(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.UnitY, 2000f))), (SoundUpdateCallback) null);
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300, true, false);
        target.AddBuff(ModContent.BuffType<MidasBuff>(), 300, true, false);
      }
      target.AddBuff(30, 300, true, false);
    }

    public float WidthFunction(float _)
    {
      return (float) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale * 3.0);
    }

    public static Color ColorFunction(float _) => new Color(253, 254, 32, 100);

    public override bool PreDraw(ref Color lightColor)
    {
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        return false;
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.WillDeathray");
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitY), (float) this.drawDistance));
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 150f));
      Vector2[] vector2Array = new Vector2[8];
      for (int index = 0; index < vector2Array.Length; ++index)
        vector2Array[index] = Vector2.Lerp(vector2_2, vector2_1, (float) index / ((float) vector2Array.Length - 1f));
      Color color;
      // ISSUE: explicit constructor call
      ((Color) ref color).\u002Ector(252, 252, 192, 100);
      shader.TrySetParameter("mainColor", (object) color);
      ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/Trails/WillStreak", (AssetRequestMode) 2).Value.SetTexture1();
      // ISSUE: method pointer
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), WillDeathraySmall.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (WillDeathraySmall.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), (PrimitiveSettings.VertexOffsetFunction) null, true, false, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(30));
      return false;
    }
  }
}
