// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Minions.MutantPowerBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Minions
{
  public class MutantPowerBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoTimeDisplay[this.Type] = true;
      Main.buffNoSave[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      int whoAmI = ((Entity) player).whoAmI;
      int player1 = Main.myPlayer;
    }
  }
}
