// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Expert.RustedOxygenTank
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Expert
{
  public class RustedOxygenTank : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.rare = -12;
      this.Item.value = Item.sellPrice(0, 4, 0, 0);
      this.Item.expert = true;
    }

    public static void PassiveEffect(Player player)
    {
      player.ignoreWater = true;
      if (Collision.WetCollision(((Entity) player).position, ((Entity) player).width, ((Entity) player).height))
      {
        player.moveSpeed += 1.25f;
        player.maxRunSpeed += 1.25f;
      }
      else
      {
        if (player.merman)
        {
          player.gravity = 0.3f;
          player.maxFallSpeed = 7f;
        }
        else if (player.trident)
        {
          player.gravity = 0.25f;
          player.maxFallSpeed = 6f;
          Player.jumpHeight = 25;
          Player.jumpSpeed = 5.51f;
          if (player.controlUp)
          {
            player.gravity = 0.1f;
            player.maxFallSpeed = 2f;
          }
        }
        else
        {
          player.gravity = 0.2f;
          player.maxFallSpeed = 5f;
          Player.jumpHeight = 30;
          Player.jumpSpeed = 6.01f;
        }
        if (((Entity) player).wet)
          return;
        ((Entity) player).wet = true;
        ((Entity) player).wetCount = (byte) 10;
      }
    }

    public virtual bool CanRightClick() => true;

    public virtual void RightClick(Player player)
    {
      player.ReplaceItem(this.Item, ModContent.ItemType<RustedOxygenTankInactive>());
    }

    public virtual void UpdateInventory(Player player) => player.FargoSouls().OxygenTank = true;
  }
}
