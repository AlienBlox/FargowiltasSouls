// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.GladiatorSpears
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class GladiatorSpears : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<WillHeader>();

    public override int ToggleItemType => ModContent.ItemType<GladiatorEnchant>();

    public override bool ExtraAttackEffect => true;

    public override void OnHitNPCEither(
      Player player,
      NPC target,
      NPC.HitInfo hitInfo,
      DamageClass damageClass,
      int baseDamage,
      Projectile projectile,
      Item item)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (((Entity) player).whoAmI != Main.myPlayer || fargoSoulsPlayer.GladiatorCD > 0 || projectile != null && projectile.type == ModContent.ProjectileType<GladiatorJavelin>())
        return;
      bool flag = player.HasBuff<GladiatorBuff>();
      int num1 = baseDamage / (flag ? 3 : 5);
      if (num1 <= 0)
        return;
      if (!fargoSoulsPlayer.TerrariaSoul)
        num1 = Math.Min(num1, FargoSoulsUtil.HighestDamageTypeScaling(player, 300));
      Item obj = this.EffectItem(player);
      for (int index = 0; index < 3; ++index)
      {
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector(((Entity) target).Center.X + Utils.NextFloat(Main.rand, -300f, 300f), ((Entity) target).Center.Y - (float) Main.rand.Next(600, 801));
        Vector2 vector2_2 = Vector2.op_Addition(((Entity) target).Center, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) target).velocity, 15f), Utils.NextFloat(Main.rand, 0.7f, 1.3f)));
        Vector2 vector2_3 = Vector2.op_Subtraction(vector2_2, vector2_1);
        float num2 = ((Vector2) ref vector2_3).Length() / 15f * Utils.NextFloat(Main.rand, 0.8f, 1.2f);
        Projectile.NewProjectile(player.GetSource_Accessory(obj, (string) null), vector2_1, Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.Normalize(Vector2.op_Subtraction(vector2_2, vector2_1)), 0.15707963705062866), num2), ModContent.ProjectileType<GladiatorJavelin>(), num1, 4f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      fargoSoulsPlayer.GladiatorCD = fargoSoulsPlayer.ForceEffect<GladiatorEnchant>() ? 10 : 30;
      fargoSoulsPlayer.GladiatorCD = flag ? fargoSoulsPlayer.GladiatorCD : (int) Math.Round((double) fargoSoulsPlayer.GladiatorCD * 1.5);
    }
  }
}
