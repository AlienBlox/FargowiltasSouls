// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.AccessorySourceProjectileHack
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class AccessorySourceProjectileHack : GlobalProjectile
  {
    public bool needsStatUpdate;

    public virtual bool InstancePerEntity => true;

    public virtual void OnSpawn(Projectile projectile, IEntitySource source)
    {
      if (!(source is EntitySource_ItemUse entitySourceItemUse) || entitySourceItemUse.Item == null || entitySourceItemUse.Item.type == Main.player[projectile.owner].HeldItem.type)
        return;
      this.needsStatUpdate = true;
    }

    public virtual bool PreAI(Projectile projectile)
    {
      if (this.needsStatUpdate)
      {
        this.needsStatUpdate = false;
        projectile.CritChance += (int) Main.player[projectile.owner].GetTotalCritChance(projectile.DamageType);
        projectile.ArmorPenetration += (int) Main.player[projectile.owner].GetTotalArmorPenetration(projectile.DamageType);
      }
      return base.PreAI(projectile);
    }
  }
}
