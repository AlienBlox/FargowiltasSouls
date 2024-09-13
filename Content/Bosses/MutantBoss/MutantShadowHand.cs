// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantShadowHand
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantShadowHand : MutantFishron
  {
    public override string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "Terraria/Images/Projectile_965" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantShadowHand_April";
      }
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = Main.projFrames[965];
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 50;
      this.Projectile.alpha = 0;
      this.Projectile.scale = 1.5f;
    }

    public override bool PreAI() => true;

    public override void AI()
    {
      if ((double) this.Projectile.localAI[1] == 0.0)
      {
        this.Projectile.localAI[1] = 1f;
        SoundEngine.PlaySound(ref SoundID.DD2_GhastlyGlaiveImpactGhost, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        this.p = Luminance.Common.Utilities.Utilities.AnyBosses() ? Main.npc[FargoSoulsGlobalNPC.boss].target : (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
        this.Projectile.netUpdate = true;
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 6.28318548f);
      }
      if ((double) ++this.Projectile.localAI[0] > 85.0)
      {
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
      }
      else
      {
        int p = this.p;
        if ((double) this.Projectile.localAI[0] == 85.0)
        {
          ((Entity) this.Projectile).velocity = Vector2.op_Subtraction(((Entity) Main.player[p]).Center, ((Entity) this.Projectile).Center);
          ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, this.Projectile.type == ModContent.ProjectileType<MutantFishron>() ? 24f : 20f);
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
        }
        else
        {
          Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[p]).Center, ((Entity) this.Projectile).Center);
          this.Projectile.rotation += ((Vector2) ref ((Entity) this.Projectile).velocity).Length() / 20f;
          this.Projectile.rotation = Utils.AngleLerp(this.Projectile.rotation, Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.player[p]).Center, ((Entity) this.Projectile).Center)), (float) ((double) this.Projectile.localAI[0] / 85.0 * 0.079999998211860657));
          if ((double) vector2_1.X > 0.0)
          {
            vector2_1.X -= 300f;
            ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = 1;
          }
          else
          {
            vector2_1.X += 300f;
            ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = -1;
          }
          Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) Main.player[p]).Center, new Vector2(this.Projectile.ai[0], this.Projectile.ai[1])), ((Entity) this.Projectile).Center), 4f);
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 19f), vector2_2), 20f);
          Projectile projectile = this.Projectile;
          ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Division(((Entity) Main.player[p]).velocity, 2f));
        }
      }
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      }
      target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 900, true, false);
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
      Color color1 = lightColor;
      Color color2 = Color.op_Multiply(Color.Black, this.Projectile.Opacity);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      float num3 = 0.0f;
      if (this.Projectile.spriteDirection < 0)
        num3 += 3.14159274f;
      float num4 = (double) this.Projectile.localAI[0] > 85.0 ? 0.6f : 0.3f;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color3 = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.LightBlue, this.Projectile.Opacity), num4), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num5 = this.Projectile.oldRot[index] + num3;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color3, num5, vector2, this.Projectile.scale * 1.2f, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, this.Projectile.rotation + num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
