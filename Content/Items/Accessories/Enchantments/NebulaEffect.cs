// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.NebulaEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class NebulaEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<CosmoHeader>();

    public override int ToggleItemType => ModContent.ItemType<NebulaEnchant>();

    public override bool IgnoresMutantPresence => true;

    public override void PostUpdateMiscEffects(Player player)
    {
      if (player.setNebula)
        return;
      player.setNebula = true;
      if (player.nebulaCD > 0)
        --player.nebulaCD;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.TerrariaSoul || fargoSoulsPlayer.ForceEffect<NebulaEnchant>())
        return;
      if (player.nebulaLevelDamage == 3)
        DecrementBuff(181);
      if (player.nebulaLevelLife == 3)
        DecrementBuff(175);
      if (player.nebulaLevelMana != 3)
        return;
      DecrementBuff(178);

      void DecrementBuff(int buffType)
      {
        for (int index = 0; index < player.buffType.Length; ++index)
        {
          if (player.buffType[index] == buffType && player.buffTime[index] > 3)
          {
            player.buffTime[index] = 3;
            break;
          }
        }
      }
    }

    public override void OnHitNPCEither(
      Player player,
      NPC target,
      NPC.HitInfo hitInfo,
      DamageClass damageClass,
      int baseDamage,
      Projectile projectile,
      Item item)
    {
      if (damageClass == DamageClass.Magic || player.nebulaCD > 0 || !Utils.NextBool(Main.rand, 3))
        return;
      player.nebulaCD = 30;
      int num = Utils.SelectRandom<int>(Main.rand, new int[3]
      {
        3453,
        3454,
        3455
      });
      int index = Item.NewItem(player.GetSource_OpenItem(num, (string) null), (int) ((Entity) target).position.X, (int) ((Entity) target).position.Y, ((Entity) target).width, ((Entity) target).height, num, 1, false, 0, false, false);
      ((Entity) Main.item[index]).velocity.Y = (float) Main.rand.Next(-20, 1) * 0.2f;
      ((Entity) Main.item[index]).velocity.X = (float) ((double) Main.rand.Next(10, 31) * 0.20000000298023224 * (projectile == null ? (double) ((Entity) player).direction : (double) ((Entity) projectile).direction));
      if (Main.netMode != 1)
        return;
      NetMessage.SendData(21, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }
  }
}
