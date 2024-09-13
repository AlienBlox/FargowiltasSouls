// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomStyxGazer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.AbomBoss
{
  public class AbomStyxGazer : ModProjectile
  {
    private const int maxTime = 60;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Items/Weapons/FinalUpgrades/StyxGazer";
    }

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 60;
      ((Entity) this.Projectile).height = 60;
      this.Projectile.scale = 1f;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 60;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.hide = true;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      this.Projectile.hide = false;
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>());
      if (npc != null)
      {
        if ((double) npc.ai[0] == 0.0)
          this.Projectile.extraUpdates = 1;
        if ((double) this.Projectile.localAI[0] == 0.0)
          this.Projectile.localAI[1] = this.Projectile.ai[1] / 60f;
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) this.Projectile.ai[1], new Vector2());
        this.Projectile.ai[1] -= this.Projectile.localAI[1];
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(Utils.RotatedBy(new Vector2(60f, 60f), (double) Utils.ToRotation(((Entity) this.Projectile).velocity) - 0.78539818525314331, new Vector2()), this.Projectile.scale));
        if ((double) this.Projectile.localAI[0] == 0.0)
        {
          this.Projectile.localAI[0] = 1f;
          SoundEngine.PlaySound(ref SoundID.Item71, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        this.Projectile.Opacity = (float) Math.Min(1.0, (double) (2 - this.Projectile.extraUpdates) * Math.Sin(Math.PI * (double) (60 - this.Projectile.timeLeft) / 60.0));
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = Math.Sign(this.Projectile.ai[1]);
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + MathHelper.ToRadians(((Entity) this.Projectile).direction < 0 ? 135f : 45f);
      }
      else
        this.Projectile.Kill();
    }

    public virtual void OnKill(int timeLeft)
    {
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.AddBuff(ModContent.BuffType<AbomFangBuff>(), 300, true, false);
        target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 120, true, false);
      }
      target.AddBuff(30, 600, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      Color color = Color.op_Multiply(lightColor, this.Projectile.Opacity);
      ((Color) ref color).A = (byte) ((double) byte.MaxValue * (double) this.Projectile.Opacity);
      return new Color?(color);
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
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Items/Weapons/FinalUpgrades/StyxGazer_glow", (AssetRequestMode) 1).Value, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(Color.White, this.Projectile.Opacity), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
