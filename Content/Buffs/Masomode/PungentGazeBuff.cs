// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.PungentGazeBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class PungentGazeBuff : ModBuff
  {
    public const int MAX_TIME = 300;

    public virtual string Texture => "FargowiltasSouls/Content/Buffs/PlaceholderDebuff";

    public virtual void SetStaticDefaults() => Main.debuff[this.Type] = true;

    public virtual void Update(NPC npc, ref int buffIndex)
    {
      FargoSoulsGlobalNPC fargoSoulsGlobalNpc = npc.FargoSouls();
      int num = fargoSoulsGlobalNpc.PungentGazeWasApplied ? 300 : 2;
      if (npc.buffTime[buffIndex] > num)
        npc.buffTime[buffIndex] = num;
      fargoSoulsGlobalNpc.PungentGazeTime = npc.buffTime[buffIndex];
      fargoSoulsGlobalNpc.PungentGazeWasApplied = false;
    }

    public virtual bool ReApply(NPC npc, int time, int buffIndex)
    {
      npc.buffTime[buffIndex] += time;
      npc.FargoSouls().PungentGazeWasApplied = true;
      return base.ReApply(npc, time, buffIndex);
    }
  }
}
