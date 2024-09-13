// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.RefreshedBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class RefreshedBuff : ModBuff
  {
    public virtual void SetStaticDefaults() => Main.buffNoSave[this.Type] = true;

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.FargoSouls().WingTimeModifier += 0.5f;
      player.hasMagiluminescence = true;
      player.frogLegJumpBoost = true;
    }
  }
}
