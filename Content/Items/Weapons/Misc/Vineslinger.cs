// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Misc.Vineslinger
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.JungleMimic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.Misc
{
  public class Vineslinger : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 30;
      ((Entity) this.Item).height = 10;
      this.Item.value = Item.sellPrice(0, 8, 0, 0);
      this.Item.rare = 4;
      this.Item.noMelee = true;
      this.Item.useStyle = 5;
      this.Item.useAnimation = 40;
      this.Item.useTime = 40;
      this.Item.knockBack = 5.5f;
      this.Item.damage = 58;
      this.Item.scale = 1.1f;
      this.Item.noUseGraphic = true;
      this.Item.shoot = ModContent.ProjectileType<VineslingerBall>();
      this.Item.shootSpeed = 38f;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.DamageType = DamageClass.Melee;
      this.Item.channel = true;
    }
  }
}
