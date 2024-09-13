// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Will.WillBomb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Will
{
  public class WillBomb : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 50;
      ((Entity) this.Projectile).height = 50;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 40;
      this.Projectile.tileCollide = false;
      this.Projectile.penetrate = -1;
      this.CooldownSlot = 1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), this.Projectile.ai[0]);
      Projectile projectile = this.Projectile;
      ((Entity) projectile).velocity = Vector2.op_Subtraction(((Entity) projectile).velocity, vector2);
      this.Projectile.rotation += ((Vector2) ref ((Entity) this.Projectile).velocity).Length() * 0.03f * (float) Math.Sign(((Entity) this.Projectile).velocity.X);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300, true, false);
        target.AddBuff(ModContent.BuffType<MidasBuff>(), 300, true, false);
      }
      target.AddBuff(30, 300, true, false);
    }

    private void SpawnSphereRing(int max, float speed, int damage, float rotationModifier)
    {
      if (Main.netMode == 1)
        return;
      float num1 = 6.28318548f / (float) max;
      Vector2 vector2 = Vector2.op_Multiply(Vector2.UnitY, speed);
      int num2 = ModContent.ProjectileType<WillTyphoon>();
      for (int index = 0; index < max; ++index)
      {
        vector2 = Utils.RotatedBy(vector2, (double) num1, new Vector2());
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2, num2, damage, 0.0f, Main.myPlayer, rotationModifier, speed, 0.0f);
      }
      SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if (((Entity) Main.LocalPlayer).active)
        ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
      if (FargoSoulsUtil.HostCheck)
      {
        if (WorldSavingSystem.EternityMode)
          Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<WillRitual>(), this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, this.Projectile.ai[1], 0.0f);
        if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.championBoss, ModContent.NPCType<WillChampion>()))
        {
          if ((double) Main.npc[EModeGlobalNPC.championBoss].ai[0] > -1.0)
          {
            if ((double) Main.npc[EModeGlobalNPC.championBoss].localAI[2] == 1.0)
            {
              this.SpawnSphereRing(8, 8f, this.Projectile.damage, 2f);
              this.SpawnSphereRing(8, 8f, this.Projectile.damage, -2f);
            }
            if ((double) Main.npc[EModeGlobalNPC.championBoss].localAI[3] == 1.0)
            {
              this.SpawnSphereRing(8, 8f, this.Projectile.damage, 0.5f);
              this.SpawnSphereRing(8, 8f, this.Projectile.damage, -0.5f);
            }
          }
          for (int index = 0; index < 4; ++index)
          {
            float num = 0.0f;
            if ((double) Main.npc[EModeGlobalNPC.championBoss].localAI[2] == 1.0)
              num = 0.00186999573f;
            if ((double) Main.npc[EModeGlobalNPC.championBoss].localAI[3] == 1.0)
              num = -0.00249332748f;
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Utils.RotatedBy(Vector2.UnitX, Math.PI / 2.0 * (double) index + Math.PI / 4.0, new Vector2()), ModContent.ProjectileType<WillDeathray>(), this.Projectile.damage, 0.0f, Main.myPlayer, num, this.Projectile.ai[1], 0.0f);
          }
        }
      }
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = 250;
      ((Entity) this.Projectile).height = 250;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, 0.0f, 0.0f, 100, new Color(), 3f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 12f);
        Main.dust[index2].noLight = true;
        int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, 0.0f, 0.0f, 100, new Color(), 2f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 9f);
        Main.dust[index3].noGravity = true;
        Main.dust[index3].noLight = true;
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, 0.0f, 0.0f, 100, new Color(), 4.5f);
        Dust dust3 = Main.dust[index4];
        dust3.velocity = Vector2.op_Multiply(dust3.velocity, Utils.NextFloat(Main.rand, 9f, 12f));
        Main.dust[index4].position = ((Entity) this.Projectile).Center;
      }
      for (int index5 = 0; index5 < 50; ++index5)
      {
        int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 4f);
        Main.dust[index6].scale *= Utils.NextFloat(Main.rand, 1f, 2.5f);
        Main.dust[index6].noGravity = true;
        Main.dust[index6].velocity = Vector2.op_Multiply(Utils.RotatedByRandom(Main.dust[index6].velocity, (double) MathHelper.ToRadians(40f)), 6f);
        Dust dust4 = Main.dust[index6];
        dust4.velocity = Vector2.op_Multiply(dust4.velocity, 4f);
        int index7 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 4f);
        Dust dust5 = Main.dust[index7];
        dust5.velocity = Vector2.op_Multiply(dust5.velocity, 8f);
      }
      float num1 = 2.5f;
      for (int index8 = 0; index8 < 20; ++index8)
      {
        int index9 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).position, new Vector2((float) Main.rand.Next(((Entity) this.Projectile).width), (float) Main.rand.Next(((Entity) this.Projectile).height))), Vector2.Zero, Main.rand.Next(61, 64), num1);
        Main.gore[index9].velocity.Y += 2f;
        Gore gore = Main.gore[index9];
        gore.velocity = Vector2.op_Multiply(gore.velocity, 6f);
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
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), 0.75f), 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
