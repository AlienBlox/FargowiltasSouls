// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.EaterBody
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class EaterBody : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      EModeGlobalProjectile.IgnoreMinionNerf[this.Type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 28;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft *= 5;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.friendly = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.netImportant = true;
      this.Projectile.minionSlots = 0.25f;
      this.Projectile.hide = true;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 25;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.Projectile.localAI[1]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.Projectile.localAI[1] = reader.ReadSingle();
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);

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

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      Color color = Lighting.GetColor((int) ((double) ((Entity) this.Projectile).Center.X / 16.0), (int) ((double) ((Entity) this.Projectile).Center.Y / 16.0));
      int num2 = num1 * this.Projectile.frame;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(new Rectangle(0, num2, texture2D.Width, num1)), color, this.Projectile.rotation, new Vector2((float) texture2D.Width / 2f, (float) num1 / 2f), this.Projectile.scale, this.Projectile.spriteDirection == 1 ? (SpriteEffects) 0 : (SpriteEffects) 1, 0.0f);
      return false;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if ((int) Main.time % 120 == 0)
        this.Projectile.netUpdate = true;
      if (!((Entity) player).active)
      {
        ((Entity) this.Projectile).active = false;
      }
      else
      {
        if (player.dead)
          fargoSoulsPlayer.EaterMinion = false;
        if (fargoSoulsPlayer.EaterMinion)
          this.Projectile.timeLeft = 2;
        int num1 = 30;
        bool flag = false;
        Vector2 vector2_1 = Vector2.Zero;
        Vector2 zero = Vector2.Zero;
        float num2 = 0.0f;
        if ((double) this.Projectile.ai[1] == 1.0)
        {
          this.Projectile.ai[1] = 0.0f;
          this.Projectile.netUpdate = true;
        }
        int projectileByIdentity = FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, (int) this.Projectile.ai[0], new int[2]
        {
          this.Projectile.type,
          ModContent.ProjectileType<EaterHead>()
        });
        if (projectileByIdentity >= 0 && ((Entity) Main.projectile[projectileByIdentity]).active)
        {
          flag = true;
          vector2_1 = ((Entity) Main.projectile[projectileByIdentity]).Center;
          Vector2 velocity = ((Entity) Main.projectile[projectileByIdentity]).velocity;
          num2 = Main.projectile[projectileByIdentity].rotation;
          double num3 = (double) MathHelper.Clamp(Main.projectile[projectileByIdentity].scale, 0.0f, 50f);
          int alpha = Main.projectile[projectileByIdentity].alpha;
          Main.projectile[projectileByIdentity].localAI[0] = this.Projectile.localAI[0] + 1f;
          if (Main.projectile[projectileByIdentity].type != ModContent.ProjectileType<EaterHead>())
            Main.projectile[projectileByIdentity].localAI[1] = (float) this.Projectile.identity;
        }
        if (!flag)
          return;
        if (this.Projectile.alpha > 0)
        {
          for (int index1 = 0; index1 < 2; ++index1)
          {
            int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 135, 0.0f, 0.0f, 100, new Color(), 2f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].noLight = true;
          }
        }
        this.Projectile.alpha -= 42;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
        if ((double) num2 != (double) this.Projectile.rotation)
        {
          float num4 = MathHelper.WrapAngle(num2 - this.Projectile.rotation);
          vector2_2 = Utils.RotatedBy(vector2_2, (double) num4 * 0.10000000149011612, new Vector2());
        }
        this.Projectile.rotation = Utils.ToRotation(vector2_2) + 1.57079637f;
        ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
        ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = (int) ((double) num1 * (double) this.Projectile.scale);
        ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
        float num5 = 26f;
        if (Main.projectile[projectileByIdentity].type == ModContent.ProjectileType<EaterHead>())
          num5 = 32f;
        if (Vector2.op_Inequality(vector2_2, Vector2.Zero))
          ((Entity) this.Projectile).Center = Vector2.op_Subtraction(vector2_1, Vector2.op_Multiply(Vector2.Normalize(vector2_2), num5));
        this.Projectile.spriteDirection = (double) vector2_2.X > 0.0 ? 1 : -1;
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      Player player = Main.player[this.Projectile.owner];
      if ((double) player.slotsMinions + (double) this.Projectile.minionSlots <= (double) player.maxMinions || this.Projectile.owner != Main.myPlayer)
        return;
      int projectileByIdentity1 = FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, (int) this.Projectile.ai[0], new int[2]
      {
        this.Projectile.type,
        ModContent.ProjectileType<EaterHead>()
      });
      if (projectileByIdentity1 == -1)
        return;
      Projectile projectile1 = Main.projectile[projectileByIdentity1];
      if (projectile1.type != ModContent.ProjectileType<EaterHead>())
        projectile1.localAI[1] = this.Projectile.localAI[1];
      int projectileByIdentity2 = FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, (int) this.Projectile.localAI[1], new int[2]
      {
        this.Projectile.type,
        ModContent.ProjectileType<EaterHead>()
      });
      if (projectileByIdentity2 == -1)
        return;
      Projectile projectile2 = Main.projectile[projectileByIdentity2];
      projectile2.ai[0] = this.Projectile.ai[0];
      projectile2.ai[1] = 1f;
      projectile2.netUpdate = true;
    }
  }
}
