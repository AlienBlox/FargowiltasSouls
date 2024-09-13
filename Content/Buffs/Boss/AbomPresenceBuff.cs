// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Boss.AbomPresenceBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Boss
{
  public class AbomPresenceBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.buffNoSave[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.FargoSouls().noDodge = true;
      player.FargoSouls().noSupersonic = true;
      player.moonLeech = true;
      player.bleed = true;
      Player player1 = player;
      player1.statDefense = Player.DefenseStat.op_Subtraction(player1.statDefense, 30);
      player.endurance -= 0.25f;
    }
  }
}
