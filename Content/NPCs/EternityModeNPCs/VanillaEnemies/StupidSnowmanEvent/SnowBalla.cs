// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.StupidSnowmanEvent.SnowBalla
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.StupidSnowmanEvent
{
  public class SnowBalla : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(145);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if ((double) npc.ai[2] != 8.0)
        return;
      ((Entity) npc).velocity.X = 0.0f;
      ((Entity) npc).velocity.Y = 0.0f;
      float num1 = 10f;
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector(((Entity) npc).position.X + (float) ((Entity) npc).width * 0.5f - (float) (((Entity) npc).direction * 12), ((Entity) npc).position.Y + (float) ((Entity) npc).height * 0.25f);
      double num2 = (double) ((Entity) Main.player[npc.target]).position.X + (double) ((Entity) Main.player[npc.target]).width / 2.0 - (double) vector2.X;
      float num3 = ((Entity) Main.player[npc.target]).position.Y - vector2.Y;
      float num4 = (float) Math.Sqrt(num2 * num2 + (double) num3 * (double) num3);
      float num5 = num1 / num4;
      float num6 = (float) num2 * num5;
      float num7 = num3 * num5;
      if (!FargoSoulsUtil.HostCheck)
        return;
      int num8 = 35;
      int num9 = 109;
      int index = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2.X, vector2.Y, num6, num7, num9, num8, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      Main.projectile[index].ai[0] = 2f;
      Main.projectile[index].timeLeft = 300;
      Main.projectile[index].friendly = false;
      NetMessage.SendData(27, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      npc.netUpdate = true;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 300, true, false);
      target.AddBuff(44, 300, true, false);
    }
  }
}
