// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.FusedBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class FusedBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.FargoSouls().Fused = true;
      if (player.buffTime[buffIndex] != 2)
        return;
      player.immune = false;
      player.immuneTime = 0;
      int num = (int) ((double) Math.Max(player.statLife, player.statLifeMax) * 2.0 / 3.0);
      player.Hurt(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.FargowiltasSouls.DeathMessage.Fused", (object) player.name)), num, 0, false, false, -1, false, 0.0f, 0.0f, 4.5f);
      Projectile.NewProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<FusedExplosion>(), num, 12f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public virtual bool ReApply(NPC npc, int time, int buffIndex) => true;
  }
}
