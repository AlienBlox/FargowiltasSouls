// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.OOA.DD2Skeleton
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
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.OOA
{
  public class DD2Skeleton : EModeNPCBehaviour
  {
    public int AttackTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(566, 567);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      this.AttackTimer = Main.rand.Next(180);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.AttackTimer <= 420)
        return;
      this.AttackTimer = 0;
      FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 30, velocity: new Vector2());
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<RottingBuff>(), 1200, true, false);
    }
  }
}
