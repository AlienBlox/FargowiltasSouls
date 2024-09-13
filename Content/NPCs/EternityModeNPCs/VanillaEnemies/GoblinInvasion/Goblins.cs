// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.GoblinInvasion.Goblins
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.GoblinInvasion
{
  public class Goblins : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(111, 26, 73, 29, 471, 27, 28);
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      if (npc.type != 28)
        return;
      npc.knockBackResist /= 10f;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (WorldSavingSystem.DownedAnyBoss || npc.type != 28 && npc.type != 27 && npc.type != 111 || NPC.CountNPCS(npc.type) <= 3)
        return;
      npc.Transform(26);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      if (Main.hardMode)
        target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300, true, false);
      if (npc.type != 27)
        return;
      target.AddBuff(ModContent.BuffType<MidasBuff>(), 600, true, false);
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      if (FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, new Vector2(Utils.NextFloat(Main.rand, -2f, 2f), -5f), ModContent.ProjectileType<GoblinSpikyBall>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      if (!NPC.downedGoblins || WorldSavingSystem.HaveForcedAbomFromGoblins)
        return;
      WorldSavingSystem.HaveForcedAbomFromGoblins = true;
      ModNPC modNpc;
      if (!ModContent.TryFind<ModNPC>("Fargowiltas", "Abominationn", ref modNpc) || NPC.AnyNPCs(modNpc.Type))
        return;
      int closest = (int) Player.FindClosest(((Entity) npc).Center, 0, 0);
      if (closest == -1)
        return;
      NPC.SpawnOnPlayer(closest, modNpc.Type);
    }
  }
}
