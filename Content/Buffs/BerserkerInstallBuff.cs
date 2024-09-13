// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.BerserkerInstallBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs
{
  public class BerserkerInstallBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.buffNoSave[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
    }

    public static void DebuffPlayerStats(Player player)
    {
      player.endurance -= 0.3f;
      Player player1 = player;
      player1.statDefense = Player.DefenseStat.op_Subtraction(player1.statDefense, 30);
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      BerserkerInstallBuff.DebuffPlayerStats(player);
      player.FargoSouls().Berserked = true;
      player.FargoSouls().AttackSpeed += 0.5f;
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 0.2f);
      player.GetCritChance(DamageClass.Generic) += 20f;
      player.moveSpeed += 0.2f;
      player.hasMagiluminescence = true;
      player.noKnockback = true;
      if (!player.controlLeft && !player.controlRight)
      {
        if ((double) ((Entity) player).velocity.X > 0.0)
          player.controlRight = true;
        else if ((double) ((Entity) player).velocity.X < 0.0)
          player.controlLeft = true;
        else if (((Entity) player).direction > 0)
          player.controlRight = true;
        else
          player.controlLeft = true;
      }
      if (player.buffTime[buffIndex] > 2)
        player.FargoSouls().NoMomentum = true;
      if (player.buffTime[buffIndex] != 2)
        return;
      int num = 120;
      player.AddBuff(ModContent.BuffType<BerserkerInstallCDBuff>(), num, true, false);
      player.AddBuff(ModContent.BuffType<StunnedBuff>(), num, true, false);
    }
  }
}
