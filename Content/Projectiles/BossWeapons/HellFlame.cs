// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HellFlame
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HellFlame : ModProjectile
  {
    public int targetID = -1;
    public int searchTimer = 18;

    public virtual string Texture => "Terraria/Images/Projectile_687";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 3;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      Main.projFrames[this.Projectile.type] = Main.projFrames[645];
    }

    public virtual void SendExtraAI(BinaryWriter writer) => writer.Write(this.targetID);

    public virtual void ReceiveExtraAI(BinaryReader reader) => this.targetID = reader.ReadInt32();

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.friendly = true;
      this.Projectile.alpha = 0;
      this.Projectile.penetrate = -1;
      this.Projectile.extraUpdates = 1;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.aiStyle = -1;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.hide = true;
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      behindProjectiles.Add(index);
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.frame = (int) this.Projectile.ai[0];
        this.Projectile.ai[0] = -1f;
        this.Projectile.localAI[0] = Utils.NextFloat(Main.rand, 0.25f, 2f);
        this.searchTimer = Main.rand.Next(18);
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 6.28318548f);
      }
      if (this.Projectile.timeLeft > 120)
        this.Projectile.timeLeft = 120;
      ++this.Projectile.ai[1];
      this.Projectile.scale = (float) (1.0 + (double) this.Projectile.ai[1] / 80.0);
      this.Projectile.rotation += 0.3f * (float) ((Entity) this.Projectile).direction;
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter > 17)
      {
        ++this.Projectile.frame;
        this.Projectile.frameCounter = 0;
      }
      if (this.Projectile.frame > 6)
        this.Projectile.Kill();
      if (this.Projectile.frame > 4)
        this.Projectile.alpha = 155;
      else if (this.targetID == -1)
      {
        if (this.searchTimer == 0)
        {
          this.searchTimer = 18;
          this.targetID = FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 300f);
          this.Projectile.netUpdate = true;
        }
        --this.searchTimer;
      }
      else
      {
        NPC npc = Main.npc[this.targetID];
        if (((Entity) npc).active && npc.CanBeChasedBy((object) null, false))
        {
          Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center);
          double num1 = (double) Utils.ToRotation(vector2_1) - (double) Utils.ToRotation(((Entity) this.Projectile).velocity);
          if (num1 > Math.PI)
            num1 -= 2.0 * Math.PI;
          if (num1 < -1.0 * Math.PI)
            num1 += 2.0 * Math.PI;
          if ((double) this.Projectile.ai[0] == -1.0)
          {
            if (Math.Abs(num1) > 3.0 * Math.PI / 4.0)
            {
              ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, num1 * 0.07 * (double) this.Projectile.localAI[0], new Vector2());
            }
            else
            {
              float num2 = ((Vector2) ref vector2_1).Length();
              float num3 = 12.7f / num2;
              Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(vector2_1, num3), 7f);
              Projectile projectile1 = this.Projectile;
              ((Entity) projectile1).velocity = Vector2.op_Addition(((Entity) projectile1).velocity, vector2_2);
              if ((double) num2 <= 70.0)
                return;
              Projectile projectile2 = this.Projectile;
              ((Entity) projectile2).velocity = Vector2.op_Multiply(((Entity) projectile2).velocity, 0.977f);
            }
          }
          else
            ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, num1 * 0.1 * (double) this.Projectile.localAI[0], new Vector2());
        }
        else
        {
          this.targetID = -1;
          this.searchTimer = 0;
          this.Projectile.netUpdate = true;
        }
      }
    }

    public virtual void ModifyDamageHitbox(ref Rectangle hitbox)
    {
      hitbox.X -= 30;
      hitbox.Y -= 30;
      hitbox.Width += 60;
      hitbox.Height += 60;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 5;
      target.AddBuff(24, 180, false);
      target.AddBuff(204, 180, false);
      target.AddBuff(203, 180, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Color alpha = this.Projectile.GetAlpha(Color.Fuchsia);
      float scale = this.Projectile.scale;
      Vector2 vector2_2 = ((Entity) this.Projectile).Center;
      if (Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero) && !Utils.HasNaNs(((Entity) this.Projectile).velocity))
        vector2_2 = Vector2.op_Subtraction(vector2_2, Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), 4f));
      float num3 = this.Projectile.rotation + Main.GlobalTimeWrappedHourly * 0.6f;
      Vector2 vector2_3 = Vector2.op_Subtraction(this.Projectile.oldPos[2], ((Entity) this.Projectile).position);
      float num4 = this.Projectile.oldRot[2] + Main.GlobalTimeWrappedHourly * 0.6f;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_3, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, num4, vector2_1, scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_3, ((Entity) this.Projectile).Center), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(Color.Black), num4, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, num3, vector2_1, scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(Color.Black), num3, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
