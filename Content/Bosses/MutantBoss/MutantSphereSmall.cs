// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantSphereSmall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantSphereSmall : ModProjectile
  {
    public virtual string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "Terraria/Images/Projectile_454" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantSphere_April";
      }
    }

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 2;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 46;
      ((Entity) this.Projectile).height = 46;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = 1;
      this.Projectile.timeLeft = 120;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = 200;
      this.CooldownSlot = 1;
    }

    public virtual bool CanHitPlayer(Player target) => target.hurtCooldowns[1] == 0;

    public virtual void AI()
    {
      if ((double) this.Projectile.ai[0] > -1.0 && (double) this.Projectile.ai[0] < (double) byte.MaxValue && (double) ++this.Projectile.ai[1] > 20.0)
      {
        int index = (int) this.Projectile.ai[0];
        ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[index]).Center), 5f), 0.05f);
      }
      if (this.Projectile.alpha > 0)
      {
        this.Projectile.alpha -= 20;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
      }
      this.Projectile.scale = (float) ((1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue) * 0.75);
      if (++this.Projectile.frameCounter < 6)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame <= 1)
        return;
      this.Projectile.frame = 0;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      }
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, true, false);
    }

    public virtual void OnKill(int timeleft)
    {
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 208;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index2].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
      }
      for (int index3 = 0; index3 < 10; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
        Main.dust[index4].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 1f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index5].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 1f);
        Main.dust[index5].noGravity = true;
      }
      if (!FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<MutantBombSmall>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantSphereGlow", (AssetRequestMode) 1).Value;
      int height = texture2D.Height;
      int num1 = 0;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num1, texture2D.Width, height);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color1 = Color.Lerp(FargoSoulsUtil.AprilFools ? Color.Red : new Color(196, 247, (int) byte.MaxValue, 0), Color.Transparent, 0.85f);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color2 = Color.op_Multiply(color1, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        float num2 = this.Projectile.scale * (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
        Vector2 vector2_2 = Vector2.op_Subtraction(this.Projectile.oldPos[index], Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), (float) index), 2f));
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, num2 * 1.5f, (SpriteEffects) 0, 0.0f);
      }
      Color color3 = Color.Lerp(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), Color.Transparent, 0.8f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color3, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, this.Projectile.scale * 1.5f, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public virtual void PostDraw(Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
    }
  }
}
