// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Boss.BaronsBurdenBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Boss
{
  public class BaronsBurdenBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.buffNoSave[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.gills = true;
      player.ignoreWater = true;
      if (((Entity) player).wet)
        return;
      ((Entity) player).velocity.Y += 0.4f;
      ((Entity) player).velocity.X *= 0.9f;
      if (player.statLife <= 10)
        return;
      player.lifeRegen = -90;
    }
  }
}
