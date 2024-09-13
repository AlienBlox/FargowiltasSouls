// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomLaser
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.AbomBoss
{
  public class AbomLaser : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_466";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 5;
      ((Entity) this.Projectile).height = 5;
      this.Projectile.aiStyle = 1;
      this.AIType = 449;
      this.Projectile.hostile = true;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.extraUpdates = 2;
      this.Projectile.timeLeft = 360;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 0.3f;
      this.CooldownSlot = 1;
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      this.Projectile.FargoSouls().GrazeCheck = (Func<Projectile, bool>) (Projectile => false);
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 4; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 222, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
        target.AddBuff(ModContent.BuffType<AbomFangBuff>(), 300, true, false);
      target.AddBuff(144, 180, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      Rectangle bounds = texture2D.Bounds;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(bounds), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index1 = 1; index1 < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index1)
      {
        if (!Vector2.op_Equality(this.Projectile.oldPos[index1], Vector2.Zero) && !Vector2.op_Equality(this.Projectile.oldPos[index1 - 1], this.Projectile.oldPos[index1]))
        {
          Vector2 vector2_2 = Vector2.op_Subtraction(this.Projectile.oldPos[index1 - 1], this.Projectile.oldPos[index1]);
          int num = (int) ((Vector2) ref vector2_2).Length();
          ((Vector2) ref vector2_2).Normalize();
          Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          for (int index2 = 0; index2 < num; index2 += 3)
          {
            Vector2 vector2_3 = Vector2.op_Addition(this.Projectile.oldPos[index1], Vector2.op_Multiply(vector2_2, (float) index2));
            Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_3, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(bounds), color, this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 1, 0.0f);
          }
        }
      }
      return false;
    }
  }
}
