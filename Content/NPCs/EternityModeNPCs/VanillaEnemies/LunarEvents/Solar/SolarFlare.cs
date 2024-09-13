// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar.SolarFlare
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar
{
  public class SolarFlare : EModeNPCBehaviour
  {
    public bool IsCultistProjectile;
    public int Timer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(516);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.noTileCollide = true;
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.cultBoss, 439) || (double) ((Entity) npc).Distance(((Entity) Main.npc[EModeGlobalNPC.cultBoss]).Center) >= 3000.0)
        return;
      this.IsCultistProjectile = true;
      if (WorldSavingSystem.MasochistModeReal)
        return;
      npc.damage = (int) ((double) npc.damage * 0.6);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (!this.IsCultistProjectile || WorldSavingSystem.SwarmActive || WorldSavingSystem.MasochistModeReal)
        return;
      NPC npc1 = npc;
      ((Entity) npc1).position = Vector2.op_Addition(((Entity) npc1).position, Vector2.op_Multiply(((Entity) npc).velocity, Math.Min(0.5f, (float) ((double) ++this.Timer / 60.0 - 1.0))));
    }
  }
}
