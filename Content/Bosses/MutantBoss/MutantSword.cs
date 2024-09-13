// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantSword
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
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantSword : ModProjectile
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
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 12;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 64;
      ((Entity) this.Projectile).height = 64;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 110;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (((Rectangle) ref projHitbox).Intersects(targetHitbox))
        return new bool?(true);
      Rectangle rectangle = projHitbox;
      rectangle.X = (int) ((Entity) this.Projectile).oldPosition.X;
      rectangle.Y = (int) ((Entity) this.Projectile).oldPosition.Y;
      if (((Rectangle) ref rectangle).Intersects(targetHitbox))
        return new bool?(true);
      rectangle = projHitbox;
      rectangle.X = (int) MathHelper.Lerp(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).oldPosition.X, 0.5f);
      rectangle.Y = (int) MathHelper.Lerp(((Entity) this.Projectile).position.Y, ((Entity) this.Projectile).oldPosition.Y, 0.5f);
      return ((Rectangle) ref rectangle).Intersects(targetHitbox) ? new bool?(true) : new bool?(false);
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>());
      if (npc != null)
      {
        if (((double) npc.ai[0] == 9.0 && ((double) npc.localAI[2] != 2.0 || (double) npc.ai[1] <= 20.0) || (double) npc.ai[0] == 45.0 && (double) npc.ai[2] != 0.0 ? 1 : ((double) npc.ai[0] != 46.0 ? 0 : ((double) npc.ai[1] <= 20.0 ? 1 : 0))) == 0 && FargoSoulsUtil.HostCheck)
        {
          this.Projectile.Kill();
        }
        else
        {
          if ((double) this.Projectile.localAI[0] == 0.0)
          {
            this.Projectile.localAI[0] = 1f;
            this.Projectile.localAI[1] = Utils.ToRotation(((Entity) this.Projectile).DirectionFrom(((Entity) npc).Center));
          }
          Vector2 vector2 = Utils.RotatedBy(new Vector2(this.Projectile.ai[1], 0.0f), (double) npc.ai[3] + (double) this.Projectile.localAI[1], new Vector2());
          ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, vector2);
          if (this.Projectile.alpha > 0)
          {
            this.Projectile.alpha -= 10;
            if (this.Projectile.alpha < 0)
              this.Projectile.alpha = 0;
          }
          this.Projectile.scale = this.Projectile.Opacity;
          if (++this.Projectile.frameCounter < 6)
            return;
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame <= 1)
            return;
          this.Projectile.frame = 0;
        }
      }
      else
        this.Projectile.Kill();
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      ((Entity) target).velocity.X = (double) ((Entity) target).Center.X < (double) ((Entity) Main.npc[(int) this.Projectile.ai[0]]).Center.X ? -15f : 15f;
      ((Entity) target).velocity.Y = -10f;
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
      SoundEngine.PlaySound(ref SoundID.NPCDeath6, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 208;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      int num = FargoSoulsUtil.AprilFools ? 259 : 229;
      for (int index1 = 0; index1 < 2; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index2].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
      }
      for (int index3 = 0; index3 < 5; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num, 0.0f, 0.0f, 0, new Color(), 2.5f);
        Main.dust[index4].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 1f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index5].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 1f);
        Main.dust[index5].noGravity = true;
      }
      for (int index6 = 0; index6 < 5; ++index6)
      {
        int index7 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num, 0.0f, 0.0f, 100, new Color(), 3f);
        Dust dust = Main.dust[index7];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index8 = 0; index8 < 5; ++index8)
      {
        int index9 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index9].noGravity = true;
        Dust dust3 = Main.dust[index9];
        dust3.velocity = Vector2.op_Multiply(dust3.velocity, 7f);
        int index10 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust4 = Main.dust[index10];
        dust4.velocity = Vector2.op_Multiply(dust4.velocity, 3f);
      }
      for (int index11 = 0; index11 < 10; ++index11)
      {
        int index12 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index12].noGravity = true;
        Dust dust5 = Main.dust[index12];
        dust5.velocity = Vector2.op_Multiply(dust5.velocity, 21f * this.Projectile.scale);
        Main.dust[index12].noLight = true;
        int index13 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num, 0.0f, 0.0f, 100, new Color(), 1f);
        Dust dust6 = Main.dust[index13];
        dust6.velocity = Vector2.op_Multiply(dust6.velocity, 12f);
        Main.dust[index13].noGravity = true;
        Main.dust[index13].noLight = true;
      }
      for (int index14 = 0; index14 < 10; ++index14)
      {
        int index15 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num, 0.0f, 0.0f, 100, new Color(), Utils.NextFloat(Main.rand, 2f, 3.5f));
        if (Utils.NextBool(Main.rand, 3))
          Main.dust[index15].noGravity = true;
        Dust dust = Main.dust[index15];
        dust.velocity = Vector2.op_Multiply(dust.velocity, Utils.NextFloat(Main.rand, 9f, 12f));
        Main.dust[index15].position = ((Entity) this.Projectile).Center;
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
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color1 = Color.Lerp(FargoSoulsUtil.AprilFools ? Color.Red : new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), Color.Transparent, 0.85f);
      for (float index = 0.0f; (double) index < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 0.5f)
      {
        Color color2 = Color.op_Multiply(Color.op_Multiply(color1, 0.5f), ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        float num2 = this.Projectile.scale * ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
        Vector2 oldPo = this.Projectile.oldPos[(int) index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2, num2 * 1.5f, (SpriteEffects) 0, 0.0f);
      }
      Color color3 = Color.Lerp(new Color(196, 247, (int) byte.MaxValue, 0), Color.Transparent, 0.8f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color3, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2, this.Projectile.scale * 1.5f, (SpriteEffects) 0, 0.0f);
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
