// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.DeviBoss.DeviRitual2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.DeviBoss
{
  public class DeviRitual2 : ModProjectile
  {
    private const float PI = 3.14159274f;
    private const float rotationPerTick = 0.0551156625f;
    private const float threshold = 150f;

    public virtual void SetStaticDefaults() => ((ModType) this).SetStaticDefaults();

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 12;
      ((Entity) this.Projectile).height = 12;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = (int) byte.MaxValue;
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>());
      if (npc != null)
      {
        this.Projectile.alpha -= 2;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
      }
      else
      {
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        this.Projectile.alpha += 2;
        if (this.Projectile.alpha > (int) byte.MaxValue)
        {
          this.Projectile.Kill();
          return;
        }
      }
      this.Projectile.timeLeft = 2;
      this.Projectile.scale = (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue);
      this.Projectile.ai[0] -= (float) Math.PI / 57f;
      if ((double) this.Projectile.ai[0] >= -3.1415927410125732)
        return;
      this.Projectile.ai[0] += 6.28318548f;
      this.Projectile.netUpdate = true;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index = 0; index < 9; ++index)
      {
        Vector2 vector2_2 = Utils.RotatedBy(new Vector2((float) (150.0 / (Main.getGoodWorld ? 2.0 : 1.0) * (double) this.Projectile.scale / 2.0), 0.0f), (double) this.Projectile.ai[0], new Vector2());
        float num3 = 0.69813174f * (float) index;
        Vector2 vector2_3 = Utils.RotatedBy(vector2_2, (double) num3, new Vector2());
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_3), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, (float) ((double) num3 + (double) this.Projectile.ai[0] + 1.5707963705062866), vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }
  }
}
