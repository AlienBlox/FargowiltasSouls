// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.LightningVortex
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class LightningVortex : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_578";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.friendly = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = (int) byte.MaxValue;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1]);
      if (npc == null || !npc.CanBeChasedBy((object) null, false))
      {
        this.Projectile.ai[1] = (float) FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 1000f);
        this.Projectile.netUpdate = true;
      }
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
        if (!Utils.NextBool(Main.rand, 4))
          return;
        Vector2 vector2_1 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
        Dust dust1 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_1, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
        dust1.noGravity = true;
        dust1.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_1, 30f));
        dust1.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_1, -1.5707963705062866, new Vector2()), 2f);
        dust1.scale = 0.5f + Utils.NextFloat(Main.rand);
        dust1.fadeIn = 0.5f;
      }
      else if ((double) this.Projectile.ai[0] <= 90.0)
      {
        this.Projectile.scale = (float) (((double) this.Projectile.ai[0] - 50.0) / 40.0);
        this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale);
        this.Projectile.rotation -= 0.1570796f;
        if (Utils.NextBool(Main.rand))
        {
          Vector2 vector2 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
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
          Vector2 vector2 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
          Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
          dust.noGravity = true;
          dust.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 30f));
          dust.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, -1.5707963705062866, new Vector2()), 3f);
          dust.scale = 0.5f + Utils.NextFloat(Main.rand);
          dust.fadeIn = 0.5f;
          dust.customData = (object) ((Entity) this.Projectile).Center;
        }
        if ((double) this.Projectile.ai[0] != 90.0 || (double) this.Projectile.ai[1] == -1.0 || !FargoSoulsUtil.HostCheck || npc == null)
          return;
        Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center);
        ((Vector2) ref vector2_2).Normalize();
        Vector2 vector2_3 = Vector2.op_Multiply(vector2_2, 8f);
        float num = (float) Main.rand.Next(80);
        int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
        if (closest == -1 || !((Entity) Main.player[closest]).active || Main.player[closest].dead || Main.player[closest].ghost || (double) ((Entity) this.Projectile).Distance(((Entity) Main.player[closest]).Center) >= 1000.0)
          return;
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center.X - vector2_3.X, ((Entity) this.Projectile).Center.Y - vector2_3.Y, vector2_3.X, vector2_3.Y, ModContent.ProjectileType<LightningArc>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, Utils.ToRotation(vector2_2), num, 0.0f);
      }
      else if ((double) this.Projectile.ai[0] <= 120.0)
      {
        this.Projectile.scale = 1f;
        this.Projectile.alpha = 0;
        this.Projectile.rotation -= (float) Math.PI / 60f;
        if (Utils.NextBool(Main.rand))
        {
          Vector2 vector2 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
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
          Vector2 vector2 = Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515);
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
        this.Projectile.scale = (float) (1.0 - ((double) this.Projectile.ai[0] - 120.0) / 60.0);
        this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale);
        this.Projectile.rotation -= (float) Math.PI / 30f;
        if (this.Projectile.alpha >= (int) byte.MaxValue)
          this.Projectile.Kill();
        for (int index = 0; index < 2; ++index)
        {
          switch (Main.rand.Next(3))
          {
            case 0:
              Vector2 vector2_4 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
              Dust dust2 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_4, 30f)), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f)];
              dust2.noGravity = true;
              dust2.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_4, (float) Main.rand.Next(10, 21)));
              dust2.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_4, 1.5707963705062866, new Vector2()), 6f);
              dust2.scale = 0.5f + Utils.NextFloat(Main.rand);
              dust2.fadeIn = 0.5f;
              dust2.customData = (object) ((Entity) this.Projectile).Center;
              break;
            case 1:
              Vector2 vector2_5 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 6.28318548202515), this.Projectile.scale);
              Dust dust3 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_5, 30f)), 0, 0, 240, 0.0f, 0.0f, 0, new Color(), 1f)];
              dust3.noGravity = true;
              dust3.position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_5, 30f));
              dust3.velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2_5, -1.5707963705062866, new Vector2()), 3f);
              dust3.scale = 0.5f + Utils.NextFloat(Main.rand);
              dust3.fadeIn = 0.5f;
              dust3.customData = (object) ((Entity) this.Projectile).Center;
              break;
          }
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
