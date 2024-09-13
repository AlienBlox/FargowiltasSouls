// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Cosmos.CosmosVortex
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
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
namespace FargowiltasSouls.Content.Bosses.Champions.Cosmos
{
  public class CosmosVortex : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_578";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual bool? CanDamage() => new bool?((double) this.Projectile.scale >= 2.0);

    public virtual void AI()
    {
      int num1 = 360;
      int num2 = 3;
      if ((double) this.Projectile.ai[1] == 0.0)
        num1 = 30;
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] <= 50.0)
      {
        if (Utils.NextBool(Main.rand, 4))
        {
          Vector2 vector2 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
          dust.noGravity = true;
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, (float) Main.rand.Next(10, 21)));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, 1.5707963705062866, new Vector2()), 4f);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.fadeIn = 0.5f;
        }
        if (Utils.NextBool(Main.rand, 4))
        {
          Vector2 vector2 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
          dust.noGravity = true;
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, -1.5707963705062866, new Vector2()), 2f);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.fadeIn = 0.5f;
        }
      }
      else if ((double) this.Projectile.ai[0] <= 90.0)
      {
        this.Projectile.scale = (float) (((double) this.Projectile.ai[0] - 50.0) / 40.0) * (float) num2;
        this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale / (double) num2);
        this.Projectile.rotation -= 0.1570796f;
        if (Utils.NextBool(Main.rand))
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
          dust.noGravity = true;
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, (float) Main.rand.Next(10, 21)));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, 1.5707963705062866, new Vector2()), 6f);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.fadeIn = 0.5f;
          dust.customData = (object) ((Entity) this.Projectile).Center;
        }
        if (Utils.NextBool(Main.rand))
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
          dust.noGravity = true;
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, -1.5707963705062866, new Vector2()), 3f);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.fadeIn = 0.5f;
          dust.customData = (object) ((Entity) this.Projectile).Center;
        }
        if ((double) this.Projectile.ai[1] != 0.0)
          Suck();
        int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
        if (closest != -1)
        {
          this.Projectile.localAI[1] = Vector2.op_Equality(((Entity) this.Projectile).Center, ((Entity) Main.player[closest]).Center) ? 0.0f : Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[closest]).Center));
          this.Projectile.localAI[1] += 1.04719758f;
        }
      }
      else if ((double) this.Projectile.ai[0] <= (double) (90 + num1))
      {
        this.Projectile.scale = (float) num2;
        this.Projectile.alpha = 0;
        this.Projectile.rotation -= (float) Math.PI / 60f;
        if (Utils.NextBool(Main.rand))
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
          dust.noGravity = true;
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, (float) Main.rand.Next(10, 21)));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, 1.5707963705062866, new Vector2()), 6f);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.fadeIn = 0.5f;
          dust.customData = (object) ((Entity) this.Projectile).Center;
        }
        if ((double) this.Projectile.ai[1] != 0.0)
        {
          Suck();
          int num3 = !WorldSavingSystem.EternityMode || (double) this.Projectile.ai[1] == 1.0 ? 15 : 6;
          if ((double) this.Projectile.localAI[0] % (double) num3 == 0.0)
          {
            SoundEngine.PlaySound(ref SoundID.Item82, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              for (int index = 0; index < 3; ++index)
              {
                Vector2 vector2_1 = Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.localAI[1] + 2.0943951606750488 * (double) index, new Vector2());
                float num4 = Utils.NextBool(Main.rand) ? 1f : -1f;
                Vector2 vector2_2 = Vector2.op_Multiply(Vector2.Normalize(vector2_1), 6f);
                Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_2, 6f), ModContent.ProjectileType<CosmosLightning>(), this.Projectile.damage, 0.0f, Main.myPlayer, Utils.ToRotation(vector2_1), num4, 0.0f);
              }
            }
            this.Projectile.localAI[1] += MathHelper.ToRadians(0.8f * (float) num3) * this.Projectile.ai[1];
          }
          if (WorldSavingSystem.EternityMode && (double) this.Projectile.ai[1] != 1.0 && (double) this.Projectile.localAI[0] % 75.0 == 0.0 && FargoSoulsUtil.HostCheck)
          {
            for (int index1 = 0; index1 < 7; ++index1)
            {
              Vector2 vector2 = Utils.RotatedBy(Vector2.UnitX, -(double) this.Projectile.localAI[1] + 0.89759790897369385 * (double) index1, new Vector2());
              int index2 = Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 4f), ModContent.ProjectileType<CosmosLightningOrb>(), this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              if (index2 != Main.maxProjectiles)
                Main.projectile[index2].timeLeft = 90 + num1 - (int) this.Projectile.ai[0] + 60;
            }
          }
          ++this.Projectile.localAI[0];
        }
      }
      else
      {
        this.Projectile.scale = (float) (1.0 - ((double) this.Projectile.ai[0] - (double) num1) / 60.0) * (float) num2;
        this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale / (double) num2);
        this.Projectile.rotation -= (float) Math.PI / 30f;
        if (this.Projectile.alpha >= (int) byte.MaxValue)
          this.Projectile.Kill();
        Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
        Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
        dust.noGravity = true;
        dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, (float) Main.rand.Next(10, 21)));
        dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, 1.5707963705062866, new Vector2()), 6f);
        dust.scale = 0.5f + Utils.NextFloat(Main.rand);
        dust.fadeIn = 0.5f;
        dust.customData = (object) ((Entity) this.Projectile).Center;
      }
      if (Utils.NextBool(Main.rand))
      {
        Dust dust = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
        dust.fadeIn = 1f;
        dust.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
        dust.noGravity = true;
      }
      float num5 = 0.5f;
      for (int index = 0; index < 5; ++index)
      {
        if ((double) Utils.NextFloat(Main.rand) >= (double) num5)
        {
          float num6 = Utils.NextFloat(Main.rand) * 6.283185f;
          float num7 = Utils.NextFloat(Main.rand);
          Dust dust = Dust.NewDustPerfect(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.ToRotationVector2(num6), (float) (110.0 + 600.0 * (double) num7))), 229, new Vector2?(Vector2.op_Multiply(Utils.ToRotationVector2(num6 - 3.141593f), (float) (14.0 + 8.0 * (double) num7))), 0, new Color(), 1f);
          dust.scale = 0.9f;
          dust.fadeIn = (float) (1.1499999761581421 + (double) num7 * 0.30000001192092896);
          dust.noGravity = true;
        }
      }

      void Suck()
      {
        Player localPlayer = Main.LocalPlayer;
        if (!((Entity) localPlayer).active || localPlayer.dead || localPlayer.ghost || !Vector2.op_Inequality(((Entity) this.Projectile).Center, ((Entity) localPlayer).Center) || (double) ((Entity) this.Projectile).Distance(((Entity) localPlayer).Center) >= 3000.0)
          return;
        float num = ((Entity) this.Projectile).Distance(((Entity) localPlayer).Center) / 60f;
        Player player = localPlayer;
        ((Entity) player).position = Vector2.op_Addition(((Entity) player).position, Vector2.op_Multiply(((Entity) this.Projectile).DirectionFrom(((Entity) localPlayer).Center), num));
        localPlayer.AddBuff(ModContent.BuffType<LowGroundBuff>(), 2, true, false);
        localPlayer.wingTime = 60f;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(144, 360, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      int num = 229;
      for (int index = 0; index < 80; ++index)
      {
        Dust dust1 = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num, 0.0f, 0.0f, 0, new Color(), 1f)];
        Dust dust2 = dust1;
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 10f);
        dust1.fadeIn = 1f;
        dust1.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
        if (!Utils.NextBool(Main.rand, 3))
        {
          dust1.noGravity = true;
          Dust dust3 = dust1;
          dust3.velocity = Vector2.op_Multiply(dust3.velocity, 3f);
          dust1.scale *= 2f;
        }
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(Color.Black, this.Projectile.Opacity), -this.Projectile.rotation, vector2, this.Projectile.scale * 1.25f, (SpriteEffects) 1, 0.0f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
