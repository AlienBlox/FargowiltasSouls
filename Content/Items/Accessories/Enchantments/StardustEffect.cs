// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.StardustEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class StardustEffect : AccessoryEffect
  {
    public const int TIMESTOP_DURATION = 540;

    public override Header ToggleHeader => (Header) null;

    public override void PostUpdateEquips(Player player)
    {
      player.setStardust = true;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!fargoSoulsPlayer.FreezeTime || fargoSoulsPlayer.freezeLength <= 0)
        return;
      player.buffImmune[ModContent.BuffType<TimeFrozenBuff>()] = true;
      if (fargoSoulsPlayer.freezeLength > 0 && Main.netMode != 2)
      {
        ManagedScreenFilter filter = ShaderManager.GetFilter("FargowiltasSouls.Invert");
        filter.SetFocusPosition(((Entity) player).Center);
        if (fargoSoulsPlayer.freezeLength > 60)
          filter.Activate();
        if (SoulConfig.Instance.ForcedFilters && Main.WaveQuality == 0)
          Main.WaveQuality = 1;
      }
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        NPC npc = Main.npc[index];
        if (((Entity) npc).active && !npc.HasBuff(ModContent.BuffType<TimeFrozenBuff>()))
          npc.AddBuff(ModContent.BuffType<TimeFrozenBuff>(), fargoSoulsPlayer.freezeLength, false);
      }
      for (int index = 0; index < Main.maxProjectiles; ++index)
      {
        Projectile projectile = Main.projectile[index];
        if (((Entity) projectile).active && (!projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]) && !projectile.FargoSouls().TimeFreezeImmune && projectile.FargoSouls().TimeFrozen == 0)
          projectile.FargoSouls().TimeFrozen = fargoSoulsPlayer.freezeLength;
      }
      --fargoSoulsPlayer.freezeLength;
      FargowiltasSouls.FargowiltasSouls.ManageMusicTimestop(fargoSoulsPlayer.freezeLength < 5);
      if (fargoSoulsPlayer.freezeLength == 90 && !Main.dedServ)
      {
        SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ZaWarudoResume", (SoundType) 0);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
      }
      if (fargoSoulsPlayer.freezeLength > 0)
        return;
      fargoSoulsPlayer.FreezeTime = false;
      fargoSoulsPlayer.freezeLength = 540;
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        NPC npc = Main.npc[index];
        if (((Entity) npc).active && !npc.dontTakeDamage && npc.life == 1 && npc.lifeMax > 1)
          npc.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
      }
    }
  }
}
