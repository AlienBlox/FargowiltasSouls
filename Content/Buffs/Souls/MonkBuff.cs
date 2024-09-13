// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Souls.MonkBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Souls
{
  public class MonkBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoSave[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      if (((Entity) player).whoAmI != Main.myPlayer || player.HasEffect<MonkDashEffect>())
        player.buffTime[buffIndex] = 2;
      if (player.mount.Active)
        return;
      player.FargoSouls().HasDash = true;
      player.FargoSouls().FargoDash = DashManager.DashType.Monk;
    }
  }
}
