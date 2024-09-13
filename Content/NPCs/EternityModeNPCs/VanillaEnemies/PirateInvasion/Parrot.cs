// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PirateInvasion.Parrot
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PirateInvasion
{
  public class Parrot : NoclipFliers
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(252);

    public override void AI(NPC npc)
    {
      base.AI(npc);
      this.CanNoclip = true;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<SqueakyToyBuff>(), 120, true, false);
      target.AddBuff(ModContent.BuffType<MidasBuff>(), 600, true, false);
      if (!WorldSavingSystem.MasochistModeReal || npc.type != 252 || target.Male)
        return;
      target.KillMe(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.FargowiltasSouls.DeathMessage.Parrots", (object) target.name)), 999999.0, 0, false);
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (Main.hardMode)
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
