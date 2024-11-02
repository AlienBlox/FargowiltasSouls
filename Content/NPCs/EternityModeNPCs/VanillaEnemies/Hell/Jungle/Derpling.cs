// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Jungle.Derpling
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Jungle
{
  public class Derpling : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(177);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.scale *= 0.5f;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      EModeGlobalNPC.Aura(npc, 200f, 30, dustid: 5, color: new Color());
      if (++this.Counter <= 10)
        return;
      this.Counter = 0;
      if (!((Entity) Main.LocalPlayer).active || Main.LocalPlayer.ghost || Main.LocalPlayer.dead || !Main.LocalPlayer.bleed || (double) ((Entity) npc).Distance(((Entity) Main.LocalPlayer).Center) >= 200.0)
        return;
      Player localPlayer = Main.LocalPlayer;
      localPlayer.statLife -= 5;
      CombatText.NewText(((Entity) localPlayer).Hitbox, Color.Red, 5, false, true);
      if (localPlayer.statLife < 0)
        localPlayer.KillMe(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.FargowiltasSouls.DeathMessage.Derpling", (object) localPlayer.name)), 999.0, 0, false);
      npc.life += 5;
      if (npc.life > npc.lifeMax)
        npc.life = npc.lifeMax;
      npc.HealEffect(5, true);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(31, 180, true, false);
    }
  }
}
