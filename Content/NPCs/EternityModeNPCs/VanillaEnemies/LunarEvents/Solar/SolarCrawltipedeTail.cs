// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar.SolarCrawltipedeTail
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar
{
  public class SolarCrawltipedeTail : EModeNPCBehaviour
  {
    public int Timer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(414);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.trapImmune = true;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (this.Timer < 4)
        return;
      this.Timer = 0;
      int index1 = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
      if (index1 == -1)
        return;
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[index1]).Center, ((Entity) npc).Center);
      if ((double) ((Vector2) ref vector2_1).Length() >= 400.0)
        return;
      ((Vector2) ref vector2_1).Normalize();
      Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 6f);
      int index2 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2_2, 188, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      Main.projectile[index2].friendly = false;
      SoundEngine.PlaySound(ref SoundID.Item34, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
    }
  }
}
