// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Daawnz.ComputationOrb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Daawnz
{
  public class ComputationOrb : PatreonModItem
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = 8;
      this.Item.value = 100000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.GetModPlayer<PatreonPlayer>().CompOrb = true;
    }
  }
}
