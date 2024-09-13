// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PirateInvasion.FlyingDutchman
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PirateInvasion
{
  public class FlyingDutchman : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(491);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.noTileCollide = true;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (!npc.HasValidTarget || (double) ((Entity) npc).velocity.Y >= 0.0 || (double) ((Entity) npc).position.Y + (double) ((Entity) npc).height >= (double) ((Entity) Main.player[npc.target]).position.Y)
        return;
      ((Entity) npc).velocity.Y = 0.0f;
    }
  }
}
