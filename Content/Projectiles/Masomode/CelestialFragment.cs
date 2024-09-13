// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.CelestialFragment
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
  public class CelestialFragment : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 8;
      ((Entity) this.Projectile).height = 8;
      this.Projectile.aiStyle = -1;
      this.Projectile.scale = 1.25f;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 720;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      Projectile projectile = this.Projectile;
      ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.985f);
      this.Projectile.rotation += ((Entity) this.Projectile).velocity.X / 30f;
      this.Projectile.frame = (int) this.Projectile.ai[0];
      if (!Utils.NextBool(Main.rand, 20))
        return;
      int num1;
      switch ((int) this.Projectile.ai[0])
      {
        case 0:
          num1 = 242;
          break;
        case 1:
          num1 = (int) sbyte.MaxValue;
          break;
        case 2:
          num1 = 229;
          break;
        default:
          num1 = 135;
          break;
      }
      int num2 = num1;
      Dust dust = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num2, 0.0f, 0.0f, 0, new Color(), 1f)];
      dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      dust.fadeIn = 1f;
      dust.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
      dust.noGravity = true;
    }

    public virtual void OnKill(int timeLeft)
    {
      int num1;
      switch ((int) this.Projectile.ai[0])
      {
        case 0:
          num1 = 242;
          break;
        case 1:
          num1 = (int) sbyte.MaxValue;
          break;
        case 2:
          num1 = 229;
          break;
        default:
          num1 = 135;
          break;
      }
      int num2 = num1;
      for (int index = 0; index < 20; ++index)
      {
        Dust dust = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num2, 0.0f, 0.0f, 0, new Color(), 1f)];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 6f);
        dust.fadeIn = 1f;
        dust.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
        dust.noGravity = true;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 300, true, false);
      switch ((int) this.Projectile.ai[0])
      {
        case 0:
          target.AddBuff(ModContent.BuffType<ReverseManaFlowBuff>(), 180, true, false);
          break;
        case 1:
          target.AddBuff(ModContent.BuffType<AtrophiedBuff>(), 180, true, false);
          break;
        case 2:
          target.AddBuff(ModContent.BuffType<JammedBuff>(), 180, true, false);
          break;
        default:
          target.AddBuff(ModContent.BuffType<AntisocialBuff>(), 180, true, false);
          break;
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 150));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
