// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.StyxScythe
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class StyxScythe : ModProjectile
  {
    public int rotationDirection;

    public virtual string Texture => "Terraria/Images/Projectile_274";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 40;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 420;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.extraUpdates = 1;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.Projectile.localAI[1]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.Projectile.localAI[1] = reader.ReadSingle();
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 6.28318548f);
        this.rotationDirection = Math.Sign(this.Projectile.ai[1]);
        SoundEngine.PlaySound(ref SoundID.Item71, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if ((double) this.Projectile.localAI[0] == 1.0)
      {
        this.Projectile.ai[0] += ((Vector2) ref ((Entity) this.Projectile).velocity).Length();
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) Main.player[this.Projectile.owner]).Center, Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), this.Projectile.ai[0]));
        if ((double) ((Entity) this.Projectile).Distance(((Entity) Main.player[this.Projectile.owner]).Center) > (double) Math.Abs(this.Projectile.ai[1]))
        {
          ++this.Projectile.localAI[0];
          this.Projectile.localAI[1] = (float) Math.Sign(this.Projectile.ai[1]);
          this.Projectile.ai[0] = Math.Abs(this.Projectile.ai[1]) - ((Vector2) ref ((Entity) this.Projectile).velocity).Length();
          this.Projectile.ai[1] = 0.0f;
          this.Projectile.netUpdate = true;
        }
      }
      else if ((double) this.Projectile.localAI[0] == 2.0)
      {
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) Main.player[this.Projectile.owner]).Center, Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), this.Projectile.ai[0]));
        Projectile projectile = this.Projectile;
        ((Entity) projectile).Center = Vector2.op_Addition(((Entity) projectile).Center, Utils.RotatedBy(((Entity) this.Projectile).velocity, Math.PI / 2.0 * (double) this.Projectile.localAI[1], new Vector2()));
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(((Entity) this.Projectile).DirectionFrom(((Entity) Main.player[this.Projectile.owner]).Center), ((Vector2) ref ((Entity) this.Projectile).velocity).Length());
        if ((double) ++this.Projectile.ai[1] > 180.0)
        {
          ++this.Projectile.localAI[0];
          this.Projectile.localAI[1] = 0.0f;
          this.Projectile.ai[0] = 0.0f;
          this.Projectile.ai[1] = 0.0f;
          this.Projectile.netUpdate = true;
        }
      }
      else if (this.Projectile.timeLeft > 60)
      {
        if ((double) this.Projectile.ai[0] >= 0.0 && (double) this.Projectile.ai[0] < (double) Main.maxNPCs)
        {
          int index = (int) this.Projectile.ai[0];
          if (Main.npc[index].CanBeChasedBy((object) null, false))
          {
            double num = (double) Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.npc[index]).Center, ((Entity) this.Projectile).Center)) - (double) Utils.ToRotation(((Entity) this.Projectile).velocity);
            if (num > Math.PI)
              num -= 2.0 * Math.PI;
            if (num < -1.0 * Math.PI)
              num += 2.0 * Math.PI;
            ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, num * 0.17000000178813934, new Vector2());
          }
          else
          {
            this.Projectile.ai[0] = -1f;
            this.Projectile.netUpdate = true;
          }
        }
        else if ((double) ++this.Projectile.localAI[1] > 12.0)
        {
          this.Projectile.localAI[1] = 0.0f;
          this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 2000f);
          this.Projectile.netUpdate = true;
        }
      }
      ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = this.rotationDirection;
      this.Projectile.rotation += (float) this.Projectile.spriteDirection * 0.7f * (float) this.rotationDirection;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 10; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 27, 0.0f, 0.0f, 0, new Color(), 2.5f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(153, 300, false);
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 300, false);
      target.immune[this.Projectile.owner] = 6;
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
        Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);
  }
}
