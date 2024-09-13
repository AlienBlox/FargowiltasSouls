// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.OOA.DD2KoboldFlyer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.OOA
{
  public class DD2KoboldFlyer : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(574, 575);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.Counter <= 60)
        return;
      this.Counter = 0;
      if (!FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, new Vector2(Utils.NextFloat(Main.rand, -2f, 2f), -5f), ModContent.ProjectileType<GoblinSpikyBall>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<FusedBuff>(), 1800, true, false);
    }
  }
}
