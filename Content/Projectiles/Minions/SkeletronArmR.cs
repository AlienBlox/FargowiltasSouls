// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.SkeletronArmR
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class SkeletronArmR : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/NPC_36";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 52;
      ((Entity) this.Projectile).height = 52;
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

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (((Entity) player).active && !player.dead && player.FargoSouls().SkeletronArms)
        this.Projectile.timeLeft = 2;
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) player).position, ((Entity) player).oldPosition);
      Projectile projectile1 = this.Projectile;
      ((Entity) projectile1).position = Vector2.op_Addition(((Entity) projectile1).position, vector2_1);
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] >= 0.0)
      {
        Vector2 center = ((Entity) player).Center;
        center.X += 200f;
        center.Y -= 50f;
        Vector2 vector2_2 = Vector2.op_Subtraction(center, ((Entity) this.Projectile).Center);
        float num = ((Vector2) ref vector2_2).Length();
        ((Vector2) ref vector2_2).Normalize();
        if ((double) this.Projectile.ai[0] == 0.0)
        {
          if ((double) num > 15.0)
          {
            this.Projectile.ai[0] = -1f;
            if ((double) num > 1300.0)
            {
              this.Projectile.Kill();
              return;
            }
          }
          else
          {
            ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
            Projectile projectile2 = this.Projectile;
            ((Entity) projectile2).velocity = Vector2.op_Multiply(((Entity) projectile2).velocity, 3f + Utils.NextFloat(Main.rand, 3f));
            this.Projectile.netUpdate = true;
          }
        }
        else
          vector2_2 = Vector2.op_Division(vector2_2, 8f);
        if ((double) num > 120.0)
        {
          this.Projectile.ai[0] = -1f;
          this.Projectile.netUpdate = true;
        }
        Projectile projectile3 = this.Projectile;
        ((Entity) projectile3).velocity = Vector2.op_Addition(((Entity) projectile3).velocity, vector2_2);
        if ((double) num > 30.0)
        {
          Projectile projectile4 = this.Projectile;
          ((Entity) projectile4).velocity = Vector2.op_Multiply(((Entity) projectile4).velocity, 0.96f);
        }
        if ((double) this.Projectile.ai[0] > 90.0)
        {
          this.Projectile.ai[0] = 20f;
          NPC npc = FargoSoulsUtil.NPCExists(FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 400f, center: new Vector2()), Array.Empty<int>());
          if (npc != null)
          {
            ((Entity) this.Projectile).velocity = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center);
            ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
            Projectile projectile5 = this.Projectile;
            ((Entity) projectile5).velocity = Vector2.op_Multiply(((Entity) projectile5).velocity, 16f);
            Projectile projectile6 = this.Projectile;
            ((Entity) projectile6).velocity = Vector2.op_Addition(((Entity) projectile6).velocity, Vector2.op_Division(((Entity) npc).velocity, 2f));
            Projectile projectile7 = this.Projectile;
            ((Entity) projectile7).velocity = Vector2.op_Subtraction(((Entity) projectile7).velocity, Vector2.op_Division(vector2_1, 2f));
            this.Projectile.ai[0] *= -1f;
          }
          this.Projectile.netUpdate = true;
        }
      }
      Vector2 vector2_3 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.Projectile).Center);
      vector2_3.X += 200f;
      vector2_3.Y += 180f;
      this.Projectile.rotation = (float) Math.Atan2((double) vector2_3.Y, (double) vector2_3.X) + 1.57079637f;
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      ((NPC.HitModifiers) ref modifiers).HitDirectionOverride = new int?(Math.Sign(((Entity) target).Center.X - ((Entity) Main.player[this.Projectile.owner]).Center.X));
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
