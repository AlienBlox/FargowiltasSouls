﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.AnglerFish
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class AnglerFish : EModeNPCBehaviour
  {
    public bool WasHit;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(102);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.Opacity /= 5f;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (this.WasHit)
        return;
      Lighting.AddLight(((Entity) npc).Center, 0.1f, 0.5f, 0.5f);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(30, 300, true, false);
    }

    public override void OnHitByAnything(NPC npc, Player player, NPC.HitInfo hit, int damageDone)
    {
      base.OnHitByAnything(npc, player, hit, damageDone);
      if (this.WasHit)
        return;
      this.WasHit = true;
      npc.Opacity *= 5f;
    }
  }
}