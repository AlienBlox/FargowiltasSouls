// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.BloodMoon.Drippler
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.BloodMoon
{
  public class Drippler : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(490);

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<RottingBuff>(), 600, true, false);
      target.AddBuff(ModContent.BuffType<AnticoagulationBuff>(), 600, true, false);
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      if (!Utils.NextBool(Main.rand, 3) || !FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < 4; ++index)
        FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 2, velocity: new Vector2(Utils.NextFloat(Main.rand, -3f, 3f), Utils.NextFloat(Main.rand, -3f, 3f)));
    }
  }
}
