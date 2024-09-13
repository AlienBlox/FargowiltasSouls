// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.FragmentRitual
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.VanillaEternity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class FragmentRitual : ModProjectile
  {
    private const float PI = 3.14159274f;
    private const float rotationPerTick = 0.0224399474f;
    private const float threshold = 600f;

    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 5;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 8;
      ((Entity) this.Projectile).height = 8;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = (int) byte.MaxValue;
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 398);
      if (npc != null && (double) npc.ai[0] != 2.0)
      {
        this.Projectile.hide = false;
        this.Projectile.alpha -= 2;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
        this.Projectile.localAI[0] = (float) (int) npc.GetGlobalNPC<MoonLordCore>().VulnerabilityTimer / 56.25f;
        --this.Projectile.localAI[0];
        Projectile projectile = this.Projectile;
        int num;
        switch (npc.GetGlobalNPC<MoonLordCore>().VulnerabilityState)
        {
          case 0:
            num = 1;
            break;
          case 1:
            num = 2;
            break;
          case 2:
            num = 0;
            break;
          case 3:
            num = 3;
            break;
          default:
            num = 4;
            break;
        }
        projectile.frame = num;
      }
      else
      {
        this.Projectile.hide = true;
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        this.Projectile.alpha += 2;
        if (this.Projectile.alpha > (int) byte.MaxValue)
        {
          this.Projectile.Kill();
          return;
        }
      }
      this.Projectile.timeLeft = 2;
      this.Projectile.scale = (float) ((1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue) * 1.5 + ((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.25);
      if ((double) this.Projectile.scale < 0.10000000149011612)
        this.Projectile.scale = 0.1f;
      this.Projectile.ai[0] += (float) Math.PI / 140f;
      if ((double) this.Projectile.ai[0] > 3.1415927410125732)
      {
        this.Projectile.ai[0] -= 6.28318548f;
        this.Projectile.netUpdate = true;
      }
      this.Projectile.rotation = this.Projectile.ai[0];
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
      for (int index = 0; index < 32; ++index)
      {
        if ((double) index >= (double) this.Projectile.localAI[0])
        {
          Vector2 vector2_2;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_2).\u002Ector((float) (600.0 * (double) this.Projectile.scale / 2.0), 0.0f);
          vector2_2 = Utils.RotatedBy(vector2_2, 0.19634954631328583 * (double) (index + 1) - 1.5707963705062866, new Vector2());
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
        }
      }
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), 0.75f));
    }
  }
}
