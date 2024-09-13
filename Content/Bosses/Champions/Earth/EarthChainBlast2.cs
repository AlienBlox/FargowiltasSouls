// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Earth.EarthChainBlast2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Earth
{
  public class EarthChainBlast2 : MoonLordSunBlast
  {
    public override string Texture => "Terraria/Images/Projectile_687";

    public override void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.projFrames[645];
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(24, 300, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(67, 300, true, false);
      target.AddBuff(ModContent.BuffType<LethargicBuff>(), 300, true, false);
    }

    public override bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color white = Color.White;
      Color color = (double) this.Projectile.ai[1] <= 3.0 ? Color.Lerp(new Color((int) byte.MaxValue, 95, 46, 50), new Color(150, 35, 0, 100), (float) ((3.0 - (double) this.Projectile.ai[1]) / 3.0)) : Color.Lerp(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), new Color((int) byte.MaxValue, 95, 46, 50), Math.Min(2f, 7f - this.Projectile.ai[1]) / 4f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
