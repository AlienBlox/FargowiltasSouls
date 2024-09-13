// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantBossProjectile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantBossProjectile : ModProjectile
  {
    public bool auraTrail;
    private const int auraFrames = 19;
    public bool sansEye;
    public float SHADOWMUTANTREAL;
    public bool Cake;

    public virtual string Texture
    {
      get
      {
        return "FargowiltasSouls/Content/Bosses/MutantBoss/MutantBoss" + FargoSoulsUtil.TryAprilFoolsTexture;
      }
    }

    public static string trailTexture
    {
      get
      {
        return "FargowiltasSouls/Assets/ExtraTextures/Eternals/MutantSoul" + FargoSoulsUtil.TryAprilFoolsTexture;
      }
    }

    public static int npcType => ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>();

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 4;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 8;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 34;
      ((Entity) this.Projectile).height = 50;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      if (!this.Projectile.hide)
        return;
      behindProjectiles.Add(index);
    }

    public virtual void AI()
    {
      this.Cake = false;
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], MutantBossProjectile.npcType);
      if (npc != null)
      {
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
        this.Projectile.alpha = npc.alpha;
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = ((Entity) npc).direction;
        this.Projectile.timeLeft = 30;
        this.auraTrail = (double) npc.localAI[3] >= 3.0;
        this.Projectile.hide = Main.player[this.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantSpearAim>()] > 0 || Main.player[this.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantSpearDash>()] > 0 || Main.player[this.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantSpearSpin>()] > 0 || Main.player[this.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantSlimeRain>()] > 0;
        this.sansEye = (double) npc.ai[0] == 10.0 && (double) npc.ai[1] > 150.0 || (double) npc.ai[0] == -5.0 && (double) npc.ai[2] > 330.0 && (double) npc.ai[2] < 420.0;
        if ((double) npc.ai[0] == 10.0 && WorldSavingSystem.EternityMode)
        {
          this.SHADOWMUTANTREAL += 0.03f;
          if ((double) this.SHADOWMUTANTREAL > 0.75)
            this.SHADOWMUTANTREAL = 0.75f;
          if ((double) npc.ai[1] > 150.0 && WorldSavingSystem.MasochistModeReal && Main.getGoodWorld)
            this.Cake = true;
        }
        this.Projectile.localAI[1] = this.sansEye ? MathHelper.Lerp(this.Projectile.localAI[1], 1f, 0.05f) : 0.0f;
        this.Projectile.ai[0] = this.sansEye ? this.Projectile.ai[0] + 1f : 0.0f;
        if (WorldSavingSystem.MasochistModeReal && ((double) npc.ai[0] >= 11.0 || (double) npc.ai[0] < 0.0))
        {
          this.sansEye = true;
          this.Projectile.ai[0] = -1f;
        }
        if (!Main.dedServ)
          this.Projectile.frame = (int) ((double) npc.frame.Y / (double) (TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type]));
        if (npc.frameCounter == 0.0 && (double) ++this.Projectile.localAI[0] >= 19.0)
          this.Projectile.localAI[0] = 0.0f;
        if (!npc.HasValidTarget && (double) ((Entity) npc).velocity.Y < 0.0 && WorldSavingSystem.MasochistModeReal && Main.getGoodWorld)
          this.Cake = true;
        this.SHADOWMUTANTREAL -= 0.01f;
        if ((double) this.SHADOWMUTANTREAL >= 0.0)
          return;
        this.SHADOWMUTANTREAL = 0.0f;
      }
      else
      {
        this.sansEye = false;
        if (!FargoSoulsUtil.HostCheck)
          return;
        this.Projectile.Kill();
      }
    }

    public virtual void OnKill(int timeLeft)
    {
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      Texture2D texture2D2 = ModContent.Request<Texture2D>(MutantBossProjectile.trailTexture, (AssetRequestMode) 1).Value;
      if (this.Cake)
      {
        texture2D1 = texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantCake", (AssetRequestMode) 1).Value;
        this.sansEye = false;
        this.auraTrail = true;
      }
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / (this.Cake ? 1 : Main.projFrames[this.Projectile.type]);
      int num2 = num1 * (this.Cake ? 0 : this.Projectile.frame);
      Rectangle rectangle1;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle1).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle1), 2f);
      Texture2D texture2D3 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantAura" + FargoSoulsUtil.TryAprilFoolsTexture, (AssetRequestMode) 1).Value;
      int num3 = texture2D3.Height / 19;
      int num4 = num3 * (int) this.Projectile.localAI[0];
      Rectangle rectangle2;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle2).\u002Ector(0, num4, texture2D3.Width, num3);
      Color alpha = this.Projectile.GetAlpha(!this.Projectile.hide || Main.netMode != 1 ? lightColor : Lighting.GetColor((int) ((Entity) this.Projectile).Center.X / 16, (int) ((Entity) this.Projectile).Center.Y / 16));
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      float num5 = (float) (((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.40000000596046448 + 0.89999997615814209) * this.Projectile.scale;
      Color color1 = FargoSoulsUtil.AprilFools ? new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 100) : new Color(51, (int) byte.MaxValue, 191, 100);
      Color color2 = Color.op_Multiply(this.Cake ? color1 : new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 200), this.Projectile.Opacity);
      if (this.auraTrail || (double) this.SHADOWMUTANTREAL > 0.0)
      {
        int num6 = this.Cake ? 5 : 1;
        for (int index = 0; index < num6; ++index)
          Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle1), color2, this.Projectile.rotation, vector2_1, num5, spriteEffects, 0.0f);
      }
      if (this.auraTrail)
      {
        for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.25f)
        {
          Color color3 = Color.op_Multiply(Color.op_Multiply(color2, 0.5f), ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          int index2 = (int) index1 - 1;
          if (index2 < 0)
            index2 = 0;
          float num7 = this.Projectile.oldRot[index2];
          Vector2 vector2_2 = Vector2.op_Addition(Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
          Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle1), color3, num7, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
        }
        Main.EntitySpriteDraw(texture2D3, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Multiply(-16f, Vector2.UnitY), ((Entity) this.Projectile).Center), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle2), color2, this.Projectile.rotation, Vector2.op_Division(Utils.Size(rectangle2), 2f), num5, spriteEffects, 0.0f);
      }
      else
      {
        for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
        {
          Color color4 = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          Vector2 oldPo = this.Projectile.oldPos[index];
          float num8 = this.Projectile.oldRot[index];
          Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle1), color4, num8, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      Color color5 = Color.Lerp(alpha, Color.Black, this.SHADOWMUTANTREAL);
      Main.spriteBatch.Draw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle1), color5, this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      if (this.sansEye)
      {
        Color color6 = FargoSoulsUtil.AprilFools ? Color.Red : new Color(51, (int) byte.MaxValue, 191);
        int num9 = !WorldSavingSystem.MasochistModeReal ? 0 : ((double) this.Projectile.ai[0] == -1.0 ? 1 : 0);
        float num10 = this.Projectile.ai[0];
        float num11 = 6.28318548f * this.Projectile.localAI[1];
        float num12 = Math.Min(1f, (float) Math.Sin(Math.PI * (double) num10 / 120.0) * 2f);
        float num13 = num9 != 0 ? 1f : Math.Min(1f, num12 * 2f);
        float num14 = num9 != 0 ? (float) ((double) this.Projectile.scale * (double) Main.cursorScale * 0.800000011920929) * Utils.NextFloat(Main.rand, 0.75f, 1.25f) : (float) ((double) this.Projectile.scale * (double) num12 * (double) Main.cursorScale * 1.25);
        Texture2D texture2D4 = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/Effects/LifeStar", (AssetRequestMode) 1).Value;
        Rectangle rectangle3;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle3).\u002Ector(0, 0, texture2D4.Width, texture2D4.Height);
        Vector2 vector2_3;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_3).\u002Ector((float) (texture2D4.Width / 2) + num14, (float) (texture2D4.Height / 2) + num14);
        Vector2 center = ((Entity) this.Projectile).Center;
        center.X += (float) (8 * this.Projectile.spriteDirection);
        center.Y -= 11f;
        Main.spriteBatch.End();
        Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
        Main.spriteBatch.Draw(texture2D4, Vector2.op_Addition(Vector2.op_Subtraction(center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle3), Color.op_Multiply(color6, num13), num11, vector2_3, num14, (SpriteEffects) 0, 0.0f);
        Main.spriteBatch.Draw(texture2D4, Vector2.op_Addition(Vector2.op_Subtraction(center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle3), Color.op_Multiply(Color.op_Multiply(Color.White, num13), 0.75f), num11, vector2_3, num14, (SpriteEffects) 0, 0.0f);
        Main.spriteBatch.End();
        Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      return false;
    }
  }
}
