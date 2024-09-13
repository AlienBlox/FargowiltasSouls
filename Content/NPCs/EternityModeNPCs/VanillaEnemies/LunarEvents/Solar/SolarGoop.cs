// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar.SolarGoop
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar
{
  public class SolarGoop : EModeNPCBehaviour
  {
    public int Timer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(519);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.noTileCollide = true;
      npc.lavaImmune = true;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      ++this.Timer;
      if (this.Timer >= 300)
      {
        npc.life = 0;
        npc.HitEffect(0, 10.0, new bool?());
        npc.checkDead();
        ((Entity) npc).active = false;
      }
      if (npc.HasPlayerTarget)
      {
        Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
        ((Vector2) ref vector2_1).Normalize();
        Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 12f);
        ((Entity) npc).velocity.X += vector2_2.X / 100f;
        if ((double) ((Vector2) ref ((Entity) npc).velocity).Length() > 16.0)
        {
          ((Vector2) ref ((Entity) npc).velocity).Normalize();
          NPC npc1 = npc;
          ((Entity) npc1).velocity = Vector2.op_Multiply(((Entity) npc1).velocity, 16f);
        }
      }
      else if (this.Timer % 10 == 0)
        npc.TargetClosest(false);
      npc.dontTakeDamage = true;
    }
  }
}
