// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.Dicer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.BossDrops
{
  public class Dicer : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
      ItemID.Sets.Yoyo[this.Item.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Item.useStyle = 5;
      ((Entity) this.Item).width = 24;
      ((Entity) this.Item).height = 24;
      this.Item.noUseGraphic = true;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.DamageType = DamageClass.Melee;
      this.Item.channel = true;
      this.Item.noMelee = true;
      this.Item.shoot = ModContent.ProjectileType<DicerYoyo>();
      this.Item.useAnimation = 25;
      this.Item.useTime = 25;
      this.Item.shootSpeed = 16f;
      this.Item.knockBack = 2.5f;
      this.Item.damage = 60;
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
      this.Item.rare = 8;
    }

    public virtual void HoldItem(Player player) => player.stringColor = 5;
  }
}
