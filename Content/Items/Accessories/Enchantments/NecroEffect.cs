// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.NecroEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class NecroEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<ShadowHeader>();

    public override int ToggleItemType => ModContent.ItemType<NecroEnchant>();

    public override bool ExtraAttackEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.NecroCD == 0)
        return;
      --fargoSoulsPlayer.NecroCD;
    }

    public static void NecroSpawnGraveEnemy(NPC npc, Player player, FargoSoulsPlayer modPlayer)
    {
      if (player.ownedProjectileCounts[ModContent.ProjectileType<NecroGrave>()] >= 15)
        return;
      int num = npc.lifeMax / 3;
      if (num <= 0)
        return;
      Projectile.NewProjectile(player.GetSource_Accessory(player.EffectItem<NecroEffect>(), (string) null), ((Entity) npc).Bottom, new Vector2(0.0f, -4f), ModContent.ProjectileType<NecroGrave>(), 0, 0.0f, ((Entity) player).whoAmI, (float) num, 0.0f, 0.0f);
    }

    public static void NecroSpawnGraveBoss(
      FargoSoulsGlobalNPC globalNPC,
      NPC npc,
      Player player,
      int damage)
    {
      globalNPC.NecroDamage += damage;
      if (globalNPC.NecroDamage <= npc.lifeMax / 10 || player.ownedProjectileCounts[ModContent.ProjectileType<NecroGrave>()] >= 45)
        return;
      globalNPC.NecroDamage = 0;
      int num = npc.lifeMax / 25;
      if (num <= 0)
        return;
      Projectile.NewProjectile(player.GetSource_Accessory(player.EffectItem<NecroEffect>(), (string) null), ((Entity) npc).Bottom, new Vector2(0.0f, -4f), ModContent.ProjectileType<NecroGrave>(), 0, 0.0f, ((Entity) player).whoAmI, (float) num, 0.0f, 0.0f);
    }
  }
}
