// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.HallowEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class HallowEffect : AccessoryEffect
  {
    public const int RepelRadius = 350;

    public override Header ToggleHeader => (Header) Header.GetHeader<SpiritHeader>();

    public override int ToggleItemType => ModContent.ItemType<HallowEnchant>();

    public static void HealRepel(Player player)
    {
      SoundEngine.PlaySound(ref SoundID.Item72, new Vector2?(), (SoundUpdateCallback) null);
      new HallowEnchantBarrier(((Entity) player).Center, Vector2.Zero, 35f / 16f, 32).Spawn();
      foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => p.hostile && FargoSoulsUtil.CanDeleteProjectile(p) && (double) ((Entity) p).Distance(((Entity) player).Center) <= 350.0)))
      {
        ((Entity) projectile).velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) projectile).Center, ((Entity) player).Center)), ((Vector2) ref ((Entity) projectile).velocity).Length());
        projectile.hostile = false;
      }
    }
  }
}
