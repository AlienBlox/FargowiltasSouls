// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.FossilBones
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class FossilBones : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<SpiritHeader>();

    public override int ToggleItemType => ModContent.ItemType<FossilEnchant>();

    public override void OnHurt(Player player, Player.HurtInfo info)
    {
      int damage = ((Player.HurtInfo) ref info).Damage;
      for (int index = 0; index < 5 && damage >= 30; ++index)
      {
        damage -= 30;
        float num1 = (float) Main.rand.Next(-5, 6) * 3f;
        float num2 = (float) Main.rand.Next(-5, 6) * 3f;
        Projectile.NewProjectile(this.GetSource_EffectItem(player), ((Entity) player).position.X + num1, ((Entity) player).position.Y + num2, num1, num2, ModContent.ProjectileType<FossilBone>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
    }
  }
}
