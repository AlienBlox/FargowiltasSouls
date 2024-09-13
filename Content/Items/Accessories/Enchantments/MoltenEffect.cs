// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.MoltenEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class MoltenEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<NatureHeader>();

    public override int ToggleItemType => ModContent.ItemType<MoltenEnchant>();

    public override void PostUpdateEquips(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      player.inferno = true;
      Lighting.AddLight((int) ((double) ((Entity) player).Center.X / 16.0), (int) ((double) ((Entity) player).Center.Y / 16.0), 0.65f, 0.4f, 0.1f);
      int num1 = 24;
      float num2 = 200f;
      int dmg = 20;
      if (player.FargoSouls().ForceEffect<MoltenEnchant>())
      {
        num2 *= 1.5f;
        dmg *= 4;
      }
      int num3 = FargoSoulsUtil.HighestDamageTypeScaling(player, dmg);
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        NPC npc = Main.npc[index];
        if (((Entity) npc).active && !npc.friendly && !npc.dontTakeDamage && (npc.damage != 0 || npc.lifeMax != 5) && (double) Vector2.Distance(((Entity) player).Center, FargoSoulsUtil.ClosestPointInHitbox(((Entity) npc).Hitbox, ((Entity) player).Center)) <= (double) num2)
        {
          if (player.FindBuffIndex(24) == -1)
            player.AddBuff(24, 10, true, false);
          int num4 = 30;
          if (npc.FindBuffIndex(num1) == -1)
            npc.AddBuff(num1, 120, false);
          int num5 = ModContent.BuffType<MoltenAmplifyBuff>();
          if (npc.FindBuffIndex(num5) == -1)
            npc.AddBuff(num5, 10, false);
          if (player.infernoCounter % num4 == 0)
            player.ApplyDamageToNPC(npc, num3, 0.0f, 0, false, (DamageClass) null, false);
        }
      }
    }
  }
}
