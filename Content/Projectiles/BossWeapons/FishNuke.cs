// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.FishNuke
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
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class FishNuke : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 22;
      ((Entity) this.Projectile).height = 22;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.penetrate = 1;
      this.Projectile.timeLeft = 1800;
      this.Projectile.ignoreWater = true;
      this.Projectile.extraUpdates = 1;
      this.Projectile.scale = 2f;
    }

    public virtual void AI()
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
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, num * 0.10000000149011612, new Vector2());
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
        this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 500f, true);
        this.Projectile.netUpdate = true;
      }
      if ((double) ++this.Projectile.localAI[0] >= 24.0)
      {
        this.Projectile.localAI[0] = 0.0f;
        for (int index1 = 0; index1 < 18; ++index1)
        {
          Vector2 vector2 = Utils.RotatedBy(Vector2.op_Addition(Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitX, (float) -((Entity) this.Projectile).width), 2f), Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) index1 * 2.0 * 3.14159274101257 / 18.0, new Vector2())), new Vector2(8f, 16f))), (double) this.Projectile.rotation - 1.5707963705062866, new Vector2());
          int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 135, 0.0f, 0.0f, 160, new Color(), 1f);
          Main.dust[index2].scale = 2f;
          Main.dust[index2].noGravity = true;
          Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 2f));
          Main.dust[index2].velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 3f)), Main.dust[index2].position)), 1.25f);
        }
      }
      Vector2 vector2_1 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitY, (double) this.Projectile.rotation, new Vector2()), 8f), 2f);
      int index3 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 6, 0.0f, 0.0f, 0, new Color(), 1f);
      Main.dust[index3].position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1);
      Main.dust[index3].scale = 1.25f;
      Main.dust[index3].noGravity = true;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(44, 300, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if (this.Projectile.owner == Main.myPlayer)
      {
        int num = Utils.NextBool(Main.rand) ? 1 : -1;
        this.SpawnRazorbladeRing(6, 17f, 1f * (float) -num);
        this.SpawnRazorbladeRing(6, 17f, 0.5f * (float) num);
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<FishNukeExplosion>(), this.Projectile.damage / 2, this.Projectile.knockBack * 2f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      }
      int num1 = 36;
      for (int index1 = 0; index1 < num1; ++index1)
      {
        Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), new Vector2((float) ((Entity) this.Projectile).width / 2f, (float) ((Entity) this.Projectile).height)), 0.75f), (double) (index1 - (num1 / 2 - 1)) * 6.28318548202515 / (double) num1, new Vector2()), ((Entity) this.Projectile).Center);
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
        int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, new Color(), 1.4f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Main.dust[index2].velocity = vector2_2;
      }
    }

    private void SpawnRazorbladeRing(int max, float speed, float rotationModifier)
    {
      float num1 = 6.28318548f / (float) max;
      Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI), speed);
      int num2 = ModContent.ProjectileType<RazorbladeTyphoonFriendly>();
      for (int index = 0; index < max; ++index)
      {
        vector2 = Utils.RotatedBy(vector2, (double) num1, new Vector2());
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2, num2, this.Projectile.damage / 2, this.Projectile.knockBack, this.Projectile.owner, rotationModifier, 6f, 0.0f);
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
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
