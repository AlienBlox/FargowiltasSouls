// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.CultistVortex
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
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class CultistVortex : ModProjectile
  {
    private int p;

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
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      int num1 = 15 * (int) this.Projectile.ai[1];
      if ((double) this.Projectile.ai[0] == 0.0)
        this.p = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
      if (this.p != -1)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Multiply(((Entity) Main.player[this.p]).velocity, 0.8f));
        Vector2 center = ((Entity) Main.player[this.p]).Center;
        center.Y -= 400f;
        Vector2 vector2 = Vector2.op_Division(Vector2.op_Subtraction(center, ((Entity) this.Projectile).Center), 6f);
        ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 19f), vector2), 20f);
      }
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] <= 30.0)
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
      else if ((double) this.Projectile.ai[0] <= 60.0)
      {
        this.Projectile.scale = (float) (((double) this.Projectile.ai[0] - 30.0) / 40.0 * 1.0);
        this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale / 1.0);
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
      }
      else if ((double) this.Projectile.ai[0] <= (double) (60 + num1))
      {
        this.Projectile.scale = 1f;
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
        else
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
        if ((double) this.Projectile.ai[0] == (double) (60 + num1))
        {
          SoundEngine.PlaySound(ref SoundID.Item82, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 unitY = Vector2.UnitY;
            float num2 = (float) Main.rand.Next(100);
            Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(Utils.RotatedByRandom(unitY, Math.PI / 4.0)), 5f);
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2, 466, this.Projectile.damage, 0.0f, Main.myPlayer, Utils.ToRotation(unitY), num2, 0.0f);
          }
        }
      }
      else
      {
        this.Projectile.scale = (float) (1.0 - ((double) this.Projectile.ai[0] - (double) num1) / 60.0) * 1f;
        this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale / 1.0);
        this.Projectile.rotation -= (float) Math.PI / 30f;
        if (this.Projectile.alpha >= (int) byte.MaxValue)
          this.Projectile.Kill();
        for (int index = 0; index < 2; ++index)
        {
          switch (Main.rand.Next(3))
          {
            case 0:
              Vector2 vector2_1 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
              Dust dust1 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_1, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
              dust1.noGravity = true;
              dust1.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_1, (float) Main.rand.Next(10, 21)));
              dust1.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_1, 1.5707963705062866, new Vector2()), 6f);
              dust1.scale = 0.5f + Utils.NextFloat(Main.rand);
              dust1.fadeIn = 0.5f;
              dust1.customData = (object) ((Entity) this.Projectile).Center;
              break;
            case 1:
              Vector2 vector2_2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
              Dust dust2 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_2, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
              dust2.noGravity = true;
              dust2.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_2, 30f));
              dust2.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_2, -1.5707963705062866, new Vector2()), 3f);
              dust2.scale = 0.5f + Utils.NextFloat(Main.rand);
              dust2.fadeIn = 0.5f;
              dust2.customData = (object) ((Entity) this.Projectile).Center;
              break;
          }
        }
      }
      Dust dust3 = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
      dust3.velocity = Vector2.op_Multiply(dust3.velocity, 5f);
      dust3.fadeIn = 1f;
      dust3.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
      dust3.noGravity = true;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(144, 360, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<LightningRodBuff>(), 360, true, false);
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
