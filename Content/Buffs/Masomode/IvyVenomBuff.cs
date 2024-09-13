// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.IvyVenomBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class IvyVenomBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = true;
    }

    public virtual bool ReApply(Player player, int time, int buffIndex)
    {
      player.buffTime[buffIndex] += time;
      return false;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      if (player.buffTime[buffIndex] > 1200)
      {
        player.AddBuff(ModContent.BuffType<NeurotoxinBuff>(), player.buffTime[buffIndex], true, false);
        player.buffTime[buffIndex] = 1;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        if (((Entity) player).whoAmI == Main.myPlayer)
          Main.NewText(Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Buffs.IvyVenomBuff.Transform"), (byte) 175, (byte) 75, byte.MaxValue);
      }
      player.venom = true;
    }
  }
}
