// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.BerserkedBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class BerserkedBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 0.1f);
      player.FargoSouls().Berserked = true;
      player.moveSpeed += 0.1f;
      player.endurance -= 0.1f;
    }

    public virtual bool ReApply(Player player, int time, int buffIndex) => time > 3;
  }
}
