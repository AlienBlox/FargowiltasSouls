// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Jungle.GiantTortoise
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Jungle
{
  public class GiantTortoise : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(153);

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (!Utils.NextBool(Main.rand, 3) || !npc.FargoSouls().CanHordeSplit)
        return;
      EModeGlobalNPC.Horde(npc, Main.rand.Next(2, 6));
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      npc.reflectsProjectiles = (double) npc.ai[0] == 3.0 && npc.HasValidTarget && ((double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) < 160.0 || Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0));
    }

    public override void SafeOnHitByItem(
      NPC npc,
      Player player,
      Item item,
      NPC.HitInfo hit,
      int damageDone)
    {
      base.SafeOnHitByItem(npc, player, item, hit, damageDone);
      if (npc.type != 153)
        return;
      player.Hurt(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.FargowiltasSouls.DeathMessage.GiantTortoise", (object) player.name)), ((NPC.HitInfo) ref hit).Damage / 2, 0, false, false, -1, true, 0.0f, 0.0f, 4.5f);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300, true, false);
      target.FargoSouls().AddBuffNoStack(ModContent.BuffType<StunnedBuff>(), 60);
      ((Entity) target).velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) target).Center, ((Entity) npc).Center)), 30f);
    }
  }
}
