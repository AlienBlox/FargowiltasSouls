// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Souls.FrozenBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Souls
{
  public class FrozenBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoSave[this.Type] = true;
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = false;
      Main.buffNoSave[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
    }

    public virtual string Texture => "FargowiltasSouls/Content/Buffs/PlaceholderDebuff";

    public virtual void Update(NPC npc, ref int buffIndex)
    {
      npc.FargoSouls().TimeFrozen = true;
      npc.FargoSouls().Chilled = true;
    }

    public virtual bool ReApply(NPC npc, int time, int buffIndex)
    {
      npc.buffTime[buffIndex] += time;
      return base.ReApply(npc, time, buffIndex);
    }
  }
}
