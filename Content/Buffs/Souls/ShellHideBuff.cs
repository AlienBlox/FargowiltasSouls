// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Souls.ShellHideBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Souls
{
  public class ShellHideBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.buffNoSave[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      FargoSoulsPlayer modPlayer = player.FargoSouls();
      player.noKnockback = true;
      player.endurance = 0.9f;
      player.thorns *= 10f;
      modPlayer.ShellHide = true;
      float distance = 48f;
      if (player.ownedProjectileCounts[ModContent.ProjectileType<TurtleShield>()] < 1)
        Projectile.NewProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<TurtleShield>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      if (modPlayer.TurtleCounter > 80)
        ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (x =>
        {
          if (((Entity) x).active && x.hostile && x.damage > 0 && (double) Vector2.Distance(((Entity) x).Center, ((Entity) player).Center) <= (double) distance)
          {
            bool? nullable = ProjectileLoader.CanDamage(x);
            bool flag = false;
            if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue) && ProjectileLoader.CanHitPlayer(x, player))
              return FargoSoulsUtil.CanDeleteProjectile(x);
          }
          return false;
        })).ToList<Projectile>().ForEach((Action<Projectile>) (x =>
        {
          int index1 = Dust.NewDust(new Vector2(((Entity) x).position.X, ((Entity) x).position.Y + 2f), ((Entity) x).width, ((Entity) x).height + 5, 228, ((Entity) x).velocity.X * 0.2f, ((Entity) x).velocity.Y * 0.2f, 100, new Color(), 2f);
          Main.dust[index1].noGravity = true;
          int index2 = Dust.NewDust(new Vector2(((Entity) x).position.X, ((Entity) x).position.Y + 2f), ((Entity) x).width, ((Entity) x).height + 5, 228, ((Entity) x).velocity.X * 0.2f, ((Entity) x).velocity.Y * 0.2f, 100, new Color(), 2f);
          Main.dust[index2].noGravity = true;
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
          x.hostile = false;
          x.friendly = true;
          --modPlayer.TurtleShellHP;
        }));
      if (modPlayer.TurtleShellHP > 0)
        return;
      player.AddBuff(ModContent.BuffType<BrokenShellBuff>(), 1800, true, false);
      modPlayer.TurtleShellHP = 19;
      for (int index3 = 0; index3 < 30; ++index3)
      {
        Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitY, 5f), (double) (index3 - 14) * 6.2831854820251465 / 30.0, new Vector2()), ((Entity) Main.LocalPlayer).Center);
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) Main.LocalPlayer).Center);
        int index4 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 273, 0.0f, 0.0f, 0, new Color(), 2f);
        Main.dust[index4].noGravity = true;
        Main.dust[index4].velocity = vector2_2;
      }
    }
  }
}
