// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Nebula.NebulaBrain
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Nebula
{
  public class NebulaBrain : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(420);

    public virtual void AI(NPC npc) => base.AI(npc);

    public virtual bool CheckDead(NPC npc)
    {
      if (npc.HasValidTarget)
      {
        Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
        ((Vector2) ref vector2_1).Normalize();
        Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 4.5f);
        for (int index = 0; index < (int) npc.localAI[2] / 60; ++index)
        {
          Vector2 position = ((Entity) npc).position;
          position.X += (float) Main.rand.Next(((Entity) npc).width);
          position.Y += (float) Main.rand.Next(((Entity) npc).height);
          Vector2 vector2_3 = Vector2.op_Multiply(Utils.RotatedBy(vector2_2, (double) MathHelper.ToRadians((float) Main.rand.Next(-20, 21)), new Vector2()), Utils.NextFloat(Main.rand, 0.8f, 1.2f));
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), position, vector2_3, 576, 48, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      return base.CheckDead(npc);
    }
  }
}
