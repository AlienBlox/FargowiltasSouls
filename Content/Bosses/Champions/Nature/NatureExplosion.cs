// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Nature.NatureExplosion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Nature
{
  public class NatureExplosion : ModProjectile
  {
    public virtual string Texture => FargoSoulsUtil.EmptyTexture;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 800;
      ((Entity) this.Projectile).height = 800;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = false;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 2;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.hide = true;
      this.Projectile.extraUpdates = 1;
      this.CooldownSlot = 1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
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

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
        target.AddBuff(67, 300, true, false);
      target.AddBuff(24, 300, true, false);
      ((Entity) target).velocity.X = (double) ((Entity) target).Center.X < (double) ((Entity) Main.npc[(int) this.Projectile.ai[0]]).Center.X ? -15f : 15f;
      ((Entity) target).velocity.Y = -10f;
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 45; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 60; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      for (int index6 = 0; index6 < 3; ++index6)
      {
        float num = 0.4f;
        if (index6 == 1)
          num = 0.8f;
        int index7 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore1 = Main.gore[index7];
        gore1.velocity = Vector2.op_Multiply(gore1.velocity, num);
        ++Main.gore[index7].velocity.X;
        ++Main.gore[index7].velocity.Y;
        int index8 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore2 = Main.gore[index8];
        gore2.velocity = Vector2.op_Multiply(gore2.velocity, num);
        --Main.gore[index8].velocity.X;
        ++Main.gore[index8].velocity.Y;
        int index9 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore3 = Main.gore[index9];
        gore3.velocity = Vector2.op_Multiply(gore3.velocity, num);
        ++Main.gore[index9].velocity.X;
        --Main.gore[index9].velocity.Y;
        int index10 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore4 = Main.gore[index10];
        gore4.velocity = Vector2.op_Multiply(gore4.velocity, num);
        --Main.gore[index10].velocity.X;
        --Main.gore[index10].velocity.Y;
      }
      for (int index11 = 0; index11 < 40; ++index11)
      {
        Vector2 position = ((Entity) this.Projectile).position;
        position.X += (float) Main.rand.Next(((Entity) this.Projectile).width);
        position.Y += (float) Main.rand.Next(((Entity) this.Projectile).height);
        for (int index12 = 0; index12 < 30; ++index12)
        {
          int index13 = Dust.NewDust(position, 32, 32, 31, 0.0f, 0.0f, 100, new Color(), 3f);
          Dust dust = Main.dust[index13];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
        }
        for (int index14 = 0; index14 < 20; ++index14)
        {
          int index15 = Dust.NewDust(position, 32, 32, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
          Main.dust[index15].noGravity = true;
          Dust dust3 = Main.dust[index15];
          dust3.velocity = Vector2.op_Multiply(dust3.velocity, 7f);
          int index16 = Dust.NewDust(position, 32, 32, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust4 = Main.dust[index16];
          dust4.velocity = Vector2.op_Multiply(dust4.velocity, 3f);
        }
        float num = 0.5f;
        for (int index17 = 0; index17 < 4; ++index17)
        {
          int index18 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), position, new Vector2(), Main.rand.Next(61, 64), 1f);
          Gore gore = Main.gore[index18];
          gore.velocity = Vector2.op_Multiply(gore.velocity, num);
          ++Main.gore[index18].velocity.X;
          ++Main.gore[index18].velocity.Y;
        }
      }
      for (int index19 = 0; index19 < 80; ++index19)
      {
        Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitX, 40f), (double) (index19 - 39) * 6.2831854820251465 / 80.0, new Vector2()), ((Entity) this.Projectile).Center);
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
        int index20 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 6, 0.0f, 0.0f, 0, new Color(), 3f);
        Main.dust[index20].noGravity = true;
        Main.dust[index20].velocity = vector2_2;
      }
    }
  }
}
