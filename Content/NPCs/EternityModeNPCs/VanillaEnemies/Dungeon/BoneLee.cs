// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon.BoneLee
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon
{
  public class BoneLee : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(287);

    public override void ModifyHitByAnything(
      NPC npc,
      Player player,
      ref NPC.HitModifiers modifiers)
    {
      base.ModifyHitByAnything(npc, player, ref modifiers);
      if (!Utils.NextBool(Main.rand, 10) || !npc.HasPlayerTarget || ((Entity) player).whoAmI != npc.target || !((Entity) player).active || player.dead || player.ghost)
        return;
      bool flag = true;
      Vector2 center = ((Entity) player).Center;
      float num = 100f * (float) -((Entity) player).direction;
      center.X += num;
      center.Y -= 50f;
      if (!Collision.CanHit(center, 1, 1, ((Entity) player).position, ((Entity) player).width, ((Entity) player).height))
      {
        center.X -= num * 2f;
        if (!Collision.CanHit(center, 1, 1, ((Entity) player).position, ((Entity) player).width, ((Entity) player).height))
          flag = false;
      }
      if (!flag)
        return;
      FargoSoulsUtil.GrossVanillaDodgeDust((Entity) npc);
      modifiers.Null();
      ((Entity) npc).Center = center;
      npc.netUpdate = true;
      FargoSoulsUtil.GrossVanillaDodgeDust((Entity) npc);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(163, 60, true, false);
      ((Entity) target).velocity.X = ((Vector2) ref ((Entity) npc).velocity).Length() * (float) ((Entity) npc).direction;
    }
  }
}
