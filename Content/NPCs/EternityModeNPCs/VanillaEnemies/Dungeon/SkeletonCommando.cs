// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon.SkeletonCommando
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon
{
  public class SkeletonCommando : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(293);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if ((double) npc.ai[2] <= 0.0 || (double) npc.ai[1] > 50.0)
        return;
      if (FargoSoulsUtil.HostCheck)
      {
        Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
        vector2.X += (float) Main.rand.Next(-20, 21);
        vector2.Y += (float) Main.rand.Next(-20, 21);
        ((Vector2) ref vector2).Normalize();
        int num = Main.expertMode ? 48 : 60;
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(4f, vector2), 303, num, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(3f, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(10f), new Vector2())), 303, num, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(3f, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(-10f), new Vector2())), 303, num, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      SoundEngine.PlaySound(ref SoundID.Item11, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      npc.ai[2] = 0.0f;
      npc.ai[1] = 0.0f;
      npc.netUpdate = true;
    }
  }
}
