// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.EaterofWorldsSegment
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class EaterofWorldsSegment : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(14, 15);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.damage *= 2;
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (WorldSavingSystem.SwarmActive || !((IEnumerable<NPC>) Main.npc).Any<NPC>((Func<NPC, bool>) (n =>
      {
        if (!((Entity) n).active || ((Entity) n).whoAmI == ((Entity) npc).whoAmI)
          return false;
        return n.type == 14 || n.type == 13 || n.type == 15;
      })))
        return base.CheckDead(npc);
      ((Entity) npc).active = false;
      if (npc.DeathSound.HasValue)
      {
        SoundStyle soundStyle = npc.DeathSound.Value;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      }
      return false;
    }
  }
}
