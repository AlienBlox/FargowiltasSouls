// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.SlimeKingsSlasher
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.BossDrops
{
  public class SlimeKingsSlasher : SoulsItem
  {
    private int numSpikes = 3;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 15;
      this.Item.DamageType = DamageClass.Melee;
      ((Entity) this.Item).width = 40;
      ((Entity) this.Item).height = 44;
      this.Item.useTime = 25;
      this.Item.useAnimation = 25;
      this.Item.useStyle = 1;
      this.Item.knockBack = 6f;
      this.Item.value = 10000;
      this.Item.rare = 2;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<SlimeSpikeFriendly>();
      this.Item.shootSpeed = 12f;
    }

    public virtual bool Shoot(
      Player player,
      EntitySource_ItemUse_WithAmmo source,
      Vector2 position,
      Vector2 velocity,
      int type,
      int damage,
      float knockback)
    {
      int index = Projectile.NewProjectile(player.GetSource_ItemUse(((EntitySource_ItemUse) source).Item, (string) null), ((Entity) player).Center, velocity, type, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      float maxSpread = 0.3926991f;
      if (this.numSpikes == 5)
        maxSpread = 0.628318548f;
      FargoSoulsGlobalProjectile.SplitProj(Main.projectile[index], this.numSpikes, maxSpread, 1f, true);
      this.numSpikes += 2;
      if (this.numSpikes > 5)
        this.numSpikes = 3;
      return false;
    }

    public virtual void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(137, 120, false);
    }
  }
}
