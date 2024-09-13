// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.MutantNibbleBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class MutantNibbleBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.FargoSouls().MutantNibble = true;
      player.moonLeech = true;
      player.rabid = true;
      if (WorldSavingSystem.MasochistModeReal && Utils.NextBool(Main.rand, 1200))
      {
        switch (Main.rand.Next(10))
        {
          case 0:
            player.AddBuff(ModContent.BuffType<DefenselessBuff>(), Main.rand.Next(300), true, false);
            break;
          case 1:
            player.AddBuff(ModContent.BuffType<LethargicBuff>(), Main.rand.Next(240), true, false);
            break;
          case 2:
            player.AddBuff(ModContent.BuffType<FlippedBuff>(), Main.rand.Next(120), true, false);
            break;
          case 3:
            player.AddBuff(ModContent.BuffType<HexedBuff>(), Main.rand.Next(120), true, false);
            break;
          case 4:
            player.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), Main.rand.Next(120), true, false);
            break;
          case 5:
            player.AddBuff(ModContent.BuffType<PurifiedBuff>(), Main.rand.Next(60), true, false);
            break;
          case 6:
            player.AddBuff(ModContent.BuffType<RottingBuff>(), Main.rand.Next(300), true, false);
            break;
          case 7:
            player.AddBuff(ModContent.BuffType<SqueakyToyBuff>(), Main.rand.Next(120), true, false);
            break;
          case 8:
            player.AddBuff(ModContent.BuffType<UnstableBuff>(), Main.rand.Next(90), true, false);
            break;
          case 9:
            player.AddBuff(ModContent.BuffType<BerserkedBuff>(), Main.rand.Next(180), true, false);
            break;
        }
      }
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 0.2f);
    }

    public virtual void Update(NPC npc, ref int buffIndex) => npc.FargoSouls().MutantNibble = true;
  }
}
