// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.Dash
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class Dash : ModProjectile
  {
    public float baseSpeed;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 42;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.ignoreWater = true;
      this.Projectile.penetrate = -1;
      this.Projectile.hide = true;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.extraUpdates = 5;
      this.Projectile.timeLeft = 15 * (this.Projectile.extraUpdates + 1);
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      if (this.Projectile.owner != Main.myPlayer)
        return;
      Main.player[this.Projectile.owner].RemoveAllGrapplingHooks();
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (player.dead || !((Entity) player).active)
        this.Projectile.timeLeft = 0;
      else if ((double) ((Entity) this.Projectile).Center.X < 50.0 || (double) ((Entity) this.Projectile).Center.X > (double) (Main.maxTilesX * 16 - 50) || (double) ((Entity) this.Projectile).Center.Y < 50.0 || (double) ((Entity) this.Projectile).Center.Y > (double) (Main.maxTilesY * 16 - 50))
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.FargoSouls().TimeFreezeImmune = player.HasEffect<StardustEffect>();
        if (player.mount.Active)
          player.mount.Dismount(player);
        ((Entity) player).Center = ((Entity) this.Projectile).Center;
        ((Entity) player).velocity = Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.5f);
        if ((double) ((Entity) this.Projectile).velocity.X != 0.0)
          player.ChangeDir((double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1);
        player.itemRotation = (float) Math.Atan2((double) ((Entity) this.Projectile).velocity.Y * (double) ((Entity) this.Projectile).direction, (double) ((Entity) this.Projectile).velocity.X * (double) ((Entity) this.Projectile).direction);
        player.controlLeft = false;
        player.controlRight = false;
        player.controlJump = false;
        player.controlDown = false;
        player.controlUseTile = false;
        player.controlHook = false;
        player.controlMount = false;
        player.itemTime = 2;
        player.itemAnimation = 2;
        player.FargoSouls().IsDashingTimer = 0;
        if ((double) this.baseSpeed == 0.0 && Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero))
        {
          this.baseSpeed = ((Vector2) ref ((Entity) this.Projectile).velocity).Length();
          if (this.Projectile.owner == Main.myPlayer)
          {
            foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.owner == this.Projectile.owner && p.aiStyle == 7)))
              projectile.Kill();
          }
        }
        if (((double) this.Projectile.ai[1] != 2.0 || (double) this.Projectile.localAI[1] > 0.0) && (double) this.Projectile.localAI[0] == 0.0)
        {
          player.immune = true;
          player.immuneTime = Math.Max(player.immuneTime, 2);
          player.hurtCooldowns[0] = Math.Max(player.hurtCooldowns[0], 2);
          player.hurtCooldowns[1] = Math.Max(player.hurtCooldowns[1], 2);
          player.immuneNoBlink = true;
        }
        player.fallStart = (int) ((double) ((Entity) player).position.Y / 16.0);
        player.fallStart2 = player.fallStart;
        if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < (double) this.baseSpeed * 0.89999997615814209)
          this.Projectile.localAI[0] = 1f;
        if (this.Projectile.owner != Main.myPlayer || this.Projectile.timeLeft % this.Projectile.MaxUpdates != 0)
          return;
        if ((double) this.Projectile.localAI[0] == 0.0)
        {
          if ((double) this.Projectile.localAI[1] == 1.0 && (double) this.Projectile.ai[1] == 1.0)
          {
            Vector2 rotationVector2 = Utils.ToRotationVector2(this.Projectile.ai[0]);
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(rotationVector2, 1500f)), rotationVector2, ModContent.ProjectileType<HentaiSpearDeathray2>(), this.Projectile.damage, this.Projectile.knockBack, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(rotationVector2, 1500f)), Vector2.op_UnaryNegation(rotationVector2), ModContent.ProjectileType<HentaiSpearDeathray2>(), this.Projectile.damage, this.Projectile.knockBack, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
          }
          if ((double) this.Projectile.localAI[1] > 0.0)
          {
            if ((double) this.Projectile.ai[1] == 0.0)
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) player).Center.X, ((Entity) player).Center.Y, 0.0f, 0.0f, ModContent.ProjectileType<PhantasmalSphere>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
            else if ((double) this.Projectile.ai[1] == 1.0)
            {
              Vector2 vector2 = Utils.RotatedBy(Utils.ToRotationVector2(this.Projectile.ai[0]), Math.PI / 2.0, new Vector2());
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) player).Center, Vector2.op_Multiply(16f, vector2), ModContent.ProjectileType<PhantasmalSphere>(), this.Projectile.damage, this.Projectile.knockBack / 2f, this.Projectile.owner, 1f, 0.0f, 0.0f);
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) player).Center, Vector2.op_Multiply(16f, Vector2.op_UnaryNegation(vector2)), ModContent.ProjectileType<PhantasmalSphere>(), this.Projectile.damage, this.Projectile.knockBack / 2f, this.Projectile.owner, 1f, 0.0f, 0.0f);
            }
          }
        }
        ++this.Projectile.localAI[1];
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      Player player = Main.player[this.Projectile.owner];
      player.itemAnimation = 0;
      player.itemTime = 0;
      if (this.Projectile.owner != Main.myPlayer || (double) this.Projectile.ai[1] != 2.0 || (double) this.Projectile.localAI[1] <= 2.0 || timeLeft <= 0)
        return;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<HentaiNuke>(), this.Projectile.damage, this.Projectile.knockBack * 10f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity) => (double) this.Projectile.ai[1] == 2.0;
  }
}
