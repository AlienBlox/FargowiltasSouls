// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.SpectralEoC
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core;
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
  public class SpectralEoC : ModProjectile
  {
    private const string EoCName = "NPC_4";
    private int alphaCounter;
    private bool FinalPhaseBerserkDashesComplete;
    private int FinalPhaseDashCD;
    private bool FinalPhaseDashHorizSpeedSet;
    private int FinalPhaseDashStageDuration;
    private int FinalPhaseAttackCounter;
    private Vector2 targetCenter = Vector2.Zero;

    public virtual string Texture => "FargowiltasSouls/Assets/ExtraTextures/Resprites/NPC_4";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.npcFrameCount[4];
      ProjectileID.Sets.TrailCacheLength[this.Type] = 15;
      ProjectileID.Sets.TrailingMode[this.Type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = 100;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 3600;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.extraUpdates = 1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public ref float Timer => ref this.Projectile.ai[0];

    public virtual void AI()
    {
      if (this.Projectile.frame < 2)
        this.Projectile.frame = 3;
      if (++this.Projectile.frameCounter > 4 && ++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
        this.Projectile.frame = 3;
      int index1 = (int) this.Projectile.ai[1];
      if (!index1.IsWithinBounds((int) byte.MaxValue))
      {
        this.Projectile.Kill();
      }
      else
      {
        Player player = Main.player[index1];
        if (player == null || !((Entity) player).active || player.dead)
        {
          this.Projectile.Kill();
        }
        else
        {
          if (Vector2.op_Equality(this.targetCenter, Vector2.Zero))
            this.targetCenter = ((Entity) player).Center;
          if (!Main.dayTime || Main.zenithWorld || Main.remixWorld)
          {
            if (this.Projectile.timeLeft < 300)
              this.Projectile.timeLeft = 300;
          }
          else
            this.Projectile.Kill();
          ++this.Timer;
          if ((double) this.Timer < 90.0)
          {
            this.Projectile.alpha -= WorldSavingSystem.MasochistModeReal ? 5 : 4;
            if (this.Projectile.alpha < 0)
            {
              this.Projectile.alpha = 0;
              if (WorldSavingSystem.MasochistModeReal && (double) this.Timer < 90.0)
                this.Timer = 90f;
            }
            if ((double) this.Projectile.rotation > 3.1415927410125732)
              this.Projectile.rotation -= 6.28318548f;
            if ((double) this.Projectile.rotation < -3.1415927410125732)
              this.Projectile.rotation += 6.28318548f;
            float num = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, this.targetCenter)) - 1.57079637f;
            if ((double) num > 3.1415927410125732)
              num -= 6.28318548f;
            if ((double) num < -3.1415927410125732)
              num += 6.28318548f;
            this.Projectile.rotation = MathHelper.Lerp(this.Projectile.rotation, num, 0.07f);
            for (int index2 = 0; index2 < 3; ++index2)
            {
              int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
              Main.dust[index3].noGravity = true;
              Main.dust[index3].noLight = true;
              Dust dust = Main.dust[index3];
              dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
            }
            Vector2 targetCenter = this.targetCenter;
            targetCenter.X += (double) ((Entity) this.Projectile).Center.X < (double) targetCenter.X ? -600f : 600f;
            targetCenter.Y += (double) ((Entity) this.Projectile).Center.Y < (double) targetCenter.Y ? -400f : 400f;
            ((Entity) this.Projectile).velocity = Vector2.Zero;
          }
          else if (!this.FinalPhaseBerserkDashesComplete)
          {
            this.Timer = 90f;
            if (++this.FinalPhaseDashCD == 1)
            {
              if (!this.FinalPhaseDashHorizSpeedSet)
              {
                this.FinalPhaseDashHorizSpeedSet = true;
                ((Entity) this.Projectile).velocity.X = (double) ((Entity) this.Projectile).Center.X < (double) this.targetCenter.X ? 18f : -18f;
              }
              ((Entity) this.Projectile).velocity.Y = (double) ((Entity) this.Projectile).Center.Y < (double) this.targetCenter.Y ? 40f : -40f;
              this.Projectile.netUpdate = true;
            }
            else if (this.FinalPhaseDashCD > 20)
              this.FinalPhaseDashCD = 0;
            if (this.FinalPhaseDashStageDuration == 1)
            {
              SoundStyle forceRoarPitched = SoundID.ForceRoarPitched;
              ((SoundStyle) ref forceRoarPitched).Volume = 0.2f;
              ((SoundStyle) ref forceRoarPitched).Pitch = 0.5f;
              SoundEngine.PlaySound(ref forceRoarPitched, new Vector2?(this.targetCenter), (SoundUpdateCallback) null);
            }
            if ((double) ++this.FinalPhaseDashStageDuration > 105.0)
            {
              this.FinalPhaseDashStageDuration = 0;
              this.FinalPhaseBerserkDashesComplete = true;
              if (!WorldSavingSystem.MasochistModeReal)
                ++this.FinalPhaseAttackCounter;
              Projectile projectile = this.Projectile;
              ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.75f);
              this.Projectile.netUpdate = true;
            }
            this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
            if ((double) this.Projectile.rotation > 3.1415927410125732)
              this.Projectile.rotation -= 6.28318548f;
            if ((double) this.Projectile.rotation >= -3.1415927410125732)
              return;
            this.Projectile.rotation += 6.28318548f;
          }
          else
          {
            bool flag1 = this.FinalPhaseAttackCounter >= 3;
            int num = 180;
            if (flag1)
              num += 240;
            if (flag1 && (double) this.Timer < 330.0)
            {
              if ((double) this.Timer == 91.0)
                ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center), ((Vector2) ref ((Entity) this.Projectile).velocity).Length()), 0.75f);
              ((Entity) this.Projectile).velocity.X *= 0.98f;
              if ((double) Math.Abs(((Entity) this.Projectile).Center.X - ((Entity) player).Center.X) < 300.0)
                ((Entity) this.Projectile).velocity.X *= 0.9f;
              bool flag2 = Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height);
              if (!flag2 && (double) ((Entity) this.Projectile).Bottom.X > 0.0 && (double) ((Entity) this.Projectile).Bottom.X < (double) (Main.maxTilesX * 16) && (double) ((Entity) this.Projectile).Bottom.Y > 0.0 && (double) ((Entity) this.Projectile).Bottom.Y < (double) (Main.maxTilesY * 16))
              {
                Tile tileSafely = Framing.GetTileSafely(((Entity) this.Projectile).Bottom);
                if (Tile.op_Inequality(tileSafely, (ArgumentException) null) && ((Tile) ref tileSafely).HasUnactuatedTile)
                  flag2 = Main.tileSolid[(int) ((Tile) ref tileSafely).TileType];
              }
              if (flag2)
              {
                ((Entity) this.Projectile).velocity.X *= 0.95f;
                ((Entity) this.Projectile).velocity.Y -= 0.3f;
                if ((double) ((Entity) this.Projectile).velocity.Y > 0.0)
                  ((Entity) this.Projectile).velocity.Y = 0.0f;
                if ((double) Math.Abs(((Entity) this.Projectile).velocity.Y) > 24.0)
                  ((Entity) this.Projectile).velocity.Y = (float) (24 * Math.Sign(((Entity) this.Projectile).velocity.Y));
              }
              else
              {
                ((Entity) this.Projectile).velocity.Y += 0.3f;
                if ((double) ((Entity) this.Projectile).velocity.Y < 0.0)
                  ((Entity) this.Projectile).velocity.Y += 0.6f;
                if ((double) ((Entity) this.Projectile).velocity.Y > 15.0)
                  ((Entity) this.Projectile).velocity.Y = 15f;
              }
            }
            else
            {
              this.alphaCounter += WorldSavingSystem.MasochistModeReal ? 16 : 4;
              if (this.alphaCounter > (int) byte.MaxValue)
              {
                this.alphaCounter = (int) byte.MaxValue;
                if (WorldSavingSystem.MasochistModeReal && (double) this.Timer < (double) num)
                  this.Timer = (float) num;
              }
              if (flag1)
              {
                ((Entity) this.Projectile).velocity.Y -= 0.15f;
                if ((double) ((Entity) this.Projectile).velocity.Y > 0.0)
                  ((Entity) this.Projectile).velocity.Y = 0.0f;
                if ((double) Math.Abs(((Entity) this.Projectile).velocity.Y) > 24.0)
                  ((Entity) this.Projectile).velocity.Y = (float) (24 * Math.Sign(((Entity) this.Projectile).velocity.Y));
              }
              else
              {
                Projectile projectile = this.Projectile;
                ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.98f);
              }
            }
            this.Projectile.rotation = MathHelper.WrapAngle(MathHelper.Lerp(this.Projectile.rotation, MathHelper.WrapAngle(Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center)) - 1.57079637f), 0.07f));
            if (this.alphaCounter > 0)
            {
              for (int index4 = 0; index4 < 3; ++index4)
              {
                int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
                Main.dust[index5].noGravity = true;
                Main.dust[index5].noLight = true;
                Dust dust = Main.dust[index5];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
              }
            }
            if ((double) this.Timer <= (double) num)
              return;
            this.Projectile.Kill();
          }
        }
      }
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      int num1 = !SoulConfig.Instance.BossRecolors ? 0 : (WorldSavingSystem.EternityMode ? 1 : 0);
      Texture2D texture2D = TextureAssets.Npc[4].Value;
      int num2 = texture2D.Height / Main.projFrames[this.Type];
      int num3 = this.Projectile.frame * num2;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num3, texture2D.Width, num2);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Color color1 = num1 != 0 ? Color.Cyan : Color.Red;
      ((Color) ref color1).A = (byte) 0;
      Color color2 = Color.op_Multiply(color1, 0.13f);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color3 = Color.op_Multiply(Color.op_Multiply(color2, 0.75f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num4 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color3, num4, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(color2), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
