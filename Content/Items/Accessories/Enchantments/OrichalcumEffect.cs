// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.OrichalcumEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class OrichalcumEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<EarthHeader>();

    public override int ToggleItemType => ModContent.ItemType<OrichalcumEnchant>();

    public override bool ExtraAttackEffect => true;

    public static void OriDotModifier(NPC npc, FargoSoulsPlayer modPlayer, ref int damage)
    {
      float num = 2.5f;
      if (modPlayer.Player.ForceEffect<OrichalcumEffect>())
        num = 3.5f;
      npc.lifeRegen = (int) ((double) npc.lifeRegen * (double) num);
      damage = (int) ((double) damage * (double) num);
      if (!npc.daybreak)
        return;
      npc.lifeRegen /= 2;
      damage /= 2;
    }

    public override void PostUpdateEquips(Player player) => player.onHitPetal = true;

    public override void OnHitNPCWithProj(
      Player player,
      Projectile proj,
      NPC target,
      NPC.HitInfo hit,
      int damageDone)
    {
      if (proj.type != 221)
        return;
      target.AddBuff(ModContent.BuffType<OriPoisonBuff>(), 300, false);
      target.immune[proj.owner] = 2;
    }
  }
}
