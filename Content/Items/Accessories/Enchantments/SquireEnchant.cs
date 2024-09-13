// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.SquireEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.Common.Configs;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class SquireEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(148, 143, 140);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 6;
      this.Item.value = 150000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      SquireEnchant.SquireEffect(player, this.Item);
    }

    public static void SquireEffect(Player player, Item item)
    {
      player.AddEffect<SquireMountSpeed>(item);
      player.AddEffect<SquireMountJump>(item);
      FargoSoulsPlayer modPlayer = player.FargoSouls();
      modPlayer.SquireEnchantActive = true;
      player.buffImmune[205] = true;
      Mount mount1 = player.mount;
      if (!mount1.Active)
        return;
      if (modPlayer.BaseMountType != mount1.Type)
      {
        if (modPlayer.BaseMountType != -1)
          SquireEnchant.ResetMountStats(modPlayer);
        Mount.MountData mount2 = Mount.mounts[mount1.Type];
        modPlayer.BaseSquireMountData = new Mount.MountData()
        {
          acceleration = mount2.acceleration,
          dashSpeed = mount2.dashSpeed,
          fallDamage = mount2.fallDamage,
          jumpSpeed = mount2.jumpSpeed
        };
        modPlayer.BaseMountType = mount1.Type;
      }
      int num1;
      float num2;
      float num3;
      if (modPlayer.ValhallaEnchantActive && modPlayer.ForceEffect<ValhallaKnightEnchant>())
      {
        num1 = 20;
        num2 = 2f;
        num3 = 1.5f;
      }
      else if (modPlayer.ValhallaEnchantActive || modPlayer.ForceEffect<SquireEnchant>())
      {
        num1 = 15;
        num2 = 1.5f;
        num3 = 1.5f;
      }
      else
      {
        num1 = 10;
        num2 = 1.25f;
        num3 = 1.25f;
      }
      if (!player.HasEffect<SquireMountSpeed>())
      {
        num2 = 1f;
        num3 = 1f;
      }
      if (player.HasEffect<FargowiltasSouls.Content.Items.Accessories.Enchantments.ValhallaDash>())
      {
        if (modPlayer.ValhallaDashCD > 0)
          --modPlayer.ValhallaDashCD;
        if (modPlayer.ValhallaDashCD == 0 && Main.myPlayer == ((Entity) player).whoAmI)
        {
          if (Fargowiltas.Fargowiltas.DashKey.Current)
          {
            if (player.controlDown)
              SquireEnchant.ValhallaDash(player, true, 1);
            else if (player.controlUp)
              SquireEnchant.ValhallaDash(player, true, -1);
            if (player.controlRight)
              SquireEnchant.ValhallaDash(player, false, 1);
            else if (player.controlLeft)
              SquireEnchant.ValhallaDash(player, false, -1);
          }
          else if (!ModContent.GetInstance<FargoClientConfig>().DoubleTapDashDisabled)
          {
            if (player.controlDown && player.releaseDown)
            {
              if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15)
                SquireEnchant.ValhallaDash(player, true, 1);
            }
            else if (player.controlUp && player.releaseUp && player.doubleTapCardinalTimer[1] > 0 && player.doubleTapCardinalTimer[1] != 15)
              SquireEnchant.ValhallaDash(player, true, -1);
            if (player.controlRight && player.releaseRight)
            {
              if (player.doubleTapCardinalTimer[2] > 0 && player.doubleTapCardinalTimer[2] != 15)
                SquireEnchant.ValhallaDash(player, false, 1);
            }
            else if (player.controlLeft && player.releaseLeft && player.doubleTapCardinalTimer[3] > 0 && player.doubleTapCardinalTimer[3] != 15)
              SquireEnchant.ValhallaDash(player, false, -1);
          }
        }
      }
      if (player.HasEffect<SquireMountJump>())
        ((ExtraJumpState) ref player.GetJumpState<ExtraJump>(ExtraJump.FartInAJar)).Enable();
      Player player1 = player;
      player1.statDefense = Player.DefenseStat.op_Addition(player1.statDefense, num1);
      mount1._data.acceleration = modPlayer.BaseSquireMountData.acceleration * num2;
      mount1._data.dashSpeed = modPlayer.BaseSquireMountData.dashSpeed * num3;
      mount1._data.jumpSpeed = modPlayer.BaseSquireMountData.jumpSpeed * num3;
      mount1._data.fallDamage = 0.0f;
      player.noFallDmg = true;
    }

    public static void ValhallaDash(Player player, bool vertical, int direction)
    {
      if (!vertical)
      {
        player.FargoSouls().MonkDashing = 10;
        ((Entity) player).velocity.X = 50f * (float) direction;
      }
      else
      {
        float num = direction != 1 ? 40f : 80f;
        player.FargoSouls().MonkDashing = -10;
        ((Entity) player).velocity.Y = num * (float) direction;
      }
      player.FargoSouls().ValhallaDashCD = 30;
      player.dashDelay = 60;
      if (player.FargoSouls().IsDashingTimer < 10)
        player.FargoSouls().IsDashingTimer = 10;
      if (Main.netMode == 1)
        NetMessage.SendData(13, -1, -1, (NetworkText) null, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(new Vector2(((Entity) player).position.X, ((Entity) player).position.Y), ((Entity) player).width, ((Entity) player).height, 31, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index2].position.X += (float) Main.rand.Next(-5, 6);
        Main.dust[index2].position.Y += (float) Main.rand.Next(-5, 6);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.2f);
        Main.dust[index2].scale *= (float) (1.0 + (double) Main.rand.Next(20) * 0.0099999997764825821);
      }
      int index3 = Gore.NewGore(((Entity) player).GetSource_FromThis((string) null), new Vector2((float) ((double) ((Entity) player).position.X + (double) (((Entity) player).width / 2) - 24.0), (float) ((double) ((Entity) player).position.Y + (double) (((Entity) player).height / 2) - 34.0)), new Vector2(), Main.rand.Next(61, 64), 1f);
      Main.gore[index3].velocity.X = (float) Main.rand.Next(-50, 51) * 0.01f;
      Main.gore[index3].velocity.Y = (float) Main.rand.Next(-50, 51) * 0.01f;
      Gore gore1 = Main.gore[index3];
      gore1.velocity = Vector2.op_Multiply(gore1.velocity, 0.4f);
      int index4 = Gore.NewGore(((Entity) player).GetSource_FromThis((string) null), new Vector2((float) ((double) ((Entity) player).position.X + (double) (((Entity) player).width / 2) - 24.0), (float) ((double) ((Entity) player).position.Y + (double) (((Entity) player).height / 2) - 14.0)), new Vector2(), Main.rand.Next(61, 64), 1f);
      Main.gore[index4].velocity.X = (float) Main.rand.Next(-50, 51) * 0.01f;
      Main.gore[index4].velocity.Y = (float) Main.rand.Next(-50, 51) * 0.01f;
      Gore gore2 = Main.gore[index4];
      gore2.velocity = Vector2.op_Multiply(gore2.velocity, 0.4f);
    }

    public static void ResetMountStats(FargoSoulsPlayer modPlayer)
    {
      if (modPlayer.BaseSquireMountData == null || modPlayer.BaseMountType < 0 || modPlayer.BaseMountType >= Mount.mounts.Length)
        return;
      Mount.mounts[modPlayer.BaseMountType].acceleration = modPlayer.BaseSquireMountData.acceleration;
      Mount.mounts[modPlayer.BaseMountType].dashSpeed = modPlayer.BaseSquireMountData.dashSpeed;
      Mount.mounts[modPlayer.BaseMountType].fallDamage = modPlayer.BaseSquireMountData.fallDamage;
      Mount.mounts[modPlayer.BaseMountType].jumpSpeed = modPlayer.BaseSquireMountData.jumpSpeed;
      modPlayer.BaseMountType = -1;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3800, 1).AddIngredient(3801, 1).AddIngredient(3802, 1).AddIngredient(3825, 1).AddIngredient(4786, 1).AddIngredient(4788, 1).AddTile(125).Register();
    }
  }
}
