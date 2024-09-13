// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.DeviBoss.DeviSparklingLoveSmall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
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
namespace FargowiltasSouls.Content.Bosses.DeviBoss
{
  public class DeviSparklingLoveSmall : ModProjectile
  {
    private const int maxTime = 60;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Items/Weapons/FinalUpgrades/SparklingLove";
    }

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 80;
      ((Entity) this.Projectile).height = 80;
      this.Projectile.scale = 1.5f;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 60;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.hide = true;
    }

    public virtual void AI()
    {
      this.Projectile.hide = false;
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>());
      if (npc != null)
      {
        if ((double) this.Projectile.localAI[0] == 0.0)
          this.Projectile.localAI[1] = this.Projectile.ai[1] / 60f;
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) this.Projectile.ai[1], new Vector2());
        this.Projectile.ai[1] -= this.Projectile.localAI[1];
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(Utils.RotatedBy(new Vector2(50f, 50f), (double) Utils.ToRotation(((Entity) this.Projectile).velocity) - 0.78539818525314331, new Vector2()), this.Projectile.scale));
        if ((double) this.Projectile.localAI[0] == 0.0)
        {
          this.Projectile.localAI[0] = 1f;
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Division(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 141f), 2f), this.Projectile.scale));
          for (int index1 = 0; index1 < 40; ++index1)
          {
            int index2 = Dust.NewDust(Vector2.op_Addition(vector2, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, Utils.NextFloat(Main.rand, 141f)), this.Projectile.scale)), 0, 0, 86, 0.0f, 0.0f, 0, new Color(), 3f);
            Dust dust = Main.dust[index2];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 4.5f);
            Main.dust[index2].noGravity = true;
          }
          SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = Math.Sign(this.Projectile.ai[1]);
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + MathHelper.ToRadians(((Entity) this.Projectile).direction < 0 ? 135f : 45f);
      }
      else
        this.Projectile.Kill();
    }

    public virtual void OnKill(int timeLeft)
    {
      Vector2 vector2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Division(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 141f), 2f), this.Projectile.scale));
      for (int index1 = 0; index1 < 40; ++index1)
      {
        int index2 = Dust.NewDust(Vector2.op_Addition(vector2, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, Utils.NextFloat(Main.rand, 141f)), this.Projectile.scale)), 0, 0, 86, 0.0f, 0.0f, 0, new Color(), 3f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4.5f);
        Main.dust[index2].noGravity = true;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 240, true, false);
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 240, true, false);
      target.AddBuff(ModContent.BuffType<GuiltyBuff>(), 240, true, false);
      target.AddBuff(ModContent.BuffType<LovestruckBuff>(), 240, true, false);
      target.AddBuff(ModContent.BuffType<RottingBuff>(), 240, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      Color color = Color.op_Multiply(lightColor, this.Projectile.Opacity);
      ((Color) ref color).A = (byte) Math.Min((double) byte.MaxValue, (double) byte.MaxValue * Math.Sin(Math.PI * (double) (60 - this.Projectile.timeLeft) / 60.0) * 1.0);
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
      Color alpha = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Items/Weapons/FinalUpgrades/SparklingLove_glow", (AssetRequestMode) 1).Value, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(Color.White, this.Projectile.Opacity), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
