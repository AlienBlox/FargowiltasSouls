// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantEyeOfCthulhu
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantEyeOfCthulhu : ModProjectile
  {
    private const float degreesOffset = 22.5f;
    private const float dashSpeed = 120f;
    private const float baseDistance = 700f;
    private bool spawned;

    public virtual string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "FargowiltasSouls/Assets/ExtraTextures/Resprites/NPC_4" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantEyeOfCthulhu_April";
      }
    }

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.npcFrameCount[4];
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 12;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 80;
      ((Entity) this.Projectile).height = 80;
      this.Projectile.penetrate = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.CooldownSlot = 1;
      this.Projectile.timeLeft = 216;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.alpha = (int) byte.MaxValue;
    }

    public virtual bool CanHitPlayer(Player target) => target.hurtCooldowns[1] == 0;

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.Projectile.localAI[1]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.Projectile.localAI[1] = reader.ReadSingle();
    }

    public virtual bool? CanDamage() => new bool?((double) this.Projectile.ai[1] >= 120.0);

    public virtual void AI()
    {
      Player player = FargoSoulsUtil.PlayerExists(this.Projectile.ai[0]);
      if (player == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        if (!this.spawned)
        {
          this.spawned = true;
          SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, -1f, 4f, 0.0f);
        }
        if ((double) ++this.Projectile.ai[1] < 120.0)
        {
          this.Projectile.alpha -= 8;
          if (this.Projectile.alpha < 0)
            this.Projectile.alpha = 0;
          Projectile projectile = this.Projectile;
          ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Division(((Entity) player).velocity, 2f));
          float num1 = (float) ((double) this.Projectile.ai[1] * 1.5 / 120.0);
          if ((double) num1 < 0.25)
            num1 = 0.25f;
          if ((double) num1 > 1.0)
            num1 = 1f;
          Vector2 vector2 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.Projectile).DirectionFrom(((Entity) player).Center), (double) MathHelper.ToRadians(20f), new Vector2()), 700f), num1));
          float num2 = 0.6f;
          if ((double) ((Entity) this.Projectile).Center.X < (double) vector2.X)
          {
            ((Entity) this.Projectile).velocity.X += num2;
            if ((double) ((Entity) this.Projectile).velocity.X < 0.0)
              ((Entity) this.Projectile).velocity.X += num2 * 2f;
          }
          else
          {
            ((Entity) this.Projectile).velocity.X -= num2;
            if ((double) ((Entity) this.Projectile).velocity.X > 0.0)
              ((Entity) this.Projectile).velocity.X -= num2 * 2f;
          }
          if ((double) ((Entity) this.Projectile).Center.Y < (double) vector2.Y)
          {
            ((Entity) this.Projectile).velocity.Y += num2;
            if ((double) ((Entity) this.Projectile).velocity.Y < 0.0)
              ((Entity) this.Projectile).velocity.Y += num2 * 2f;
          }
          else
          {
            ((Entity) this.Projectile).velocity.Y -= num2;
            if ((double) ((Entity) this.Projectile).velocity.Y > 0.0)
              ((Entity) this.Projectile).velocity.Y -= num2 * 2f;
          }
          if ((double) Math.Abs(((Entity) this.Projectile).velocity.X) > 24.0)
            ((Entity) this.Projectile).velocity.X = (float) (24 * Math.Sign(((Entity) this.Projectile).velocity.X));
          if ((double) Math.Abs(((Entity) this.Projectile).velocity.Y) > 24.0)
            ((Entity) this.Projectile).velocity.Y = (float) (24 * Math.Sign(((Entity) this.Projectile).velocity.Y));
          this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center)) - 1.57079637f;
        }
        else if ((double) this.Projectile.ai[1] == 120.0)
        {
          this.Projectile.localAI[0] = ((Entity) player).Center.X;
          this.Projectile.localAI[1] = ((Entity) player).Center.Y;
          ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.Projectile).DirectionFrom(((Entity) player).Center), 700f));
          ((Entity) this.Projectile).velocity = Vector2.Zero;
          this.Projectile.netUpdate = true;
        }
        else if ((double) this.Projectile.ai[1] == 121.0)
        {
          if (FargoSoulsUtil.HostCheck)
          {
            SpawnProjectile(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).velocity, 2f)));
            float num = 0.025f;
            Vector2 vector2;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2).\u002Ector(this.Projectile.localAI[0], this.Projectile.localAI[1]);
            float rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, vector2));
            int index1 = Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<MutantScythe2>(), this.Projectile.damage, 0.0f, Main.myPlayer, num, rotation, 0.0f);
            if (index1 != Main.maxProjectiles)
              Main.projectile[index1].timeLeft = this.Projectile.timeLeft + 180 + 30;
            if (WorldSavingSystem.MasochistModeReal)
            {
              int index2 = Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<MutantScythe2>(), this.Projectile.damage, 0.0f, Main.myPlayer, num, rotation, 0.0f);
              if (index2 != Main.maxProjectiles)
                Main.projectile[index2].timeLeft = this.Projectile.timeLeft + 180 + 30 + 150;
            }
          }
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(120f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, new Vector2(this.Projectile.localAI[0], this.Projectile.localAI[1])), (double) MathHelper.ToRadians(22.5f), new Vector2()));
          this.Projectile.netUpdate = true;
          SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        else if ((double) this.Projectile.ai[1] < 131.66667175292969)
        {
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
          if (FargoSoulsUtil.HostCheck)
          {
            SpawnProjectile(((Entity) this.Projectile).Center);
            SpawnProjectile(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).velocity, 2f)));
          }
        }
        else
        {
          if (FargoSoulsUtil.HostCheck)
          {
            SpawnProjectile(((Entity) this.Projectile).Center);
            SpawnProjectile(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).velocity, 2f)));
          }
          this.Projectile.ai[1] = 120f;
        }
        if (++this.Projectile.frameCounter > 6)
        {
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
            this.Projectile.frame = 0;
        }
        if (this.Projectile.frame >= 3)
          return;
        this.Projectile.frame = 3;
      }

      void SpawnProjectile(Vector2 position)
      {
        float num = 0.03f;
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(this.Projectile.localAI[0], this.Projectile.localAI[1]);
        float rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, Vector2.op_Addition(vector2, Vector2.op_Multiply(180f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, vector2), 1.5707963705062866, new Vector2())))));
        int index = Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), position, Vector2.Zero, ModContent.ProjectileType<MutantScythe1>(), this.Projectile.damage, 0.0f, Main.myPlayer, num, rotation, 0.0f);
        if (index == Main.maxProjectiles)
          return;
        Main.projectile[index].timeLeft = this.Projectile.timeLeft + 180 + 30 + 150;
        if (!WorldSavingSystem.MasochistModeReal)
          return;
        Main.projectile[index].timeLeft -= 30;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(163, 15, true, false);
      target.FargoSouls().MaxLifeReduction += 100;
      target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 120, true, false);
      target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 50; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 5, 0.0f, 0.0f, 0, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
      }
      Vector2 vector2 = (double) this.Projectile.localAI[0] == 0.0 || (double) this.Projectile.localAI[1] == 0.0 ? Vector2.Zero : Vector2.op_Multiply(30f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, new Vector2(this.Projectile.localAI[0], this.Projectile.localAI[1])), (double) MathHelper.ToRadians(22.5f), new Vector2()));
      for (int index = 0; index < 2; ++index)
      {
        if (!Main.dedServ)
        {
          Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) this.Projectile).width), Utils.NextFloat(Main.rand, (float) ((Entity) this.Projectile).height))), vector2, ModContent.Find<ModGore>(((ModType) this).Mod.Name, "Gore_8").Type, 1f);
          Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) this.Projectile).width), Utils.NextFloat(Main.rand, (float) ((Entity) this.Projectile).height))), vector2, ModContent.Find<ModGore>(((ModType) this).Mod.Name, "Gore_9").Type, 1f);
          Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) this.Projectile).width), Utils.NextFloat(Main.rand, (float) ((Entity) this.Projectile).height))), vector2, ModContent.Find<ModGore>(((ModType) this).Mod.Name, "Gore_10").Type, 1f);
        }
      }
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      float num3 = (float) (((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.30000001192092896 + 0.89999997615814209) * this.Projectile.scale;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, (double) this.Projectile.ai[1] >= 120.0 ? 0.75f : 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num4 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num4, vector2, num3, (SpriteEffects) 0, 0.0f);
      }
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
