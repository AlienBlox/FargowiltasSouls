// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomFlocko
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.AbomBoss
{
  public class AbomFlocko : ModProjectile
  {
    public virtual string Texture => FargoSoulsUtil.VanillaTextureNPC(352);

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 4;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 50;
      ((Entity) this.Projectile).height = 50;
      this.Projectile.timeLeft = 420;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.CooldownSlot = 1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>());
      if (npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        Vector2 center = ((Entity) npc).Center;
        center.X += (float) (((double) npc.localAI[3] > 1.0 ? 1200.0 : 2000.0) * Math.Sin(Math.PI / 360.0 * (double) this.Projectile.ai[1]++));
        center.Y -= 1100f;
        Vector2 vector2_1 = Vector2.op_Subtraction(center, ((Entity) this.Projectile).Center);
        if ((double) ((Vector2) ref vector2_1).Length() > 100.0)
        {
          vector2_1 = Vector2.op_Division(vector2_1, 8f);
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 23f), vector2_1), 24f);
        }
        else if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 12.0)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.05f);
        }
        if ((double) ++this.Projectile.localAI[0] > 90.0 && (double) ++this.Projectile.localAI[1] > ((double) npc.localAI[3] > 1.0 ? 4.0 : 2.0))
        {
          SoundEngine.PlaySound(ref SoundID.Item27, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
          this.Projectile.localAI[1] = 0.0f;
          if (FargoSoulsUtil.HostCheck)
          {
            if ((double) Math.Abs(((Entity) npc).Center.X - ((Entity) this.Projectile).Center.X) > 400.0)
            {
              for (int index = 0; index < 2; ++index)
              {
                Vector2 vector2_2;
                // ISSUE: explicit constructor call
                ((Vector2) ref vector2_2).\u002Ector((float) Main.rand.Next(-1000, 1001), (float) Main.rand.Next(-1000, 1001));
                ((Vector2) ref vector2_2).Normalize();
                vector2_2 = Vector2.op_Multiply(vector2_2, 8f);
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_2, 4f)), vector2_2, ModContent.ProjectileType<AbomFrostShard>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
              }
            }
            if (((Entity) Main.player[npc.target]).active && !Main.player[npc.target].dead && (double) ((Entity) Main.player[npc.target]).Center.Y < (double) ((Entity) this.Projectile).Center.Y)
            {
              SoundEngine.PlaySound(ref SoundID.Item120, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
              {
                Vector2 vector2_3 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, new Vector2((float) Main.rand.Next(-200, 201), (float) Main.rand.Next(-200, 201)))), 12f);
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2_3, ModContent.ProjectileType<AbomFrostWave>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
              }
            }
          }
        }
        this.Projectile.rotation += (float) ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() / 12.0 * ((double) ((Entity) this.Projectile).velocity.X > 0.0 ? -0.20000000298023224 : 0.20000000298023224));
        if (++this.Projectile.frameCounter <= 3)
          return;
        if (++this.Projectile.frame >= 6)
          this.Projectile.frame = 0;
        this.Projectile.frameCounter = 0;
      }
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, Utils.NextBool(Main.rand) ? 80 : 76, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        ++Main.dust[index2].scale;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 200));
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
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
