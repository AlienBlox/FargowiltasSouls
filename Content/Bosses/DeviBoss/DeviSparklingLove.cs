// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.DeviBoss.DeviSparklingLove
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Systems;
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
namespace FargowiltasSouls.Content.Bosses.DeviBoss
{
  public class DeviSparklingLove : ModProjectile
  {
    public int scaleCounter;
    private const int maxTime = 180;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Items/Weapons/FinalUpgrades/SparklingLove";
    }

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 12;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 100;
      ((Entity) this.Projectile).height = 100;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 180;
      this.Projectile.alpha = 250;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>());
      if (npc != null)
      {
        if ((double) this.Projectile.localAI[0] == 0.0)
        {
          this.Projectile.localAI[0] = 1f;
          this.Projectile.localAI[1] = Utils.ToRotation(((Entity) this.Projectile).DirectionFrom(((Entity) npc).Center));
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, -1f, -17f, 0.0f);
        }
        if (this.Projectile.alpha > 0)
        {
          this.Projectile.alpha -= 4;
          if (this.Projectile.alpha < 0)
            this.Projectile.alpha = 0;
        }
        if ((double) ++this.Projectile.localAI[0] > 31.0)
        {
          this.Projectile.localAI[0] = 1f;
          if (++this.scaleCounter < 3)
          {
            ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
            Projectile projectile1 = this.Projectile;
            ((Entity) projectile1).width = ((Entity) projectile1).width * 2;
            Projectile projectile2 = this.Projectile;
            ((Entity) projectile2).height = ((Entity) projectile2).height * 2;
            this.Projectile.scale *= 2f;
            ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
            this.MakeDust();
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, -1f, (float) (this.scaleCounter - 16), 0.0f);
            SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          }
        }
        Vector2 vector2_1 = Utils.RotatedBy(new Vector2(this.Projectile.ai[1], 0.0f), (double) npc.ai[3] + (double) this.Projectile.localAI[1], new Vector2());
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(vector2_1, this.Projectile.scale));
        if (this.Projectile.timeLeft == 2)
        {
          SoundEngine.PlaySound(ref SoundID.NPCDeath6, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, -1f, -14f, 0.0f);
          if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
            ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
          if (FargoSoulsUtil.HostCheck)
          {
            float num1 = WorldSavingSystem.EternityMode ? Utils.NextFloat(Main.rand, 6.28318548f) : 0.0f;
            int num2 = 8;
            if (WorldSavingSystem.EternityMode)
              num2 = 12;
            if (WorldSavingSystem.MasochistModeReal)
              num2 = 8;
            for (int index = 0; index < num2; ++index)
            {
              Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(2f, Vector2.op_Multiply(600f, Utils.RotatedBy(Vector2.UnitX, 6.2831854820251465 / (double) num2 * (double) index + (double) num1, new Vector2()))), 90f);
              float num3 = (float) (-(double) ((Vector2) ref vector2_2).Length() / 90.0);
              float rotation = Utils.ToRotation(vector2_2);
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2_2, ModContent.ProjectileType<DeviEnergyHeart>(), (int) ((double) this.Projectile.damage * 0.75), 0.0f, Main.myPlayer, rotation + 1.57079637f, num3, 0.0f);
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), this.Projectile.damage, 0.0f, Main.myPlayer, 2f, rotation, 0.0f);
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2_2, ModContent.ProjectileType<GlowLine>(), this.Projectile.damage, 0.0f, Main.myPlayer, 2f, rotation + 1.57079637f, 0.0f);
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2_2, ModContent.ProjectileType<GlowLine>(), this.Projectile.damage, 0.0f, Main.myPlayer, 2f, rotation - 1.57079637f, 0.0f);
            }
            if (WorldSavingSystem.MasochistModeReal)
            {
              for (int index = 0; index < num2; ++index)
              {
                Vector2 vector2_3 = Vector2.op_Division(Vector2.op_Multiply(2f, Vector2.op_Multiply(300f, Utils.RotatedBy(Vector2.UnitX, 6.2831854820251465 / (double) num2 * ((double) index + 0.5) + (double) num1, new Vector2()))), 90f);
                float num4 = (float) (-(double) ((Vector2) ref vector2_3).Length() / 90.0);
                float rotation = Utils.ToRotation(vector2_3);
                Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2_3, ModContent.ProjectileType<DeviEnergyHeart>(), (int) ((double) this.Projectile.damage * 0.75), 0.0f, Main.myPlayer, rotation + 1.57079637f, num4, 0.0f);
                Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), this.Projectile.damage, 0.0f, Main.myPlayer, 2f, rotation, 0.0f);
                Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2_3, ModContent.ProjectileType<GlowLine>(), this.Projectile.damage, 0.0f, Main.myPlayer, 2f, rotation + 1.57079637f, 0.0f);
                Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2_3, ModContent.ProjectileType<GlowLine>(), this.Projectile.damage, 0.0f, Main.myPlayer, 2f, rotation - 1.57079637f, 0.0f);
              }
            }
          }
        }
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = ((Entity) npc).direction;
        this.Projectile.rotation = (float) ((double) npc.ai[3] + (double) this.Projectile.localAI[1] + 1.5707963705062866 + 0.78539818525314331);
        if (this.Projectile.spriteDirection < 0)
          return;
        this.Projectile.rotation -= 1.57079637f;
      }
      else
        this.Projectile.Kill();
    }

    public virtual void OnKill(int timeLeft) => this.MakeDust();

    private void MakeDust()
    {
      Vector2 vector2_1 = Vector2.op_Multiply((float) ((Entity) this.Projectile).width, Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.rotation - 0.78539818525314331, new Vector2()));
      if ((double) Math.Abs(vector2_1.X) > (double) (((Entity) this.Projectile).width / 2))
        vector2_1.X = (float) (((Entity) this.Projectile).width / 2 * Math.Sign(vector2_1.X));
      if ((double) Math.Abs(vector2_1.Y) > (double) (((Entity) this.Projectile).height / 2))
        vector2_1.Y = (float) (((Entity) this.Projectile).height / 2 * Math.Sign(vector2_1.Y));
      int num1 = (int) ((Vector2) ref vector2_1).Length();
      vector2_1 = Vector2.Normalize(vector2_1);
      float num2 = (float) ((double) this.scaleCounter / 3.0 + 0.5);
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

    public virtual bool PreDraw(ref Color lightColor)
    {
      Vector2 vector2_1 = Vector2.Zero;
      if (this.Projectile.timeLeft > 30)
      {
        float num = (float) (4.0 * (1.0 - (double) this.Projectile.timeLeft / 180.0));
        vector2_1 = Utils.NextVector2Circular(Main.rand, num, num);
      }
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>());
      if (npc != null && Vector2.op_Inequality(((Entity) npc).velocity, Vector2.Zero))
      {
        Main.spriteBatch.End();
        Main.spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
        GameShaders.Armor.GetShaderFromItemId(1018).Apply((Entity) this.Projectile, new DrawData?());
        for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
        {
          Color color;
          // ISSUE: explicit constructor call
          ((Color) ref color).\u002Ector((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 50);
          color = Color.op_Multiply(Color.op_Multiply(color, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          Vector2 oldPo = this.Projectile.oldPos[index];
          float num3 = this.Projectile.oldRot[index];
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2_2, this.Projectile.scale, spriteEffects, 0.0f);
        }
        Main.spriteBatch.End();
        Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_2, this.Projectile.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Items/Weapons/FinalUpgrades/SparklingLove_glow", (AssetRequestMode) 1).Value, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(Color.White, this.Projectile.Opacity), this.Projectile.rotation, vector2_2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
