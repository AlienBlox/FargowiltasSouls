// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantScythe1
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantScythe1 : ModProjectile
  {
    public virtual string Texture
    {
      get
      {
        return "FargowiltasSouls/Content/Bosses/MutantBoss/MutantScythe1" + FargoSoulsUtil.TryAprilFoolsTexture;
      }
    }

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 3;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.alpha = 0;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 600;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.CooldownSlot = 1;
      this.Projectile.hide = true;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write7BitEncodedInt(this.Projectile.timeLeft);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.timeLeft = reader.Read7BitEncodedInt();
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

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      targetHitbox.Y = ((Rectangle) ref targetHitbox).Center.Y;
      targetHitbox.Height = Math.Min(targetHitbox.Width, targetHitbox.Height);
      targetHitbox.Y -= targetHitbox.Height / 2;
      return base.Colliding(projHitbox, targetHitbox);
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.rotation == 0.0)
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 6.28318548f);
      float num = (float) ((180.0 - (double) this.Projectile.timeLeft + 90.0) / 180.0);
      if ((double) num < 0.0)
        num = 0.0f;
      if ((double) num > 1.0)
        num = 1f;
      this.Projectile.rotation += (float) (0.10000000149011612 + 0.699999988079071 * (double) num);
      if (this.Projectile.timeLeft >= 180)
        return;
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
      {
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(this.Projectile.ai[1]);
        this.Projectile.netUpdate = true;
      }
      Projectile projectile = this.Projectile;
      ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1f + this.Projectile.ai[0]);
    }

    public virtual void PostAI()
    {
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
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
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      target.AddBuff(30, 600, true, false);
    }
  }
}
