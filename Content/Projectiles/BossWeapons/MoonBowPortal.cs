// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.MoonBowPortal
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class MoonBowPortal : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_578";

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.friendly = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.timeLeft = 60;
      this.Projectile.hide = true;
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      behindProjectiles.Add(index);
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (!((Entity) player).active)
      {
        this.Projectile.Kill();
      }
      else
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Subtraction(((Entity) player).position, ((Entity) player).oldPosition));
        float num = 0.4f;
        ++this.Projectile.ai[0];
        this.Projectile.scale = Math.Min(1f, this.Projectile.ai[0] / 20f) * num;
        this.Projectile.alpha = (int) byte.MaxValue - (int) ((double) byte.MaxValue * (double) this.Projectile.scale / (double) num);
        this.Projectile.rotation -= 0.1570796f;
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      if (this.Projectile.owner != Main.myPlayer || !((Entity) Main.player[this.Projectile.owner]).active)
        return;
      Vector2 mouseWorld = Main.MouseWorld;
      if (Main.player[this.Projectile.owner].HasBuff<MoonBowBuff>())
        mouseWorld.Y += 16f;
      Vector2 vector2 = Vector2.op_Multiply(32f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, mouseWorld));
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2, 640, this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 100), this.Projectile.Opacity));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(Color.Black, this.Projectile.Opacity), -this.Projectile.rotation, vector2, this.Projectile.scale * 1.25f, (SpriteEffects) 1, 0.0f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
