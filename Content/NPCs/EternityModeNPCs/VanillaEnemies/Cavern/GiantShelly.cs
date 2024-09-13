// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.GiantShelly
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class GiantShelly : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(496, 497);

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(32, 120, true, false);
    }

    public override void OnHitByAnything(NPC npc, Player player, NPC.HitInfo hit, int damageDone)
    {
      base.OnHitByAnything(npc, player, hit, damageDone);
      if ((double) npc.ai[0] != 3.0)
        return;
      Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center)), 4f);
      int index = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, 55, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 1f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      FargoSoulsGlobalProjectile.SplitProj(Main.projectile[index], 12, 0.2617994f, 1f);
    }
  }
}
