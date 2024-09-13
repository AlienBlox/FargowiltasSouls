// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MoonLordVortex
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Cosmos;
using FargowiltasSouls.Content.Bosses.VanillaEternity;
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
  public class MoonLordVortex : CosmosVortex
  {
    public override string Texture => "Terraria/Images/Projectile_578";

    public override bool? CanDamage() => new bool?((double) this.Projectile.localAI[1] > 120.0);

    public override void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 398);
      if (npc != null && (double) npc.ai[0] != 2.0)
      {
        float num1 = (float) (15.0 * (1.0 - (double) (npc.life / npc.lifeMax)));
        if ((double) this.Projectile.localAI[1] == 0.0 && FargoSoulsUtil.HostCheck)
        {
          for (int index = -1; index <= 1; index += 2)
          {
            Vector2 vector2 = Utils.RotatedBy(Vector2.UnitY, (double) MathHelper.ToRadians(num1) * (double) index, new Vector2());
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2, ModContent.ProjectileType<GlowLine>(), 0, 0.0f, Main.myPlayer, 14f, (float) this.Projectile.identity, 0.0f);
          }
        }
        Projectile projectile1 = FargoSoulsUtil.ProjectileExists(this.Projectile.localAI[0], ModContent.ProjectileType<LunarRitual>());
        if (projectile1 == null)
        {
          for (int index = 0; index < Main.maxProjectiles; ++index)
          {
            if (((Entity) Main.projectile[index]).active && Main.projectile[index].type == ModContent.ProjectileType<LunarRitual>() && (double) Main.projectile[index].ai[1] == (double) ((Entity) npc).whoAmI)
            {
              this.Projectile.localAI[0] = (float) index;
              break;
            }
          }
        }
        if (projectile1 != null)
        {
          Vector2 center = ((Entity) projectile1).Center;
          center.X += 800f * this.Projectile.ai[0];
          center.Y -= 800f;
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Subtraction(center, ((Entity) this.Projectile).Center), 10f);
        }
        if ((double) this.Projectile.localAI[1] < 120.0)
        {
          float num2 = 0.5f;
          for (int index = 0; index < 5; ++index)
          {
            if ((double) Utils.NextFloat(Main.rand) >= (double) num2)
            {
              float num3 = Utils.NextFloat(Main.rand) * 6.283185f;
              float num4 = Utils.NextFloat(Main.rand);
              Dust dust = Dust.NewDustPerfect(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.ToRotationVector2(num3), (float) (110.0 + 600.0 * (double) num4))), 229, new Vector2?(Vector2.op_Multiply(Utils.ToRotationVector2(num3 - 3.141593f), (float) (14.0 + 8.0 * (double) num4))), 0, new Color(), 1f);
              dust.scale = 0.9f;
              dust.fadeIn = (float) (1.1499999761581421 + (double) num4 * 0.30000001192092896);
              dust.noGravity = true;
            }
          }
        }
        else if ((double) this.Projectile.localAI[1] > 180.0 && (double) this.Projectile.localAI[1] % 6.0 == 0.0 && FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 24f, 64f), Utils.RotatedByRandom(Vector2.UnitY, (double) MathHelper.ToRadians(num1)));
          float num5 = Utils.NextBool(Main.rand) ? 1f : -1f;
          Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2, ModContent.ProjectileType<HostileLightning>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, Utils.ToRotation(vector2), num5 * 0.75f, 0.0f);
        }
        if (projectile1 != null)
        {
          Projectile projectile2 = this.Projectile;
          ((Entity) projectile2).position = Vector2.op_Addition(((Entity) projectile2).position, Vector2.op_Subtraction(((Entity) projectile1).position, ((Entity) projectile1).oldPosition));
        }
        if (npc.GetGlobalNPC<MoonLordCore>().VulnerabilityState != 1 && this.Projectile.timeLeft > 60)
          this.Projectile.timeLeft = 60;
        ++this.Projectile.localAI[1];
        if ((double) this.Projectile.localAI[1] <= 50.0)
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
        else if ((double) this.Projectile.localAI[1] <= 90.0)
        {
          this.Projectile.scale = (float) (((double) this.Projectile.localAI[1] - 50.0) / 40.0 * 3.0);
          this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale / 3.0);
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
        else if ((double) this.Projectile.localAI[1] <= 1890.0)
        {
          Projectile projectile3 = this.Projectile;
          ((Entity) projectile3).velocity = Vector2.op_Multiply(((Entity) projectile3).velocity, 0.9f);
          this.Projectile.scale = 3f;
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
      else
        this.Projectile.Kill();
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
