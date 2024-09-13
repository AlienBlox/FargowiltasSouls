// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantBomb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantBomb : ModProjectile
  {
    public virtual string Texture
    {
      get => "Terraria/Images/Projectile_" + (FargoSoulsUtil.AprilFools ? "687" : "645");
    }

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.projFrames[645];
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 400;
      ((Entity) this.Projectile).height = 400;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = false;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 60;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.CooldownSlot = 1;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.FargoSouls().GrazeCheck = (Func<Projectile, bool>) (projectile => false);
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      int num1 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X;
      int num2 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y;
      if (Math.Abs(num1) > targetHitbox.Width / 2)
        num1 = targetHitbox.Width / 2 * Math.Sign(num1);
      if (Math.Abs(num2) > targetHitbox.Height / 2)
        num2 = targetHitbox.Height / 2 * Math.Sign(num2);
      int num3 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X - num1;
      int num4 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y - num2;
      return new bool?(Math.Sqrt((double) (num3 * num3 + num4 * num4)) <= (double) (((Entity) this.Projectile).width / 2));
    }

    public virtual bool CanHitPlayer(Player target) => target.hurtCooldowns[1] == 0;

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 6.28318548f);
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 2; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
        }
        for (int index3 = 0; index3 < 5; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 135, 0.0f, 0.0f, 0, new Color(), 3.5f);
          Main.dust[index4].noGravity = true;
          Main.dust[index4].noLight = true;
          Dust dust = Main.dust[index4];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
        }
        for (int index5 = 0; index5 < 2; ++index5)
        {
          int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
          Main.dust[index6].noGravity = true;
          Dust dust1 = Main.dust[index6];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
          int index7 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust2 = Main.dust[index7];
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
        }
        float num = 0.5f;
        int index = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore = Main.gore[index];
        gore.velocity = Vector2.op_Multiply(gore.velocity, num);
        ++Main.gore[index].velocity.X;
        ++Main.gore[index].velocity.Y;
      }
      if (++this.Projectile.frameCounter < 3)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame < Main.projFrames[this.Projectile.type])
        return;
      --this.Projectile.frame;
      this.Projectile.Kill();
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      }
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 900, true, false);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 900, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) sbyte.MaxValue), this.Projectile.Opacity));
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
      ((Color) ref alpha).A = (byte) 210;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale * 4f, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
