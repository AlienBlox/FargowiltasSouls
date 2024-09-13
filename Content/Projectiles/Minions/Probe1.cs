// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.Probe1
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
  public class Probe1 : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/NPC_139";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.timeLeft *= 5;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (((Entity) player).whoAmI == Main.myPlayer && ((Entity) player).active && !player.dead && player.FargoSouls().Probes)
        this.Projectile.timeLeft = 2;
      this.Projectile.ai[0] -= (float) Math.PI / 60f;
      ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) player).Center, Utils.RotatedBy(new Vector2(60f, 0.0f), (double) this.Projectile.ai[0], new Vector2()));
      if (this.Projectile.owner != Main.myPlayer)
        return;
      if ((double) this.Projectile.ai[1] > 0.0)
      {
        --this.Projectile.ai[1];
        if ((double) this.Projectile.ai[1] % 10.0 == 0.0)
        {
          List<NPC> list = ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => n.CanBeChasedBy((object) null, false) && (double) ((Entity) this.Projectile).Distance(((Entity) n).Center) < 1200.0 && Collision.CanHitLine(((Entity) this.Projectile).Center, 0, 0, ((Entity) n).Center, 0, 0))).ToList<NPC>();
          if (list.Count > 0)
          {
            this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Utils.Next<NPC>(Main.rand, (IList<NPC>) list)).Center));
            int index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Utils.RotatedByRandom(Utils.RotatedBy(new Vector2(16f, 0.0f), (double) this.Projectile.rotation, new Vector2()), 0.39269909262657166), ModContent.ProjectileType<LightningArc>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, this.Projectile.rotation, (float) Main.rand.Next(100), 0.0f);
            if (index != Main.maxProjectiles)
              Main.projectile[index].DamageType = this.Projectile.DamageType;
            this.Projectile.rotation += 3.14159274f;
          }
        }
      }
      else
      {
        this.Projectile.rotation = Utils.ToRotation(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) this.Projectile).Center));
        if ((double) --this.Projectile.localAI[0] < 0.0)
        {
          if (player.controlUseItem && player.HeldItem.IsWeapon())
          {
            this.Projectile.localAI[0] = player.FargoSouls().MasochistSoul ? 15f : 30f;
            Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Utils.RotatedBy(new Vector2(8f, 0.0f), (double) this.Projectile.rotation, new Vector2()), ModContent.ProjectileType<ProbeLaser>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
            this.Projectile.netUpdate = true;
          }
          else
            this.Projectile.localAI[0] = 0.0f;
        }
        this.Projectile.rotation += 3.14159274f;
      }
      if ((double) ++this.Projectile.localAI[1] <= 20.0)
        return;
      this.Projectile.localAI[1] = 0.0f;
      this.Projectile.netUpdate = true;
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
