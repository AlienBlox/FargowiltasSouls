// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.LaBonez.PiranhaPlantVoodooDoll
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.LaBonez
{
  public class PiranhaPlantVoodooDoll : PatreonModItem
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

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

    public virtual bool? UseItem(Player player)
    {
      PatreonPlayer modPlayer = player.GetModPlayer<PatreonPlayer>();
      modPlayer.PiranhaPlantMode = !modPlayer.PiranhaPlantMode;
      string stringAndClear;
      if (!modPlayer.PiranhaPlantMode)
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
  }
}
