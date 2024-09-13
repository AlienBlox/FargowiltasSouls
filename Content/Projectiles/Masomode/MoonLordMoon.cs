// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MoonLordMoon
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Cosmos;
using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Core.Globals;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MoonLordMoon : CosmosMoon
  {
    public virtual string Texture => "FargowiltasSouls/Content/Bosses/Champions/Cosmos/CosmosMoon";

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.alpha = 200;
    }

    public virtual bool? CanDamage()
    {
      bool? nullable = base.CanDamage();
      bool flag = false;
      return nullable.GetValueOrDefault() == flag & nullable.HasValue ? new bool?(false) : new bool?((double) this.Projectile.localAI[0] > 120.0);
    }

    public override void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
        SoundEngine.PlaySound(ref SoundID.Item89, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
        if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
          ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
        for (int index1 = 0; index1 < 20; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
        }
        for (int index3 = 0; index3 < 40; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
          Main.dust[index4].noGravity = true;
          Dust dust1 = Main.dust[index4];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
          int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust2 = Main.dust[index5];
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
        }
        for (int index6 = 0; index6 < 2; ++index6)
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
        for (int index11 = 0; index11 < 20; ++index11)
        {
          Vector2 position = ((Entity) this.Projectile).position;
          position.X += (float) Main.rand.Next(((Entity) this.Projectile).width);
          position.Y += (float) Main.rand.Next(((Entity) this.Projectile).height);
          for (int index12 = 0; index12 < 15; ++index12)
          {
            int index13 = Dust.NewDust(position, 32, 32, 31, 0.0f, 0.0f, 100, new Color(), 3f);
            Dust dust = Main.dust[index13];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
          }
          for (int index14 = 0; index14 < 10; ++index14)
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
        for (int index19 = 0; index19 < 30; ++index19)
        {
          Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitX, 40f), (double) (index19 - 14) * 6.2831854820251465 / 30.0, new Vector2()), ((Entity) this.Projectile).Center);
          Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
          int index20 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 6, 0.0f, 0.0f, 0, new Color(), 3f);
          Main.dust[index20].noGravity = true;
          Main.dust[index20].velocity = vector2_2;
        }
      }
      NPC npc = FargoSoulsUtil.NPCExists(EModeGlobalNPC.moonBoss, new int[1]
      {
        398
      });
      Projectile projectile1 = FargoSoulsUtil.ProjectileExists(FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, this.Projectile.ai[0], ModContent.ProjectileType<LunarRitual>()), Array.Empty<int>());
      if (npc != null && projectile1 != null && (double) npc.ai[0] != 2.0)
      {
        if (npc.GetGlobalNPC<MoonLordCore>().VulnerabilityState == 4)
          this.Projectile.timeLeft = 60;
        float num = Math.Abs(this.Projectile.ai[1]) + 400f * (float) npc.life / (float) npc.lifeMax;
        if ((double) ++this.Projectile.localAI[0] < 60.0)
        {
          ((Entity) this.Projectile).Center = Vector2.Lerp(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) projectile1).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, num)), 0.05f);
          Projectile projectile2 = this.Projectile;
          ((Entity) projectile2).position = Vector2.op_Addition(((Entity) projectile2).position, ((Entity) projectile1).velocity);
          Projectile projectile3 = this.Projectile;
          ((Entity) projectile3).position = Vector2.op_Subtraction(((Entity) projectile3).position, ((Entity) this.Projectile).velocity);
          this.Projectile.alpha -= 10;
          if (this.Projectile.alpha >= 0)
            return;
          this.Projectile.alpha = 0;
        }
        else
        {
          this.Projectile.alpha = 0;
          ((Entity) this.Projectile).velocity = Vector2.Zero;
          this.Projectile.rotation += MathHelper.ToRadians(3.5f) * Math.Min(1f, (float) (((double) this.Projectile.localAI[0] - 60.0) / 180.0)) * (float) Math.Sign(this.Projectile.ai[1]);
          ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) projectile1).Center, Vector2.op_Multiply(num, Utils.ToRotationVector2(this.Projectile.rotation)));
        }
      }
      else
      {
        if (!FargoSoulsUtil.HostCheck)
          return;
        this.Projectile.Kill();
      }
    }
  }
}
