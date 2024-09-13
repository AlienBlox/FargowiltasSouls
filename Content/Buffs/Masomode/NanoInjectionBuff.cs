﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.NanoInjectionBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class NanoInjectionBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.FargoSouls().NanoInjection = true;
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Subtraction(local, 0.1f);
      player.moveSpeed -= 0.1f;
      Player player1 = player;
      player1.statDefense = Player.DefenseStat.op_Subtraction(player1.statDefense, 10);
    }
  }
}