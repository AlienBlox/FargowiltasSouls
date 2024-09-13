// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Crimson.IchorSticker
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Crimson
{
  public class IchorSticker : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(268);

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(69, 600, true, false);
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      if (!FargoSoulsUtil.HostCheck)
        return;
      FargoSoulsUtil.XWay(5, ((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, 288, 4f, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 2f);
    }
  }
}
