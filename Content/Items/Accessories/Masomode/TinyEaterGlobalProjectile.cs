// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.TinyEaterGlobalProjectile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class TinyEaterGlobalProjectile : GlobalProjectile
  {
    private int HeartItemType = -1;

    public virtual bool InstancePerEntity => true;

    public virtual bool AppliesToEntity(Projectile entity, bool lateInstantiation)
    {
      return entity.type == 307;
    }

    private bool fromEnch => this.HeartItemType != -1;

    public virtual void OnSpawn(Projectile projectile, IEntitySource source)
    {
      if (!projectile.owner.IsWithinBounds((int) byte.MaxValue))
        return;
      Player player = Main.player[projectile.owner];
      Item darkenedHeartItem = player.FargoSouls().DarkenedHeartItem;
      if (player == null || darkenedHeartItem == null || !((Entity) player).active || !(source is EntitySource_ItemUse entitySourceItemUse) || entitySourceItemUse.Item.type != darkenedHeartItem.type)
        return;
      this.HeartItemType = darkenedHeartItem.type;
    }

    public virtual bool PreDraw(Projectile projectile, ref Color lightColor)
    {
      if (!this.fromEnch || this.HeartItemType == ModContent.ItemType<DarkenedHeart>())
        return base.PreDraw(projectile, ref lightColor);
      Texture2D texture = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/Misc/PureSeeker", (AssetRequestMode) 1).Value;
      FargoSoulsUtil.GenericProjectileDraw(projectile, lightColor, texture);
      return false;
    }

    public virtual void OnHitNPC(
      Projectile projectile,
      NPC target,
      NPC.HitInfo hit,
      int damageDone)
    {
      if (!this.fromEnch)
        return;
      if (this.HeartItemType != ModContent.ItemType<DarkenedHeart>())
        target.AddBuff(ModContent.BuffType<SublimationBuff>(), 120, false);
      else
        target.AddBuff(39, 120, false);
    }
  }
}
