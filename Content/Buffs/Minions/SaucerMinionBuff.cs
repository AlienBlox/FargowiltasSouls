﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Minions.SaucerMinionBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Minions
{
  public class SaucerMinionBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoTimeDisplay[this.Type] = true;
      Main.buffNoSave[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.FargoSouls().MiniSaucer = true;
      if (((Entity) player).whoAmI != Main.myPlayer || player.ownedProjectileCounts[ModContent.ProjectileType<MiniSaucer>()] >= 1)
        return;
      FargoSoulsUtil.NewSummonProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<MiniSaucer>(), 50, 3f, ((Entity) player).whoAmI);
    }
  }
}