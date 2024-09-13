// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Phupperbat.ChibiiRemiiBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Phupperbat
{
  public class ChibiiRemiiBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoTimeDisplay[this.Type] = true;
      Main.vanityPet[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.buffTime[buffIndex] = 18000;
      player.GetModPlayer<PatreonPlayer>().ChibiiRemii = true;
      if (((Entity) player).whoAmI != Main.myPlayer || player.ownedProjectileCounts[ModContent.ProjectileType<ChibiiRemii>()] >= 1)
        return;
      Projectile.NewProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Top, Vector2.Zero, ModContent.ProjectileType<ChibiiRemii>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }
  }
}
