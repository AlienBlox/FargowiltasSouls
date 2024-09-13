// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.RainUmbrella
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class RainUmbrella : ModProjectile
  {
    private bool firstTick = true;
    private int reflectHP = 200;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 72;
      ((Entity) this.Projectile).height = 58;
      this.Projectile.timeLeft = 900;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
    }

    private int getReflectHP(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      int reflectHp = 200;
      if (fargoSoulsPlayer.ForceEffect<RainEnchant>())
        reflectHp = 400;
      return reflectHp;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer modPlayer = player.FargoSouls();
      if (((Entity) player).whoAmI == Main.myPlayer && (player.dead || !player.HasEffect<RainUmbrellaEffect>()))
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.timeLeft = 2;
        if (this.firstTick)
        {
          for (int index1 = 0; index1 < 12; ++index1)
          {
            Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.rotation, new Vector2()), 6f), (double) (index1 - 5) * 6.2831854820251465 / 12.0, new Vector2()), ((Entity) this.Projectile).Center);
            Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
            int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 33, 0.0f, 0.0f, 0, new Color(), 1.5f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity = vector2_2;
          }
          this.reflectHP = this.getReflectHP(player);
          this.firstTick = false;
        }
        ((Entity) this.Projectile).position.X = (float) (int) ((Entity) this.Projectile).position.X;
        ((Entity) this.Projectile).position.Y = (float) (int) ((Entity) this.Projectile).position.Y;
        this.Projectile.scale = (float) ((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.2f + 0.95f;
        ((Entity) this.Projectile).position.X = ((Entity) player).Center.X - (float) (((Entity) this.Projectile).width / 2);
        ((Entity) this.Projectile).position.Y = (float) ((double) ((Entity) player).Center.Y - (double) (((Entity) this.Projectile).height / 2) + (double) player.gfxOffY - 50.0);
        if (Main.raining)
        {
          Tile tileSafely = Framing.GetTileSafely(((Entity) player).Center);
          if (((Tile) ref tileSafely).WallType == (ushort) 0)
          {
            for (int index = 0; index < 20; ++index)
            {
              Vector2 vector2 = new Vector2();
              double num = Main.rand.NextDouble() * Math.PI + Math.PI / 2.0;
              vector2.X += (float) (Math.Sin(num) * 40.0);
              vector2.Y += (float) (Math.Cos(num) * 40.0 + (double) (((Entity) this.Projectile).height / 2) - 4.0);
              Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2), new Vector2(4f, 4f)), 0, 0, 33, 0.0f, 0.0f, 100, new Color(), 0.75f)];
            }
          }
        }
        ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (x =>
        {
          if (((Entity) x).active && x.hostile && x.damage > 0 && (double) Vector2.Distance(((Entity) x).Center, ((Entity) this.Projectile).Center) <= (double) (40 + Math.Min(((Entity) x).width, ((Entity) x).height) / 2))
          {
            bool? nullable = ProjectileLoader.CanDamage(x);
            bool flag = false;
            if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue) && ProjectileLoader.CanHitPlayer(x, player))
              return FargoSoulsUtil.CanDeleteProjectile(x);
          }
          return false;
        })).ToList<Projectile>().ForEach((Action<Projectile>) (x =>
        {
          if ((double) ((Entity) this.Projectile).Center.Y > (double) ((Entity) x).Center.Y && x.FargoSouls().canUmbrellaReflect)
          {
            for (int index3 = 0; index3 < 5; ++index3)
            {
              int index4 = Dust.NewDust(new Vector2(((Entity) x).position.X, ((Entity) x).position.Y + 2f), ((Entity) x).width, ((Entity) x).height + 5, 33, ((Entity) x).velocity.X * 0.2f, ((Entity) x).velocity.Y * 0.2f, 100, new Color(), 1f);
              Main.dust[index4].noGravity = true;
            }
            x.hostile = false;
            x.friendly = true;
            x.owner = ((Entity) player).whoAmI;
            Projectile projectile = x;
            ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, -1f);
            if ((double) ((Entity) x).Center.X > (double) ((Entity) player).Center.X)
            {
              ((Entity) x).direction = 1;
              x.spriteDirection = 1;
            }
            else
            {
              ((Entity) x).direction = -1;
              x.spriteDirection = -1;
            }
            x.netUpdate = true;
            this.reflectHP -= x.damage;
            x.damage *= 2;
            if (modPlayer.ForceEffect<RainEnchant>())
              x.damage *= 3;
            if (this.reflectHP > 0)
              return;
            this.Projectile.Kill();
          }
          else
            x.FargoSouls().canUmbrellaReflect = false;
        }));
      }
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual Color? GetAlpha(Color lightColor)
    {
      return this.reflectHP < this.getReflectHP(Main.player[this.Projectile.owner]) / 4 ? new Color?(new Color(150, 0, 0, 150)) : base.GetAlpha(lightColor);
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 12; ++index1)
      {
        Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.rotation, new Vector2()), 6f), (double) (index1 - 5) * 6.2831854820251465 / 12.0, new Vector2()), ((Entity) this.Projectile).Center);
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
        int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 33, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity = vector2_2;
      }
      Main.player[this.Projectile.owner].AddBuff(ModContent.BuffType<RainCDBuff>(), 900, true, false);
    }
  }
}
