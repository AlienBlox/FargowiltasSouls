// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.MasochistReal
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Localization;

#nullable disable
namespace FargowiltasSouls.Content.Items
{
  public class MasochistReal : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 1;
      this.Item.rare = 1;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
      this.Item.consumable = false;
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool CanRightClick() => true;

    public virtual void RightClick(Player player)
    {
      string textValue = Language.GetTextValue("Mods.FargowiltasSouls.Items.MasochistReal.WorldState");
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
      interpolatedStringHandler.AppendFormatted<bool>(WorldSavingSystem.MasochistModeReal);
      string stringAndClear = interpolatedStringHandler.ToStringAndClear();
      Main.NewText(textValue + stringAndClear, byte.MaxValue, byte.MaxValue, byte.MaxValue);
    }

    public virtual bool? UseItem(Player player)
    {
      if (player.altFunctionUse == 2)
      {
        WorldSavingSystem.CanPlayMaso = !WorldSavingSystem.CanPlayMaso;
        Main.NewText(Language.GetTextValue("Mods.FargowiltasSouls.Items.MasochistReal.VarWorld") + WorldSavingSystem.CanPlayMaso.ToString(), byte.MaxValue, byte.MaxValue, byte.MaxValue);
      }
      else
      {
        player.FargoSouls().Toggler.CanPlayMaso = !player.FargoSouls().Toggler.CanPlayMaso;
        Main.NewText(Language.GetTextValue("Mods.FargowiltasSouls.Items.MasochistReal.VarMod") + player.FargoSouls().Toggler.CanPlayMaso.ToString(), byte.MaxValue, byte.MaxValue, byte.MaxValue);
      }
      return new bool?(true);
    }
  }
}
