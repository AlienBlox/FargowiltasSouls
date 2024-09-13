// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MechElectricOrb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MechElectricOrb : ModProjectile
  {
    public static readonly SoundStyle ShotSound;
    public static readonly SoundStyle HumSound;
    public const int Red = 0;
    public const int Blue = 1;
    public const int Yellow = 2;
    public const int Green = 3;
    private bool lastSecondAccel;

    private ref float ColorAI => ref this.Projectile.ai[2];

    public float ColorType
    {
      get
      {
        return !WorldSavingSystem.EternityMode || !SoulConfig.Instance.BossRecolors ? 0.0f : this.ColorAI;
      }
    }

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
      Main.projFrames[this.Type] = 10;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 24;
      ((Entity) this.Projectile).height = 24;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = 50;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 600;
      this.Projectile.hostile = true;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (((Rectangle) ref projHitbox).Intersects(targetHitbox))
        return new bool?(true);
      Rectangle rectangle = projHitbox;
      rectangle.X = (int) ((Entity) this.Projectile).oldPosition.X;
      rectangle.Y = (int) ((Entity) this.Projectile).oldPosition.Y;
      return ((Rectangle) ref rectangle).Intersects(targetHitbox) ? new bool?(true) : new bool?(false);
    }

    public virtual void AI()
    {
      if (++this.Projectile.frameCounter > 6)
      {
        if (++this.Projectile.frame >= Main.projFrames[this.Type])
          this.Projectile.frame = 0;
        this.Projectile.frameCounter = 0;
      }
      if ((double) this.Projectile.localAI[1] == 0.0)
      {
        SoundStyle shotSound = MechElectricOrb.ShotSound;
        ((SoundStyle) ref shotSound).Volume = 0.5f;
        SoundEngine.PlaySound(ref shotSound, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
        this.Projectile.localAI[1] = 1f;
        SoundStyle humSound = MechElectricOrb.HumSound;
        ((SoundStyle) ref humSound).PitchVariance = 0.3f;
        ((SoundStyle) ref humSound).Volume = 0.2f;
        ((SoundStyle) ref humSound).MaxInstances = 5;
        ((SoundStyle) ref humSound).SoundLimitBehavior = (SoundLimitBehavior) 1;
        SoundEngine.PlaySound(ref humSound, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
        this.lastSecondAccel = this.Projectile.type == ModContent.ProjectileType<MechElectricOrb>();
      }
      if ((double) this.Projectile.localAI[0] == 0.0)
        this.Projectile.localAI[0] = Utils.NextBool(Main.rand) ? 1f : -1f;
      this.Projectile.Opacity = 1f;
      this.Projectile.rotation += 0.157079637f * this.Projectile.localAI[0];
      float colorType = this.ColorType;
      Color color = (double) colorType == 1.0 ? Color.Teal : ((double) colorType == 3.0 ? Color.Green : ((double) colorType == 2.0 ? Color.Yellow : Color.Red));
      if (Utils.NextBool(Main.rand, 6))
        new ElectricSpark(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.Normalize(Vector2.op_UnaryNegation(Utils.RotatedByRandom(((Entity) this.Projectile).velocity, 0.62831854820251465))), Math.Max(4f, ((Vector2) ref ((Entity) this.Projectile).velocity).Length() / 2f)), Color.op_Multiply(color, 0.7f), Utils.NextFloat(Main.rand, 0.7f, 1f), 20).Spawn();
      Lighting.AddLight(((Entity) this.Projectile).Center, (float) ((Color) ref color).R / (float) byte.MaxValue, (float) ((Color) ref color).G / (float) byte.MaxValue, (float) ((Color) ref color).B / (float) byte.MaxValue);
      if (this.lastSecondAccel && (double) this.Projectile.ai[0] == -1.0 && (double) --this.Projectile.ai[1] < 0.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.03f);
      }
      float num = ((Vector2) ref ((Entity) this.Projectile).velocity).Length() / (float) (((Entity) this.Projectile).width * 3);
      if ((double) num <= 1.0)
        return;
      Projectile projectile1 = this.Projectile;
      ((Entity) projectile1).velocity = Vector2.op_Division(((Entity) projectile1).velocity, num);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.primeBoss, (int) sbyte.MaxValue))
        target.AddBuff(ModContent.BuffType<NanoInjectionBuff>(), 360, true, false);
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.destroyBoss, 134))
        target.AddBuff(144, 60, true, false);
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.retiBoss, 125))
        target.AddBuff(69, 300, true, false);
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.spazBoss, 126))
        return;
      target.AddBuff(39, 180, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item10, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      float colorType = this.ColorType;
      Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, (double) colorType == 1.0 ? 59 : ((double) colorType == 3.0 ? 61 : ((double) colorType == 2.0 ? 64 : 60)), ((Entity) this.Projectile).velocity.X * 0.1f, ((Entity) this.Projectile).velocity.Y * 0.1f, 150, new Color(), 1.2f);
      if (Main.dedServ)
        return;
      Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).position, new Vector2(((Entity) this.Projectile).velocity.X * 0.05f, ((Entity) this.Projectile).velocity.Y * 0.05f), Main.rand.Next(16, 18), 1f);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Type].Value;
      int num1 = texture2D.Height / Main.projFrames[this.Type];
      int num2 = texture2D.Width / 4;
      int num3 = this.Projectile.frame * num1;
      int num4 = (int) this.ColorType * num2;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(num4, num3, num2, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.33f)
      {
        float colorType = this.ColorType;
        Color color1 = (double) colorType == 1.0 ? Color.Teal : ((double) colorType == 3.0 ? Color.Green : ((double) colorType == 2.0 ? Color.Yellow : Color.Red));
        ((Color) ref color1).A = (byte) 50;
        float num5 = ((float) ProjectileID.Sets.TrailCacheLength[this.Type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Type];
        Color color2 = Color.op_Multiply(color1, num5);
        float num6 = (float) ((double) this.Projectile.scale / 2.0 + (double) this.Projectile.scale * (double) num5 / 2.0);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          Vector2 vector2_2 = Vector2.op_Addition(Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(vector2_1, 2f));
          float num7 = this.Projectile.oldRot[index2];
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, num7, vector2_1, num6, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.White, this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    static MechElectricOrb()
    {
      SoundStyle soundStyle;
      // ISSUE: explicit constructor call
      ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/ElectricOrbShot", (SoundType) 0);
      ((SoundStyle) ref soundStyle).PitchVariance = 0.3f;
      ((SoundStyle) ref soundStyle).Volume = 7f;
      MechElectricOrb.ShotSound = soundStyle;
      MechElectricOrb.HumSound = new SoundStyle("FargowiltasSouls/Assets/Sounds/ElectricOrbHum", (SoundType) 0);
    }
  }
}
