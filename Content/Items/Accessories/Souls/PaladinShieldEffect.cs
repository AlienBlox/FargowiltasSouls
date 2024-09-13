// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.PaladinShieldEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public class PaladinShieldEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<ColossusHeader>();

    public override int ToggleItemType => 938;

    public override bool IgnoresMutantPresence => true;

    public override void PostUpdateEquips(Player player)
    {
      if ((double) player.statLife <= (double) player.statLifeMax2 * 0.25)
        return;
      player.hasPaladinShield = true;
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        Player player1 = Main.player[index];
        if (((Entity) player1).active && player != player1 && (double) Vector2.Distance(((Entity) player1).Center, ((Entity) player).Center) < 400.0)
          player1.AddBuff(43, 30, true, false);
      }
    }
  }
}
