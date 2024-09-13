// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.SkeletonArcher
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class SkeletonArcher : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(110);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if ((double) npc.ai[2] <= 0.0 || (double) npc.ai[1] > 40.0)
        return;
      if (FargoSoulsUtil.HostCheck)
      {
        Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
        vector2_1.Y -= Math.Abs(vector2_1.X) * 0.075f;
        vector2_1.X += (float) Main.rand.Next(-24, 25);
        vector2_1.Y += (float) Main.rand.Next(-24, 25);
        ((Vector2) ref vector2_1).Normalize();
        Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 11f);
        int num = Main.expertMode ? 28 : 35;
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2_2, ModContent.ProjectileType<SkeletonArcherArrow>(), num, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      SoundEngine.PlaySound(ref SoundID.Item5, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      npc.ai[2] = 0.0f;
      npc.ai[1] = 0.0f;
      npc.netUpdate = true;
    }
  }
}
