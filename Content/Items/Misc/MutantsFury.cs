// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Misc.MutantsFury
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Misc
{
  public class MutantsFury : SoulsItem
  {
    public virtual string Texture => "FargowiltasSouls/Content/Items/Placeholder";

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.rare = 11;
      this.Item.maxStack = 999;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
      this.Item.consumable = false;
      this.Item.value = Item.buyPrice(1, 0, 0, 0);
    }

    public virtual bool? UseItem(Player player)
    {
      WorldSavingSystem.AngryMutant = !WorldSavingSystem.AngryMutant;
      string stringAndClear;
      if (!WorldSavingSystem.AngryMutant)
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 2);
        interpolatedStringHandler.AppendLiteral("Mods.");
        interpolatedStringHandler.AppendFormatted(((ModType) this).Mod.Name);
        interpolatedStringHandler.AppendLiteral(".Items.");
        interpolatedStringHandler.AppendFormatted(((ModType) this).Name);
        interpolatedStringHandler.AppendLiteral(".Off");
        stringAndClear = interpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 2);
        interpolatedStringHandler.AppendLiteral("Mods.");
        interpolatedStringHandler.AppendFormatted(((ModType) this).Mod.Name);
        interpolatedStringHandler.AppendLiteral(".Items.");
        interpolatedStringHandler.AppendFormatted(((ModType) this).Name);
        interpolatedStringHandler.AppendLiteral(".On");
        stringAndClear = interpolatedStringHandler.ToStringAndClear();
      }
      FargoSoulsUtil.PrintLocalization(stringAndClear, 175, 75, (int) byte.MaxValue);
      if (Main.netMode == 2)
        NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
      return new bool?(true);
    }

    public override void SafeModifyTooltips(List<TooltipLine> list)
    {
      foreach (TooltipLine tooltipLine in list)
      {
        if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
          tooltipLine.OverrideColor = new Color?(new Color(Main.DiscoR, 51, (int) byte.MaxValue - (int) ((double) Main.DiscoR * 0.4)));
      }
    }
  }
}
