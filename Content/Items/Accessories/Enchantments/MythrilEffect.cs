// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.MythrilEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Weapons.BossDrops;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class MythrilEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<EarthHeader>();

    public override int ToggleItemType => ModContent.ItemType<MythrilEnchant>();

    public static void CalcMythrilAttackSpeed(FargoSoulsPlayer modPlayer, Item item)
    {
      if (item.DamageType == DamageClass.Default || item.pick != 0 || item.axe != 0 || item.hammer != 0 || item.type == ModContent.ItemType<PrismaRegalia>())
        return;
      float num = Math.Max((float) modPlayer.MythrilTimer / (float) modPlayer.MythrilMaxTime, 0.0f);
      modPlayer.AttackSpeed += modPlayer.MythrilMaxSpeedBonus * num;
    }

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      int num = fargoSoulsPlayer.MythrilMaxTime - 300;
      if (fargoSoulsPlayer.WeaponUseTimer > 0)
      {
        --fargoSoulsPlayer.MythrilTimer;
      }
      else
      {
        ++fargoSoulsPlayer.MythrilTimer;
        if (fargoSoulsPlayer.MythrilTimer == fargoSoulsPlayer.MythrilMaxTime - 1 && ((Entity) player).whoAmI == Main.myPlayer)
        {
          SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ChargeSound", (SoundType) 0);
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        }
      }
      if (fargoSoulsPlayer.MythrilTimer > fargoSoulsPlayer.MythrilMaxTime)
        fargoSoulsPlayer.MythrilTimer = fargoSoulsPlayer.MythrilMaxTime;
      if (fargoSoulsPlayer.MythrilTimer >= num)
        return;
      fargoSoulsPlayer.MythrilTimer = num;
    }
  }
}
