// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantSansBeam
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantSansBeam : BaseDeathray
  {
    private const int descendTime = 50;

    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/GolemBeam";

    public MutantSansBeam()
      : base(420f)
    {
    }

    public virtual bool CanHitPlayer(Player target) => target.hurtCooldowns[1] == 0;

    public virtual bool? CanDamage() => new bool?((double) this.Projectile.localAI[0] > 50.0);

    public virtual void AI()
    {
      this.Projectile.alpha = 0;
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      Projectile projectile = FargoSoulsUtil.ProjectileExists(FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, this.Projectile.ai[1]), new int[1]
      {
        ModContent.ProjectileType<MutantSansHead>()
      });
      if (projectile != null)
      {
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 16f), 3f));
        if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
          ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
        if ((double) this.Projectile.localAI[0] == 0.0 && !Main.dedServ)
        {
          SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/GolemBeam", (SoundType) 0);
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        float num1 = 1.3f;
        ++this.Projectile.localAI[0];
        if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
        {
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.scale = num1;
          float rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          this.Projectile.rotation = rotation - 1.57079637f;
          ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(rotation);
          float length = 3f;
          int width = ((Entity) this.Projectile).width;
          Vector2 center = ((Entity) this.Projectile).Center;
          if (nullable.HasValue)
          {
            Vector2 vector2 = nullable.Value;
          }
          float[] numArray = new float[(int) length];
          for (int index = 0; index < numArray.Length; ++index)
            numArray[index] = 1800f;
          float num2 = 0.0f;
          for (int index = 0; index < numArray.Length; ++index)
            num2 += numArray[index];
          float num3 = num2 / length;
          if ((double) this.Projectile.localAI[0] > 50.0)
            return;
          this.Projectile.localAI[1] = MathHelper.Lerp(0.0f, Math.Max(num3, 320f), this.Projectile.localAI[0] / 50f);
          if (++this.Projectile.frameCounter <= 3)
            return;
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame < Main.projFrames[this.Projectile.type])
            return;
          this.Projectile.frame = 0;
        }
      }
      else
        this.Projectile.Kill();
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

    private Rectangle Frame(Texture2D tex)
    {
      int num = tex.Height / Main.projFrames[this.Projectile.type];
      return new Rectangle(0, num * this.Projectile.frame, tex.Width, num);
    }

    public override bool PreDraw(ref Color lightColor)
    {
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        return false;
      SpriteEffects spriteEffects1 = (SpriteEffects) 0;
      Texture2D texture2D1 = ModContent.Request<Texture2D>(base.Texture, (AssetRequestMode) 1).Value;
      Texture2D texture2D2 = ModContent.Request<Texture2D>(base.Texture + "2", (AssetRequestMode) 1).Value;
      Texture2D texture2D3 = ModContent.Request<Texture2D>(base.Texture + "3", (AssetRequestMode) 1).Value;
      float num1 = this.Projectile.localAI[1];
      Texture2D texture2D4 = texture2D1;
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition);
      Rectangle bounds1 = texture2D1.Bounds;
      Vector2 vector2_2 = vector2_1;
      Rectangle? nullable1 = new Rectangle?(bounds1);
      Color color1 = lightColor;
      double rotation1 = (double) this.Projectile.rotation;
      Vector2 vector2_3 = Vector2.op_Division(Utils.Size(bounds1), 2f);
      double scale1 = (double) this.Projectile.scale;
      Main.EntitySpriteDraw(texture2D4, vector2_2, nullable1, color1, (float) rotation1, vector2_3, (float) scale1, (SpriteEffects) 0, 0.0f);
      float num2 = num1 - (float) (texture2D1.Height / 2 + texture2D3.Height) * this.Projectile.scale;
      Vector2 vector2_4 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.scale), (float) texture2D1.Height), 2f));
      if ((double) num2 > 0.0)
      {
        float num3 = 0.0f;
        Rectangle bounds2 = texture2D2.Bounds;
        while ((double) num3 < (double) num2)
        {
          Main.EntitySpriteDraw(texture2D2, Vector2.op_Subtraction(vector2_4, Main.screenPosition), new Rectangle?(bounds2), Lighting.GetColor((int) vector2_4.X / 16, (int) vector2_4.Y / 16), this.Projectile.rotation, Vector2.op_Division(Utils.Size(bounds2), 2f), this.Projectile.scale, spriteEffects1, 0.0f);
          num3 += (float) bounds2.Height * this.Projectile.scale;
          vector2_4 = Vector2.op_Addition(vector2_4, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, (float) bounds2.Height), this.Projectile.scale));
        }
      }
      Texture2D texture2D5 = texture2D3;
      Vector2 vector2_5 = Vector2.op_Subtraction(vector2_4, Main.screenPosition);
      Rectangle bounds3 = texture2D3.Bounds;
      Vector2 vector2_6 = vector2_5;
      Rectangle? nullable2 = new Rectangle?(bounds3);
      Color color2 = Lighting.GetColor((int) vector2_4.X / 16, (int) vector2_4.Y / 16);
      double rotation2 = (double) this.Projectile.rotation;
      Vector2 vector2_7 = Vector2.op_Division(Utils.Size(bounds3), 2f);
      double scale2 = (double) this.Projectile.scale;
      SpriteEffects spriteEffects2 = spriteEffects1;
      Main.EntitySpriteDraw(texture2D5, vector2_6, nullable2, color2, (float) rotation2, vector2_7, (float) scale2, spriteEffects2, 0.0f);
      return false;
    }
  }
}
