// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.BloodMoon.WanderingEyeFish
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Night;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.BloodMoon
{
  public class WanderingEyeFish : DemonEyes
  {
    public int SickleTimer;
    public int SpawnTimer = 60;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(587);

    public override void AI(NPC npc)
    {
      base.AI(npc);
      if (this.SpawnTimer > 0 && --this.SpawnTimer % 5 == 0 && FargoSoulsUtil.HostCheck)
        FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 2, velocity: Utils.NextVector2Circular(Main.rand, 8f, 8f));
      if (npc.life >= npc.lifeMax / 2 || ++this.SickleTimer <= 15)
        return;
      this.SickleTimer = 0;
      if (!FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(1.5f, Vector2.Normalize(((Entity) npc).velocity)), ModContent.ProjectileType<BloodScythe>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 120, true, false);
      target.AddBuff(ModContent.BuffType<AnticoagulationBuff>(), 600, true, false);
    }
  }
}
