// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.LeashOfCthulhu
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
  public class LeashOfCthulhu : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 15;
      ((Entity) this.Item).width = 30;
      ((Entity) this.Item).height = 10;
      this.Item.value = Item.sellPrice(0, 1, 0, 0);
      this.Item.rare = 1;
      this.Item.noMelee = true;
      this.Item.useStyle = 5;
      this.Item.useAnimation = 25;
      this.Item.useTime = 25;
      this.Item.knockBack = 4f;
      this.Item.noUseGraphic = true;
      this.Item.shoot = ModContent.ProjectileType<LeashFlail>();
      this.Item.shootSpeed = 25f;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.DamageType = DamageClass.Melee;
      this.Item.autoReuse = true;
    }
  }
}
