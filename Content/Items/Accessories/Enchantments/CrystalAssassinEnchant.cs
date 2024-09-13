// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.CrystalAssassinEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class CrystalAssassinEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(36, 157, 207);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 5;
      this.Item.value = 150000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      CrystalAssassinEnchant.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.CrystalEnchantActive = true;
      if (fargoSoulsPlayer.SmokeBombCD != 0)
        --fargoSoulsPlayer.SmokeBombCD;
      if (fargoSoulsPlayer.HasDash || !player.AddEffect<CrystalAssassinDash>(item))
        return;
      player.dashType = 5;
    }

    public static void SmokeBombKey(FargoSoulsPlayer modPlayer)
    {
      if (modPlayer.CrystalSmokeBombProj == null)
      {
        float num = 60f;
        Vector2 vector2 = Vector2.op_Subtraction(Main.MouseWorld, ((Entity) modPlayer.Player).Center);
        vector2.X /= num;
        vector2.Y = (float) ((double) vector2.Y / (double) num - 0.090000003576278687 * (double) num);
        modPlayer.CrystalSmokeBombProj = Main.projectile[Projectile.NewProjectile(((Entity) modPlayer.Player).GetSource_Misc(""), ((Entity) modPlayer.Player).Center, Vector2.op_Addition(vector2, Vector2.op_Multiply(Utils.NextVector2Square(Main.rand, 0.0f, 0.0f), 2f)), 196, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f)];
        modPlayer.SmokeBombCD = 15;
      }
      else
      {
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector(((Entity) modPlayer.CrystalSmokeBombProj).position.X, ((Entity) modPlayer.CrystalSmokeBombProj).position.Y - 30f);
        Vector2 vector2_2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_2).\u002Ector(vector2_1.X, vector2_1.Y);
        int num1 = 0;
        int num2 = 10;
        while (Collision.SolidCollision(vector2_1, ((Entity) modPlayer.Player).width, ((Entity) modPlayer.Player).height))
        {
          vector2_1 = vector2_2;
          switch (num1)
          {
            case 0:
              vector2_1.X -= (float) num2;
              break;
            case 1:
              vector2_1.X += (float) num2;
              break;
            case 2:
              vector2_1.Y += (float) num2;
              break;
            default:
              vector2_1.Y -= (float) num2;
              num2 += 10;
              break;
          }
          ++num1;
          if (num1 >= 4)
            num1 = 0;
          if (num2 > 100)
            return;
        }
        if ((double) vector2_1.X <= 50.0 || (double) vector2_1.X >= (double) (Main.maxTilesX * 16 - 50) || (double) vector2_1.Y <= 50.0 || (double) vector2_1.Y >= (double) (Main.maxTilesY * 16 - 50))
          return;
        modPlayer.Player.Teleport(vector2_1, 1, 0);
        NetMessage.SendData(65, -1, -1, (NetworkText) null, 0, (float) ((Entity) modPlayer.Player).whoAmI, vector2_1.X, vector2_1.Y, 1, 0, 0);
        modPlayer.Player.AddBuff(ModContent.BuffType<FirstStrikeBuff>(), 60, true, false);
        modPlayer.CrystalSmokeBombProj.timeLeft = 120;
        modPlayer.SmokeBombCD = 300;
        modPlayer.CrystalSmokeBombProj = (Projectile) null;
      }
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(4982, 1).AddIngredient(4983, 1).AddIngredient(4984, 1).AddIngredient(3030, 1).AddIngredient(856, 1).AddIngredient(1168, 50).AddTile(125).Register();
    }
  }
}
