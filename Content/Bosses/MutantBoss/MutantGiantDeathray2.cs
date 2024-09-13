// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantGiantDeathray2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantGiantDeathray2 : MutantSpecialDeathray
  {
    public int dustTimer;
    public bool stall;
    private int hits;

    public MutantGiantDeathray2()
      : base(600)
    {
    }

    public bool BeBrighter => (double) this.Projectile.ai[0] > 0.0;

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ProjectileID.Sets.DismountsPlayersOnHit[this.Projectile.type] = true;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.netImportant = true;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      this.maxTime += 180f;
    }

    public virtual bool? CanDamage()
    {
      this.Projectile.maxPenetrate = 1;
      return new bool?((double) this.Projectile.scale >= 5.0);
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      base.SendExtraAI(writer);
      writer.Write(this.Projectile.localAI[0]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      base.ReceiveExtraAI(reader);
      this.Projectile.localAI[0] = reader.ReadSingle();
    }

    public override void AI()
    {
      base.AI();
      if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
        FargoSoulsUtil.ScreenshakeRumble(6f);
      this.Projectile.timeLeft = 2;
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>());
      if (npc != null)
      {
        ((Entity) this.Projectile).Center = Vector2.op_Addition(Vector2.op_Addition(((Entity) npc).Center, Utils.NextVector2Circular(Main.rand, 5f, 5f)), Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, (double) npc.ai[3], new Vector2()), (double) npc.ai[0] == -7.0 ? 100f : 175f), this.Projectile.scale), 10f));
        if ((double) npc.ai[0] == -7.0)
          this.maxTime = (float) byte.MaxValue;
        else if ((double) npc.ai[0] == -5.0)
        {
          if (npc.HasValidTarget && Main.player[npc.target].HasBuff<TimeFrozenBuff>())
            this.stall = true;
          if ((double) npc.localAI[2] > 30.0)
          {
            if ((double) this.Projectile.localAI[0] < (double) this.maxTime - 90.0)
              this.Projectile.localAI[0] = this.maxTime - 90f;
          }
          else if (this.stall)
          {
            --this.Projectile.localAI[0];
            this.Projectile.netUpdate = true;
            --npc.ai[2];
            npc.netUpdate = true;
          }
          else if (Main.getGoodWorld && (double) this.Projectile.localAI[0] > (double) this.maxTime - 10.0 && npc.life > 1)
            --this.Projectile.localAI[0];
        }
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
          float num2 = this.stall ? 1f : (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime);
          this.stall = false;
          this.Projectile.scale = num2 * 7f * num1;
          if (WorldSavingSystem.MasochistModeReal)
            this.Projectile.scale *= 5f;
          if ((double) this.Projectile.scale > (double) num1)
            this.Projectile.scale = num1;
          float num3 = npc.ai[3] - 1.57079637f;
          double rotation = (double) this.Projectile.rotation;
          this.Projectile.rotation = num3;
          ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(num3 + 1.57079637f);
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
          float num4 = 0.0f;
          for (int index = 0; index < numArray.Length; ++index)
            num4 += numArray[index];
          this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num4 / length, 0.5f);
          if (this.Projectile.damage <= 0 || !((Entity) Main.LocalPlayer).active || !this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) Main.LocalPlayer).Hitbox))
            return;
          Main.LocalPlayer.immune = false;
          Main.LocalPlayer.immuneTime = 0;
          Main.LocalPlayer.hurtCooldowns[0] = 0;
          Main.LocalPlayer.hurtCooldowns[1] = 0;
          Main.LocalPlayer.ClearBuff(ModContent.BuffType<GoldenStasisBuff>());
        }
      }
      else
        this.Projectile.Kill();
    }

    public virtual void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
      base.ModifyHitPlayer(target, ref modifiers);
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, this.DamageRampup());
      if (this.hits <= 180)
        return;
      target.endurance = 0.0f;
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      base.ModifyHitNPC(target, ref modifiers);
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, this.DamageRampup());
    }

    private float DamageRampup()
    {
      this.stall = true;
      ++this.hits;
      int num1 = this.hits - 90;
      if (num1 <= 0)
        return (float) this.hits / 90f;
      float num2 = (float) Math.Min(Math.Pow((double) num1, 2.0), 100000.0);
      if ((double) num2 < 0.0)
      {
        --this.hits;
        num2 = 100000f;
      }
      return num2;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      }
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, true, false);
      target.immune = false;
      target.immuneTime = 0;
      target.hurtCooldowns[0] = 0;
      target.hurtCooldowns[1] = 0;
      ((Entity) target).velocity = Vector2.op_Multiply(-0.4f, Vector2.UnitY);
      target.FargoSouls().NoUsingItems = 2;
    }

    public float WidthFunction(float trailInterpolant)
    {
      return this.Projectile.scale * (float) ((Entity) this.Projectile).width;
    }

    public static Color ColorFunction(float trailInterpolant)
    {
      return Color.Lerp(FargoSoulsUtil.AprilFools ? new Color((int) byte.MaxValue, 0, 0, 100) : new Color(31, 187, 192, 100), FargoSoulsUtil.AprilFools ? new Color((int) byte.MaxValue, 191, 51, 100) : new Color(51, (int) byte.MaxValue, 191, 100), trailInterpolant);
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
      Vector2 vector2_3 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.BeBrighter ? 90f : 180f));
      Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(vector2_3, Main.screenPosition), new Rectangle?(), color, this.Projectile.rotation, Vector2.op_Multiply(Utils.Size(texture2D), 0.5f), this.Projectile.scale * 0.4f, (SpriteEffects) 0, 0.0f);
      // ISSUE: method pointer
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), MutantGiantDeathray2.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (MutantGiantDeathray2.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), (PrimitiveSettings.VertexOffsetFunction) null, true, false, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(60));
      return false;
    }
  }
}
