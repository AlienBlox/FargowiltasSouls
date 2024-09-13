// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.OpticRetinazer
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
  public class OpticRetinazer : ModProjectile
  {
    private bool spawn;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.MinionTargettingFeature[this.Projectile.type] = true;
      ProjectileID.Sets.MinionSacrificable[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 100;
      ((Entity) this.Projectile).height = 100;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.minion = true;
      this.Projectile.minionSlots = 1.5f;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.netImportant = true;
      this.Projectile.scale = 0.5f;
    }

    public virtual void AI()
    {
      if (!this.spawn)
      {
        this.spawn = true;
        this.Projectile.ai[0] = -1f;
      }
      Player player = Main.player[this.Projectile.owner];
      if (((Entity) player).active && !player.dead && player.FargoSouls().TwinsEX)
        this.Projectile.timeLeft = 2;
      if ((double) this.Projectile.ai[0] >= 0.0 && (double) this.Projectile.ai[0] < (double) Main.maxNPCs)
      {
        NPC minionAttackTargetNpc = this.Projectile.OwnerMinionAttackTargetNPC;
        if (minionAttackTargetNpc != null && (double) this.Projectile.ai[0] != (double) ((Entity) minionAttackTargetNpc).whoAmI && minionAttackTargetNpc.CanBeChasedBy((object) this.Projectile, false))
          this.Projectile.ai[0] = (float) ((Entity) minionAttackTargetNpc).whoAmI;
        NPC npc = Main.npc[(int) this.Projectile.ai[0]];
        if (npc.CanBeChasedBy((object) this.Projectile, false))
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Division(((Entity) npc).velocity, 4f));
          Vector2 vector2 = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(((Entity) npc).velocity, this.Projectile.ai[1]));
          Vector2 targetPos = Vector2.op_Addition(vector2, Vector2.op_Multiply(((Entity) this.Projectile).DirectionFrom(vector2), 300f));
          if ((double) ((Entity) this.Projectile).Distance(targetPos) > 50.0)
            this.Movement(targetPos, 0.5f);
          this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, vector2)) - 1.57079637f;
          if ((double) ++this.Projectile.localAI[0] > 15.0)
          {
            this.Projectile.localAI[0] = 0.0f;
            if (this.Projectile.owner == Main.myPlayer)
            {
              Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, vector2), 12f), ModContent.ProjectileType<OpticLaser>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
              this.Projectile.ai[1] = Utils.NextFloat(Main.rand, 10f, 30f);
              this.Projectile.netUpdate = true;
            }
          }
        }
        else
        {
          this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1500f, center: new Vector2());
          this.Projectile.netUpdate = true;
        }
      }
      else
      {
        this.Projectile.ai[1] = 0.0f;
        Vector2 center = ((Entity) player).Center;
        center.Y -= 100f;
        if ((double) ((Entity) this.Projectile).Distance(center) > 3000.0)
          ((Entity) this.Projectile).Center = ((Entity) player).Center;
        else if ((double) ((Entity) this.Projectile).Distance(center) > 200.0)
          this.Movement(center, 0.5f);
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
        if ((double) ++this.Projectile.localAI[1] > 6.0)
        {
          this.Projectile.localAI[1] = 0.0f;
          this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1500f, center: new Vector2());
          if ((double) this.Projectile.ai[0] != -1.0)
            this.Projectile.netUpdate = true;
        }
      }
      if (++this.Projectile.frameCounter > 4)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= 6)
          this.Projectile.frame = 3;
      }
      if (this.Projectile.frame < 3)
        this.Projectile.frame = 3;
      int otherMinion = ModContent.ProjectileType<OpticSpazmatism>();
      foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.owner == this.Projectile.owner && (p.type == this.Projectile.type || p.type == otherMinion) && ((Entity) p).whoAmI != ((Entity) this.Projectile).whoAmI && (double) ((Entity) p).Distance(((Entity) this.Projectile).Center) < (double) ((Entity) this.Projectile).width)))
      {
        ((Entity) this.Projectile).velocity.X += (float) (0.05000000074505806 * ((double) ((Entity) this.Projectile).Center.X < (double) ((Entity) projectile).Center.X ? -1.0 : 1.0));
        ((Entity) this.Projectile).velocity.Y += (float) (0.05000000074505806 * ((double) ((Entity) this.Projectile).Center.Y < (double) ((Entity) projectile).Center.Y ? -1.0 : 1.0));
        ((Entity) projectile).velocity.X += (float) (0.05000000074505806 * ((double) ((Entity) projectile).Center.X < (double) ((Entity) this.Projectile).Center.X ? -1.0 : 1.0));
        ((Entity) projectile).velocity.Y += (float) (0.05000000074505806 * ((double) ((Entity) projectile).Center.Y < (double) ((Entity) this.Projectile).Center.Y ? -1.0 : 1.0));
      }
    }

    private void Movement(Vector2 targetPos, float speedModifier)
    {
      if ((double) ((Entity) this.Projectile).Center.X < (double) targetPos.X)
      {
        ((Entity) this.Projectile).velocity.X += speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.X < 0.0)
          ((Entity) this.Projectile).velocity.X += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.Projectile).velocity.X -= speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.X > 0.0)
          ((Entity) this.Projectile).velocity.X -= speedModifier * 2f;
      }
      if ((double) ((Entity) this.Projectile).Center.Y < (double) targetPos.Y)
      {
        ((Entity) this.Projectile).velocity.Y += speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.Y < 0.0)
          ((Entity) this.Projectile).velocity.Y += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.Projectile).velocity.Y -= speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.Y > 0.0)
          ((Entity) this.Projectile).velocity.Y -= speedModifier * 2f;
      }
      if ((double) Math.Abs(((Entity) this.Projectile).velocity.X) > 24.0)
        ((Entity) this.Projectile).velocity.X = (float) (24 * Math.Sign(((Entity) this.Projectile).velocity.X));
      if ((double) Math.Abs(((Entity) this.Projectile).velocity.Y) <= 24.0)
        return;
      ((Entity) this.Projectile).velocity.Y = (float) (24 * Math.Sign(((Entity) this.Projectile).velocity.Y));
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 8;
      target.AddBuff(69, 600, false);
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
