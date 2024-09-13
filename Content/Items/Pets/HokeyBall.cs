// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Pets.HokeyBall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Pets;
using FargowiltasSouls.Content.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Pets
{
  public class HokeyBall : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.CloneDefaults(1810);
      this.Item.shoot = ModContent.ProjectileType<Nibble>();
      this.Item.buffType = ModContent.BuffType<NibbleBuff>();
    }

    public virtual void UseStyle(Player player, Rectangle heldItemFrame)
    {
      base.UseStyle(player, heldItemFrame);
      if (((Entity) player).whoAmI != Main.myPlayer || player.itemTime != 0)
        return;
      player.AddBuff(this.Item.buffType, 3600, true, false);
    }
  }
}
