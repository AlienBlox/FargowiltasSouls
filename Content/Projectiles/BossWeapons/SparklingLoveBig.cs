// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.SparklingLoveBig
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class SparklingLoveBig : ModProjectile
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Items/Weapons/FinalUpgrades/SparklingLove";
    }

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 12;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 110;
      ((Entity) this.Projectile).height = 110;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 65;
      this.Projectile.aiStyle = -1;
      this.Projectile.scale = 4f;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void AI()
    {
      int projectileByIdentity = FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, (int) this.Projectile.ai[1], new int[1]
      {
        ModContent.ProjectileType<SparklingDevi>()
      });
      if (projectileByIdentity != -1)
      {
        Projectile projectile = Main.projectile[projectileByIdentity];
        if (this.Projectile.timeLeft > 15)
        {
          Vector2 vector2 = Utils.RotatedBy(new Vector2(0.0f, -360f), Math.PI / 4.0 * (double) projectile.spriteDirection, new Vector2());
          ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) projectile).Center, vector2);
          this.Projectile.rotation = (float) (0.78539818525314331 * (double) projectile.spriteDirection - 0.78539818525314331);
        }
        else
        {
          if (this.Projectile.timeLeft == 15)
            this.Projectile.rotation = (float) (0.78539818525314331 * (double) projectile.spriteDirection - 0.78539818525314331);
          this.Projectile.rotation -= (float) (0.20943951606750488 * (double) projectile.spriteDirection * 0.75);
          Vector2 vector2 = Utils.RotatedBy(new Vector2(0.0f, -360f), (double) this.Projectile.rotation + 0.78539818525314331, new Vector2());
          ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) projectile).Center, vector2);
        }
        this.Projectile.spriteDirection = -projectile.spriteDirection;
        this.Projectile.localAI[1] = Utils.ToRotation(((Entity) projectile).velocity);
        if ((double) this.Projectile.localAI[0] != 0.0)
          return;
        this.Projectile.localAI[0] = 1f;
        if (this.Projectile.owner == Main.myPlayer)
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, -1f, -14f, 0.0f);
        SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        this.MakeDust();
      }
      else
      {
        if (this.Projectile.owner != Main.myPlayer || this.Projectile.timeLeft >= 60)
          return;
        this.Projectile.Kill();
      }
    }

    private void MakeDust()
    {
      Vector2 vector2_1 = Vector2.op_Multiply((float) ((Entity) this.Projectile).width, Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.rotation - 0.78539818525314331, new Vector2()));
      if ((double) Math.Abs(vector2_1.X) > (double) (((Entity) this.Projectile).width / 2))
        vector2_1.X = (float) (((Entity) this.Projectile).width / 2 * Math.Sign(vector2_1.X));
      if ((double) Math.Abs(vector2_1.Y) > (double) (((Entity) this.Projectile).height / 2))
        vector2_1.Y = (float) (((Entity) this.Projectile).height / 2 * Math.Sign(vector2_1.Y));
      int num1 = (int) ((Vector2) ref vector2_1).Length();
      vector2_1 = Vector2.Normalize(vector2_1);
      float num2 = 1.5f;
      for (int index1 = -num1; index1 <= num1; index1 += 80)
      {
        Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_1, (float) index1));
        vector2_2.X -= 23f;
        vector2_2.Y -= 23f;
        for (int index2 = 0; index2 < 15; ++index2)
        {
          int index3 = Dust.NewDust(vector2_2, 46, 46, 86, 0.0f, 0.0f, 0, new Color(), num2 * 2.5f);
          Main.dust[index3].noGravity = true;
          Dust dust1 = Main.dust[index3];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 16f * num2);
          int index4 = Dust.NewDust(vector2_2, 46, 46, 86, 0.0f, 0.0f, 0, new Color(), num2);
          Dust dust2 = Main.dust[index4];
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 8f * num2);
          Main.dust[index4].noGravity = true;
        }
        for (int index5 = 0; index5 < 5; ++index5)
        {
          int index6 = Dust.NewDust(vector2_2, 46, 46, 86, 0.0f, 0.0f, 0, new Color(), Utils.NextFloat(Main.rand, 1f, 2f) * num2);
          Dust dust = Main.dust[index6];
          dust.velocity = Vector2.op_Multiply(dust.velocity, Utils.NextFloat(Main.rand, 1f, 4f) * num2);
        }
      }
    }

    public virtual bool? CanDamage()
    {
      this.Projectile.maxPenetrate = 1;
      return new bool?(true);
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      if (this.Projectile.timeLeft >= 15)
        return;
      ((NPC.HitModifiers) ref modifiers).SetCrit();
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(119, 300, false);
    }

    public virtual void OnKill(int timeleft)
    {
      if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
        ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.166666672f);
      this.MakeDust();
      for (int index1 = -1; index1 <= 1; index1 += 2)
      {
        for (int index2 = 0; index2 < 50; ++index2)
        {
          int index3 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 31, (float) index1 * 3f, 0.0f, 50, new Color(), 3f);
          Main.dust[index3].noGravity = Utils.NextBool(Main.rand);
          Dust dust = Main.dust[index3];
          dust.velocity = Vector2.op_Multiply(dust.velocity, Utils.NextFloat(Main.rand, 3f));
        }
        for (int index4 = 0; index4 < 15; ++index4)
        {
          int index5 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
          Main.gore[index5].velocity.X += (float) index4 / 3f * (float) index1;
          Main.gore[index5].velocity.Y += Utils.NextFloat(Main.rand, 2f);
        }
      }
      for (int index6 = 0; index6 < 15; ++index6)
      {
        for (int index7 = 0; index7 < 2; ++index7)
        {
          int index8 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 31, 0.0f, 0.0f, 50, new Color(), 4f);
          Main.dust[index8].noGravity = true;
          Main.dust[index8].velocity.Y -= 3f;
          Dust dust = Main.dust[index8];
          dust.velocity = Vector2.op_Multiply(dust.velocity, Utils.NextFloat(Main.rand, 3f));
        }
        if (Utils.NextBool(Main.rand, 3))
        {
          int index9 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 0.5f);
          Main.gore[index9].velocity.Y -= 3f;
          Gore gore = Main.gore[index9];
          gore.velocity = Vector2.op_Multiply(gore.velocity, Utils.NextFloat(Main.rand, 2f));
        }
      }
      SoundStyle npcDeath6 = SoundID.NPCDeath6;
      ((SoundStyle) ref npcDeath6).Volume = 1.5f;
      SoundEngine.PlaySound(ref npcDeath6, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      SoundStyle soundStyle = SoundID.Item92;
      ((SoundStyle) ref soundStyle).Volume = 1.5f;
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if (this.Projectile.owner == Main.myPlayer)
      {
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, -1f, -14f, 0.0f);
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Zero, 683, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      if (this.Projectile.owner != Main.myPlayer)
        return;
      float num1 = 0.0f;
      for (int index = 0; index < Main.maxProjectiles; ++index)
      {
        if (((Entity) Main.projectile[index]).active && !Main.projectile[index].hostile && Main.projectile[index].owner == this.Projectile.owner && (double) Main.projectile[index].minionSlots > 0.0)
          num1 += Main.projectile[index].minionSlots;
      }
      float num2 = (float) Main.player[this.Projectile.owner].maxMinions - num1;
      if ((double) num2 < 0.0)
        num2 = 0.0f;
      if ((double) num2 > 12.0)
        num2 = 12f;
      int num3 = (int) num2 + 4;
      for (int index = 0; index < num3; ++index)
      {
        Vector2 vector2 = Vector2.op_Division(Vector2.op_Multiply(2f, Vector2.op_Multiply(600f, Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, 2.0 * Math.PI / (double) num3 * (double) index + (double) this.Projectile.localAI[1], new Vector2())))), 90f);
        float num4 = (float) (-(double) ((Vector2) ref vector2).Length() / 90.0);
        float num5 = Utils.ToRotation(vector2) + 1.57079637f;
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2, ModContent.ProjectileType<SparklingLoveEnergyHeart>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, num5, num4, 0.0f);
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(14f, Utils.RotatedBy(Vector2.UnitY, 2.0 * Math.PI / (double) num3 * ((double) index + 0.5) + (double) this.Projectile.localAI[1], new Vector2())), ModContent.ProjectileType<SparklingLoveHeart2>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, -1f, 45f, 0.0f);
      }
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      this.Projectile.GetAlpha(lightColor);
      float num3 = this.Projectile.spriteDirection > 0 ? 0.0f : 1.57079637f;
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      GameShaders.Armor.GetShaderFromItemId(1018).Apply((Entity) this.Projectile, new DrawData?());
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color1 = Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 50), 0.5f);
        if (this.Projectile.timeLeft > 15)
          color1 = Color.op_Multiply(color1, 0.5f);
        Color color2 = Color.op_Multiply(color1, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num4 = this.Projectile.oldRot[index] + num3;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, num4, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation + num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Items/Weapons/FinalUpgrades/SparklingLove_glow", (AssetRequestMode) 1).Value, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(Color.White, this.Projectile.Opacity), this.Projectile.rotation + num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
