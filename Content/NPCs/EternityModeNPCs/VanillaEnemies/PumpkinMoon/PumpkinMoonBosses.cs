// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PumpkinMoon.PumpkinMoonBosses
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PumpkinMoon
{
  public class PumpkinMoonBosses : EModeNPCBehaviour
  {
    public const int WAVELOCK = 15;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(325, 327);

    public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) => base.ModifyNPCLoot(npc, npcLoot);

    public virtual bool PreKill(NPC npc)
    {
      if (!Main.pumpkinMoon || NPC.waveNumber >= 15)
        return base.PreKill(npc);
      for (int index = 0; index < 10; ++index)
      {
        if (FargoSoulsUtil.HostCheck)
          Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, 58, 1, false, 0, false, false);
      }
      return false;
    }
  }
}
