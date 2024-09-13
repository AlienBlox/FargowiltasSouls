// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.GiantDeathray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core;
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
namespace FargowiltasSouls.Content.Projectiles
{
  public class GiantDeathray : MutantSpecialDeathray
  {
    public GiantDeathray()
      : base(180)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 0;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.hide = true;
      this.CooldownSlot = -1;
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

    public override void AI()
    {
      base.AI();
      if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
        FargoSoulsUtil.ScreenshakeRumble(6f);
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      ((Entity) this.Projectile).Center = ((Entity) Main.player[this.Projectile.owner]).Center;
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      if ((double) this.Projectile.localAI[0] == 0.0 && !Main.dedServ)
      {
        SoundStyle soundStyle;
        // ISSUE: explicit constructor call
        ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/DeviBigDeathray", (SoundType) 0);
        ((SoundStyle) ref soundStyle).Volume = 1.5f;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        // ISSUE: explicit constructor call
        ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/FinalSpark", (SoundType) 0);
        ((SoundStyle) ref soundStyle).Volume = 1.5f;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      float num1 = 10f;
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.scale = (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * 3f * num1;
        if ((double) this.Projectile.scale > (double) num1)
          this.Projectile.scale = num1;
        float rotation1 = Utils.ToRotation(((Entity) this.Projectile).velocity);
        double rotation2 = (double) this.Projectile.rotation;
        this.Projectile.rotation = rotation1 - 1.57079637f;
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(rotation1);
        float length = 3f;
        int width = ((Entity) this.Projectile).width;
        Vector2 center = ((Entity) this.Projectile).Center;
        if (nullable.HasValue)
        {
          Vector2 vector2 = nullable.Value;
        }
        float[] numArray = new float[(int) length];
        for (int index = 0; index < numArray.Length; ++index)
          numArray[index] = 3000f;
        float num2 = 0.0f;
        for (int index = 0; index < numArray.Length; ++index)
          num2 += numArray[index];
        this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num2 / length, 0.5f);
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, false);
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 300, false);
    }

    public override void PostAI()
    {
      base.PostAI();
      this.Projectile.hide = true;
      if (Main.dedServ)
        return;
      ShaderManager.GetFilter("FargowiltasSouls.FinalSpark").Activate();
      if (!SoulConfig.Instance.ForcedFilters || Main.WaveQuality != 0)
        return;
      Main.WaveQuality = 1;
    }

    public virtual void OnKill(int timeLeft) => base.OnKill(timeLeft);

    public bool BeBrighter => (double) this.Projectile.ai[0] > 0.0;

    public float WidthFunction(float trailInterpolant)
    {
      return this.Projectile.scale * (float) ((Entity) this.Projectile).width;
    }

    public static Color ColorFunction(float trailInterpolant)
    {
      return Color.Lerp(new Color(31, 187, 192, 100), new Color(51, (int) byte.MaxValue, 191, 100), trailInterpolant);
    }

    public override bool PreDraw(ref Color lightColor)
    {
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        return false;
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.MutantDeathray");
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitY), (float) this.drawDistance));
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 400f));
      Vector2[] vector2Array = new Vector2[8];
      for (int index = 0; index < vector2Array.Length; ++index)
        vector2Array[index] = Vector2.Lerp(vector2_2, vector2_1, (float) index / ((float) vector2Array.Length - 1f));
      Color color;
      // ISSUE: explicit constructor call
      ((Color) ref color).\u002Ector(194, (int) byte.MaxValue, 242, 100);
      shader.TrySetParameter("mainColor", (object) color);
      FargosTextureRegistry.MutantStreak.Value.SetTexture1();
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/GlowRing", (AssetRequestMode) 2).Value;
      Vector2 center = ((Entity) this.Projectile).Center;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(center, Main.screenPosition), new Rectangle?(), color, this.Projectile.rotation, Vector2.op_Multiply(Utils.Size(texture2D), 0.5f), this.Projectile.scale * 0.4f, (SpriteEffects) 0, 0.0f);
      // ISSUE: method pointer
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), GiantDeathray.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (GiantDeathray.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), (PrimitiveSettings.VertexOffsetFunction) null, true, false, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(60));
      return false;
    }
  }
}
