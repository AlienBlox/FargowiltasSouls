// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Mounts.TrojanSquirrelMountBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Mounts;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Mounts
{
  public class TrojanSquirrelMountBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoTimeDisplay[this.Type] = true;
      Main.buffNoSave[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.mount.SetMount(ModContent.MountType<TrojanSquirrelMount>(), player, false);
      player.buffTime[buffIndex] = 10;
      player.FargoSouls().SquirrelMount = true;
    }
  }
}
