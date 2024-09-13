// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.HoverShooter
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public abstract class HoverShooter : Minion
  {
    protected float ChaseAccel = 6f;
    protected float ChaseDist = 200f;
    protected float IdleAccel = 0.05f;
    protected float Inertia = 40f;
    protected int Shoot;
    protected float ShootCool = 90f;
    protected float ShootSpeed;
    protected float SpacingMult = 1f;
    protected float ViewDist = 400f;

    public virtual void CreateDust()
    {
    }

    public virtual void SelectFrame()
    {
    }

    public override void Behavior()
    {
      Player player = Main.player[this.Projectile.owner];
      float num1 = (float) ((Entity) this.Projectile).width * this.SpacingMult;
      for (int index = 0; index < Main.maxProjectiles; ++index)
      {
        Projectile projectile = Main.projectile[index];
        if (index != ((Entity) this.Projectile).whoAmI && ((Entity) projectile).active && projectile.owner == this.Projectile.owner && projectile.type == this.Projectile.type && (double) Math.Abs(((Entity) this.Projectile).position.X - ((Entity) projectile).position.X) + (double) Math.Abs(((Entity) this.Projectile).position.Y - ((Entity) projectile).position.Y) < (double) num1)
        {
          if ((double) ((Entity) this.Projectile).position.X < (double) ((Entity) Main.projectile[index]).position.X)
            ((Entity) this.Projectile).velocity.X -= this.IdleAccel;
          else
            ((Entity) this.Projectile).velocity.X += this.IdleAccel;
          if ((double) ((Entity) this.Projectile).position.Y < (double) ((Entity) Main.projectile[index]).position.Y)
            ((Entity) this.Projectile).velocity.Y -= this.IdleAccel;
          else
            ((Entity) this.Projectile).velocity.Y += this.IdleAccel;
        }
      }
      Vector2 vector2_1 = ((Entity) this.Projectile).position;
      float viewDist = this.ViewDist;
      bool flag = false;
      this.Projectile.tileCollide = true;
      float num2;
      if (player.HasMinionAttackTargetNPC)
      {
        NPC npc = Main.npc[player.MinionAttackTargetNPC];
        if (Collision.CanHitLine(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, ((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height))
        {
          num2 = Vector2.Distance(((Entity) this.Projectile).Center, vector2_1);
          vector2_1 = ((Entity) npc).Center;
          flag = true;
        }
      }
      else
      {
        int prioritizingMinionFocus = FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, viewDist, true, new Vector2());
        if (prioritizingMinionFocus != -1)
        {
          num2 = ((Entity) this.Projectile).Distance(((Entity) Main.npc[prioritizingMinionFocus]).Center);
          vector2_1 = ((Entity) Main.npc[prioritizingMinionFocus]).Center;
          flag = true;
        }
      }
      if ((double) Vector2.Distance(((Entity) player).Center, ((Entity) this.Projectile).Center) > (flag ? 1000.0 : 500.0))
      {
        this.Projectile.ai[0] = 1f;
        this.Projectile.netUpdate = true;
      }
      if ((double) this.Projectile.ai[0] == 1.0)
        this.Projectile.tileCollide = false;
      if (flag && (double) this.Projectile.ai[0] == 0.0)
      {
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
        if ((double) ((Vector2) ref vector2_2).Length() > (double) this.ChaseDist)
        {
          ((Vector2) ref vector2_2).Normalize();
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Inertia), Vector2.op_Multiply(vector2_2, this.ChaseAccel)), this.Inertia + 1f);
        }
        else
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, (float) Math.Pow(0.97, 40.0 / (double) this.Inertia));
        }
      }
      else
      {
        if (!Collision.CanHitLine(((Entity) this.Projectile).Center, 1, 1, ((Entity) player).Center, 1, 1))
          this.Projectile.ai[0] = 1f;
        float num3 = 6f;
        if ((double) this.Projectile.ai[0] == 1.0)
          num3 = 15f;
        Vector2 center = ((Entity) this.Projectile).Center;
        Vector2 vector2_3 = Vector2.op_Subtraction(((Entity) player).Center, center);
        this.Projectile.ai[1] = 3600f;
        this.Projectile.netUpdate = true;
        int num4 = 1;
        for (int index = 0; index < ((Entity) this.Projectile).whoAmI; ++index)
        {
          if (((Entity) Main.projectile[index]).active && Main.projectile[index].owner == this.Projectile.owner && Main.projectile[index].type == this.Projectile.type)
            ++num4;
        }
        vector2_3.X -= (float) ((10 + num4 * 40) * ((Entity) player).direction);
        vector2_3.Y -= 70f;
        double num5 = (double) ((Vector2) ref vector2_3).Length();
        if (num5 > 200.0 && (double) num3 < 9.0)
          num3 = 9f;
        if (num5 < 100.0 && (double) this.Projectile.ai[0] == 1.0 && !Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
        {
          this.Projectile.ai[0] = 0.0f;
          this.Projectile.netUpdate = true;
        }
        if (num5 > 2000.0)
          ((Entity) this.Projectile).Center = ((Entity) player).Center;
        if (num5 > 48.0)
        {
          ((Vector2) ref vector2_3).Normalize();
          vector2_3 = Vector2.op_Multiply(vector2_3, num3);
          float num6 = this.Inertia / 2f;
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, num6), vector2_3), num6 + 1f);
        }
        else
        {
          ((Entity) this.Projectile).direction = ((Entity) Main.player[this.Projectile.owner]).direction;
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, (float) Math.Pow(0.9, 40.0 / (double) this.Inertia));
        }
      }
      this.Projectile.rotation = ((Entity) this.Projectile).velocity.X * 0.05f;
      this.SelectFrame();
      this.CreateDust();
      if ((double) ((Entity) this.Projectile).velocity.X > 0.0)
        this.Projectile.spriteDirection = ((Entity) this.Projectile).direction = -1;
      else if ((double) ((Entity) this.Projectile).velocity.X < 0.0)
        this.Projectile.spriteDirection = ((Entity) this.Projectile).direction = 1;
      if ((double) this.Projectile.ai[1] > 0.0)
      {
        ++this.Projectile.ai[1];
        if (Utils.NextBool(Main.rand, 3))
          ++this.Projectile.ai[1];
      }
      if ((double) this.Projectile.ai[1] > (double) this.ShootCool)
      {
        this.Projectile.ai[1] = 0.0f;
        this.Projectile.netUpdate = true;
      }
      if ((double) this.Projectile.ai[0] != 0.0 || !flag)
        return;
      if ((double) Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center).X > 0.0)
        this.Projectile.spriteDirection = ((Entity) this.Projectile).direction = -1;
      else if ((double) Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center).X < 0.0)
        this.Projectile.spriteDirection = ((Entity) this.Projectile).direction = 1;
      if ((double) this.Projectile.ai[1] != 0.0)
        return;
      this.Projectile.ai[1] = 1f;
      if (Main.myPlayer != this.Projectile.owner)
        return;
      Vector2 vector2_4 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
      if (Vector2.op_Equality(vector2_4, Vector2.Zero))
      {
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_4).\u002Ector(0.0f, 1f);
      }
      ((Vector2) ref vector2_4).Normalize();
      vector2_4 = Vector2.op_Multiply(vector2_4, this.ShootSpeed);
      int index1 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2_4, this.Shoot, this.Projectile.damage, this.Projectile.knockBack, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      Main.projectile[index1].timeLeft = 300;
      Main.projectile[index1].netUpdate = true;
      this.Projectile.netUpdate = true;
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = true;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }
  }
}
