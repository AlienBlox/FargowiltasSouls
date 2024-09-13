// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.LihzahrdBlessingBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class LihzahrdBlessingBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.buffImmune[ModContent.BuffType<LihzahrdCurseBuff>()] = true;
      Tile tileSafely = Framing.GetTileSafely(((Entity) player).Center);
      if (((Tile) ref tileSafely).WallType != (ushort) 87)
        return;
      player.sunflower = true;
      player.ZonePeaceCandle = true;
      player.calmed = true;
    }
  }
}
