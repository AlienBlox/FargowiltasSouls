// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon.TacticalSkeleton
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon
{
  public class TacticalSkeleton : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(292);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if ((double) npc.ai[2] <= 0.0 || (double) npc.ai[1] > 65.0)
        return;
      if (FargoSoulsUtil.HostCheck)
      {
        for (int index = 0; index < 6; ++index)
        {
          float num1 = ((Entity) Main.player[npc.target]).Center.X - ((Entity) npc).Center.X;
          float num2 = ((Entity) Main.player[npc.target]).Center.Y - ((Entity) npc).Center.Y;
          float num3 = 11f / (float) Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2);
          float num4 = num1 + (float) Main.rand.Next(-40, 41);
          double num5 = (double) num2 + (double) Main.rand.Next(-40, 41);
          float num6 = num4 * num3;
          double num7 = (double) num3;
          float num8 = (float) (num5 * num7);
          int num9 = Main.expertMode ? 40 : 50;
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center.X, ((Entity) npc).Center.Y, num6, num8, 36, num9, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      SoundEngine.PlaySound(ref SoundID.Item38, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      npc.ai[2] = 0.0f;
      npc.ai[1] = 0.0f;
      npc.ai[3] = 0.0f;
      npc.netUpdate = true;
    }
  }
}
