// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.BeetleEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class BeetleEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<LifeHeader>();

    public override int ToggleItemType => ModContent.ItemType<BeetleEnchant>();

    public override void OnHitNPCEither(
      Player player,
      NPC target,
      NPC.HitInfo hitInfo,
      DamageClass damageClass,
      int baseDamage,
      Projectile projectile,
      Item item)
    {
      if (!player.beetleOffense || damageClass == DamageClass.Melee)
        return;
      player.beetleCounter += (float) ((NPC.HitInfo) ref hitInfo).Damage;
    }

    public override void OnHurt(Player player, Player.HurtInfo info)
    {
      player.FargoSouls().BeetleEnchantDefenseTimer = 600;
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      int num1 = -1;
      for (int index = 0; index < Player.MaxBuffs; ++index)
      {
        if (player.buffTime[index] > 0 && (player.buffType[index] == 98 || player.buffType[index] == 99 || player.buffType[index] == 100))
        {
          if (num1 < player.buffType[index])
            num1 = player.buffType[index];
          player.DelBuff(index);
          --index;
        }
      }
      if (num1 == -1)
        return;
      int num2 = 98 - num1;
      player.AddBuff(95 + num2, 5, false, false);
    }
  }
}
