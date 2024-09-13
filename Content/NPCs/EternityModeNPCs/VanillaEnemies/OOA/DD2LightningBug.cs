// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.OOA.DD2LightningBug
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.OOA
{
  public class DD2LightningBug : Shooters
  {
    public int Counter;

    public DD2LightningBug()
      : base(240, ModContent.ProjectileType<LightningVortexHostile>(), 0.5f, dustType: 229)
    {
    }

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(578);

    public override void AI(NPC npc)
    {
      base.AI(npc);
      EModeGlobalNPC.Aura(npc, 400f, ModContent.BuffType<LightningRodBuff>(), dustid: 229, color: new Color());
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(144, 300, true, false);
      target.FargoSouls().AddBuffNoStack(149, 60);
    }
  }
}
