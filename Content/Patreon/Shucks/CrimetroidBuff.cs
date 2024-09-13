// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Shucks.CrimetroidBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Shucks
{
  public class CrimetroidBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoTimeDisplay[this.Type] = true;
      Main.vanityPet[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.buffTime[buffIndex] = 18000;
      player.GetModPlayer<PatreonPlayer>().Crimetroid = true;
      if (((Entity) player).whoAmI != Main.myPlayer || player.ownedProjectileCounts[ModContent.ProjectileType<Crimetroid>()] >= 1)
        return;
      FargoSoulsUtil.NewSummonProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<Crimetroid>(), 1, 1f, ((Entity) player).whoAmI);
    }
  }
}
