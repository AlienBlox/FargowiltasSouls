// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.LunarCultistLightningOrb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class LunarCultistLightningOrb : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_465";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 4;
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 80;
      ((Entity) this.Projectile).height = 80;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 240;
      this.Projectile.penetrate = -1;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
      Mod mod;
      if (!Terraria.ModLoader.ModLoader.TryGetMod("Fargowiltas", ref mod))
        return;
      mod.Call(new object[2]
      {
        (object) "LowRenderProj",
        (object) this.Projectile
      });
    }

    public virtual void AI()
    {
      this.Projectile.alpha += this.Projectile.timeLeft > 51 ? -10 : 10;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      if (this.Projectile.alpha > (int) byte.MaxValue)
        this.Projectile.alpha = (int) byte.MaxValue;
      if (this.Projectile.timeLeft % 30 == 0)
      {
        int num1 = -1;
        Projectile projectile = FargoSoulsUtil.ProjectileExists(this.Projectile.ai[0], ModContent.ProjectileType<LunarCultist>());
        if (projectile != null)
        {
          NPC npc = FargoSoulsUtil.NPCExists(projectile.ai[0]);
          if (npc != null)
          {
            num1 = ((Entity) npc).whoAmI;
            if (npc.CanBeChasedBy((object) null, false) && Collision.CanHitLine(((Entity) this.Projectile).Center, 0, 0, ((Entity) npc).Center, 0, 0))
            {
              Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center);
              float num2 = (float) Main.rand.Next(100);
              Vector2 vector2_2 = Vector2.op_Multiply(Vector2.Normalize(Utils.RotatedByRandom(vector2_1, Math.PI / 4.0)), 7f);
              if (this.Projectile.owner == Main.myPlayer)
                Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2_2, ModContent.ProjectileType<LunarCultistLightningArc>(), this.Projectile.damage, this.Projectile.knockBack / 2f, this.Projectile.owner, Utils.ToRotation(vector2_1), num2, 0.0f);
            }
          }
        }
        float num3 = 2000f;
        int index = -1;
        foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => n.CanBeChasedBy((object) null, false) && Collision.CanHitLine(((Entity) this.Projectile).Center, 0, 0, ((Entity) n).Center, 0, 0))))
        {
          float num4 = ((Entity) this.Projectile).Distance(((Entity) npc).Center);
          if ((double) num4 < (double) num3 && ((Entity) npc).whoAmI != num1)
          {
            num3 = num4;
            index = ((Entity) npc).whoAmI;
          }
        }
        if (index != -1)
        {
          Vector2 vector2_3 = Vector2.op_Subtraction(((Entity) Main.npc[index]).Center, ((Entity) this.Projectile).Center);
          float num5 = (float) Main.rand.Next(100);
          Vector2 vector2_4 = Vector2.op_Multiply(Vector2.Normalize(Utils.RotatedByRandom(vector2_3, Math.PI / 4.0)), 7f);
          if (this.Projectile.owner == Main.myPlayer)
            Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2_4, ModContent.ProjectileType<LunarCultistLightningArc>(), this.Projectile.damage, this.Projectile.knockBack / 2f, this.Projectile.owner, Utils.ToRotation(vector2_3), num5, 0.0f);
        }
      }
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.4f, 0.85f, 0.9f);
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter > 3)
      {
        this.Projectile.frameCounter = 0;
        ++this.Projectile.frame;
        if (this.Projectile.frame > 3)
          this.Projectile.frame = 0;
      }
      float num6 = (float) (Main.rand.NextDouble() * 1.0 - 0.5);
      if ((double) num6 < -0.5)
        num6 = -0.5f;
      if ((double) num6 > 0.5)
        num6 = 0.5f;
      Vector2 vector2_5 = Utils.RotatedBy(Utils.RotatedBy(new Vector2((float) -((Entity) this.Projectile).width * 0.2f * this.Projectile.scale, 0.0f), (double) num6 * 6.28318548202515, new Vector2()), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2());
      int index1 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.One, 5f)), 10, 10, 226, (float) (-(double) ((Entity) this.Projectile).velocity.X / 3.0), (float) (-(double) ((Entity) this.Projectile).velocity.Y / 3.0), 150, Color.Transparent, 0.7f);
      Main.dust[index1].position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_5);
      Main.dust[index1].velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Main.dust[index1].position, ((Entity) this.Projectile).Center)), 2f);
      Main.dust[index1].noGravity = true;
      float num7 = (float) (Main.rand.NextDouble() * 1.0 - 0.5);
      if ((double) num7 < -0.5)
        num7 = -0.5f;
      if ((double) num7 > 0.5)
        num7 = 0.5f;
      Vector2 vector2_6 = Utils.RotatedBy(Utils.RotatedBy(new Vector2((float) -((Entity) this.Projectile).width * 0.6f * this.Projectile.scale, 0.0f), (double) num7 * 6.28318548202515, new Vector2()), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2());
      int index2 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.One, 5f)), 10, 10, 226, (float) (-(double) ((Entity) this.Projectile).velocity.X / 3.0), (float) (-(double) ((Entity) this.Projectile).velocity.Y / 3.0), 150, Color.Transparent, 0.7f);
      Main.dust[index2].velocity = Vector2.Zero;
      Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_6);
      Main.dust[index2].noGravity = true;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue)));
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
