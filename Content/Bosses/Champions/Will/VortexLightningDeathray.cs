// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Will.VortexLightningDeathray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Will
{
  public class VortexLightningDeathray : BaseDeathray
  {
    public virtual string Texture => "FargowiltasSouls/Content/Bosses/Champions/Will/WillDeathray";

    public VortexLightningDeathray()
      : base(15f, hitboxModifier: 4.5f)
    {
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 5;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.scale = 4.5f;
    }

    public virtual void AI()
    {
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.UnitY;
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.UnitY;
      float num1 = 1f;
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
        float length = 3f;
        int width = ((Entity) this.Projectile).width;
        Vector2 center = ((Entity) this.Projectile).Center;
        if (nullable.HasValue)
        {
          Vector2 vector2_1 = nullable.Value;
        }
        float[] numArray = new float[(int) length];
        for (int index = 0; index < numArray.Length; ++index)
          numArray[index] = 3000f;
        float num2 = 0.0f;
        for (int index = 0; index < numArray.Length; ++index)
          num2 += numArray[index];
        this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num2 / length, 0.5f);
        Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1] - 14f));
        for (int index1 = 0; index1 < 2; ++index1)
        {
          float num3 = Utils.ToRotation(((Entity) this.Projectile).velocity) + (float) ((Utils.NextBool(Main.rand, 2) ? -1.0 : 1.0) * 1.5707963705062866);
          float num4 = (float) (Main.rand.NextDouble() * 2.0 + 2.0);
          Vector2 vector2_3;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_3).\u002Ector((float) Math.Cos((double) num3) * num4, (float) Math.Sin((double) num3) * num4);
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
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
        if (++this.Projectile.frameCounter <= 2)
          return;
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame < Main.projFrames[this.Projectile.type])
          return;
        this.Projectile.frame = 0;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      int num = WorldSavingSystem.EternityMode ? 1 : 0;
      target.AddBuff(144, 300, true, false);
    }

    public float WidthFunction(float _)
    {
      return (float) ((Entity) this.Projectile).width * this.Projectile.scale;
    }

    public static Color ColorFunction(float _)
    {
      Color cyan = Color.Cyan;
      ((Color) ref cyan).A = (byte) 100;
      return cyan;
    }

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
      Color white = Color.White;
      ((Color) ref white).A = (byte) 100;
      Color color = white;
      shader.TrySetParameter("mainColor", (object) color);
      ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/Trails/WillStreak", (AssetRequestMode) 2).Value.SetTexture1();
      // ISSUE: method pointer
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), VortexLightningDeathray.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (VortexLightningDeathray.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), (PrimitiveSettings.VertexOffsetFunction) null, true, false, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(30));
      return false;
    }
  }
}
