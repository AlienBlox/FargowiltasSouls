// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.TribalCharm
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  [AutoloadEquip]
  public class TribalCharm : SoulsItem
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
      this.Item.rare = 5;
      this.Item.value = Item.sellPrice(0, 4, 0, 0);
      this.Item.defense = 6;
    }

    public virtual void UpdateInventory(Player player) => player.FargoSouls().TribalCharm = true;

    public virtual void UpdateVanity(Player player) => player.FargoSouls().TribalCharm = true;

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.buffImmune[149] = true;
      player.buffImmune[ModContent.BuffType<PurifiedBuff>()] = true;
      player.FargoSouls().TribalCharm = true;
      player.FargoSouls().TribalCharmEquipped = true;
      player.AddEffect<TribalCharmClickBonus>(this.Item);
    }

    public static void Effects(FargoSoulsPlayer modPlayer)
    {
      Player player = modPlayer.Player;
      if (player.controlUseItem || player.controlUseTile)
      {
        if (modPlayer.TribalCharmClickBonus)
        {
          modPlayer.TribalCharmClickBonus = false;
          ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
          local = StatModifier.op_Addition(local, 0.3f);
        }
      }
      else if (player.ItemTimeIsZero && player.HasEffect<TribalCharmClickBonus>())
        modPlayer.TribalCharmClickBonus = true;
      if (!modPlayer.TribalCharmClickBonus)
        return;
      player.AddBuff(ModContent.BuffType<TribalCharmClickBuff>(), 2, true, false);
      int index = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 157, ((Entity) player).velocity.X * 0.4f, ((Entity) player).velocity.Y * 0.4f, 0, new Color(), 2f);
      Main.dust[index].noGravity = true;
      Dust dust = Main.dust[index];
      dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
    }
  }
}
