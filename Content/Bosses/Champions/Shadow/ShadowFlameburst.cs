﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Shadow.ShadowFlameburst
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Shadow
{
  public class ShadowFlameburst : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_664";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 14;
      ((Entity) this.Projectile).height = 14;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 300;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        for (int index1 = 0; index1 < 5; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, ((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y, 0, new Color(), 1.5f);
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 6f);
        }
      }
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.championBoss, ModContent.NPCType<ShadowChampion>()) && (double) Main.npc[EModeGlobalNPC.championBoss].localAI[3] > 1.0)
        this.Projectile.tileCollide = false;
      if ((double) ++this.Projectile.localAI[0] > 30.0 && (double) this.Projectile.localAI[0] < 120.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, this.Projectile.ai[0]);
      }
      if ((double) this.Projectile.localAI[0] > 60.0 && (double) this.Projectile.localAI[0] < 180.0 && FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.championBoss, ModContent.NPCType<ShadowChampion>()))
      {
        float rotation1 = Utils.ToRotation(((Entity) this.Projectile).velocity);
        float rotation2 = Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.player[Main.npc[EModeGlobalNPC.championBoss].target]).Center, ((Entity) this.Projectile).Center));
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(new Vector2(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), 0.0f), (double) Utils.AngleLerp(rotation1, rotation2, this.Projectile.ai[1]), new Vector2());
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, ((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y, 100, new Color(), 1.2f);
      Main.dust[index].position = Vector2.op_Division(Vector2.op_Addition(Main.dust[index].position, ((Entity) this.Projectile).Center), 2f);
      Main.dust[index].noGravity = true;
      Main.dust[index].velocity = Vector2.op_Multiply(Main.dust[index].velocity, 0.3f);
      Main.dust[index].velocity = Vector2.op_Subtraction(Main.dust[index].velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.1f));
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 15; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 10; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(22, 300, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300, true, false);
      target.AddBuff(80, 300, true, false);
      target.AddBuff(24, 300, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);

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
