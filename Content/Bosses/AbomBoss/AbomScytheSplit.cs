// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomScytheSplit
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.AbomBoss
{
  public class AbomScytheSplit : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_274";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 40;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.aiStyle = -1;
      this.Projectile.timeLeft = 600;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.CooldownSlot = 1;
      this.Projectile.scale = 2f;
    }

    public virtual void AI()
    {
      ++this.Projectile.rotation;
      if ((double) --this.Projectile.ai[0] > 0.0)
        return;
      this.Projectile.Kill();
    }

    public virtual void OnKill(int timeLeft)
    {
      int num1 = (double) this.Projectile.ai[1] >= 0.0 ? 50 : 25;
      float num2 = (double) this.Projectile.ai[1] >= 0.0 ? 15f : 6f;
      for (int index1 = 0; index1 < num1; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 70, 0.0f, 0.0f, 0, new Color(), 3.5f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, num2);
        Main.dust[index2].noGravity = true;
      }
      if ((double) this.Projectile.ai[1] < 0.0 || !FargoSoulsUtil.HostCheck)
        return;
      int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
      if (closest == -1)
        return;
      Vector2 vector2 = Vector2.op_Multiply((double) this.Projectile.ai[1] == 0.0 ? Vector2.Normalize(((Entity) this.Projectile).velocity) : Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[closest]).Center), 30f);
      int num3 = (double) this.Projectile.ai[1] == 0.0 ? 6 : (WorldSavingSystem.MasochistModeReal ? 10 : 8);
      for (int index = 0; index < num3; ++index)
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Utils.RotatedBy(vector2, 6.2831854820251465 / (double) num3 * (double) index, new Vector2()), ModContent.ProjectileType<AbomSickle3>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, (float) closest, 0.0f, 0.0f);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.AddBuff(ModContent.BuffType<AbomFangBuff>(), 300, true, false);
        target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 120, true, false);
      }
      target.AddBuff(30, 600, true, false);
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
      int num3 = ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        num3 /= 2;
      for (int index = 0; index < num3; ++index)
      {
        Color color = Color.op_Multiply(alpha, (float) (num3 - index) / (float) num3);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num4 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num4, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), this.Projectile.Opacity));
    }
  }
}
