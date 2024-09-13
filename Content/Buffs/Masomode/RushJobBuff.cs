// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.RushJobBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class RushJobBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      if (!Luminance.Common.Utilities.Utilities.AnyBosses() || player.buffTime[buffIndex] >= 10)
        return;
      player.buffTime[buffIndex] = 10;
    }
  }
}
