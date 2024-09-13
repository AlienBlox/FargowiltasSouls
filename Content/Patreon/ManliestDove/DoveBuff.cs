// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.ManliestDove.DoveBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.ManliestDove
{
  public class DoveBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoTimeDisplay[this.Type] = true;
      Main.vanityPet[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.buffTime[buffIndex] = 18000;
      player.GetModPlayer<PatreonPlayer>().DovePet = true;
      if (player.ownedProjectileCounts[ModContent.ProjectileType<DoveProj>()] > 0 || ((Entity) player).whoAmI != Main.myPlayer)
        return;
      Projectile.NewProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).position.X + (float) (((Entity) player).width / 2), ((Entity) player).position.Y + (float) (((Entity) player).height / 2), 0.0f, 0.0f, ModContent.ProjectileType<DoveProj>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }
  }
}
