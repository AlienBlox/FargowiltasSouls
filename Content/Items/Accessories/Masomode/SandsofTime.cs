// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.SandsofTime
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class SandsofTime : SoulsItem
  {
    public override bool Eternity => true;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = 1;
      this.Item.value = Item.sellPrice(0, 4, 0, 0);
      this.Item.useTime = 180;
      this.Item.useAnimation = 180;
      this.Item.useStyle = 4;
      this.Item.useTurn = true;
      this.Item.UseSound = new SoundStyle?(SoundID.Item6);
    }

    private static void PassiveEffect(Player player)
    {
      player.buffImmune[194] = true;
      player.FargoSouls().SandsofTime = true;
      player.FargoSouls().CactusImmune = true;
    }

    public virtual void UpdateInventory(Player player) => SandsofTime.PassiveEffect(player);

    public virtual void UpdateVanity(Player player) => SandsofTime.PassiveEffect(player);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      SandsofTime.PassiveEffect(player);
    }

    public static void Use(Player player)
    {
      if (player.itemTime != player.itemTimeMax / 2 || !Vector2.op_Inequality(player.lastDeathPostion, Vector2.Zero))
        return;
      for (int index1 = 0; index1 < 70; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 87, ((Entity) player).velocity.X * 0.5f, ((Entity) player).velocity.Y * 0.5f, 150, new Color(), 1.5f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
        Main.dust[index2].noGravity = true;
      }
      player.grappling[0] = -1;
      player.grapCount = 0;
      for (int index = 0; index < Main.maxProjectiles; ++index)
      {
        if (((Entity) Main.projectile[index]).active && Main.projectile[index].owner == ((Entity) player).whoAmI && Main.projectile[index].aiStyle == 7)
          Main.projectile[index].Kill();
      }
      if (((Entity) player).whoAmI == Main.myPlayer)
      {
        player.Teleport(player.lastDeathPostion, 1, 0);
        ((Entity) player).velocity = Vector2.Zero;
        if (Main.netMode == 1)
          NetMessage.SendData(65, -1, -1, (NetworkText) null, 0, (float) ((Entity) player).whoAmI, player.lastDeathPostion.X, player.lastDeathPostion.Y, 1, 0, 0);
      }
      for (int index3 = 0; index3 < 70; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 87, 0.0f, 0.0f, 150, new Color(), 1.5f);
        Dust dust = Main.dust[index4];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
        Main.dust[index4].noGravity = true;
      }
    }

    public virtual void UseItemFrame(Player player) => SandsofTime.Use(player);

    public virtual bool? UseItem(Player player) => new bool?(true);
  }
}
