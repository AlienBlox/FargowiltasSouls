// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.EaterHead
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class EaterHead : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.MinionTargettingFeature[this.Projectile.type] = true;
      EModeGlobalProjectile.IgnoreMinionNerf[this.Type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 28;
      ((Entity) this.Projectile).height = 50;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft *= 5;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.friendly = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.netImportant = true;
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
        Vector2 center = ((Entity) player).Center;
        float num2 = 300f;
        float num3 = 400f;
        int index1 = -1;
        if ((double) ((Entity) this.Projectile).Distance(center) > 1800.0)
        {
          ((Entity) this.Projectile).Center = center;
          this.Projectile.netUpdate = true;
        }
        if (true)
        {
          NPC minionAttackTargetNpc = this.Projectile.OwnerMinionAttackTargetNPC;
          if (minionAttackTargetNpc != null && minionAttackTargetNpc.CanBeChasedBy((object) this.Projectile, false) && (double) ((Entity) this.Projectile).Distance(((Entity) minionAttackTargetNpc).Center) < (double) num2 * 3.0)
          {
            index1 = ((Entity) minionAttackTargetNpc).whoAmI;
            if (minionAttackTargetNpc.boss)
            {
              int whoAmI1 = ((Entity) minionAttackTargetNpc).whoAmI;
            }
            else
            {
              int whoAmI2 = ((Entity) minionAttackTargetNpc).whoAmI;
            }
          }
          if (index1 < 0)
          {
            for (int index2 = 0; index2 < Main.maxNPCs; ++index2)
            {
              NPC npc = Main.npc[index2];
              if (npc.CanBeChasedBy((object) this.Projectile, false) && ((double) ((Entity) player).Distance(((Entity) npc).Center) < (double) num3 || (double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) < (double) num3) && (double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) < (double) num2)
              {
                index1 = index2;
                int num4 = npc.boss ? 1 : 0;
              }
            }
          }
        }
        float y = ((Entity) player).Center.Y;
        for (int index3 = 0; index3 < 20 && !IsInTile(new Vector2(((Entity) player).Center.X, y)); ++index3)
          y += 16f;
        int num5 = (double) ((Entity) this.Projectile).Center.Y > (double) y ? 1 : 0;
        bool flag = false;
        if (num5 != 0 || (double) ((Entity) this.Projectile).Distance(((Entity) Main.player[this.Projectile.owner]).Center) > 1200.0)
        {
          flag = true;
        }
        else
        {
          for (int index4 = 0; index4 < 4; ++index4)
          {
            if (IsInTile(Vector2.op_Addition(((Entity) this.Projectile).Top, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, 16f), (float) index4))))
            {
              flag = true;
              break;
            }
          }
        }
        if (!flag)
        {
          ((Entity) this.Projectile).velocity.Y += 0.4f;
          if ((double) ((Entity) this.Projectile).velocity.Y > 16.0)
            ((Entity) this.Projectile).velocity.Y = 16f;
          if ((double) ((Entity) this.Projectile).velocity.Y > 4.0)
          {
            if ((double) ((Entity) this.Projectile).velocity.X < 0.0)
              ((Entity) this.Projectile).velocity.X += 0.09f;
            else
              ((Entity) this.Projectile).velocity.X -= 0.09f;
          }
          else if ((double) Math.Abs(((Entity) this.Projectile).velocity.X) + (double) Math.Abs(((Entity) this.Projectile).velocity.Y) < 6.4000000953674316)
          {
            if ((double) ((Entity) this.Projectile).velocity.X < 0.0)
              ((Entity) this.Projectile).velocity.X -= 0.11f;
            else
              ((Entity) this.Projectile).velocity.X += 0.11f;
          }
        }
        else if (index1 != -1)
        {
          NPC npc = Main.npc[index1];
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center);
          Utils.ToDirectionInt((double) vector2.X > 0.0);
          Utils.ToDirectionInt((double) vector2.Y > 0.0);
          float num6 = 0.4f;
          if ((double) ((Vector2) ref vector2).Length() < 600.0)
            num6 = 0.6f;
          if ((double) ((Vector2) ref vector2).Length() < 300.0)
            num6 = 0.8f;
          double num7 = (double) ((Vector2) ref vector2).Length();
          Vector2 size = ((Entity) npc).Size;
          double num8 = (double) ((Vector2) ref size).Length() * 0.75;
          if (num7 > num8)
          {
            Projectile projectile1 = this.Projectile;
            ((Entity) projectile1).velocity = Vector2.op_Addition(((Entity) projectile1).velocity, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(vector2), num6), 1.5f));
            if ((double) Vector2.Dot(((Entity) this.Projectile).velocity, vector2) < 0.25)
            {
              Projectile projectile2 = this.Projectile;
              ((Entity) projectile2).velocity = Vector2.op_Multiply(((Entity) projectile2).velocity, 0.8f);
            }
          }
          float num9 = 30f;
          if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() > (double) num9)
            ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), num9);
        }
        else
        {
          Vector2 bottom = ((Entity) player).Bottom;
          float num10 = 0.2f;
          Vector2 vector2 = Vector2.op_Subtraction(bottom, ((Entity) this.Projectile).Center);
          if ((double) ((Vector2) ref vector2).Length() < 200.0)
            num10 = 0.12f;
          if ((double) ((Vector2) ref vector2).Length() < 140.0)
            num10 = 0.06f;
          if ((double) ((Vector2) ref vector2).Length() > 100.0 || (double) Math.Abs(vector2.Y) > 32.0)
          {
            if ((double) Math.Abs(bottom.X - ((Entity) this.Projectile).Center.X) > 20.0)
              ((Entity) this.Projectile).velocity.X = ((Entity) this.Projectile).velocity.X + num10 * (float) Math.Sign(bottom.X - ((Entity) this.Projectile).Center.X);
            if ((double) Math.Abs(bottom.Y - ((Entity) this.Projectile).Center.Y) > 10.0)
              ((Entity) this.Projectile).velocity.Y = ((Entity) this.Projectile).velocity.Y + num10 * (float) Math.Sign(bottom.Y - ((Entity) this.Projectile).Center.Y);
          }
          else if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() > 2.0)
          {
            Projectile projectile = this.Projectile;
            ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.96f);
          }
          if ((double) Math.Abs(((Entity) this.Projectile).velocity.Y) < 1.0)
            ((Entity) this.Projectile).velocity.Y = ((Entity) this.Projectile).velocity.Y - 0.1f;
          float num11 = 15f;
          if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() > (double) num11)
            ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), num11);
        }
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
        int direction1 = ((Entity) this.Projectile).direction;
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
        int direction2 = ((Entity) this.Projectile).direction;
        if (direction1 != direction2)
          this.Projectile.netUpdate = true;
        float num12 = MathHelper.Clamp(this.Projectile.localAI[0], 0.0f, 50f);
        ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
        this.Projectile.scale = (float) (1.0 + (double) num12 * 0.0099999997764825821);
        ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = (int) ((double) num1 * (double) this.Projectile.scale);
        ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
        if (this.Projectile.alpha > 0)
        {
          this.Projectile.alpha -= 42;
          if (this.Projectile.alpha < 0)
            this.Projectile.alpha = 0;
        }
        Projectile projectile3 = this.Projectile;
        ((Entity) projectile3).position = Vector2.op_Subtraction(((Entity) projectile3).position, Vector2.op_Division(((Entity) this.Projectile).velocity, 2f));
      }

      static bool IsInTile(Vector2 pos)
      {
        Tile tileSafely = Framing.GetTileSafely(pos);
        if (!((Tile) ref tileSafely).HasUnactuatedTile)
          return false;
        return Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] || Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType];
      }
    }
  }
}
