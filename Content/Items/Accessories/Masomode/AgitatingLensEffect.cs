// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.AgitatingLensEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class AgitatingLensEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<SupremeFairyHeader>();

    public override int ToggleItemType => ModContent.ItemType<AgitatingLens>();

    public override bool ExtraAttackEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.AgitatingLensCD++ <= 15)
        return;
      fargoSoulsPlayer.AgitatingLensCD = 0;
      if ((double) Math.Abs(((Entity) player).velocity.X) < 5.0 && (double) Math.Abs(((Entity) player).velocity.Y) < 5.0 || ((Entity) player).whoAmI != Main.myPlayer)
        return;
      int num1 = 18;
      if (fargoSoulsPlayer.SupremeDeathbringerFairy)
        num1 *= 2;
      if (fargoSoulsPlayer.MasochistSoul)
        num1 *= 2;
      int num2 = (int) ((double) num1 * (double) player.ActualClassDamage(DamageClass.Magic));
      Projectile.NewProjectile(this.GetSource_EffectItem(player), ((Entity) player).Center, Vector2.op_Multiply(((Entity) player).velocity, 0.1f), ModContent.ProjectileType<BloodScytheFriendly>(), num2, 5f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }
  }
}
