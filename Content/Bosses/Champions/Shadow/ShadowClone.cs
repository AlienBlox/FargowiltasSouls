// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Shadow.ShadowClone
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Shadow
{
  public class ShadowClone : ModProjectile
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Bosses/Champions/Shadow/ShadowChampion";
    }

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 5;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 110;
      ((Entity) this.Projectile).height = 110;
      this.Projectile.penetrate = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.CooldownSlot = 1;
      this.Projectile.timeLeft = 720;
    }

    public virtual void AI()
    {
      Player player = Main.player[(int) this.Projectile.ai[0]];
      ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).Center.X < (double) ((Entity) player).Center.X ? 1 : -1;
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 27, 0.0f, 0.0f, 0, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      }
      for (int index3 = 0; index3 < 3; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 54, 0.0f, 0.0f, 0, new Color(), 5f);
        Main.dust[index4].noGravity = true;
      }
      if ((double) --this.Projectile.ai[1] > 0.0)
      {
        Vector2 targetPos = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.Projectile).DirectionFrom(((Entity) player).Center), 400f));
        if ((double) ((Entity) this.Projectile).Distance(targetPos) > 50.0)
          this.Movement(targetPos, 0.3f, 24f);
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Multiply(((Entity) player).velocity, 0.9f));
      }
      else if ((double) this.Projectile.ai[1] == 0.0)
      {
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Division(((Entity) player).velocity, 4f));
        this.Projectile.netUpdate = true;
        this.Projectile.localAI[0] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center));
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center), ModContent.ProjectileType<ShadowDeathraySmall>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      else if ((double) this.Projectile.ai[1] == -30.0)
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(45f, Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.localAI[0], new Vector2()));
      if (++this.Projectile.frameCounter <= 3)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame < 5)
        return;
      this.Projectile.frame = 0;
    }

    private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f, bool fastY = false)
    {
      if ((double) ((Entity) this.Projectile).Center.X < (double) targetPos.X)
      {
        ((Entity) this.Projectile).velocity.X += speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.X < 0.0)
          ((Entity) this.Projectile).velocity.X += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.Projectile).velocity.X -= speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.X > 0.0)
          ((Entity) this.Projectile).velocity.X -= speedModifier * 2f;
      }
      if ((double) ((Entity) this.Projectile).Center.Y < (double) targetPos.Y)
      {
        ((Entity) this.Projectile).velocity.Y += fastY ? speedModifier * 2f : speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.Y < 0.0)
          ((Entity) this.Projectile).velocity.Y += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.Projectile).velocity.Y -= fastY ? speedModifier * 2f : speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.Y > 0.0)
          ((Entity) this.Projectile).velocity.Y -= speedModifier * 2f;
      }
      if ((double) Math.Abs(((Entity) this.Projectile).velocity.X) > (double) cap)
        ((Entity) this.Projectile).velocity.X = cap * (float) Math.Sign(((Entity) this.Projectile).velocity.X);
      if ((double) Math.Abs(((Entity) this.Projectile).velocity.Y) <= (double) cap)
        return;
      ((Entity) this.Projectile).velocity.Y = cap * (float) Math.Sign(((Entity) this.Projectile).velocity.Y);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(22, 300, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300, true, false);
      target.AddBuff(80, 300, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.Black);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      GameShaders.Armor.GetShaderFromItemId(3530).Apply((Entity) this.Projectile, new DrawData?());
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Shadow/ShadowChampion_Trail", (AssetRequestMode) 1).Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.White, 0.25f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      return false;
    }
  }
}
