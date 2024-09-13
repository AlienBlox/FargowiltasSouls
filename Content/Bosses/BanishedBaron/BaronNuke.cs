// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.BanishedBaron.BaronNuke
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.BanishedBaron
{
  public class BaronNuke : ModProjectile
  {
    private readonly int ExplosionDiameter = WorldSavingSystem.MasochistModeReal ? 500 : 500;
    public static readonly SoundStyle Beep = new SoundStyle("FargowiltasSouls/Assets/Sounds/NukeBeep", (SoundType) 0);
    private int NextBeep = 1;
    private int beep = 1;
    private int RingFlash;
    private const int RingFlashDuration = 20;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Type] = 4;
      ProjectileID.Sets.TrailCacheLength[this.Type] = 10;
      ProjectileID.Sets.TrailingMode[this.Type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.light = 1f;
      this.Projectile.timeLeft = Luminance.Common.Utilities.Utilities.SecondsToFrames(60f);
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      int num1 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X;
      int num2 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y;
      if (Math.Abs(num1) > targetHitbox.Width / 2)
        num1 = targetHitbox.Width / 2 * Math.Sign(num1);
      if (Math.Abs(num2) > targetHitbox.Height / 2)
        num2 = targetHitbox.Height / 2 * Math.Sign(num2);
      int num3 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X - num1;
      int num4 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y - num2;
      return new bool?(Math.Sqrt((double) (num3 * num3 + num4 * num4)) <= (double) (((Entity) this.Projectile).width / 2));
    }

    private ref float Duration => ref this.Projectile.ai[0];

    private ref float Timer => ref this.Projectile.localAI[0];

    private bool Rocket => (double) this.Projectile.ai[2] != 0.0;

    public virtual bool CanHitPlayer(Player target) => (double) this.Timer > 60.0;

    public virtual void AI()
    {
      if ((double) this.Duration < 190.0)
        this.Duration = 190f;
      if (++this.Projectile.frameCounter > 8)
      {
        if (++this.Projectile.frame >= Main.projFrames[this.Type])
          this.Projectile.frame = 0;
        this.Projectile.frameCounter = 0;
      }
      if ((double) this.Timer == (double) this.NextBeep)
      {
        SoundEngine.PlaySound(ref BaronNuke.Beep, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        this.NextBeep = (int) ((double) (int) this.Timer + Math.Floor((double) this.Duration / (double) (3 + 2 * this.beep)));
        ++this.beep;
        this.RingFlash = 20;
      }
      if (this.RingFlash > 0)
        --this.RingFlash;
      Vector2 worldPosition = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), 130f), this.Projectile.scale), 2f)), Utils.NextVector2Circular(Main.rand, 10f, 10f));
      if (Utils.NextBool(Main.rand, 3) && (double) ((Vector2) ref ((Entity) this.Projectile).velocity).LengthSquared() > 9.0)
      {
        if (this.Rocket)
        {
          Dust.NewDust(worldPosition, 2, 2, 6, -((Entity) this.Projectile).velocity.X, -((Entity) this.Projectile).velocity.Y, 0, new Color(), 1f);
        }
        else
        {
          if (Main.netMode != 2)
            new Bubble(worldPosition, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedByRandom(((Entity) this.Projectile).velocity, 0.37699112296104431)), Utils.NextFloat(Main.rand, 0.6f, 1f)), 2f), 1f, 30, rotation: Utils.NextFloat(Main.rand, 6.28318548f)).Spawn();
          Dust.NewDust(worldPosition, 2, 2, 33, -((Entity) this.Projectile).velocity.X, -((Entity) this.Projectile).velocity.Y, 0, new Color(), 1f);
        }
      }
      this.Projectile.rotation = Utils.ToRotation(Utils.RotatedBy(((Entity) this.Projectile).velocity, 3.1415927410125732, new Vector2()));
      if (!Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
        this.Projectile.tileCollide = true;
      if ((double) ++this.Timer >= (double) this.Duration - 2.0)
      {
        this.Projectile.tileCollide = false;
        this.Projectile.alpha = 0;
        ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
        ((Entity) this.Projectile).width = this.ExplosionDiameter;
        ((Entity) this.Projectile).height = this.ExplosionDiameter;
        ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      }
      if ((double) this.Timer > (double) this.Duration)
        this.Projectile.Kill();
      Player player = FargoSoulsUtil.PlayerExists(this.Projectile.ai[1]);
      if ((double) this.Timer < 60.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.965f);
      }
      else
      {
        if (player == null || !((Entity) player).active || player.ghost)
          return;
        Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.Projectile).Center);
        float num1 = WorldSavingSystem.MasochistModeReal ? 24f : 20f;
        float num2 = 48f;
        ((Vector2) ref vector2_1).Normalize();
        Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, num1);
        ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, num2 - 1f), vector2_2), num2);
        if (!Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
          return;
        ((Entity) this.Projectile).velocity.X = -0.15f;
        ((Entity) this.Projectile).velocity.Y = -0.05f;
      }
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      if (this.Projectile.soundDelay == 0)
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      this.Projectile.soundDelay = 10;
      if ((double) ((Entity) this.Projectile).velocity.X != (double) oldVelocity.X && (double) Math.Abs(oldVelocity.X) > 1.0)
        ((Entity) this.Projectile).velocity.X = oldVelocity.X * -0.9f;
      if ((double) ((Entity) this.Projectile).velocity.Y != (double) oldVelocity.Y && (double) Math.Abs(oldVelocity.Y) > 1.0)
        ((Entity) this.Projectile).velocity.Y = oldVelocity.Y * -0.9f;
      return false;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(24, 600, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.FargoSouls().MaxLifeReduction += 50;
      target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 1800, true, false);
      target.AddBuff(323, 600, true, false);
      target.AddBuff(36, 2400, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
      for (int index = 0; index < 200; ++index)
      {
        Vector2 position = Vector2.op_Addition(((Entity) this.Projectile).Center, Utils.RotatedBy(new Vector2(0.0f, Utils.NextFloat(Main.rand, (float) this.ExplosionDiameter * 0.8f)), (double) Utils.NextFloat(Main.rand, 6.28318548f), new Vector2()));
        Vector2 velocity = Vector2.op_Division(Vector2.op_Subtraction(position, ((Entity) this.Projectile).Center), 500f);
        ExpandingBloomParticle expandingBloomParticle = new ExpandingBloomParticle(position, velocity, Color.Lerp(Color.Yellow, Color.Red, Utils.Distance(position, ((Entity) this.Projectile).Center) / ((float) this.ExplosionDiameter / 2f)), Vector2.op_Multiply(Vector2.One, 3f), Vector2.op_Multiply(Vector2.One, 6f), 60);
        expandingBloomParticle.Velocity = Vector2.op_Multiply(expandingBloomParticle.Velocity, 2f);
        expandingBloomParticle.Spawn();
      }
      float num = 2f;
      for (int index = 0; index < 20; ++index)
        Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Utils.RotatedByRandom(Vector2.op_Multiply(Vector2.UnitX, 5f), 6.2831854820251465), Main.rand.Next(61, 64), num);
      SoundStyle soundStyle = SoundID.Item62;
      ((SoundStyle) ref soundStyle).Pitch = -0.2f;
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      for (int index1 = 0; index1 < 24; ++index1)
      {
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2_1 = Utils.RotatedBy(new Vector2(0.0f, Utils.NextFloat(Main.rand, 5f, 7f)), (double) index1 * 6.2831854820251465 / 24.0, new Vector2());
          Vector2 vector2_2 = Utils.RotatedBy(vector2_1, (double) Utils.NextFloat(Main.rand, -1f * (float) Math.PI / 32f, (float) Math.PI / 32f), new Vector2());
          int index2 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1), vector2_2, ModContent.ProjectileType<BaronShrapnel>(), this.Projectile.damage, this.Projectile.knockBack, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          if (index2 != Main.maxProjectiles)
          {
            Main.projectile[index2].hostile = this.Projectile.hostile;
            Main.projectile[index2].friendly = this.Projectile.friendly;
          }
        }
      }
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      if ((double) this.Timer >= (double) this.Duration - 2.0)
        return false;
      float num1 = (float) this.RingFlash / 20f;
      Color color1 = Color.Lerp(Color.Orange, Color.Red, num1);
      Texture2D texture2D1 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/GlowRing", (AssetRequestMode) 1).Value;
      float num2 = this.Projectile.scale * 2f * (float) this.ExplosionDiameter / (float) texture2D1.Height;
      Rectangle rectangle1;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle1).\u002Ector(0, 0, texture2D1.Width, texture2D1.Height);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle1), 2f);
      Color color2 = Color.op_Multiply(color1, num1);
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle1), color2, this.Projectile.rotation, vector2_1, num2, (SpriteEffects) 0, 0.0f);
      Texture2D texture2D2 = this.Rocket ? ModContent.Request<Texture2D>(this.Texture + "Rocket", (AssetRequestMode) 1).Value : TextureAssets.Projectile[this.Type].Value;
      int num3 = texture2D2.Height / Main.projFrames[this.Projectile.type];
      int num4 = num3 * this.Projectile.frame;
      Rectangle rectangle2;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle2).\u002Ector(0, num4, texture2D2.Width, num3);
      Vector2 vector2_2 = Vector2.op_Division(Utils.Size(rectangle2), 2f);
      Vector2 vector2_3 = Vector2.op_Division(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), (float) (texture2D2.Width - ((Entity) this.Projectile).width)), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect) null, Main.GameViewMatrix.TransformationMatrix);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color3 = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num5 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(oldPo, vector2_3), Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle2), color3, num5, vector2_2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_3), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle2), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
