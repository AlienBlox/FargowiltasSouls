// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.FrostMoon.ElfArcher
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
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.FrostMoon
{
  public class ElfArcher : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(350);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if ((double) npc.ai[2] <= 0.0 || (double) npc.ai[1] > 60.0)
        return;
      if (FargoSoulsUtil.HostCheck)
      {
        Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
        vector2_1.Y -= Math.Abs(vector2_1.X) * 0.1f;
        vector2_1.X += (float) Main.rand.Next(-20, 21);
        vector2_1.Y += (float) Main.rand.Next(-20, 21);
        ((Vector2) ref vector2_1).Normalize();
        Vector2 vector2_2 = vector2_1;
        Vector2 vector2_3 = Vector2.op_Multiply(vector2_1, 7f);
        int num1 = Main.expertMode ? 36 : 45;
        float num2 = 0.3141593f;
        int num3 = 5;
        Vector2 vector2_4 = Vector2.op_Multiply(vector2_2, 40f);
        bool flag = Collision.CanHit(((Entity) npc).Center, 0, 0, Vector2.op_Addition(((Entity) npc).Center, vector2_4), 0, 0);
        for (int index1 = 0; index1 < num3; ++index1)
        {
          float num4 = (float) index1 - (float) (((double) num3 - 1.0) / 2.0);
          Vector2 vector2_5 = Utils.RotatedBy(vector2_4, (double) num2 * (double) num4, new Vector2());
          if (!flag)
            vector2_5 = Vector2.op_Subtraction(vector2_5, vector2_4);
          int index2 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, vector2_5), vector2_3, ModContent.ProjectileType<ElfArcherArrow>(), num1, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          Main.projectile[index2].noDropItem = true;
        }
      }
      SoundEngine.PlaySound(ref SoundID.Item5, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      npc.ai[2] = 0.0f;
      npc.ai[1] = 0.0f;
      npc.netUpdate = true;
    }
  }
}
