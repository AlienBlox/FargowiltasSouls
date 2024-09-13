// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.ClippedEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class ClippedEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<BionomicHeader>();

    public override int ToggleItemType => ModContent.ItemType<WyvernFeather>();

    public override void OnHitNPCEither(
      Player player,
      NPC target,
      NPC.HitInfo hitInfo,
      DamageClass damageClass,
      int baseDamage,
      Projectile projectile,
      Item item)
    {
      if (target.boss || target.buffImmune[ModContent.BuffType<ClippedWingsBuff>()] || !Utils.NextBool(Main.rand, 10))
        return;
      ((Entity) target).velocity.X = 0.0f;
      ((Entity) target).velocity.Y = 10f;
      target.AddBuff(ModContent.BuffType<ClippedWingsBuff>(), 240, false);
      target.netUpdate = true;
    }
  }
}
