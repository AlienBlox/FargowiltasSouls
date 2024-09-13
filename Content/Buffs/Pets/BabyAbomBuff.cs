// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Pets.BabyAbomBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Pets
{
  public class BabyAbomBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoTimeDisplay[this.Type] = true;
      Main.vanityPet[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.buffTime[buffIndex] = 18000;
      player.FargoSouls().BabyAbom = true;
      if (player.ownedProjectileCounts[ModContent.ProjectileType<BabyAbom>()] > 0 || ((Entity) player).whoAmI != Main.myPlayer)
        return;
      Projectile.NewProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<BabyAbom>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }
  }
}
