// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.ShadowBalls
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class ShadowBalls : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<ShadowHeader>();

    public override int ToggleItemType => ModContent.ItemType<ShadowEnchant>();

    public override bool MinionEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      int ownedProjectileCount = player.ownedProjectileCounts[ModContent.ProjectileType<ShadowEnchantOrb>()];
      int num1 = 2;
      bool shadowEnchantActive = fargoSoulsPlayer.AncientShadowEnchantActive;
      bool flag = fargoSoulsPlayer.ForceEffect<ShadowEnchant>();
      if (fargoSoulsPlayer.TerrariaSoul)
        num1 = 5;
      else if (flag & shadowEnchantActive)
        num1 = 4;
      else if (shadowEnchantActive | flag)
        num1 = 3;
      if (ownedProjectileCount == 0)
      {
        float num2 = 6.28318548f / (float) num1;
        for (int index1 = 0; index1 < num1; ++index1)
        {
          Vector2 vector2 = Vector2.op_Addition(((Entity) player).Center, Utils.RotatedBy(new Vector2(60f, 0.0f), (double) num2 * (double) index1, new Vector2()));
          int index2 = Projectile.NewProjectile(((Entity) player).GetSource_Misc(""), vector2, Vector2.Zero, ModContent.ProjectileType<ShadowEnchantOrb>(), 0, 10f, ((Entity) player).whoAmI, 0.0f, num2 * (float) index1, 0.0f);
          Main.projectile[index2].FargoSouls().CanSplit = false;
        }
      }
      else if (ownedProjectileCount < num1 && fargoSoulsPlayer.ShadowOrbRespawnTimer <= 0 || ownedProjectileCount > num1)
      {
        fargoSoulsPlayer.ShadowOrbRespawnTimer = 600;
        for (int index = 0; index < Main.maxProjectiles; ++index)
        {
          Projectile projectile = Main.projectile[index];
          if (((Entity) projectile).active && projectile.type == ModContent.ProjectileType<ShadowEnchantOrb>() && projectile.owner == ((Entity) player).whoAmI)
            projectile.Kill();
        }
        float num3 = 6.28318548f / (float) num1;
        for (int index3 = 0; index3 < num1; ++index3)
        {
          Vector2 vector2 = Vector2.op_Addition(((Entity) player).Center, Utils.RotatedBy(new Vector2(60f, 0.0f), (double) num3 * (double) index3, new Vector2()));
          int index4 = Projectile.NewProjectile(this.GetSource_EffectItem(player), vector2, Vector2.Zero, ModContent.ProjectileType<ShadowEnchantOrb>(), 0, 10f, ((Entity) player).whoAmI, 0.0f, num3 * (float) index3, 0.0f);
          Main.projectile[index4].FargoSouls().CanSplit = false;
        }
      }
      --fargoSoulsPlayer.ShadowOrbRespawnTimer;
    }
  }
}
