// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.DragonBreath
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.BossDrops
{
  public class DragonBreath : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 55;
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 24;
      ((Entity) this.Item).height = 24;
      this.Item.useTime = 45;
      this.Item.useAnimation = 45;
      this.Item.channel = true;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 1.5f;
      this.Item.UseSound = new SoundStyle?(SoundID.DD2_BetsyFlameBreath);
      this.Item.useAmmo = AmmoID.Gel;
      this.Item.value = 50000;
      this.Item.rare = 8;
      this.Item.autoReuse = false;
      this.Item.shoot = ModContent.ProjectileType<DragonBreathProj>();
      this.Item.shootSpeed = 35f;
      this.Item.noUseGraphic = false;
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(-20f, -6f));

    public virtual bool CanUseItem(Player player)
    {
      return player.ownedProjectileCounts[this.Item.shoot] <= 0;
    }

    public virtual bool CanConsumeAmmo(Item ammo, Player player) => !Utils.NextBool(Main.rand, 3);
  }
}
