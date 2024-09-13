// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantEye
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Systems;
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
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantEye : ModProjectile
  {
    protected bool DieOutsideArena;
    private int ritualID = -1;

    public virtual string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "Terraria/Images/Projectile_452" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantEye_April";
      }
    }

    public virtual int TrailAdditive => 0;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 5;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 12;
      ((Entity) this.Projectile).height = 12;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = 1;
      this.Projectile.timeLeft = 300;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = 0;
      this.CooldownSlot = 1;
      this.DieOutsideArena = this.Projectile.type == ModContent.ProjectileType<MutantEye>();
    }

    public virtual void AI()
    {
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      if ((double) this.Projectile.localAI[0] < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type])
        this.Projectile.localAI[0] += 0.1f;
      else
        this.Projectile.localAI[0] = (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
      this.Projectile.localAI[1] += 0.25f;
      if (!this.DieOutsideArena)
        return;
      if (this.ritualID == -1)
      {
        this.ritualID = -2;
        for (int index = 0; index < Main.maxProjectiles; ++index)
        {
          if (((Entity) Main.projectile[index]).active && Main.projectile[index].type == ModContent.ProjectileType<MutantRitual>())
          {
            this.ritualID = index;
            break;
          }
        }
      }
      Projectile projectile = FargoSoulsUtil.ProjectileExists(this.ritualID, new int[1]
      {
        ModContent.ProjectileType<MutantRitual>()
      });
      if (projectile == null || (double) ((Entity) this.Projectile).Distance(((Entity) projectile).Center) <= 1200.0)
        return;
      this.Projectile.timeLeft = 0;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (target.FargoSouls().BetsyDashing)
        return;
      if (WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      }
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, true, false);
      this.Projectile.timeLeft = 0;
    }

    public virtual void OnKill(int timeleft)
    {
      SoundEngine.PlaySound(ref SoundID.Zombie103, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 144;
      ((Entity) this.Projectile).position.X -= (float) (((Entity) this.Projectile).width / 2);
      ((Entity) this.Projectile).position.Y -= (float) (((Entity) this.Projectile).height / 2);
      for (int index = 0; index < 2; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 3f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
        Main.dust[index3].noGravity = true;
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public float WidthFunction(float completionRatio)
    {
      return MathHelper.SmoothStep((float) ((double) this.Projectile.scale * (double) ((Entity) this.Projectile).width * 1.7000000476837158), 3.5f, completionRatio);
    }

    public static Color ColorFunction(float completionRatio)
    {
      return Color.op_Multiply(Color.Lerp(FargoSoulsUtil.AprilFools ? Color.Yellow : Color.Cyan, Color.Transparent, completionRatio), 0.7f);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      if (SoulConfig.Instance.PerformanceMode)
        return false;
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantEye_Glow", (AssetRequestMode) 2).Value;
      int num1 = texture2D.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color1 = Color.Lerp(FargoSoulsUtil.AprilFools ? new Color((int) byte.MaxValue, 0, 0, this.TrailAdditive) : new Color(31, 187, 192, this.TrailAdditive), Color.Transparent, 0.74f);
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 14f));
      for (int index = 0; index < 3; ++index)
      {
        Vector2 vector2_3 = Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, Utils.RotatedBy(Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 8f), 0.62831854820251465 - (double) index * 3.1415927410125732 / 5.0, new Vector2())), Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 8f));
        float num3 = this.Projectile.scale + (float) Math.Sin((double) this.Projectile.localAI[1]) / 10f;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_3, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color1, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, num3, (SpriteEffects) 0, 0.0f);
      }
      for (float index = this.Projectile.localAI[0] - 1f; (double) index > 0.0; index -= this.Projectile.localAI[0] / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type])
      {
        float num4 = 0.2f;
        if ((double) index > 5.0 && (double) index < 10.0)
          num4 = 0.4f;
        if ((double) index >= 10.0)
          num4 = 0.6f;
        Color color2 = Color.op_Multiply(Color.Lerp(color1, Color.Transparent, 0.1f + num4), (float) ((int) (((double) this.Projectile.localAI[0] - (double) index) / (double) this.Projectile.localAI[0]) ^ 2));
        float num5 = this.Projectile.scale * (this.Projectile.localAI[0] - index) / this.Projectile.localAI[0] + (float) Math.Sin((double) this.Projectile.localAI[1]) / 10f;
        Vector2 vector2_4 = Vector2.op_Subtraction(this.Projectile.oldPos[(int) index], Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 14f));
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_4, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, num5 * 0.8f, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }

    public virtual void PostDraw(Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.White, this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
    }
  }
}
