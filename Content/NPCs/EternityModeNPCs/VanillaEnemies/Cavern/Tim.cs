// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.Tim
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class Tim : Teleporters
  {
    public int SpawnTimer = 60;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(45);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lavaImmune = true;
      npc.lifeMax *= 2;
      npc.damage /= 2;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[24] = true;
    }

    public override void AI(NPC npc)
    {
      base.AI(npc);
      if (this.SpawnTimer > 0 && --this.SpawnTimer % 10 == 0 && FargoSoulsUtil.HostCheck)
        FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, 32, velocity: Utils.NextVector2Circular(Main.rand, 8f, 8f));
      EModeGlobalNPC.Aura(npc, 450f, 196, true, 15, new Color());
      EModeGlobalNPC.Aura(npc, 150f, 23, dustid: 20, color: new Color());
    }
  }
}
