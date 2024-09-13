// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.CrystalSkull
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class CrystalSkull : ModProjectile
  {
    private int clickTimer;

    public virtual string Texture => "Terraria/Images/NPC_289";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 6;

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 50;
      ((Entity) this.Projectile).height = 50;
      this.Projectile.timeLeft *= 5;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.hide = true;
      this.Projectile.scale = 0.5f;
    }

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

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (((Entity) player).active && !player.dead && player.FargoSouls().CrystalSkullMinion)
        this.Projectile.timeLeft = 2;
      if (this.Projectile.originalDamage == 0)
        this.Projectile.originalDamage = 30;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector(0.0f, -60f);
      Vector2 vector2_2 = Vector2.op_Addition(player.MountedCenter, vector2_1);
      double num1 = (double) Vector2.Distance(((Entity) this.Projectile).Center, vector2_2);
      if (num1 > 1000.0)
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) player).Center, vector2_1);
      Vector2 vector2_3 = Vector2.op_Subtraction(vector2_2, ((Entity) this.Projectile).Center);
      float num2 = 4f;
      if (num1 < (double) num2)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.25f);
      }
      if (Vector2.op_Inequality(vector2_3, Vector2.Zero))
      {
        if ((double) ((Vector2) ref vector2_3).Length() < (double) num2)
          ((Entity) this.Projectile).velocity = vector2_3;
        else
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(vector2_3, 0.1f);
      }
      if ((double) this.Projectile.localAI[1] > 0.0)
      {
        --this.Projectile.localAI[1];
        if (this.Projectile.owner == Main.myPlayer)
          this.Projectile.netUpdate = true;
      }
      if (this.Projectile.owner == Main.myPlayer)
      {
        this.Projectile.ai[0] = Main.MouseWorld.X;
        this.Projectile.ai[1] = Main.MouseWorld.Y;
      }
      this.Projectile.rotation = Utils.AngleLerp(this.Projectile.rotation, Utils.ToRotation(Vector2.op_Subtraction(new Vector2(this.Projectile.ai[0], this.Projectile.ai[1]), ((Entity) this.Projectile).Center)), 0.08f);
      this.Projectile.frame = 1;
      if ((double) this.Projectile.localAI[0] < 0.0)
      {
        ++this.Projectile.localAI[0];
        this.Projectile.frame = 4;
        if ((double) this.Projectile.localAI[0] % 5.0 == 0.0)
        {
          SoundEngine.PlaySound(ref SoundID.NPCDeath52, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          if (this.Projectile.owner == Main.myPlayer)
            Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(12f, Utils.RotatedByRandom(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, Main.MouseWorld), (double) MathHelper.ToRadians(4f))), ModContent.ProjectileType<ShadowflamesFriendly>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
        }
      }
      else if (player.controlUseItem || this.clickTimer > 0 && (double) this.Projectile.localAI[0] <= 360.0)
      {
        --this.clickTimer;
        ++this.Projectile.localAI[0];
        if ((double) this.Projectile.localAI[0] == 360.0)
        {
          if (this.Projectile.owner == Main.myPlayer)
            this.Projectile.netUpdate = true;
          for (int index1 = 0; index1 < 64; ++index1)
          {
            Vector2 vector2_4 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.rotation, new Vector2()), 6f), (double) (index1 - 31) * 6.2831854820251465 / 64.0, new Vector2()), ((Entity) this.Projectile).Center);
            Vector2 vector2_5 = Vector2.op_Subtraction(vector2_4, ((Entity) this.Projectile).Center);
            int index2 = Dust.NewDust(Vector2.op_Addition(vector2_4, vector2_5), 0, 0, 27, 0.0f, 0.0f, 0, new Color(), 3f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity = vector2_5;
          }
        }
        if ((double) this.Projectile.localAI[0] > 360.0)
        {
          int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 27, ((Entity) this.Projectile).velocity.X * 0.4f, ((Entity) this.Projectile).velocity.Y * 0.4f, 0, new Color(), 1f);
          Main.dust[index3].noGravity = true;
          Dust dust1 = Main.dust[index3];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 4f);
          Main.dust[index3].scale += 0.5f;
          int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 27, ((Entity) this.Projectile).velocity.X * 0.4f, ((Entity) this.Projectile).velocity.Y * 0.4f, 0, new Color(), 1f);
          Main.dust[index4].noGravity = true;
          Dust dust2 = Main.dust[index4];
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 1.5f);
        }
      }
      else if (this.Projectile.owner == Main.myPlayer)
      {
        if ((double) this.Projectile.localAI[0] > 360.0)
        {
          this.Projectile.localAI[0] = -120f;
          this.Projectile.netUpdate = true;
        }
        else
          this.Projectile.localAI[0] = 0.0f;
      }
      if (player.controlUseItem || player.itemTime > 0 || player.itemAnimation > 0 || player.reuseDelay > 0)
        this.clickTimer = 20;
      this.Projectile.spriteDirection = (double) Math.Abs(MathHelper.WrapAngle(this.Projectile.rotation)) > 1.5707963705062866 ? -1 : 1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 100), this.Projectile.Opacity));
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      float rotation = this.Projectile.rotation;
      if (this.Projectile.spriteDirection < 0)
        rotation += 3.14159274f;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
