// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomScytheSpin
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
namespace FargowiltasSouls.Content.Bosses.AbomBoss
{
  public class AbomScytheSpin : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_274";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 40;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 420;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item71, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if (this.Projectile.timeLeft == 390)
      {
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        this.Projectile.netUpdate = true;
      }
      else if (this.Projectile.timeLeft == 360)
        SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      else if (this.Projectile.timeLeft < 360)
      {
        NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>());
        if (npc == null)
        {
          this.Projectile.Kill();
          return;
        }
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center), Math.PI / 2.0 * (double) this.Projectile.ai[1], new Vector2());
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, (float) Math.PI / 180f);
      }
      this.Projectile.spriteDirection = (int) this.Projectile.ai[1];
      this.Projectile.rotation += (float) this.Projectile.spriteDirection * 0.5f;
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item71, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 27, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        ++Main.dust[index2].scale;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      }
      if (!FargoSoulsUtil.HostCheck)
        return;
      int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
      if (closest == -1)
        return;
      Vector2 vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[closest]).Center);
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2, ModContent.ProjectileType<AbomSickle>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);
  }
}
