// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.DeerclawpsEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class DeerclawpsEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<LumpofFleshHeader>();

    public override int ToggleItemType => ModContent.ItemType<Deerclawps>();

    public static void DeerclawpsAttack(Player player, Vector2 pos)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      Vector2 vector2 = Vector2.op_Multiply(16f, Vector2.op_UnaryNegation(Utils.RotatedByRandom(Vector2.UnitY, (double) MathHelper.ToRadians(30f))));
      int num1 = 32;
      int num2 = 961;
      float num3 = -15f;
      float num4 = Utils.NextFloat(Main.rand, 0.5f, 1f);
      if (player.FargoSouls().LumpOfFlesh)
      {
        num1 = 48;
        num2 = 756;
        num3 *= 2f;
        num4 += 0.5f;
      }
      int num5 = (int) ((double) num1 * (double) player.ActualClassDamage(DamageClass.Melee));
      if ((double) ((Entity) player).velocity.Y == 0.0)
        Projectile.NewProjectile(player.GetSource_EffectItem<DeerclawpsEffect>(), pos, vector2, num2, num5, 4f, Main.myPlayer, num3, num4, 0.0f);
      else
        Projectile.NewProjectile(player.GetSource_EffectItem<DeerclawpsEffect>(), pos, Vector2.op_Multiply(vector2, Utils.NextBool(Main.rand) ? 1f : -1f), num2, num5, 4f, Main.myPlayer, num3, num4 / 2f, 0.0f);
    }
  }
}
