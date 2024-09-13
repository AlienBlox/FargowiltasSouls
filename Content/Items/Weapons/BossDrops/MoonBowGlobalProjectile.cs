// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.MoonBowGlobalProjectile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.BossDrops
{
  public class MoonBowGlobalProjectile : GlobalProjectile
  {
    private bool isMoonBowArrow;
    private bool noGravArrow;
    private bool spawned;

    public virtual bool InstancePerEntity => true;

    public virtual void OnSpawn(Projectile projectile, IEntitySource source)
    {
      if (!projectile.arrow && projectile.type != 640)
        return;
      if (source is EntitySource_ItemUse entitySourceItemUse && entitySourceItemUse.Item.ModItem is MoonBow)
        this.isMoonBowArrow = true;
      if (!(source is EntitySource_Parent entitySourceParent) || !(entitySourceParent.Entity is Projectile) || !(entitySourceParent.Entity is Projectile entity) || !entity.GetGlobalProjectile<MoonBowGlobalProjectile>().isMoonBowArrow)
        return;
      this.isMoonBowArrow = true;
    }

    public virtual bool PreAI(Projectile projectile)
    {
      if (!this.spawned)
      {
        this.spawned = true;
        if ((projectile.arrow || projectile.type == 640) && Main.player[projectile.owner].HasBuff<MoonBowBuff>())
        {
          this.noGravArrow = true;
          ++projectile.extraUpdates;
        }
      }
      if (this.noGravArrow)
        ((Entity) projectile).velocity.Y -= 0.1f;
      return base.PreAI(projectile);
    }

    private void TryShootPortalArrow(Projectile projectile)
    {
      if (!this.isMoonBowArrow || projectile.owner != Main.myPlayer)
        return;
      this.isMoonBowArrow = false;
      for (int index = 0; index < 20; ++index)
      {
        Vector2 vector2 = Vector2.op_Addition(((Entity) Main.player[projectile.owner]).Center, Utils.NextVector2Circular(Main.rand, 192f, 192f));
        if (Collision.CanHitLine(((Entity) Main.player[projectile.owner]).Center, 0, 0, vector2, 0, 0))
        {
          Projectile.NewProjectile(Entity.InheritSource((Entity) projectile), vector2, Vector2.Zero, ModContent.ProjectileType<MoonBowPortal>(), projectile.damage, projectile.knockBack, projectile.owner, 0.0f, 0.0f, 0.0f);
          break;
        }
      }
    }

    public virtual void OnHitNPC(
      Projectile projectile,
      NPC target,
      NPC.HitInfo hit,
      int damageDone)
    {
      this.TryShootPortalArrow(projectile);
    }

    public virtual void OnKill(Projectile projectile, int timeLeft)
    {
      this.TryShootPortalArrow(projectile);
    }
  }
}
