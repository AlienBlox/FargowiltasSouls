// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MoonLordVortexOld
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Cosmos;
using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MoonLordVortexOld : CosmosVortex
  {
    public int suck;

    public override string Texture => "Terraria/Images/Projectile_578";

    public override bool? CanDamage() => new bool?((double) this.Projectile.localAI[1] > 120.0);

    public override void AI()
    {
      if (this.suck > 0)
        --this.suck;
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 398);
      if (npc != null)
      {
        ++this.Projectile.localAI[0];
        Vector2 vector2_1;
        vector2_1.X = 300f * (float) Math.Sin(Math.PI / 180.0 * (double) this.Projectile.localAI[0]);
        vector2_1.Y = 150f * (float) Math.Sin(Math.PI / 90.0 * (double) this.Projectile.localAI[0]);
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, vector2_1);
        if (npc.GetGlobalNPC<MoonLordCore>().VulnerabilityState != 1 && this.Projectile.timeLeft > 60)
          this.Projectile.timeLeft = 60;
        for (int index = 0; index < 10; ++index)
        {
          Vector2 vector2_2 = new Vector2();
          double num = Main.rand.NextDouble() * 2.0 * Math.PI;
          vector2_2.X += (float) (Math.Sin(num) * 150.0);
          vector2_2.Y += (float) (Math.Cos(num) * 150.0);
          Dust dust1 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), new Vector2(4f, 4f)), 0, 0, 229, 0.0f, 0.0f, 100, Color.White, 1f)];
          dust1.velocity = Vector2.op_Division(((Entity) npc).velocity, 3f);
          if (Utils.NextBool(Main.rand, 3))
          {
            Dust dust2 = dust1;
            dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.Normalize(vector2_2));
          }
          dust1.noGravity = true;
        }
        ++this.Projectile.localAI[1];
        if ((double) this.Projectile.localAI[1] <= 50.0)
        {
          if (Utils.NextBool(Main.rand, 4))
          {
            Vector2 vector2_3 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
            Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_3, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
            dust.noGravity = true;
            dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_3, (float) Main.rand.Next(10, 21)));
            dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_3, 1.5707963705062866, new Vector2()), 4f);
            dust.scale = 0.5f + Utils.NextFloat(Main.rand);
            dust.fadeIn = 0.5f;
          }
          if (Utils.NextBool(Main.rand, 4))
          {
            Vector2 vector2_4 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
            Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_4, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
            dust.noGravity = true;
            dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_4, 30f));
            dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_4, -1.5707963705062866, new Vector2()), 2f);
            dust.scale = 0.5f + Utils.NextFloat(Main.rand);
            dust.fadeIn = 0.5f;
          }
        }
        else if ((double) this.Projectile.localAI[1] <= 90.0)
        {
          this.Projectile.scale = (float) (((double) this.Projectile.localAI[1] - 50.0) / 40.0 * 3.0);
          this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale / 3.0);
          this.Projectile.rotation -= 0.1570796f;
          if (Utils.NextBool(Main.rand))
          {
            Vector2 vector2_5 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
            Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_5, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
            dust.noGravity = true;
            dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_5, (float) Main.rand.Next(10, 21)));
            dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_5, 1.5707963705062866, new Vector2()), 6f);
            dust.scale = 0.5f + Utils.NextFloat(Main.rand);
            dust.fadeIn = 0.5f;
            dust.customData = (object) ((Entity) this.Projectile).Center;
          }
          if (Utils.NextBool(Main.rand))
          {
            Vector2 vector2_6 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
            Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_6, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
            dust.noGravity = true;
            dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_6, 30f));
            dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_6, -1.5707963705062866, new Vector2()), 3f);
            dust.scale = 0.5f + Utils.NextFloat(Main.rand);
            dust.fadeIn = 0.5f;
            dust.customData = (object) ((Entity) this.Projectile).Center;
          }
          Suck();
        }
        else if ((double) this.Projectile.localAI[1] <= 1890.0)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.9f);
          this.Projectile.scale = 3f;
          this.Projectile.alpha = 0;
          this.Projectile.rotation -= (float) Math.PI / 60f;
          if (Utils.NextBool(Main.rand))
          {
            Vector2 vector2_7 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
            Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_7, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
            dust.noGravity = true;
            dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_7, (float) Main.rand.Next(10, 21)));
            dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_7, 1.5707963705062866, new Vector2()), 6f);
            dust.scale = 0.5f + Utils.NextFloat(Main.rand);
            dust.fadeIn = 0.5f;
            dust.customData = (object) ((Entity) this.Projectile).Center;
          }
          else
          {
            Vector2 vector2_8 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
            Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_8, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
            dust.noGravity = true;
            dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_8, 30f));
            dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_8, -1.5707963705062866, new Vector2()), 3f);
            dust.scale = 0.5f + Utils.NextFloat(Main.rand);
            dust.fadeIn = 0.5f;
            dust.customData = (object) ((Entity) this.Projectile).Center;
          }
          Suck();
        }
        else
        {
          this.Projectile.scale = (float) (1.0 - ((double) this.Projectile.localAI[1] - 1800.0) / 60.0) * 3f;
          this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale / 3.0);
          this.Projectile.rotation -= (float) Math.PI / 30f;
          if (this.Projectile.alpha >= (int) byte.MaxValue)
            this.Projectile.Kill();
          for (int index = 0; index < 2; ++index)
          {
            switch (Main.rand.Next(3))
            {
              case 0:
                Vector2 vector2_9 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
                Dust dust3 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_9, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
                dust3.noGravity = true;
                dust3.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_9, (float) Main.rand.Next(10, 21)));
                dust3.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_9, 1.5707963705062866, new Vector2()), 6f);
                dust3.scale = 0.5f + Utils.NextFloat(Main.rand);
                dust3.fadeIn = 0.5f;
                dust3.customData = (object) ((Entity) this.Projectile).Center;
                break;
              case 1:
                Vector2 vector2_10 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
                Dust dust4 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_10, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
                dust4.noGravity = true;
                dust4.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_10, 30f));
                dust4.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_10, -1.5707963705062866, new Vector2()), 3f);
                dust4.scale = 0.5f + Utils.NextFloat(Main.rand);
                dust4.fadeIn = 0.5f;
                dust4.customData = (object) ((Entity) this.Projectile).Center;
                break;
            }
          }
        }
        Dust dust5 = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
        dust5.velocity = Vector2.op_Multiply(dust5.velocity, 5f);
        dust5.fadeIn = 1f;
        dust5.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
        dust5.noGravity = true;
      }
      else
        this.Projectile.Kill();

      void Suck()
      {
        foreach (Projectile projectile1 in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.friendly && (double) ((Entity) p).Distance(((Entity) this.Projectile).Center) < 150.0 && !FargoSoulsUtil.IsSummonDamage(p) && FargoSoulsUtil.CanDeleteProjectile(p) && p.type != ModContent.ProjectileType<LunarCultistLightningArc>())))
        {
          ((Entity) projectile1).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) projectile1, ((Entity) this.Projectile).Center), ((Vector2) ref ((Entity) projectile1).velocity).Length());
          Projectile projectile2 = projectile1;
          ((Entity) projectile2).velocity = Vector2.op_Multiply(((Entity) projectile2).velocity, 1.015f);
          if (FargoSoulsUtil.HostCheck && this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) projectile1).Hitbox))
          {
            Player player = Main.player[projectile1.owner];
            if (((Entity) player).active && !player.dead && !player.ghost && this.suck <= 0)
            {
              this.suck = 6;
              Vector2 vector2_1 = Utils.RotatedByRandom(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center), (double) MathHelper.ToRadians(10f));
              float num = Utils.NextBool(Main.rand) ? 1f : -1f;
              Vector2 vector2_2 = Vector2.op_Multiply(Vector2.Normalize(vector2_1), 6f);
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_2, 6f), ModContent.ProjectileType<CosmosLightning>(), this.Projectile.damage, 0.0f, Main.myPlayer, Utils.ToRotation(vector2_1), num, 0.0f);
            }
            projectile1.Kill();
          }
        }
      }
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(144, 360, true, false);
    }

    public override void OnKill(int timeLeft)
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

    public override Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public override bool PreDraw(ref Color lightColor)
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
