// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.HallowSword
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.Graphics;
using Terraria.Graphics.Capture;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class HallowSword : ModProjectile
  {
    private Vector2 mousePos;
    private int syncTimer;
    public Vector2 handlePos = Vector2.Zero;
    private int HitsLeft;
    public int SlashCDMax = 120;
    public const int MaxDistance = 300;
    public bool Reflected;

    private ref float SlashCD => ref this.Projectile.ai[1];

    private ref float Action => ref this.Projectile.ai[0];

    private ref float SlashRotation => ref this.Projectile.localAI[0];

    private ref float SlashArc => ref this.Projectile.localAI[1];

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      this.Projectile.CloneDefaults(946);
      this.AIType = -1;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.minion = true;
      this.Projectile.timeLeft = 18000;
      this.Projectile.hide = false;
      this.Projectile.scale = 1f;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.mousePos.X);
      writer.Write(this.mousePos.Y);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      Vector2 vector2;
      vector2.X = reader.ReadSingle();
      vector2.Y = reader.ReadSingle();
      if (this.Projectile.owner == Main.myPlayer)
        return;
      this.mousePos = vector2;
    }

    public virtual void AI()
    {
      if (Vector2.op_Equality(this.handlePos, Vector2.Zero))
        this.handlePos = Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Multiply(Vector2.UnitY, (float) ((Entity) this.Projectile).height));
      Player player = Main.player[this.Projectile.owner];
      if (((Entity) player).whoAmI == Main.myPlayer && (player.dead || !player.HasEffect<AncientHallowMinion>()))
      {
        this.Projectile.Kill();
      }
      else
      {
        Color transparent = Color.Transparent;
        DelegateMethods.v3_1 = ((Color) ref transparent).ToVector3();
        Point tileCoordinates = Utils.ToTileCoordinates(((Entity) this.Projectile).Center);
        DelegateMethods.CastLightOpen(tileCoordinates.X, tileCoordinates.Y);
        this.Position(player);
        if ((double) this.Action == 1.0)
          this.Action = 2f;
        if ((double) this.Action == 2.0)
          this.Recover(player);
        if (!this.CheckRightClick(player) || (double) this.SlashCD > 0.0)
          return;
        this.HitsLeft = 10;
        this.Slash(player);
      }
    }

    private void Position(Player player)
    {
      Vector2 vector2 = Vector2.op_Addition(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, 50f), this.Projectile.scale), (float) this.GetSide(player)), Vector2.op_Multiply(Vector2.UnitY, 0.0f));
      if (((Entity) player).whoAmI == Main.myPlayer)
      {
        this.mousePos = this.MousePos(player);
        if (++this.syncTimer > 20)
        {
          this.syncTimer = 0;
          this.Projectile.netUpdate = true;
        }
      }
      this.handlePos = Vector2.Lerp(this.handlePos, Vector2.op_Addition(this.mousePos, vector2), 0.5f);
      ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Subtraction(this.handlePos, ((Entity) this.Projectile).Center), 3f);
      if ((double) this.Action != 0.0)
        return;
      this.Projectile.rotation = this.Wobble();
    }

    private float Wobble()
    {
      return (double) Utils.ToRotation(((Entity) this.Projectile).velocity) > 3.1415927410125732 ? (float) (0.0 - 3.1415927410125732 * (double) ((Entity) this.Projectile).velocity.X / 200.0) : (float) (0.0 + 3.1415927410125732 * (double) ((Entity) this.Projectile).velocity.X / 200.0);
    }

    private void Slash(Player player)
    {
      this.Action = 1f;
      this.Projectile.knockBack = 3f;
      SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      this.SlashRotation = this.Projectile.rotation;
      this.Projectile.rotation -= (float) ((double) this.GetSide(player) * 3.1415927410125732 * 0.89999997615814209);
      this.SlashArc = (float) (((double) this.Projectile.rotation - (double) this.SlashRotation + 3.1415927410125732 + 6.2831854820251465) % 6.2831854820251465 - 3.1415927410125732);
      for (int index = 0; index < this.Projectile.localNPCImmunity.Length; ++index)
        this.Projectile.localNPCImmunity[index] = 0;
      this.Reflected = false;
      if (!player.HasBuff<HallowCooldownBuff>())
        this.Reflect(this.Projectile);
      this.SlashCDMax = this.Reflected ? 120 : 60;
      this.SlashCD = (float) this.SlashCDMax;
    }

    private void Recover(Player player)
    {
      if ((double) this.SlashCD <= (double) (this.SlashCDMax - 10))
        this.RotateTowards(this.Wobble(), (float) (2.0 / ((double) this.SlashCD / (double) this.SlashCDMax)));
      if ((double) this.SlashCD <= 0.0)
        return;
      --this.SlashCD;
      if ((double) this.SlashCD > 0.0)
        return;
      this.Action = 0.0f;
    }

    private void RotateTowards(float rotToAlignWith, float speed)
    {
      Vector2 rotationVector2_1 = Utils.ToRotationVector2(rotToAlignWith);
      Vector2 rotationVector2_2 = Utils.ToRotationVector2(this.Projectile.rotation);
      float num = (float) Math.Atan2((double) rotationVector2_1.Y * (double) rotationVector2_2.X - (double) rotationVector2_1.X * (double) rotationVector2_2.Y, (double) rotationVector2_2.X * (double) rotationVector2_1.X + (double) rotationVector2_2.Y * (double) rotationVector2_1.Y);
      this.Projectile.rotation = Utils.ToRotation(Utils.RotatedBy(Utils.ToRotationVector2(this.Projectile.rotation), (double) Math.Sign(num) * (double) Math.Min(Math.Abs(num), (float) ((double) speed * 3.1415927410125732 / 180.0)), new Vector2()));
    }

    private int GetSide(Player player)
    {
      return -Math.Sign(this.MousePos(player).X - ((Entity) player).Center.X);
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      Player player = Main.player[this.Projectile.owner];
      if (player == null || !((Entity) player).active || player.dead || this.HitsLeft <= 0)
        return new bool?(false);
      int width = TextureAssets.Projectile[this.Type].Value.Width;
      int height = TextureAssets.Projectile[this.Type].Value.Height;
      if ((double) this.Action != 1.0)
        return new bool?(false);
      if ((double) Utils.Distance(Utils.ClosestPointInRect(projHitbox, this.handlePos), this.handlePos) > (double) width * (double) this.Projectile.scale)
        return new bool?(false);
      for (int index = 0; index < 25; ++index)
      {
        float num1 = (float) ((double) this.SlashRotation + (double) this.SlashArc * (double) ((float) index / 25f) - 1.5707963705062866);
        float num2 = 0.0f;
        if (Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), this.handlePos, Vector2.op_Addition(this.handlePos, Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(num1), (float) width), this.Projectile.scale)), (float) height * this.Projectile.scale, ref num2))
        {
          --this.HitsLeft;
          return new bool?(true);
        }
      }
      return new bool?(false);
    }

    private void Reflect(Projectile sword)
    {
      Player player = Main.player[sword.owner];
      if (player == null || !((Entity) player).active)
        return;
      int damageCap = player.FargoSouls().ForceEffect<AncientHallowEnchant>() ? 200 : 150;
      foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.hostile && p.damage > 0 && FargoSoulsUtil.CanDeleteProjectile(p) && p.damage <= damageCap && sword.Colliding(((Entity) sword).Hitbox, ((Entity) p).Hitbox))))
      {
        SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/parrynmuse", (SoundType) 0);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) projectile).Center), (SoundUpdateCallback) null);
        this.Reflected = true;
        projectile.hostile = false;
        projectile.friendly = true;
        player.AddBuff(ModContent.BuffType<HallowCooldownBuff>(), 900, true, false);
        projectile.owner = sword.owner;
        projectile.damage = (int) ((double) sword.damage * 1.5);
        projectile.DamageType = sword.DamageType;
        Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_UnaryNegation(((Entity) projectile).velocity)), 30f);
        NPC sourceNpc = projectile.GetSourceNPC();
        if (sourceNpc == null || !((Entity) sourceNpc).active || sourceNpc.townNPC)
        {
          int targetWithLineOfSight = projectile.FindTargetWithLineOfSight(800f);
          sourceNpc = Main.npc[targetWithLineOfSight];
        }
        if (sourceNpc != null && ((Entity) sourceNpc).active && !sourceNpc.townNPC)
          vector2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) sourceNpc).Center, ((Entity) projectile).Center)), 30f);
        ((Entity) projectile).velocity = vector2;
        projectile.netUpdate = true;
      }
    }

    private Vector2 MousePos(Player player)
    {
      Vector2 center = ((Entity) player).Center;
      Vector2 vector2_1 = Luminance.Common.Utilities.Utilities.SafeDirectionTo(((Entity) player).Center, Main.MouseWorld);
      Vector2 vector2_2 = Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center);
      double num = (double) Math.Min(((Vector2) ref vector2_2).Length(), 300f);
      Vector2 vector2_3 = Vector2.op_Multiply(vector2_1, (float) num);
      return Vector2.op_Addition(center, vector2_3);
    }

    private bool CheckRightClick(Player player)
    {
      return player.controlUseTile && !player.tileInteractionHappened && !player.mouseInterface && !CaptureManager.Instance.Active && !Main.HoveringOverAnNPC && !Main.SmartInteractShowingGenuine && PlayerInput.Triggers.Current.MouseRight;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      EmpressBladeDrawer empressBladeDrawer = new EmpressBladeDrawer();
      float num1 = (float) ((double) Main.GlobalTimeWrappedHourly % 3.0 / 3.0);
      float num2 = MathHelper.Max(1f, (float) Main.player[this.Projectile.owner].maxMinions);
      float hueOverride = (float) this.Projectile.identity % num2 / num2 + num1;
      Color queenWeaponsColor1 = this.Projectile.GetFairyQueenWeaponsColor(0.0f, 0.0f, new float?(hueOverride % 1f));
      Color queenWeaponsColor2 = this.Projectile.GetFairyQueenWeaponsColor(0.0f, 0.0f, new float?((float) (((double) hueOverride + 0.5) % 1.0)));
      empressBladeDrawer.ColorStart = queenWeaponsColor1;
      empressBladeDrawer.ColorEnd = queenWeaponsColor2;
      ((EmpressBladeDrawer) ref empressBladeDrawer).Draw(this.Projectile);
      HallowSword.DrawProj_EmpressBlade(this.Projectile, hueOverride);
      return false;
    }

    private static void DrawProj_EmpressBlade(Projectile proj, float hueOverride)
    {
      Main.CurrentDrawnEntityShader = -1;
      HallowSword.PrepareDrawnEntityDrawing((Entity) proj, Main.GetProjectileDesiredShader(proj));
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) proj).Center, Main.screenPosition);
      proj.GetFairyQueenWeaponsColor(0.0f, 0.0f, new float?(hueOverride));
      Color queenWeaponsColor = proj.GetFairyQueenWeaponsColor(0.5f, 0.0f, new float?(hueOverride));
      Texture2D texture2D = TextureAssets.Projectile[proj.type].Value;
      Vector2 vector2_2 = Vector2.op_Addition(vector2_1, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(proj.rotation - 1.57079637f), (float) texture2D.Width), proj.scale), 2f));
      Vector2 vector2_3 = Vector2.op_Division(Utils.Size(Utils.Frame(texture2D, 1, 1, 0, 0, 0, 0)), 2f);
      Color color1 = Color.op_Multiply(Color.White, proj.Opacity);
      ((Color) ref color1).A = (byte) ((double) ((Color) ref color1).A * 0.699999988079071);
      ref Color local = ref queenWeaponsColor;
      ((Color) ref local).A = (byte) ((uint) ((Color) ref local).A / 2U);
      float scale = proj.scale;
      float num1 = proj.rotation - 1.57079637f;
      float num2 = proj.Opacity * 0.3f;
      float num3 = 0.0f;
      if ((double) num2 > 0.0)
      {
        float lerpValue = Utils.GetLerpValue(60f, 50f, num3, true);
        float num4 = Utils.GetLerpValue(70f, 50f, num3, true) * Utils.GetLerpValue(40f, 45f, num3, true);
        for (float num5 = 0.0f; (double) num5 < 1.0; num5 += 0.166666672f)
        {
          Vector2 vector2_4 = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(num1), -120f), num5), lerpValue);
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(vector2_2, vector2_4), new Rectangle?(), Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(queenWeaponsColor, num2), 1f - num5), num4), num1, vector2_3, scale * 1.5f, (SpriteEffects) 0, 0.0f);
        }
        for (float num6 = 0.0f; (double) num6 < 1.0; num6 += 0.25f)
        {
          Vector2 vector2_5 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(num6 * 6.28318548f + num1), 4f), scale);
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(vector2_2, vector2_5), new Rectangle?(), Color.op_Multiply(queenWeaponsColor, num2), num1, vector2_3, scale, (SpriteEffects) 0, 0.0f);
        }
      }
      HallowSword hallowSword = proj.ModProjectile == null || !(proj.ModProjectile is HallowSword) ? (HallowSword) null : Luminance.Common.Utilities.Utilities.As<HallowSword>(proj);
      float num7 = (hallowSword.SlashCD + 5f - (float) hallowSword.SlashCDMax) / 5f;
      Color transparent = Color.Transparent;
      Color lightGoldenrodYellow = Color.LightGoldenrodYellow;
      ((Color) ref lightGoldenrodYellow).A = (byte) 0;
      Color color2 = lightGoldenrodYellow;
      double num8 = (double) num7;
      Color color3 = Color.Lerp(transparent, color2, (float) num8);
      if (hallowSword != null && (double) hallowSword.Action == 2.0 && (double) hallowSword.SlashCD >= (double) hallowSword.SlashCDMax - 5.0)
      {
        for (int index = 0; index < 100; ++index)
        {
          float num9 = (float) (1.0 - (double) index / 100.0);
          float num10 = hallowSword.SlashRotation + hallowSword.SlashArc * num9;
          Vector2 vector2_6 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) proj).Center, Main.screenPosition), Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(num10 - 1.57079637f), (float) texture2D.Width), proj.scale), 2f));
          float num11 = num10 - 1.57079637f;
          Color color4 = Color.Lerp(Color.Transparent, color3, num9);
          Main.EntitySpriteDraw(texture2D, vector2_6, new Rectangle?(), color4, num11, vector2_3, scale, (SpriteEffects) 0, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D, vector2_2, new Rectangle?(), color1, num1, vector2_3, scale, (SpriteEffects) 0, 0.0f);
      Main.EntitySpriteDraw(texture2D, vector2_2, new Rectangle?(), Color.op_Multiply(Color.op_Multiply(queenWeaponsColor, num2), 0.5f), num1, vector2_3, scale, (SpriteEffects) 0, 0.0f);
    }

    public static void PrepareDrawnEntityDrawing(Entity entity, int intendedShader)
    {
      Main.CurrentDrawnEntity = entity;
      if (intendedShader != 0)
      {
        if (Main.CurrentDrawnEntityShader == 0 || Main.CurrentDrawnEntityShader == -1)
        {
          Main.spriteBatch.End();
          Main.spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect) null, Main.Transform);
        }
      }
      else if (Main.CurrentDrawnEntityShader != 0 && Main.CurrentDrawnEntityShader != -1)
      {
        Main.spriteBatch.End();
        Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect) null, Main.Transform);
      }
      Main.CurrentDrawnEntityShader = intendedShader;
    }
  }
}
