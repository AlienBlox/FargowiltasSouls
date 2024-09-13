// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantSansHead
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantSansHead : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Assets/ExtraTextures/Resprites/NPC_246B";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 6;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 70;
      ((Entity) this.Projectile).height = 70;
      this.Projectile.penetrate = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.CooldownSlot = 1;
      this.Projectile.timeLeft = 420;
      this.Projectile.hide = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool CanHitPlayer(Player target) => target.hurtCooldowns[1] == 0;

    public virtual bool? CanDamage() => new bool?((double) this.Projectile.ai[0] < 0.0);

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

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.rotation = (double) this.Projectile.ai[2] == 1.0 ? 0.0f : 3.14159274f;
      }
      if ((double) --this.Projectile.ai[0] == 0.0)
      {
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        this.Projectile.netUpdate = true;
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.UnitY, this.Projectile.ai[2]), ModContent.ProjectileType<MutantSansBeam>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, (float) this.Projectile.identity, 0.0f);
      }
      else if ((double) this.Projectile.ai[0] < -170.0)
        ((Entity) this.Projectile).velocity.X *= 1.025f;
      else if ((double) this.Projectile.ai[0] < -50.0)
      {
        ((Entity) this.Projectile).velocity.X = this.Projectile.ai[1];
        ((Entity) this.Projectile).velocity.Y = 0.0f;
      }
      this.Projectile.frame = 1;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      }
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / 6;
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle1;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle1).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle1), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle1), alpha, this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      Texture2D texture2D2 = TextureAssets.Golem[1].Value;
      Rectangle rectangle2;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle2).\u002Ector(0, texture2D2.Height / 2, texture2D2.Width, texture2D2.Height / 2);
      Vector2 vector2_2 = Vector2.op_Division(Utils.Size(rectangle2), 2f);
      vector2_2.Y -= 4f;
      Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle2), Color.op_Multiply(Color.White, this.Projectile.Opacity), this.Projectile.rotation, vector2_2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
