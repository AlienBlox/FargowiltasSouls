// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Timber.TimberTree
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Timber
{
  public class TimberTree : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 96;
      ((Entity) this.Projectile).height = 304;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 60;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = true;
      this.Projectile.alpha = (int) byte.MaxValue;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      ++((Entity) this.Projectile).velocity.Y;
      if (this.Projectile.alpha <= 0)
        return;
      this.Projectile.alpha -= 10;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      for (int index = 0; index < 5; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 2, 0.0f, 0.0f, 0, new Color(), 1f);
    }

    public virtual void OnKill(int timeLeft)
    {
      if (WorldSavingSystem.EternityMode)
        FargoSoulsUtil.NewNPCEasy(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Subtraction(((Entity) this.Projectile).Top, Vector2.op_Multiply(20f, Vector2.UnitY)), ModContent.NPCType<LesserSquirrel>(), velocity: new Vector2(Utils.NextFloat(Main.rand, -10f, 10f), Utils.NextFloat(Main.rand, -20f, -10f)));
      if (!FargoSoulsUtil.HostCheck)
        return;
      Player player = FargoSoulsUtil.PlayerExists(this.Projectile.ai[0]);
      if (player == null)
        return;
      for (int index = 0; index < 10; ++index)
      {
        Vector2 position = ((Entity) this.Projectile).position;
        position.X += (float) ((Entity) this.Projectile).width / 2f + Utils.NextFloat(Main.rand, -40f, 40f);
        position.Y += 40f + Utils.NextFloat(Main.rand, -40f, 40f);
        float num1 = 30f;
        Vector2 vector2 = Vector2.op_Subtraction(((Entity) player).Center, position);
        vector2.X = Utils.NextFloat(Main.rand, -1.5f, 1.5f);
        vector2.Y = (float) ((double) vector2.Y / (double) num1 - 0.10000000149011612 * (double) num1);
        float num2 = Utils.NextFloat(Main.rand, -12f, -9f);
        if ((double) vector2.Y > (double) num2)
          vector2.Y = num2;
        vector2 = Vector2.op_Addition(vector2, Utils.NextVector2Square(Main.rand, -0.5f, 0.5f));
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), position, vector2, ModContent.ProjectileType<TimberAcorn>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      }
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = false;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity) => false;

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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
