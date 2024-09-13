// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.BossBags.DeviBag
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.BossBags
{
  public class DeviBag : BossBag
  {
    protected override bool IsPreHMBag => true;

    public virtual void RightClick(Player player)
    {
      for (int index = -1; index < 2; ++index)
      {
        Vector2 vector2 = Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitY, 0.5f), Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, 6f), (float) index));
        Projectile.NewProjectile(player.GetSource_OpenItem(this.Type, (string) null), ((Entity) player).Center, vector2, 371, 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
      base.RightClick(player);
    }

    public virtual void ModifyItemLoot(ItemLoot itemLoot)
    {
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(ModContent.ItemType<DeviatingEnergy>(), 1, 15, 30));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>()));
    }
  }
}
