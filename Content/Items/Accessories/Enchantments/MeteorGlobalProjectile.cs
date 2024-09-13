// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.MeteorGlobalProjectile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Forces;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class MeteorGlobalProjectile : GlobalProjectile
  {
    private bool fromEnch;

    public virtual bool InstancePerEntity => true;

    public virtual bool AppliesToEntity(Projectile entity, bool lateInstantiation)
    {
      return entity.type == 424 || entity.type == 425 || entity.type == 426;
    }

    public virtual void OnSpawn(Projectile projectile, IEntitySource source)
    {
      if (!(source is EntitySource_ItemUse entitySourceItemUse) || entitySourceItemUse.Item.type != ModContent.ItemType<MeteorEnchant>() && entitySourceItemUse.Item.type != ModContent.ItemType<CosmoForce>())
        return;
      this.fromEnch = true;
      projectile.FargoSouls().CanSplit = false;
    }

    public virtual void OnHitNPC(
      Projectile projectile,
      NPC target,
      NPC.HitInfo hit,
      int damageDone)
    {
      if (!this.fromEnch)
        return;
      Main.player[projectile.owner].FargoSouls().MeteorTimer -= 6;
    }
  }
}
