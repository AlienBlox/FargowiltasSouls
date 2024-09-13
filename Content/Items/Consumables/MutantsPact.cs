// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Consumables.MutantsPact
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Items.Consumables
{
  public class MutantsPact : SoulsItem
  {
    public override bool Eternity => true;

    public virtual void SetStaticDefaults()
    {
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(4, 6, false));
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.rare = 11;
      this.Item.maxStack = 99;
      this.Item.useStyle = 4;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.consumable = true;
      this.Item.UseSound = new SoundStyle?(SoundID.Item123);
      this.Item.value = Item.sellPrice(0, 15, 0, 0);
    }

    public virtual bool CanUseItem(Player player) => !player.FargoSouls().MutantsPactSlot;

    public virtual bool? UseItem(Player player)
    {
      if (player.itemAnimation > 0 && player.itemTime == 0)
      {
        player.FargoSouls().MutantsPactSlot = true;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        if (!Main.dedServ)
        {
          SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Thunder", (SoundType) 0);
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        }
      }
      return new bool?(true);
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);
  }
}
