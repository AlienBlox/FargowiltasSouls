// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.FrostMoon.ElfCopter
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.FrostMoon
{
  public class ElfCopter : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(347);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if ((double) npc.localAI[0] < 14.0)
        return;
      npc.localAI[0] = 0.0f;
      if (!FargoSoulsUtil.HostCheck)
        return;
      float num1 = ((Entity) Main.player[npc.target]).Center.X - ((Entity) npc).Center.X;
      double num2 = (double) ((Entity) Main.player[npc.target]).Center.Y - (double) ((Entity) npc).Center.Y;
      float num3 = num1 + (float) Main.rand.Next(-35, 36);
      double num4 = (double) Main.rand.Next(-35, 36);
      double num5 = num2 + num4;
      float num6 = num3 * (float) (1.0 + (double) Main.rand.Next(-20, 21) * 0.014999999664723873);
      double num7 = 1.0 + (double) Main.rand.Next(-20, 21) * 0.014999999664723873;
      float num8 = (float) (num5 * num7);
      float num9 = 10f / (float) Math.Sqrt((double) num6 * (double) num6 + (double) num8 * (double) num8);
      double num10 = (double) num6 * (double) num9;
      float num11 = num8 * num9;
      double num12 = 1.0 + (double) Main.rand.Next(-20, 21) * 0.012500000186264515;
      float num13 = (float) (num10 * num12);
      float num14 = num11 * (float) (1.0 + (double) Main.rand.Next(-20, 21) * 0.012500000186264515);
      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center.X, ((Entity) npc).Center.Y, num13, num14, ModContent.ProjectileType<ElfCopterBullet>(), 32, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }
  }
}
