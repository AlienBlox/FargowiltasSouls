// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.FlippedHallowBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class FlippedHallowBuff : FlippedBuff
  {
    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.buffNoTimeDisplay[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
      base.Update(player, ref buffIndex);
      if ((double) ((Entity) player).Center.Y / 16.0 > Main.worldSurface || player.buffTime[buffIndex] <= 2)
        return;
      player.buffTime[buffIndex] = 2;
    }
  }
}
