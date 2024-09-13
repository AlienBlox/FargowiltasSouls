// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Will.BetsyHomingFireball
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Will
{
  public class BetsyHomingFireball : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_711";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 600;
      this.Projectile.alpha = 60;
      this.Projectile.ignoreWater = true;
      this.Projectile.extraUpdates = 1;
    }

    public virtual void AI()
    {
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      if ((double) this.Projectile.ai[1] == 0.0)
      {
        this.Projectile.ai[1] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item34, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      }
      else if ((double) this.Projectile.ai[1] == 1.0 && Main.netMode != 1)
      {
        int num1 = -1;
        float num2 = 2000f;
        for (int index = 0; index < (int) byte.MaxValue; ++index)
        {
          if (((Entity) Main.player[index]).active && !Main.player[index].dead)
          {
            Vector2 center = ((Entity) Main.player[index]).Center;
            float num3 = Vector2.Distance(center, ((Entity) this.Projectile).Center);
            if (((double) num3 < (double) num2 || num1 == -1) && Collision.CanHit(((Entity) this.Projectile).Center, 1, 1, center, 1, 1))
            {
              num2 = num3;
              num1 = index;
            }
          }
        }
        if ((double) num2 < 20.0)
        {
          this.Projectile.Kill();
          return;
        }
        if (num1 != -1)
        {
          this.Projectile.ai[1] = 21f;
          this.Projectile.ai[0] = (float) num1;
          this.Projectile.netUpdate = true;
        }
      }
      else if ((double) this.Projectile.ai[1] > 20.0 && (double) this.Projectile.ai[1] < 200.0)
      {
        ++this.Projectile.ai[1];
        int index = (int) this.Projectile.ai[0];
        if (!((Entity) Main.player[index]).active || Main.player[index].dead)
        {
          this.Projectile.ai[1] = 1f;
          this.Projectile.ai[0] = 0.0f;
          this.Projectile.netUpdate = true;
        }
        else
        {
          float rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[index]).Center, ((Entity) this.Projectile).Center);
          if ((double) ((Vector2) ref vector2).Length() < 20.0)
          {
            this.Projectile.Kill();
            return;
          }
          float num4 = Utils.ToRotation(vector2);
          if (Vector2.op_Equality(vector2, Vector2.Zero))
            num4 = rotation;
          float num5 = Utils.AngleLerp(rotation, num4, 0.008f);
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(new Vector2(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), 0.0f), (double) num5, new Vector2());
        }
      }
      if ((double) this.Projectile.ai[1] >= 1.0 && (double) this.Projectile.ai[1] < 20.0)
      {
        ++this.Projectile.ai[1];
        if ((double) this.Projectile.ai[1] == 20.0)
          this.Projectile.ai[1] = 1f;
      }
      int num = (!WorldSavingSystem.EternityMode ? 0 : (SoulConfig.Instance.BossRecolors ? 1 : 0)) != 0 ? 27 : 6;
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] == 12.0)
      {
        this.Projectile.localAI[0] = 0.0f;
        for (int index1 = 0; index1 < 12; ++index1)
        {
          Vector2 vector2 = Utils.RotatedBy(Vector2.op_Addition(Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitX, (float) -((Entity) this.Projectile).width), 2f), Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) index1 * 3.1415927410125732 / 6.0, new Vector2())), new Vector2(8f, 16f))), (double) this.Projectile.rotation - 1.5707963705062866, new Vector2());
          int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, num, 0.0f, 0.0f, 160, new Color(), 1f);
          Main.dust[index2].scale = 1.1f;
          Main.dust[index2].noGravity = true;
          Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2);
          Main.dust[index2].velocity = Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.1f);
          Main.dust[index2].velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 3f)), Main.dust[index2].position)), 1.25f);
        }
      }
      if (Utils.NextBool(Main.rand, 4))
      {
        for (int index3 = 0; index3 < 1; ++index3)
        {
          Vector2 vector2 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.19634954631328583), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
          int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1f);
          Dust dust = Main.dust[index4];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.1f);
          Main.dust[index4].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2, (float) ((Entity) this.Projectile).width), 2f));
          Main.dust[index4].fadeIn = 0.9f;
        }
      }
      if (Utils.NextBool(Main.rand, 32))
      {
        for (int index5 = 0; index5 < 1; ++index5)
        {
          Vector2 vector2 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.39269909262657166), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
          int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 155, new Color(), 0.8f);
          Dust dust = Main.dust[index6];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.3f);
          Main.dust[index6].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2, (float) ((Entity) this.Projectile).width), 2f));
          if (Utils.NextBool(Main.rand, 2))
            Main.dust[index6].fadeIn = 1.4f;
        }
      }
      if (!Utils.NextBool(Main.rand, 2))
        return;
      for (int index7 = 0; index7 < 2; ++index7)
      {
        Vector2 vector2 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.78539818525314331), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
        int index8 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num, 0.0f, 0.0f, 0, new Color(), 1.2f);
        Dust dust = Main.dust[index8];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.3f);
        Main.dust[index8].noGravity = true;
        Main.dust[index8].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2, (float) ((Entity) this.Projectile).width), 2f));
        if (Utils.NextBool(Main.rand, 2))
          Main.dust[index8].fadeIn = 1.4f;
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      int num1 = (!WorldSavingSystem.EternityMode ? 0 : (SoulConfig.Instance.BossRecolors ? 1 : 0)) != 0 ? 27 : 6;
      for (int index1 = 0; index1 < 30; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 20; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num1, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num1, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      float num2 = 0.5f;
      for (int index6 = 0; index6 < 4; ++index6)
      {
        int index7 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore = Main.gore[index7];
        gore.velocity = Vector2.op_Multiply(gore.velocity, num2);
        ++Main.gore[index7].velocity.X;
        ++Main.gore[index7].velocity.Y;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.betsyBoss, 551) && WorldSavingSystem.EternityMode)
      {
        target.AddBuff(195, Main.rand.Next(60, 300), true, false);
        target.AddBuff(196, Main.rand.Next(60, 300), true, false);
        target.AddBuff(67, 300, true, false);
      }
      this.Projectile.timeLeft = 0;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.betsyBoss, 551) && WorldSavingSystem.EternityMode && SoulConfig.Instance.BossRecolors)
        texture2D = TextureAssets.Projectile[686].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(Color.White);
      ((Color) ref alpha).A = (byte) this.Projectile.alpha;
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        float num3 = 0.0f;
        if (index > 3 && index < 5)
          num3 = 0.6f;
        if (index >= 5)
          num3 = 0.8f;
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.Lerp(Color.White, Color.Purple, num3), 0.75f), 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        float num4 = this.Projectile.scale * (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num5 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num5, vector2, num4, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
