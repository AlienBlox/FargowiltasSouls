// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Souls.TimeFrozenBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Cosmos;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Souls
{
  public class TimeFrozenBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoSave[this.Type] = true;
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = false;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      if (player.mount.Active)
        player.mount.Dismount(player);
      player.controlLeft = false;
      player.controlRight = false;
      player.controlJump = false;
      player.controlDown = false;
      player.controlUseItem = false;
      player.controlUseTile = false;
      player.controlHook = false;
      player.controlMount = false;
      ((Entity) player).velocity = ((Entity) player).oldVelocity;
      ((Entity) player).position = ((Entity) player).oldPosition;
      player.FargoSouls().MutantNibble = true;
      player.FargoSouls().NoUsingItems = 2;
      FargowiltasSouls.FargowiltasSouls.ManageMusicTimestop(player.buffTime[buffIndex] < 5);
      if (Main.dedServ || ((Entity) player).whoAmI != Main.myPlayer)
        return;
      ManagedScreenFilter filter = ShaderManager.GetFilter("FargowiltasSouls.Invert");
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.championBoss, ModContent.NPCType<CosmosChampion>()) && (double) Main.npc[EModeGlobalNPC.championBoss].ai[0] == 15.0)
        filter.SetFocusPosition(((Entity) Main.npc[EModeGlobalNPC.championBoss]).Center);
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()) && WorldSavingSystem.MasochistModeReal && (double) Main.npc[EModeGlobalNPC.mutantBoss].ai[0] == -5.0)
        filter.SetFocusPosition(((Entity) Main.npc[EModeGlobalNPC.mutantBoss]).Center);
      if (player.buffTime[buffIndex] > 60)
        filter.Activate();
      if (player.buffTime[buffIndex] == 90)
      {
        SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ZaWarudoResume", (SoundType) 0);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
      }
      if (!SoulConfig.Instance.ForcedFilters || Main.WaveQuality != 0)
        return;
      Main.WaveQuality = 1;
    }

    public virtual void Update(NPC npc, ref int buffIndex) => npc.FargoSouls().TimeFrozen = true;
  }
}
