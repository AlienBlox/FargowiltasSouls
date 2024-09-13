// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Challengers.RoseTintedVisor
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.Challengers
{
  public class RoseTintedVisor : SoulsItem
  {
    private int Charges;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 280;
      this.Item.mana = 22;
      this.Item.DamageType = DamageClass.Magic;
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.useTime = 45;
      this.Item.useAnimation = 45;
      this.Item.useStyle = 10;
      this.Item.knockBack = 6f;
      this.Item.value = Item.sellPrice(0, 5, 0, 0);
      this.Item.rare = 4;
      this.Item.shoot = ModContent.ProjectileType<RoseTintedVisorDeathray>();
      this.Item.shootSpeed = 3f;
      this.Item.channel = true;
      this.Item.noUseGraphic = true;
      this.Item.noMelee = true;
      this.Item.autoReuse = true;
    }

    public virtual void ModifyWeaponCrit(Player player, ref float crit)
    {
      crit += (float) this.Charges * 16.666666f;
    }

    public virtual void HoldItem(Player player)
    {
      if (this.Charges == 6)
        new SmallSparkle((double) player.gravDir > 0.0 ? Vector2.op_Addition(((Entity) player).Top, Vector2.op_Multiply(Vector2.UnitY, 7f)) : Vector2.op_Subtraction(((Entity) player).Bottom, Vector2.op_Multiply(Vector2.UnitY, 7f)), Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 6.2831854820251465), Utils.NextFloat(Main.rand, 2f, 5f)), Color.DeepPink, 1f, 10, Utils.NextFloat(Main.rand, 6.28318548f), Utils.NextFloat(Main.rand, -0.1308997f, 0.1308997f)).Spawn();
      if (!Main.mouseLeftRelease && !player.dead || this.Charges <= 0 || ((Entity) player).whoAmI != Main.myPlayer)
        return;
      for (int index = 0; index < this.Charges; ++index)
      {
        Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center)), 0.098174773156642914), this.Item.shootSpeed);
        Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), Vector2.op_Addition(((Entity) player).Top, Vector2.op_Multiply(Vector2.UnitY, 8f)), vector2, this.Item.shoot, (int) ((double) player.ActualClassDamage(DamageClass.Magic) * (double) this.Item.damage), this.Item.knockBack, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
        Player player1 = player;
        ((Entity) player1).velocity = Vector2.op_Subtraction(((Entity) player1).velocity, vector2);
      }
      if (this.Charges >= 6)
        SoundEngine.PlaySound(ref SoundID.Item68, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
      player.reuseDelay = 45;
      this.Charges = 0;
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
      if (this.Charges < 6)
      {
        SoundStyle soundStyle = SoundID.Item25;
        ((SoundStyle) ref soundStyle).Pitch = (float) ((double) this.Charges / 6.0 - 0.5);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        ++this.Charges;
      }
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient<BanishedBaronBag>(2).AddTile(220).DisableDecraft().Register();
    }
  }
}
