// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.VileSpitEaterofWorlds
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Corruption;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class VileSpitEaterofWorlds : VileSpit
  {
    public int SuicideCounter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(666);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.scale *= 2f;
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      npc.dontTakeDamage = true;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.SuicideCounter <= 600)
        return;
      npc.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      if (!WorldSavingSystem.MasochistModeReal || !FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < 8; ++index)
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitY, Math.PI / 4.0 * (double) index, new Vector2()), 2f), 147, 0, 0.0f, Main.myPlayer, 8f, 0.0f, 0.0f);
    }
  }
}
