// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.BigMimicExplosion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class BigMimicExplosion : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_696";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.projFrames[696];
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(696);
      this.AIType = 696;
      this.Projectile.hostile = true;
      this.Projectile.friendly = false;
      this.Projectile.usesLocalNPCImmunity = false;
      this.Projectile.localNPCHitCooldown = 0;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool? CanDamage()
    {
      return new bool?(this.Projectile.frame == 1 || this.Projectile.frame == 2);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<MidasBuff>(), 600, true, false);
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
      Color alpha = this.Projectile.GetAlpha(lightColor);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
