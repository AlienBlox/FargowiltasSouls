// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.DevAesthetic.DevRocket
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
namespace FargowiltasSouls.Content.Patreon.DevAesthetic
{
  internal class DevRocket : ModProjectile
  {
    private Color color;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 24;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(616);
      this.Projectile.aiStyle = -1;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.penetrate = 2;
      this.Projectile.timeLeft = 75 * (this.Projectile.extraUpdates + 1);
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 15;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = ((Vector2) ref ((Entity) this.Projectile).velocity).Length() * (Utils.NextBool(Main.rand) ? 1f : -1f);
        this.color = new Color(50 * Main.rand.Next(6) + 5, 50 * Main.rand.Next(6) + 5, 50 * Main.rand.Next(6) + 5);
        this.Projectile.ai[0] = (float) Main.rand.Next(-30, 30);
        this.Projectile.ai[1] = -1f;
      }
      this.Projectile.alpha -= 25;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      if ((double) ++this.Projectile.ai[0] > 30.0)
      {
        this.Projectile.ai[0] = 20f;
        if (this.Projectile.timeLeft > 45 * this.Projectile.MaxUpdates)
          this.Projectile.timeLeft = 45 * this.Projectile.MaxUpdates;
        if ((double) this.Projectile.ai[1] == -1.0)
        {
          this.Projectile.ai[1] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1000f, true, new Vector2());
          this.Projectile.netUpdate = true;
        }
      }
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1]);
      if (npc != null && npc.CanBeChasedBy((object) null, false))
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Division(((Entity) npc).velocity, 5f));
        Vector2 vector2_1 = ((Entity) npc).Center;
        float num = (float) (120 * this.Projectile.timeLeft / (30 * this.Projectile.MaxUpdates) * 2);
        if ((double) ((Entity) this.Projectile).Distance(vector2_1) > (double) num)
          vector2_1 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), 1.5707963705062866, new Vector2()), num), (float) Math.Sign(this.Projectile.localAI[0])));
        Vector2 vector2_2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, vector2_1), Math.Abs(this.Projectile.localAI[0]));
        ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 7f), vector2_2), 8f);
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
    }

    public virtual void OnKill(int timeLeft)
    {
      if (this.Projectile.owner == Main.myPlayer && (double) this.Projectile.localAI[1] == 1.0)
      {
        foreach (Projectile projectile in FargoSoulsUtil.XWay(Main.rand.Next(3, 7), Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, this.Projectile.type, 6f, this.Projectile.damage, this.Projectile.knockBack))
        {
          if (projectile != null)
            projectile.localAI[1] = 2f;
        }
      }
      Color color = this.color;
      ((Color) ref color).A = (byte) 100;
      for (int index1 = 0; index1 < 2; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 76, ((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y, 100, color, 2f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
        Main.dust[index2].noGravity = true;
      }
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      if ((double) ((Entity) this.Projectile).velocity.X != (double) oldVelocity.X)
        ((Entity) this.Projectile).velocity.X = -oldVelocity.X;
      if ((double) ((Entity) this.Projectile).velocity.Y != (double) oldVelocity.Y)
        ((Entity) this.Projectile).velocity.Y = -oldVelocity.Y;
      return false;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      this.Projectile.timeLeft = 0;
      if ((double) this.Projectile.localAI[1] == 0.0)
        this.Projectile.localAI[1] = 1f;
      target.immune[this.Projectile.owner] = 3;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(this.color, this.Projectile.Opacity));
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
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color1 = Color.op_Multiply(alpha, 0.75f);
        ((Color) ref color1).A = (byte) 0;
        Color color2 = Color.op_Multiply(color1, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        float scale = this.Projectile.scale;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, num3, vector2, scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
