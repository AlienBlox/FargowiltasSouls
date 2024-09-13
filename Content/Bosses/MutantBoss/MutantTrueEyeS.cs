// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantTrueEyeS
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
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantTrueEyeS : ModProjectile
  {
    private float localAI0;
    private float localAI1;

    public virtual string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "Terraria/Images/Projectile_650" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantTrueEye_April";
      }
    }

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 4;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 42;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.CooldownSlot = 1;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[1]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[1] = reader.ReadSingle();
    }

    public virtual void AI()
    {
      Player target = Main.player[(int) this.Projectile.ai[0]];
      ++this.Projectile.localAI[0];
      switch ((int) this.Projectile.ai[1])
      {
        case 0:
          Vector2 vector2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) target).Center, ((Entity) this.Projectile).Center), new Vector2(-200f * this.Projectile.localAI[1], -200f));
          if (Vector2.op_Inequality(vector2, Vector2.Zero))
          {
            ((Vector2) ref vector2).Normalize();
            vector2 = Vector2.op_Multiply(vector2, 24f);
            ((Entity) this.Projectile).velocity.X = (float) (((double) ((Entity) this.Projectile).velocity.X * 29.0 + (double) vector2.X) / 30.0);
            ((Entity) this.Projectile).velocity.Y = (float) (((double) ((Entity) this.Projectile).velocity.Y * 29.0 + (double) vector2.Y) / 30.0);
          }
          if ((double) ((Entity) this.Projectile).Distance(((Entity) target).Center) < 150.0)
          {
            if ((double) ((Entity) this.Projectile).Center.X < (double) ((Entity) target).Center.X)
              ((Entity) this.Projectile).velocity.X -= 0.25f;
            else
              ((Entity) this.Projectile).velocity.X += 0.25f;
            if ((double) ((Entity) this.Projectile).Center.Y < (double) ((Entity) target).Center.Y)
              ((Entity) this.Projectile).velocity.Y -= 0.25f;
            else
              ((Entity) this.Projectile).velocity.Y += 0.25f;
          }
          if ((double) this.Projectile.localAI[0] > 60.0)
          {
            this.Projectile.localAI[0] = 0.0f;
            ++this.Projectile.ai[1];
            this.Projectile.netUpdate = true;
            break;
          }
          break;
        case 1:
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.9f);
          if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 1.0)
          {
            ((Entity) this.Projectile).velocity = Vector2.Zero;
            this.Projectile.localAI[0] = 0.0f;
            ++this.Projectile.ai[1];
            this.Projectile.netUpdate = true;
            break;
          }
          break;
        case 2:
          if ((double) this.Projectile.localAI[0] == 7.0)
          {
            SoundEngine.PlaySound(ref SoundID.NPCDeath6, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
            this.ShootBolts(target);
            break;
          }
          if ((double) this.Projectile.localAI[0] == 14.0)
          {
            this.ShootBolts(target);
            break;
          }
          if ((double) this.Projectile.localAI[0] > 21.0)
          {
            this.Projectile.localAI[0] = 0.0f;
            ++this.Projectile.ai[1];
            break;
          }
          break;
        default:
          for (int index1 = 0; index1 < 30; ++index1)
          {
            int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 135, 0.0f, 0.0f, 0, new Color(), 3f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].noLight = true;
            Dust dust = Main.dust[index2];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 8f);
          }
          SoundEngine.PlaySound(ref SoundID.Zombie102, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          this.Projectile.Kill();
          break;
      }
      if ((double) this.Projectile.rotation > 3.14159274101257)
        this.Projectile.rotation -= 6.283185f;
      this.Projectile.rotation = (double) this.Projectile.rotation <= -0.005 || (double) this.Projectile.rotation >= 0.005 ? this.Projectile.rotation * 0.96f : 0.0f;
      if (++this.Projectile.frameCounter >= 4)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          this.Projectile.frame = 0;
      }
      if ((double) this.Projectile.ai[1] == 2.0)
        return;
      this.UpdatePupil();
    }

    private void ShootBolts(Player target)
    {
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.UnitY, 6f));
      Vector2 vector2_2 = Vector2.op_Subtraction(Vector2.op_Addition(((Entity) target).Center, Vector2.op_Multiply(((Entity) target).velocity, 15f)), vector2_1);
      if (!Vector2.op_Inequality(vector2_2, Vector2.Zero))
        return;
      ((Vector2) ref vector2_2).Normalize();
      vector2_2 = Vector2.op_Multiply(vector2_2, 8f);
      if (!FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), vector2_1, vector2_2, 462, this.Projectile.damage, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
    }

    private void UpdatePupil()
    {
      float num1 = (float) ((double) this.localAI0 % 6.28318548202515 - 3.14159274101257);
      float num2 = (float) Math.IEEERemainder((double) this.localAI1, 1.0);
      if ((double) num2 < 0.0)
        ++num2;
      float num3 = (float) Math.Floor((double) this.localAI1);
      float num4 = 0.999f;
      float num5 = 0.1f;
      double num6 = (double) ((Entity) this.Projectile).AngleTo(((Entity) Main.player[(int) this.Projectile.ai[0]]).Center);
      int num7 = 2;
      float num8 = MathHelper.Clamp(num2 + 0.05f, 0.0f, num4);
      float num9 = num3 + (float) Math.Sign(-12f - num3);
      Vector2 rotationVector2 = Utils.ToRotationVector2((float) num6);
      this.localAI0 = (float) ((double) Utils.ToRotation(Vector2.Lerp(Utils.ToRotationVector2(num1), rotationVector2, num5)) + (double) num7 * 6.28318548202515 + 3.14159274101257);
      this.localAI1 = num9 + num8;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
    }

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      float num3 = (float) (((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.40000000596046448 + 1.0) * this.Projectile.scale;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color1 = Color.op_Multiply(alpha, 0.75f);
        ((Color) ref color1).A = (byte) 0;
        Color color2 = Color.op_Multiply(color1, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num4 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, num4, vector2_1, num3, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/Minions/TrueEyePupil", (AssetRequestMode) 1).Value;
      Vector2 vector2_2 = Vector2.op_Addition(Utils.RotatedBy(new Vector2(this.localAI1 / 2f, 0.0f), (double) this.localAI0, new Vector2()), Utils.RotatedBy(new Vector2(0.0f, -6f), (double) this.Projectile.rotation, new Vector2()));
      Vector2 vector2_3 = Vector2.op_Division(Utils.Size(texture2D2), 2f);
      Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, ((Entity) this.Projectile).Center), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(texture2D2.Bounds), alpha, 0.0f, vector2_3, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
