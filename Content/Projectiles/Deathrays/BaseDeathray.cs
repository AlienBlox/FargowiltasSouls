// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Deathrays.BaseDeathray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Deathrays
{
  public abstract class BaseDeathray : ModProjectile
  {
    protected float maxTime;
    protected readonly float transparency;
    protected readonly float hitboxModifier;
    protected readonly int grazeCD;
    protected readonly BaseDeathray.TextureSheeting sheeting;
    protected readonly int drawDistance;

    protected BaseDeathray(
      float maxTime,
      float transparency = 0.0f,
      float hitboxModifier = 1f,
      int drawDistance = 3000,
      int grazeCD = 15,
      BaseDeathray.TextureSheeting sheeting = BaseDeathray.TextureSheeting.Horizontal)
    {
      this.maxTime = maxTime;
      this.transparency = transparency;
      this.hitboxModifier = hitboxModifier;
      this.drawDistance = drawDistance;
      this.grazeCD = grazeCD;
      this.sheeting = sheeting;
    }

    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = this.drawDistance;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 48;
      ((Entity) this.Projectile).height = 48;
      this.Projectile.hostile = true;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 3600;
      this.CooldownSlot = 1;
      this.Projectile.FargoSouls().GrazeCheck = (Func<Projectile, bool>) (Projectile =>
      {
        float num = 0.0f;
        bool? nullable = this.CanDamage();
        bool flag = false;
        return !(nullable.GetValueOrDefault() == flag & nullable.HasValue) && Collision.CheckAABBvLineCollision(Utils.TopLeft(((Entity) Main.LocalPlayer).Hitbox), Utils.Size(((Entity) Main.LocalPlayer).Hitbox), ((Entity) Projectile).Center, Vector2.op_Addition(((Entity) Projectile).Center, Vector2.op_Multiply(((Entity) Projectile).velocity, Projectile.localAI[1])), (float) (22.0 * (double) Projectile.scale + (double) Main.LocalPlayer.FargoSouls().GrazeRadius * 2.0 + 42.0), ref num);
      });
      this.Projectile.hide = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual void PostAI()
    {
      if (this.Projectile.hide)
      {
        this.Projectile.hide = false;
        if (this.Projectile.friendly)
          this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      }
      if (this.Projectile.FargoSouls().GrazeCD <= this.grazeCD)
        return;
      this.Projectile.FargoSouls().GrazeCD = this.grazeCD;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 50), 0.95f));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        return false;
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Texture2D texture1 = TextureAssets.Projectile[this.Projectile.type].Value;
      Texture2D texture2 = ModContent.Request<Texture2D>(this.Texture + "2", (AssetRequestMode) 1).Value;
      Texture2D texture3 = ModContent.Request<Texture2D>(this.Texture + "3", (AssetRequestMode) 1).Value;
      Rectangle frame1 = GetFrame(texture1);
      Rectangle frame2 = GetFrame(texture2);
      Rectangle frame3 = GetFrame(texture3);
      int num1 = this.sheeting == BaseDeathray.TextureSheeting.Vertical ? Main.projFrames[this.Projectile.type] : 1;
      float num2 = this.Projectile.localAI[1];
      Color color = Color.Lerp(this.Projectile.GetAlpha(lightColor), Color.Transparent, this.transparency);
      Main.EntitySpriteDraw(texture1, Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Rectangle?(frame1), color, this.Projectile.rotation, Vector2.op_Division(Utils.Size(frame1), 2f), this.Projectile.scale, spriteEffects, 0.0f);
      float num3 = num2 - (float) (texture1.Height / 2 + texture3.Height) * this.Projectile.scale / (float) num1;
      Vector2 vector2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.scale), (float) texture1.Height), 2f), (float) num1));
      if ((double) num3 > 0.0)
      {
        float num4 = 0.0f;
        Rectangle rectangle = frame2;
        int num5 = this.sheeting == BaseDeathray.TextureSheeting.Vertical ? texture2.Height / Main.projFrames[this.Projectile.type] * this.Projectile.frame : 0;
        int num6 = rectangle.Height - num5;
        rectangle.Height /= num1;
        while ((double) num4 + 1.0 < (double) num3)
        {
          if ((double) num3 - (double) num4 < (double) num6)
            rectangle.Height = num5 + (int) ((double) num3 - (double) num4);
          Main.EntitySpriteDraw(texture2, Vector2.op_Subtraction(vector2, Main.screenPosition), new Rectangle?(rectangle), color, this.Projectile.rotation, new Vector2((float) (rectangle.Width / 2), 0.0f), this.Projectile.scale, spriteEffects, 0.0f);
          num4 += (float) rectangle.Height * this.Projectile.scale;
          vector2 = Vector2.op_Addition(vector2, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, (float) rectangle.Height), this.Projectile.scale));
          rectangle.Y += 16;
          if (rectangle.Y + rectangle.Height > texture2.Height / num1)
            rectangle.Y = num5;
        }
      }
      Main.EntitySpriteDraw(texture3, Vector2.op_Subtraction(vector2, Main.screenPosition), new Rectangle?(frame3), color, this.Projectile.rotation, new Vector2((float) (frame3.Width / 2), 0.0f), this.Projectile.scale, spriteEffects, 0.0f);
      return false;

      Rectangle GetFrame(Texture2D texture)
      {
        return Utils.Frame(texture, this.sheeting == BaseDeathray.TextureSheeting.Horizontal ? Main.projFrames[this.Projectile.type] : 1, this.sheeting == BaseDeathray.TextureSheeting.Vertical ? Main.projFrames[this.Projectile.type] : 1, this.sheeting == BaseDeathray.TextureSheeting.Horizontal ? this.Projectile.frame : 0, this.sheeting == BaseDeathray.TextureSheeting.Vertical ? this.Projectile.frame : 0, 0, 0);
      }
    }

    public virtual void CutTiles()
    {
      DelegateMethods.tilecut_0 = (TileCuttingContext) 2;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1])), (float) ((Entity) this.Projectile).width * this.Projectile.scale, BaseDeathray.\u003C\u003EO.\u003C0\u003E__CutTiles ?? (BaseDeathray.\u003C\u003EO.\u003C0\u003E__CutTiles = new Utils.TileActionAttempt((object) null, __methodptr(CutTiles))));
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (((Rectangle) ref projHitbox).Intersects(targetHitbox))
        return new bool?(true);
      float num = 0.0f;
      return Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), ((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1])), 22f * this.Projectile.scale * this.hitboxModifier, ref num) ? new bool?(true) : new bool?(false);
    }

    protected enum TextureSheeting
    {
      Horizontal,
      Vertical,
    }
  }
}
