// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.PlanterasChild
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class PlanterasChild : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 46;
      ((Entity) this.Projectile).height = 46;
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
      if (((Entity) player).active && !player.dead && player.FargoSouls().PlanterasChild)
        this.Projectile.timeLeft = 2;
      NPC minionAttackTargetNpc = this.Projectile.OwnerMinionAttackTargetNPC;
      if (minionAttackTargetNpc != null && (double) this.Projectile.ai[0] != (double) ((Entity) minionAttackTargetNpc).whoAmI && minionAttackTargetNpc.CanBeChasedBy((object) null, false))
      {
        this.Projectile.ai[0] = (float) ((Entity) minionAttackTargetNpc).whoAmI;
        this.Projectile.netUpdate = true;
      }
      if ((double) this.Projectile.ai[0] >= 0.0 && (double) this.Projectile.ai[0] < (double) Main.maxNPCs)
      {
        NPC npc = Main.npc[(int) this.Projectile.ai[0]];
        if (npc.CanBeChasedBy((object) null, false))
        {
          Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center);
          float num1 = ((Vector2) ref vector2_1).Length();
          if ((double) num1 > 1000.0)
          {
            this.Projectile.ai[0] = -1f;
            this.Projectile.netUpdate = true;
          }
          else if ((double) num1 > 50.0)
          {
            ((Vector2) ref vector2_1).Normalize();
            vector2_1 = Vector2.op_Multiply(vector2_1, 16f);
            ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 40f), vector2_1), 41f);
          }
          ++this.Projectile.localAI[0];
          if ((double) this.Projectile.localAI[0] > 15.0)
          {
            this.Projectile.localAI[0] = 0.0f;
            if (this.Projectile.owner == Main.myPlayer)
            {
              Vector2 velocity = ((Entity) this.Projectile).velocity;
              ((Vector2) ref velocity).Normalize();
              Vector2 vector2_2 = Vector2.op_Multiply(velocity, 17f);
              float num2 = (float) ((double) this.Projectile.damage * 2.0 / 3.0);
              int num3;
              if (Utils.NextBool(Main.rand))
              {
                num2 = (float) ((double) num2 * 5.0 / 4.0);
                num3 = ModContent.ProjectileType<PoisonSeedPlanterasChild>();
                SoundEngine.PlaySound(ref SoundID.Item17, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
              }
              else if (Utils.NextBool(Main.rand, 6))
              {
                num2 = (float) ((double) num2 * 3.0 / 2.0);
                num3 = ModContent.ProjectileType<SpikyBallPlanterasChild>();
              }
              else
              {
                num3 = ModContent.ProjectileType<SeedPlanterasChild>();
                SoundEngine.PlaySound(ref SoundID.Item17, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
              }
              if (this.Projectile.owner == Main.myPlayer)
                Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2_2, num3, (int) num2, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
            }
          }
        }
        else
        {
          this.Projectile.ai[0] = -1f;
          this.Projectile.netUpdate = true;
        }
      }
      else
      {
        Vector2 vector2_3 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.Projectile).Center);
        vector2_3.Y -= 50f;
        float num = ((Vector2) ref vector2_3).Length();
        if ((double) num > 2000.0)
        {
          ((Entity) this.Projectile).Center = ((Entity) player).Center;
          this.Projectile.ai[0] = -1f;
          this.Projectile.netUpdate = true;
        }
        else if ((double) num > 70.0)
        {
          ((Vector2) ref vector2_3).Normalize();
          Vector2 vector2_4 = Vector2.op_Multiply(vector2_3, (double) num > 200.0 ? 10f : 6f);
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 40f), vector2_4), 41f);
        }
        ++this.Projectile.localAI[1];
        if ((double) this.Projectile.localAI[1] > 6.0)
        {
          this.Projectile.localAI[1] = 0.0f;
          this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1000f, true, ((Entity) player).Center);
          this.Projectile.netUpdate = true;
        }
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
    }

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(ModContent.BuffType<InfestedBuff>(), 360, false);
      target.AddBuff(70, 360, false);
      target.AddBuff(20, 360, false);
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
