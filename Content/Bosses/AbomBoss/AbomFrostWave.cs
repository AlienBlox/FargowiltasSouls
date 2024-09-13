// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomFrostWave
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.AbomBoss
{
  public class AbomFrostWave : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_348";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 40;
      this.Projectile.tileCollide = false;
      this.Projectile.aiStyle = 1;
      this.AIType = 348;
      this.Projectile.hostile = true;
      this.Projectile.extraUpdates = 1;
      this.Projectile.timeLeft = 180;
      this.Projectile.penetrate = -1;
      this.CooldownSlot = 1;
      this.Projectile.coldDamage = true;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
        target.AddBuff(ModContent.BuffType<AbomFangBuff>(), 300, true, false);
      target.AddBuff(44, 120, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color(200, 200, 200, this.Projectile.alpha));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
