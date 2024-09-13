// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Challengers.Lightslinger
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Projectiles.ChallengerItems;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.Challengers
{
  public class Lightslinger : SoulsItem
  {
    private const int ReqShots = 40;
    private int ShotType = ModContent.ProjectileType<LightslingerShot>();

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 22;
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 76;
      ((Entity) this.Item).height = 48;
      this.Item.useTime = 8;
      this.Item.useAnimation = 8;
      this.Item.useStyle = 5;
      this.Item.knockBack = 0.5f;
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
      this.Item.rare = 5;
      this.Item.UseSound = new SoundStyle?(SoundID.Item12);
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<LightslingerShot>();
      this.Item.shootSpeed = 12f;
      this.Item.useAmmo = AmmoID.Bullet;
      this.Item.noMelee = true;
    }

    public virtual bool CanConsumeAmmo(Item ammo, Player player) => !Utils.NextBool(Main.rand, 4);

    public virtual void ModifyShootStats(
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      type = this.ShotType;
      if (player.altFunctionUse != 2)
        return;
      damage *= 10;
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(-27f, -12f));

    public virtual bool AltFunctionUse(Player player)
    {
      return player.FargoSouls().LightslingerHitShots >= 40;
    }

    public virtual bool CanUseItem(Player player)
    {
      if (player.altFunctionUse == 2)
      {
        this.ShotType = ModContent.ProjectileType<LightslingerBomb>();
        this.Item.shootSpeed = 12f;
        this.Item.UseSound = new SoundStyle?(SoundID.Item91);
        this.Item.useAnimation = 30;
        this.Item.useTime = 30;
      }
      else
      {
        this.Item.UseSound = new SoundStyle?(SoundID.Item12);
        this.ShotType = ModContent.ProjectileType<LightslingerShot>();
        this.Item.shootSpeed = 16f;
        this.Item.useAnimation = 6;
        this.Item.useTime = 6;
      }
      return base.CanUseItem(player);
    }

    public virtual bool? UseItem(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.altFunctionUse == 2)
        fargoSoulsPlayer.LightslingerHitShots = 0;
      else if (++fargoSoulsPlayer.LightslingerHitShots >= 40 && ((Entity) player).whoAmI == Main.myPlayer)
      {
        if (fargoSoulsPlayer.ChargeSoundDelay <= 0)
        {
          fargoSoulsPlayer.ChargeSoundDelay = 120;
          SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ChargeSound", (SoundType) 0);
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        }
        Vector2 vector2_1 = Vector2.op_Multiply(Utils.ToRotationVector2(player.itemRotation), (float) ((Entity) player).direction);
        Vector2 vector2_2 = Vector2.op_Multiply(Utils.RotatedBy(vector2_1, 1.5707963705062866, new Vector2()), (float) ((Entity) player).direction);
        for (int index = 0; index < 7; ++index)
          Dust.NewDust(Vector2.op_Addition(Vector2.op_Addition(player.itemLocation, Vector2.op_Multiply(Vector2.op_Multiply(vector2_1, (float) ((Entity) this.Item).width), 0.5f)), Vector2.op_Multiply(Vector2.op_Multiply(vector2_2, (float) ((Entity) this.Item).height), 0.3f)), 15, 15, 242, vector2_1.X * Utils.NextFloat(Main.rand, 2f, 5f), vector2_1.Y * Utils.NextFloat(Main.rand, 2f, 5f), 0, new Color(), 1f);
      }
      return base.UseItem(player);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient<LifelightBag>(2).AddTile(220).DisableDecraft().Register();
    }
  }
}
