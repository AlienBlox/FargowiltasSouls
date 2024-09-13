// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.PlanteraCrystalLeafRing
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class PlanteraCrystalLeafRing : MutantCrystalLeaf
  {
    public override string Texture => "Terraria/Images/Projectile_226";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.scale = 1.5f;
      this.CooldownSlot = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public override void AI()
    {
      bool flag = SoulConfig.Instance.BossRecolors && WorldSavingSystem.EternityMode;
      if ((double) ++this.Projectile.localAI[0] == 0.0)
      {
        for (int index1 = 0; index1 < 30; ++index1)
        {
          int num = flag ? (Utils.NextBool(Main.rand) ? 41 : 307) : (Utils.NextBool(Main.rand) ? 107 : 157);
          Vector2 vector2 = Utils.NextVector2Circular(Main.rand, 4f, 4f);
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num, vector2.X, vector2.Y, 0, new Color(), 2f);
          Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
        }
      }
      if (flag)
        Lighting.AddLight(((Entity) this.Projectile).Center, 0.09803922f, 0.184313729f, 0.2509804f);
      else
        Lighting.AddLight(((Entity) this.Projectile).Center, 0.1f, 0.4f, 0.2f);
      this.Projectile.scale = (float) (((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.20000000298023224 + 0.949999988079071);
      this.Projectile.scale *= 1.5f;
      int projectileByIdentity = FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, (int) this.Projectile.ai[0], new int[1]
      {
        ModContent.ProjectileType<MutantMark2>()
      });
      if (projectileByIdentity != -1)
      {
        Vector2 vector2 = Utils.RotatedBy(new Vector2(100f, 0.0f), (double) this.Projectile.ai[1], new Vector2());
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) Main.projectile[projectileByIdentity]).Center, vector2);
        this.Projectile.localAI[1] = Math.Max(0.0f, 150f - Main.projectile[projectileByIdentity].ai[1]) / 150f;
        if ((double) this.Projectile.localAI[1] > 1.0)
          this.Projectile.localAI[1] = 1f;
        this.Projectile.ai[1] += 0.15f * this.Projectile.localAI[1];
      }
      this.Projectile.rotation = this.Projectile.ai[1] + 1.57079637f;
      if ((double) this.Projectile.localAI[0] <= 20.0)
        return;
      this.Projectile.localAI[0] = 1f;
      NPC npc = FargoSoulsUtil.NPCExists(NPC.plantBoss, new int[1]
      {
        262
      });
      if (npc == null || (double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) >= 1600.0 || !FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(4f, Utils.ToRotationVector2(this.Projectile.ai[1])), ModContent.ProjectileType<CrystalLeafShot>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, (float) ((Entity) npc).whoAmI, 0.0f, 0.0f);
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<IvyVenomBuff>(), 240, true, false);
    }

    public override bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = (!SoulConfig.Instance.BossRecolors ? 0 : (WorldSavingSystem.EternityMode ? 1 : 0)) != 0 ? ModContent.Request<Texture2D>("FargowiltasSouls/Content/NPCs/EternityModeNPCs/CrystalLeaf", (AssetRequestMode) 2).Value : TextureAssets.Projectile[this.Type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      this.Projectile.GetAlpha(lightColor);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), this.Projectile.localAI[1]), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
