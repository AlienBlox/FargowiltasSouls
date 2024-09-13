// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Solar.SolarSpearman
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
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
  public class SolarSpearman : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(518);

    public virtual bool CheckDead(NPC npc)
    {
      int index = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
      if (index != -1 && ((Entity) Main.player[index]).active && !Main.player[index].dead && FargoSoulsUtil.HostCheck)
      {
        Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[index]).Center, ((Entity) npc).Center);
        ((Vector2) ref vector2).Normalize();
        vector2 = Vector2.op_Multiply(vector2, 14f);
        Projectile.NewProjectile(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<DrakanianDaybreak>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 1f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      if (!Utils.NextBool(Main.rand))
        return base.CheckDead(npc);
      npc.Transform(419);
      return false;
    }
  }
}
