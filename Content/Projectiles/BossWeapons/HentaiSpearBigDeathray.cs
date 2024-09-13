// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSpearBigDeathray
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
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HentaiSpearBigDeathray : MutantSpecialDeathray
  {
    public HentaiSpearBigDeathray()
      : base(60, 1.5f)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.CooldownSlot = -1;
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.hide = true;
      this.Projectile.penetrate = -1;
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

    public Vector2 TipOffset
    {
      get
      {
        return Vector2.op_Multiply(Vector2.op_Multiply(9f, ((Entity) this.Projectile).velocity), this.Projectile.scale);
      }
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      double num1 = (double) ((Entity) this.Projectile).Distance(FargoSoulsUtil.ClosestPointInHitbox(targetHitbox, ((Entity) this.Projectile).Center));
      Vector2 tipOffset = this.TipOffset;
      double num2 = (double) ((Vector2) ref tipOffset).Length() * 2.0;
      return num1 < num2 ? new bool?(true) : base.Colliding(projHitbox, targetHitbox);
    }

    public override void AI()
    {
      base.AI();
      Player player = Main.player[this.Projectile.owner];
      if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
        FargoSoulsUtil.ScreenshakeRumble(6f);
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      Projectile projectile1 = FargoSoulsUtil.ProjectileExists(FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, (int) this.Projectile.ai[1], new int[1]
      {
        ModContent.ProjectileType<HentaiSpearWand>()
      }), Array.Empty<int>());
      if (projectile1 != null)
      {
        this.Projectile.timeLeft = 2;
        float num = ((Entity) player).direction < 0 ? 3.14159274f : 0.0f;
        if ((double) Math.Abs(player.itemRotation) > Math.PI / 2.0)
          num = (double) num == 0.0 ? 3.14159274f : 0.0f;
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(player.itemRotation + num);
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) projectile1).Center, Utils.NextVector2Circular(Main.rand, 5f, 5f));
        Projectile projectile2 = this.Projectile;
        ((Entity) projectile2).position = Vector2.op_Addition(((Entity) projectile2).position, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 164f), projectile1.scale), 0.45f));
        this.Projectile.damage = player.GetWeaponDamage(player.HeldItem, false);
        this.Projectile.CritChance = player.GetWeaponCrit(player.HeldItem);
        this.Projectile.knockBack = player.GetWeaponKnockback(player.HeldItem, player.HeldItem.knockBack);
      }
      else if ((double) ++this.Projectile.localAI[0] > 5.0)
      {
        this.Projectile.Kill();
        return;
      }
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
      if ((double) this.Projectile.localAI[0] == (double) this.maxTime / 2.0)
      {
        if (this.Projectile.owner == Main.myPlayer && (!player.controlUseTile || player.altFunctionUse != 2 || player.HeldItem.type != ModContent.ItemType<FargowiltasSouls.Content.Items.Weapons.FinalUpgrades.HentaiSpear>()))
          ++this.Projectile.localAI[0];
        else
          --this.Projectile.localAI[0];
      }
      else
        ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.scale = (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * 1.5f * num1;
        if ((double) this.Projectile.scale > (double) num1)
          this.Projectile.scale = num1;
        this.Projectile.scale *= projectile1.scale / 1.3f;
        Projectile projectile3 = this.Projectile;
        ((Entity) projectile3).position = Vector2.op_Addition(((Entity) projectile3).position, this.TipOffset);
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
        Projectile projectile4 = this.Projectile;
        ((Entity) projectile4).position = Vector2.op_Subtraction(((Entity) projectile4).position, ((Entity) this.Projectile).velocity);
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
        if ((double) ++this.Projectile.ai[0] <= 60.0)
          return;
        this.Projectile.ai[0] = 0.0f;
        SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        if (this.Projectile.owner != Main.myPlayer)
          return;
        for (int index = 0; index < 10; ++index)
        {
          Vector2 vector2_2 = Vector2.op_Multiply(12f, Utils.RotatedBy(((Entity) this.Projectile).velocity, Math.PI / 5.0 * (double) index, new Vector2()));
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) player).Center, vector2_2, ModContent.ProjectileType<HentaiSphereRing>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.5f, 12f, 0.0f);
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) player).Center, vector2_2, ModContent.ProjectileType<HentaiSphereRing>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, -0.5f, 12f, 0.0f);
        }
      }
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

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 1;
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, false);
    }

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
      Vector2 vector2_3 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.BeBrighter ? 90f : 180f));
      Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(vector2_3, Main.screenPosition), new Rectangle?(), color, this.Projectile.rotation, Vector2.op_Multiply(Utils.Size(texture2D), 0.5f), this.Projectile.scale * 0.4f, (SpriteEffects) 0, 0.0f);
      // ISSUE: method pointer
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), HentaiSpearBigDeathray.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (HentaiSpearBigDeathray.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), (PrimitiveSettings.VertexOffsetFunction) null, true, false, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(60));
      return false;
    }
  }
}
