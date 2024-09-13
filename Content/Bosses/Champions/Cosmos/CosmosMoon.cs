// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Cosmos.CosmosMoon
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Cosmos
{
  public class CosmosMoon : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 410;
      ((Entity) this.Projectile).height = 410;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.netImportant = true;
      this.Projectile.extraUpdates = 0;
      this.CooldownSlot = 0;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.penetrate = -1;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      int num1 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X;
      int num2 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y;
      if (Math.Abs(num1) > targetHitbox.Width / 2)
        num1 = targetHitbox.Width / 2 * Math.Sign(num1);
      if (Math.Abs(num2) > targetHitbox.Height / 2)
        num2 = targetHitbox.Height / 2 * Math.Sign(num2);
      int num3 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X - num1;
      int num4 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y - num2;
      return new bool?(Math.Sqrt((double) (num3 * num3 + num4 * num4)) <= (double) (((Entity) this.Projectile).width / 2));
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        this.Projectile.rotation = this.Projectile.ai[0];
      }
      if ((double) this.Projectile.localAI[0] == 2.0)
      {
        this.Projectile.damage = 0;
        ((Entity) this.Projectile).velocity.Y += 0.2f;
        this.Projectile.tileCollide = true;
      }
      else
      {
        NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<CosmosChampion>());
        if (npc != null)
        {
          this.Projectile.timeLeft = 600;
          float num = Math.Abs(850f * (float) Math.Sin((double) npc.ai[2] * 2.0 * 3.1415927410125732 / 200.0)) + 150f;
          this.Projectile.ai[0] += 0.01f;
          ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(num, Utils.ToRotationVector2(this.Projectile.ai[0])));
        }
        else
        {
          this.Projectile.localAI[0] = 2f;
          ((Entity) this.Projectile).velocity = Vector2.op_Subtraction(((Entity) this.Projectile).position, this.Projectile.oldPos[1]);
        }
      }
      this.Projectile.rotation += 0.04f;
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      width = height = (int) ((double) ((Entity) this.Projectile).width / Math.Sqrt(2.0));
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item89, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
        ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
      if (FargoSoulsUtil.HostCheck)
      {
        for (int index = 0; index < 8; ++index)
        {
          float num = (float) (0.78539818525314331 * ((double) index + (double) Utils.NextFloat(Main.rand, -0.5f, 0.5f)));
          Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<MoonLordMoonBlast>(), 0, this.Projectile.knockBack, this.Projectile.owner, num, 3f, 0.0f);
        }
        if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.moonBoss, 398))
          Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<MoonLordMoonBlast>(), 0, this.Projectile.knockBack, this.Projectile.owner, -Utils.ToRotation(Vector2.UnitY), 32f, 0.0f);
      }
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector(500f, 500f);
      Vector2 center = ((Entity) this.Projectile).Center;
      center.X -= vector2_1.X / 2f;
      center.Y -= vector2_1.Y / 2f;
      for (int index1 = 0; index1 < 30; ++index1)
      {
        int index2 = Dust.NewDust(center, (int) vector2_1.X, (int) vector2_1.Y, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 20; ++index3)
      {
        int index4 = Dust.NewDust(center, (int) vector2_1.X, (int) vector2_1.Y, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index5 = Dust.NewDust(center, (int) vector2_1.X, (int) vector2_1.Y, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      for (int index6 = 0; index6 < 2; ++index6)
      {
        float num = 0.4f;
        if (index6 == 1)
          num = 0.8f;
        int index7 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore1 = Main.gore[index7];
        gore1.velocity = Vector2.op_Multiply(gore1.velocity, num);
        ++Main.gore[index7].velocity.X;
        ++Main.gore[index7].velocity.Y;
        int index8 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore2 = Main.gore[index8];
        gore2.velocity = Vector2.op_Multiply(gore2.velocity, num);
        --Main.gore[index8].velocity.X;
        ++Main.gore[index8].velocity.Y;
        int index9 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore3 = Main.gore[index9];
        gore3.velocity = Vector2.op_Multiply(gore3.velocity, num);
        ++Main.gore[index9].velocity.X;
        --Main.gore[index9].velocity.Y;
        int index10 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore4 = Main.gore[index10];
        gore4.velocity = Vector2.op_Multiply(gore4.velocity, num);
        --Main.gore[index10].velocity.X;
        --Main.gore[index10].velocity.Y;
      }
      for (int index11 = 0; index11 < 20; ++index11)
      {
        Vector2 vector2_2 = center;
        vector2_2.X += (float) Main.rand.Next((int) vector2_1.X);
        vector2_2.Y += (float) Main.rand.Next((int) vector2_1.Y);
        for (int index12 = 0; index12 < 15; ++index12)
        {
          int index13 = Dust.NewDust(vector2_2, 32, 32, 31, 0.0f, 0.0f, 100, new Color(), 3f);
          Dust dust = Main.dust[index13];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
        }
        for (int index14 = 0; index14 < 10; ++index14)
        {
          int index15 = Dust.NewDust(vector2_2, 32, 32, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
          Main.dust[index15].noGravity = true;
          Dust dust3 = Main.dust[index15];
          dust3.velocity = Vector2.op_Multiply(dust3.velocity, 7f);
          int index16 = Dust.NewDust(vector2_2, 32, 32, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust4 = Main.dust[index16];
          dust4.velocity = Vector2.op_Multiply(dust4.velocity, 3f);
        }
        float num = 0.5f;
        for (int index17 = 0; index17 < 2; ++index17)
        {
          int index18 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_2, new Vector2(), Main.rand.Next(61, 64), 1f);
          Gore gore = Main.gore[index18];
          gore.velocity = Vector2.op_Multiply(gore.velocity, num);
          ++Main.gore[index18].velocity.X;
          ++Main.gore[index18].velocity.Y;
        }
      }
      for (int index19 = 0; index19 < 30; ++index19)
      {
        Vector2 vector2_3 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitX, 40f), (double) (index19 - 14) * 6.2831854820251465 / 30.0, new Vector2()), ((Entity) this.Projectile).Center);
        Vector2 vector2_4 = Vector2.op_Subtraction(vector2_3, ((Entity) this.Projectile).Center);
        int index20 = Dust.NewDust(Vector2.op_Addition(vector2_3, vector2_4), 0, 0, 6, 0.0f, 0.0f, 0, new Color(), 3f);
        Main.dust[index20].noGravity = true;
        Main.dust[index20].velocity = vector2_4;
      }
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 2f);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(36, 300, true, false);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 300, true, false);
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
      Color.op_Multiply(new Color(Main.DiscoR + 210, Main.DiscoG + 210, Main.DiscoB + 210), this.Projectile.Opacity);
      Color color = Color.op_Multiply(new Color(Main.DiscoR + 50, Main.DiscoG + 50, Main.DiscoB + 50), this.Projectile.Opacity);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(color, 0.35f), num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(color, 0.35f), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      return false;
    }
  }
}
