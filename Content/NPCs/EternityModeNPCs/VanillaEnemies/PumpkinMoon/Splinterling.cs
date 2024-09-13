// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PumpkinMoon.Splinterling
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PumpkinMoon
{
  public class Splinterling : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(329);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.Counter < 300)
        return;
      this.Counter = 0;
      if (!FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < 5; ++index)
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center.X, ((Entity) npc).Center.Y, (float) Main.rand.Next(-3, 4), (float) Main.rand.Next(-5, 0), Main.rand.Next(326, 329), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (NPC.downedPlantBoss)
        return base.CheckDead(npc);
      ((Entity) npc).active = false;
      if (npc.DeathSound.HasValue)
      {
        SoundStyle soundStyle = npc.DeathSound.Value;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      }
      return false;
    }
  }
}
