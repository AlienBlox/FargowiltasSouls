// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.OOA.DD2Ogre
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.OOA
{
  public class DD2Ogre : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(576, 577);

    public virtual void SetDefaults(NPC entity)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(entity);
      entity.scale *= 2f;
    }

    public virtual void OnSpawn(NPC npc, IEntitySource source)
    {
      base.OnSpawn(npc, source);
      FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 564, target: npc.target, velocity: new Vector2());
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      EModeGlobalNPC.Aura(npc, 500f, 120, dustid: 188, color: new Color());
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.FargoSouls().AddBuffNoStack(ModContent.BuffType<StunnedBuff>(), 60);
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300, true, false);
      target.AddBuff(36, 300, true, false);
    }
  }
}
