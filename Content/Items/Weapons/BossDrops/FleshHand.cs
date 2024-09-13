// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.FleshHand
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
  public class FleshHand : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 38;
      this.Item.DamageType = DamageClass.Magic;
      this.Item.mana = 20;
      ((Entity) this.Item).width = 50;
      ((Entity) this.Item).height = 50;
      this.Item.useTime = 32;
      this.Item.useAnimation = 32;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 2f;
      this.Item.UseSound = new SoundStyle?(SoundID.NPCDeath13);
      this.Item.value = 50000;
      this.Item.rare = 5;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<Hungry>();
      this.Item.shootSpeed = 20f;
      this.Item.noUseGraphic = true;
    }
  }
}
