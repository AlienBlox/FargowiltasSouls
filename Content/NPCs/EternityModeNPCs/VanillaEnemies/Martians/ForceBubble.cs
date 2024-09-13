// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Martians.ForceBubble
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Martians
{
  public class ForceBubble : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(384);

    public override void OnHitByAnything(NPC npc, Player player, NPC.HitInfo hit, int damageDone)
    {
      base.OnHitByAnything(npc, player, hit, damageDone);
      if (!FargoSoulsUtil.HostCheck)
        return;
      int num = Main.expertMode ? 28 : 35;
      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(10f, Utils.RotatedByRandom(Vector2.UnitX, 6.2831854820251465)), 435, num, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      if (!Utils.NextBool(Main.rand, 3))
        return;
      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(10f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) player).Center)), 435, num, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }
  }
}
